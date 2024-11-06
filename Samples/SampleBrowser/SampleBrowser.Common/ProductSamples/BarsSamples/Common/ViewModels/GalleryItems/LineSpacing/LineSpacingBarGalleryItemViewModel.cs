using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Represents a gallery item view model for a line spacing style that will appear as menu item, only intended for use within a menu gallery.
	/// </summary>
	public class LineSpacingBarGalleryItemViewModel : BarGalleryItemViewModel<double> {

		/// <summary>
		/// The default value for spacing.
		/// </summary>
		public const double DefaultSpacing = 1.2;

		/// <summary>
		/// The name of the category for line spacing.
		/// </summary>
		public const string DefaultCategory = "Line Spacing";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a default value and category.
		/// </summary>
		public LineSpacingBarGalleryItemViewModel()
			: this(DefaultSpacing) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and a default category.
		/// </summary>
		/// <param name="value">The item's value.</param>
		public LineSpacingBarGalleryItemViewModel(double value)
			: this(value, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and category
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public LineSpacingBarGalleryItemViewModel(double value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value, category, and label.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public LineSpacingBarGalleryItemViewModel(double value, string? category, string? label)
			: base(value, category, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override string CoerceLabel() {
			return Value.ToString("0.0");
		}

		/// <summary>
		/// Creates a default collection of gallery item view models representing common line spacing values.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<LineSpacingBarGalleryItemViewModel> CreateDefaultCollection() {
			return new LineSpacingBarGalleryItemViewModel[] {
				new LineSpacingBarGalleryItemViewModel(1.0),
				new LineSpacingBarGalleryItemViewModel(1.2),
				new LineSpacingBarGalleryItemViewModel(1.5),
				new LineSpacingBarGalleryItemViewModel(2.0),
				new LineSpacingBarGalleryItemViewModel(2.5),
				new LineSpacingBarGalleryItemViewModel(3.0),
			};
		}
		
		/// <summary>
		/// Creates a <see cref="ICollectionView"/> of gallery item view models representing common line spacing values, allowing for possible categorization and filtering.
		/// </summary>
		/// <param name="categorize">Whether the collection view should support categorization by including a group description based on <see cref="IBarGalleryItemViewModel.Category"/> property values.</param>
		/// <returns>The <see cref="ICollectionView"/> of gallery item view models that was created.</returns>
		public static ICollectionView CreateDefaultCollectionView(bool categorize)
			=> BarGalleryViewModel.CreateCollectionView(CreateDefaultCollection(), categorize);
		
		/// <inheritdoc/>
		public override bool IsLabelVisible => true;


	}

}
