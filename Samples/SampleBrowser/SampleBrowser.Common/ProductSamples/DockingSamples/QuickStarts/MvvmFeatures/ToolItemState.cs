namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures {

	//
	// NOTE: This enum and the related ToolItemStateConverter can be used in scenarios where you don't wish for your models to directly 
	//       reference types in the Docking/MDI assembly... it allows you to have a layer of abstraction if desired, but there
	//       is nothing wrong with directly referencing DockingWindowState in your VM class to avoid having to use this abstraction layer
	//

	/// <summary>
	/// Specifies the state of a tool item's view.
	/// </summary>
	public enum ToolItemState {

		/// <summary>
		/// The view's docking window is docked in a container outside of the MDI (multiple document interface) area.
		/// </summary>
		Docked,

		/// <summary>
		/// The view's docking window is a document within the MDI (multiple document interface) area.
		/// </summary>
		Document,

		/// <summary>
		/// The view's docking window is auto-hidden in a container along the side of a dock host.
		/// </summary>
		AutoHide

	}

}
