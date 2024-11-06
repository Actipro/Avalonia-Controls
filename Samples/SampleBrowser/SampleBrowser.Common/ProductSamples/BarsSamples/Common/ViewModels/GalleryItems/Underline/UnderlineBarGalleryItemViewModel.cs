using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Represents an underline style as a gallery item view model for a bar gallery control.
	/// </summary>
	public class UnderlineBarGalleryItemViewModel : BarGalleryItemViewModel<UnderlineKind> {

		/// <summary>
		/// The name of the category for underlines.
		/// </summary>
		public const string DefaultCategory = "Underline";
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a default category.
		/// </summary>
		public UnderlineBarGalleryItemViewModel()
			: this(value: default) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and a default category.
		/// </summary>
		/// <param name="value">The item's value.</param>
		public UnderlineBarGalleryItemViewModel(UnderlineKind value)
			: this(value, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and category.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public UnderlineBarGalleryItemViewModel(UnderlineKind value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value, category, and label.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public UnderlineBarGalleryItemViewModel(UnderlineKind value, string? category, string? label)
			: base(value, category, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing underline kinds.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<UnderlineBarGalleryItemViewModel> CreateDefaultCollection() {
			return new UnderlineBarGalleryItemViewModel[] {
				new UnderlineBarGalleryItemViewModel(UnderlineKind.Underline),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.DoubleUnderline),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.ThickUnderline),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.DashedUnderline),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.DotDashUnderline),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.DotDotDashUnderline),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.WaveUnderline),
				new UnderlineBarGalleryItemViewModel(UnderlineKind.None) { Label = "No Underline" },
			};
		}
		
		/// <summary>
		/// Creates an <see cref="ICollectionView"/> of gallery item view models representing underline kinds, allowing for possible categorization and filtering.
		/// </summary>
		/// <param name="categorize">Whether the collection view should support categorization by including a group description based on <see cref="IBarGalleryItemViewModel.Category"/> property values.</param>
		/// <returns>The <see cref="ICollectionView"/> of gallery item view models that was created.</returns>
		public static ICollectionView CreateDefaultCollectionView(bool categorize)
			=> BarGalleryViewModel.CreateCollectionView(CreateDefaultCollection(), categorize);

	}

}
