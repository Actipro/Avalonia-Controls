using ActiproSoftware.UI.Avalonia;
using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System.Linq;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.DefaultLocations {

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
		/// Occurs when a docking window's default location is requested.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowDefaultLocationEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowDefaultLocationRequested(object? sender, DockingWindowDefaultLocationEventArgs e) {
			if (e.Window?.SerializationId == "bottomLeft1") {
				if (e.State == DockingWindowState.Docked) {
					// Dock in hierarchy under the left tool window
					var targetToolWindow = dockSite.ToolWindows.FirstOrDefault(x => x.SerializationId == "left1");
					if (targetToolWindow?.IsOpen == true) {
						e.Target = targetToolWindow;
						e.Side = Dock.Bottom;
					}
				}
			}
		}

		/// <summary>
		/// Creates the content of a tool window populated by the given text.
		/// </summary>
		/// <param name="text">The text to include as content.</param>
		/// <returns>The visual representing the content.</returns>
		private Visual CreateContent(string text) {
			return new TextBox() {
				BorderThickness = new Thickness(0),
				TextWrapping = TextWrapping.Wrap,
				Text = text
			};
		}

		/// <summary>
		/// Opens the tool windows for this sample.
		/// </summary>
		private void OpenToolWindows() {

			var toolWindow = new ToolWindow(dockSite, "right1", "Tool Window 1", icon: null,
				content: CreateContent("This first tool window has no default dock side set, and will fall back to docking on the right side of the primary dock host."));
			toolWindow.WindowGroupName = "Right Group";
			toolWindow.ContainerDockedSize = new Size(150, 200);
			toolWindow.Activate(focus: false);

			toolWindow = new ToolWindow(dockSite, "bottom1", "Tool Window 2", icon: null,
				content: CreateContent("This second tool window has DefaultDockSide = Bottom and will default to open at the bottom of the primary dock host."));
			toolWindow.DefaultDockSide = Dock.Bottom;
			toolWindow.WindowGroupName = "Bottom Group";
			toolWindow.ContainerDockedSize = new Size(200, 150);
			toolWindow.Activate(focus: false);

			toolWindow = new ToolWindow(dockSite, "bottom2", "Tool Window 3", icon: null,
				content: CreateContent("This third tool window has no default dock side set, but is in the same WindowGroupName as \"Tool Window 2\", and will default to attach to it."));
			toolWindow.WindowGroupName = "Bottom Group";
			toolWindow.ContainerDockedSize = new Size(200, 150);
			toolWindow.Activate(focus: false);

			toolWindow = new ToolWindow(dockSite, "right2", "Tool Window 4", icon: null,
				content: CreateContent("This fourth tool window has DefaultDockSide = Bottom, but is in the same WindowGroupName as \"Tool Window 1\", and will default to attach to it because that takes priority over DefaultDockSide."));
			toolWindow.DefaultDockSide = Dock.Bottom;
			toolWindow.WindowGroupName = "Right Group";
			toolWindow.ContainerDockedSize = new Size(150, 200);
			toolWindow.Activate(focus: false);

			toolWindow = new ToolWindow(dockSite, "left1", "Tool Window 5", icon: null,
				content: CreateContent("This fifth tool window specifies the same parameters as \"Tool Window 4\" but also has a DefaultLocationRequested event handler that overrides everything by forcing a left side dock."));
			toolWindow.DefaultDockSide = Dock.Bottom;
			toolWindow.WindowGroupName = "Right Group";
			toolWindow.ContainerDockedSize = new Size(150, 200);
			toolWindow.DefaultLocationRequested += (sender, e) => {
				if (e.State == DockingWindowState.Docked) {
					// Force a left side dock
					e.Target = null;
					e.Side = Dock.Left;
				}
			};
			toolWindow.Activate(focus: false);

			toolWindow = new ToolWindow(dockSite, "bottomLeft1", "Tool Window 6", icon: null,
				content: CreateContent("This sixth tool window's default location is set in a generalized DockSite.WindowDefaultLocationRequested event handler."));
			toolWindow.ContainerDockedSize = new Size(150, 200);
			toolWindow.Activate(focus: false);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		protected override void OnLoaded(RoutedEventArgs e) {
			base.OnLoaded(e);

			if (dockSite.ToolWindows.Count == 0)
				this.OpenToolWindows();
		}

	}

}
