using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures.ViewModels;

/// <summary>
/// Represents the image document view-model.
/// </summary>
public class ImageDocumentItemViewModel : DocumentItemViewModel {

	private IImage? _image;
	private string? _imageRelativePath;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public ImageDocumentItemViewModel() {
		Description = "Image document";
		Icon = ImageLoader.GetIcon("Picture16.png");
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void InvalidateImage() {
		_image = null;
		OnPropertyChanged(nameof(Image));
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	public IImage? Image {
		get => _image ??= (ImageRelativePath is { } relPath)
			? ImageLoader.GetProfilePhoto(relPath)
			: null;
	}

	/// <summary>
	/// The relative path of the image associated with the view-model.
	/// </summary>
	public string? ImageRelativePath {
		get => _imageRelativePath;
		set {
			if (SetProperty(ref _imageRelativePath, value)) {
				// Property changed, so invalidate any image generated from the old path
				InvalidateImage();
			}
		}
	}

}
