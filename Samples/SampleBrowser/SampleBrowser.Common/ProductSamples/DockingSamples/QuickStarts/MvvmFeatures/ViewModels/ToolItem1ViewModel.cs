namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures.ViewModels {

	/// <summary>
	/// Represents the tool view-model.
	/// </summary>
	public class ToolItem1ViewModel : ToolItemViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public ToolItem1ViewModel() {
			SerializationId = "Tool1";  // NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
			Title = "Tool 1";
		}

	}

}
