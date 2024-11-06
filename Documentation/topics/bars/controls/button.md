---
title: "Button"
page-title: "Button - Bars Controls"
order: 100
---
# Button

Regular and toggle buttons are controls that can be clicked, which then executes a command or raises an event.

> [!NOTE]
> This topic extends the [Control Basics](control-basics.md) topic with additional information specific to the control types described below.  Please refer to the base topic for more generalized concepts that apply to all controls, including this one.

## Control Implementations

There are separate regular and toggle button concept control implementations based on the usage context.

### Ribbon and Toolbar Contexts

Use the [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) control to implement a regular non-checkable button within a ribbon or toolbar context.

If the button should be checkable, use the [BarToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarToggleButton) control instead.  This control class inherits [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) and all its features but adds toggle support.

![Screenshot](../images/button-large.png)
![Screenshot](../images/button-medium.png)
![Screenshot](../images/button-small.png)

*BarButton examples in several variant sizes (large, medium, and small)*

@if (avalonia) {
| Specification | Details |
|-----|-----|
| Base class | Native `Button`. |
| Has key | Yes, via the [Key](xref:@ActiproUIRoot.Controls.Bars.BarButton.Key) property. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.BarButton.Label) property.  Auto-generated from the `Key` value if not specified. |
| Has image | Yes, via the [SmallIcon](xref:@ActiproUIRoot.Controls.Bars.BarButton.SmallIcon), [MediumIcon](xref:@ActiproUIRoot.Controls.Bars.BarButton.MediumIcon), and [LargeIcon](xref:@ActiproUIRoot.Controls.Bars.BarButton.LargeIcon) properties. |
| Has popup | No. |
| Is checkable | Yes, but only when using [BarToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarToggleButton). |
| Variant sizes | `Small` (image only), `Medium` (image and label), `Large` (tall size, image and multi-line label). |
| Command support | Yes, via the `Command` property. |
| Key tip support | Yes, via the [KeyTipText](xref:@ActiproUIRoot.Controls.Bars.BarButton.KeyTipText) property.  Auto-generated from the `Label` value if not specified. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | Yes, via the [CanCloneToRibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.BarButton.CanCloneToRibbonQuickAccessToolBar) property. |
| [MVVM Library](../mvvm-support.md) VM | [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) class for regular buttons, [BarToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarToggleButtonViewModel) class for toggle buttons. |
}
@if (wpf) {
| Specification | Details |
|-----|-----|
| Base class | Native `Button`. |
| Has key | Yes, via the [Key](xref:@ActiproUIRoot.Controls.Bars.BarButton.Key) property. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.BarButton.Label) property.  Auto-generated from the `Key` value if not specified. |
| Has image | Yes, via the [SmallImageSource](xref:@ActiproUIRoot.Controls.Bars.BarButton.SmallImageSource), [MediumImageSource](xref:@ActiproUIRoot.Controls.Bars.BarButton.MediumImageSource), and [LargeImageSource](xref:@ActiproUIRoot.Controls.Bars.BarButton.LargeImageSource) properties. |
| Has popup | No. |
| Is checkable | Yes, but only when using [BarToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarToggleButton). |
| Variant sizes | `Small` (image only), `Medium` (image and label), `Large` (tall size, image and multi-line label). |
| Command support | Yes, via the `Command` property. |
| Key tip support | Yes, via the [KeyTipText](xref:@ActiproUIRoot.Controls.Bars.BarButton.KeyTipText) property.  Auto-generated from the `Label` value if not specified. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | Yes, via the [CanCloneToRibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.BarButton.CanCloneToRibbonQuickAccessToolBar) property. |
| UI density support | Yes, via the [UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.BarButton.UserInterfaceDensity) property. |
| [MVVM Library](../mvvm-support.md) VM | [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) class for regular buttons, [BarToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarToggleButtonViewModel) class for toggle buttons. |
}

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:StandaloneToolBar>
	<!-- Label is auto-generated from Key, but we want to override the auto-generated KeyTipText -->
	<actipro:BarButton
		Key="Undo"
		KeyTipText="AZ"
		SmallIcon="{StaticResource UndoIcon}"
		Command="{Binding UndoCommand}"
		/>
	...
</actipro:StandaloneToolBar>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:StandaloneToolBar>
	<!-- Label is auto-generated from Key, but we want to override the auto-generated KeyTipText -->
	<bars:BarButton
		Key="Undo"
		KeyTipText="AZ"
		SmallImageSource="/Images/Undo16.png"
		Command="{Binding UndoCommand}"
		/>
	...
</bars:StandaloneToolBar>
```
}

### Menu Contexts

Use the [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) control to implement a regular or toggle button concept within a menu context.

![Screenshot](../images/button-menu.png)

*A BarMenuItem example*

@if (avalonia) {
| Specification | Details |
|-----|-----|
| Base class | Native `MenuItem`. |
| Has key | Yes, via the [Key](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Key) property. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Label) property.  Auto-generated from the `Key` value if not specified. |
| Has image | Yes, via the [SmallIcon](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.SmallIcon) and [LargeIcon](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.LargeIcon) properties. |
| Has popup | Not when a button concept is desired. |
| Is checkable | Yes, when the `ToggleType` property is set to `CheckBox` or `Radio`. |
| Variant sizes | None, but has a [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) property that triggers a large height and displays an extended [Description](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Description). |
| Command support | Yes, via the `Command` property. |
| Key tip support | Yes, via the [KeyTipText](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.KeyTipText) property.  Auto-generated from the `Label` value if not specified. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | Yes, via the [CanCloneToRibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.CanCloneToRibbonQuickAccessToolBar) property. |
| [MVVM Library](../mvvm-support.md) VM | [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) class for non-checkable menu items, [BarToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarToggleButtonViewModel) class for checkable menu items. |
}
@if (wpf) {
| Specification | Details |
|-----|-----|
| Base class | Native `MenuItem`. |
| Has key | Yes, via the [Key](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Key) property. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Label) property.  Auto-generated from the `Key` value if not specified. |
| Has image | Yes, via the [SmallImageSource](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.SmallImageSource) and [LargeImageSource](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.LargeImageSource) properties. |
| Has popup | Not when a button concept is desired. |
| Is checkable | Yes, when the `IsCheckable` property is set to `true`. |
| Variant sizes | None, but has a [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) property that triggers a large height and displays an extended [Description](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Description). |
| Command support | Yes, via the `Command` property. |
| Key tip support | Yes, via the [KeyTipText](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.KeyTipText) property.  Auto-generated from the `Label` value if not specified. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | Yes, via the [CanCloneToRibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.CanCloneToRibbonQuickAccessToolBar) property. |
| UI density support | None. |
| [MVVM Library](../mvvm-support.md) VM | [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) class for non-checkable menu items, [BarToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarToggleButtonViewModel) class for checkable menu items. |
}

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:BarContextMenu>
	<!-- Label is auto-generated from Key, but we want to override the auto-generated KeyTipText -->
	<actipro:BarMenuItem
		Key="Undo"
		KeyTipText="AZ"
		SmallIcon="{StaticResource UndoIcon}"
		Command="{Binding UndoCommand}"
		/>
	...
</actipro:BarContextMenu>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:BarContextMenu>
	<!-- Label is auto-generated from Key, but we want to override the auto-generated KeyTipText -->
	<bars:BarMenuItem
		Key="Undo"
		KeyTipText="AZ"
		SmallImageSource="/Images/Undo16.png"
		Command="{Binding UndoCommand}"
		/>
	...
</bars:BarContextMenu>
```
}

## Appearance

There are several appearance-related properties that determine how the controls render.

### Label

The controls have a string `Label` that can be set, which is visible in UI.

The `Label` can be auto-generated based on the control's `Key` property.  For instance, a control with `Key` of `"FormatPainter"` will automatically assign `"Format Painter"` as the `Label` value.  The auto-generated default can be overridden by setting the `Label` property.

The [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[Label](xref:@ActiproUIRoot.Controls.Bars.BarButton.Label) is rendered on the button when it is in a `Medium` or `Large` variant size.  When using a `Large` variant size button, the label will wrap words to two lines to minimize overall width.  In cases where a run of label text should not be broken up into two lines, use a non-breaking space character (ASCII code 160) in place of any whitespace, like this:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:BarButton ... Label="Data&#160;Set" />
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:BarButton ... Label="Data&#160;Set" />
```
}

The [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[Label](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Label) is rendered on the menu item as its primary content.

### Images

The controls can display images that help identify their function.

@if (avalonia) {
All [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) instances should set a [SmallIcon](xref:@ActiproUIRoot.Controls.Bars.BarButton.SmallIcon) at a minimum, which is generally used for `Small` and `Medium` variants, as well as in the [Ribbon Quick Access Toolbar](../ribbon-features/quick-access-toolbar.md) and if the control overflows to a menu.  If the button supports a `Large` variant size, it should also define a [LargeIcon](xref:@ActiproUIRoot.Controls.Bars.BarButton.LargeIcon).  When the button is located on a ribbon with `Simplified` layout mode, it will try to use [MediumIcon](xref:@ActiproUIRoot.Controls.Bars.BarButton.MediumIcon).

> [!TIP]
> See the [Control Basics](control-basics.md) topic for more detail on the fallback logic and custom data templates for button icons.

[BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) instances can optionally define a [SmallIcon](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.SmallIcon) that appears in the menu's icon column.  When [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) is set to create a large menu item, the [LargeIcon](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.LargeIcon) property is used instead.  When the menu item is checked, a highlight box will appear around the image.  If no image is specified, a standard check glyph will be used in place of the image.}
@if (wpf) {
All [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) instances should set a [SmallImageSource](xref:@ActiproUIRoot.Controls.Bars.BarButton.SmallImageSource) at a minimum, which is generally used for `Small` and `Medium` variants, as well as in the [Ribbon Quick Access Toolbar](../ribbon-features/quick-access-toolbar.md) and if the control overflows to a menu.  If the button supports a `Large` variant size, it should also define a [LargeImageSource](xref:@ActiproUIRoot.Controls.Bars.BarButton.LargeImageSource).  When the button has a `Spacious` UI density, it will try to use [MediumImageSource](xref:@ActiproUIRoot.Controls.Bars.BarButton.MediumImageSource).

> [!TIP]
> See the [Control Basics](control-basics.md) topic for more detail on the fallback logic for button images.

[BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) instances can optionally define a [SmallImageSource](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.SmallImageSource) that appears in the menu's icon column.  When [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) is set to create a large menu item, the [LargeImageSource](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.LargeImageSource) property is used instead.  When the menu item is checked, a highlight box will appear around the image.  If no image is specified, a standard check glyph will be used in place of the image.
}

### Description (BarMenuItem only)

When [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) is set to create a large menu item, the [Description](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Description) property's string value is displayed under the menu item's bold label as an extended description.

### Title

An optional string [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[Title](xref:@ActiproUIRoot.Controls.Bars.BarButton.Title) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[Title](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Title) can be specified, which are intended to override the control's `Label` when displayed in screen tips and customization UI.

### Variant Sizes

[BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) supports multiple variant sizes via its [VariantSize](xref:@ActiproUIRoot.Controls.Bars.BarButton.VariantSize) property.  This feature can be used in conjunction with [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) within a ribbon in `Classic` layout mode to achieve various button layouts as available width changes.

When a ribbon is in `Simplified` layout mode, the [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) control will render in a `Small` variant size by default and will collapse to an overflow menu when necessary.  The [ToolBarItemVariantBehavior](xref:@ActiproUIRoot.Controls.Bars.BarButton.ToolBarItemVariantBehavior) property can be set to `All` to show a label.  Alternatively, the [ToolBarItemCollapseBehavior](xref:@ActiproUIRoot.Controls.Bars.BarButton.ToolBarItemCollapseBehavior) property can be set to `Always` to always have the button in the overflow menu.

While [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) doesn't support variant sizes, it does have a [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) property that can be set to show a large version of the menu item.  This large version uses a large image and can display an extended [Description](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Description).

See the [Resizing and Variants](../ribbon-features/resizing.md) topic for more information on ribbon's variant sizing features.

@if (wpf) {
### User Interface Density (BarButton only)

The [UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.BarButton.UserInterfaceDensity) property can alter the appearance of the button, such as its size, padding, and image used.  This property is not generally set on the button instance itself, and is instead meant to be set on the root bar control to inherit down, such as with the [Ribbon.UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.Ribbon.UserInterfaceDensity) property.
}

### Checked State (BarToggleButton and BarMenuItem only)

Whereas [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) implements a non-toggleable button, the [BarToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarToggleButton) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) controls both support a checked state.

Set the [BarToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarToggleButton).[IsChecked](xref:@ActiproUIRoot.Controls.Bars.BarToggleButton.IsChecked) or [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).`IsChecked` property to `true` to render the control in a checked state.

## Key Tips

The controls support key tips.  When a control's key tip is accessed, a click is simulated.

The `KeyTipText` can be auto-generated based on the control's `Label` property.  For instance, a control with `Label` of `"Copy"` will automatically assign `"C"` as the `KeyTipText` value.  The auto-generated default can be overridden by setting the `KeyTipText` property.

The [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[KeyTipText](xref:@ActiproUIRoot.Controls.Bars.BarButton.KeyTipText) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[KeyTipText](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.KeyTipText) properties designate the key tip text to use for the control.

See the [Key Tips](../ribbon-features/key-tips.md) topic for more information on key tips.

## Commands and Events

The `ICommand` in the [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).`Command` and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).`Command` properties is executed when the control is clicked or a key tip accesses the control.

A [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).`Click` and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).`Click` event is also raised in these scenarios.

See the [Using Commands](using-commands.md) topic for more information on commands.

## Input Gesture Text

The control can have input gesture text associated with it that describes a related keyboard shortcut, and is displayed in the screen tip for the control or in the menu item itself.

@if (avalonia) {
Input gesture text can be specified in the [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[InputGesture](xref:@ActiproUIRoot.Controls.Bars.BarButton.InputGesture) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).`InputGesture` properties.

Input gesture text may be hidden altogether in UI by setting the [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[IsInputGestureTextVisible](xref:@ActiproUIRoot.Controls.Bars.BarButton.IsInputGestureTextVisible) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[IsInputGestureTextVisible](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.IsInputGestureTextVisible) properties to `false`.
}
@if (wpf) {
This input gesture text is automatically derived from commands that inherit `RoutedCommand` and have a `KeyGesture` set.
Or input gesture text can be specified in the [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[InputGestureText](xref:@ActiproUIRoot.Controls.Bars.BarButton.InputGestureText) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).`InputGestureText` properties.

Input gesture text may be hidden altogether in UI by setting the [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[IsInputGestureTextVisible](xref:@ActiproUIRoot.Controls.Bars.BarButton.IsInputGestureTextVisible) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[IsInputGestureTextVisible](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.IsInputGestureTextVisible) properties to `false`.
}

## Screen Tips

The controls support screen tips, which are formatted tool tips.

The control's `Title` is used as the default screen tip header, falling back to `Label` if no `Title` is available.  The [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[ScreenTipHeader](xref:@ActiproUIRoot.Controls.Bars.BarButton.ScreenTipHeader) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[ScreenTipHeader](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.ScreenTipHeader) properties can override the default screen tip header value if desired.

If the control's @if (avalonia) { `ToolTip.Tip` }@if (wpf) { `ToolTip` } property is set to a value that doesn't derive from a native `ToolTip` control, such as a string, the value will be used in the screen tip's content area, with the screen tip header becoming bold.  The screen tip's content area is where extended descriptions are displayed.

If the optional [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).[ScreenTipFooter](xref:@ActiproUIRoot.Controls.Bars.BarButton.ScreenTipFooter) and [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[ScreenTipFooter](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.ScreenTipFooter) properties are specified, they will appear in a footer area of the screen tip.

See the [Screen Tips](../ribbon-features/screen-tips.md) topic for more information on screen tips.

## MVVM Support

The optional companion [MVVM Library](../mvvm-support.md) defines [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) (regular buttons) and [BarToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarToggleButtonViewModel) (toggle buttons) classes that are intended to be used as view models for buttons.

These view model classes map over to the appropriate view controls described above based on usage context and configure all necessary bindings between the view models and the view controls.

> [!TIP]
> See the [MVVM Support](../mvvm-support.md) topic for more information on how to use the library's view models and view templates to create and manage your application's bars controls with MVVM techniques.
