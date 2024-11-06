using ActiproSoftware.Properties.Bars.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a font family.
	/// </summary>
	public class FontFamilyBarGalleryItemViewModel : BarGalleryItemViewModel<FontFamily> {

		private static ReadOnlyCollection<FontFamilyBarGalleryItemViewModel>? _cachedDefaultCollection;
		private static string? _defaultFontFamilyName;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a default font family name and category.
		/// </summary>
		public FontFamilyBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(DefaultFontFamilyName) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified name and a default category.
		/// </summary>
		/// <param name="fontFamilyName">The font family name.</param>
		public FontFamilyBarGalleryItemViewModel(string? fontFamilyName)
			: this(fontFamilyName, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified name and category.
		/// </summary>
		/// <param name="fontFamilyName">The font family name.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public FontFamilyBarGalleryItemViewModel(string? fontFamilyName, string? category)
			: this(fontFamilyName, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified name, category, and label.
		/// </summary>
		/// <param name="fontFamilyName">The font family name.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public FontFamilyBarGalleryItemViewModel(string? fontFamilyName, string? category, string? label)
			: this(new FontFamily(ValidateFontFamilyName(fontFamilyName, nameof(fontFamilyName))), category, label) { }
		
		/// <summary>
		/// Initializes a new instance of the class with the specified <see cref="FontFamily"/> and a default category.
		/// </summary>
		/// <param name="value">The <see cref="FontFamily"/> value.</param>
		public FontFamilyBarGalleryItemViewModel(FontFamily value)
			: this(value, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified <see cref="FontFamily"/> and category.
		/// </summary>
		/// <param name="value">The <see cref="FontFamily"/> value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public FontFamilyBarGalleryItemViewModel(FontFamily value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified <see cref="FontFamily"/>, category, and label.
		/// </summary>
		/// <param name="value">The <see cref="FontFamily"/> value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public FontFamilyBarGalleryItemViewModel(FontFamily value, string? category, string? label)
			: base(value, category, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Tests if the given font family name is valid and throws <see cref="ArgumentException"/> if invalid.
		/// </summary>
		/// <param name="name">The value to be tested.</param>
		/// <param name="paramName">The name of the parameter which defined the value.</param>
		/// <exception cref="ArgumentException">Throw if the given name is not valid.</exception>
		/// <returns>Returns the font family name if an exception is not thrown.</returns>
		private static string ValidateFontFamilyName(string? name, string paramName) {
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentException("Font family name cannot be null, empty, or filled with white space.", paramName);

			return name;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override string? CoerceLabel() {
			// Make sure any FontFamily.Key is not included in the displayed font name
			return Value?.Name;
		}

		/// <summary>
		/// Creates a default collection of gallery item view models representing the installed font families.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<FontFamilyBarGalleryItemViewModel> CreateDefaultCollection() {
			// Reading font data is a performance-sensitive operation, so the initial results will be
			// cached and re-used on subsequent calls to this method.
			if (_cachedDefaultCollection is null) {

				var list = new List<FontFamilyBarGalleryItemViewModel>();
				var allFontsCategory = SR.GetString(SRName.UIGalleryItemCategoryAllFontsText);

				foreach (var fontFamily in FontManager.Current.SystemFonts)
					list.Add(new FontFamilyBarGalleryItemViewModel(fontFamily.Name, allFontsCategory));

				// Final, combined list of view models should be pre-sorted by label and wrapped as read-only
				_cachedDefaultCollection = list.OrderBy(x => x.Label).ToList().AsReadOnly();
			}

			return _cachedDefaultCollection
				?? Enumerable.Empty<FontFamilyBarGalleryItemViewModel>();
		}
		
		/// <summary>
		/// The localizable default category to be used for view models of this type.
		/// </summary>
		public static string DefaultCategory
			=> SR.GetString(SRName.UIGalleryItemCategoryAllFontsText) ?? "All Fonts";

		/// <summary>
		/// The default font family name.
		/// </summary>
		internal static string DefaultFontFamilyName
			=> (_defaultFontFamilyName ??= FontManager.Current.DefaultFontFamily.Name);

	}

}
