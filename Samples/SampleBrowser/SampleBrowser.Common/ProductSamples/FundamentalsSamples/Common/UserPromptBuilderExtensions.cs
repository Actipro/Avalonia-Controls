using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Themes;
using ActiproSoftware.UI.Avalonia.Themes.Generation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using System;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Common {

	/// <summary>
	/// Defines extension methods for <see cref="UserPromptBuilder"/>.
	/// </summary>
	public static class UserPromptBuilderExtensions {

		/// <summary>
		/// Configures the builder to display an prompt when a dialog-based user prompt is requested on an unsupported platform.
		/// </summary>
		/// <param name="builder">The builder to configure.</param>
		/// <returns>The builder, for use with method-chaining.</returns>
		/// <exception cref="ArgumentNullException" />
		public static UserPromptBuilder ForDialogDisplayModeNotSupportedNotice(this UserPromptBuilder builder) {
			if (builder is null)
				throw new ArgumentNullException(nameof(builder));

			return builder
				.WithHeaderContent("Unsupported platform")
				.WithContent($"Dialogs are not supported on this platform, and attempting to display as a '{nameof(UserPromptDisplayMode.Dialog)}' would normally throw a PlatformNotSupportedException.")
				.WithFooterContent($"Use '{nameof(UserPromptDisplayMode.DialogPreferred)}' to fallback to overlays on unsupported platforms.")
				.WithStatusImage(MessageBoxImage.Error)
				.WithStatusImageTheme(MessageBoxImage.Error)
				.WithDisplayMode(UserPromptDisplayMode.Overlay);
		}

		/// <summary>
		/// Configures the builder to customize resources on the <see cref="UserPromptControl"/> with a <see cref="Hue"/>
		/// that corresponds to the intent of the given status image (e.g., red hue for error messages).
		/// </summary>
		/// <param name="builder">The builder to configure.</param>
		/// <param name="statusImage">The status image whose intent will be used to determine the <see cref="Hue"/>.</param>
		/// <returns>The builder, for use with method-chaining.</returns>
		/// <exception cref="InvalidOperationException" />
		public static UserPromptBuilder WithStatusImageTheme(this UserPromptBuilder builder, MessageBoxImage? statusImage = null) {
			return builder.AfterBuild(_ => {

				var userPromptControl = builder.Instance!;

				statusImage ??= userPromptControl.StandardStatusImage;

				Hue hue;
				switch (statusImage) {
					case MessageBoxImage.Error:
						hue = Hue.Red;
						break;
					case MessageBoxImage.Information:
						hue = Hue.Sky;
						break;
					case MessageBoxImage.Warning:
						hue = Hue.Orange;
						break;
					case MessageBoxImage.Question:
						hue = Hue.Indigo;
						break;
					default:
						// Nothing to apply
						return;
				}

				// Adjust assets based on theme
				var colorPalette = new DefaultColorPaletteFactory().Create();
				var colorRamp = colorPalette.Ramps[hue] ?? throw new InvalidOperationException("Unable to resolve the color ramp.");

				var lightestColorFamilyBrush = new SolidColorBrush(colorRamp.Shades.Resolve(50).Color);
				var lighterColorFamilyBrush = new SolidColorBrush(colorRamp.Shades.Resolve(100).Color);
				var lightColorFamilyBrush = new SolidColorBrush(colorRamp.Shades.Resolve(200).Color);
				var litColorFamilyBrush = new SolidColorBrush(colorRamp.Shades.Resolve(300).Color);
				var baseColorFamilyBrush = new SolidColorBrush(colorRamp.Shades.Resolve(350).Color);
				var dimColorFamilyBrush = new SolidColorBrush(colorRamp.Shades.Resolve(400).Color);
				var darkerColorFamilyBrush = new SolidColorBrush(colorRamp.Shades.Resolve(800).Color);

				// Set direct properties on the User Prompt Control
				userPromptControl.Foreground = Brushes.Black;
				userPromptControl.Background = lightestColorFamilyBrush;
				userPromptControl.TrayForeground = Brushes.Black;
				userPromptControl.TrayBackground = lighterColorFamilyBrush;
				userPromptControl.BorderBrush = lightColorFamilyBrush;
				userPromptControl.HeaderForeground = darkerColorFamilyBrush;

				//
				// Customize the assets used by buttons to change appearance in all states without style-based triggers
				//

				// Normal
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonForegroundBrush.ToResourceKey(), Brushes.Black);
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonBackgroundBrushOutline.ToResourceKey(), lightColorFamilyBrush);
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonBorderBrushOutline.ToResourceKey(), litColorFamilyBrush);

				// Hover
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonBackgroundBrushOutlinePointerOver.ToResourceKey(), litColorFamilyBrush);
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonBorderBrushOutlinePointerOver.ToResourceKey(), dimColorFamilyBrush);

				// Pressed
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonBackgroundBrushOutlinePressed.ToResourceKey(), baseColorFamilyBrush);
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonBorderBrushOutlinePressed.ToResourceKey(), darkerColorFamilyBrush);

				// Disabled
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonForegroundBrushDisabled.ToResourceKey(), new SolidColorBrush(Color.FromArgb(200, 0, 0, 0))); // Black with transparency
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonBackgroundBrushOutlineDisabled.ToResourceKey(), lightestColorFamilyBrush);
				userPromptControl.Resources.Add(ThemeResourceKind.ButtonBorderBrushOutlineDisabled.ToResourceKey(), lightColorFamilyBrush);
			})
			.AfterInitializeWindow(window => {
				// Callback to customize the UserPromptWindow before display
				// Override theme assets for the window title bar foreground to always use colors that match the custom theme (even when in a dark theme)
				window.Resources.Add(ThemeResourceKind.TitleBarForegroundBrush.ToResourceKey(), new SolidColorBrush(Color.FromRgb(112, 113, 113)));
				window.Resources.Add(ThemeResourceKind.TitleBarForegroundBrushActive.ToResourceKey(), new SolidColorBrush(Color.FromRgb(17, 17, 17)));
			});
		}

		/// <summary>
		/// Configures the content of a <see cref="UserPromptControl"/> button instance to include an icon.
		/// </summary>
		/// <param name="builder">The builder to configure.</param>
		/// <param name="image">The icon.</param>
		/// <param name="imageAlignment">The alignment of the icon.</param>
		/// <returns>The builder, for use with method-chaining.</returns>
		/// <exception cref="NotSupportedException">Thrown for unsupported image alignment values.</exception>
		public static UserPromptButtonBuilder WithIcon(this UserPromptButtonBuilder builder, IImage? image, HorizontalAlignment imageAlignment) {
			return builder.AfterBuild(_ => {
				if (image is not null
					&& builder.Instance is Button button) {

					var existingContent = button.Content as Control;
					if (existingContent is null
						&& button.Content is string label) {

						existingContent = new AccessText() { Text = label };
					}

					var imageControl = new DynamicImage() {
						Source = image,
						Height = 16,
						Width = 16,
						UseLayoutRounding = true,
					};

					if (existingContent is null) {
						// No content, so image will be centered without additional content
						imageControl.HorizontalAlignment = HorizontalAlignment.Center;
						imageControl.VerticalAlignment = VerticalAlignment.Center;
						button.Content = imageControl;
					}
					else {
						// Remove existing content from button before adding as new child
						button.Content = null;

						// Arrange the image next to the content based on image alignment
						double spacing = 4;
						switch (imageAlignment) {
							case HorizontalAlignment.Left:
								imageControl.VerticalAlignment = VerticalAlignment.Center;
								existingContent.VerticalAlignment = VerticalAlignment.Center;
								button.Content = new StackPanel {
									Spacing = spacing,
									Orientation = Orientation.Horizontal,
									Children = { imageControl, existingContent }
								};
								break;

							case HorizontalAlignment.Right:
								imageControl.VerticalAlignment = VerticalAlignment.Center;
								existingContent.VerticalAlignment = VerticalAlignment.Center;
								button.Content = new StackPanel {
									Spacing = spacing,
									Orientation = Orientation.Horizontal,
									Children = { existingContent, imageControl }
								};
								break;

							case HorizontalAlignment.Center:
								imageControl.HorizontalAlignment = HorizontalAlignment.Center;
								existingContent.HorizontalAlignment = HorizontalAlignment.Center;
								button.Content = new StackPanel {
									Spacing = spacing,
									Orientation = Orientation.Vertical,
									HorizontalAlignment = HorizontalAlignment.Center,
									Children = { imageControl, existingContent }
								};
								break;

							default:
								throw new NotSupportedException();
						}
					}

				}
			});
		}

	}

}
