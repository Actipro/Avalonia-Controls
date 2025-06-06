using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures.ViewModels {

	//
	// NOTE: The State property in this VM class returns ToolItemState enums, which which allows for an abstraction layer between
	//       them and the DockingWindowState enum values they represent...
	//       This is useful in scenarios where you don't wish to have your models directly reference types in the Docking/MDI assembly... 
	//       If that is not a factor, there is nothing wrong with changing the properties to directly return the Actipro type instead
	//

	/// <summary>
	/// Represents a tool item view-model.
	/// </summary>
	public class ToolItemViewModel : DockingItemViewModelBase {

		private Dock _defaultDockSide = Dock.Right;
		private ToolItemState _state = ToolItemState.Docked;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The default side that the tool window will dock towards when no prior location is known.
		/// </summary>
		public Dock DefaultDockSide {
			get => _defaultDockSide;
			set => SetProperty(ref _defaultDockSide, value);
		}

		/// <inheritdoc/>
		public override bool IsTool
			=> true;

		/// <summary>
		/// The current state of the view.
		/// </summary>
		public ToolItemState State {
			get => _state;
			set => SetProperty(ref _state, value);
		}

	}

}
