using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a tab control within a ribbon.
	/// </summary>
	public class RibbonTabViewModel : BarKeyedObjectViewModelBase {

		private string? _contextualTabGroupKey;
		private VariantCollection? _controlVariants;
		private string? _description;
		private VariantCollection? _groupVariants;
		private bool _isVisible = true;
		private string? _keyTipText;
		private string? _label;
		private string? _title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public RibbonTabViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public RibbonTabViewModel(string? key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public RibbonTabViewModel(string? key, string? label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public RibbonTabViewModel(string? key, string? label, string? keyTipText)
			: base(key) {
			_label = label ?? BarControlService.LabelGenerator.FromKey(key);
			_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromLabel(_label);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The string key of the related contextual tab group, if this should be a contextual tab.
		/// </summary>
		public string? ContextualTabGroupKey {
			get => _contextualTabGroupKey;
			set => SetProperty(ref _contextualTabGroupKey, value);
		}
		
		/// <summary>
		/// The collection of variant size transitions to apply to all controls within the tab when the ribbon is in Simplified layout mode.
		/// </summary>
		public VariantCollection? ControlVariants {
			get => _controlVariants;
			set => SetProperty(ref _controlVariants, value);
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Description"/>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}
		
		/// <summary>
		/// The collection of group view models within the tab.
		/// </summary>
		public ObservableCollection<RibbonGroupViewModel> Groups { get; } = new();

		/// <summary>
		/// The collection of variant size transitions to apply to all groups within the tab when the ribbon is in Classic layout mode.
		/// </summary>
		public VariantCollection? GroupVariants {
			get => _groupVariants;
			set => SetProperty(ref _groupVariants, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		/// <remarks>
		/// This property should not be set to control the visibility of contextual tabs.
		/// </remarks>
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
