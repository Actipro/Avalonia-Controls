using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a size selection gallery control within a bar control.
	/// </summary>
	public class BarSizeSelectionMenuGalleryViewModel : BarGalleryViewModelBase {

		private string? _defaultHeadingText;
		private int _menuColumnCount = 10;
		private int _menuRowCount = 8;
		private string? _sizeHeadingTextFormat;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarSizeSelectionMenuGalleryViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarSizeSelectionMenuGalleryViewModel(string? key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarSizeSelectionMenuGalleryViewModel(string? key, string? label)
			: this(key, label, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarSizeSelectionMenuGalleryViewModel(string? key, ICommand? command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarSizeSelectionMenuGalleryViewModel(string? key, string? label, ICommand? command)
			: base(key, label, command) {

			ItemSpacing = 1.0;
			MinItemHeight = 20.0;
			MinItemWidth = 20.0;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The default heading text that is displayed above the menu gallery.
		/// </summary>
		public string? DefaultHeadingText {
			get => _defaultHeadingText;
			set => SetProperty(ref _defaultHeadingText, value);
		}
		
		/// <summary>
		/// The number of columns when in a menu.
		/// </summary>
		/// <value>
		/// The default value is <c>10</c>.
		/// </value>
		public int MenuColumnCount {
			get => _menuColumnCount ;
			set => SetProperty(ref _menuColumnCount, value);
		}
		
		/// <summary>
		/// The number of rows when in a menu.
		/// </summary>
		/// <value>
		/// The default value is <c>8</c>.
		/// </value>
		public int MenuRowCount {
			get => _menuRowCount ;
			set => SetProperty(ref _menuRowCount, value);
		}
		
		/// <summary>
		/// The format string to use for the heading when the end user is selecting a size in the gallery.
		/// </summary>
		/// <remarks>
		/// The first parameter to the format string is the current width.
		/// The second parameter to the format string is the current height.
		/// </remarks>
		public string? SizeHeadingTextFormat {
			get => _sizeHeadingTextFormat;
			set => SetProperty(ref _sizeHeadingTextFormat, value);
		}
		
	}

}
