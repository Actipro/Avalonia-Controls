using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;
using ActiproSoftware.UI.Avalonia.Input;
using ActiproSoftware.UI.Avalonia.Media;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Avalonia.Styling;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ColorPickers {

	public partial class MainControl : UserControl {

		BarGalleryViewModel? _basicUsageBarGalleryViewModel;
		BarGalleryViewModel? _showcaseFontColorBarGalleryViewModel;
		BarPopupButtonViewModel? _showcaseFontColorPickerButtonViewModel;
		BarGalleryViewModel? _showcaseHighlightColorBarGalleryViewModel;
		BarPopupButtonViewModel? _showcaseHighlightColorPickerButtonViewModel;
		BarGalleryViewModel? _standardMenuItemsBarGalleryViewModel;
		
		ICollectionView? _customColorItemsView;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// Configure the Ribbon view model for the "Showcase" MVVM-based sample
			ShowcaseRibbonViewModel = CreateShowcaseRibbonViewModel();
			showcaseRibbonMvvm.DataContext = ShowcaseRibbonViewModel;
			ShowcaseFontColorOptions.PropertyChanged += (s, _) => {
				if (s is ColorPickerOptionsViewModel options) {
					_showcaseFontColorPickerButtonViewModel!.SmallIcon = options.SmallIcon;
					UpdateBarGalleryViewModelFromOptions(_showcaseFontColorBarGalleryViewModel, options);
				}
			};
			ShowcaseHighlightColorOptions.PropertyChanged += (s, _) => {
				if (s is ColorPickerOptionsViewModel options) {
					_showcaseHighlightColorPickerButtonViewModel!.SmallIcon = options.SmallIcon;
					UpdateBarGalleryViewModelFromOptions(_showcaseHighlightColorBarGalleryViewModel, options);
				}
			};

			// Configure the Ribbon view model for the "Basic usage" MVVM-based sample
			BasicUsageRibbonViewModel = CreateBasicUsageRibbonViewModel();
			basicUsageRibbonMvvm.DataContext = BasicUsageRibbonViewModel;
			BasicUsageOptions.PropertyChanged += (s, _) => UpdateBarGalleryViewModelFromOptions(_basicUsageBarGalleryViewModel, s as ColorPickerOptionsViewModel);

			// Configure the Ribbon view model for the "Layout behavior" MVVM-based sample
			LayoutBehaviorRibbonViewModel = CreateLayoutBehaviorRibbonViewModel();
			layoutBehaviorRibbonMvvm.DataContext = LayoutBehaviorRibbonViewModel;

			// Configure the Ribbon view model for the "Default colors" MVVM-based sample
			DefaultColorsRibbonViewModel = CreateDefaultColorsRibbonViewModel();
			defaultColorsRibbonMvvm.DataContext = DefaultColorsRibbonViewModel;

			// Configure the Ribbon view model for the "Custom colors" MVVM-based sample
			CustomColorsRibbonViewModel = CreateCustomColorsRibbonViewModel();
			customColorsRibbonMvvm.DataContext = CustomColorsRibbonViewModel;

			// Configure the Ribbon view model for the "Preview command" MVVM-based sample
			PreviewCommandRibbonViewModel = CreatePreviewCommandRibbonViewModel();
			previewCommandRibbonMvvm.DataContext = PreviewCommandRibbonViewModel;

			// Configure the Ribbon view model for the "Standard menu items" MVVM-based sample
			StandardMenuItemsRibbonViewModel = CreateStandardMenuItemsRibbonViewModel();
			standardMenuItemsRibbonMvvm.DataContext = StandardMenuItemsRibbonViewModel;
			StandardMenuItemsOptions.PropertyChanged += (s, _) => UpdateBarGalleryViewModelFromOptions(_standardMenuItemsBarGalleryViewModel, s as ColorPickerOptionsViewModel);

			// Configure the Ribbon view model for the "Custom theme" MVVM-based sample
			CustomThemeRibbonViewModel = CreateCustomThemeRibbonViewModel();
			customThemeRibbonMvvm.DataContext = CustomThemeRibbonViewModel;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// A <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.
		/// </summary>
		private static ColorBarGalleryItemViewModel AutomaticColorGalleryItemViewModel { get; }
			= new ColorBarGalleryItemViewModel(Colors.Black, category: null, "Automatic") {
					LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
				};

		/// <summary>
		/// Creates the ribbon view model for the "Basic usage" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateBasicUsageRibbonViewModel() {
			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new BarPopupButtonViewModel("ColorPicker") {
										LargeIcon = ImageLoader.GetIcon("ColorPicker32.png"),
										SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											(_basicUsageBarGalleryViewModel = new BarGalleryViewModel("ColorPickerGallery") {
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												AreSurroundingSeparatorsAllowed = BasicUsageOptions.AreSurroundingSeparatorsAllowed,
												CanCategorize = BasicUsageOptions.CanCategorizeOnMenu,
												Command = BasicSetColorCommand,
												ItemSpacing = BasicUsageOptions.ItemSpacing,
												MinMenuColumnCount = BasicUsageOptions.MinColumnCount,
												MaxMenuColumnCount = BasicUsageOptions.MaxColumnCount,
												UseAccentedItemBorder = BasicUsageOptions.UseAccentedItemBorder,
												MinItemHeight = BasicUsageOptions.MinItemLength,
												MinItemWidth = BasicUsageOptions.MinItemLength,
												Items = new[] {
													new ColorBarGalleryItemViewModel(Colors.Red),
													new ColorBarGalleryItemViewModel(Colors.Orange),
													new ColorBarGalleryItemViewModel(Colors.Yellow),
													new ColorBarGalleryItemViewModel(Colors.Green),
													new ColorBarGalleryItemViewModel(Colors.Blue),
													new ColorBarGalleryItemViewModel(Colors.Purple),
													new ColorBarGalleryItemViewModel(UIColor.Parse("#ffffff")) { Label = "White" },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#888888")) { Label = "Gray" },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#000000")) { Label = "Black" },
												}
											})
										}
									},
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Custom colors" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateCustomColorsRibbonViewModel() {

			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new BarPopupButtonViewModel("ColorPicker") {
										LargeIcon = ImageLoader.GetIcon("ColorPicker32.png"),
										SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											new BarGalleryViewModel("ColorPickerGallery") {
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												Command = BasicSetColorCommand,
												ItemSpacing = 4,
												MinMenuColumnCount = 8,
												MaxMenuColumnCount = 8,
												UseAccentedItemBorder = true,
												Items = CustomColorItemsView,
											}
										}
									},
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Custom theme" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateCustomThemeRibbonViewModel() {

			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new BarPopupButtonViewModel("ColorPicker") {
										LargeIcon = ImageLoader.GetIcon("ColorPicker32.png"),
										SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											new BarGalleryViewModel("ColorPickerGallery") {
												Command = BasicSetColorCommand,
												ItemSpacing = 4,
												MinMenuColumnCount = 7,
												UseAccentedItemBorder = true,
												ItemContainerTheme = this.FindResource("BarGalleryItemCircleTheme") as ControlTheme,
												ItemTemplate = this.FindResource("CircleStyleGalleryItemItemplate") as IDataTemplate,
												Items = new[] {
													new ColorBarGalleryItemViewModel(Colors.Red),
													new ColorBarGalleryItemViewModel(Colors.Orange),
													new ColorBarGalleryItemViewModel(Colors.Yellow),
													new ColorBarGalleryItemViewModel(Colors.Green),
													new ColorBarGalleryItemViewModel(Colors.Blue),
													new ColorBarGalleryItemViewModel(Colors.Purple),
													new ColorBarGalleryItemViewModel(Colors.Gray),
												}
											}
										}
									},
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Default colors" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateDefaultColorsRibbonViewModel() {

			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new BarPopupButtonViewModel("ColorPicker") {
										LargeIcon = ImageLoader.GetIcon("ColorPicker32.png"),
										SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											new BarGalleryViewModel("ColorPickerGallery") {
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												Command = BasicSetColorCommand,
												ItemSpacing = 4,
												MinMenuColumnCount = 10,
												MaxMenuColumnCount = 10,
												UseAccentedItemBorder = true,
												Items = BarGalleryViewModel.CreateCollectionView(
													ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection(),
													categorize: true
												),
											}
										}
									},
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Layout behavior" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateLayoutBehaviorRibbonViewModel() {

			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new BarPopupButtonViewModel("ColorPicker") {
										LargeIcon = ImageLoader.GetIcon("ColorPicker32.png"),
										SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											new BarGalleryViewModel("ColorPickerGallery") {
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												Command = BasicSetColorCommand,
												ItemSpacing = 4,
												MinMenuColumnCount = 5,
												MaxMenuColumnCount = 5,
												UseAccentedItemBorder = true,
												MinItemHeight = 22,
												MinItemWidth = 22,
												Items = new[] {

													new ColorBarGalleryItemViewModel(Colors.Black) { Label = "Automatic", Description = "LayoutBehavior = MenuItem", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },

													new ColorBarGalleryItemViewModel(UIColor.Parse("#ed7d31")) { Description = "LayoutBehavior = Default" },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#a5a5a5")) { Description = "LayoutBehavior = Default" },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#ffc000")) { Description = "LayoutBehavior = Default" },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#5b9bd5")) { Description = "LayoutBehavior = Default" },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#70ad47")) { Description = "LayoutBehavior = Default" },

													new ColorBarGalleryItemViewModel(UIColor.Parse("#fbe5d5")) { Description = "LayoutBehavior = GroupStart", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#ededed")) { Description = "LayoutBehavior = GroupStart", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#fff2cc")) { Description = "LayoutBehavior = GroupStart", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#deebf6")) { Description = "LayoutBehavior = GroupStart", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#e2efd9")) { Description = "LayoutBehavior = GroupStart", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupStart },

													new ColorBarGalleryItemViewModel(UIColor.Parse("#f4b183")) { Description = "LayoutBehavior = GroupInner", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#c9c9c9")) { Description = "LayoutBehavior = GroupInner", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#ffd965")) { Description = "LayoutBehavior = GroupInner", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#9cc3e5")) { Description = "LayoutBehavior = GroupInner", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#a8d08d")) { Description = "LayoutBehavior = GroupInner", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupInner },

													new ColorBarGalleryItemViewModel(UIColor.Parse("#833c0b")) { Description = "LayoutBehavior = GroupEnd", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#525252")) { Description = "LayoutBehavior = GroupEnd", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#7f6000")) { Description = "LayoutBehavior = GroupEnd", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#1e4e79")) { Description = "LayoutBehavior = GroupEnd", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },
													new ColorBarGalleryItemViewModel(UIColor.Parse("#375623")) { Description = "LayoutBehavior = GroupEnd", LayoutBehavior = BarGalleryItemLayoutBehavior.GroupEnd },

												}
											}
										}
									},
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Preview command" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreatePreviewCommandRibbonViewModel() {

			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new BarPopupButtonViewModel("ColorPicker") {
										LargeIcon = ImageLoader.GetIcon("ColorPicker32.png"),
										SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											new BarGalleryViewModel("ColorPickerGallery") {
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												Command = PreviewCommandOptions.SetColorCommand,
												ItemSpacing = 4,
												MinMenuColumnCount = 10,
												MaxMenuColumnCount = 10,
												UseAccentedItemBorder = true,
												Items = BarGalleryViewModel.CreateCollectionView(
													ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection(),
													categorize: true
												),
											}
										}
									},
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Showcase" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateShowcaseRibbonViewModel() {
			var primaryColorsCategory = "Primary Colors";
			var secondaryColorsCategory = "Secondary Colors";
			var categoryColorsCategory = "Category Colors";

			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("CommonColorPickers") {
								Items = {
									new RibbonControlGroup() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = {
											(_showcaseFontColorPickerButtonViewModel = new BarPopupButtonViewModel("FontColor") {
												SmallIcon = ShowcaseFontColorOptions.SmallIcon,
												ToolBarItemVariantBehavior = ItemVariantBehavior.All,
												MenuItems = {
													(_showcaseFontColorBarGalleryViewModel = new BarGalleryViewModel("FontColorPickerGallery") {
														AreSurroundingSeparatorsAllowed = ShowcaseFontColorOptions.AreSurroundingSeparatorsAllowed,
														Command = ShowcaseFontColorOptions.SetColorCommand,
														ItemSpacing = ShowcaseFontColorOptions.ItemSpacing,
														ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
														MinItemHeight = ShowcaseFontColorOptions.MinItemLength,
														MinItemWidth = ShowcaseFontColorOptions.MinItemLength,
														MinMenuColumnCount = ShowcaseFontColorOptions.MinColumnCount,
														MaxMenuColumnCount = ShowcaseFontColorOptions.MaxColumnCount,
														UseAccentedItemBorder = ShowcaseFontColorOptions.UseAccentedItemBorder,
														Items = BarGalleryViewModel.CreateCollectionView(
															new[] { AutomaticColorGalleryItemViewModel }
																.Concat(ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection())
															, categorize: true),
													}),
													new BarPopupButtonViewModel("FontColorMoreColors", "More Colors...") {
														Command = NotImplementedCommand,
														SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
													}
												}
											}),
											(_showcaseHighlightColorPickerButtonViewModel = new BarPopupButtonViewModel("HighlightColor") {
												SmallIcon = ShowcaseHighlightColorOptions.SmallIcon,
												ToolBarItemVariantBehavior = ItemVariantBehavior.All,
												MenuItems = {
													(_showcaseHighlightColorBarGalleryViewModel = new BarGalleryViewModel("HighlightColorPickerGallery") {
														AreSurroundingSeparatorsAllowed = ShowcaseHighlightColorOptions.AreSurroundingSeparatorsAllowed,
														Command = ShowcaseHighlightColorOptions.SetColorCommand,
														ItemSpacing = ShowcaseHighlightColorOptions.ItemSpacing,
														ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
														MinItemHeight = ShowcaseHighlightColorOptions.MinItemLength,
														MinItemWidth = ShowcaseHighlightColorOptions.MinItemLength,
														MinMenuColumnCount = ShowcaseHighlightColorOptions.MinColumnCount,
														MaxMenuColumnCount = ShowcaseHighlightColorOptions.MaxColumnCount,
														UseAccentedItemBorder = ShowcaseHighlightColorOptions.UseAccentedItemBorder,
														Items = BarGalleryViewModel.CreateCollectionView(ColorBarGalleryItemViewModel.CreateDefaultTextHighlightCollection(), categorize: true),
													}),
													new BarPopupButtonViewModel("StopHighlighting") {
														Command = NotImplementedCommand,
													}
												}
											}),
										}
									}
								},
							},
							new RibbonGroupViewModel("Other") {
								Items = {
									new BarPopupButtonViewModel("MoreSamples") {
										LargeIcon = ImageLoader.GetIcon("ColorPicker32.png"),
										SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {
											new BarPopupButtonViewModel("MenuItems") {
												MenuItems = {
													new BarGalleryViewModel("MenuItemsPickerGallery") {
														Command = BasicSetColorCommand,
														ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
														Items = BarGalleryViewModel.CreateCollectionView(new ColorBarGalleryItemViewModel[] {
															new (Colors.Red, primaryColorsCategory) { KeyTipText = "R", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
															new (Colors.Yellow, primaryColorsCategory) { KeyTipText = "Y", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
															new (Colors.Blue, primaryColorsCategory) { KeyTipText = "B", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
															new (Colors.Orange, secondaryColorsCategory) { KeyTipText = "O", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
															new (Colors.Green, secondaryColorsCategory) { KeyTipText = "G", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
															new (Colors.Purple, secondaryColorsCategory) { KeyTipText = "P", LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem },
														}, categorize: true)
													},
													new BarPopupButtonViewModel("MenuItemsMoreColors", "More Colors...") {
														Command = NotImplementedCommand,
														SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
													}
												}
											},
											new BarPopupButtonViewModel("CustomStyle") {
												MenuItems = {
													new BarGalleryViewModel("MenuItemsPickerGallery") {
														AreSurroundingSeparatorsAllowed = false,
														Command = BasicSetColorCommand,
														ItemSpacing = 4,
														MinMenuColumnCount = 7,
														UseAccentedItemBorder = true,
														ItemContainerTheme = this.FindResource("BarGalleryItemCircleTheme") as ControlTheme,
														ItemTemplate = this.FindResource("CircleStyleGalleryItemItemplate") as IDataTemplate,
														Items = BarGalleryViewModel.CreateCollectionView(new ColorBarGalleryItemViewModel[] {
															new (UIColor.Parse("#f04f58"), categoryColorsCategory, "Red") { KeyTipText = "R" },
															new (UIColor.Parse("#f1a247"), categoryColorsCategory, "Orange") { KeyTipText = "O" },
															new (UIColor.Parse("#f3cf4a"), categoryColorsCategory, "Yellow") { KeyTipText = "Y" },
															new (UIColor.Parse("#5dd260"), categoryColorsCategory, "Green") { KeyTipText = "G" },
															new (UIColor.Parse("#5c85f5"), categoryColorsCategory, "Blue") { KeyTipText = "B" },
															new (UIColor.Parse("#b163d3"), categoryColorsCategory, "Purple") { KeyTipText = "P" },
															new (UIColor.Parse("#9c9ca0"), categoryColorsCategory, "Gray") { KeyTipText = "A" },
														}, categorize: true)
													},
													new BarSeparatorViewModel(),
													new BarPopupButtonViewModel("CustomStyleCustomize", "Customize...") {
														Command = NotImplementedCommand,
													}
												}
											}
										}
									}
								}
							}
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Standard menu items" MVVM-based sample.
		/// </summary>
		private RibbonViewModel CreateStandardMenuItemsRibbonViewModel() {

			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new BarPopupButtonViewModel("ColorPicker") {
										LargeIcon = ImageLoader.GetIcon("ColorPicker32.png"),
										SmallIcon = ImageLoader.GetIcon("ColorPicker16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {

											// Menu item (before)
											new BarToggleButtonViewModel("HighContrastOnly", "Show high-contrast only", NotImplementedCommand),

											// Color gallery
											(_standardMenuItemsBarGalleryViewModel = new BarGalleryViewModel("ColorPickerGallery") {
												AreSurroundingSeparatorsAllowed = StandardMenuItemsOptions.AreSurroundingSeparatorsAllowed,
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												Command = BasicSetColorCommand,
												ItemSpacing = StandardMenuItemsOptions.ItemSpacing,
												MinMenuColumnCount = StandardMenuItemsOptions.MinColumnCount,
												MaxMenuColumnCount = StandardMenuItemsOptions.MaxColumnCount,
												UseAccentedItemBorder = StandardMenuItemsOptions.UseAccentedItemBorder,
												Items = BarGalleryViewModel.CreateCollectionView(
													new[] { AutomaticColorGalleryItemViewModel }.Concat(ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()),
													categorize: true
												),
											}),

											// Menu item (after)
											new BarButtonViewModel("MoreColors", "More colors...", NotImplementedCommand) { SmallIcon = ImageLoader.GetIcon("ColorPicker16.png") },

										}
									},
								},
							},
						}
					},
				}
			};
		}

		private void UpdateBarGalleryViewModelFromOptions(BarGalleryViewModel? barGalleryViewModel, ColorPickerOptionsViewModel? optionsViewModel) {
			if (barGalleryViewModel is null || optionsViewModel is null)
				return;

			barGalleryViewModel.AreSurroundingSeparatorsAllowed = optionsViewModel.AreSurroundingSeparatorsAllowed;
			barGalleryViewModel.CanCategorize = optionsViewModel.CanCategorizeOnMenu;
			barGalleryViewModel.UseAccentedItemBorder = optionsViewModel.UseAccentedItemBorder;
			barGalleryViewModel.ItemSpacing = optionsViewModel.ItemSpacing;
			barGalleryViewModel.MaxMenuColumnCount = optionsViewModel.MaxColumnCount;
			barGalleryViewModel.MinMenuColumnCount = optionsViewModel.MinColumnCount;
			barGalleryViewModel.MinItemHeight = barGalleryViewModel.MinItemWidth = optionsViewModel.MinItemLength;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// A basic command to be executed when a color is selected from a color picker.
		/// </summary>
		public ICommand BasicSetColorCommand { get; }
			= new DelegateCommand<ColorBarGalleryItemViewModel>(
				param => {
					if (param is not null) {
						ApplicationViewModel.Instance.MessageService?.ShowMessage(
							$"You selected the color {param.Label} [{UIColor.FromRgb(param.Value).ToHexString()}].",
							"Set Color",
							Avalonia.Controls.Notifications.NotificationType.Information);
					}
				}
			);

		/// <summary>
		/// The options view model for the "Basic usage" sample.
		/// </summary>
		public ColorPickerOptionsViewModel BasicUsageOptions { get; }
			= new ColorPickerOptionsViewModel() {
				CanCategorizeOnMenu = false,
				MinColumnCount = 3,
				MinItemLength = 20,
			};

		/// <summary>
		/// The ribbon view model for the "Basic usage" MVVM-based sample.
		/// </summary>
		public RibbonViewModel BasicUsageRibbonViewModel { get; }

		/// <summary>
		/// The gallery item view models for a color picker using custom colors and auto-generated shades.
		/// </summary>
		public ICollectionView CustomColorItemsView {
			get {
				if (_customColorItemsView is null) {

					// Define the base colors
					var category = "Custom Theme Colors";
					var customBaseThemeColors = new ColorBarGalleryItemViewModel[] {
						new (UIColor.Parse("#dfe3e5"), category, "Ice Blue"),
						new (UIColor.Parse("#335b74"), category, "Dark Teal"),
						new (UIColor.Parse("#1cade4"), category, "Turquoise"),
						new (UIColor.Parse("#2683c6"), category, "Blue"),
						new (UIColor.Parse("#27ced7"), category, "Turquoise"),
						new (UIColor.Parse("#42ba97"), category, "Green"),
						new (UIColor.Parse("#3e8853"), category, "Dark Green"),
						new (UIColor.Parse("#62a39f"), category, "Teal"),
					};

					// ColorBarGalleryItemViewModel.CreateShadedCollection can be used to create a new collection that includes
					// all of the given base colors plus 5 additional shades for each color. The LayoutBehavior of each
					// collection of shades is configured to display the shades as a group.
					// - The first 8 colors are the base theme colors.
					// - The next 8 colors are the first of five alternate shades for each base theme color (BarGalleryItemLayoutBehavior.GroupStart).
					// - The next 8 colors are the second of five alternate shades (BarGalleryItemLayoutBehavior.GroupInner).
					// - The next 8 colors are the third of five alternate shades (BarGalleryItemLayoutBehavior.GroupInner).
					// - The next 8 colors are the forth of five alternate shades (BarGalleryItemLayoutBehavior.GroupInner).
					// - The next 8 colors are the last of five alternate shades (BarGalleryItemLayoutBehavior.GroupEnd).
					//
					// The BarMenuGallery should be configured with with a fixed size of 8 columns to properly display these colors
					_customColorItemsView = BarGalleryViewModel.CreateCollectionView(
						ColorBarGalleryItemViewModel.CreateShadedCollection(customBaseThemeColors),
						categorize: true
					);

				}
				return _customColorItemsView;
			}
		}

		/// <summary>
		/// The ribbon view model for the "Custom colors" MVVM-based sample.
		/// </summary>
		public RibbonViewModel CustomColorsRibbonViewModel { get; }

		/// <summary>
		/// The ribbon view model for the "Custom theme" MVVM-based sample.
		/// </summary>
		public RibbonViewModel CustomThemeRibbonViewModel { get; }

		/// <summary>
		/// The gallery item view models for a color picker using the default collection.
		/// </summary>
		public ICollectionView DefaultColorItemsView { get; }
			// The default collection in ColorBarGalleryItemViewModel is based on 70 colors:
			// - The first 10 colors are the base theme colors.
			// - The next 10 colors are the first of five alternate shades for each base theme color (BarGalleryItemLayoutBehavior.GroupStart).
			// - The next 10 colors are the second of five alternate shades (BarGalleryItemLayoutBehavior.GroupInner).
			// - The next 10 colors are the third of five alternate shades (BarGalleryItemLayoutBehavior.GroupInner).
			// - The next 10 colors are the forth of five alternate shades (BarGalleryItemLayoutBehavior.GroupInner).
			// - The next 10 colors are the last of five alternate shades (BarGalleryItemLayoutBehavior.GroupEnd).
			// - The last 10 colors are standard colors that will not have alternate shades.
			//
			// The BarMenuGallery should be configured with with a fixed size of 10 columns to properly display these colors
			= BarGalleryViewModel.CreateCollectionView(ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection(), categorize: true);

		/// <summary>
		/// The gallery item view models for a color picker using the default collection with the additional 'Automatic' selection.
		/// </summary>
		public ICollectionView DefaultColorItemsWithAutomaticView { get; }
			// Prepend the default colors with an "Automatic" view model that is configured as BarGalleryItemLayoutBehavior.MenuItem
			= BarGalleryViewModel.CreateCollectionView(
				new[] { AutomaticColorGalleryItemViewModel }.Concat(ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()),
				categorize: true
			);

		/// <summary>
		/// The ribbon view model for the "Default colors" MVVM-based sample.
		/// </summary>
		public RibbonViewModel DefaultColorsRibbonViewModel { get; }

		/// <summary>
		/// The ribbon view model for the "Layout behavior" MVVM-based sample.
		/// </summary>
		public RibbonViewModel LayoutBehaviorRibbonViewModel { get; }

		/// <summary>
		/// A special command associated with controls that are for demonstration purposes only and provide no implemented functionality.
		/// </summary>
		public ICommand NotImplementedCommand { get; }
			= ApplicationViewModel.Instance.NotImplementedCommand;

		/// <summary>
		/// The options view model for the "Preview command" sample.
		/// </summary>
		public ColorPickerOptionsViewModel PreviewCommandOptions { get; } 
			= new ColorPickerOptionsViewModel() {
				SelectedColor = Colors.Yellow,
			};

		/// <summary>
		/// The ribbon view model for the "Preview command" MVVM-based sample.
		/// </summary>
		public RibbonViewModel PreviewCommandRibbonViewModel { get; }

		/// <summary>
		/// The options view model for the "Showcase" sample's font color picker.
		/// </summary>
		public ColorPickerOptionsViewModel ShowcaseFontColorOptions { get; }
			= new ColorPickerOptionsViewModel() {
				MinColumnCount = 10,
				MaxColumnCount = 10,
			};

		/// <summary>
		/// The options view model for the "Showcase" sample's highlight color picker.
		/// </summary>
		public ColorPickerOptionsViewModel ShowcaseHighlightColorOptions { get; }
			= new ColorPickerOptionsViewModel() {
				BaseImageSmall = "TextHighlightColor16.png",
				MinItemLength = 30,
				SelectedColor = Colors.Yellow,
			};

		/// <summary>
		/// The ribbon view model for the "Showcase" MVVM-based sample.
		/// </summary>
		public RibbonViewModel ShowcaseRibbonViewModel { get; }

		/// <summary>
		/// The ribbon view model for the "Standard menu items" MVVM-based sample.
		/// </summary>
		public RibbonViewModel StandardMenuItemsRibbonViewModel { get; }

		/// <summary>
		/// The options view model for the "Standard menu items" sample.
		/// </summary>
		public ColorPickerOptionsViewModel StandardMenuItemsOptions { get; }
			= new ColorPickerOptionsViewModel() {
				MinColumnCount = 10,
				MaxColumnCount = 10,
			};

	}

}
