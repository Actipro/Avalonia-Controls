using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.DynamicPopupMenus {

	public partial class MainControl : UserControl {

		private readonly Random _random = new();
		private List<RecentDocumentItem>? _recentDocuments;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			// Configure the MVVM-based sample.
			RibbonViewModel = CreateRibbonViewModel();

			InitializeComponent();

			mvvmRibbon.MenuOpening += OnRibbonMenuOpeningMvvm;
			xamlRibbon.MenuOpening += OnRibbonMenuOpeningXaml;

			mvvmTextBox.ContextFlyout = CreateTextBoxContextFlyout();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a view model that is used as a placeholder child item in the MVVM sample.
		/// </summary>
		/// <returns>A new <see cref="BarButtonViewModel"/>.</returns>
		private static BarButtonViewModel CreateChildPlaceholderMvvm() {
			// A BarPopupButtonViewModel or BarSplitButtonViewModel will always raise the MenuOpening
			// event even if they do not have children, but when displayed on a menu as a
			// BarMenuItem (which derives from MenuItem) it must define at least one child to be recognized
			// as a MenuItem with a sub-menu. Since BarPopupButtonViewModel and BarSplitButtonViewModel
			// can be cloned to an overflow menu where they are displayed as BarMenuItem, it is still
			// important to add a placeholder child item.
			return new BarButtonViewModel(BarControlKeys.PlaceholderChild);
		}

		/// <summary>
		/// Creates an item that is used as a placeholder child item in the XAML sample.
		/// </summary>
		private BarMenuItem CreateChildPlaceholderXaml() {
			// A BarPopupButton or BarSplitButton will always raise the MenuOpening
			// event even if they do not have children, but a BarMenuItem (which derives from MenuItem)
			// must define at least one child to be recognized as a MenuItem with a sub-menu.
			// Since BarPopupButton and BarSplitButton can be cloned to an overflow menu where they are
			// displayed as BarMenuItem, it is still important to add a placeholder child item.
			return new BarMenuItem() { Key = BarControlKeys.PlaceholderChild };
		}

		/// <summary>
		/// Creates the view model for the MVVM sample.
		/// </summary>
		private RibbonViewModel CreateRibbonViewModel() {
			return new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				IsCollapsible = false,
				LayoutMode = RibbonLayoutMode.Simplified,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Hidden,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {

							new RibbonGroupViewModel("Sample") {
								CanAutoCollapse = false,
								Items = {
									new BarSplitButtonViewModel(BarControlKeys.OpenDocument) {
										Description = "The recent documents in this popup menu are dynamically generated.",
										Label = "Open Document",
										LargeIcon = ImageLoader.GetIcon("Open32.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										SmallIcon = ImageLoader.GetIcon("Open16.png"),
										MenuItems = { CreateChildPlaceholderMvvm() }
									},
									new BarPopupButtonViewModel(BarControlKeys.DynamicPopupButton) {
										Description = "The menu of this popup button is dynamically generated when the popup is opened.",
										Label = "Popup Button with Dynamic Menu",
										LargeIcon = ImageLoader.GetIcon("QuickStart32.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
										SmallIcon = ImageLoader.GetIcon("QuickStart16.png"),
										MenuItems = { CreateChildPlaceholderMvvm() }
									},
								},
							},

							new RibbonGroupViewModel("SimplifiedLayoutOverflow") {
								CanAutoCollapse = false,
								Items = {
									new BarPopupButtonViewModel(BarControlKeys.DynamicPopupOverflowButton) {
										Label = "Popup Button Overflowed in Simplified Layout",
										LargeIcon = ImageLoader.GetIcon("QuickStartGreen32.png"),
										ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
										SmallIcon = ImageLoader.GetIcon("QuickStartGreen16.png"),
										MenuItems = { CreateChildPlaceholderMvvm() }
									},
								}
							}

						}
					},

				}
			};
		}

		/// <summary>
		/// Create the context menu flyout that will be assigned to the <c>TextBox</c> control used by this sample.
		/// </summary>
		private MenuFlyout CreateTextBoxContextFlyout() {
			// Configure the menu
			var menu = new BarMenuFlyout() {

				// Make sure the MenuFlyout has the same ItemContainerTemplateSelector as the Ribbon so
				// view models are properly templated
				ItemContainerTemplateSelector = RibbonViewModel.ItemContainerTemplateSelector,

				Items = {
					new BarSplitButtonViewModel(BarControlKeys.OpenDocument) {
						Description = "The recent documents in this popup menu are dynamically generated.",
						Label = "Open Document",
						LargeIcon = ImageLoader.GetIcon("Open32.png"),
						SmallIcon = ImageLoader.GetIcon("Open16.png"),
						MenuItems = { CreateChildPlaceholderMvvm() }
					},
					new BarPopupButtonViewModel(BarControlKeys.DynamicPopupButton) {
						Description = "The menu of this popup button is dynamically generated when the popup is opened.",
						Label = "Popup Button with Dynamic Menu",
						LargeIcon = ImageLoader.GetIcon("QuickStart32.png"),
						SmallIcon = ImageLoader.GetIcon("QuickStart16.png"),
						MenuItems = { CreateChildPlaceholderMvvm() }
					},
				}
			};

			// The context menu flyout must have a RootBarControl set so the MenuOpening event can
			// be raised that allows for customizing the popup menu
			BarControlService.SetRootBarControl(menu, mvvmRibbon);

			return menu;
		}

		/// <summary>
		/// Initializes the items for a dynamically generated menu in the XAML sample.
		/// </summary>
		/// <param name="menu">The menu whose items are to be generated.</param>
		/// <param name="exampleKind">Indicates if the menu items are for an MVVM or XAML example.</param>
		private void InitializeDynamicMenuItems(ItemsControl? menu, CodeExampleKind exampleKind) {
			if (menu is null)
				return;

			// Use the menu's key to determine which items will be added
			var key = BarControlService.GetKey(menu);
			switch (key) {
				case BarControlKeys.DynamicMenuItem:
				case BarControlKeys.DynamicPopupButton:
				case BarControlKeys.DynamicPopupOverflowButton:
					// Populate with sample menu items that clearly show they are dynamically generated
					var dynamicMenuItems = ResolveMenuItemsList(menu);
					if (dynamicMenuItems != null) {
						dynamicMenuItems.Clear();
						switch (exampleKind) {
							case CodeExampleKind.Mvvm:
								PopulateSampleDynamicMenuItemsMvvm(key, dynamicMenuItems);
								break;
							case CodeExampleKind.Xaml:
								PopulateSampleDynamicMenuItemsXaml(key, dynamicMenuItems);
								break;
						}
					}
					break;

				case BarControlKeys.OpenDocument:
					// Populate recently opened documents for an Open command
					var openDocumentItems = ResolveMenuItemsList(menu);
					if (openDocumentItems != null) {
						openDocumentItems.Clear();
						switch (exampleKind) {
							case CodeExampleKind.Mvvm:
								PopulateOpenDocumentMenuItemsMvvm(openDocumentItems);
								break;
							case CodeExampleKind.Xaml:
								PopulateOpenDocumentMenuItemsXaml(openDocumentItems);
								break;
						}
					}
					break;
			}
		}

		private void OnRibbonMenuOpeningMvvm(object? sender, BarMenuEventArgs e) {
			// This demo focuses only on command popup menus
			if (e.Kind == BarMenuKind.PopupButtonMenu
				|| e.Kind == BarMenuKind.MenuItemSubmenu) {

				InitializeDynamicMenuItems(e.Menu as ItemsControl, CodeExampleKind.Mvvm);
			}
		}

		private void OnRibbonMenuOpeningXaml(object? sender, BarMenuEventArgs e) {
			// This demo focuses only on command popup menus
			if (e.Kind == BarMenuKind.PopupButtonMenu
				|| e.Kind == BarMenuKind.MenuItemSubmenu) {

				InitializeDynamicMenuItems(e.Menu as ItemsControl, CodeExampleKind.Xaml);
			}
		}

		/// <summary>
		/// Populates the list of menu items for the open document popup menu in the MVVM-based sample.
		/// </summary>
		/// <param name="menuItems">The list of menu items to populate.</param>
		private void PopulateOpenDocumentMenuItemsMvvm(IList menuItems) {
			// Simulate a list of recent documents like those used by the RecentDocumentControl
			_recentDocuments ??= new(RecentDocumentHelper.CreateDefaultItems());

			// Dynamically add a menu item for each recent document
			var pinnedMenuItems = new List<object>();
			var unpinnedMenuItems = new List<object>();
			foreach (var recentDocumentItem in _recentDocuments.OrderBy(x => x.IsPinned ? 0 : 1).ThenByDescending(x => x.LastOpenedDateTime).Take(10)) {
				var targetList = recentDocumentItem.IsPinned ? pinnedMenuItems : unpinnedMenuItems;
				targetList.Add(new BarButtonViewModel(recentDocumentItem.DocumentName) {
					CanCloneToRibbonQuickAccessToolBar = false,
					LargeIcon = recentDocumentItem.LargeIcon,
					SmallIcon = recentDocumentItem.SmallIcon,
					Description = recentDocumentItem.Location?.LocalPath,
				});
			}
			bool includeHeaders = (pinnedMenuItems.Any() && unpinnedMenuItems.Any());
			if (pinnedMenuItems.Count > 0) {
				if (includeHeaders)
					menuItems.Add(new BarHeadingViewModel("Pinned Documents"));
				foreach (var menuItem in pinnedMenuItems)
					menuItems.Add(menuItem);
			}
			if (unpinnedMenuItems.Count > 0) {
				if (includeHeaders)
					menuItems.Add(new BarHeadingViewModel("Unpinned Documents"));
				foreach (var menuItem in unpinnedMenuItems)
					menuItems.Add(menuItem);
			}

			if (menuItems.Count == 0) {
				menuItems.Add(new BarButtonViewModel("No recent documents") {
					CanCloneToRibbonQuickAccessToolBar = false,
					// Disable the menu item with a command that cannot execute
					Command = new DelegateCommand<object>(executeAction: _ => { /* no-op */ }, canExecuteFunc: _ => false)
				});
			}

			menuItems.Add(new BarSeparatorViewModel());
			menuItems.Add(new BarButtonViewModel(key: "Browse", label: "Browse...") {
				CanCloneToRibbonQuickAccessToolBar = false,
				LargeIcon = ImageLoader.GetIcon("Open32.png"),
				SmallIcon = ImageLoader.GetIcon("Open16.png"),
			});
		}

		/// <summary>
		/// Populates the list of menu items for the open document popup menu in the XAML-based sample.
		/// </summary>
		/// <param name="menuItems">The list of menu items to populate.</param>
		private void PopulateOpenDocumentMenuItemsXaml(IList menuItems) {
			// Simulate a list of recent documents like those used by the RecentDocumentControl
			_recentDocuments ??= new(RecentDocumentHelper.CreateDefaultItems());

			// Dynamically add a menu item for each recent document
			var pinnedMenuItems = new List<object>();
			var unpinnedMenuItems = new List<object>();
			foreach (var recentDocumentItem in _recentDocuments.OrderBy(x => x.IsPinned ? 0 : 1).ThenByDescending(x => x.LastOpenedDateTime).Take(10)) {
				var targetList = recentDocumentItem.IsPinned ? pinnedMenuItems : unpinnedMenuItems;
				targetList.Add(new BarMenuItem(recentDocumentItem.DocumentName) {
					CanCloneToRibbonQuickAccessToolBar = false,
					LargeIcon = recentDocumentItem.LargeIcon,
					SmallIcon = recentDocumentItem.SmallIcon,
					ScreenTipHeader = recentDocumentItem.Location?.LocalPath,
				});
			}
			bool includeHeaders = (pinnedMenuItems.Any() && unpinnedMenuItems.Any());
			if (pinnedMenuItems.Count > 0) {
				if (includeHeaders)
					menuItems.Add(new BarMenuHeading("Pinned Documents"));
				foreach (var menuItem in pinnedMenuItems)
					menuItems.Add(menuItem);
			}
			if (unpinnedMenuItems.Count > 0) {
				if (includeHeaders)
					menuItems.Add(new BarMenuHeading("Unpinned Documents"));
				foreach (var menuItem in unpinnedMenuItems)
					menuItems.Add(menuItem);
			}

			if (menuItems.Count == 0)
				menuItems.Add(new BarMenuItem("No recent documents") { CanCloneToRibbonQuickAccessToolBar = false, IsEnabled = false });

			menuItems.Add(new BarMenuSeparator());
			menuItems.Add(new BarMenuItem("Browse...") {
				CanCloneToRibbonQuickAccessToolBar = false,
				LargeIcon = ImageLoader.GetIcon("Open32.png"),
				SmallIcon = ImageLoader.GetIcon("Open16.png"),
			});
		}

		/// <summary>
		/// Populates a list of dynamically generated menu items for the MVVM-based generic sample controls.
		/// </summary>
		/// <param name="key">The key of the control whose menu is being generated.</param>
		/// <param name="menuItems">The list of menu items to populate.</param>
		private void PopulateSampleDynamicMenuItemsMvvm(string key, IList menuItems) {
			// Dynamically generate new menu items
			menuItems.Add(new BarHeadingViewModel(key));
			menuItems.Add(new BarSeparatorViewModel());
			var itemCount = _random.Next(2, 9); // Randomize the number of menu items
			for (int index = 0; index < itemCount; index++) {
				var menuItem = new BarButtonViewModel(key: $"DynamicItem{(index + 1)}", label: $"Dynamic Item #{(index + 1)}") {
					CanCloneToRibbonQuickAccessToolBar = false,
				};
				menuItems.Add(menuItem);
			}
			if (key != BarControlKeys.DynamicMenuItem) {
				// Add a menu item whose sub-menu is dynamically generated
				menuItems.Add(new BarSeparatorViewModel());
				menuItems.Add(new BarPopupButtonViewModel(key: BarControlKeys.DynamicMenuItem, label: "Dynamic Sub-Menu") {
					CanCloneToRibbonQuickAccessToolBar = false,
					MenuItems = { CreateChildPlaceholderMvvm() }
				});
			}
			menuItems.Add(new BarSeparatorViewModel());
			menuItems.Add(new BarHeadingViewModel($"Menu Created @ {DateTime.Now.ToLongTimeString()}"));
		}

		/// <summary>
		/// Populates a list of dynamically generated menu items for the XAML-based generic sample controls.
		/// </summary>
		/// <param name="key">The key of the control whose menu is being generated.</param>
		/// <param name="menuItems">The list of menu items to populate.</param>
		private void PopulateSampleDynamicMenuItemsXaml(string key, IList menuItems) {
			// Dynamically generate new menu items
			menuItems.Add(new BarMenuHeading(key));
			menuItems.Add(new BarMenuSeparator());
			var itemCount = _random.Next(2, 9); // Randomize the number of menu items
			for (int index = 0; index < itemCount; index++) {
				var menuItem = new BarMenuItem(label: $"Dynamic Item #{(index + 1)}") {
					CanCloneToRibbonQuickAccessToolBar = false,
					Key = $"DynamicItem{(index + 1)}"
				};
				menuItems.Add(menuItem);
			}
			if (key != BarControlKeys.DynamicMenuItem) {
				// Add a menu item whose sub-menu is dynamically generated
				menuItems.Add(new BarMenuSeparator());
				menuItems.Add(new BarMenuItem() {
					CanCloneToRibbonQuickAccessToolBar = false,
					Key = BarControlKeys.DynamicMenuItem,
					Label = "Dynamic Sub-Menu",
					Items = { CreateChildPlaceholderXaml() }
				});
			}
			menuItems.Add(new BarMenuSeparator());
			menuItems.Add(new BarMenuHeading($"Menu Created @ {DateTime.Now.ToLongTimeString()}"));
		}

		/// <summary>
		/// Resolves the list of menu items associated with a menu.
		/// </summary>
		/// <param name="menu">The menu to examine.</param>
		/// <returns>An <see cref="IList"/> for the menu items associated with a menu.</returns>
		private static IList? ResolveMenuItemsList(ItemsControl menu) {
			// Always work with an ItemsSource when available; otherwise, fall back to the Items collection
			return (menu?.ItemsSource as IList) ?? menu?.Items;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The ribbon view model for the MVVM sample.
		/// </summary>
		public RibbonViewModel RibbonViewModel { get; }

	}

}
