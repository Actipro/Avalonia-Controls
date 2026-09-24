using ActiproSoftware.UI.Avalonia.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.CustomDockingWindows;

public partial class CustomToolWindow : ToolWindow {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public CustomToolWindow() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override Type StyleKeyOverride {
		// Ensure ToolWindow implicit styles are applied to the derived class
		get => typeof(ToolWindow);
	}

}
