using ActiproSoftware.ProductSamples.DockingSamples.Common;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.TabbedMdiNewTabButton;

public partial class MainControl : UserControl {

	private int _documentIndex = 3;

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
	/// Occurs when a new window is requested by the user.
	/// </summary>
	private void OnDockSiteNewWindowRequested(object? sender, RoutedEventArgs e)
		=> DocumentHelper.CreateTextDocumentWindow(dockSite, ++_documentIndex);

}
