using ActiproSoftware.ProductSamples.DockingSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls.Docking;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.PrimaryDocumentTracking;

public partial class MainControl : UserControl {

	private int _documentIndex = 3; // XAML predefines several documents

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
	/// Occurs when the primary document is changed.
	/// </summary>
	private void OnDockSitePrimaryDocumentChanged(object? sender, DockingWindowEventArgs e)
		=> UpdateStatusBar();

	/// <summary>
	/// Occurs when a docking window is activated or deactivated.
	/// </summary>
	private void OnDockSiteWindowActivatedOrDeactivated(object? sender, DockingWindowEventArgs e)
		=> UpdateStatusBar();

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	private void OnNewDocumentMenuItemClick(object? sender, RoutedEventArgs e)
		=> DocumentHelper.CreateTextDocumentWindow(dockSite, ++_documentIndex);

	/// <summary>
	/// Occurs when the menu item is clicked.
	/// </summary>
	private async void OnOpenDocumentMenuItemClick(object? sender, RoutedEventArgs e)
		=> _ = await DocumentHelper.OpenTextDocumentWindowAsync(dockSite);

	/// <summary>
	/// Updates the status bar.
	/// </summary>
	private void UpdateStatusBar() {
		primaryDocumentTextBlock.Text = (dockSite.PrimaryDocument is not null ? dockSite.PrimaryDocument.Title : "(none)");
		activeWindowTextBlock.Text = (dockSite.ActiveWindow is not null ? dockSite.ActiveWindow.Title : "(none)");
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnLoaded(RoutedEventArgs e) {
		base.OnLoaded(e);

		// Activate the first tool window
		dockSite.ToolWindows.FirstOrDefault()?.Activate();
	}

}
