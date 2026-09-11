using ActiproSoftware.UI.Avalonia.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common;

public partial class DocumentPropertiesToolWindow : ToolWindow {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public DocumentPropertiesToolWindow() {
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
