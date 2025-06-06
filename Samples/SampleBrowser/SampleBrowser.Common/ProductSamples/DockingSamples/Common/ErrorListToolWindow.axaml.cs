using ActiproSoftware.UI.Avalonia.Controls.Docking;
using ActiproSoftware.UI.Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common {

	public partial class ErrorListToolWindow : ToolWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public ErrorListToolWindow() {
			// Initialize the sample view models
			Items = new ObservableCollection<ErrorListItem>(CreateSampleErrorListItems());

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public IEnumerable<ErrorListItem> CreateSampleErrorListItems() {
			yield return new ErrorListItem() { Project = "Docking/MDI", IconKey = SharedImageSourceKeys.Information, FileName = "ErrorList.axaml", Description = "This tool window is for demonstration purposes only and is not fully functional." };
			yield return new ErrorListItem() { Project = "Docking/MDI", IconKey = SharedImageSourceKeys.Error, Line = "42", FileName = "SampleFile.cs", Description = "; expected" };
			yield return new ErrorListItem() { Project = "Docking/MDI", IconKey = SharedImageSourceKeys.Error, FileName = "SampleFile.axaml", Description = "One or more errors occurred." };
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The collection of items to be displayed in the list.
		/// </summary>
		public ObservableCollection<ErrorListItem> Items { get; }

		/// <inheritdoc/>
		protected override Type StyleKeyOverride
			// Ensure ToolWindow implicit styles are applied to the derived class
			=> typeof(ToolWindow);

	}

}
