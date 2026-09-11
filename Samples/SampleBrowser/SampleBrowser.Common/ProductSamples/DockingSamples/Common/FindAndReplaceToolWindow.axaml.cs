using ActiproSoftware.UI.Avalonia.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common;

public partial class FindAndReplaceToolWindow : ToolWindow {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public FindAndReplaceToolWindow() {
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
