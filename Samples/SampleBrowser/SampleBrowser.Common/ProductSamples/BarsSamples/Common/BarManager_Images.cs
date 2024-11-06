using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using Avalonia;
using Avalonia.Media;
using System;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	partial class BarManager {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc cref="CreateBitmapImage(BarImageOptions, string, string, string)"/>
		private static IImage? CreateBitmapImage(BarImageOptions options, string smallFileName)
			=> CreateBitmapImage(options, smallFileName, mediumFileName: null, largeFileName: null);

		/// <summary>
		/// Creates an <see cref="ImageSource"/> based on the given options.
		/// </summary>
		/// <param name="options">The options which define the image to be created.</param>
		/// <param name="smallFileName">The name of the resource file that represents the small image (e.g., 16x16).</param>
		/// <param name="mediumFileName">The name of the resource file that represents the medium image (e.g., 24x24).</param>
		/// <param name="largeFileName">The name of the resource file that represents the large image (e.g., 32x32).</param>
		/// <returns>An <see cref="ImageSource"/> based on the given options, or <c>null</c> if a corresponding image is not available.</returns>
		/// <seealso cref="RegisterImages"/>
		private static IImage? CreateBitmapImage(BarImageOptions options, string? smallFileName, string? mediumFileName, string? largeFileName) {
			// Determine the name of the resource file that is appropriate for the requested image size
			string? fileNameResolved = null;
			switch (options.Size) {
				case BarImageSize.Small:
					fileNameResolved = smallFileName;
					break;
				case BarImageSize.Medium:
					// Medium images not supported by this sample
					break;
				case BarImageSize.Large:
					fileNameResolved = largeFileName;
					break;
			}
			if (!string.IsNullOrEmpty(fileNameResolved))
				return ImageLoader.GetIcon(fileNameResolved);

			return null;
		}

		/// <inheritdoc cref="CreateBitmapImageWithColorBar(BarImageOptions, Color, string, string, string)"/>
		private static IImage? CreateBitmapImageWithColorBar(BarImageOptions options, Color defaultColor, string smallFileName)
			=> CreateBitmapImageWithColorBar(options, defaultColor, smallFileName, mediumFileName: null, largeFileName: null);

		/// <summary>
		/// Creates an <see cref="ImageSource"/> based on the given options with a color bar overlay.
		/// </summary>
		/// <inheritdoc cref="CreateBitmapImage(BarImageOptions, string, string, string)"/>
		/// <param name="defaultColor">The default color to be used for the color bar when <see cref="BarImageOptions.ContextualColor"/> is not defined.</param>
		private static IImage? CreateBitmapImageWithColorBar(BarImageOptions options, Color defaultColor, string? smallFileName, string? mediumFileName, string? largeFileName) {
			// Load the base image
			var imageSource = CreateBitmapImage(options, smallFileName, mediumFileName, largeFileName);
			if (imageSource != null) {

				// Determine the bounds of the color bar
				var imageHeight = imageSource.Size.Height;
				var imageWidth = imageSource.Size.Width;
				if (imageHeight > 0 && imageWidth > 0) {
					var colorBarHeight = Math.Max(1, imageHeight / 4);
					var colorBarBounds = new Rect(0, imageHeight - colorBarHeight, imageWidth, colorBarHeight);
					if (colorBarBounds.Height > 0 && colorBarBounds.Width > 0) {
						// Add the color bar to the image
						imageSource = UI.Avalonia.Media.ImageProvider.GetImageSourceWithColorSwatch(imageSource, colorBarBounds, options.ContextualColor.GetValueOrDefault(defaultColor));
					}
				}

				return imageSource;
			}

			return null;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Gets the provider that will be used to provide images.
		/// </summary>
		/// <value>A <see cref="BarImageProvider"/>.</value>
		public BarImageProvider ImageProvider { get; } = new BarImageProvider();

	}

}
