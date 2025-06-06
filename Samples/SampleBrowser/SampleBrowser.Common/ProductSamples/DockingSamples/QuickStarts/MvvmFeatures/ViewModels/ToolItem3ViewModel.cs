namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures.ViewModels {

	/// <summary>
	/// Represents the tool view-model.
	/// </summary>
	public class ToolItem3ViewModel : ToolItemViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public ToolItem3ViewModel() {
			SerializationId = "Tool3";  // NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
			Title = "Tool 3";
		}

	}

}
