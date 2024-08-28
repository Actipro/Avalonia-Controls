---
title: "Converting to v24.1"
page-title: "Converting to v24.1 - Conversion Notes"
order: 99
---
# Converting to v24.1

## Avalonia Dependency

Updated the minimum Avalonia dependency from v11.0.5 to v11.0.7.

## RingSlice

The [Ring Slice](../shared/shapes/ring-slice.md) shape was added in v23.1 and is a very handy primitive for controls that need to render an arc shape.  When integrating the shape into some other controls, we found that the design needed to be slightly refactored for ease of use and to better support animation.

These API changes were made:
- The [StartAngle](xref:@ActiproUIRoot.Controls.Shapes.RingSlice.StartAngle) property is now a `Double` instead of an [Angle](xref:ActiproSoftware.Angle).
- The former `EndAngle` property is now a `Double` instead of an [Angle](xref:ActiproSoftware.Angle) and specified via the [SweepAngle](xref:@ActiproUIRoot.Controls.Shapes.RingSlice.SweepAngle) property instead.  The resolved end angle is now calculated by adding the sweep angle to the start angle.

The end result is that if a ring slice previously specified `StartAngle="90" EndAngle="270"`, the same slice after the updates would be specified `StartAngle="90" SweepAngle="180"`.

## Some Image-Based Identifiers Renamed

Some properties and types related to `IImage` were renamed from "Image" to "ImageSource" as part of a new naming convention.  Some other image-related types were also impacted.  The following breaking changes were made:

- [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).`FooterImage` renamed as `FooterImageSource`.
- [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).`StatusImage` renamed as `StatusImageSource`.
- `ActiproSoftware.UI.Avalonia.Controls.Converters.ImageKeyToImageConverter` type renamed as [ImageKeyToImageSourceConverter](xref:@ActiproUIRoot.Controls.Converters.ImageKeyToImageSourceConverter)
- `ActiproSoftware.UI.Avalonia.Media.SharedImageKeys` type renamed as [SharedImageSourceKeys](xref:@ActiproUIRoot.Media.SharedImageSourceKeys).

## ButtonCard Theme Removed

Previous builds contained a `ButtonCard` theme (`theme-card` style class name) for native `Button` control instances. With the introduction of the new [Card](../fundamentals/controls/card.md) control, the older `ButtonCard` theme has been removed.  Those who used the old theme can use the [Card](../fundamentals/controls/card.md) control instead.

## Theme Resource Renamed

For naming consistency, the [ThemeResourceKind](xref:@ActiproUIRoot.Themes.ThemeResourceKind) enum value `NotificationBorderBrushInfo` was renamed to `NotificationBorderBrushInformation`.