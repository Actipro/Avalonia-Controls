---
title: "ToggleThemeButton"
page-title: "ToggleThemeButton - Shared Library Controls"
order: 45
---
# ToggleThemeButton

The [ToggleThemeButton](xref:@ActiproUIRoot.Controls.ToggleThemeButton) is a button that, when invoked, toggles a `RequestedThemeVariant` property value between `ThemeVariant.Light` and `ThemeVariant.Dark`.

![Screenshot](../images/togglethemebutton-200%-multi.png)

By default, the control theme for [ToggleThemeButton](xref:@ActiproUIRoot.Controls.ToggleThemeButton) displays a "moon" icon when the light theme is requested and a "sun" icon when the dark theme is requested.

## Theme Scope

When defined within a `ThemeVariantScope`, the button will toggle the `ThemeVariantScope.RequestedThemeVariant` property.  Otherwise, the button will toggle the global `Application.RequestedThemeVariant` property.

To modify a `ThemeVariantScope` that is not a direct ancestor of the button, set the [Target](xref:@ActiproUIRoot.Controls.ToggleThemeButton.Target) property to the desired `ThemeVariantScope` (or any of its descendants).

## Example

The following examples demonstrate how to define a [ToggleThemeButton](xref:@ActiproUIRoot.Controls.ToggleThemeButton) for different scenarios:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<!-- Toggle Application Theme -->
<actipro:ToggleThemeButton />

<!-- Toggle Ancestor ThemeVariantScope Theme -->
<ThemeVariantScope>
    ...
    <actipro:ToggleThemeButton />
</ThemeVariantScope>

<!-- Toggle Unrelated ThemeVariantScope Theme -->
<ThemeVariantScope x:Name="targetScope">
    ...
</ThemeVariantScope>
<actipro:ToggleThemeButton Target="{Binding #targetScope}" />
```
