using ActiproSoftware.UI.Avalonia.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.LayoutSerialization;

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

	/// <summary>
	/// An object which defines additional custom data associated with the tool window.  This sample shows
	/// how this custom data can be serialized/deserialized along with other layout information.
	/// </summary>
	public CustomToolWindowData? CustomData { get; set; }

	/// <inheritdoc/>
	protected override Type StyleKeyOverride {
		// Ensure ToolWindow implicit styles are applied to the derived class
		get => typeof(ToolWindow);
	}

}
