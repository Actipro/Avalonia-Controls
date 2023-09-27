---
title: "ShadowChrome"
page-title: "ShadowChrome - Shared Library Controls"
order: 9
---
# ShadowChrome

[ShadowChrome](xref:@ActiproUIRoot.Controls.ShadowChrome) is a decorator that renders a drop shadow around its child control.

![Screenshot](../images/shadowchrome.png)

*A ShadowChrome providing a soft shadow around a contact card*

## Elevation

The [Elevation](xref:@ActiproUIRoot.Controls.ShadowChrome.Elevation) property determines how large of a shadow to render.  The elevation value can be from `0` (no shadow) to `24`, and defaults to `4`.  A larger-than-zero value must be specified for a shadow to be rendered.

> [!TIP]
> The constant [PopupElevation](xref:@ActiproUIRoot.Controls.ShadowChrome.PopupElevation) defines a default elevation that can be used for popup content.

## Enabled

The [IsShadowEnabled](xref:@ActiproUIRoot.Controls.ShadowChrome.IsShadowEnabled) property can be set to `false` to prevent the shadow from rendering.  When `false`, the elevation value is effectively coerced to `0`.

## Opacity

The [ShadowOpacity](xref:@ActiproUIRoot.Controls.ShadowChrome.ShadowOpacity) property determines the intensity of the shadow.  The default property value is `0.3`, meaning 30% opacity.

> [!NOTE]
> The default control theme for [ShadowChrome](xref:@ActiproUIRoot.Controls.ShadowChrome) defines a style that uses theme-specific resources for [ShadowOpacity](xref:@ActiproUIRoot.Controls.ShadowChrome.ShadowOpacity). In a light variant theme, the opacity resource matches the default value of `0.3`, while the dark variant theme is `0.5`.

## Resulting Thickness

The read-only [ShadowThickness](xref:@ActiproUIRoot.Controls.ShadowChrome.ShadowThickness) property returns the margin around the shadow chrome's child element that is required to fully render the shadow in its current configuration.

> [!WARNING]
> When using the shadow chrome in a `Popup`, the `ShadowChrome.Margin` should be set to the [ShadowThickness](xref:@ActiproUIRoot.Controls.ShadowChrome.ShadowThickness) property value to ensure the popup is large enough to render the semi-transparent shadow.

## Working with Dark Themes

A dark shadow is harder to see when rendered on backgrounds that are already dark.  For most dark themes, consider increasing the [ShadowOpacity](xref:@ActiproUIRoot.Controls.ShadowChrome.ShadowOpacity) to `0.5` (50%) or higher so the shadow is more intense.

For very dark themes where the background is almost the same color as the shadow, try changing the [ShadowColor](xref:@ActiproUIRoot.Controls.ShadowChrome.ShadowColor) to a lighter color. This will provide a subtle backlight effect instead of a shadow.

> [!NOTE]
> The default control theme for [ShadowChrome](xref:@ActiproUIRoot.Controls.ShadowChrome) automatically adjusts [ShadowOpacity](xref:@ActiproUIRoot.Controls.ShadowChrome.ShadowOpacity) and [ShadowColor](xref:@ActiproUIRoot.Controls.ShadowChrome.ShadowColor) when switching theme variants.