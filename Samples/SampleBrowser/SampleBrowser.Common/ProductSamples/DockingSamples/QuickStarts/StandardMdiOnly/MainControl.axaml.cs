using ActiproSoftware.ProductSamples.DockingSamples.Common;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System.Threading.Tasks;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.StandardMdiOnly {

	public partial class MainControl : UserControl {

		private int _documentIndex = 3; // XAML predefines several documents

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// The Sample Browser may or may not be showing the side panel that impacts
			// the initial size of the dock site and, thus, the initial size of the documents.
			// Cascade the documents shortly after the first size change is detected to ensure
			// the initial layout is optimized for the size.
			Dispatcher.UIThread.InvokeAsync(async () => {
				await Task.Delay(50);
				dockSite.CascadeDocuments();
			}, DispatcherPriority.Input);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnCanWindowsMaximizeMenuItemClick(object? sender, RoutedEventArgs e) {
			standardMdiHost.CanWindowsMaximize = !standardMdiHost.CanWindowsMaximize;
		}

		private void OnCanWindowsMinimizeMenuItemClick(object? sender, RoutedEventArgs e) {
			standardMdiHost.CanWindowsMinimize = !standardMdiHost.CanWindowsMinimize;
		}

		private void OnAreMaximizedWindowFramesVisibleMenuItemClick(object? sender, RoutedEventArgs e) {
			standardMdiHost.AreMaximizedWindowFramesVisible = !standardMdiHost.AreMaximizedWindowFramesVisible;
		}

		private void OnIsScrollingEnabledMenuItemClick(object? sender, RoutedEventArgs e) {
			standardMdiHost.IsScrollingEnabled = !standardMdiHost.IsScrollingEnabled;
		}

		private void OnArrangeMinimizedWindowsMenuItemClick(object? sender, RoutedEventArgs e) {
			standardMdiHost.ArrangeMinimizedWindows();
		}

		private void OnNewDocumentMenuItemClick(object? sender, RoutedEventArgs e) {
			DocumentHelper.CreateTextDocumentWindow(dockSite, ++_documentIndex);
		}

		private async void OnOpenDocumentMenuItemClick(object? sender, RoutedEventArgs e) {
			_ = await DocumentHelper.OpenTextDocumentWindowAsync(dockSite);
		}

	}

}
