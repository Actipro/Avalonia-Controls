using ActiproSoftware.ProductSamples.DockingSamples.Common;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.Switchers {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		protected override void OnLoaded(RoutedEventArgs e) {
			base.OnLoaded(e);

			// Preload the Overflow sample with several documents
			var selectNextDocumentGesture = overflowStandardSwitcher.SelectNextWindowKeyGesture?.ToString() ?? "NULL";
			for (var documentIndex = 1; documentIndex <= 50; documentIndex++) {
				var documentWindow = DocumentHelper.CreateTextDocumentWindow(overflowDockSite, documentIndex, $"Document {documentIndex}{Environment.NewLine}{Environment.NewLine}Press '{selectNextDocumentGesture}' to show the switcher and see how multiple columns and/or scroll buttons are used to display a large number of documents.");

				// Simulate a file name with directory since this appears in the switcher UI
				documentWindow.FileName = @"C:\Users\Actipro\My Documents\" + documentWindow.FileName;
			}
		}

	}

}
