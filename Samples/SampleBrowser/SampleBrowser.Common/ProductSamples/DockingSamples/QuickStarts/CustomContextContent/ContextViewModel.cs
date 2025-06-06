namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.CustomContextContent {

	/// <summary>
	/// Represents the context view-model.
	/// </summary>
	public class ContextViewModel : ObservableObjectBase {

		private bool _isApproved;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Whether the data is approved.
		/// </summary>
		/// <value>
		/// <c>true</c> if the data is approved; otherwise, <c>false</c>.
		/// </value>
		public bool IsApproved {
			get => _isApproved;
			set => SetProperty(ref _isApproved, value);
		}

	}

}
