using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;
using ActiproSoftware.UI.Avalonia.Input;
using ActiproSoftware.UI.Avalonia.Media;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GalleryInRibbon {

	public partial class MainControl : UserControl {

		private BarGalleryViewModel? _galleryViewModel;
		private ICollectionView? _colorItems;
		private ICommand? _configureOneRowLayoutCommand;
		private ICommand? _configureTwoRowLayoutCommand;
		private ICommand? _configureThreeRowLayoutCommand;
		private OptionsViewModel? _optionsViewModel;
		private DelegateCommand<ColorBarGalleryItemViewModel>? _setColorCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// Use 1-Row layout by default
			ConfigureOneRowLayout();

			// Configure the Ribbon view model for the "Basic usage" MVVM-based sample
			BasicUsageRibbonViewModel = CreateBasicUsageRibbonViewModel();
			basicUsageRibbonMvvm.DataContext = BasicUsageRibbonViewModel;

			Options.PropertyChanged += (s, e) => {
				// Update the enabled state of the gallery command
				if (e.PropertyName == nameof(OptionsViewModel.IsSetColorCommandEnabled))
					_setColorCommand?.RaiseCanExecuteChanged();

				// Update the view model from the current options
				ApplySampleOptionsToGallery(_galleryViewModel);
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Update properties on the gallery view model to match the current sample options.
		/// </summary>
		/// <param name="viewModel">The gallery to update.</param>
		private void ApplySampleOptionsToGallery(BarGalleryViewModel? viewModel) {
			var options = this.Options;
			if ((viewModel is not null) && (options is not null)) {
				// General properties
				viewModel.ItemSpacing = options.ItemSpacing;
				viewModel.ItemTemplate = options.ItemTemplate;
				viewModel.UseAccentedItemBorder = options.UseAccentedItemBorder;

				// Ribbon-specific properties
				viewModel.MinMediumRibbonColumnCount = options.MinMediumRibbonColumnCount;
				viewModel.MinLargeRibbonColumnCount = options.MinLargeRibbonColumnCount;
				viewModel.MaxRibbonColumnCount = options.MaxRibbonColumnCount;

				// Menu-specific properties
				viewModel.CanCategorize = options.CanCategorizeOnMenu;
				viewModel.CanFilter = options.CanFilterOnMenu;
				viewModel.MenuResizeMode = options.MenuResizeMode;
				viewModel.MinMenuColumnCount = options.MinMenuColumnCount;
				viewModel.MaxMenuColumnCount = options.MaxMenuColumnCount;
			}
		}

		private void ConfigureOneRowLayout() {
			Options.DesiredRowCount = 1;
			Options.ItemTemplate = ResolveResource<IDataTemplate>("LargeItemDataTemplate");
		}

		private void ConfigureThreeRowLayout() {
			Options.DesiredRowCount = 3;
			Options.ItemTemplate = ResolveResource<IDataTemplate>("SmallItemDataTemplate");
		}

		private void ConfigureTwoRowLayout() {
			Options.DesiredRowCount = 2;
			Options.ItemTemplate = ResolveResource<IDataTemplate>("MediumItemDataTemplate");
		}

		/// <summary>
		/// Creates the ribbon view model for the "Basic usage" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateBasicUsageRibbonViewModel() {
			//
			// Define Buttons
			//

			var configureOneRowLayoutButtonViewModel = new BarButtonViewModel("OneRowLayout", "1-Row Layout", ConfigureOneRowLayoutCommand) {
				Description = "Configures the gallery with a layout that will typically display as 1 row.",
				LargeIcon = ResolveResource<IImage>("OneRowLayoutLargeIcon"),
				SmallIcon = ResolveResource<IImage>("OneRowLayoutSmallIcon")
			};
			var configureTwoRowLayoutButtonViewModel = new BarButtonViewModel("TwoRowLayout", "2-Row Layout", ConfigureTwoRowLayoutCommand) {
				Description = "Configures the gallery with a layout that will typically display as 2 rows.",
				LargeIcon = ResolveResource<IImage>("TwoRowLayoutLargeIcon"),
				SmallIcon = ResolveResource<IImage>("TwoRowLayoutSmallIcon")
			};
			var configureThreeRowLayoutButtonViewModel = new BarButtonViewModel("ThreeRowLayout", "3-Row Layout", ConfigureThreeRowLayoutCommand) {
				Description = "Configures the gallery with a layout that will typically display as 3 rows.",
				LargeIcon = ResolveResource<IImage>("ThreeRowLayoutLargeIcon"),
				SmallIcon = ResolveResource<IImage>("ThreeRowLayoutSmallIcon")
			};

			//
			// Define Gallery
			//

			_galleryViewModel = new BarGalleryViewModel("ColorPicker", "Color Picker", SetColorCommand, ColorItems) {
				LargeIcon = ImageLoader.GetIcon("ColorPicker32.png"),
				SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
				BelowMenuItems = {
					configureOneRowLayoutButtonViewModel,
					configureTwoRowLayoutButtonViewModel,
					configureThreeRowLayoutButtonViewModel
				}
			};
			ApplySampleOptionsToGallery(_galleryViewModel);

			//
			// Define Ribbon
			//

			return new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				IsCollapsible = false,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Hidden,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("RibbonGallery") {
								SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
								Items = { _galleryViewModel },
							},
							new RibbonGroupViewModel("Layout") {
								CanAutoCollapse = false,
								Items= {
									new RibbonControlGroupViewModel() {
										Items = {
											configureOneRowLayoutButtonViewModel,
											configureTwoRowLayoutButtonViewModel,
											configureThreeRowLayoutButtonViewModel
										}
									},
								}
							}
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates a collection of gallery item view models.
		/// </summary>
		internal static ICollectionView CreateColorItems() {
			var warmColorsCategory = "Warm Colors";
			var coolColorsCategory = "Cool Colors";
			var neutralColorsCategory = "Neutral Colors";

			// Wrap the collection in an ICollectionView to support categories
			return BarGalleryViewModel.CreateCollectionView(new[] {
				new ColorBarGalleryItemViewModel(UIColor.Parse("#eeece1"), neutralColorsCategory, "Tan"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#ddd9c3"), neutralColorsCategory, "Tan, Darker 10%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#c4bd97"), neutralColorsCategory, "Tan, Darker 25%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#938953"), neutralColorsCategory, "Tan, Darker 50%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#494429"), neutralColorsCategory, "Tan, Darker 75%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#1d1b10"), neutralColorsCategory, "Tan, Darker 90%"),

				new ColorBarGalleryItemViewModel(UIColor.Parse("#1f497d"), coolColorsCategory, "Dark Blue"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#c6d9f0"), coolColorsCategory, "Dark Blue, Lighter 80%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#8db3e2"), coolColorsCategory, "Dark Blue, Lighter 60%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#548dd4"), coolColorsCategory, "Dark Blue, Lighter 40%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#17365d"), coolColorsCategory, "Dark Blue, Darker 25%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#17365d"), coolColorsCategory, "Dark Blue, Darker 50%"),

				new ColorBarGalleryItemViewModel(UIColor.Parse("#4f81bd"), coolColorsCategory, "Blue"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#dbe5f1"), coolColorsCategory, "Blue, Lighter 80%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#b8cce4"), coolColorsCategory, "Blue, Lighter 60%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#95b3d7"), coolColorsCategory, "Blue, Lighter 40%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#366092"), coolColorsCategory, "Blue, Darker 25%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#244061"), coolColorsCategory, "Blue, Darker 50%"),

				new ColorBarGalleryItemViewModel(UIColor.Parse("#c0504d"), warmColorsCategory, "Red"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#f2dbdb"), warmColorsCategory, "Red, Lighter 80%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#e5b9b7"), warmColorsCategory, "Red, Lighter 60%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#d99694"), warmColorsCategory, "Red, Lighter 40%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#953734"), warmColorsCategory, "Red, Darker 25%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#632423"), warmColorsCategory, "Red, Darker 50%"),

				new ColorBarGalleryItemViewModel(UIColor.Parse("#f79646"), warmColorsCategory, "Orange"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#fdeada"), warmColorsCategory, "Orange, Lighter 80%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#fbd5b5"), warmColorsCategory, "Orange, Lighter 60%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#fac090"), warmColorsCategory, "Orange, Lighter 40%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#e36c09"), warmColorsCategory, "Orange, Darker 25%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#974806"), warmColorsCategory, "Orange, Darker 50%"),

				new ColorBarGalleryItemViewModel(UIColor.Parse("#9bbb59"), coolColorsCategory, "Olive Green"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#ebf1dd"), coolColorsCategory, "Olive Green, Lighter 80%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#d6e3bc"), coolColorsCategory, "Olive Green, Lighter 60%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#c3d69b"), coolColorsCategory, "Olive Green, Lighter 40%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#76923c"), coolColorsCategory, "Olive Green, Darker 25%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#4f6128"), coolColorsCategory, "Olive Green, Darker 50%"),

				new ColorBarGalleryItemViewModel(UIColor.Parse("#4bacc6"), coolColorsCategory, "Aqua"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#dbeef3"), coolColorsCategory, "Aqua, Lighter 80%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#b6dde8"), coolColorsCategory, "Aqua, Lighter 60%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#93cddc"), coolColorsCategory, "Aqua, Lighter 40%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#31859b"), coolColorsCategory, "Aqua, Darker 25%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#205867"), coolColorsCategory, "Aqua, Darker 50%"),

				new ColorBarGalleryItemViewModel(UIColor.Parse("#8064a2"), coolColorsCategory, "Purple"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#e5e0ec"), coolColorsCategory, "Purple, Lighter 80%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#ccc0d9"), coolColorsCategory, "Purple, Lighter 60%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#b2a2c7"), coolColorsCategory, "Purple, Lighter 40%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#5f497a"), coolColorsCategory, "Purple, Darker 25%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#3f3151"), coolColorsCategory, "Purple, Darker 50%"),

				new ColorBarGalleryItemViewModel(UIColor.Parse("#ffffff"), neutralColorsCategory, "White"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#f2f2f2"), neutralColorsCategory, "White, Darker 5%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#d8d8d8"), neutralColorsCategory, "White, Darker 15%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#bfbfbf"), neutralColorsCategory, "White, Darker 25%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#a5a5a5"), neutralColorsCategory, "White, Darker 35%"),
				new ColorBarGalleryItemViewModel(UIColor.Parse("#7f7f7f"), neutralColorsCategory, "White, Darker 50%"),

			}, categorize: true);
		}

		private T? ResolveResource<T>(string key) {
			if (this.TryGetResource(key, out var resource) && (resource is T typedResource))
				return typedResource;
			return default;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The ribbon view model for the "Basic usage" MVVM-based sample.
		/// </summary>
		public RibbonViewModel BasicUsageRibbonViewModel { get; }

		/// <summary>
		/// The collection of color items to be displayed in the gallery.
		/// </summary>
		public ICollectionView ColorItems
			=> _colorItems ??= CreateColorItems();

		/// <summary>
		/// The command that will configure the gallery with a 1-row layout.
		/// </summary>
		public ICommand ConfigureOneRowLayoutCommand
			=> _configureOneRowLayoutCommand ??= new DelegateCommand<object>(_ => ConfigureOneRowLayout());

		/// <summary>
		/// The command that will configure the gallery with a 3-row layout.
		/// </summary>
		public ICommand ConfigureThreeRowLayoutCommand
			=> _configureThreeRowLayoutCommand ??= new DelegateCommand<object>(_ => ConfigureThreeRowLayout());

		/// <summary>
		/// The command that will configure the gallery with a 2-row layout.
		/// </summary>
		public ICommand ConfigureTwoRowLayoutCommand
			=> _configureTwoRowLayoutCommand ??= new DelegateCommand<object>(_ => ConfigureTwoRowLayout());

		/// <summary>
		/// The options for configuring the sample.
		/// </summary>
		public OptionsViewModel Options
			=> _optionsViewModel ??= new OptionsViewModel();

		/// <summary>
		/// The command to be invoked when a color gallery item is selected.
		/// </summary>
		public DelegateCommand<ColorBarGalleryItemViewModel> SetColorCommand
			=> _setColorCommand ??= new DelegateCommand<ColorBarGalleryItemViewModel>(
				executeAction: param => {
					if (param is not null) {
						ApplicationViewModel.Instance.MessageService?.ShowMessage(
							$"This is where you would apply the following selected color:\r\n\r\n{param.Value} {param.Label}",
							"Set Color",
							Avalonia.Controls.Notifications.NotificationType.Information);
					}
				},
				canExecuteFunc: _ => this.Options?.IsSetColorCommandEnabled == true
			);

	}

}
