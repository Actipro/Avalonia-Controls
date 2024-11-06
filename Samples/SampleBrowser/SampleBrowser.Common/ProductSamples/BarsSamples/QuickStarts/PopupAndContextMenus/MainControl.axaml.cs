using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.ObjectModel;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.PopupAndContextMenus {

	public partial class MainControl : UserControl {

		private ObservableCollection<object>? _contextMenuItems;
		private ICollectionView? _pasteOptions;
		private DelegateCommand<object>? _popupMenuSampleCommand;

		#region Property Definitions

		public static readonly DirectProperty<MainControl, ObservableCollection<object>?> ContextMenuItemsProperty
			= AvaloniaProperty.RegisterDirect<MainControl, ObservableCollection<object>?>(nameof(ContextMenuItems), x => x.ContextMenuItems);

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// Configure TextBox Context Menu Items
			ContextMenuItems = CreateContextMenuItems();

			// Configure the Ribbon view model for the MVVM "Popup menu" sample
			BasicUsagePopupMenuRibbonViewModel = CreateBasicUsagePopupMenuRibbonViewModel();
			basicUsagePopupMenuRibbonMvvm.DataContext = BasicUsagePopupMenuRibbonViewModel;

			// Configure the Ribbon view model for the MVVM "Large size" sample
			LargeSizePopupMenuRibbonViewModel = CreateLargeSizePopupMenuRibbonViewModel();
			largeSizeRibbonMvvm.DataContext = LargeSizePopupMenuRibbonViewModel;

			// Synchronize the popup menu sample command with the checked state of the UI option indicating if items should be enabled
			basicUsagePopupMenuIsEnabledCheckBox.IsCheckedChanged += (_, _) => _popupMenuSampleCommand?.RaiseCanExecuteChanged();

			// Sychronize other popup menu UI options with the view models
			basicUsagePopupMenuStaysOpenOnClickCheckBox.IsCheckedChanged += (_, _) => UpdatePopupMenuViewModels();
			basicUsagePopupMenuShowIconsCheckBox.IsCheckedChanged += (_, _) => UpdatePopupMenuViewModels();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private ObservableCollection<object> CreateContextMenuItems() {
			var keymap = Application.Current?.PlatformSettings?.HotkeyConfiguration;
			var textBox = contextMenuTextBoxMvvm;
			return new ObservableCollection<object>() {
				new BarButtonViewModel("Undo") {
					Command = new DelegateCommand<TextBox>(p => p?.Undo(), p => p?.CanUndo == true),
					CommandParameter = textBox,
					InputGesture = keymap?.Undo.FirstOrDefault(),
					SmallIcon = ImageLoader.GetIcon("Undo16.png")
				},
				new BarButtonViewModel("Redo") {
					Command = new DelegateCommand<TextBox>(p => p?.Redo(), p => p?.CanRedo == true),
					CommandParameter = textBox,
					InputGesture = keymap?.Redo.FirstOrDefault(),
					SmallIcon = ImageLoader.GetIcon("Redo16.png")
				},
				new BarSeparatorViewModel(),
				new BarButtonViewModel("Cut") {
					Command = new DelegateCommand<TextBox>(p => p?.Cut(), p => p?.CanCut == true),
					CommandParameter = textBox,
					InputGesture = keymap?.Cut.FirstOrDefault(),
					SmallIcon = ImageLoader.GetIcon("Cut16.png"),
				},
				new BarButtonViewModel("Copy") {
					Command = new DelegateCommand<TextBox>(p => p?.Copy(), p => p?.CanCopy == true),
					CommandParameter = textBox,
					InputGesture = keymap?.Copy.FirstOrDefault(),
					SmallIcon = ImageLoader.GetIcon("Copy16.png")
				},
				new BarButtonViewModel("Paste") {
					Command = new DelegateCommand<TextBox>(p => p?.Paste(), p => p?.CanPaste== true),
					CommandParameter = textBox,
					InputGesture = keymap?.Paste.FirstOrDefault(),
					SmallIcon = ImageLoader.GetIcon("Paste16.png")
				},
				new BarSeparatorViewModel(),
				new BarButtonViewModel("SelectAll") {
					Command = new DelegateCommand<TextBox>(p => p?.SelectAll()),
					CommandParameter = textBox,
					InputGesture = keymap?.SelectAll.FirstOrDefault(),
					SmallIcon = ImageLoader.GetIcon("SelectAll16.png")
				},
			};
		}

		/// <summary>
		/// Creates the ribbon view model for the "Large size" sample.
		/// </summary>
		private RibbonViewModel CreateLargeSizePopupMenuRibbonViewModel() {
			return new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {

							new RibbonGroupViewModel("SampleGroup") {
								CanAutoCollapse = false,
								Items = {

									new BarPopupButtonViewModel("PopupMenu") {
										LargeIcon = ImageLoader.GetIcon("Height32.png"),
										SmallIcon = ImageLoader.GetIcon("Height16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {

											new BarHeadingViewModel("Large Items"),
											new BarPopupButtonViewModel("New") {
												Description = "Create a new document.",
												LargeIcon = GetViewModelLargeIcon("New"),
												UseLargeMenuItem = true,
												MenuItems = {
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 1)"},
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 2)"},
												}
											},
											new BarSplitButtonViewModel("Open") {
												Description = "Open an existing document.",
												LargeIcon = GetViewModelLargeIcon("Open"),
												UseLargeMenuItem = true,
												MenuItems = {
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 1)"},
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 2)"},
												}
											},
											new BarButtonViewModel("Save") {
												Description = "Save the current document.",
												LargeIcon = GetViewModelLargeIcon("Save"),
												UseLargeMenuItem = true,
											},

											new BarSeparatorViewModel(),

											new BarHeadingViewModel("Large Items (No Description)"),
											new BarPopupButtonViewModel("NewNoDesc") {
												Label = "New",
												LargeIcon = GetViewModelLargeIcon("New"),
												UseLargeMenuItem = true,
												MenuItems = {
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 1)"},
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 2)"},
												}
											},
											new BarSplitButtonViewModel("OpenNoDesc") {
												Label = "Open",
												LargeIcon = GetViewModelLargeIcon("Open"),
												UseLargeMenuItem = true,
												MenuItems = {
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 1)"},
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 2)"},
												}
											},
											new BarButtonViewModel("SaveNoDesc") {
												Label = "Save",
												LargeIcon = GetViewModelLargeIcon("Save"),
												UseLargeMenuItem = true,
											},

											new BarSeparatorViewModel(),

											new BarHeadingViewModel("Checkable (BarToggleButtonViewModel)"),
											new BarToggleButtonViewModel("DefaultCheck") {
												Description = "A checkmark glyph is automatically used as image for checkable menu items.",
												IsChecked = true,
												UseLargeMenuItem = true,
											},
											new BarToggleButtonViewModel("ExplicitImage") {
												Description = "Explicit images also supported with a checked state.",
												IsChecked = true,
												LargeIcon = GetViewModelLargeIcon("QuickStart"),
												UseLargeMenuItem = true,
											},

											new BarSeparatorViewModel(),

											new BarHeadingViewModel("Checkable (BarSplitToggleButtonViewModel)"),
											new BarSplitToggleButtonViewModel("DefaultCheckSplit") {
												Label = "Default Check",
												Description = "A checkmark glyph is automatically used as image for checkable menu items.",
												IsChecked = true,
												UseLargeMenuItem = true,
												MenuItems = {
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 1)"},
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 2)"},
												}
											},
											new BarSplitToggleButtonViewModel("ExplicitImageSplit") {
												Label = "Explicit Image",
												Description = "Explicit images also supported with a checked state.",
												IsChecked = true,
												LargeIcon = GetViewModelLargeIcon("QuickStart"),
												UseLargeMenuItem = true,
												MenuItems = {
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 1)"},
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 2)"},
												}
											},
											new BarSeparatorViewModel(),

											new BarHeadingViewModel("Small Items in Same Menu"),
											new BarButtonViewModel("Undo") {
												SmallIcon = GetViewModelSmallIcon("Undo"),
											},
											new BarButtonViewModel("Redo") {
												SmallIcon = GetViewModelSmallIcon("Redo"),
											},

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
		/// Creates the ribbon view model for the "Basic Usage" sample.
		/// </summary>
		private RibbonViewModel CreateBasicUsagePopupMenuRibbonViewModel() {
			return new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {

							new RibbonGroupViewModel("SampleGroup") {
								CanAutoCollapse = false,
								Items = {

									new BarPopupButtonViewModel("PopupMenu") {
										Description = "Common menu controls shown in states and configurations based on the sample options.",
										LargeIcon = ImageLoader.GetIcon("Menu32.png"),
										SmallIcon = ImageLoader.GetIcon("Menu16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										MenuItems = {

											new BarHeadingViewModel("Standard Button Samples"),
											new BarButtonViewModel("New") {
												Label = "BarButtonViewModel",
												Command = PopupMenuSampleCommand,
												SmallIcon = GetViewModelSmallIcon("New"),
											},
											new BarToggleButtonViewModel("QuickStart") {
												Label = "BarToggleButtonViewModel (Checkable)",
												Command = PopupMenuSampleCommand,
												IsChecked = true,
												SmallIcon = GetViewModelSmallIcon("QuickStart"),
											},
											new BarPopupButtonViewModel("Open") {
												Label = "BarPopupButtonViewModel (With Children)",
												Command = PopupMenuSampleCommand,
												SmallIcon = GetViewModelSmallIcon("Open"),
												MenuItems = {
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 1)"},
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 2)"},
												}
											},

											new BarSeparatorViewModel(),

											new BarHeadingViewModel("Split Button Samples"),
											new BarSplitButtonViewModel("Save") {
												Label = "BarSplitButtonViewModel",
												Command = PopupMenuSampleCommand,
												SmallIcon = GetViewModelSmallIcon("Save"),
												MenuItems = {
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 1)"},
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 2)"},
												}
											},
											new BarSplitToggleButtonViewModel("Print") {
												Label = "BarSplitToggleButtonViewModel (Checkable)",
												Command = PopupMenuSampleCommand,
												IsChecked = true,
												SmallIcon = GetViewModelSmallIcon("Print"),
												MenuItems = {
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 1)"},
													new BarButtonViewModel() { Label = "BarButtonViewModel (Child 2)"},
												},
											},

										}
									},

								},
							},

						}
					},
				}
			};
		}

		private IImage? GetViewModelLargeIcon(string key)
			=> ImageLoader.GetIcon($"{key}32.png");

		private IImage? GetViewModelSmallIcon(string key)
			=> ImageLoader.GetIcon($"{key}16.png");

		private DelegateCommand<object> PopupMenuSampleCommand {
			// The enabled state of a view model is dictated by the associated command, so the CanExecute result
			// of the command for this sample is based on the checked state of the corresponding option in the UI
			get => _popupMenuSampleCommand ??= new DelegateCommand<object>(
				executeAction:  _ => { /* no op */ },
				canExecuteFunc: _ => basicUsagePopupMenuIsEnabledCheckBox.IsChecked == true);
		}

		private void UpdatePopupMenuViewModels() {
			bool staysOpenOnClick = (basicUsagePopupMenuStaysOpenOnClickCheckBox.IsChecked == true);
			bool showIcons = (basicUsagePopupMenuShowIconsCheckBox.IsChecked == true);

			var samplePopupButton = BasicUsagePopupMenuRibbonViewModel.Tabs[0].Groups[0].Items[0] as BarPopupButtonViewModel;
			if (samplePopupButton is not null) {
				foreach (var item in samplePopupButton.MenuItems) {
					switch (item) {
						case BarButtonViewModel button:
							button.StaysOpenOnClick = staysOpenOnClick;
							button.SmallIcon = (showIcons ? GetViewModelSmallIcon(button.Key!) : null);
							break;
						case BarSplitButtonViewModel splitButton:
							splitButton.StaysOpenOnClick = staysOpenOnClick;
							splitButton.SmallIcon = (showIcons ? GetViewModelSmallIcon(splitButton.Key!) : null);
							UpdateChildMenuItems(splitButton.MenuItems);
							break;
						case BarPopupButtonViewModel popupButton:
							popupButton.SmallIcon = (showIcons ? GetViewModelSmallIcon(popupButton.Key!) : null);
							UpdateChildMenuItems(popupButton.MenuItems);
							break;
					}
				}
			}

			void UpdateChildMenuItems(ObservableCollection<object> menuItems) {
				if (menuItems is not null) {
					foreach (var item in menuItems.OfType<BarButtonViewModel>())
						item.StaysOpenOnClick = staysOpenOnClick;
				}
			}

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The ribbon view model for the "Popup menu" sample.
		/// </summary>
		public RibbonViewModel BasicUsagePopupMenuRibbonViewModel { get; }

		/// <summary>
		/// The collection of menu item view models to be displayed in the context menu.
		/// </summary>
		public ObservableCollection<object>? ContextMenuItems {
			get => _contextMenuItems;
			private set => SetAndRaise(ContextMenuItemsProperty, ref _contextMenuItems, value);
		}

		/// <summary>
		/// The ribbon view model for the "Large size" sample.
		/// </summary>
		public RibbonViewModel LargeSizePopupMenuRibbonViewModel { get; }

		/// <summary>
		/// The collection of view models for the available paste options used by the "Advanced paste options" sample.
		/// </summary>
		public ICollectionView PasteOptions
			=> _pasteOptions ??= PasteOptionGalleryItem.CreateDefaultCollectionView();
		

		/// <summary>
		/// The collection of view models for the available tag colors used by the "View options with color tagging" sample.
		/// </summary>
		public ObservableCollection<TagColorGalleryItem> TagColors { get; }
			= new(TagColorGalleryItem.CreateDefaultCollection());

	}

}
