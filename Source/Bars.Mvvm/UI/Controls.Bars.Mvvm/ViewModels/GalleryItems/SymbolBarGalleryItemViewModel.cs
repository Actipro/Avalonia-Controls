using ActiproSoftware.Properties.Bars.Mvvm;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a symbol.
	/// </summary>
	public class SymbolBarGalleryItemViewModel : BarGalleryItemViewModel<string> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a default category.
		/// </summary>
		public SymbolBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(value: null, DefaultCategory, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified symbol and a default category.
		/// </summary>
		/// <param name="value">The string symbol.</param>
		public SymbolBarGalleryItemViewModel(string? value)
			: this(value, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified symbol and category.
		/// </summary>
		/// <param name="value">The string symbol.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public SymbolBarGalleryItemViewModel(string? value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified symbol, category, and label.
		/// </summary>
		/// <param name="value">The string symbol.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public SymbolBarGalleryItemViewModel(string? value, string? category, string? label)
			: base(value, category, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The localizable default category to be used for view models of this type.
		/// </summary>
		public static string DefaultCategory
			=> SR.GetString(SRName.UIGalleryItemCategorySymbols) ?? "Symbols";

	}

}
