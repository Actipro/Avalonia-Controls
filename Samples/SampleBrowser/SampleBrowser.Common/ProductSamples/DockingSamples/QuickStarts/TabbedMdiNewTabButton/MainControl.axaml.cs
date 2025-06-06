using ActiproSoftware.ProductSamples.DockingSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Text;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.TabbedMdiNewTabButton {

	public partial class MainControl : UserControl {

		private int _documentIndex = 3;

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
		/// Occurs when a new window is requested by the user.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contained data related to this event.</param>
		private void OnDockSiteNewWindowRequested(object? sender, RoutedEventArgs e) {
			DocumentHelper.CreateTextDocumentWindow(dockSite, ++_documentIndex);
		}

	}

}
