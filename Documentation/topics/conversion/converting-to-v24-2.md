---
title: "Converting to v24.2"
page-title: "Converting to v24.2 - Conversion Notes"
order: 98
---
# Converting to v24.2

## Avalonia Dependency

Updated the minimum Avalonia dependency from v11.0.7 to v11.1.0.

## MenuItem Toggle Support

Avalonia v11.1.0 adds built-in toggle support for menu items using either a checkbox or radio button.  Previously, a `CheckBox` or `RadioButton` control had to be assigned as content for the `MenuItem.Icon` property to reflect the toggle state.

Actipro Themes provided the [CheckBoxMenuIndicator](xref:@ActiproUIRoot.Themes.ControlThemeKind.CheckBoxMenuIndicator) and [RadioButtonMenuIndicator](xref:@ActiproUIRoot.Themes.ControlThemeKind.RadioButtonMenuIndicator) themes specifically for the purpose of using `CheckBox` or `RadioButton` as icons.  Actipro's native themes have been updated to support the new `MenuItem` toggle capabilities, and developers should migrate their code to use the new features as well.  The [CheckBoxMenuIndicator](xref:@ActiproUIRoot.Themes.ControlThemeKind.CheckBoxMenuIndicator) and [RadioButtonMenuIndicator](xref:@ActiproUIRoot.Themes.ControlThemeKind.RadioButtonMenuIndicator) themes are no longer necessary and will be removed in a future release.

## Removed Control Themes

Avalonia v11.1.0 defines a default control theme for `ContentControl` (including derived `UserControl`) and `NativeMenuBar`, so Actipro Themes no longer needs to provide a default control theme.  These control themes and their corresponding entires in [ControlThemeKind](xref:@ActiproUIRoot.Themes.ControlThemeKind) have been removed.