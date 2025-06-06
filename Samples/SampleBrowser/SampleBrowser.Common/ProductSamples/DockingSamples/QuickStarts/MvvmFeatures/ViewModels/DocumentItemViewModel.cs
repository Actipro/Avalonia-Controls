namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures.ViewModels {

	/// <summary>
	/// Represents a document item view-model.
	/// </summary>
	public class DocumentItemViewModel : DockingItemViewModelBase {

		private string? _fileName;
		private bool _isReadOnly;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The file name associated with the view-model.
		/// </summary>
		public string? FileName {
			get => _fileName;
			set => SetProperty(ref _fileName, value);
		}

		/// <summary>
		/// The read-only state of the associated with the view-model.
		/// </summary>
		public bool IsReadOnly {
			get => _isReadOnly;
			set => SetProperty(ref _isReadOnly, value);
		}

		/// <inheritdoc/>
		public override bool IsTool
			=> false;

	}

}
