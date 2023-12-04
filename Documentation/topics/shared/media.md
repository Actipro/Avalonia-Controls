---
title: "Media"
page-title: "Media - Shared Library Reference"
order: 9
---
# Media

The [ActiproSoftware.UI.Avalonia.Media](xref:@ActiproUIRoot.Media) namespace defines a [UIColor](xref:@ActiproUIRoot.Media.UIColor) structure for working with colors and an extension method to easily convert `Avalonia.Media.Color` to a [UIColor](xref:@ActiproUIRoot.Media.UIColor).

## UIColor Structure

The [UIColor](xref:@ActiproUIRoot.Media.UIColor) structure provides an enhanced representation of a color that supports the RGB, HSL, HSV, OKLAB, and OKLCH color models, conversion between models, and numerous other helper methods.  Static methods on the structure are used to create instances of the structure based on the supported models.

These instance members are found in the structure:

| Member | Description |
|-----|-----|
| [Alpha](xref:@ActiproUIRoot.Media.UIColor.Alpha) Property | The alpha component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HslHue](xref:@ActiproUIRoot.Media.UIColor.HslHue) Property | The HSL hue component of the color, which is a color wheel degree value in the range `0` to `360`. |
| [HslLightness](xref:@ActiproUIRoot.Media.UIColor.HslLightness) Property | The HSL lightness component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HslSaturation](xref:@ActiproUIRoot.Media.UIColor.HslSaturation) Property | The HSL saturation component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HsvHue](xref:@ActiproUIRoot.Media.UIColor.HsvHue) Property | The HSV hue component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HsvSaturation](xref:@ActiproUIRoot.Media.UIColor.HsvSaturation) Property | The HSV saturation component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HsvValue](xref:@ActiproUIRoot.Media.UIColor.HsvValue) Property | The HSV value (brightness) component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [IsDark](xref:@ActiproUIRoot.Media.UIColor.IsDark) Property | Gets whether the color is a dark color. |
| [IsLight](xref:@ActiproUIRoot.Media.UIColor.IsLight) Property | Gets whether the color is a light color. |
| [OklabAChromaticity](xref:@ActiproUIRoot.Media.UIColor.OklabAChromaticity) Property | The OKLAB A chromaticity component of the color, which is a value in the range `-0.4`..`0.4`. |
| [OklabBChromaticity](xref:@ActiproUIRoot.Media.UIColor.OklabBChromaticity) Property | The OKLAB B chromaticity component of the color, which is a value in the range `-0.4`..`0.4`. |
| [OklabLightness](xref:@ActiproUIRoot.Media.UIColor.OklabLightness) Property | The OKLAB lightness component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [OklchChroma](xref:@ActiproUIRoot.Media.UIColor.OklchChroma) Property | The OKLCH chroma (saturation) component of the color, which is a value in the range `0.0`..`0.4`. |
| [OklchHue](xref:@ActiproUIRoot.Media.UIColor.OklchHue) Property | The OKLCH hue component of the color, which is a color wheel degree value in the range `0`..`360`. |
| [OklchLightness](xref:@ActiproUIRoot.Media.UIColor.OklchLightness) Property | The OKLCH lightness component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [RbgAlpha](xref:@ActiproUIRoot.Media.UIColor.RgbAlpha) Property | The RGB alpha component of the color, which is a byte value in the range `0` to `255`. |
| [RbgBlue](xref:@ActiproUIRoot.Media.UIColor.RgbBlue) Property | The RGB blue component of the color, which is a byte value in the range `0` to `255`. |
| [RbgGreen](xref:@ActiproUIRoot.Media.UIColor.RgbGreen) Property | The RGB green component of the color, which is a byte value in the range `0` to `255`. |
| [RbgRed](xref:@ActiproUIRoot.Media.UIColor.RgbRed) Property | The RGB red component of the color, which is a byte value in the range `0` to `255`. |
| [ToChromaticAdaptation](xref:@ActiproUIRoot.Media.UIColor.ToChromaticAdaptation*) Method | Uses chromatic adaptation to create a new [UIColor](xref:@ActiproUIRoot.Media.UIColor) that renders clearly on the specified background color.  Chromatic adaptation is commonly used to adapt a color that was intended for use with light backgrounds to render properly on dark backgrounds.  |
| [ToGrayscale](xref:@ActiproUIRoot.Media.UIColor.ToGrayscale*) Method | Creates a new [UIColor](xref:@ActiproUIRoot.Media.UIColor) that is a grayscale transformation of the specified color. |
| [ToHexString](xref:@ActiproUIRoot.Media.UIColor.ToHexString*) Method | Converts the color value to an RGB hexadecimal string (e.g., `"#ff00ff"`, `"#80ff00ff"`). |
| [ToHsl](xref:@ActiproUIRoot.Media.UIColor.ToHsl*) Method | Converts the color value to an `HslColor` instance that represents the color in the HSL (hue, saturation, and lightness) color model. |
| [ToHsv](xref:@ActiproUIRoot.Media.UIColor.ToHsv*) Method | Converts the color value to an `HsvColor` instance that represents the color in the HSV (hue, saturation, and value) color model. |
| [ToRgb](xref:@ActiproUIRoot.Media.UIColor.ToRgb*) Method | Converts the color value to a `Color` instance that represents the color in the RGB (red, green, and blue) color model. |

> [!WARNING]
> The [Alpha](xref:@ActiproUIRoot.Media.UIColor.Alpha) property is a percentage of opacity from `0.0` to `1.0`. Be careful to use the [RgbAlpha](xref:@ActiproUIRoot.Media.UIColor.RgbAlpha) property (which is a byte value from `0` to `255`)  when working with the RGB color model.

These static members are found in the structure:

| Member | Description |
|-----|-----|
| [FromHsl](xref:@ActiproUIRoot.Media.UIColor.FromHsl*) Method | Creates a `UIColor` instance from the specified HSL (hue, saturation, and lightness) and optional alpha components, or `HslColor` instance. |
| [FromHsv](xref:@ActiproUIRoot.Media.UIColor.FromHsv*) Method | Creates a `UIColor` instance from the specified HSV (hue, saturation, and value) and optional alpha components, or `HsvColor` instance. |
| [FromMix](xref:@ActiproUIRoot.Media.UIColor.FromMix*) Method | Creates a `UIColor` instance that is the specified percentage between the value of two `Color` objects. |
| [FromOklab](xref:@ActiproUIRoot.Media.UIColor.FromOklab*) Method | Creates a `UIColor` instance from the specified OKLAB (lightness, A, and B) and optional alpha components. |
| [FromOklch](xref:@ActiproUIRoot.Media.UIColor.FromOklch*) Method | Creates a `UIColor` instance from the specified OKLCH (lightness, chroma, and hue) and optional alpha components. |
| [FromRgb](xref:@ActiproUIRoot.Media.UIColor.FromRgb*) Method | Creates a `UIColor` instance from the specified RGB (red, green, and blue) and optional alpha components, or `Color` instance. |
| [Parse](xref:@ActiproUIRoot.Media.UIColor.Parse*) Method | Creates a `UIColor` instance from the specified color name, RGB hexadecimal string, or any other color string supported by `Color.Parse`. |
| [TryParse](xref:@ActiproUIRoot.Media.UIColor.Parse*) Method | Tries to creates a `UIColor` instance from the specified color name, RGB hexadecimal string, or any other color string supported by `Color.Parse`. |

> [!TIP]
> [UIColor](xref:@ActiproUIRoot.Media.UIColor) defines several implicit cast overloads to easily convert to/from [UIColor](xref:@ActiproUIRoot.Media.UIColor) and the Avalonia types for `Color`, `HslColor`, and `HsvColor`.

## Color Extensions

The following extension methods to `Avalonia.Media.Color` are defined on [ColorExtensions](xref:@ActiproUIRoot.Media.ColorExtensions):

| Member | Description |
|-----|-----|
| [ToUIColor](xref:@ActiproUIRoot.Media.ColorExtensions.ToUIColor*) Method | Creates a new [UIColor](xref:@ActiproUIRoot.Media.UIColor) initialized from the `Color` for easily accessing the additional capabilities of [UIColor](xref:@ActiproUIRoot.Media.UIColor).  |

> [!IMPORTANT]
> The [ActiproSoftware.UI.Avalonia.Media](xref:@ActiproUIRoot.Media) namespace must be imported with a `using` statement to access the extensions.