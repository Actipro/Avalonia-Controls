using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;
using Avalonia.Media;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.PopupAndContextMenus {

	/// <summary>
	/// Represents a paste option for a gallery item used by the "Advanced Paste Options" showcase sample.
	/// </summary>
	public class PasteOptionGalleryItem : BarGalleryItemViewModel<PasteSpecialKind> {

		private IImage? _image;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="PasteOptionGalleryItem"/> class.
		/// </summary>
		/// <param name="kind">The kind of special paste operation represented by the gallery item.</param>
		public PasteOptionGalleryItem(PasteSpecialKind kind)
			: base(kind, category: "Paste Options:") {

			// NOTE: The base gallery item category is used to display the category name above the paste options
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the default <see cref="ICollectionView"/> of <see cref="PasteOptionGalleryItem"/> instances.
		/// </summary>
		public static ICollectionView CreateDefaultCollectionView() {
			// NOTE: An ICollectionView is necessary to support the display of categories
			return BarGalleryViewModel.CreateCollectionView(new [] {
				new PasteOptionGalleryItem(PasteSpecialKind.MergeFormatting) { Label = "Merge Formatting", KeyTipText = "M", Image = ImageLoader.GetIcon("PasteGalleryMerge24.png") },
				new PasteOptionGalleryItem(PasteSpecialKind.TextOnly) { Label = "Keep Text Only", KeyTipText = "T", Image = ImageLoader.GetIcon("PasteGalleryTextOnly24.png") },
				new PasteOptionGalleryItem(PasteSpecialKind.Picture) { Label = "Picture", KeyTipText = "U", Image = ImageLoader.GetIcon("PasteGalleryPicture24.png") },
			}, categorize: true);
		}

		/// <summary>
		/// The image for this paste option.
		/// </summary>
		public IImage? Image {
			get => _image;
			set => SetProperty(ref _image, value);
		}

	}

}
