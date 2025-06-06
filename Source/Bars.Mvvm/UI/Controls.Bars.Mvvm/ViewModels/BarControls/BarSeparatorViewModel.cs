namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a separator control within a bar control.
	/// </summary>
	public class BarSeparatorViewModel : ObservableObjectBase, IHasTag {

		private bool _isVisible = true;
		private object? _tag;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object? Tag {
			get => _tag;
			set => SetProperty(ref _tag, value);
		}
	}

}
