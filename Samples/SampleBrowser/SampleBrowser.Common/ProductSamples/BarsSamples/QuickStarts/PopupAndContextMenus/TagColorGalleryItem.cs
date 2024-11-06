using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.PopupAndContextMenus {

	/// <summary>
	/// Represents a tag color for a gallery item used by the "View Options with Color Tagging" showcase sample.
	/// </summary>
	public class TagColorGalleryItem : ColorBarGalleryItemViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="TagColorGalleryItem"/> class.
		/// </summary>
		/// <param name="value">The color associated with the tag.</param>
		/// <param name="label">The label associated with the tag.</param>
		public TagColorGalleryItem(UIColor value, string label)
			: base(value.ToRgb(), category: null, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the default collection of <see cref="TagColorGalleryItem"/> instances.
		/// </summary>
		/// <returns>An array of type <see cref="TagColorGalleryItem"/>.</returns>
		public static TagColorGalleryItem[] CreateDefaultCollection() {
			return new TagColorGalleryItem[] {
				new TagColorGalleryItem(UIColor.Parse("#f04f58"), "Red"),
				new TagColorGalleryItem(UIColor.Parse("#f1a247"), "Orange"),
				new TagColorGalleryItem(UIColor.Parse("#f3cf4a"), "Yellow"),
				new TagColorGalleryItem(UIColor.Parse("#5dd260"), "Green"),
				new TagColorGalleryItem(UIColor.Parse("#5c85f5"), "Blue"),
				new TagColorGalleryItem(UIColor.Parse("#b163d3"), "Purple"),
				new TagColorGalleryItem(UIColor.Parse("#9c9ca0"), "Gray"),
			};
		}

	}

}
