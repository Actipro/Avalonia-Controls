using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a popup button control within a bar control.
	/// </summary>
	public class BarPopupButtonViewModel : BarKeyedObjectViewModelBase, IHasVariantImages {

		private bool _canCloneToRibbonQuickAccessToolBar = true;
		private ICommand? _command;
		private object? _commandParameter;
		private string? _description;
		private bool _isVisible = true;
		private string? _keyTipText;
		private string? _label;
		private object? _largeIcon;
		private object? _mediumIcon;
		private ICommand? _popupOpeningCommand;
		private object? _smallIcon;
		private string? _title;
		private ItemCollapseBehavior _toolBarItemCollapseBehavior = ItemCollapseBehavior.Default;
		private ItemVariantBehavior _toolBarItemVariantBehavior = ItemVariantBehavior.AlwaysSmall;
		private bool _useLargeMenuItem;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarPopupButtonViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarPopupButtonViewModel(string? key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarPopupButtonViewModel(string? key, string? label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarPopupButtonViewModel(string? key, string? label, string? keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		protected BarPopupButtonViewModel(string? key, string? label, string? keyTipText, ICommand? command)
			: base(key) {

			// NOTE: This class has an ICommand, but it is primarily only used by derived classes and
			//		 is why this overload is protected (instead of public)

			_label = label ?? BarControlService.LabelGenerator.FromCommand(command) ?? BarControlService.LabelGenerator.FromKey(key);
			_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromCommand(command) ?? BarControlService.KeyTipTextGenerator.FromLabel(this._label);
			_command = command;
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

		/// <inheritdoc cref="BarButtonViewModel.LargeIcon"/>
		public object? LargeIcon {
			get => _largeIcon;
			set => SetProperty(ref _largeIcon, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.MediumIcon"/>
		public object? MediumIcon {
			get => _mediumIcon;
			set => SetProperty(ref _mediumIcon, value);
		}

		/// <summary>
		/// The collection of menu items that appear within the popup.
		/// </summary>
		[Content]
		public ObservableCollection<object> MenuItems { get; } = new ObservableCollection<object>();
		
		/// <summary>
		/// The <see cref="ICommand"/> that executes before the button's popup is opened, allowing its items to be customized in MVVM scenarios.
		/// </summary>
		public ICommand? PopupOpeningCommand {
			get => _popupOpeningCommand;
			set => SetProperty(ref _popupOpeningCommand, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.SmallIcon"/>
		public object? SmallIcon {
			get => _smallIcon;
			set => SetProperty(ref _smallIcon, value);
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

		/// <inheritdoc cref="BarButtonViewModel.ToolBarItemVariantBehavior"/>
		public ItemVariantBehavior ToolBarItemVariantBehavior {
			get => _toolBarItemVariantBehavior;
			set => SetProperty(ref _toolBarItemVariantBehavior, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.UseLargeMenuItem"/>
		public bool UseLargeMenuItem {
			get => _useLargeMenuItem;
			set => SetProperty(ref _useLargeMenuItem, value);
		}

	}

}
