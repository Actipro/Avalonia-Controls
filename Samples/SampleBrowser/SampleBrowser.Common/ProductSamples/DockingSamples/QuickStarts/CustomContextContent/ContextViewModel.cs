namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.CustomContextContent;

/// <summary>
/// Represents the context view-model.
/// </summary>
public class ContextViewModel : ObservableObjectBase {

	private bool _isApproved;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Whether the data is approved.
	/// </summary>
	public bool IsApproved {
		get => _isApproved;
		set => SetProperty(ref _isApproved, value);
	}

}
