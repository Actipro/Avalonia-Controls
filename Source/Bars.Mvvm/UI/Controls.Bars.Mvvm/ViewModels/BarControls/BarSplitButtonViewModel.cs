using Avalonia.Input;
using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a split button control within a bar control.
	/// </summary>
	public class BarSplitButtonViewModel : BarPopupButtonViewModel {

		private KeyGesture? _inputGesture;
		private bool _isInputGestureTextVisible = true;
		private bool _staysOpenOnClick;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarSplitButtonViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarSplitButtonViewModel(string? key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarSplitButtonViewModel(string? key, string? label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarSplitButtonViewModel(string? key, string? label, string? keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarSplitButtonViewModel(string? key, ICommand? command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarSplitButtonViewModel(string? key, string? label, ICommand? command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public BarSplitButtonViewModel(string? key, string? label, string? keyTipText, ICommand? command)
			: base(key, label, keyTipText, command) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.InputGesture"/>
		public KeyGesture? InputGesture {
			get => _inputGesture;
			set => SetProperty(ref _inputGesture, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.IsInputGestureTextVisible"/>
		public bool IsInputGestureTextVisible {
			get => _isInputGestureTextVisible;
			set => SetProperty(ref _isInputGestureTextVisible, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.StaysOpenOnClick"/>
		public bool StaysOpenOnClick {
			get => _staysOpenOnClick;
			set => SetProperty(ref _staysOpenOnClick, value);
		}

	}

}
