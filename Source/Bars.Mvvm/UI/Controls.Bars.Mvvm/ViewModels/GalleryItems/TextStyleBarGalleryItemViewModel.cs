using ActiproSoftware.Properties.Bars.Mvvm;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a text style (font, color, weight, etc.).
	/// </summary>
	public class TextStyleBarGalleryItemViewModel : BarGalleryItemViewModel<TextStyle> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a default text style and category.
		/// </summary>
		public TextStyleBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(default, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified text style and default category.
		/// </summary>
		/// <param name="value">The text style.</param>
		public TextStyleBarGalleryItemViewModel(TextStyle? value)
			: this(value, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified text style and category.
		/// </summary>
		/// <param name="value">The text style.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public TextStyleBarGalleryItemViewModel(TextStyle? value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified text style, category, and label.
		/// </summary>
		/// <param name="value">The text style.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public TextStyleBarGalleryItemViewModel(TextStyle? value, string? category, string? label) 
			: base(value, category, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The localizable default category to be used for view models of this type.
		/// </summary>
		public static string DefaultCategory
			=> SR.GetString(SRName.UIGalleryItemCategoryTextStylesText) ?? "Text Styles";

	}

}
