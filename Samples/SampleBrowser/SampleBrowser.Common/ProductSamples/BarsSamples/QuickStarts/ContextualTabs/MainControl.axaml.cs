using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using System.ComponentModel;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ContextualTabs {

	public partial class MainControl : UserControl {

		private OptionsViewModel? _optionsViewModel;

		private RibbonContextualTabGroupViewModel? _pictureToolsContextualTabGroup;
		private RibbonContextualTabGroupViewModel? _tableToolsContextualTabGroup;

		private BarToggleButtonViewModel? _togglePictureToolsButton;
		private BarToggleButtonViewModel? _toggleTableToolsButton;

		private ICommand? _togglePictureToolsCommand;
		private ICommand? _toggleTableToolsCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// Initialize the Ribbon view models
			InitializeRibbonViewModels();

			// Configure the Ribbon view model for the MVVM popup menu sample
			basicUsageRibbonMvvm.DataContext = BasicUsageRibbonViewModel;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Updates view models with the current sample options.
		/// </summary>
		private void ApplySampleOptionsToViewModels() {
			if (Options is null)
				return;

			// Update active contextual tab groups
			if (_pictureToolsContextualTabGroup is not null)
				_pictureToolsContextualTabGroup.IsActive = Options.IsPictureToolsContextualTabGroupVisible;
			if (_tableToolsContextualTabGroup is not null)
				_tableToolsContextualTabGroup.IsActive = Options.IsTableToolsContextualTabGroupVisible;

			// Update toggle state of buttons
			if (_togglePictureToolsButton is not null)
				_togglePictureToolsButton.IsChecked = Options.IsPictureToolsContextualTabGroupVisible;
			if (_toggleTableToolsButton is not null)
				_toggleTableToolsButton.IsChecked = Options.IsTableToolsContextualTabGroupVisible;
		}

		/// <summary>
		/// Initializes the view models for the MVVM-based ribbon.
		/// </summary>
		private void InitializeRibbonViewModels() {

			//
			// Define Contextual Tab Groups
			//

			// The IsActive property of a RibbonContextualTabGroupViewModel will determine the visibility of all
			// RibbonTabViewModel instances whose ContextualTabGroupKey matches RibbonContextualTabGroupViewModel.Key
			_pictureToolsContextualTabGroup = new RibbonContextualTabGroupViewModel(ContextualTabGroupKeys.PictureTools) {
				IsActive = Options?.IsPictureToolsContextualTabGroupVisible ?? false
			};
			_tableToolsContextualTabGroup = new RibbonContextualTabGroupViewModel(ContextualTabGroupKeys.TableTools) {
				IsActive = Options?.IsTableToolsContextualTabGroupVisible ?? false
			};

			//
			// Define Toggle Buttons
			//

			_togglePictureToolsButton = new BarToggleButtonViewModel("TogglePictureTools", "Picture Tools") {
				Command = TogglePictureToolsCommand,
				Description = "Click to toggle the visibility of the Picture Tools contextual tab group.",
				IsChecked = Options?.IsPictureToolsContextualTabGroupVisible ?? false,
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
				LargeIcon = ImageLoader.GetIcon("Picture32.png"),
				SmallIcon = ImageLoader.GetIcon("Picture16.png"),
			};
			_toggleTableToolsButton = new BarToggleButtonViewModel("ToggleTableTools", "Table Tools") {
				Command = ToggleTableToolsCommand,
				Description = "Click to toggle the visibility of the Table Tools contextual tab group.",
				IsChecked = Options?.IsTableToolsContextualTabGroupVisible ?? false,
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
				LargeIcon = ImageLoader.GetIcon("Table32.png"),
				SmallIcon = ImageLoader.GetIcon("Table16.png"),
			};

			//
			// Configure Ribbon
			//

			BasicUsageRibbonViewModel = new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				IsCollapsible = false,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				ContextualTabGroups = {
					_pictureToolsContextualTabGroup,
					_tableToolsContextualTabGroup,
				},
				Tabs = {

					//
					// Standard Tabs (Always Visible)
					//

					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("ContextualTabs") {
								Items = {
									_togglePictureToolsButton,
									_toggleTableToolsButton,
								}
							},
						},
					},
					new RibbonTabViewModel("Home") {
						Groups = {
							new RibbonGroupViewModel("Placeholder") {
								Items = {
									new BarButtonViewModel("Placeholder", "Non-Contextual Tab for Demo") {
										LargeIcon = ImageLoader.GetIcon("QuickStart32.png"),
										SmallIcon = ImageLoader.GetIcon("QuickStart16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									}
								}
							}
						}
					},

					//
					// Picture Tools Contextual Tabs
					//

					new RibbonTabViewModel("PictureFormat") {
						ContextualTabGroupKey = ContextualTabGroupKeys.PictureTools,
						Groups = {
							new RibbonGroupViewModel("ImageSize") {
								LargeIcon = ImageLoader.GetIcon("Height32.png"),
								SmallIcon = ImageLoader.GetIcon("Height16.png"),
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
										Items = {
											new BarTextBoxViewModel("PictureHeight", "Height") { RequestedWidth = 75, Text = "3.5\"" },
											new BarTextBoxViewModel("PictureWidth", "Width") { RequestedWidth = 75, Text = "5.0\"" },
										}
									}
								}
							}
						}
					},

					//
					// Table Tools Contextual Tabs
					//

					new RibbonTabViewModel("TableDesign") {
						ContextualTabGroupKey = ContextualTabGroupKeys.TableTools,
						Groups = {
							new RibbonGroupViewModel("TableStyleOptions") {
								LargeIcon = ImageLoader.GetIcon("Table32.png"),
								SmallIcon = ImageLoader.GetIcon("Table16.png"),
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
										Items = {
											new BarCheckBoxViewModel("HeaderRow"),
											new BarCheckBoxViewModel("TotalRow"),
											new BarCheckBoxViewModel("BandedRows"),
											new BarCheckBoxViewModel("FirstColumn"),
											new BarCheckBoxViewModel("LastColumn"),
											new BarCheckBoxViewModel("BandedColumns"),
										}
									}
								}
							}
						}
					},
					new RibbonTabViewModel("Layout") {
						ContextualTabGroupKey = ContextualTabGroupKeys.TableTools,
						Groups = {
							new RibbonGroupViewModel("CellSize") {
								LargeIcon = ImageLoader.GetIcon("Height32.png"),
								SmallIcon = ImageLoader.GetIcon("Height16.png"),
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
										Items = {
											new BarTextBoxViewModel("TableCellHeight", "Height") { RequestedWidth = 75, Text = "0.20\"" },
											new BarTextBoxViewModel("TableCellWidth", "Width") { RequestedWidth = 75, Text = "2.15\"" },
										}
									},
								}
							},
							new RibbonGroupViewModel("Alignment") {
								LargeIcon = ImageLoader.GetIcon("AlignTextCenter32.png"),
								SmallIcon = ImageLoader.GetIcon("AlignTextCenter16.png"),
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
										Items = {
											new BarToggleButtonViewModel("Left") { SmallIcon = ImageLoader.GetIcon("AlignTextLeft16.png") },
											new BarToggleButtonViewModel("Center") { SmallIcon = ImageLoader.GetIcon("AlignTextCenter16.png") },
											new BarToggleButtonViewModel("Right") { SmallIcon = ImageLoader.GetIcon("AlignTextRight16.png") },
										}
									}
								}
							}
						}
					}

				},
			};

		}

		private void OnOptionsViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
			// Update the view models when the Options property is changed
			ApplySampleOptionsToViewModels();
		}

		/// <summary>
		/// Gets the command to toggle the Picture Tools contextual tab group.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		private ICommand TogglePictureToolsCommand {
			get {
				if (_togglePictureToolsCommand is null) {
					_togglePictureToolsCommand = new DelegateCommand<object>(
						_ => Options.IsPictureToolsContextualTabGroupVisible = !Options.IsPictureToolsContextualTabGroupVisible
					);
				}
				return _togglePictureToolsCommand;
			}
		}

		/// <summary>
		/// Gets the command to toggle the Table Tools contextual tab group.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		private ICommand ToggleTableToolsCommand {
			get {
				if (_toggleTableToolsCommand is null) {
					_toggleTableToolsCommand = new DelegateCommand<object>(
						_ => Options.IsTableToolsContextualTabGroupVisible = !Options.IsTableToolsContextualTabGroupVisible
					);
				}
				return _toggleTableToolsCommand;
			}
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The ribbon view model for the MVVM sample.
		/// </summary>
		public RibbonViewModel? BasicUsageRibbonViewModel { get; set; }

		public OptionsViewModel Options {
			get {
				if (_optionsViewModel is null) {
					_optionsViewModel = new OptionsViewModel();
					_optionsViewModel.PropertyChanged += this.OnOptionsViewModelPropertyChanged;
				}
				return _optionsViewModel;
			}
		}

	}

}
