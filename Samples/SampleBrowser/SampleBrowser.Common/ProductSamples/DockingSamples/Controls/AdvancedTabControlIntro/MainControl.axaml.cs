using ActiproSoftware.UI.Avalonia.Controls.Docking;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Input;
using System;
using System.Linq;

namespace ActiproSoftware.ProductSamples.DockingSamples.Controls.AdvancedTabControlIntro {

	public partial class MainControl : UserControl {

		private int _newDocumentIndex = 3; // 3 documents are defined in XAML, so next zero-based index is 3

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			// Generate platform-specific keyboard shortcuts for activating tabs by position
			var activateNormalTabKeyModifiers = AdvancedTabControl.ActivateNormalTabKeyModifiersProperty.GetDefaultValue(typeof(AdvancedTabControl));
			ActivateNormalTab1KeyGestureText = new KeyGesture(Key.D1, activateNormalTabKeyModifiers).ToPlatformString();
			ActivateNormalTab2KeyGestureText = new KeyGesture(Key.D2, activateNormalTabKeyModifiers).ToPlatformString();

			var activatePinnedTabKeyModifiers = AdvancedTabControl.ActivatePinnedTabKeyModifiersProperty.GetDefaultValue(typeof(AdvancedTabControl));
			ActivatePinnedTab1KeyGestureText = new KeyGesture(Key.D1, activatePinnedTabKeyModifiers).ToPlatformString();
			ActivatePinnedTab2KeyGestureText = new KeyGesture(Key.D2, activatePinnedTabKeyModifiers).ToPlatformString();

			var activatePreviewTabKeyModifiers = AdvancedTabControl.ActivatePreviewTabKeyModifiersProperty.GetDefaultValue(typeof(AdvancedTabControl));
			ActivatePreviewTab1KeyGestureText = new KeyGesture(Key.D1, activatePreviewTabKeyModifiers).ToPlatformString();
			ActivatePreviewTab2KeyGestureText = new KeyGesture(Key.D2, activatePreviewTabKeyModifiers).ToPlatformString();

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnCustomMenuUsageMenuOpening(object? sender, AdvancedTabControlMenuEventArgs e) {
			const string CustomMenuTag = "CustomMenu";

			if (e.TabItem is null) {
				//
				// Overflow Menu
				//

				if (e.Menu is not null) {
					bool addSeparator = true;

					foreach (var menuItem in e.Menu.Items.OfType<MenuItem>()) {
						if (menuItem.CommandParameter is AdvancedTabItem tabItem) {
							menuItem.Header = tabItem.Header?.ToString() + (tabItem.IsSelected ? " (Selected)" : null);
						}
					}

					// Look for previous custom menu item already in the menu
					var customMenuItem = e.Menu.Items.OfType<MenuItem>().FirstOrDefault(x => x.Tag?.ToString() == CustomMenuTag);
					if (customMenuItem is not null) {
						// Separator will have already been added
						addSeparator = false;

						// Remove the old custom item
						e.Menu.Items.Remove(customMenuItem);
					}

					if (addSeparator)
						e.Menu.Items.Add(new Separator());

					// Add the custom menu item
					e.Menu.Items.Add(new MenuItem() { Header = "Modified: " + DateTime.Now.ToLongTimeString(), Tag = CustomMenuTag });
				}
			}
			else if (e.TabItem is not null) {
				//
				// Tab Context Menu
				//

				if (e.TabItem == modifiedContextMenuTabItem) {
					if (e.Menu is not null) {
						bool addSeparator = true;

						// Look for previous custom menu item already in the menu
						var customMenuItem = e.Menu.Items.OfType<MenuItem>().FirstOrDefault(x => x.Tag?.ToString() == CustomMenuTag);
						if (customMenuItem is not null) {
							// Separator will have already been added
							addSeparator = false;

							// Remove the old custom item
							e.Menu.Items.Remove(customMenuItem);
						}

						if (addSeparator)
							e.Menu.Items.Add(new Separator());

						// Add the custom menu item
						e.Menu.Items.Add(new MenuItem() { Header = "Modified: " + DateTime.Now.ToLongTimeString(), Tag = CustomMenuTag });
					}
				}
				else if (e.TabItem == customContextMenuTabItem) {
					// Create a new menu
					var menu = new MenuFlyout() { OverlayDismissEventPassThrough = true };
					menu.Items.Add("Custom Menu Item 1");
					menu.Items.Add("Custom Menu Item 2");
					menu.Items.Add(new Separator());
					menu.Items.Add("Created: " + DateTime.Now.ToLongTimeString());

					// Pass the menu back to the event
					e.Menu = menu;
				}
			}
		}

		private void OnNewTabRequested(object? sender, System.EventArgs e) {
			if (sender is AdvancedTabControl tabControl) {
				var documentNumber = ++_newDocumentIndex;
				var tabItem = new AdvancedTabItem() {
					Header = $"Document{documentNumber}.txt",
					Content = new TextBox() {
						Text = $"Content {documentNumber}",
						Margin = new Avalonia.Thickness(10),
						AcceptsReturn = true,
					}
				};
				tabControl.Items.Add(tabItem);
				tabItem.IsSelected = true;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The textual representation of the key gesture that will activate the first normal tab.
		/// </summary>
		public string ActivateNormalTab1KeyGestureText { get; }

		/// <summary>
		/// The textual representation of the key gesture that will activate the second normal tab.
		/// </summary>
		public string ActivateNormalTab2KeyGestureText { get; }

		/// <summary>
		/// The textual representation of the key gesture that will activate the first pinned tab.
		/// </summary>
		public string ActivatePinnedTab1KeyGestureText { get; }

		/// <summary>
		/// The textual representation of the key gesture that will activate the second pinned tab.
		/// </summary>
		public string ActivatePinnedTab2KeyGestureText { get; }

		/// <summary>
		/// The textual representation of the key gesture that will activate the first preview tab.
		/// </summary>
		public string ActivatePreviewTab1KeyGestureText { get; }

		/// <summary>
		/// The textual representation of the key gesture that will activate the second preview tab.
		/// </summary>
		public string ActivatePreviewTab2KeyGestureText { get; }

	}

}
