using Avalonia.Controls.Templates;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a tab control within a ribbon backstage.
	/// </summary>
	public class RibbonBackstageTabViewModel : BarKeyedObjectViewModelBase {

		private object? _content;
		private IDataTemplate? _contentTemplate;
		private string? _description;
		private RibbonBackstageHeaderAlignment _headerAlignment = RibbonBackstageHeaderAlignment.Top;
		private bool _isVisible = true;
		private string? _keyTipText;
		private string? _label;
		private object? _largeIcon;
		private object? _smallIcon;
		private string? _title;
		private VariantSize _variantSize = VariantSize.Medium;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public RibbonBackstageTabViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public RibbonBackstageTabViewModel(string? key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public RibbonBackstageTabViewModel(string? key, string? label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public RibbonBackstageTabViewModel(string? key, string? label, string? keyTipText)
			: base(key) {

			// It is critical that the content of this view model is initialized to
			// itself so that the IDataTemplate assigned to RibbonBackstage.ContentTemplate
			// can use the view model's data type to select the appropriate template.  If
			// an IDataTemplate is not defined, the view model's ToString() output will be
			// displayed as an indicator that a template is needed.
			Content = this;

			_label = label ?? BarControlService.LabelGenerator.FromKey(key);
			_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromLabel(this._label);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// The content for the tab's content area, which can be a UI control, a data object, or even this view model instance.
		/// </summary>
		public object? Content {
			get => _content;
			set => SetProperty(ref _content, value);
		}
		
		/// <summary>
		/// The <see cref="IDataTemplate"/> for the tab's <see cref="Content"/>.
		/// </summary>
		public IDataTemplate? ContentTemplate {
			get => _contentTemplate;
			set => SetProperty(ref _contentTemplate, value);
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Description"/>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}
		
		/// <summary>
		/// A <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.
		/// </summary>
		/// <value>
		/// The default value is <see cref="RibbonBackstageHeaderAlignment.Top"/>.
		/// </value>
		public RibbonBackstageHeaderAlignment HeaderAlignment {
			get => _headerAlignment;
			set => SetProperty(ref _headerAlignment, value);
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

		/// <summary>
		/// The variant size of the tab.
		/// </summary>
		/// <value>
		/// The default value is <see cref="VariantSize.Medium"/>.
		/// </value>
		public VariantSize VariantSize {
			get => _variantSize;
			set => SetProperty(ref _variantSize, value);
		}

	}

}
