using ActiproSoftware.UI.Avalonia.Controls.Templates;
using Avalonia.Controls.Templates;
using Avalonia.Styling;
using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents an abstract view model base for a gallery control within a bar control.
	/// </summary>
	public abstract class BarGalleryViewModelBase : BarKeyedObjectViewModelBase {

		private bool _areSurroundingSeparatorsAllowed = true;
		private bool _canCloneToRibbonQuickAccessToolBar = true;
		private ICommand? _command;
		private ControlTheme? _itemContainerTheme;
		private bool _isVisible = true;
		private double _itemSpacing;
		private IDataTemplate? _itemTemplate;
		private IDataTemplateSelector? _itemTemplateSelector;
		private string? _label;
		private double _minItemHeight = 16.0;
		private double _minItemWidth = 16.0;
		private object? _smallIcon;
		private string? _title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		protected BarGalleryViewModelBase()  // Parameterless constructor required for XAML support
			: this(key: null) { }
		
		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		protected BarGalleryViewModelBase(string? key)
			: base(key) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		protected BarGalleryViewModelBase(string? key, string? label, ICommand? command)
			: base(key) {

			_label = label ?? BarControlService.LabelGenerator.FromCommand(command) ?? BarControlService.LabelGenerator.FromKey(key);
			_command = command;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indicates whether the menu gallery can render surrounding separators.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool AreSurroundingSeparatorsAllowed {
			get => _areSurroundingSeparatorsAllowed;
			set => SetProperty(ref _areSurroundingSeparatorsAllowed, value);
		}

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

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <summary>
		/// The <see cref="ControlTheme"/> to apply to gallery item container elements.
		/// </summary>
		public ControlTheme? ItemContainerTheme {
			get => _itemContainerTheme;
			set => SetProperty(ref _itemContainerTheme, value);
		}

		/// <summary>
		/// The amount of spacing between gallery items.
		/// </summary>
		/// <value>
		/// The default value is <c>0.0</c>.
		/// </value>
		public double ItemSpacing {
			get => _itemSpacing;
			set => SetProperty(ref _itemSpacing, value);
		}
		
		/// <summary>
		/// The <see cref="IDataTemplate"/> used to display the content for each gallery item.
		/// </summary>
		public IDataTemplate? ItemTemplate {
			get => _itemTemplate;
			set => SetProperty(ref _itemTemplate, value);
		}

		/// <summary>
		/// The <see cref="IDataTemplateSelector"/> that picks an <see cref="IDataTemplate"/> used to display the content for each gallery item.
		/// </summary>
		public IDataTemplateSelector? ItemTemplateSelector {
			get => _itemTemplateSelector;
			set => SetProperty(ref _itemTemplateSelector, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string? Label {
			get => _label;
			set => SetProperty(ref _label, value);
		}
		
		/// <summary>
		/// The minimum item height.
		/// </summary>
		/// <value>
		/// The default value is <c>16.0</c>.
		/// </value>
		public double MinItemHeight {
			get => _minItemHeight;
			set => SetProperty(ref _minItemHeight, value);
		}
		
		/// <summary>
		/// The minimum item width.
		/// </summary>
		/// <value>
		/// The default value is <c>16.0</c>.
		/// </value>
		public double MinItemWidth {
			get => _minItemWidth;
			set => SetProperty(ref _minItemWidth, value);
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

	}

}
