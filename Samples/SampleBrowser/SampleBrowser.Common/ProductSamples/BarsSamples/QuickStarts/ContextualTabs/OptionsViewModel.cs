namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ContextualTabs {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class OptionsViewModel : ObservableObjectBase {

		private bool _isPictureToolsContextualTabGroupVisible = false;
		private bool _isTableToolsContextualTabGroupVisible = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indicates if the Picture Tools contextual tab group is visible.
		/// </summary>
		/// <value><c>true</c> if the tab group is visible; otherwise <c>false</c>.</value>
		public bool IsPictureToolsContextualTabGroupVisible {
			get => _isPictureToolsContextualTabGroupVisible;
			set => SetProperty(ref _isPictureToolsContextualTabGroupVisible, value);
		}

		/// <summary>
		/// Indicates if the Table Tools contextual tab group is visible.
		/// </summary>
		public bool IsTableToolsContextualTabGroupVisible {
			get => _isTableToolsContextualTabGroupVisible;
			set => SetProperty(ref _isTableToolsContextualTabGroupVisible, value);
		}

	}

}
