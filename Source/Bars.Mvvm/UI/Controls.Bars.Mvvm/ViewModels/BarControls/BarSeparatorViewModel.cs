namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a separator control within a bar control.
	/// </summary>
	public class BarSeparatorViewModel : ObservableObjectBase, IHasTag {

		private bool _isVisible = true;
		private StandaloneToolBarSeparatorMode _standaloneToolBarSeparatorMode = StandaloneToolBarSeparatorMode.Default;
		private object? _tag;

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <summary>
		/// The mode that determines how the separator behaves within a <see cref="StandaloneToolBar"/>.
		/// </summary>
		/// <value>
		/// The default value is <see cref="StandaloneToolBarSeparatorMode.Default"/>.
		/// </value>
		public StandaloneToolBarSeparatorMode StandaloneToolBarSeparatorMode {
			get => _standaloneToolBarSeparatorMode;
			set => SetProperty(ref _standaloneToolBarSeparatorMode, value);
		}

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object? Tag {
			get => _tag;
			set => SetProperty(ref _tag, value);
		}
	}

}
