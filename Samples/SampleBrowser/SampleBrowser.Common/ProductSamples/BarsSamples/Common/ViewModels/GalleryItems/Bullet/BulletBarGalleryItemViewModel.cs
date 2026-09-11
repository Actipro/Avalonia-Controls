using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a bullet style as a gallery item view model for a bar gallery control.
/// </summary>
public class BulletBarGalleryItemViewModel : BarGalleryItemViewModel<BulletKind> {

	/// <summary>
	/// The name of the category for bullets.
	/// </summary>
	public const string DefaultCategory = "Bullet Library";

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class with a default value and category.
	/// </summary>
	public BulletBarGalleryItemViewModel()
		: this(value: default) { }

	/// <summary>
	/// Initializes an instance of the class with the specified value and a default category.
	/// </summary>
	/// <param name="value">The item's value.</param>
	public BulletBarGalleryItemViewModel(BulletKind value)
		: this(value, DefaultCategory) { }

	/// <summary>
	/// Initializes an instance of the class with the specified value and category
	/// </summary>
	/// <param name="value">The item's value.</param>
	/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
	public BulletBarGalleryItemViewModel(BulletKind value, string? category)
		: this(value, category, label: null) { }

	/// <summary>
	/// Initializes an instance of the class with the specified value, category, and label.
	/// </summary>
	/// <param name="value">The item's value.</param>
	/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
	/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
	public BulletBarGalleryItemViewModel(BulletKind value, string? category, string? label)
		: base(value, category, label) { }

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates a default collection of gallery item view models representing bullet kinds.
	/// </summary>
	public static IEnumerable<BulletBarGalleryItemViewModel> CreateDefaultCollection() {
		return [
			new(BulletKind.None),
			new(BulletKind.Circle),
			new(BulletKind.FilledCircle),
			new(BulletKind.Square),
			new(BulletKind.FilledSquare),
		];
	}

	/// <summary>
	/// Creates a <see cref="ICollectionView"/> of gallery item view models representing bullet kinds, allowing for possible categorization and filtering.
	/// </summary>
	/// <param name="categorize">Whether the collection view should support categorization by including a group description based on <see cref="IBarGalleryItemViewModel.Category"/> property values.</param>
	public static ICollectionView CreateDefaultCollectionView(bool categorize)
		=> BarGalleryViewModel.CreateCollectionView(CreateDefaultCollection(), categorize);

}
