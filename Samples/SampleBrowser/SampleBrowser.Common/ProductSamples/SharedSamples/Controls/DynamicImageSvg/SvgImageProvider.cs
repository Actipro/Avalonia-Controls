using ActiproSoftware.UI.Avalonia.Media;
using Avalonia.Media;
using Avalonia.Svg.Skia;
using ShimSkiaSharp;

namespace ActiproSoftware.ProductSamples.SharedSamples.Controls.DynamicImageSvg;

/// <summary>
/// Enhances the default <see cref="ImageProvider"/> with support for SVG images from the "Svg.Controls.Skia.Avalonia" NuGet package.
/// </summary>
public class SvgImageProvider : ImageProvider {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Converts a <see cref="SKColor"/> to an Avalonia <see cref="Color"/>.
	/// </summary>
	/// <param name="color">The color to convert.</param>
	/// <returns>The converted color.</returns>
	private static Color ToColor(SKColor color)
		=> Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue);

	/// <summary>
	/// Converts an Avalonia <see cref="Color"/> to <see cref="SKColor"/>.
	/// </summary>
	/// <param name="color">The color to convert.</param>
	/// <returns>The converted color.</returns>
	private static SKColor ToSKColor(Color color)
		=> new(color.R, color.G, color.B, color.A);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override IImage? AdaptImageSource(IImage originalImageSource, ImageProviderRequest request) {
		if (originalImageSource is SvgImage svgImage) {
			var canAdapt = GetCanAdapt(svgImage);

			// Clone the image
			svgImage = svgImage.Clone();
			SetCanAdapt(svgImage, canAdapt);

			// Iterate through draw commands to adapt colors
			if (svgImage.Source?.Svg?.Model?.Commands is { } commands) {
				foreach (var command in commands) {
					SKPaint? paint = null;
					if (command is DrawPathCanvasCommand drawPathCommand)
						paint = drawPathCommand.Paint;

					if (paint?.Color is { } skColor) {
						var color = ToColor(skColor);
						var adaptedColor = AdaptColor(color, request);
						if (adaptedColor != color)
							paint.Color = ToSKColor(adaptedColor);
					}

					if ((paint?.Shader is ColorShader shader) && (shader.Color is { } skShaderColor)) {
						var color = ToColor(skShaderColor);
						var adaptedColor = AdaptColor(color, request);
						if (adaptedColor != color)
							paint.Shader = SKShader.CreateColor(ToSKColor(adaptedColor), shader.ColorSpace);
					}
				}
			}

			return svgImage;
		}

		return base.GetImageSource(originalImageSource, request);
	}

}
