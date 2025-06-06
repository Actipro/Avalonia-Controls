using ActiproSoftware.ProductSamples.DockingSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.PromptOnClose {

	public partial class MainControl : UserControl {

		private int _documentIndex = 3; // XAML predefines several documents
		private bool _ignorePromptOnClose = false;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs before one or more docking windows are closed, allowing for cancellation of the close.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private async void OnDockSiteWindowsClosing(object sender, DockingWindowsEventArgs e) {
			// Prevent re-entry
			if (_ignorePromptOnClose)
				return;

			// This sample only prompts for closing document windows (not tool windows)
			var documents = e.Windows.OfType<DocumentWindow>().ToArray();
			if (documents.Length > 0) {
				var message = new StringBuilder("Are you sure you want to close the following:\r\n");
				foreach (var document in documents)
					message.Append($"\r\n  - {document.FileName}");

				// IMPORTANT NOTE: The following prompt is displayed asynchronously, so the WindowsClosing event
				// must be canceled and handled *before* showing the prompt since control will return to the
				// calling routine before waiting on a response to the prompt
				e.Cancel = true;
				e.Handled = true;

				// Ensure control returns to the caller even if MessageBox.Show ends up executing synchronously
				await Task.Yield();

				var result = await MessageBox.Show(message.ToString(), "Confirm Close", MessageBoxButtons.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
				if (result == MessageBoxResult.Yes) {
					// Since the original WindowClosing even was canceled, programmatically close each
					// document now that the user has confirmed
					try {
						// Ignore the WindowsClosing event that will be re-raised when programmatically closing
						_ignorePromptOnClose = true;

						// Programmatically close each document
						foreach (var document in documents)
							document.Close();
					}
					finally {
						_ignorePromptOnClose = false;
					}
				}
			}
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		private void OnNewDocumentMenuItemClick(object? sender, RoutedEventArgs e) {
			DocumentHelper.CreateTextDocumentWindow(dockSite, ++_documentIndex);
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		private async void OnOpenDocumentMenuItemClick(object? sender, RoutedEventArgs e) {
			_ = await DocumentHelper.OpenTextDocumentWindowAsync(dockSite);
		}

	}

}
