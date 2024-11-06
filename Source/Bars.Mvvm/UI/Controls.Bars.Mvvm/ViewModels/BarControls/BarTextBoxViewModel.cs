using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a textbox control within a bar control.
	/// </summary>
	public class BarTextBoxViewModel : BarKeyedObjectViewModelBase, IHasVariantImages {

		private bool _canCloneToRibbonQuickAccessToolBar = true;
		private ICommand? _command;
		private object? _commandParameter;
		private string? _description;
		private bool _isStarSizingAllowed;
		private bool _isVisible = true;
		private string? _keyTipText;
		private string? _label;
		private string? _placeholderText;
		private double _requestedWidth = 110.0;
		private object? _smallIcon;
		private string? _text;
		private string? _title;
		private ItemCollapseBehavior _toolBarItemCollapseBehavior = ItemCollapseBehavior.Default;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarTextBoxViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarTextBoxViewModel(string? key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarTextBoxViewModel(string? key, string? label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarTextBoxViewModel(string? key, string? label, string? keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarTextBoxViewModel(string? key, ICommand? command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarTextBoxViewModel(string? key, string? label, ICommand? command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public BarTextBoxViewModel(string? key, string? label, string? keyTipText, ICommand? command)
			: base(key) {

			_label = label ?? BarControlService.LabelGenerator.FromCommand(command) ?? BarControlService.LabelGenerator.FromKey(key);
			_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromCommand(command) ?? BarControlService.KeyTipTextGenerator.FromLabel(this._label);
			_command = command;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		object? IHasVariantImages.LargeIcon {
			get => null;
			set { /* No-op since a large image is not supported by the control */ }
		}

		/// <inheritdoc/>
		object? IHasVariantImages.MediumIcon {
			get => null;
			set { /* No-op since a medium image is not supported by the control */ }
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc cref="BarButtonViewModel.CanCloneToRibbonQuickAccessToolBar"/>
		public bool CanCloneToRibbonQuickAccessToolBar {
			get => _canCloneToRibbonQuickAccessToolBar;
			set => SetProperty(ref _canCloneToRibbonQuickAccessToolBar, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.Command"/>
		public ICommand? Command {
			get => _command;
			set => SetProperty(ref _command, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.CommandParameter"/>
		public object? CommandParameter {
			get => _commandParameter;
			set => SetProperty(ref _commandParameter, value);
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Description"/>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}

		/// <inheritdoc cref="BarComboBoxViewModel.IsStarSizingAllowed"/>
		public bool IsStarSizingAllowed {
			get => _isStarSizingAllowed;
			set => SetProperty(ref _isStarSizingAllowed, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.KeyTipText"/>
		public string? KeyTipText {
			get => _keyTipText;
			set => SetProperty(ref _keyTipText, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string? Label {
			get => _label;
			set => SetProperty(ref _label, value);
		}

		/// <summary>
		/// The placeholder text to display when the control is empty.
		/// </summary>
		public string? PlaceholderText {
			get => _placeholderText;
			set => SetProperty(ref _placeholderText, value);
		}
		
		/// <inheritdoc cref="BarComboBoxViewModel.RequestedWidth"/>
		public double RequestedWidth {
			get => _requestedWidth;
			set => SetProperty(ref _requestedWidth, value);
		}
		
		/// <inheritdoc cref="BarButtonViewModel.SmallIcon"/>
		public object? SmallIcon {
			get => _smallIcon;
			set => SetProperty(ref _smallIcon, value);
		}
		
		/// <summary>
		/// The text being edited in the control.
		/// </summary>
		public string? Text {
			get => _text;
			set => SetProperty(ref _text, value);
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Title"/>
		public string? Title {
			get => _title;
			set => SetProperty(ref _title, value);
		}
		
		/// <inheritdoc cref="BarButtonViewModel.ToolBarItemCollapseBehavior"/>
		public ItemCollapseBehavior ToolBarItemCollapseBehavior {
			get => _toolBarItemCollapseBehavior;
			set => SetProperty(ref _toolBarItemCollapseBehavior, value);
		}

	}

}
