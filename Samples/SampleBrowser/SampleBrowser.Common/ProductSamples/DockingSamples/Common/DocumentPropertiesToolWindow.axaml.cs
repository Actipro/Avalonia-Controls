using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia;
using System;
using System.Collections.ObjectModel;
using PropertyItem = System.Collections.Generic.KeyValuePair<string, object?>;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common {

	public partial class DocumentPropertiesToolWindow : ToolWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public DocumentPropertiesToolWindow() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override Type StyleKeyOverride
			// Ensure ToolWindow implicit styles are applied to the derived class
			=> typeof(ToolWindow);

	}

}
