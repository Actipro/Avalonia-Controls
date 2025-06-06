using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common {

	public partial class ClassViewToolWindow : ToolWindow {

		private bool _expandOnLoad = OperatingSystem.IsBrowser(); // See Web Assembly comment in OnLoaded override

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public ClassViewToolWindow() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override void OnLoaded(RoutedEventArgs e) {
			base.OnLoaded(e);

			if (_expandOnLoad) {
				_expandOnLoad = false;

				// There is an issue with TreeView when used with Web Assembly (WASM) if the root TreeViewItem.IsExpanded
				//   property is set to True.  When expanded, the TreeView can fail to initialize properly and results in
				//   the tool window content area appearing to be empty.
				//
				// This issue was initially verified with Avalonia 11.3.0 and may be resolved in future updates.

				if (this.FindDescendantOfType<TreeView>() is { } treeView)
					treeView.ExpandAll();
			}
		}

		/// <inheritdoc/>
		protected override Type StyleKeyOverride
			// Ensure ToolWindow implicit styles are applied to the derived class
			=> typeof(ToolWindow);

	}

}
