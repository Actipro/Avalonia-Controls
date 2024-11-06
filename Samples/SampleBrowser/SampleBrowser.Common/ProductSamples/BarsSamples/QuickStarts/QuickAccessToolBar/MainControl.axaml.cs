using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using Avalonia.Controls;
using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.QuickAccessToolBar {

	public partial class MainControl : UserControl {

		private BasicUsageOptionsViewModel? _basicUsageOptionsViewModel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			BasicUsageRibbonViewModel = CreateBasicUsageRibbonViewModel();
			BasicUsageRibbonViewModel.PropertyChanged += this.OnBasicUsageRibbonViewModelPropertyChanged;

			CommonItemsRibbonViewModel = CreateCommonItemsRibbonViewModel();

			OverflowScenarioRibbonViewModel = CreateOverflowScenarioRibbonViewModel();

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the ribbon view model for the "Basic Usage" sample.
		/// </summary>
		private RibbonViewModel CreateBasicUsageRibbonViewModel() {

			var openControlViewModel = new BarButtonViewModel("Open", "Open Document") {
				LargeIcon = ImageLoader.GetIcon("Open32.png"),
				SmallIcon = ImageLoader.GetIcon("Open16.png"),
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			};
			var saveControlViewModel = new BarButtonViewModel("Save", "Save Document") {
				LargeIcon = ImageLoader.GetIcon("Save32.png"),
				SmallIcon = ImageLoader.GetIcon("Save16.png"),
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			};

			return new RibbonViewModel() {
				AllowLabelsOnQuickAccessToolBar = BasicUsageOptions.AllowLabels,
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarLocation = BasicUsageOptions.Location,
				QuickAccessToolBarMode = BasicUsageOptions.Mode,

				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					IsCustomizeButtonVisible = BasicUsageOptions.IsCustomizeButtonVisible,
					Items = {
						saveControlViewModel,
					},
				},

				Tabs = {
					new RibbonTabViewModel("Home") {
						Groups = {

							new RibbonGroupViewModel("Documents") {
								Items = {
									openControlViewModel,
									saveControlViewModel,
								},
							},

						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Common items" sample.
		/// </summary>
		private RibbonViewModel CreateCommonItemsRibbonViewModel() {

			var cutControlViewModel = new BarButtonViewModel("Cut") {
				SmallIcon = ImageLoader.GetIcon("Cut16.png"),
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			};
			var copyControlViewModel = new BarButtonViewModel("Copy") {
				SmallIcon = ImageLoader.GetIcon("Copy16.png"),
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			};
			var pasteControlViewModel = new BarButtonViewModel("Paste") {
				SmallIcon = ImageLoader.GetIcon("Paste16.png"),
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			};

			return new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,

				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					CommonItems = {
						cutControlViewModel,
						copyControlViewModel,
						pasteControlViewModel,
					},
					Items = {
						copyControlViewModel,
					},
				},

				Tabs = {
					new RibbonTabViewModel("Home") {
						Groups = {

							new RibbonGroupViewModel("Documents") {
								Items = {
									new BarButtonViewModel("Open", "Open Document") {
										LargeIcon = ImageLoader.GetIcon("Open32.png"),
										SmallIcon = ImageLoader.GetIcon("Open16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									},
									new BarButtonViewModel("Save", "Save Document") {
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
		/// Creates the ribbon view model for the "Overflow Scenario" sample.
		/// </summary>
		private RibbonViewModel CreateOverflowScenarioRibbonViewModel() {

			return new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,

				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					Items = {
						new BarButtonViewModel("Save") { SmallIcon = ImageLoader.GetIcon("Save16.png") },
						new BarButtonViewModel("Open") { SmallIcon = ImageLoader.GetIcon("Open16.png") },
						new BarButtonViewModel("Cut") { SmallIcon = ImageLoader.GetIcon("Cut16.png") },
						new BarButtonViewModel("Copy") { SmallIcon = ImageLoader.GetIcon("Copy16.png") },
						new BarButtonViewModel("Paste") { SmallIcon = ImageLoader.GetIcon("Paste16.png") },
						new BarButtonViewModel("Delete") { SmallIcon = ImageLoader.GetIcon("Delete16.png") },
						new BarButtonViewModel("Bold") { SmallIcon = ImageLoader.GetIcon("Bold16.png") },
						new BarButtonViewModel("Italic") { SmallIcon = ImageLoader.GetIcon("Italic16.png") },
						new BarButtonViewModel("Underline") { SmallIcon = ImageLoader.GetIcon("Underline16.png") },
						new BarButtonViewModel("AlignTextLeft") { SmallIcon = ImageLoader.GetIcon("AlignTextLeft16.png") },
						new BarButtonViewModel("AlignTextCenter") { SmallIcon = ImageLoader.GetIcon("AlignTextCenter16.png") },
						new BarButtonViewModel("AlignTextRight") { SmallIcon = ImageLoader.GetIcon("AlignTextRight16.png") },
						new BarButtonViewModel("AlignTextJustify") { SmallIcon = ImageLoader.GetIcon("AlignTextJustify16.png") },
						new BarButtonViewModel("Chart") { SmallIcon = ImageLoader.GetIcon("Chart16.png") },
						new BarButtonViewModel("Bookmark") { SmallIcon = ImageLoader.GetIcon("Bookmark16.png") },
						new BarButtonViewModel("CoverPage") { SmallIcon = ImageLoader.GetIcon("CoverPage16.png") },
						new BarButtonViewModel("CrossReference") { SmallIcon = ImageLoader.GetIcon("CrossReference16.png") },
						new BarButtonViewModel("DecreaseIndent") { SmallIcon = ImageLoader.GetIcon("DecreaseIndent16.png") },
						new BarButtonViewModel("IncreaseIndent") { SmallIcon = ImageLoader.GetIcon("IncreaseIndent16.png") },
						new BarButtonViewModel("Footer") { SmallIcon = ImageLoader.GetIcon("Footer16.png") },
						new BarButtonViewModel("Header") { SmallIcon = ImageLoader.GetIcon("Header16.png") },
					},
				},

				Tabs = {
					new RibbonTabViewModel("Home") {
						Groups = {

							new RibbonGroupViewModel("Documents") {
								Items = {
									new BarButtonViewModel("Open", "Open Document") {
										LargeIcon = ImageLoader.GetIcon("Open32.png"),
										SmallIcon = ImageLoader.GetIcon("Open16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									},
									new BarButtonViewModel("Save", "Save Document") {
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

		private void OnBasicUsageRibbonViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
			// Update the options to match changes in the ribbon properties
			BasicUsageOptions.AllowLabels = BasicUsageRibbonViewModel.AllowLabelsOnQuickAccessToolBar;
			BasicUsageOptions.IsCustomizeButtonVisible = (BasicUsageRibbonViewModel.QuickAccessToolBar?.IsCustomizeButtonVisible == true);
			BasicUsageOptions.Location = BasicUsageRibbonViewModel.QuickAccessToolBarLocation;
			BasicUsageOptions.Mode = BasicUsageRibbonViewModel.QuickAccessToolBarMode;
		}

		private void OnOptionsViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
			// Update the view models when the Options property is changed
			BasicUsageRibbonViewModel.AllowLabelsOnQuickAccessToolBar = BasicUsageOptions.AllowLabels;
			BasicUsageRibbonViewModel.QuickAccessToolBarLocation = BasicUsageOptions.Location;
			BasicUsageRibbonViewModel.QuickAccessToolBarMode = BasicUsageOptions.Mode;
			if (BasicUsageRibbonViewModel.QuickAccessToolBar is { } qat)
				qat.IsCustomizeButtonVisible = BasicUsageOptions.IsCustomizeButtonVisible;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The ribbon view model for the "Basic Usage" sample.
		/// </summary>
		public RibbonViewModel BasicUsageRibbonViewModel { get; }

		/// <summary>
		/// The view model of configuration options for the "Basic Usage" sample.
		/// </summary>
		public BasicUsageOptionsViewModel BasicUsageOptions {
			get {
				if (_basicUsageOptionsViewModel is null) {
					_basicUsageOptionsViewModel = new BasicUsageOptionsViewModel();
					_basicUsageOptionsViewModel.PropertyChanged += this.OnOptionsViewModelPropertyChanged;
				}
				return _basicUsageOptionsViewModel;
			}
		}

		/// <summary>
		/// The ribbon view model for the "Common items" sample.
		/// </summary>
		public RibbonViewModel CommonItemsRibbonViewModel { get; }

		/// <summary>
		/// The ribbon view model for the "Overflow Scenario" sample.
		/// </summary>
		public RibbonViewModel OverflowScenarioRibbonViewModel { get; }

	}

}
