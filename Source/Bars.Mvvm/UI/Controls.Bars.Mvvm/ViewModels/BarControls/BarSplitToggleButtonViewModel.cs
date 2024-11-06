using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a split toggle button control within a bar control.
	/// </summary>
	public class BarSplitToggleButtonViewModel : BarSplitButtonViewModel, ICheckable {

		private bool _isChecked = false;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarSplitToggleButtonViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarSplitToggleButtonViewModel(string? key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarSplitToggleButtonViewModel(string? key, string? label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarSplitToggleButtonViewModel(string? key, string? label, string? keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarSplitToggleButtonViewModel(string? key, ICommand? command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarSplitToggleButtonViewModel(string? key, string? label, ICommand? command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public BarSplitToggleButtonViewModel(string? key, string? label, string? keyTipText, ICommand? command)
			: base(key, label, keyTipText, command) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc cref="ICheckable.IsChecked"/>
		public bool IsChecked {
			get => _isChecked;
			set => SetProperty(ref _isChecked, value);
		}
		
	}

}
