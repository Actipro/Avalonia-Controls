---
title: "Image Provider"
page-title: "Image Provider - Themes Reference"
order: 90
---
# Image Provider

The [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) class has logic to manipulate images for various scenarios.  Features include:

- Chromatic adaptation (color shifting) for images, which allows images designed for light themes to be automatically adjusted for use in dark themes.
- Converting a monochrome vector image to render in the current foreground color.
- Conversion of images to grayscale.
- Conversion of images to monochrome, in a specified color.

While all the image adaptation logic is contained within the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) class and can be called programmatically, the [DynamicImage](../shared/controls/dynamic-image.md) control is generally used within the user interface to access the image provider's functionality.

![Screenshot](../shared/images/dynamicimage-multi.png)

*A single raster image that is altered to show normal, disabled, monochrome, and monochrome disabled states in both light and dark themes*

## Usage Scenarios for ImageProvider

Common scenarios for using an image provider (and generally [DynamicImage](../shared/controls/dynamic-image.md) controls) are:

- Your application designed images for a light theme, and you want them adapted to look good in a dark theme.
- You have monochrome vector icons and want to render them in the current foreground color instead of their designed color.
- You want images adapted to grayscale.
- You want images adapted to monochrome.

## DynamicImage and ImageProvider

The [DynamicImage](../shared/controls/dynamic-image.md) control calls into the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[GetImageSource](xref:@ActiproUIRoot.Media.ImageProvider.GetImageSource*) method to adapt the `IImage` set to its `Source` property.

It watches for theme changes and updates the image adaptation appropriately.

> [!NOTE]
> The native `Image` control from which [DynamicImage](../shared/controls/dynamic-image.md) inherits does not include any functionality to interact with [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).

## Assigning a Non-Default ImageProvider to an Image

An [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) instance is created and assigned to the static [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) property by default.

When the [DynamicImage](../shared/controls/dynamic-image.md) control needs to adapt an image, it examines the `IImage` set to its `Source` property. When the source is an instance of an `AvaloniaObject` (like `DrawingImage`), it checks to see if a value is returned for the `ImageProvider.Provider` attached property.  If no specific [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) instance was set via that attached property on the image, then the static instance defined by [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) will be used for adaptation.

The following example shows how a non-default [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) instance available as a static resource could be applied be applied to a `DrawingImage`.  The `DrawingImage` will then use that non-default image provider for its adaptation logic instead of the one available via [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default).

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<UserControl.Resources>
	<!-- ImageProvider with dark theme chromatic adaptation -->
	<actipro:ImageProvider x:Key="DarkThemeImageProvider" ChromaticAdaptationMode="DarkThemes" />
</UserControl.Resources>
...

<!-- Vector image -->
<actipro:DynamicImage Width="32" Height="32">
	<DrawingImage actipro:ImageProvider.Provider="{StaticResource DarkThemeImageProvider}">
		<DrawingGroup>
			<GeometryDrawing Geometry="M 2 0 L 30 0 L 30 30 L 2 30 Z" Brush="Black" />
		</DrawingGroup>
	</DrawingImage>
</actipro:DynamicImage>

<!-- Raster image  -->
<actipro:DynamicImage Width="32" Height="32">
	<DrawingImage actipro:ImageProvider.Provider="{StaticResource DarkThemeImageProvider}">
		<ImageDrawing ImageSource="/Images/Icons/Save32.png" Rect="0,0,32,32" />
	</DrawingImage>
</actipro:DynamicImage>
```

> [!IMPORTANT]
> See the "Working with Raster Images" section below for more detail on why a `DrawingImage` is used with attached properties in the example instead of a `Bitmap` object.

In summary, if you want all the [DynamicImage](../shared/controls/dynamic-image.md) control instances in your application to use the same image provider configuration, simply set that configuration on the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) instance.  If, instead, you want some images to have an alternate configuration, define another [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) instance and set that on each applicable `DrawingImage` via the `ImageProvider.Provider` attached property.

## Chromatic Adaptation

Chromatic adaptation is the process of converting colors in an image so that they render well on a specific background color.  This is especially useful when your application has images designed for a light theme, and you wish for your application to support a dark theme.  An image provider can automatically convert them for you so that you don't have to design dark themed variations.

![Screenshot](../shared/images/dynamicimage-chromatic.png)

*A single image that is altered to adapt to the current background color*

> [!IMPORTANT]
> The standard configuration of the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) instance is not configured for chromatic adaptation by default, but can be enabled as shown below.

> [!TIP]
> For best results, the source images should be designed for a light theme and use a minimal number of colors (i.e., avoid gradients).

### Dark Themes

The most common scenario for chromatic adaptation is to support dark themes.  The following example shows the configuration for the default [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to support adaptation of all images when in a
dark theme.

```csharp
using ActiproSoftware.UI.Avalonia.Media;
...
// Set this configuration during application startup before UI elements are loaded
ImageProvider.Default.ChromaticAdaptationMode = ImageChromaticAdaptationMode.DarkThemes;
```

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" />
```

### All Themes

Chromatic adaptation can also be applied in all themes (i.e., light or dark).  The following example shows the configuration for the default [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to support adaptation of all images, regardless of theme.

```csharp
using ActiproSoftware.UI.Avalonia.Media;
...
// Set this configuration during application startup before UI elements are loaded
ImageProvider.Default.ChromaticAdaptationMode = ImageChromaticAdaptationMode.Always;
```

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" />
```

While the [ImageChromaticAdaptationMode](xref:@ActiproUIRoot.Media.ImageChromaticAdaptationMode).[Always](xref:@ActiproUIRoot.Media.ImageChromaticAdaptationMode.Always) mode is supported, it's generally enough to use the [DarkThemes](xref:@ActiproUIRoot.Media.ImageChromaticAdaptationMode.DarkThemes) mode instead.  [DarkThemes](xref:@ActiproUIRoot.Media.ImageChromaticAdaptationMode.DarkThemes) mode doesn't perform any adaptation processing when in light themes.

> [!TIP]
> The current theme's default background color will be used for adaptation. When using [Always](xref:@ActiproUIRoot.Media.ImageChromaticAdaptationMode.Always) mode, the [DynamicImage](xref:@ActiproUIRoot.Controls.DynamicImage).[BackgroundHint](xref:@ActiproUIRoot.Controls.DynamicImage.BackgroundHint) property can be set to an `IBrush` that defines an alternate color to use for adaptation.

## Predefined Monochrome Vector Image Adaptation

Vector images designed in a single color (monochrome) can be adapted to render using the [DynamicImage](../shared/controls/dynamic-image.md) control's current foreground color.  This feature is extremely useful when vector images need to be used on a variety of controls, each with different foregrounds and backgrounds.

The following example shows the configuration for an [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to support adaptation of vector images designed with a single color (in this case black) so that they dynamically update to match the current foreground color instead of always being black.

```csharp
using ActiproSoftware.UI.Avalonia.Media;
...
// Set this configuration during application startup before UI elements are loaded
ImageProvider.Default.DesignForegroundColor = Colors.Black;
```

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:DynamicImage Width="32" Height="32" Source="{StaticResource MonochromeCheckVectorDrawing}" />
```

## Grayscale Adaptation

Images are often converted to grayscale when their containing controls are disabled.

![Screenshot](../shared/images/dynamicimage-grayscale.png)

*A single image that is altered to appear as grayscale with varying degrees of opacity*

The following example shows a [DynamicImage](../shared/controls/dynamic-image.md) containing an image that will render in semi-transparent grayscale when disabled.

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" IsEnabled="False" DisabledOpacity="0.5" />
```

## Monochrome Adaptation

Images are often converted to monochrome when displayed on intense backgrounds, or when going for a flat appearance.

![Screenshot](../shared/images/dynamicimage-monochrome.png)

*A single image that is altered to appear as monochrome based on a foreground color*

The following example shows a [DynamicImage](../shared/controls/dynamic-image.md) containing a raster image that will render in monochrome.  The monochrome color will match the color derived from the inherited foreground property.

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" UseMonochrome="True" />
```

The following example shows a [DynamicImage](../shared/controls/dynamic-image.md) containing a raster image that will render in monochrome.  The monochrome color will be red.

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:DynamicImage Width="32" Height="32" Source="/Images/Save32.png" UseMonochrome="True" Foreground="Red" />
```

## Preventing Portions of an Image from Being Adapted

Sometimes a `DrawingImage` may include portions within it that should not be adapted.

A common scenario of this case is a fill color image, where the image shows a paint bucket and a swatch rectangle underneath it that contains the current foreground color.  For this scenario, we only want the root portion of the image (the paint bucket) to be adapted and we do not want the swatch rectangle to be touched by adaptation logic in any way.

To prevent adaptation on a `DrawingImage` (or a `Drawing` within a `DrawingImage`), set the attached `ImageProvider.CanAdapt` property to `false` on that object.  It will be skipped during the image adaptation process.

The following example shows how the attached `ImageProvider.CanAdapt` property can be used to prevent an entire image from being adapted.

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:DynamicImage Width="32" Height="32">
	<DrawingImage actipro:ImageProvider.CanAdapt="False">
		<ImageDrawing ImageSource="/Images/Icons/Save32.png" Rect="0,0,32,32" />
	</DrawingImage>
</actipro:DynamicImage>
```

## Working with Raster Images (Bitmap)

A `Bitmap` object for raster images does not derive from `AvaloniaObject`, so that means it does not support attached properties like `ImageProvider.Provider` or `ImageProvider.CanAdapt`.  When these attached properties need to be set for a raster image, use an `ImageDrawing` within a `DrawingImage` instead of a `Bitmap`, as shown below:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:DynamicImage Width="32" Height="32">
	<DrawingImage actipro:ImageProvider.Provider="{StaticResource DarkThemeImageProvider}">
		<ImageDrawing ImageSource="/Images/Icons/Save32.png" Rect="0,0,32,32" />
	</DrawingImage>
</actipro:DynamicImage>
```

This is only necessary if attached properties must be used to configure adaptation. Otherwise, a `Bitmap` may be used as an image source.

> [!WARNING]
> Raster images must be encoded using `PixelFormats.Bgra8888` or `PixelFormats.Rgba8888` to support adaptation. Any other pixel formats will result in the original image being used without adaptation.