namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a separator control within a ribbon backstage header.
	/// </summary>
	public class RibbonBackstageHeaderSeparatorViewModel : ObservableObjectBase, IHasTag {

		private RibbonBackstageHeaderAlignment _headerAlignment = RibbonBackstageHeaderAlignment.Top;
		private bool _isVisible = true;
		private object? _tag;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public RibbonBackstageHeaderSeparatorViewModel()  // Parameterless constructor required for XAML support
			: this(RibbonBackstageHeaderAlignment.Top) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified header alignment.
		/// </summary>
		/// <param name="headerAlignment">A <see cref="RibbonBackstageHeaderAlignment"/> indicating the alignment of the control within the ribbon Backstage header.</param>
		public RibbonBackstageHeaderSeparatorViewModel(RibbonBackstageHeaderAlignment headerAlignment) {
			_headerAlignment = headerAlignment;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object? Tag {
			get => _tag;
			set => SetProperty(ref _tag, value);
		}

	}

}

