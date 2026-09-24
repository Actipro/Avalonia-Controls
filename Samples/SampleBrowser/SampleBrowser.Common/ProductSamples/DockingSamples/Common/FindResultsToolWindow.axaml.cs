using ActiproSoftware.UI.Avalonia.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common;

public partial class FindResultsToolWindow : ToolWindow {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public FindResultsToolWindow() {
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
