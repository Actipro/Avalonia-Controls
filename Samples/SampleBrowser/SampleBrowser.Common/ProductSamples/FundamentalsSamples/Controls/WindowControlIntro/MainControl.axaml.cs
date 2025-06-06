using ActiproSoftware.Extensions;
using ActiproSoftware.UI.Avalonia;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System;
using System.Linq;
using System.Text;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.WindowControlIntro {

	public partial class MainControl : UserControl {

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="IsLocationSizeEventOutputEnabled"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> IsLocationSizeEventOutputEnabledProperty
			= AvaloniaProperty.Register<MainControl, bool>(nameof(IsLocationSizeEventOutputEnabled));

		#endregion

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
		private void OnDockSiteMenuOpening(object sender, DockingMenuEventArgs e) {
			var sb = new StringBuilder($"WindowContextMenu: Kind={e.Kind}");

			if (e.Window != null) {
				sb.Append($", Title={e.Window.Title} ");

				// Add a custom menu option to the Output tool window that enables more detailed logging of location/size events
				if ((e.Menu is { } menu) && (e.Window == outputToolWindow)) {
					menu.Items.Add(new Separator());

					var menuItem = new MenuItem() { Header = "Location/Size Events", ToggleType = MenuItemToggleType.CheckBox };
					menuItem.BindToProperty(MenuItem.IsCheckedProperty, this, IsLocationSizeEventOutputEnabledProperty, BindingMode.TwoWay);
					e.Menu.Items.Add(menuItem);
				}
			}

			AppendMessage(sb.ToString());
		}

		/// <summary>
		/// Occurs when a <c>Button</c> is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnOpenWindowButtonClick(object sender, RoutedEventArgs e) {
			if (!eventsWindow.IsVisible)
				eventsWindow.Show();
			eventsWindow.Activate();
		}

		/// <summary>
		/// Occurs when the <c>Activated</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowActivated(object sender, RoutedEventArgs e)
			=> AppendMessage(nameof(WindowControl.Activated));

		/// <summary>
		/// Occurs when the <c>Closed</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowClosed(object sender, RoutedEventArgs e) {
			AppendMessage(nameof(WindowControl.Closed));
			eventsWindow.IsVisible = false;
		}

		/// <summary>
		/// Occurs when the <c>Closing</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="CancelRoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowClosing(object sender, CancelRoutedEventArgs e)
			=> AppendMessage(nameof(WindowControl.Closing));

		/// <summary>
		/// Occurs when the <c>Deactivated</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowDeactivated(object sender, RoutedEventArgs e)
			=> AppendMessage(nameof(WindowControl.Deactivated));

		/// <summary>
		/// Occurs when the <c>DragMoved</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowDragMoved(object sender, RoutedEventArgs e)
			=> AppendMessage(nameof(WindowControl.DragMoved));

		/// <summary>
		/// Occurs when the <c>DragMoving</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="CancelRoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowDragMoving(object sender, CancelRoutedEventArgs e)
			=> AppendMessage(nameof(WindowControl.DragMoving));

		/// <summary>
		/// Occurs when the <c>LocationChanged</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowLocationChanged(object sender, RoutedEventArgs e) {
			if (IsLocationSizeEventOutputEnabled)
				AppendMessage($"{nameof(WindowControl.LocationChanged)}: {eventsWindow.Left.Round()},{eventsWindow.Top.Round()}");
		}

		/// <summary>
		/// Occurs when the <c>Opened</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowOpened(object sender, RoutedEventArgs e) {
			AppendMessage(nameof(WindowControl.Opened));
			eventsWindow.IsVisible = true;
		}

		/// <summary>
		/// Occurs when the <c>SizeChanged</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowSizeChanged(object sender, RoutedEventArgs e) {
			if (IsLocationSizeEventOutputEnabled)
				AppendMessage($"{nameof(WindowControl.SizeChanged)}: {eventsWindow.Bounds.Width.Round()}x{eventsWindow.Bounds.Height.Round()}");
		}

		/// <summary>
		/// Occurs when the <c>StateChanged</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowStateChanged(object sender, RoutedEventArgs e) {
			AppendMessage($"{nameof(WindowControl.StateChanged)}: {eventsWindow.WindowState}");

			// Simulate Maximized and Minimized windows
			switch (eventsWindow.WindowState) {
				case WindowState.Maximized:
					// This basic sample matches the size of the visual parent,
					// but does not handle if the parent size changes
					if (eventsWindow.GetVisualParent() is { } parent) {
						eventsWindow.Left = 0;
						eventsWindow.Top = 0;
						eventsWindow.Width = parent.Bounds.Width;
						eventsWindow.Height = parent.Bounds.Height;
					}
					break;
				case WindowState.Minimized:
					// This basic sample minimizes to the upper-left corner
					eventsWindow.Left = 0;
					eventsWindow.Top = 0;
					break;
			}
		}

		/// <summary>
		/// Occurs when the <see cref="WindowControl.TitleBarMenuOpening"/> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="MenuEventArgs"/> that contains the event data.</param>
		private void OnWindowTitleBarMenuOpening(object sender, MenuEventArgs e) {
			if (sender is WindowControl window) {
				if (window == eventsWindow) {
					// Log the event
					AppendMessage(nameof(WindowControl.TitleBarMenuOpening));
				}
				else {
					// Initialize a default menu if a default menu is not defined
					e.Menu ??= new MenuFlyout();

					// Add a custom menu item, separated from any existing menu items
					if (e.Menu.Items.Any())
						e.Menu.Items.Add(new Separator());
					e.Menu.Items.Add(new MenuItem() { Header = "Custom Menu Item Added at " + DateTime.Now.ToShortTimeString() });
				}
			}
		}

		/// <summary>
		/// Occurs when the <c>TitleBarDoubleTapped</c> event is raised.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="CancelRoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowTitleBarDoubleTapped(object sender, RoutedEventArgs e)
			=> AppendMessage(nameof(WindowControl.TitleBarDoubleTapped));

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Whether location/size event output is enabled.
		/// </summary>
		public bool IsLocationSizeEventOutputEnabled {
			get => GetValue(IsLocationSizeEventOutputEnabledProperty);
			set => SetValue(IsLocationSizeEventOutputEnabledProperty, value);
		}

	}

}
