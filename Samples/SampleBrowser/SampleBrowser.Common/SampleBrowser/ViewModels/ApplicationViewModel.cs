using ActiproSoftware.Extensions;
using ActiproSoftware.Security;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Input;
using ActiproSoftware.UI.Avalonia.Themes;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input.Platform;
using Avalonia.Markup.Xaml;
using System.IO;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides the application view-model.
/// </summary>
public partial class ApplicationViewModel : ObservableObjectBase {

	private bool _arePrivateItemsAvailable = false;
	private bool _isDrawerOpen = true;
	private readonly NavigationService _navigationService = new();
	private ProductData? _productData;
	private double _scaleFactor = 1.0d;
	private bool _showPrivateItems = false;
	private string? _statusMessage;
	private Visual? _viewElement;
	private bool _viewHasCustomStatusBar;
	private bool _viewHasNavigationButtons;
	private IImage? _viewImageSource;
	private ProductItemInfo? _viewItemInfo;
	private string? _viewSubTitle;
	private string? _viewTitle;
	private bool _viewTransitionForward = true;

	private DelegateCommand<object?>? _navigateViewToHomeCommand;
	private DelegateCommand<object>? _navigateViewToItemInfoCommand;
	private DelegateCommand<object?>? _navigateViewToNextItemInfoCommand;
	private DelegateCommand<object?>? _navigateViewToPreviousItemInfoCommand;
	private DelegateCommand<object?>? _notImplementedCommand;
	private DelegateCommand<object>? _openGitHubSampleFolderCommand;
	private DelegateCommand<object>? _openUrlCommand;
	private DelegateCommand<object>? _setScaleFactorCommand;
	private DelegateCommand<UserInterfaceDensity?>? _setUserInterfaceDensityCommand;
	private DelegateCommand<object?>? _toggleAllowFullScreenCommand;
	private DelegateCommand<object?>? _toggleDrawerOpenCommand;
	private DelegateCommand<object?>? _toggleFlowDirectionCommand;
	private DelegateCommand<object?>? _toggleShowPrivateItemsCommand;
	private DelegateCommand<object?>? _viewEnvironmentCommand;

	public const string ProductDataResourceKey = "AppProductData";

	private const string DefaultSampleUri = null;
	//private const string DefaultSampleUri = "https://ActiproSoftware/SampleBrowser/Utilities/ColorPalette/ColorPaletteView";
	//private const string DefaultSampleUri = "https://ActiproSoftware/ProductSamples/DockingSamples/Demos/SimpleIde/MainControl";

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	private ApplicationViewModel() {
		// Internal initialization (used for internal Actipro development only)
		var methodInfo = GetType().GetMethod("InitializePrivate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
		methodInfo?.Invoke(this, []);

		// Load the home view
		NavigateViewToHome(transitionForward: true);

		// Load product data from resources
		var productDataKey = ProductDataResourceKey;
		if (Application.Current?.Resources.TryGetResource(productDataKey, theme: null, out var productDataResource) == true)
			ProductData = productDataResource as ProductData;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates the <see cref="Control"/> for the specified element's XAML path.
	/// </summary>
	/// <param name="path">The element's XAML path.</param>
	[UnconditionalSuppressMessage("Aot", "IL2026:Requires unreferenced code", Justification = "Only local resources are loaded with AvaloniaXamlLoader and this assembly, by design, should not be trimmed.")]
	[UnconditionalSuppressMessage("Aot", "IL2057:Type availability", Justification = "Only local types are created by Activator and this assembly, by design, should not be trimmed.")]
	private static Control? CreateElement(string? path) {
		if (!string.IsNullOrEmpty(path)) {
			// Markdown files display in a DocumentViewer
			if (path.EndsWith(".md", StringComparison.InvariantCulture))
				return DocumentViewer.CreateForMarkdown(path);

			// Get a possible type name from the path
			var typeName = path;
			if (typeName.StartsWith('/'))
				typeName = $"ActiproSoftware{typeName}";
			typeName = typeName.Replace('/', '.');
			typeName += $", {typeof(ApplicationViewModel).Assembly.FullName}";

			// Attempt to create an instance of a type
			Control? control;
			var type = TrustedCodeService.Resolve(typeName, throwOnError: false);
			if (type is not null) {
				control = TrustedCodeService.CreateInstance(type) as Control;
				return control;
			}

			// Attempt to load XAML
			var uri = new Uri($"avares://SampleBrowser" + path + ".axaml", UriKind.Absolute);
			control = AvaloniaXamlLoader.Load(uri) as Control;
			return control;
		}

		return null;
	}

	/// <summary>
	/// Finds the <see cref="ProductItemInfo"/> for the specified <see cref="Uri"/>.
	/// </summary>
	/// <param name="uriString">The <see cref="Uri"/> to examine.</param>
	private ProductItemInfo? FindProductInfo(Uri uri) {
		if ((uri is not null) && (_productData is not null)) {
			var targetPath = uri.LocalPath;

			return _productData.AllItems
				.FirstOrDefault(itemInfo => string.Compare(itemInfo.Path, targetPath, StringComparison.OrdinalIgnoreCase) == 0);
		}

		return null;
	}

	/// <summary>
	/// Returns whether to use a forward transition from one <see cref="ProductItemInfo"/> to another.
	/// </summary>
	/// <param name="fromItemInfo">The from <see cref="ProductItemInfo"/>.</param>
	/// <param name="toItemInfo">The to <see cref="ProductItemInfo"/>.</param>
	private static bool IsTransitionForward(ProductItemInfo? fromItemInfo, ProductItemInfo? toItemInfo) {
		var transitionForward = true;

		if ((fromItemInfo?.ProductFamily is not null) && (toItemInfo is not null)) {
			var oldIndex = fromItemInfo.ProductFamily.Items.IndexOf(fromItemInfo);
			var newIndex = fromItemInfo.ProductFamily.Items.IndexOf(toItemInfo);
			if (newIndex < oldIndex)
				transitionForward = false;
		}

		return transitionForward;
	}

	/// <summary>
	/// Opens an external sample window.
	/// </summary>
	/// <param name="fullName">The full name of the sample class to open.</param>
	private void OpenExternalSampleCore(string fullName) {
		if (!string.IsNullOrEmpty(fullName)) {
			// Create the view control
			try {
				if (!AreExternalSamplesSupported)
					throw new NotSupportedException("External samples are not supported on this platform.");

				if (CreateElement(fullName) is Window demoWindow)
					demoWindow.Show();
				else
					throw new NotSupportedException("Window could not be created.");
			}
			catch (Exception ex) {
				while (ex.InnerException is not null)
					ex = ex.InnerException;

				MessageService?.ShowError($"The sample '{fullName}' was unable to be loaded.", ex);
			}
		}
	}

	/// <summary>
	/// Resolves the default <see cref="TopLevel"/> to be used for the current application.
	/// </summary>
	private static TopLevel? ResolveDefaultTopLevel() {
		return Application.Current?.ApplicationLifetime switch {
			IClassicDesktopStyleApplicationLifetime desktopLifetime => desktopLifetime.MainWindow,
			ISingleViewApplicationLifetime singleView => TopLevel.GetTopLevel(singleView.MainView),
			_ => null
		};
	}

	/// <summary>
	/// Updates the navigation command can-execute states.
	/// </summary>
	private void UpdateNavigationCommands() {
		_navigateViewToNextItemInfoCommand?.RaiseCanExecuteChanged();
		_navigateViewToPreviousItemInfoCommand?.RaiseCanExecuteChanged();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The Actipro blog URL.
	/// </summary>
	public string ActiproBlogUrl
		=> "https://www.actiprosoftware.com/blog";

	/// <summary>
	/// The Actipro contact URL.
	/// </summary>
	public string ActiproContactUrl
		=> "https://www.actiprosoftware.com/company/contact";

	/// <summary>
	/// The Actipro GitHub URL for the parent folder to the product samples source code.
	/// </summary>
	public string ActiproGitHubProductSamplesParentFolder { get; private set; }
		= "https://github.com/Actipro/Avalonia-Controls/tree/develop/Samples/SampleBrowser/SampleBrowser.Common/";

	/// <summary>
	/// The Actipro third-party notices URL.
	/// </summary>
	public string ActiproThirdPartyNoticesUrl
		=> "https://github.com/Actipro/Avalonia-Controls/blob/develop/Samples/SampleBrowser/Notices.md";

	/// <summary>
	/// The Actipro X (aka Twitter) URL.
	/// </summary>
	public string ActiproTwitterUrl
		=> "https://twitter.com/Actipro";

	/// <summary>
	/// The Actipro web site home page URL.
	/// </summary>
	public string ActiproWebSiteUrl
		=> "https://www.actiprosoftware.com";

	/// <summary>
	/// Indicates if opening external samples is supported.
	/// </summary>
	public static bool AreExternalSamplesSupported
		=> Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime;

	/// <summary>
	/// Indicates if private items are available to be displayed in the application.
	/// </summary>
	/// <remarks>Internal use only.</remarks>
	public bool ArePrivateItemsAvailable
		=> _arePrivateItemsAvailable;

	/// <summary>
	/// The copyright message.
	/// </summary>
	public string Copyright
		=> ActiproSoftware.Properties.Shared.AssemblyInfo.Instance.CopyrightDisplayText;

	/// <summary>
	/// The singleton instance of the class.
	/// </summary>
	public static ApplicationViewModel Instance { get; } = new();

	/// <summary>
	/// Whether the drawer view is currently open.
	/// </summary>
	public bool IsDrawerOpen {
		get => _isDrawerOpen;
		set => SetProperty(ref _isDrawerOpen, value);
	}

	/// <summary>
	/// The service that can be used for displaying messages.
	/// </summary>
	public IMessageService? MessageService { get; set; }

	/// <summary>
	/// Navigates the view backward.
	/// </summary>
	public void NavigateViewBackward() {
		if (_navigationService.CanGoBack) {
			var itemInfo = _navigationService.GoBack();

			_navigationService.IsNavigatingThroughHistory = true;
			try {
				if (itemInfo is not null)
					NavigateViewToItemInfo(itemInfo, transitionForward: false);
				else
					NavigateViewToHome(transitionForward: false);
			}
			finally {
				_navigationService.IsNavigatingThroughHistory = false;
			}
		}
	}

	/// <summary>
	/// Navigates the view forward.
	/// </summary>
	public void NavigateViewForward() {
		if (_navigationService.CanGoForward) {
			_navigationService.IsNavigatingThroughHistory = true;
			try {
				var itemInfo = _navigationService.GoForward();
				if (itemInfo is not null)
					NavigateViewToItemInfo(itemInfo, transitionForward: true);
				else
					NavigateViewToHome(transitionForward: true);
			}
			finally {
				_navigationService.IsNavigatingThroughHistory = false;
			}
		}
	}

	/// <summary>
	/// Navigates the view to the home view.
	/// </summary>
	/// <param name="transitionForward">Whether the transition is in a forward direction.</param>
	public void NavigateViewToHome(bool transitionForward) {
		// Update the view
		ViewItemInfo = null;
		ViewImageSource = new UI.Avalonia.Images.ActiproLogo48();
		ViewSubTitle = "Actipro Software";
		ViewTitle = "Avalonia Controls";
		ViewHasCustomStatusBar = false;
		ViewHasNavigationButtons = false;
		StatusMessage = Copyright;
		ViewTransitionForward = transitionForward;
		ViewElement = new HomeControl();

		_navigationService.NavigateTo(itemInfo: null);

		UpdateNavigationCommands();
	}

	/// <summary>
	/// Navigates the view to the <see cref="ProductItemInfo"/> identified by the given <see cref="Uri"/>.
	/// </summary>
	/// <param name="itemInfoUri">The <see cref="Uri"/> of the <see cref="ProductItemInfo"/> navigation target.</param>
	/// <param name="transitionForward">Whether the transition is in a forward direction.</param>
	public void NavigateViewToItemInfo(Uri itemInfoUri, bool? transitionForward = null) {
		ArgumentNullException.ThrowIfNull(itemInfoUri);

		if (FindProductInfo(itemInfoUri) is { } itemInfo)
			NavigateViewToItemInfo(itemInfo, transitionForward);
	}

	/// <summary>
	/// Navigates the view to the specified <see cref="ProductItemInfo"/>.
	/// </summary>
	/// <param name="itemInfo">The <see cref="ProductItemInfo"/> navigation target.</param>
	/// <param name="transitionForward">Whether the transition is in a forward direction.</param>
	public void NavigateViewToItemInfo(ProductItemInfo itemInfo, bool? transitionForward = null) {
		// Create the view control
		Control? newViewElement = null;
		Exception? ex = null;
		try {
			newViewElement = CreateElement(itemInfo.Path);
		}
		catch (Exception ex2) {
			while (ex2.InnerException is not null)
				ex2 = ex2.InnerException;
			ex = ex2;
		}
		if (newViewElement is null) {
			var errorTextBlock = new TextBlock() {
				FontSize = 18,
				HorizontalAlignment = HorizontalAlignment.Center,
				Margin = new Thickness(50),
				MaxWidth = 800,
				Text = string.Format(CultureInfo.CurrentCulture, "The sample '{0}' was unable to be loaded.", itemInfo.Path),
				TextWrapping = TextWrapping.Wrap,
				VerticalAlignment = VerticalAlignment.Center
			};
			if (ex is not null)
				errorTextBlock.Text += Environment.NewLine + Environment.NewLine + "The error message was: " + ex.Message;
			newViewElement = errorTextBlock;
		}

		// Ensure a transition direction is set
		if (!transitionForward.HasValue)
			transitionForward = IsTransitionForward(_viewItemInfo, itemInfo);

		// Update the view
		StatusMessage = (itemInfo.HasFolderPathInStatusBar ? itemInfo.FolderPath : Copyright);
		ViewItemInfo = itemInfo;
		ViewImageSource = itemInfo.ProductFamily?.LogoImageSource;
		ViewSubTitle = itemInfo.ProductFamily?.Title + (!string.IsNullOrEmpty(itemInfo.Category) ? (!string.IsNullOrEmpty(itemInfo.ProductFamily?.Title) ? " / " : null) + itemInfo.Category : null);
		ViewTitle = itemInfo.Title;
		ViewHasCustomStatusBar = itemInfo.HasCustomStatusBar;
		ViewHasNavigationButtons = true;
		ViewTransitionForward = transitionForward.Value;
		ViewElement = newViewElement;

		_navigationService.NavigateTo(itemInfo);

		UpdateNavigationCommands();
	}

	/// <summary>
	/// The <see cref="ICommand"/> that navigates the view to the home page.
	/// </summary>
	public ICommand NavigateViewToHomeCommand
		=> _navigateViewToHomeCommand ??= new DelegateCommand<object?>(_ => NavigateViewToHome(transitionForward: true));

	/// <summary>
	/// The <see cref="ICommand"/> that navigates the view to a <see cref="ProductItemInfo"/>.
	/// </summary>
	public ICommand NavigateViewToItemInfoCommand {
		get => _navigateViewToItemInfoCommand ??= new DelegateCommand<object>(param => {
			switch (param) {
				case ProductItemInfo itemInfo:
					NavigateViewToItemInfo(itemInfo);
					break;
				case Uri uri:
					NavigateViewToItemInfo(uri);
					break;
				case string uriString when uriString.StartsWith("https://ActiproSoftware/", StringComparison.OrdinalIgnoreCase):
					NavigateViewToItemInfo(new Uri(uriString));
					break;
			}
		});
	}

	/// <summary>
	/// The <see cref="ICommand"/> that navigates the view to the next <see cref="ProductItemInfo"/>.
	/// </summary>
	public ICommand NavigateViewToNextItemInfoCommand {
		get => _navigateViewToNextItemInfoCommand ??= new DelegateCommand<object?>(
			_ => {
				if (_viewItemInfo?.NextItem is { } nextItemInfo)
					NavigateViewToItemInfo(nextItemInfo, transitionForward: true);
			},
			_ => _viewItemInfo?.NextItem is not null
		);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that navigates the view to the previous <see cref="ProductItemInfo"/>.
	/// </summary>
	public ICommand NavigateViewToPreviousItemInfoCommand {
		get => _navigateViewToPreviousItemInfoCommand ??= new DelegateCommand<object?>(
			_ => {
				if (_viewItemInfo?.PreviousItem is { } previousItemInfo)
					NavigateViewToItemInfo(previousItemInfo, transitionForward: false);
			},
			_ => _viewItemInfo?.PreviousItem is not null
		);
	}

	/// <summary>
	/// A special command associated with controls that are for demonstration purposes only and provide no implemented functionality.
	/// </summary>
	public ICommand NotImplementedCommand {
		get => _notImplementedCommand ??= new DelegateCommand<object?>(
			_ => {
				MessageBox.Show(
					"This control is for user interface demonstration purposes only and no application functionality has been implemented for it.",
					"Not Implemented",
					MessageBoxButtons.OK,
					MessageBoxImage.Information);
			}
		);
	}

	/// <summary>
	/// Opens an external uri (e.g., browser link).
	/// </summary>
	/// <param name="uri">The uri to be opened.</param>
	public static void OpenExternalUri(Uri uri) {
		if (ResolveDefaultTopLevel()?.Launcher is { } launcher) {
			Dispatcher.UIThread.Post(async () => {
				_ = await launcher.LaunchUriAsync(uri);
			});
		}
	}

	/// <summary>
	/// Opens an external sample window.
	/// </summary>
	/// <param name="className">The AXAML class name.</param>
	public void OpenExternalSample(string className) {
		if (ViewItemInfo is { } viewItemInfo) {
			var fullName = viewItemInfo.FolderPath + "/" + (className ?? "MainWindow");
			OpenExternalSampleCore(fullName);
		}
	}

	/// <summary>
	/// The <see cref="ICommand"/> that opens the GitHub folder containing the current sample.
	/// </summary>
	public ICommand OpenGitHubSampleFolderCommand {
		get => _openGitHubSampleFolderCommand ??= new DelegateCommand<object>(
			(param) => {
				var path = ActiproGitHubProductSamplesParentFolder;
				var sampleRelativePath = (param as string) ?? ViewItemInfo?.Path;

				if ((path is not null) && (sampleRelativePath is not null)) {
					var folderPath = sampleRelativePath;
					if (folderPath.StartsWith(@"/"))
						folderPath = folderPath[1..];

					path = Path.Combine(path, folderPath);
				}

				if (path is null) {
					MessageBox.Show(
						$"Unable to open sample folder because the folder path could not be determined.",
						"Folder Unknown",
						image: MessageBoxImage.Error);
				}
				else {
					try {
						OpenExternalUri(new Uri(path));
					}
					catch (Exception ex) {
						MessageBox.Show(
							$"The folder '{path}' was unable to be opened.  The error message was: {ex.Message}",
							"Folder Not Opened",
							image: MessageBoxImage.Error);
					}
				}

			},
			_ => (ViewItemInfo is { IsProductOverview: false }) && (!string.IsNullOrEmpty(ActiproGitHubProductSamplesParentFolder))
		);
	}

	/// <summary>
	/// The <see cref="ICommand"/> that opens a web URL.
	/// </summary>
	public ICommand OpenUrlCommand {
		get => _openUrlCommand ??= new DelegateCommand<object>((param) => {
			if (param is string { Length: > 0 } uriString) {
				// For web URLs, navigate externally
				if (
					uriString.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
					|| uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
				) {
					try {
						OpenExternalUri(new Uri(uriString));
					}
					catch {
						MessageService?.ShowError($"Navigation to the URL '{uriString}' was unable to be completed.  This {Environment.OSVersion.Platform} platform may not support opening an external URI.");
					}
					return;
				}
			}
		});
	}

	/// <summary>
	/// The documentation URL.
	/// </summary>
	public string PlatformDocumentationUrl
		=> "https://www.actiprosoftware.com/docs/controls/avalonia/index";

	/// <summary>
	/// The product URL.
	/// </summary>
	public string PlatformProductsUrl
		=> "https://www.actiprosoftware.com/products/controls/avalonia";

	/// <summary>
	/// The purchase licenses URL.
	/// </summary>
	public string PlatformPurchaseLicensesUrl
		=> "https://www.actiprosoftware.com/purchase/pricing/controls/avalonia";

	/// <summary>
	/// The <see cref="ProductData"/> model.
	/// </summary>
	public ProductData? ProductData {
		get => _productData;
		private set {
			if (SetProperty(ref _productData, value)) {
				// Navigate to a default sample if specified
				if (!string.IsNullOrEmpty(DefaultSampleUri))
					NavigateViewToItemInfo(new Uri(DefaultSampleUri));
			}
		}
	}

	/// <summary>
	/// Indicates the scale factor of the primary application window.
	/// </summary>
	/// <remarks>
	/// The default value is <c>1.0</c>.
	/// </remarks>
	public double ScaleFactor {
		get => _scaleFactor;
		set => SetProperty(ref _scaleFactor, value);
	}

	/// <summary>
	/// The <see cref="ICommand"/> to set the current <see cref="ScaleFactor"/>.
	/// </summary>
	public ICommand SetScaleFactorCommand
		=> _setScaleFactorCommand ??= new DelegateCommand<object>(param => ScaleFactor = Convert.ToDouble(param));

	/// <summary>
	/// The <see cref="ICommand"/> to set the user interface density.
	/// </summary>
	public ICommand SetUserInterfaceDensityCommand {
		get => _setUserInterfaceDensityCommand ??= new DelegateCommand<UserInterfaceDensity?>(density => {
			if (
				density.HasValue
				&& ModernTheme.TryGetCurrent(out var theme)
				&& theme.Definition is { } definition
			) {
				// Optionally update the base font size based on the density
				definition.BaseFontSize = density switch {
					UserInterfaceDensity.Compact => 13.0,
					UserInterfaceDensity.Normal or UserInterfaceDensity.Spacious or _ => 14.0,
				};

				// Arrange spinner buttons horizontally in Spacious density
				definition.SpinnerHasHorizontalOrientation = (density == UserInterfaceDensity.Spacious);

				// Set the new UI density
				definition.UserInterfaceDensity = density.Value;

				// Must manually refresh resources after changing definition properties
				theme.RefreshResources();
			}
		});
	}

	/// <summary>
	/// Indicates if private items should be visible.
	/// </summary>
	/// <remarks>Internal use only.</remarks>
	public bool ShowPrivateItems {
		get => _showPrivateItems;
		set => SetProperty(ref _showPrivateItems, value);
	}

	/// <summary>
	/// The status message.
	/// </summary>
	public string? StatusMessage {
		get => _statusMessage;
		set => SetProperty(ref _statusMessage, value ?? "Ready");
	}

	/// <summary>
	/// The <see cref="ICommand"/> to toggle the flow direction of the application.
	/// </summary>
	public ICommand ToggleAllowFullScreenCommand {
		get => _toggleAllowFullScreenCommand ??= new DelegateCommand<object?>(_ => {
			if (
				ResolveDefaultTopLevel() is Window window
				&& window.FindDescendantOfType<WindowTitleBar>() is { } windowTitleBar
			) {
				windowTitleBar.IsFullScreenButtonAllowed = !windowTitleBar.IsFullScreenButtonAllowed;
			}
		});
	}

	/// <summary>
	/// The <see cref="ICommand"/> to toggle the drawer visibility.
	/// </summary>
	public ICommand ToggleDrawerOpenCommand
		=> _toggleDrawerOpenCommand ??= new DelegateCommand<object?>(_ => IsDrawerOpen = !IsDrawerOpen);

	/// <summary>
	/// The <see cref="ICommand"/> to toggle the flow direction of the application.
	/// </summary>
	public ICommand ToggleFlowDirectionCommand {
		get => _toggleFlowDirectionCommand ??= new DelegateCommand<object?>(_ => {
			if (ResolveDefaultTopLevel() is { } topLevel)
				topLevel.FlowDirection = (topLevel.FlowDirection == FlowDirection.LeftToRight ? FlowDirection.RightToLeft : FlowDirection.LeftToRight);
		});
	}

	/// <summary>
	/// The <see cref="ICommand"/> to toggle the drawer visibility.
	/// </summary>
	/// <remarks>Internal use only.</remarks>
	public ICommand ToggleShowPrivateItemsCommand
		=> _toggleShowPrivateItemsCommand ??= new DelegateCommand<object?>(_ => ShowPrivateItems = !ShowPrivateItems);

	/// <summary>
	/// The <see cref="Visual"/> that renders the view's UI.
	/// </summary>
	public Visual? ViewElement {
		get => _viewElement;
		set => SetProperty(ref _viewElement, value);
	}

	/// <summary>
	/// The <see cref="ICommand"/> to show a prompt with environment information.
	/// </summary>
	public ICommand ViewEnvironmentCommand {

		get => _viewEnvironmentCommand ??= new DelegateCommand<object?>(async _ => {
			var content = new StringBuilder();

			try {
				var os = Environment.OSVersion;
				content.Append($"OS: {os.VersionString}");
				if (OperatingSystem.IsAndroid())
					content.Append(" (Android)");
				else if (OperatingSystem.IsBrowser())
					content.Append(" (Browser)");
				else if (OperatingSystem.IsIOS())
					content.Append(" (iOS)");
				else if (OperatingSystem.IsMacCatalyst())
					content.Append(" (Mac Catalyst)");
				else if (OperatingSystem.IsTvOS())
					content.Append(" (tvOS)");
				else if (OperatingSystem.IsWatchOS())
					content.Append(" (watchOS)");
			}
			catch (InvalidOperationException) { }
			finally {
				content.AppendLine();
			}

			content.AppendLine($".NET: {Environment.Version}");
			content.AppendLine($"Avalonia: {typeof(AvaloniaObject).Assembly.GetInformationalVersion()}");
			content.Append($"Actipro: {Properties.Shared.AssemblyInfo.Instance.InformationalVersionText}");

			if (ResolveDefaultTopLevel() is Window window) {
				content.AppendLine();
				content.AppendLine();
				content.AppendLine($"Main Window {(window.WindowState != WindowState.Normal ? $"({window.WindowState})" : null)}");
				content.AppendLine($"- Screen Bounds: {window.Position}, {window.FrameSize}");
				content.AppendLine($"- Client Area: {window.Width}, {window.Height}");
				content.Append($"- Render Scaling: {window.RenderScaling * 100}%");

				try {
					var currentScreen = window.Screens.ScreenFromWindow(window);
					var screens = window.Screens.All;
					for (var index = 0; index < screens.Count; index++) {
						var screen = screens[index];
						var tags = new HashSet<string>();
						if (screen.IsPrimary)
							tags.Add("Primary");
						if (screen == currentScreen)
							tags.Add("Current");

						content.AppendLine();
						content.AppendLine();
						content.AppendLine($"Screen {(index + 1)} {(tags.Count > 0 ? $"({string.Join(", ", tags)})" : null)}");
						content.AppendLine($"- Orientation: {screen.CurrentOrientation}");
						content.AppendLine($"- Bounds: {screen.Bounds}");
						content.AppendLine($"- Working Area: {screen.WorkingArea}");
						content.Append($"- Scaling: {screen.Scaling * 100}%");
					}
				}
				catch (InvalidOperationException) { }
			}

			var textBlock = new TextBlock() {
				Text = content.ToString(),
				TextWrapping = TextWrapping.Wrap
			};
			textBlock.Classes.Add("theme-text-code");
			textBlock.Classes.Add("size-xs");

			await UserPromptBuilder.Configure()
				.WithTitle("Actipro Sample Browser")
				.WithHeaderContent("Environment Information")
				.WithStatusIcon(MessageBoxImage.Information)
				.WithButton(b => b
					.WithContent("C_opy to Clipboard")
					.WithCommand(new DelegateCommand<object?>(_ => {
						var clipboard = TopLevel.GetTopLevel(textBlock)?.Clipboard;
						clipboard?.SetTextAsync(content.ToString());
					}))
				)
				.WithButton(MessageBoxResult.Close)
				.WithContent(textBlock)
				.Show();
		});
	}

	/// <summary>
	/// Whether the view has a custom statusbar.
	/// </summary>
	public bool ViewHasCustomStatusBar {
		get => _viewHasCustomStatusBar;
		set => SetProperty(ref _viewHasCustomStatusBar, value);
	}

	/// <summary>
	/// Whether the view has navigation buttons.
	/// </summary>
	public bool ViewHasNavigationButtons {
		get => _viewHasNavigationButtons;
		set => SetProperty(ref _viewHasNavigationButtons, value);
	}

	/// <summary>
	/// The view's image source.
	/// </summary>
	public IImage? ViewImageSource {
		get => _viewImageSource;
		set => SetProperty(ref _viewImageSource, value);
	}

	/// <summary>
	/// Whether the current view is for the home view.
	/// </summary>
	public bool ViewIsHome
		=> ViewItemInfo is null;

	/// <summary>
	/// The <see cref="ProductItemInfo"/> currently in the view, if any.
	/// </summary>
	public ProductItemInfo? ViewItemInfo {
		get => _viewItemInfo;
		set {
			if (SetProperty(ref _viewItemInfo, value)) {
				OnPropertyChanged(nameof(ViewIsHome));
				OnPropertyChanged(nameof(ViewUsesHomeDrawer));
			}
		}
	}

	/// <summary>
	/// The view's sub-title.
	/// </summary>
	public string? ViewSubTitle {
		get => _viewSubTitle;
		set => SetProperty(ref _viewSubTitle, value);
	}

	/// <summary>
	/// The view's title.
	/// </summary>
	public string? ViewTitle {
		get => _viewTitle;
		set => SetProperty(ref _viewTitle, value);
	}

	/// <summary>
	/// Whether the view's transition is in a forward direction.
	/// </summary>
	public bool ViewTransitionForward {
		get => _viewTransitionForward;
		set => SetProperty(ref _viewTransitionForward, value);
	}

	/// <summary>
	/// Whether the current view uses the home drawer.
	/// </summary>
	public bool ViewUsesHomeDrawer
		=> ViewIsHome || string.IsNullOrEmpty(ViewItemInfo?.ProductFamily?.Title);

}
