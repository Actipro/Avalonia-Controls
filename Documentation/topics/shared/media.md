---
title: "Media"
page-title: "Media - Shared Library Reference"
order: 9
---
# Media

The [ActiproSoftware.UI.Avalonia.Media](xref:@ActiproUIRoot.Media) namespace defines a [UIColor](xref:@ActiproUIRoot.Media.UIColor) structure for working with colors.

## UIColor Structure

The [UIColor](xref:@ActiproUIRoot.Media.UIColor) structure provides an enhanced representation of a color that supports the RGB, HSL, and HSV color models, conversion between models, and numerous other helper methods.  Static methods on the structure are used to create instances of the structure based on the supported models.

These instance members are found in the structure:

| Member | Description |
|-----|-----|
| [Alpha](xref:@ActiproUIRoot.Media.UIColor.Alpha) Property | Gets or sets the alpha component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HslHue](xref:@ActiproUIRoot.Media.UIColor.HslHue) Property | Gets or sets the HSL hue component of the color, which is a color wheel degree value in the range `0` to `360`. |
| [HslLightness](xref:@ActiproUIRoot.Media.UIColor.HslLightness) Property | Gets or sets the HSL lightness component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HslSaturation](xref:@ActiproUIRoot.Media.UIColor.HslSaturation) Property | Gets or sets the HSL saturation component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HsvHue](xref:@ActiproUIRoot.Media.UIColor.HsvHue) Property | Gets or sets the HSV hue component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HsvSaturation](xref:@ActiproUIRoot.Media.UIColor.HsvSaturation) Property | Gets or sets the HSV saturation component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [HsvValue](xref:@ActiproUIRoot.Media.UIColor.HsvValue) Property | Gets or sets the HSV value (brightness) component of the color, which is a percentage value in the range `0.0` to `1.0`. |
| [IsDark](xref:@ActiproUIRoot.Media.UIColor.IsDark) Property | Gets whether the color is a dark color. |
| [IsLight](xref:@ActiproUIRoot.Media.UIColor.IsLight) Property | Gets whether the color is a light color. |
| [RbgAlpha](xref:@ActiproUIRoot.Media.UIColor.RgbAlpha) Property | Gets or sets the RGB alpha component of the color, which is a byte value in the range `0` to `255`. |
| [RbgBlue](xref:@ActiproUIRoot.Media.UIColor.RgbBlue) Property | Gets or sets the RGB blue component of the color, which is a byte value in the range `0` to `255`. |
| [RbgGreen](xref:@ActiproUIRoot.Media.UIColor.RgbGreen) Property | Gets or sets the RGB green component of the color, which is a byte value in the range `0` to `255`. |
| [RbgRed](xref:@ActiproUIRoot.Media.UIColor.RgbRed) Property | Gets or sets the RGB red component of the color, which is a byte value in the range `0` to `255`. |
| [ToHexString](xref:@ActiproUIRoot.Media.UIColor.ToHexString*) Method | Converts the color value to an RGB hexadecimal string (e.g., `"#ff00ff"`, `"#80ff00ff"`). |
| [ToHsl](xref:@ActiproUIRoot.Media.UIColor.ToHsl*) Method | Converts the color value to an `HslColor` instance that represents the color in the HSL (hue, saturation, and lightness) color model. |
| [ToHsv](xref:@ActiproUIRoot.Media.UIColor.ToHsv*) Method | Converts the color value to an `HsvColor` instance that represents the color in the HSV (hue, saturation, and value) color model. |
| [ToRgb](xref:@ActiproUIRoot.Media.UIColor.ToRgb*) Method | Converts the color value to a `Color` instance that represents the color in the RGB (red, green, and blue) color model. |

> [!WARNING]
> The [Alpha](xref:@ActiproUIRoot.Media.UIColor.Alpha) property is a percentage of opacity from `0.0` to `1.0`. Be careful to use the [RgbAlpha](xref:@ActiproUIRoot.Media.UIColor.RgbAlpha) property (which is a byte value from `0` to `255`)  when working with the RGB color model.

These static members are found in the structure:

| Member | Description |
|-----|-----|
| [FromHsl](xref:@ActiproUIRoot.Media.UIColor.FromHsl*) Method | Creates a `UIColor` instance from the specified HSL (hue, saturation, lightness, and optional alpha) components or `HslColor` instance. |
| [FromHsv](xref:@ActiproUIRoot.Media.UIColor.FromHsv*) Method | Creates a `UIColor` instance from the specified HSV (hue, saturation, value, and optional alpha) components or `HsvColor` instance. |
| [FromMix](xref:@ActiproUIRoot.Media.UIColor.FromMix*) Method | Creates a `UIColor` instance that is the specified percentage between the value of two `Color` objects. |
| [FromRgb](xref:@ActiproUIRoot.Media.UIColor.FromRgb*) Method | Creates a `UIColor` instance from the specified RGB (red, green, blue, and optional alpha) components or `Color` instance. |
| [Parse](xref:@ActiproUIRoot.Media.UIColor.Parse*) Method | Creates a `UIColor` instance from the specified color name, RGB hexadecimal string, or any other color string supported by `Color.Parse`. |
| [TryParse](xref:@ActiproUIRoot.Media.UIColor.Parse*) Method | Tries to creates a `UIColor` instance from the specified color name, RGB hexadecimal string, or any other color string supported by `Color.Parse`. |