using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demos.DocumentEditorMvvm {

	public partial class MainControl : UserControl {

		private DelegateCommand<object?>? _toggleApplicationButtonCommand;
		private DelegateCommand<object?>? _toggleApplicationButtonImageCommand;
		private DelegateCommand<object?>? _toggleFlowDirectionCommand;
		private DelegateCommand<object?>? _toggleFooterCommand;
		private DelegateCommand<object?>? _toggleQuickAccessToolBarCommand;

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="IsExternalSampleOptionVisible"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> IsExternalSampleOptionVisibleProperty
			= AvaloniaProperty.Register<MainControl, bool>(nameof(IsExternalSampleOptionVisible), defaultValue: true);

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// Hide the external sample option when not supported
			if (!ApplicationViewModel.AreExternalSamplesSupported)
				IsExternalSampleOptionVisible = false;

			// Use an adapter to register BarManager commands that interact with a TextBox instance
			_ = new TextBoxBarManagerCommandAdapter(BarManager, textBox);

			// Create the view model for the ribbon used by this demo
			RibbonViewModel = new DocumentEditorRibbonViewModel(BarManager);

			// Configure Backstage to use the data templates defined for each backstage tab
			if ((RibbonViewModel.Backstage is not null) && (this.TryFindResource("BackstageContentTemplates", out var contentTemplate)))
				RibbonViewModel.Backstage.ContentTemplate = contentTemplate as IDataTemplate;

			// Configure the document
			CreateNewDefaultDocument();

			// Mapped composite commands
			BarManager.FlowDirectionCommand.RegisterCommand(ToggleFlowDirectionCommand);
			BarManager.NewBlankDocumentCommand.RegisterCommand(new DelegateCommand<object?>(_ => CreateNewBlankDocument()));
			BarManager.NewDefaultDocumentCommand.RegisterCommand(new DelegateCommand<object?>(_ => CreateNewDefaultDocument()));
			BarManager.ToggleApplicationButtonCommand.RegisterCommand(ToggleApplicationButtonCommand);
			BarManager.ToggleApplicationButtonImageCommand.RegisterCommand(ToggleApplicationButtonImageCommand);
			BarManager.ToggleFooterCommand.RegisterCommand(ToggleFooterCommand);
			BarManager.ToggleQuickAccessToolBarCommand.RegisterCommand(ToggleQuickAccessToolBarCommand);

			// Initialize toggle control states
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowApplicationButton, () => this.RibbonViewModel?.IsApplicationButtonVisible ?? false);
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowFooter, () => this.RibbonViewModel?.Footer != null);
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowQuickAccessToolBar, () => this.RibbonViewModel?.QuickAccessToolBarMode == RibbonQuickAccessToolBarMode.Visible);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private BarManager BarManager { get; } = new BarManager();

		private void CloseBackstage() {
			var backstage = RibbonViewModel?.Backstage;
			if (backstage is not null)
				backstage.IsOpen = false;
		}

		private void CreateNewBlankDocument()
			=> OpenDocument(text: null);

		private void CreateNewDefaultDocument()
			=> OpenDocument(@"Actipro Bars for Avalonia

Actipro Bars contains a comprehensive implementation of an Office-like ribbon user interface for Avalonia applications, including its modern fluent design and ability to create content-rich galleries.

Some things to try in this demo...
  -  Resize the window width to see control layouts scale through their variants.
  -  Use the ribbon's options button on its lower right to toggle between Classic and Simplified layout modes.
  -  Hover over font family/size combobox items to see a live preview.
  -  Hover over text highlight color or font color gallery items to see a live preview.
  -  Show the underline split button menu to see a gallery with vertical resizing and custom rendered items.
  -  Show the bullets split button menu to see a gallery with categorization and dual resizing.
  -  Press and release the Alt key to enter key tip mode.
  -  Use context menus to add controls to and remove controls from to the Quick Access Toolbar (QAT).
  -  Look at all the Home and Insert tabs to see the variety of built-in controls that can be used, including those on menus.
  -  Hover over various controls to see their screen tips.
  -  Check out the recent documents list control on the Backstage's Open tab, accessible via the File application button.
");
		
		private void OpenDocument(string? text) {
			CloseBackstage();
			textBox.IsUndoEnabled = false;
			textBox.ClearSelection();
			if (string.IsNullOrEmpty(text))
				textBox.Clear();
			else
				textBox.Text = text;
			textBox.IsUndoEnabled = true;
			textBox.Focus();
		}

		private ICommand ToggleFlowDirectionCommand {
			get {
				return _toggleFlowDirectionCommand ??= new DelegateCommand<object?>(param => {
					var targetVisual = (this.VisualRoot as Visual) ?? this;
					targetVisual.FlowDirection = (targetVisual.FlowDirection == FlowDirection.LeftToRight)
						? FlowDirection.RightToLeft
						: FlowDirection.LeftToRight;
				});
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public bool IsExternalSampleOptionVisible {
			get => GetValue(IsExternalSampleOptionVisibleProperty);
			set => SetValue(IsExternalSampleOptionVisibleProperty, value);
		}

		protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
			base.OnAttachedToVisualTree(e);

			BarManager.WatchUserInterfaceDensityChangedEvent();
		}

		protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e) {
			base.OnDetachedFromVisualTree(e);

			BarManager.UnwatchUserInterfaceDensityChangedEvent();
		}

		public ICommand OpenExternalWindowCommand { get; }
			= new DelegateCommand<object>(_ => {
				new MainWindow().Show();
			});

		public RibbonViewModel? RibbonViewModel {
			get => ribbon.DataContext as RibbonViewModel;
			set => ribbon.DataContext = value;
		}

		/// <summary>
		/// Gets the command which toggles the visibility of the ribbon application button.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleApplicationButtonCommand {
			get {
				if (_toggleApplicationButtonCommand is null) {
					_toggleApplicationButtonCommand = new DelegateCommand<object?>(
						executeAction: p => {
							if (RibbonViewModel is not null) {
								this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.ShowApplicationButton,
									isChecked => RibbonViewModel.IsApplicationButtonVisible = isChecked);
							}
						}
					);
				}
				return _toggleApplicationButtonCommand;
			}
		}

		/// <summary>
		/// Gets the command which toggles the visibility of the ribbon application button.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleApplicationButtonImageCommand {
			get {
				if (_toggleApplicationButtonImageCommand is null) {
					_toggleApplicationButtonImageCommand = new DelegateCommand<object?>(
						executeAction: p => {
							if ((RibbonViewModel is not null) && (ribbon is not null)) {
								this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.ShowApplicationButtonImage,
									isChecked => {
										if (isChecked) {
											// Override the view model configuration with an image
											ribbon.ApplicationButtonContent = new RibbonApplicationButton() {
												Content = new DynamicImage() {
													Width = 16,
													Height = 16,
													Source = ImageLoader.GetIcon("ApplicationButtonDefault16.png")
												}
											};
										}
										else {
											// Clear the explicit value to allow the view model configuration to be applied
											ribbon.ClearValue(Ribbon.ApplicationButtonContentProperty);
										}
									});
							}
						}
					);
				}
				return _toggleApplicationButtonImageCommand;
			}
		}

		/// <summary>
		/// Gets the command which toggles the visibility of the ribbon footer.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleFooterCommand {
			get {
				if (_toggleFooterCommand is null) {
					_toggleFooterCommand = new DelegateCommand<object?>(
						executeAction: p => {
							if (RibbonViewModel is not null) {
								this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.ShowFooter,
									isChecked => this.RibbonViewModel.Footer = (isChecked
										? new RibbonFooterViewModel() {
											Kind = RibbonFooterKind.Warning,
											Content = new RibbonFooterSimpleContentViewModel() {
												Icon = ImageLoader.GetIcon("InformationClear16.png"),
												Text = "Actipro Bars contains everything you need to implement modern ribbon, toolbar, and menu interfaces in your apps.",
											}
										} : null));
							}
						}
					);
				}
				return _toggleFooterCommand;
			}
		}

		/// <summary>
		/// Gets the command which toggles the visibility of the ribbon quick access toolbar.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand ToggleQuickAccessToolBarCommand {
			get {
				if (_toggleQuickAccessToolBarCommand is null) {
					_toggleQuickAccessToolBarCommand = new DelegateCommand<object?>(
						executeAction: p => {
							if (RibbonViewModel is not null) {
								this.BarManager.SetValueFromControlViewModelCheckedState(BarControlKeys.ShowQuickAccessToolBar,
									isChecked => this.RibbonViewModel.QuickAccessToolBarMode = (isChecked ? RibbonQuickAccessToolBarMode.Visible : RibbonQuickAccessToolBarMode.Hidden));
							}
						},
						canExecuteFunc: p => (this.RibbonViewModel?.QuickAccessToolBarMode ?? RibbonQuickAccessToolBarMode.None) != RibbonQuickAccessToolBarMode.None
					);
				}
				return _toggleQuickAccessToolBarCommand;
			}
		}

	}

}
