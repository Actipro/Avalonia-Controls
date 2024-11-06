using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls.Templates;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a ribbon control.
	/// </summary>
	public class RibbonViewModel : ObservableObjectBase {

		private bool _allowLabelsOnQuickAccessToolBar;
		private RibbonApplicationButtonViewModel? _applicationButton;
		private bool _areTabsVisible = true;
		private RibbonBackstageViewModel? _backstage;
		private bool _canChangeLayoutMode = true;
		private ICommand? _clearFooterCommand;
		private RibbonFooterViewModel? _footer;
		private RibbonGroupLabelMode _groupLabelMode = RibbonGroupLabelMode.Default;
		private bool _isApplicationButtonVisible = true;
		private bool _isCollapsible = true;
		private bool _isMinimizable = true;
		private BarControlTemplateSelector _itemContainerTemplateSelector = new();
		private RibbonLayoutMode _layoutMode = RibbonLayoutMode.Classic;
		private RibbonQuickAccessToolBarViewModel? _quickAccessToolBar;
		private RibbonQuickAccessToolBarLocation _quickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Above;
		private RibbonQuickAccessToolBarMode _quickAccessToolBarMode = RibbonQuickAccessToolBarMode.Visible;
		private RibbonTabRowToolBarViewModel? _tabRowToolBar;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class in Classic layout mode.
		/// </summary>
		public RibbonViewModel()
			: this(RibbonLayoutMode.Classic) { }

		/// <summary>
		/// Initializes a new instance of the class in the specified layout mode.
		/// </summary>
		public RibbonViewModel(RibbonLayoutMode layoutMode) {
			// Initialize the layout mode
			_layoutMode = layoutMode;


			// Initialize the clear footer command
			_clearFooterCommand = new DelegateCommand<object>(_ => this.Footer = null);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Whether the quick-access toolbar allows labels when it is below the ribbon.
		/// </summary>
		/// <value>
		/// The default value is <c>false</c>.
		/// </value>
		public bool AllowLabelsOnQuickAccessToolBar {
			get => _allowLabelsOnQuickAccessToolBar;
			set => SetProperty(ref _allowLabelsOnQuickAccessToolBar, value);
		}

		/// <summary>
		/// A <see cref="RibbonApplicationButtonViewModel"/> for the application button.
		/// </summary>
		public RibbonApplicationButtonViewModel? ApplicationButton {
			get => _applicationButton;
			set => SetProperty(ref _applicationButton, value);
		}
		
		/// <summary>
		/// Whether tabs are visible above the ribbon's main content area.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool AreTabsVisible {
			get => _areTabsVisible;
			set => SetProperty(ref _areTabsVisible, value);
		}

		/// <summary>
		/// A <see cref="RibbonApplicationButtonViewModel"/> for the optional Backstage.
		/// </summary>
		public RibbonBackstageViewModel? Backstage {
			get => _backstage;
			set => SetProperty(ref _backstage, value);
		}

		/// <summary>
		/// Indicates whether the end user can change the layout mode.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanChangeLayoutMode {
			get => _canChangeLayoutMode;
			set => SetProperty(ref _canChangeLayoutMode, value);
		}

		/// <summary>
		/// A command that, when invoked, will clear the footer.
		/// </summary>
		/// <value>
		/// The default value is a command that sets the <see cref="Footer"/> property to <c>null</c>.
		/// </value>
		public ICommand? ClearFooterCommand {
			get => _clearFooterCommand;
			set => SetProperty(ref _clearFooterCommand, value);
		}

		/// <summary>
		/// The collection of optional contextual tab groups within the ribbon.
		/// </summary>
		public ObservableCollection<RibbonContextualTabGroupViewModel> ContextualTabGroups { get; } = new();
		
		/// <summary>
		/// A <see cref="RibbonFooterViewModel"/> for the optional footer.
		/// </summary>
		public RibbonFooterViewModel? Footer {
			get => _footer;
			set => SetProperty(ref _footer, value);
		}
		
		/// <summary>
		/// The <see cref="RibbonGroupLabelMode"/> that specifies when a <see cref="RibbonGroup"/> is labeled.
		/// </summary>
		/// <value>
		/// The default value is <see cref="RibbonGroupLabelMode.Default"/>.
		/// </value>
		public RibbonGroupLabelMode GroupLabelMode {
			get => _groupLabelMode;
			set => SetProperty(ref _groupLabelMode, value);
		}

		/// <summary>
		/// Indicates whether the application button is visible.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsApplicationButtonVisible  {
			get => _isApplicationButtonVisible;
			set => SetProperty(ref _isApplicationButtonVisible, value);
		}
		
		/// <summary>
		/// Indicates whether the ribbon collapses when it becomes smaller than a minimum threshold width/height.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsCollapsible {
			get => _isCollapsible;
			set => SetProperty(ref _isCollapsible, value);
		}
		
		/// <summary>
		/// Indicates whether the ribbon is minimizable.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsMinimizable {
			get => _isMinimizable;
			set => SetProperty(ref _isMinimizable, value);
		}

		/// <summary>
		/// The <see cref="BarControlTemplateSelector"/> that creates UI controls for bar control view models.
		/// </summary>
		public BarControlTemplateSelector ItemContainerTemplateSelector {
			get => _itemContainerTemplateSelector;
			set => SetProperty(ref _itemContainerTemplateSelector, value);
		}

		/// <summary>
		/// A <see cref="RibbonLayoutMode"/> that indicates the current layout mode (Classic vs. Simplified).
		/// </summary>
		/// <value>
		/// The default value is <see cref="RibbonLayoutMode.Classic"/>.
		/// </value>
		public RibbonLayoutMode LayoutMode {
			get => _layoutMode;
			set => SetProperty(ref _layoutMode, value);
		}

		/// <summary>
		/// A <see cref="RibbonQuickAccessToolBarViewModel"/> for the quick-access toolbar.
		/// </summary>
		public RibbonQuickAccessToolBarViewModel? QuickAccessToolBar {
			get => _quickAccessToolBar;
			set => SetProperty(ref _quickAccessToolBar, value);
		}

		/// <summary>
		/// A <see cref="RibbonQuickAccessToolBarLocation"/> that indicates the current location of the quick-access toolbar.
		/// </summary>
		/// <value>
		/// The default value is <see cref="RibbonQuickAccessToolBarLocation.Above"/>.
		/// </value>
		public RibbonQuickAccessToolBarLocation QuickAccessToolBarLocation {
			get => _quickAccessToolBarLocation;
			set => SetProperty(ref _quickAccessToolBarLocation, value);
		}

		/// <summary>
		/// A <see cref="RibbonQuickAccessToolBarMode"/> that indicates the current display mode for the quick-access toolbar.
		/// </summary>
		/// <value>
		/// The default value is <see cref="RibbonQuickAccessToolBarMode.Visible"/>.
		/// </value>
		public RibbonQuickAccessToolBarMode QuickAccessToolBarMode {
			get => _quickAccessToolBarMode;
			set => SetProperty(ref _quickAccessToolBarMode, value);
		}
		
		/// <summary>
		/// A <see cref="RibbonTabRowToolBarViewModel"/> for the tab row toolbar that optionally appears to the right of the tabs.
		/// </summary>
		public RibbonTabRowToolBarViewModel? TabRowToolBar {
			get => _tabRowToolBar;
			set => SetProperty(ref _tabRowToolBar, value);
		}

		/// <summary>
		/// The collection of tabs within the ribbon.
		/// </summary>
		public ObservableCollection<RibbonTabViewModel> Tabs { get; } = new();

		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[{this.Tabs.Count} tabs]";

	}

}
