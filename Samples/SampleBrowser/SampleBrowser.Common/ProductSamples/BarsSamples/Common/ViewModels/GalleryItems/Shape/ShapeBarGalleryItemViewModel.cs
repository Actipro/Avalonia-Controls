using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a shape as a gallery item view model for a bar gallery control.
	/// </summary>
	public class ShapeBarGalleryItemViewModel : BarGalleryItemViewModel<ShapeKind> {

		/// <summary>
		/// The name of the category for basic shapes.
		/// </summary>
		public const string BasicShapesCategory = "Basic Shapes";

		/// <summary>
		/// The name of the category for line shapes.
		/// </summary>
		public const string LinesCategory = "Lines";

		/// <summary>
		/// The name of the category for rectangle shapes.
		/// </summary>
		public const string RectanglesCategory = "Rectangles";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a rectangular shape.
		/// </summary>
		public ShapeBarGalleryItemViewModel()
			: this(value: ShapeKind.Rectangle) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value.
		/// </summary>
		/// <param name="value">The item's value.</param>
		public ShapeBarGalleryItemViewModel(ShapeKind value)
			: this(value, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and category.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public ShapeBarGalleryItemViewModel(ShapeKind value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value, category, and label.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public ShapeBarGalleryItemViewModel(ShapeKind value, string? category, string? label)
			: base(value, category, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing shape kinds.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<ShapeBarGalleryItemViewModel> CreateDefaultCollection() {
			return new ShapeBarGalleryItemViewModel[] {
				// Lines
				new ShapeBarGalleryItemViewModel(ShapeKind.Line, LinesCategory),
				new ShapeBarGalleryItemViewModel(ShapeKind.LineArrow, LinesCategory),
				new ShapeBarGalleryItemViewModel(ShapeKind.LineArrowDouble, LinesCategory),

				// Rectangles
				new ShapeBarGalleryItemViewModel(ShapeKind.Rectangle, RectanglesCategory),
				new ShapeBarGalleryItemViewModel(ShapeKind.RectangleRoundedCorners, RectanglesCategory),
				new ShapeBarGalleryItemViewModel(ShapeKind.RectangleSingleCornerSnipped, RectanglesCategory),

				// Basic Shapes
				new ShapeBarGalleryItemViewModel(ShapeKind.Oval, BasicShapesCategory),
				new ShapeBarGalleryItemViewModel(ShapeKind.RightTriangle, BasicShapesCategory),
			};
		}
		
		/// <summary>
		/// Creates a <see cref="ICollectionView"/> of gallery item view models representing shape kinds, allowing for possible categorization and filtering.
		/// </summary>
		/// <param name="categorize">Whether the collection view should support categorization by including a group description based on <see cref="IBarGalleryItemViewModel.Category"/> property values.</param>
		/// <returns>The <see cref="ICollectionView"/> of gallery item view models that was created.</returns>
		public static ICollectionView CreateDefaultCollectionView(bool categorize)
			=> BarGalleryViewModel.CreateCollectionView(CreateDefaultCollection(), categorize);

	}

}
