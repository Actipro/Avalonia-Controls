using ActiproSoftware.UI.Avalonia;
using Avalonia.Controls;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures.ViewModels {

	/// <summary>
	/// Represents the main view-model.
	/// </summary>
	public class ToolsSampleMainViewModel : ObservableObjectBase {

		private readonly DeferrableObservableCollection<ToolItemViewModel> _toolItems = [];

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public ToolsSampleMainViewModel() {
			_toolItems.Add(new ToolItem1ViewModel());
			_toolItems.Add(new ToolItem2ViewModel() { State = ToolItemState.Document });
			_toolItems.Add(new ToolItem3ViewModel() { State = ToolItemState.AutoHide, DefaultDockSide = Dock.Left });

			foreach (var toolItem in _toolItems)
				toolItem.IsOpen = true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The tool items associated with this view-model.
		/// </summary>
		public IList<ToolItemViewModel> ToolItems
			=> _toolItems;

	}

}
