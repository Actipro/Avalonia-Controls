using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using System;
using System.Windows.Input;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides information about a list item.
	/// </summary>
	public class ListItemInfo : ObservableObjectBase {

		private IImage? _imageSource;
		private Uri? _imageUri;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ListItemInfo() {
			// Listen for changes in the application theme so theme-dependent images can be loaded
			if (Application.Current is not null)
				Application.Current.ActualThemeVariantChanged += this.OnApplicationThemeVariantChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnApplicationThemeVariantChanged(object? sender, EventArgs e) {
			// Only update the image when images are based on a URI since images can also be explicitly set
			if (ImageUri is not null)
				UpdateImageFromUri(ImageUri);
		}

		private void UpdateImageFromUri(Uri? imageUri) {
			if (imageUri is not null) {
				Uri? altUri = null;
				if (Application.Current?.ActualThemeVariant == ThemeVariant.Dark) {
					// Try to load a dark image
					altUri = new Uri(imageUri.AbsoluteUri.Replace(".png", "-dark.png"));
				}

				// Try to load the alternate image
				if (altUri is not null) {
					try {
						this.ImageSource = new Bitmap(AssetLoader.Open(altUri));
						return;
					}
					catch { }
				}

				// Load default image
				this.ImageSource = new Bitmap(AssetLoader.Open(imageUri));
			}
			else
				this.ImageSource = null;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The blurb text, such as <c>NEW!</c>.
		/// </summary>
		public string? BlurbText { get; set; }

		/// <summary>
		/// Whether there is any blurb text.
		/// </summary>
		public bool HasBlurbText
			=> !string.IsNullOrEmpty(this.BlurbText);

		/// <summary>
		/// The <see cref="Uri"/> of the image to display.
		/// </summary>
		public Uri? ImageUri {
			get => _imageUri;
			set {
				if (SetProperty(ref _imageUri, value))
					UpdateImageFromUri(value);
			}
		}

		/// <summary>
		/// The <see cref="IImage"/> to display.
		/// </summary>
		public IImage? ImageSource {
			get => _imageSource;
			set => SetProperty(ref _imageSource, value);
		}

		/// <summary>
		/// Whether the linked item is external.
		/// </summary>
		public bool IsExternal
			=> this.TargetUri?.Query?.Contains("action=open") == true;
		
		/// <summary>
		/// The <see cref="ICommand"/> that navigates the view to a <see cref="ProductItemInfo"/>.
		/// </summary>
		public ICommand NavigateViewToItemInfoCommand
			=> ApplicationViewModel.Instance.NavigateViewToItemInfoCommand;

		/// <summary>
		/// The target <see cref="Uri"/> if a link should be created.
		/// </summary>
		public Uri? TargetUri { get; set; }

		/// <summary>
		/// The title.
		/// </summary>
		public string? Title { get; set; }

	}

}
