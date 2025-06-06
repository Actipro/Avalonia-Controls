using ActiproSoftware.ProductSamples.DockingSamples.Common;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System.Threading.Tasks;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.TabbedMdiOnly {

	public partial class MainControl : UserControl {

		private int _documentIndex = 4; // XAML predefines several documents

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnNewDocumentMenuItemClick(object? sender, RoutedEventArgs e) {
			DocumentHelper.CreateTextDocumentWindow(dockSite, ++_documentIndex);
		}

		private async void OnOpenDocumentMenuItemClick(object? sender, RoutedEventArgs e) {
			_ = await DocumentHelper.OpenTextDocumentWindowAsync(dockSite);
		}

	}

}
