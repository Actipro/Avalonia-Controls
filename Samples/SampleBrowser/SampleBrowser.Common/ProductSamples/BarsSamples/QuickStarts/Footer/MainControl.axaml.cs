using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia;
using Avalonia.Controls;
using System.ComponentModel;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.Footer {

	public partial class MainControl : UserControl {

		private static ICommand DisabledCommand = new DelegateCommand<object>(_ => { /* no op */ }, _ => false);

		private BasicUsageOptionsViewModel? _basicUsageOptionsViewModel;
		private RibbonFooterViewModel? _basicUsageFooterViewModel;
		private RibbonFooterViewModel? _infoBarUsageFooterViewModel;
		private InfoBarUsageOptionsViewModel? _infoBarUsageOptionsViewModel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// Configure the Ribbon view model for the "Basic usage" MVVM-based sample
			BasicUsageRibbonViewModel = CreateBasicUsageRibbonViewModel();
			basicUsageRibbonMvvm.DataContext = BasicUsageRibbonViewModel;

			// Configure the Ribbon view model for the "Info bar usage" MVVM-based sample
			InfoBarUsageRibbonViewModel = CreateInfoBarUsageRibbonViewModel();
			infoBarUsageRibbonMvvm.DataContext = InfoBarUsageRibbonViewModel;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the view model for the footer used by the "Basic usage" MVVM-based sample.
		/// </summary>
		private RibbonFooterViewModel BasicUsageFooterViewModel {
			get {
				if (_basicUsageFooterViewModel is null) {
					_basicUsageFooterViewModel = new RibbonFooterViewModel() {

						// The RibbonFooterSimpleContentViewModel can be used to define a common footer
						// with an image to the left of a text message
						Content = new RibbonFooterSimpleContentViewModel() {
							Text = "The footer can be set to any content and is great for tips or notifications.",
							Icon = ImageLoader.GetIcon("InformationClear16.png"),
						},

						Kind = BasicUsageOptions.FooterKind,
					};
				}
				return _basicUsageFooterViewModel;
			}
		}

		/// <summary>
		/// Creates the ribbon view model for the "Basic usage" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateBasicUsageRibbonViewModel() {
			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				Footer = BasicUsageOptions?.IsFooterVisible == true ? BasicUsageFooterViewModel : null,
				IsApplicationButtonVisible = false,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Hidden,
				QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,
				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					IsCustomizeButtonVisible = false,
					Items = {
						new BarButtonViewModel("Save", "Save Document", DisabledCommand) {
							SmallIcon = ImageLoader.GetIcon("Save16.png"),
						},
					},
				},
				Tabs = {
					new RibbonTabViewModel("Home") {
						Groups = {

							new RibbonGroupViewModel("Documents") {
								Items = {
									new BarButtonViewModel("Open", "Open Document", DisabledCommand) {
										LargeIcon = ImageLoader.GetIcon("Open32.png"),
										SmallIcon = ImageLoader.GetIcon("Open16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									},
									new BarButtonViewModel("Save", "Save Document", DisabledCommand) {
										LargeIcon = ImageLoader.GetIcon("Save32.png"),
										SmallIcon = ImageLoader.GetIcon("Save16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									},
								},
							},

						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Info bar usage" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateInfoBarUsageRibbonViewModel() {
			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				Footer = InfoBarUsageFooterViewModel,
				IsApplicationButtonVisible = false,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Hidden,
				QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,
				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					IsCustomizeButtonVisible = false,
					Items = {
						new BarButtonViewModel("Save", "Save Document", DisabledCommand) {
							SmallIcon = ImageLoader.GetIcon("Save16.png"),
						},
					},
				},
				Tabs = {
					new RibbonTabViewModel("Home") {
						Groups = {

							new RibbonGroupViewModel("Documents") {
								Items = {
									new BarButtonViewModel("Open", "Open Document", DisabledCommand) {
										LargeIcon = ImageLoader.GetIcon("Open32.png"),
										SmallIcon = ImageLoader.GetIcon("Open16.png"),
									},
									new BarButtonViewModel("Save", "Save Document", DisabledCommand) {
										LargeIcon = ImageLoader.GetIcon("Save32.png"),
										SmallIcon = ImageLoader.GetIcon("Save16.png"),
									},
								},
							},
							new RibbonGroupViewModel("Sample") {
								Items = {
									new BarButtonViewModel("ShowFooter", InfoBarUsageOptions.ShowFooterMvvmCommand) {
										LargeIcon = ImageLoader.GetIcon("QuickStart32.png"),
										SmallIcon = ImageLoader.GetIcon("QuickStart16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									},
								}
							}

						}
					},
				}
			};
		}

		/// <summary>
		/// Gets the view model for the footer used by the "Info bar usage" MVVM-based sample.
		/// </summary>
		private RibbonFooterViewModel InfoBarUsageFooterViewModel {
			get {
				if (_infoBarUsageFooterViewModel is null) {
					_infoBarUsageFooterViewModel = new RibbonFooterViewModel() {
						// The RibbonFooterInfoBarContentViewModel can be used to define a footer
						// with features supported by the InfoBar control
						Content = new RibbonFooterInfoBarContentViewModel() {
							CanClose = InfoBarUsageOptions.CanClose,
							IsIconVisible = InfoBarUsageOptions.IsIconVisible,
							Message = "Use an info bar for essential app messages",
							Padding = InfoBarUsageOptions.Padding,
							Title = "InfoBar",
							Severity = InfoBarUsageOptions.Severity,
						},

						// Footer must not have padding so InfoBar can display edge-to-edge
						Padding = new Thickness(0),
					};
				}
				return _infoBarUsageFooterViewModel;
			}
		}


		private void OnBasicUsageOptionsViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
			// A footer is only visible when the footer property is populated

			// Update the XAML-based Footer
			// NOTE: The footer element is defined in XAML of this control
			basicUsageRibbonXaml.FooterContent = BasicUsageOptions.IsFooterVisible
				? footerXaml
				: null;

			// Update MVVM-based Footer
			BasicUsageFooterViewModel.Kind = BasicUsageOptions.FooterKind;
			BasicUsageRibbonViewModel.Footer = BasicUsageOptions.IsFooterVisible
				? BasicUsageFooterViewModel
				: null;
		}
		
		private void OnInfoBarUsageOptionsViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
			// NOTE: the XAML-based sample properties are defined with direct bindings to the sample options

			// Update the MVVM-based sample
			// Update properties to match the sample options
			if (InfoBarUsageFooterViewModel?.Content is RibbonFooterInfoBarContentViewModel infoBarViewModel) {

				infoBarViewModel.CanClose = InfoBarUsageOptions.CanClose;
				infoBarViewModel.IsIconVisible = InfoBarUsageOptions.IsIconVisible;
				infoBarViewModel.Padding = InfoBarUsageOptions.Padding;
				infoBarViewModel.Severity = InfoBarUsageOptions.Severity;
			}
		}

		private void ShowMvvmFooter() {
			// When the footer is closed the view model is cleared.
			// Show the footer again by re-assigning the view model that defines the footer.
			InfoBarUsageRibbonViewModel.Footer = this.InfoBarUsageFooterViewModel;
		}

		private void ShowXamlFooter() {
			// When the footer is closed the FooterContent is cleared.
			// Show the footer again by re-assigning the original RibbonFooterControl to the FooterContent.
			infoBarUsageRibbonXaml.FooterContent = infoBarFooterXaml;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The view model of options for the "Basic usage" sample.
		/// </summary>
		public BasicUsageOptionsViewModel BasicUsageOptions {
			get {
				if (_basicUsageOptionsViewModel is null) {
					_basicUsageOptionsViewModel = new();
					_basicUsageOptionsViewModel.PropertyChanged += this.OnBasicUsageOptionsViewModelPropertyChanged;
				}
				return _basicUsageOptionsViewModel;
			}
		}

		/// <summary>
		/// The ribbon view model for the "Basic usage" MVVM-based sample.
		/// </summary>
		public RibbonViewModel BasicUsageRibbonViewModel { get; }

		/// <summary>
		/// The view model of options for the "InfoBar usage" sample.
		/// </summary>
		public InfoBarUsageOptionsViewModel InfoBarUsageOptions {
			get {
				if (_infoBarUsageOptionsViewModel is null) {
					_infoBarUsageOptionsViewModel = new() {
						ShowFooterMvvmCommand = new DelegateCommand<object>(_ => ShowMvvmFooter()),
						ShowFooterXamlCommand = new DelegateCommand<object>(_ => ShowXamlFooter()),
					};
					_infoBarUsageOptionsViewModel.PropertyChanged += this.OnInfoBarUsageOptionsViewModelPropertyChanged;
				}
				return _infoBarUsageOptionsViewModel;
			}
		}

		/// <summary>
		/// The ribbon view model for the "Info bar usage" MVVM-based sample.
		/// </summary>
		public RibbonViewModel InfoBarUsageRibbonViewModel { get; }

	}

}
