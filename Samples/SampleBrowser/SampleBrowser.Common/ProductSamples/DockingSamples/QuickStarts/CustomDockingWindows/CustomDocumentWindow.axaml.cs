using ActiproSoftware.UI.Avalonia.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.CustomDockingWindows;

public partial class CustomDocumentWindow : DocumentWindow {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public CustomDocumentWindow() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override Type StyleKeyOverride {
		// Ensure DocumentWindow implicit styles are applied to the derived class
		get => typeof(DocumentWindow);
	}

}
