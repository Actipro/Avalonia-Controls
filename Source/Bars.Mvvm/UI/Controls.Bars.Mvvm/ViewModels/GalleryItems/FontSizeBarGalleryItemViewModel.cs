using ActiproSoftware.Properties.Bars.Mvvm;
using System.Collections.Generic;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a font size.
	/// </summary>
	public class FontSizeBarGalleryItemViewModel : BarGalleryItemViewModel<double> {

		internal const double DefaultFontSize = 12.0;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a default font size.
		/// </summary>
		public FontSizeBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(DefaultFontSize) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified size and a default category.
		/// </summary>
		/// <param name="value">The font size.</param>
		public FontSizeBarGalleryItemViewModel(double value)
			: this(value, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified size and category.
		/// </summary>
		/// <param name="value">The font size.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public FontSizeBarGalleryItemViewModel(double value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified size, category, and label.
		/// </summary>
		/// <param name="value">The font size.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public FontSizeBarGalleryItemViewModel(double value, string? category, string? label)
			: base(ValidateFontSize(value, nameof(value)), category, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Tests if the given font size is valid and throws <see cref="ArgumentOutOfRangeException"/> if invalid.
		/// </summary>
		/// <param name="size">The value to be tested.</param>
		/// <param name="paramName">The name of the parameter which defined the value.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if the given font size is not valid.</exception>
		/// <returns>Returns the font size if an exception is not thrown.</returns>
		private static double ValidateFontSize(double size, string paramName) {
			if (size < 1.0)
				throw new ArgumentOutOfRangeException(paramName, size, "Font size must be 1.0 or greater.");

			return size;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing common font sizes.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<FontSizeBarGalleryItemViewModel> CreateDefaultCollection() {
			return new FontSizeBarGalleryItemViewModel[] {
				new(8.0),
				new(9.0),
				new(10.0),
				new(11.0),
				new(12.0),
				new(14.0),
				new(16.0),
				new(18.0),
				new(20.0),
				new(22.0),
				new(24.0),
				new(26.0),
				new(28.0),
				new(36.0),
				new(48.0),
				new(72.0),
			};
		}

		/// <summary>
		/// The localizable default category to be used for view models of this type.
		/// </summary>
		public static string DefaultCategory
			=> SR.GetString(SRName.UIGalleryItemCategoryFontSizesText) ?? "Font Sizes";

		/// <inheritdoc/>
		public override bool IsLabelVisible
			=> true;

		/// <inheritdoc/>
		public override double Value {
			get => base.Value;
			set => base.Value = ValidateFontSize(value, nameof(value));
		}

	}

}
