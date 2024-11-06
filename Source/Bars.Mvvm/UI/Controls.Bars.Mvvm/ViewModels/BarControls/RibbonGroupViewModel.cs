using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a group within a ribbon tab.
	/// </summary>
	public class RibbonGroupViewModel : BarKeyedObjectViewModelBase {

		private bool _canAutoCollapse = true;
		private bool _canCloneToRibbonQuickAccessToolBar = true;
		private RibbonGroupChildOverflowTarget _childOverflowTarget = RibbonGroupChildOverflowTarget.Tab;
		private string? _collapsedButtonDescription;
		private string? _collapsedButtonKeyTipText;
		private bool _isVisible = true;
		private string? _label;
		private object? _largeIcon;
		private RibbonGroupLauncherButtonViewModel? _launcherButton;
		private object? _smallIcon;
		private string? _title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public RibbonGroupViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label and collapsed button key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public RibbonGroupViewModel(string? key)
			: this(key, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.  The collapsed button key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		public RibbonGroupViewModel(string? key, string? label)
			: this(key, label, collapsedButtonKeyTipText: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and key tip text.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="collapsedButtonKeyTipText">The collapsed button key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
		public RibbonGroupViewModel(string? key, string? label, string? collapsedButtonKeyTipText)
			: base(key) {

			_label = label ?? BarControlService.LabelGenerator.FromKey(key);

			// By convention, the key tips for a collapsed RibbonGroup should start with "Z"
			if (collapsedButtonKeyTipText is null) {
				collapsedButtonKeyTipText = BarControlService.KeyTipTextGenerator.FromLabel(_label);
				if (!string.IsNullOrWhiteSpace(collapsedButtonKeyTipText))
					collapsedButtonKeyTipText = "Z" + collapsedButtonKeyTipText;
			}
			_collapsedButtonKeyTipText = collapsedButtonKeyTipText;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indicates whether the group can auto-collapse.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanAutoCollapse {
			get => _canAutoCollapse;
			set => SetProperty(ref _canAutoCollapse, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.CanCloneToRibbonQuickAccessToolBar"/>
		public bool CanCloneToRibbonQuickAccessToolBar {
			get => _canCloneToRibbonQuickAccessToolBar;
			set => SetProperty(ref _canCloneToRibbonQuickAccessToolBar, value);
		}

		/// <summary>
		/// A <see cref="RibbonGroupChildOverflowTarget"/> indicating where items overflow when in a Simplified layout mode.
		/// </summary>
		/// <value>
		/// The default value is <see cref="RibbonGroupChildOverflowTarget.Tab"/>.
		/// </value>
		public RibbonGroupChildOverflowTarget ChildOverflowTarget {
			get => _childOverflowTarget;
			set => SetProperty(ref _childOverflowTarget, value);
		}
		
		/// <summary>
		/// The text description to display in screen tips for the group when it is rendered as a collapsed button.
		/// </summary>
		public string? CollapsedButtonDescription {
			get => _collapsedButtonDescription;
			set => SetProperty(ref _collapsedButtonDescription, value);
		}
		
		/// <summary>
		/// The key tip text used to access the group when it is rendered as a collapsed button.
		/// </summary>
		public string? CollapsedButtonKeyTipText {
			get => _collapsedButtonKeyTipText;
			set => SetProperty(ref _collapsedButtonKeyTipText, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <summary>
		/// The collection of items in the control.
		/// </summary>
		public ObservableCollection<object> Items { get; } = new ();

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

		/// <summary>
		/// A <see cref="RibbonGroupLauncherButtonViewModel"/> for the optional launcher button.
		/// </summary>
		public RibbonGroupLauncherButtonViewModel? LauncherButton {
			get => _launcherButton;
			set => SetProperty(ref _launcherButton, value);
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

		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[Label='{this.Label}']";

	}

}
