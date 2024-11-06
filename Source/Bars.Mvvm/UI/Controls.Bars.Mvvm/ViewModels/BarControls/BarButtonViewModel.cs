using Avalonia.Input;
using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a regular button control within a bar control.
	/// </summary>
	public class BarButtonViewModel : BarKeyedObjectViewModelBase, IHasVariantImages {

		// NOTE: Documentation comments on the members of this type are referenced via inheritdoc and reused throughout other view model types

		private bool _canCloneToRibbonQuickAccessToolBar = true;
		private ICommand? _command;
		private object? _commandParameter;
		private string? _description;
		private KeyGesture? _inputGesture;
		private bool _isInputGestureTextVisible = true;
		private bool _isVisible = true;
		private string? _keyTipText;
		private string? _label;
		private object? _largeIcon;
		private object? _mediumIcon;
		private object? _smallIcon;
		private bool _staysOpenOnClick;
		private string? _title;
		private ItemCollapseBehavior _toolBarItemCollapseBehavior = ItemCollapseBehavior.Default;
		private ItemVariantBehavior _toolBarItemVariantBehavior = ItemVariantBehavior.AlwaysSmall;
		private bool _useLargeMenuItem;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public BarButtonViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public BarButtonViewModel(string? key)
			: this(key, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		public BarButtonViewModel(string? key, string? label) 
			: this(key, label, keyTipText: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and key tip text.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
		public BarButtonViewModel(string? key, string? label, string? keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and command.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="command">The command to attach to the control.</param>
		public BarButtonViewModel(string? key, ICommand? command)
			: this(key, label: null, command) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and command.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		public BarButtonViewModel(string? key, string? label, ICommand? command)
			: this(key, label, keyTipText: null, command) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, key tip text, and command.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="command"/> or <paramref name="label"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		public BarButtonViewModel(string? key, string? label, string? keyTipText, ICommand? command)
			: base(key) {

			_label = label ?? BarControlService.LabelGenerator.FromCommand(command) ?? BarControlService.LabelGenerator.FromKey(key);
			_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromCommand(command) ?? BarControlService.KeyTipTextGenerator.FromLabel(this._label);
			_command = command;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Indicates whether the control can be cloned to the ribbon quick-access toolbar.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanCloneToRibbonQuickAccessToolBar {
			get => _canCloneToRibbonQuickAccessToolBar;
			set => SetProperty(ref _canCloneToRibbonQuickAccessToolBar, value);
		}

		/// <summary>
		/// The <see cref="ICommand"/> to attach to the control.
		/// </summary>
		public ICommand? Command {
			get => _command;
			set => SetProperty(ref _command, value);
		}

		/// <summary>
		/// The parameter to pass to the <see cref="Command"/> property.
		/// </summary>
		public object? CommandParameter {
			get => _commandParameter;
			set => SetProperty(ref _commandParameter, value);
		}
		
		/// <summary>
		/// The text description to display in screen tips.
		/// </summary>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}

		/// <summary>
		/// The input gesture to display in menu items and screen tips.
		/// </summary>
		public KeyGesture? InputGesture {
			get => _inputGesture;
			set => SetProperty(ref _inputGesture, value);
		}

		/// <summary>
		/// Indicates whether the input gesture text is allowed to be visible in the user interface.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsInputGestureTextVisible {
			get => _isInputGestureTextVisible;
			set => SetProperty(ref _isInputGestureTextVisible, value);
		}

		/// <summary>
		/// Indicates whether the control is currently visible.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <summary>
		/// The key tip text used to access the control.
		/// </summary>
		public string? KeyTipText {
			get => _keyTipText;
			set => SetProperty(ref _keyTipText, value);
		}

		/// <summary>
		/// The text label to display.
		/// </summary>
		public string? Label {
			get => _label;
			set => SetProperty(ref _label, value);
		}

		/// <inheritdoc cref="IHasVariantImages.LargeIcon"/>
		public object? LargeIcon {
			get => _largeIcon;
			set => SetProperty(ref _largeIcon, value);
		}

		/// <inheritdoc cref="IHasVariantImages.MediumIcon"/>
		public object? MediumIcon {
			get => _mediumIcon;
			set => SetProperty(ref _mediumIcon, value);
		}

		/// <inheritdoc cref="IHasVariantImages.SmallIcon"/>
		public object? SmallIcon {
			get => _smallIcon;
			set => SetProperty(ref _smallIcon, value);
		}
		
		/// <summary>
		/// Indicates whether menus should try and remain open when the control is clicked.
		/// </summary>
		/// <value>
		/// The default value is <c>false</c>.
		/// </value>
		public bool StaysOpenOnClick {
			get => _staysOpenOnClick;
			set => SetProperty(ref _staysOpenOnClick, value);
		}

		/// <summary>
		/// The string title, which can override the <see cref="Label"/> when displayed in screen tips and customization UI.
		/// </summary>
		public string? Title {
			get => _title;
			set => SetProperty(ref _title, value);
		}
		
		/// <summary>
		/// The <see cref="ItemCollapseBehavior"/> for the control when in a ribbon using Simplified layout mode.
		/// </summary>
		/// <value>
		/// The default value is <c>Default</c>.
		/// </value>
		public ItemCollapseBehavior ToolBarItemCollapseBehavior {
			get => _toolBarItemCollapseBehavior;
			set => SetProperty(ref _toolBarItemCollapseBehavior, value);
		}

		/// <summary>
		/// The <see cref="ItemVariantBehavior"/> for the control when in a toolbar, which also applies when in a ribbon using Simplified layout mode.
		/// </summary>
		/// <value>
		/// The default value is <see cref="ItemVariantBehavior.AlwaysSmall"/>.
		/// </value>
		public ItemVariantBehavior ToolBarItemVariantBehavior {
			get => _toolBarItemVariantBehavior;
			set => SetProperty(ref _toolBarItemVariantBehavior, value);
		}

		/// <summary>
		/// Indicates whether to use a large size when the control is a menu item.
		/// </summary>
		/// <value>
		/// The default value is <c>false</c>.
		/// </value>
		public bool UseLargeMenuItem {
			get => _useLargeMenuItem;
			set => SetProperty(ref _useLargeMenuItem, value);
		}

	}

}
