using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Represents a gallery item view model for a border style that will appear as menu item, only intended for use within a menu gallery.
	/// </summary>
	public class BorderBarGalleryItemViewModel : BarGalleryItemViewModel<BorderKind> {

		/// <summary>
		/// The name of the category for edge borders.
		/// </summary>
		public const string EdgeBordersCategory = "Edge Borders";

		/// <summary>
		/// The name of the category for other borders.
		/// </summary>
		public const string OtherBordersCategory = "Other Borders";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with no border.
		/// </summary>
		public BorderBarGalleryItemViewModel()
			: this(value: default) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value.
		/// </summary>
		/// <param name="value">The item's value.</param>
		public BorderBarGalleryItemViewModel(BorderKind value)
			: this(value, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and category.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public BorderBarGalleryItemViewModel(BorderKind value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value, category, and label.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public BorderBarGalleryItemViewModel(BorderKind value, string? category, string? label)
			: base(value, category, label) { }

	}

}
