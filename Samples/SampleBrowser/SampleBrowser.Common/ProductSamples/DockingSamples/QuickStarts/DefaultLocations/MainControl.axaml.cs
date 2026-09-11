using ActiproSoftware.UI.Avalonia.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.DefaultLocations;

public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when a docking window's default location is requested.
	/// </summary>
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
	private static Visual CreateContent(string text) {
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

		var toolWindow = new ToolWindow(dockSite, "right1", "Tool Window 1",
			content: CreateContent("This first tool window has no default dock side set, and will fall back to docking on the right side of the primary dock host.")) {
			WindowGroupName = "Right Group",
			ContainerDockedSize = new Size(150, 200)
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "bottom1", "Tool Window 2",
			content: CreateContent("This second tool window has DefaultDockSide = Bottom and will default to open at the bottom of the primary dock host.")) {
			DefaultDockSide = Dock.Bottom,
			WindowGroupName = "Bottom Group",
			ContainerDockedSize = new Size(200, 150)
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "bottom2", "Tool Window 3",
			content: CreateContent("This third tool window has no default dock side set, but is in the same WindowGroupName as \"Tool Window 2\", and will default to attach to it.")) {
			WindowGroupName = "Bottom Group",
			ContainerDockedSize = new Size(200, 150)
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "right2", "Tool Window 4",
			content: CreateContent("This fourth tool window has DefaultDockSide = Bottom, but is in the same WindowGroupName as \"Tool Window 1\", and will default to attach to it because that takes priority over DefaultDockSide.")) {
			DefaultDockSide = Dock.Bottom,
			WindowGroupName = "Right Group",
			ContainerDockedSize = new Size(150, 200)
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "left1", "Tool Window 5",
			content: CreateContent("This fifth tool window specifies the same parameters as \"Tool Window 4\" but also has a DefaultLocationRequested event handler that overrides everything by forcing a left side dock.")) {
			DefaultDockSide = Dock.Bottom,
			WindowGroupName = "Right Group",
			ContainerDockedSize = new Size(150, 200)
		};
		toolWindow.DefaultLocationRequested += (sender, e) => {
			if (e.State == DockingWindowState.Docked) {
				// Force a left side dock
				e.Target = null;
				e.Side = Dock.Left;
			}
		};
		toolWindow.Activate(focus: false);

		toolWindow = new ToolWindow(dockSite, "bottomLeft1", "Tool Window 6",
			content: CreateContent("This sixth tool window's default location is set in a generalized DockSite.WindowDefaultLocationRequested event handler.")) {
			ContainerDockedSize = new Size(150, 200)
		};
		toolWindow.Activate(focus: false);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	protected override void OnLoaded(RoutedEventArgs e) {
		base.OnLoaded(e);

		if (dockSite.ToolWindows.Count == 0)
			OpenToolWindows();
	}

}
