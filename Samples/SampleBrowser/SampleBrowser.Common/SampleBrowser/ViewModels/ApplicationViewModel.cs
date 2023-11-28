using ActiproSoftware.UI.Avalonia.Input;
using ActiproSoftware.UI.Avalonia.Themes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows.Input;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the application view-model.
	/// </summary>
	public partial class ApplicationViewModel : ObservableObjectBase {

		private bool _arePrivateItemsAvailable = false;
		private bool _isDrawerOpen = true;
		private readonly NavigationService _navigationService = new();
		private ProductData? _productData;
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
		private DelegateCommand<object>? _openUrlCommand;
		private DelegateCommand<UserInterfaceDensity?>? _setUserInterfaceDensityCommand;
		private DelegateCommand<object?>? _toggleDrawerOpenCommand;
		private DelegateCommand<object?>? _toggleShowPrivateItemsCommand;

		public const string ProductDataResourceKey = "AppProductData";

		private const string DefaultSampleUri = null;
		//private const string DefaultSampleUri = "https://ActiproSoftware/SampleBrowser/Utilities/ColorPalette/ColorPaletteView";
		//private const string DefaultSampleUri = "https://ActiproSoftware/ProductSamples/ThemesSamples/NativeControls/ButtonSamples";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		private ApplicationViewModel() {
			// Internal initialization
			var methodInfo = this.GetType().GetMethod("InitializePrivate", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			methodInfo?.Invoke(this, Array.Empty<object>());

			this.NavigateViewToHome(transitionForward: true);

			// Load product data from resources
			var productDataKey = ProductDataResourceKey;
			if (Application.Current?.Resources.TryGetResource(productDataKey, theme: null, out var productDataResource) == true)
				this.ProductData = productDataResource as ProductData;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the <see cref="Control"/> for the specified element's XAML path.
		/// </summary>
		/// <param name="path">The element's XAML path.</param>
		/// <returns>The <see cref="Control"/> for the specified element's XAML path.</returns>
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

				// Attempt to create an instance of a type
				Control? control;
				var type = Type.GetType(typeName);
				if (type is not null) {
					control = Activator.CreateInstance(type) as Control;
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
		/// <returns>The <see cref="ProductItemInfo"/> for the specified <see cref="Uri"/>.</returns>
		private ProductItemInfo? FindProductInfo(Uri uri) {
			if ((uri is not null) && (_productData is not null)) {
				var targetPath = uri.LocalPath;

				foreach (var itemInfo in _productData.AllItems) {
					if (string.Compare(itemInfo.Path, targetPath, StringComparison.OrdinalIgnoreCase) == 0)
						return itemInfo;
				}
			}

			return null;
		}

		/// <summary>
		/// Returns whether to use a forward transition from one <see cref="ProductItemInfo"/> to another.
		/// </summary>
		/// <param name="fromItemInfo">The from <see cref="ProductItemInfo"/>.</param>
		/// <param name="toItemInfo">The to <see cref="ProductItemInfo"/>.</param>
		/// <returns>
		/// <c>true</c> if a forward transition should be used; otherwise, <c>false</c>.
		/// </returns>
		private static bool IsTransitionForward(ProductItemInfo? fromItemInfo, ProductItemInfo? toItemInfo) {
			var transitionForward = true;

			if ((fromItemInfo is not null) && (fromItemInfo.ProductFamily is not null) && (toItemInfo is not null)) {
				var oldIndex = fromItemInfo.ProductFamily.Items.IndexOf(fromItemInfo);
				var newIndex = fromItemInfo.ProductFamily.Items.IndexOf(toItemInfo);
				if (newIndex < oldIndex)
					transitionForward = false;
			}

			return transitionForward;
		}

		/// <summary>
		/// Updates the navigation command can-execute states.
		/// </summary>
		private void UpdateNavigationCommands() {
			_navigateViewToNextItemInfoCommand?.RaiseCanExecuteChanged();
			_navigateViewToPreviousItemInfoCommand?.RaiseCanExecuteChanged();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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
		/// The Actipro third-party notices URL.
		/// </summary>
		public string ActiproThirdPartyNoticesUrl
			=> "https://github.com/Actipro/Avalonia-Controls/blob/develop/Samples/SampleBrowser/Notices.md";

		/// <summary>
		/// The Actipro Twitter URL.
		/// </summary>
		public string ActiproTwitterUrl
			=> "https://twitter.com/Actipro";

		/// <summary>
		/// The Actipro web site home page URL.
		/// </summary>
		public string ActiproWebSiteUrl
			=> "https://www.actiprosoftware.com";

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
			=> string.Format(CultureInfo.CurrentCulture, "Copyright \u00A9 2022-{0} Actipro Software LLC", DateTime.Today.Year);

		/// <summary>
		/// The singleton instance of the class.
		/// </summary>
		public static ApplicationViewModel Instance { get; } = new ApplicationViewModel();

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
						this.NavigateViewToItemInfo(itemInfo, transitionForward: false);
					else
						this.NavigateViewToHome(transitionForward: false);
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
						this.NavigateViewToItemInfo(itemInfo, transitionForward: true);
					else
						this.NavigateViewToHome(transitionForward: true);
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
			this.ViewItemInfo = null;
			this.ViewImageSource = new UI.Avalonia.Images.ActiproLogo48();
			this.ViewSubTitle = "Actipro Software";
			this.ViewTitle = "Avalonia Controls";
			this.ViewHasCustomStatusBar = false;
			this.ViewHasNavigationButtons = false;
			this.StatusMessage = this.Copyright;
			this.ViewTransitionForward = transitionForward;
			this.ViewElement = new HomeControl();

			_navigationService.NavigateTo(null);

			this.UpdateNavigationCommands();
		}

		/// <summary>
		/// Navigates the view to the <see cref="ProductItemInfo"/> identified by the given <see cref="Uri"/>.
		/// </summary>
		/// <param name="itemInfoUri">The <see cref="Uri"/> of the <see cref="ProductItemInfo"/> navigation target.</param>
		/// <param name="transitionForward">Whether the transition is in a forward direction.</param>
		public void NavigateViewToItemInfo(Uri itemInfoUri, bool? transitionForward = null) {
			if (itemInfoUri is null)
				throw new ArgumentNullException(nameof(itemInfoUri));

			var itemInfo = this.FindProductInfo(itemInfoUri);
			if (itemInfo is not null)
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
			this.StatusMessage = (itemInfo.HasFolderPathInStatusBar ? itemInfo.FolderPath : this.Copyright);
			this.ViewItemInfo = itemInfo;
			this.ViewImageSource = itemInfo.ProductFamily?.LogoImageSource;
			this.ViewSubTitle = itemInfo.ProductFamily?.Title + (!string.IsNullOrEmpty(itemInfo.Category) ? (!string.IsNullOrEmpty(itemInfo.ProductFamily?.Title) ? " / " : null) + itemInfo.Category : null);
			this.ViewTitle = itemInfo.Title;
			this.ViewHasCustomStatusBar = itemInfo.HasCustomStatusBar;
			this.ViewHasNavigationButtons = true;
			this.ViewTransitionForward = transitionForward.Value;
			this.ViewElement = newViewElement;

			_navigationService.NavigateTo(itemInfo);

			this.UpdateNavigationCommands();
		}

		/// <summary>
		/// The <see cref="ICommand"/> that navigates the view to the home page.
		/// </summary>
		public ICommand NavigateViewToHomeCommand
			=> _navigateViewToHomeCommand ??= new DelegateCommand<object?>(_ => this.NavigateViewToHome(transitionForward: true));

		/// <summary>
		/// The <see cref="ICommand"/> that navigates the view to a <see cref="ProductItemInfo"/>.
		/// </summary>
		public ICommand NavigateViewToItemInfoCommand {
			get {
				return _navigateViewToItemInfoCommand ??= new DelegateCommand<object>((param) => {
					if (param is ProductItemInfo itemInfo)
						this.NavigateViewToItemInfo(itemInfo);
					else if (param is Uri uri)
						this.NavigateViewToItemInfo(uri);
					else if (param is string uriString
						&& !string.IsNullOrEmpty(uriString)
						&& uriString.StartsWith("https://ActiproSoftware/", StringComparison.OrdinalIgnoreCase)) {

						this.NavigateViewToItemInfo(new Uri(uriString));
					}
				});
			}
		}

		/// <summary>
		/// The <see cref="ICommand"/> that navigates the view to the next <see cref="ProductItemInfo"/>.
		/// </summary>
		public ICommand NavigateViewToNextItemInfoCommand {
			get {
				return _navigateViewToNextItemInfoCommand ??= new DelegateCommand<object?>(_ => {
					if (_viewItemInfo is not null) {
						var nextItemInfo = _viewItemInfo.NextItem;
						if (nextItemInfo is not null)
							this.NavigateViewToItemInfo(nextItemInfo, transitionForward: true);
					}
				}, _ => {
					return (_viewItemInfo?.NextItem is not null);
				});
			}
		}

		/// <summary>
		/// The <see cref="ICommand"/> that navigates the view to the previous <see cref="ProductItemInfo"/>.
		/// </summary>
		public ICommand NavigateViewToPreviousItemInfoCommand {
			get {
				return _navigateViewToPreviousItemInfoCommand ??= new DelegateCommand<object?>(_ => {
					if (_viewItemInfo is not null) {
						var previousItemInfo = _viewItemInfo.PreviousItem;
						if (previousItemInfo is not null)
							this.NavigateViewToItemInfo(previousItemInfo, transitionForward: false);
					}
				}, _ => {
					return (_viewItemInfo?.PreviousItem is not null);
				});
			}
		}

		/// <summary>
		/// The <see cref="ICommand"/> that opens a web URL.
		/// </summary>
		public ICommand OpenUrlCommand {
			get {
				return _openUrlCommand ??= new DelegateCommand<object>((param) => {
					if ((param is string uriString) && !string.IsNullOrEmpty(uriString)) {
						// For web URLs, navigate externally
						if ((uriString.StartsWith("https://")) || (uriString.StartsWith("http://"))) {
							try {
								if (PlatformHelper is null)
									MessageService?.ShowError($"Platform support undefined.");
								else
									PlatformHelper.OpenExternalUri(new Uri(uriString));
							}
							catch {
								MessageService?.ShowError($"Navigation to the URL '{uriString}' was unable to be completed.  This {Environment.OSVersion.Platform} platform may not support easily launching browser windows from C# code.");
							}
							return;
						}
					}
				});
			}
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
		/// The platform-specific helper class.
		/// </summary>
		public IPlatformHelper? PlatformHelper { get; set; }

		/// <summary>
		/// The <see cref="ProductData"/> model.
		/// </summary>
		public ProductData? ProductData {
			get => _productData;
			private set {
				if (SetProperty(ref _productData, value)) {
					// Navigate to a default sample if specified
					if (!string.IsNullOrEmpty(DefaultSampleUri))
						this.NavigateViewToItemInfo(new Uri(DefaultSampleUri));
				}
			}
		}

		/// <summary>
		/// The <see cref="ICommand"/> to set the user interface density.
		/// </summary>
		public ICommand SetUserInterfaceDensityCommand
			=> _setUserInterfaceDensityCommand ??= new DelegateCommand<UserInterfaceDensity?>(density => {
				if (density.HasValue && ModernTheme.TryGetCurrent(out var theme)) {
					var definition = theme.Definition;
					if (definition is not null) {
						definition.BaseFontSize = density switch {
							UserInterfaceDensity.Compact => 13.0,
							_ => 14.0,  // Normal, Spacious
						};
						
						definition.UserInterfaceDensity = density.Value;

						theme.RefreshResources();
					}
				}
			});

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
		/// The <see cref="ICommand"/> to toggle the drawer visibility.
		/// </summary>
		public ICommand ToggleDrawerOpenCommand
			=> _toggleDrawerOpenCommand ??= new DelegateCommand<object?>(_ => {
				this.IsDrawerOpen = !this.IsDrawerOpen;
			});

		/// <summary>
		/// The <see cref="ICommand"/> to toggle the drawer visibility.
		/// </summary>
		/// <remarks>Internal use only.</remarks>
		public ICommand ToggleShowPrivateItemsCommand
			=> _toggleShowPrivateItemsCommand ??= new DelegateCommand<object?>(_ => {
				this.ShowPrivateItems = !this.ShowPrivateItems;
			});

		/// <summary>
		/// The <see cref="Visual"/> that renders the view's UI.
		/// </summary>
		public Visual? ViewElement {
			get => _viewElement;
			set => SetProperty(ref _viewElement, value);
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
			=> ViewItemInfo == null;

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

}
