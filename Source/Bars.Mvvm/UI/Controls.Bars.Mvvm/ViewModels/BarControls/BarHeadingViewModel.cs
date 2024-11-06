namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a heading control within a bar control.
	/// </summary>
	public class BarHeadingViewModel : ObservableObjectBase {

		private string? _label;
		private bool _isVisible = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public BarHeadingViewModel()  // Parameterless constructor required for XAML support
			: this(label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified label.
		/// </summary>
		/// <param name="label">The text label to display.</param>
		public BarHeadingViewModel(string? label) {
			_label = label;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string? Label {
			get => _label;
			set => SetProperty(ref _label, value);
		}

	}

}
