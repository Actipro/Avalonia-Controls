using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.SampleBrowser.SampleData;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ComboBoxAndEditors {

	public partial class MainControl : UserControl {

		private BarComboBoxViewModel? _basicUsageBarComboBoxViewModel;

		private ICommand? _comboBoxGalleryCommand;
		private ICommand? _comboBoxUnmatchedNumberTextCommand;
		private ICommand? _comboBoxUnmatchedTextCommand;
		private ICommand? _textBoxCommitCommand;

		private IEnumerable? _comboBoxColorItems;
		private IEnumerable? _comboBoxEnumItems;
		private IEnumerable? _comboBoxFontFamilyItems;
		private IEnumerable? _comboBoxFontSizeItems;
		private IEnumerable? _comboBoxNumberItems;
		private IEnumerable? _comboBoxPersonItems;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			SetPreview(null);

			BasicComboBoxUsageRibbonViewModel = CreateBasicComboBoxUsageRibbonViewModel();
			BasicComboBoxUsageOptions.PropertyChanged += (s, _) => {
				if ((s is ComboBoxOptionsViewModel options) && (_basicUsageBarComboBoxViewModel is not null)) {
					_basicUsageBarComboBoxViewModel.CanCategorize = BasicComboBoxUsageOptions.CanCategorizeOnMenu;
					_basicUsageBarComboBoxViewModel.IsEditable = BasicComboBoxUsageOptions.IsEditable;
					_basicUsageBarComboBoxViewModel.IsReadOnly = BasicComboBoxUsageOptions.IsReadOnly;
					_basicUsageBarComboBoxViewModel.IsTextCompletionEnabled = BasicComboBoxUsageOptions.IsTextCompletionEnabled;
					_basicUsageBarComboBoxViewModel.IsTextSearchEnabled = BasicComboBoxUsageOptions.IsTextSearchEnabled;
					_basicUsageBarComboBoxViewModel.IsUnmatchedTextAllowed = BasicComboBoxUsageOptions.IsUnmatchedTextAllowed;
					_basicUsageBarComboBoxViewModel.MenuResizeMode = BasicComboBoxUsageOptions.MenuResizeMode;
					_basicUsageBarComboBoxViewModel.PlaceholderText = BasicComboBoxUsageOptions.PlaceholderText;
				}
			};
			basicComboBoxUsageRibbonMvvm.DataContext = BasicComboBoxUsageRibbonViewModel;

			CategorizedComboBoxUsageRibbonViewModel = CreateCategorizedComboBoxUsageRibbonViewModel();
			categorizedComboBoxUsageRibbonMvvm.DataContext = CategorizedComboBoxUsageRibbonViewModel;

			FontComboBoxUsageRibbonViewModel = CreateFontComboBoxUsageRibbonViewModel();
			fontComboBoxUsageRibbonMvvm.DataContext = FontComboBoxUsageRibbonViewModel;

			EnumComboBoxUsageRibbonViewModel = CreateEnumComboBoxUsageRibbonViewModel();
			enumComboBoxUsageRibbonMvvm.DataContext = EnumComboBoxUsageRibbonViewModel;

			BasicTextBoxUsageRibbonViewModel = CreateBasicTextBoxUsageRibbonViewModel();
			basicTextBoxUsageRibbonMvvm.DataContext = BasicTextBoxUsageRibbonViewModel;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the ribbon view model for the "Basic combobox usage" sample.
		/// </summary>
		private RibbonViewModel CreateBasicComboBoxUsageRibbonViewModel() {
			return new RibbonViewModel() {
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = {
											(_basicUsageBarComboBoxViewModel = new BarComboBoxViewModel("SampleCombobox") {
												CanCategorize = BasicComboBoxUsageOptions.CanCategorizeOnMenu,
												IsEditable = BasicComboBoxUsageOptions.IsEditable,
												IsReadOnly = BasicComboBoxUsageOptions.IsReadOnly,
												IsTextCompletionEnabled = BasicComboBoxUsageOptions.IsTextCompletionEnabled,
												IsTextSearchEnabled = BasicComboBoxUsageOptions.IsTextSearchEnabled,
												IsUnmatchedTextAllowed = BasicComboBoxUsageOptions.IsUnmatchedTextAllowed,
												MenuResizeMode = BasicComboBoxUsageOptions.MenuResizeMode,
												PlaceholderText = BasicComboBoxUsageOptions.PlaceholderText,
												Command = ComboBoxGalleryCommand,
												UnmatchedTextCommand = ComboBoxUnmatchedTextCommand,
												RequestedWidth = 140,
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												Items = ComboBoxPersonItems,
											})
										}
									}
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Basic textbox usage" sample.
		/// </summary>
		private RibbonViewModel CreateBasicTextBoxUsageRibbonViewModel() {
			var textBoxViewModel = new BarTextBoxViewModel("Text") {
				Command = TextBoxCommitCommand,
				Description = "A textbox control that commits changed text on Enter or focus loss.",
				RequestedWidth = 120,
				Text = "A text value",
			};
			textBoxViewModel.CommandParameter = textBoxViewModel;

			return new RibbonViewModel() {
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = { textBoxViewModel }
									}
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Categorized combobox usage" sample.
		/// </summary>
		private RibbonViewModel CreateCategorizedComboBoxUsageRibbonViewModel() {
			return new RibbonViewModel() {
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = {
											// Categorized items in a single column
											SelectFirstComboBoxItem(new BarComboBoxViewModel("CategorizedSingleColumn", "Single-Column", ComboBoxGalleryCommand, ComboBoxPersonItems) {
												Description = "A combobox with items categorized and displayed in a single column.",
												IsEditable = true,
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												UnmatchedTextCommand = ComboBoxUnmatchedTextCommand,
											}),

											// Categorized items in multiple columns
											SelectFirstComboBoxItem(new BarComboBoxViewModel("CategorizedMultiColumn", "Multi-Column", ComboBoxGalleryCommand, ComboBoxNumberItems) {
												Description = "A combobox with items categorized and displayed using multiple columns.",
												IsEditable = true,
												ItemTemplate = ResolveResource<IDataTemplate>(LocalResourceKeys.NumberComboBoxGalleryItemTemplate),
												MinMenuColumnCount = 5,
												MaxMenuColumnCount = 5,
												UnmatchedTextCommand = ComboBoxUnmatchedNumberTextCommand,
											}),

											// Categorized/Filtered items with menu item appearance consistent with large menu items
											SelectFirstComboBoxItem(new BarComboBoxViewModel("MenuStyle", ComboBoxGalleryCommand, ComboBoxColorItems) {
												CanFilter = true,
												Description = "A combobox using a menu-like appearance for items, filtering, and an additional menu item below the list of combobox items.",
												IsEditable = true,
												ItemTemplate = ResolveResource<IDataTemplate>(LocalResourceKeys.LargeMenuComboBoxGalleryItemTemplate),
												UnmatchedTextCommand = ComboBoxUnmatchedTextCommand,
												BelowMenuItems = {
													new BarButtonViewModel("MoreColors", "More colors...")
												}
											}),
										}
									}
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Enum combobox usage" sample.
		/// </summary>
		private RibbonViewModel CreateEnumComboBoxUsageRibbonViewModel() {
			return new RibbonViewModel() {
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = {
											SelectFirstComboBoxItem(new BarComboBoxViewModel("EnumValue", ComboBoxGalleryCommand, ComboBoxEnumItems) {
												Description = "A combobox with items automatically generated from the fields of an Enum type.",
												IsUnmatchedTextAllowed = false,
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												RequestedWidth = 120,
											}),
										}
									}
								},
							},
						}
					},
				}
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Font combobox usage" sample.
		/// </summary>
		private RibbonViewModel CreateFontComboBoxUsageRibbonViewModel() {
			return new RibbonViewModel() {
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {
							new RibbonGroupViewModel("SampleGroup") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysMedium,
										Items = {
											// Font family
											SelectFirstComboBoxItem(new BarComboBoxViewModel("FontFamily", ComboBoxGalleryCommand, ComboBoxFontFamilyItems) {
												Description = "A combobox with system fonts and a category for recently-used fonts.",
												IsEditable = true,
												IsPreviewEnabledWhenPopupClosed = true,
												IsUnmatchedTextAllowed = false,
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												MenuResizeMode = ControlResizeMode.Vertical,
												RequestedWidth = 140,
											}),

											// Font size
											SelectFirstComboBoxItem(new BarComboBoxViewModel("FontSize", ComboBoxGalleryCommand, ComboBoxFontSizeItems) {
												Description = "A combobox with common font sizes.",
												IsEditable = true,
												IsTextCompletionEnabled = false,
												ItemTemplateSelector = new BarGalleryItemTemplateSelector(),
												MenuResizeMode = ControlResizeMode.Vertical,
												RequestedWidth = 60,
												UnmatchedTextCommand = ComboBoxUnmatchedNumberTextCommand,
											}),
										}
									}
								},
							},
						}
					},
				}
			};
		}

		private T? ResolveResource<T>(string key) {
			if (this.TryGetResource(key, out var resource) && (resource is T typedResource))
				return typedResource;
			return default;
		}

		private BarComboBoxViewModel SelectFirstComboBoxItem(BarComboBoxViewModel barComboBoxViewModel) {
			barComboBoxViewModel.SelectedItem = barComboBoxViewModel.Items?.OfType<IBarGalleryItemViewModel>().FirstOrDefault();
			return barComboBoxViewModel;
		}

		private void SetPreview(string? value)
			=> basicUsagePreviewLabel.Text = (string.IsNullOrWhiteSpace(value) ? "<NONE>" : value);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The options view model for the "Basic combobox usage" sample.
		/// </summary>
		public ComboBoxOptionsViewModel BasicComboBoxUsageOptions { get; }
			= new ComboBoxOptionsViewModel() {
				CanCategorizeOnMenu = false,
			};

		/// <summary>
		/// The ribbon view model for the "Basic combobox usage" sample.
		/// </summary>
		public RibbonViewModel BasicComboBoxUsageRibbonViewModel { get; }

		/// <summary>
		/// The ribbon view model for the "Basic textbox usage" sample.
		/// </summary>
		public RibbonViewModel BasicTextBoxUsageRibbonViewModel { get; }

		/// <summary>
		/// The ribbon view model for the "Categorized combobox usage" sample.
		/// </summary>
		public RibbonViewModel CategorizedComboBoxUsageRibbonViewModel { get; }

		/// <summary>
		/// The items to be displayed in combobox for selecting colors.
		/// </summary>
		public IEnumerable ComboBoxColorItems {
			get {
				if (_comboBoxColorItems is null) {
					var primaryCategory = "Primary Colors";
					var secondaryCategory = "Secondary Colors";

					// Wrap the items in an ICollectionView to support categorization
					_comboBoxColorItems = BarGalleryViewModel.CreateCollectionView(new [] {
						new TextBarGalleryItemViewModel("Red", primaryCategory) { Icon = ResolveResource<IImage>(LocalResourceKeys.RedSwatch) },
						new TextBarGalleryItemViewModel("Yellow", primaryCategory) { Icon = ResolveResource<IImage>(LocalResourceKeys.YellowSwatch) },
						new TextBarGalleryItemViewModel("Blue", primaryCategory) { Icon = ResolveResource<IImage>(LocalResourceKeys.BlueSwatch) },

						new TextBarGalleryItemViewModel("Orange", secondaryCategory) { Icon = ResolveResource<IImage>(LocalResourceKeys.OrangeSwatch) },
						new TextBarGalleryItemViewModel("Green", secondaryCategory) { Icon = ResolveResource<IImage>(LocalResourceKeys.GreenSwatch) },
						new TextBarGalleryItemViewModel("Purple", secondaryCategory) { Icon = ResolveResource<IImage>(LocalResourceKeys.PurpleSwatch) },
					}, categorize: true);
				}

				return _comboBoxColorItems;
			}
		}

		/// <summary>
		/// The items to be displayed in combobox based on an enum.
		/// </summary>
		public IEnumerable ComboBoxEnumItems
			=> _comboBoxEnumItems ??= BarGalleryViewModel.CreateCollectionView(
					EnumBarGalleryItemViewModel<SampleEnum>.CreateCollection().Select(x => {
						// Apply a default category
						if (x.Category is null)
							x.Category = "Uncategorized";
						return x;
					}),
					categorize: true);

		/// <summary>
		/// The items to be displayed in combobox for selecting font families.
		/// </summary>
		public IEnumerable ComboBoxFontFamilyItems {
			get {
				if (_comboBoxFontFamilyItems is null) {
					const string RecentlyUsedCategory = "Recently-Used Fonts";

					// Create a list of recently-used fonts (this sample just uses the default font)
					var recentlyUsedFonts = new List<FontFamilyBarGalleryItemViewModel>() {
						new(FontSettings.DefaultFontFamilyName, RecentlyUsedCategory)
					};

					// Create a list of the view models for the available fonts
					var defaultFontCollection = FontFamilyBarGalleryItemViewModel.CreateDefaultCollection();

					// Combine the recently-used fonts with the default fonts and
					// wrap in an ICollectionView to support categorization
					_comboBoxFontFamilyItems = BarGalleryViewModel.CreateCollectionView(
						recentlyUsedFonts.Concat(defaultFontCollection),
						categorize: true
					);
				}

				return _comboBoxFontFamilyItems;
			}
		}

		/// <summary>
		/// The items to be displayed in combobox for selecting font sizes.
		/// </summary>
		public IEnumerable ComboBoxFontSizeItems
			=> _comboBoxFontSizeItems ??= FontSizeBarGalleryItemViewModel.CreateDefaultCollection();

		/// <summary>
		/// The command for a gallery item selection from a combobox.
		/// </summary>
		public ICommand ComboBoxGalleryCommand
			=> _comboBoxGalleryCommand ??= new PreviewableDelegateCommand<IBarGalleryItemViewModel>(
				executeAction: param => ApplicationViewModel.Instance.MessageService?.ShowMessage($"The value '{param?.Label}' was matched from the gallery.", "Value Committed", NotificationType.Success),
				canExecuteFunc: _ => true,

				// The items of BarComboBox support previewing the current item just like other gallery-based controls
				previewAction: param => SetPreview(param?.Label ?? "<Unknown>"),
				cancelPreviewAction: _ => SetPreview(null)
			);

		/// <summary>
		/// The items to be displayed in combobox for selecting numbers.
		/// </summary>
		public IEnumerable ComboBoxNumberItems {
			get {
				if (_comboBoxNumberItems is null) {
					var evenCategory = "Even Numbers";
					var oddCategory = "Odd Numbers";

					var items = new List<TextBarGalleryItemViewModel>();
					for (var i = 1; i <= 20; i++) {
						bool isEven = (i % 2 == 0);
						items.Add(new TextBarGalleryItemViewModel(i.ToString(), (isEven ? evenCategory : oddCategory)));
					}

					// Wrap the items in an ICollectionView to support categorization
					_comboBoxNumberItems = BarGalleryViewModel.CreateCollectionView(items, categorize: true);
				}

				return _comboBoxNumberItems;
			}
		}

		/// <summary>
		/// The items to be displayed in combobox for selecting people.
		/// </summary>
		public IEnumerable ComboBoxPersonItems {
			get {
				if (_comboBoxPersonItems is null) {
					var items = new List<TextBarGalleryItemViewModel>();

					foreach (var person in People.All.OrderBy(x => x.FullName))
						items.Add(new TextBarGalleryItemViewModel(person.FullName, person.Position));

					// Wrap the items in an ICollectionView to support categorization
					_comboBoxPersonItems = BarGalleryViewModel.CreateCollectionView(items, categorize: true);
				}

				return _comboBoxPersonItems;
			}
		}

		/// <summary>
		/// The command that is executed when a value is entered into a combobox for selecting numbers that does not match one of the known gallery items.
		/// </summary>
		public ICommand ComboBoxUnmatchedNumberTextCommand
			=> _comboBoxUnmatchedNumberTextCommand ??= new DelegateCommand<string>(
				executeAction: param => {
					// No action necessary, but show a message to indicate that the value was accepted
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"The integer text value '{param}' was manually entered and accepted without a match in the gallery.", "Custom Number Text Value Committed", NotificationType.Success);
				},
				canExecuteFunc: param => {
					// The BarComboBox.UnmatchedTextCommand.CanExecute result will determine if the
					// typed text should be allowed... true to allow the value and false to reject it
					return int.TryParse(param, out var number) && number > 0;
				}
			);

		/// <summary>
		/// An optional command that is executed when a value is entered into a general combobox that does not match one of the known gallery items.
		/// </summary>
		public ICommand ComboBoxUnmatchedTextCommand
			=> _comboBoxUnmatchedTextCommand ??= new DelegateCommand<string>(
				executeAction: param => {
					// No action necessary, but show a message to indicate that the value was accepted
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"The text value '{param}' was manually entered and accepted without a match in the gallery.", "Custom Text Value Committed", NotificationType.Success);
				},
				canExecuteFunc: _ => {
					// The BarComboBox.UnmatchedTextCommand.CanExecute result will determine if the
					// typed text should be allowed... true to allow the value and false to reject it
					//
					// NOTE: If the BarComboBox.UnmatchedTextCommand is unassigned, all values are assumed to be allowed
					return true;
				}
			);

		/// <summary>
		/// The ribbon view model for the "Enum combobox usage" sample.
		/// </summary>
		public RibbonViewModel EnumComboBoxUsageRibbonViewModel { get; }

		/// <summary>
		/// The ribbon view model for the "Font combobox usage" sample.
		/// </summary>
		public RibbonViewModel FontComboBoxUsageRibbonViewModel { get; }

		/// <summary>
		/// The command to commit text from a textbox.
		/// </summary>
		public ICommand TextBoxCommitCommand
			=> _textBoxCommitCommand ??= new DelegateCommand<object>(
				executeAction: param => {
					// For the XAML sample the current text is passed as the CommandParameter
					// For the MVVM sample the source BarTextBoxViewModel is passed as the CommandParameter
					var textBoxValue = (param is BarTextBoxViewModel viewModel) ? viewModel.Text : param?.ToString();
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"The value '{textBoxValue}' was committed from the textbox.", "Value Committed", NotificationType.Success);
				},
				canExecuteFunc: param => true
			);

	}

}
