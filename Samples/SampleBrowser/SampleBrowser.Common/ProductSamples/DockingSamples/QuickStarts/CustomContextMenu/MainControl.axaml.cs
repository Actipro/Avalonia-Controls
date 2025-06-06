using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia.Controls;
using System;
using System.Text;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.CustomContextMenu {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Appends a message to the events <see cref="ListBox"/>.
		/// </summary>
		/// <param name="text">The text to append.</param>
		private void AppendMessage(string text) {
			var item = new ListBoxItem() { Content = text };
			eventsListBox.Items.Add(item);
			eventsListBox.SelectedItem = item;
			eventsListBox.ScrollIntoView(item);
		}

		/// <summary>
		/// Occurs when a docking-related context menu is opening, allowing for customization before it is displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingMenuEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteMenuOpening(object? sender, DockingMenuEventArgs e) {
			var message = new StringBuilder($"WindowContextMenu: Kind={e.Kind}");

			if ((e.Window is not null) && (e.Menu is not null)) {
				message.Append($", Title={e.Window.Title} ");

				if (e.Window == customizedDocumentWindow) {
					// The "Context Menu Altered" document window's context menu is altered
					// to include a custom menu item at its end.

					e.Menu.Items.Add(new Separator());

					var menuItem = new MenuItem() { Header = $"Custom Menu Item Added at {DateTime.Now.ToShortTimeString()}" };
					e.Menu.Items.Add(menuItem);
				}
				else if (e.Window == customizedToolWindow) {
					// The "Context Menu Replaced" tool window has its context menu completely
					// replaced by a new menu based on the Actipro Bars product.

					var barMenu = new BarMenuFlyout();
					barMenu.Items.Add(new BarMenuItem() {
						Label = "Custom Menu",
						LargeIcon = new ActiproSoftware.UI.Avalonia.Images.BarsLogo48(),
						Description = "This menu is based on the Actipro Bars product",
						UseLargeSize = true,
					});
					barMenu.Items.Add(new BarSeparator());
					barMenu.Items.Add(new BarMenuItem($"Custom Menu Item Added at {DateTime.Now.ToShortTimeString()}"));

					e.Menu = barMenu;
				}
			}

			AppendMessage(message.ToString());
		}

	}

}
