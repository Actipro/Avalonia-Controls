namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a contextual tab group control within a ribbon.
	/// </summary>
	public class RibbonContextualTabGroupViewModel : BarKeyedObjectViewModelBase {

		private bool _isActive;
		private string? _label;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public RibbonContextualTabGroupViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key.  The label is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public RibbonContextualTabGroupViewModel(string? key)
			: this(key, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and label.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		public RibbonContextualTabGroupViewModel(string? key, string? label)
			: base(key) {

			_label = label ?? BarControlService.LabelGenerator.FromKey(key);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indicates whether the contextual tab group is active and showing its tabs.
		/// </summary>
		/// <value>
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsActive {
			get => _isActive;
			set => SetProperty(ref _isActive, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string? Label {
			get => _label;
			set => SetProperty(ref _label, value);
		}

	}

}
