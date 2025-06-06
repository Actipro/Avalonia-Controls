---
title: "Split Button"
page-title: "Split Button - Bars Controls"
order: 110
---
# Split Button

Split and split/toggle buttons are controls with two distinct areas.  One portion can be clicked similar to a regular button, which then executes a command or raises an event.  Another portion can be clicked to open a popup menu, similar to a popup button.

> [!NOTE]
> This topic extends the [Control Basics](control-basics.md) topic with additional information specific to the control types described below.  Please refer to the base topic for more generalized concepts that apply to all controls, including this one.

## Control Implementations

There are separate split and split/toggle button concept control implementations based on the usage context.

### Ribbon and Toolbar Contexts

Use the [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) control to implement a split non-checkable button within a ribbon or toolbar context.

If the button should be checkable, use the [BarSplitToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitToggleButton) control instead.  This control class inherits [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) and all its features, but adds toggle support.

![Screenshot](../images/split-button-large.png)
![Screenshot](../images/split-button-medium.png)
![Screenshot](../images/split-button-small.png)

*BarSplitButton examples in several variant sizes (large, medium, and small)*

@if (avalonia) {
| Specification | Details |
|-----|-----|
| Base class | [BarPopupButton](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton), which indirectly inherits native `Menu`. |
| Has key | Yes, via the [Key](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.Key) property. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.Label) property.  Auto-generated from the `Key` value if not specified. |
| Has image | Yes, via the [SmallIcon](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.SmallIcon), [MediumIcon](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.MediumIcon), and [LargeIcon](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.LargeIcon) properties. |
| Has popup | Yes. |
| Is checkable | Yes, but only when using [BarSplitToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitToggleButton). |
| Variant sizes | `Small` (image only), `Medium` (image and label), `Large` (tall size, image and multi-line label). |
| Command support | Yes, via the [Command](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.Command) property (related to the button portion) and the [PopupOpeningCommand](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.PopupOpeningCommand) property (related to the popup portion). |
| Key tip support | Yes, via the [KeyTipText](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.KeyTipText) property.  Auto-generated from the `Label` value if not specified. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | Yes, via the [CanCloneToRibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.CanCloneToRibbonQuickAccessToolBar) property. |
| [MVVM Library](../mvvm-support.md) VM | [BarSplitButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitButtonViewModel) class for regular buttons, [BarSplitToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitToggleButtonViewModel) class for toggle buttons. |
}
@if (wpf) {
| Specification | Details |
|-----|-----|
| Base class | [BarPopupButton](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton), which indirectly inherits native `Menu`. |
| Has key | Yes, via the [Key](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.Key) property. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.Label) property.  Auto-generated from the `Key` value if not specified. |
| Has image | Yes, via the [SmallImageSource](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.SmallImageSource), [MediumImageSource](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.MediumImageSource), and [LargeImageSource](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.LargeImageSource) properties. |
| Has popup | Yes. |
| Is checkable | Yes, but only when using [BarSplitToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitToggleButton). |
| Variant sizes | `Small` (image only), `Medium` (image and label), `Large` (tall size, image and multi-line label). |
| Command support | Yes, via the [Command](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.Command) property (related to the button portion) and the [PopupOpeningCommand](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.PopupOpeningCommand) property (related to the popup portion). |
| Key tip support | Yes, via the [KeyTipText](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.KeyTipText) property.  Auto-generated from the `Label` value if not specified. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | Yes, via the [CanCloneToRibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.CanCloneToRibbonQuickAccessToolBar) property. |
| UI density support | Yes, via the [UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.UserInterfaceDensity) property. |
| [MVVM Library](../mvvm-support.md) VM | [BarSplitButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitButtonViewModel) class for regular buttons, [BarSplitToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitToggleButtonViewModel) class for toggle buttons. |
}

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:StandaloneToolBar>
	<!-- Label is auto-generated from Key -->
	<actipro:BarSplitButton
		Key="Shapes"
		SmallIcon="{StaticResource ShapesIcon}"
		Command="{Binding InsertLastShapeCommand}">

		<actipro:BarSplitButton.Items>
			<actipro:BarMenuItem
				Key="Rectangle"
				Command="{Binding InsertRectangleCommand}"
				/>
		</actipro:BarSplitButton.Items>
	</actipro:BarSplitButton>
	...
</actipro:StandaloneToolBar>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:StandaloneToolBar>
	<!-- Label is auto-generated from Key -->
	<bars:BarSplitButton
		Key="Shapes"
		SmallImageSource="/Images/Shapes16.png"
		Command="{Binding InsertLastShapeCommand}">

		<bars:BarSplitButton.Items>
			<bars:BarMenuItem
				Key="Rectangle"
				Command="{Binding InsertRectangleCommand}"
				/>
		</bars:BarSplitButton.Items>
	</bars:BarSplitButton>
	...
</bars:StandaloneToolBar>
```
}

### Menu Contexts

Use the [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) control to implement a split button concept within a menu context.

The [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) class indirectly inherits the native `MenuItem` control class and therefore supports a checkable state.

![Screenshot](../images/split-button-menu.png)

*A BarSplitMenuItem example*

@if (avalonia) {
| Specification | Details |
|-----|-----|
| Base class | [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem), which indirectly inherits native `MenuItem`. |
| Has key | Yes, via the [Key](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Key) property. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Label) property.  Auto-generated from the `Key` value if not specified. |
| Has image | Yes, via the [SmallIcon](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.SmallIcon) and [LargeIcon](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.LargeIcon) properties. |
| Has popup | Yes. |
| Is checkable | Yes, when the `ToggleType` property is set to `CheckBox` or `Radio`. |
| Variant sizes | None, but has a [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) property that triggers a large height and displays an extended [Description](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Description). |
| Command support | Yes, via the `Command` property (related to the button portion) and the [PopupOpeningCommand](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.PopupOpeningCommand) property (related to the popup portion). |
| Key tip support | Yes, via the [KeyTipText](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.KeyTipText) property.  Auto-generated from the `Label` value if not specified. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | Yes, via the [CanCloneToRibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.CanCloneToRibbonQuickAccessToolBar) property. |
| [MVVM Library](../mvvm-support.md) VM | [BarSplitButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitButtonViewModel) class for non-checkable menu items, [BarSplitToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitToggleButtonViewModel) class for checkable menu items. |
}
@if (wpf) {
| Specification | Details |
|-----|-----|
| Base class | [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem), which indirectly inherits native `MenuItem`. |
| Has key | Yes, via the [Key](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Key) property. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Label) property.  Auto-generated from the `Key` value if not specified. |
| Has image | Yes, via the [SmallImageSource](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.SmallImageSource) and [LargeImageSource](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.LargeImageSource) properties. |
| Has popup | Yes. |
| Is checkable | Yes, when the `IsCheckable` property is set to `true`. |
| Variant sizes | None, but has a [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) property that triggers a large height and displays an extended [Description](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Description). |
| Command support | Yes, via the `Command` property (related to the button portion) and the [PopupOpeningCommand](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.PopupOpeningCommand) property (related to the popup portion). |
| Key tip support | Yes, via the [KeyTipText](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.KeyTipText) property.  Auto-generated from the `Label` value if not specified. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | Yes, via the [CanCloneToRibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.CanCloneToRibbonQuickAccessToolBar) property. |
| UI density support | None. |
| [MVVM Library](../mvvm-support.md) VM | [BarSplitButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitButtonViewModel) class for non-checkable menu items, [BarSplitToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitToggleButtonViewModel) class for checkable menu items. |
}

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:BarMenuFlyout>
	<!-- Label is auto-generated from Key -->
	<actipro:BarSplitMenuItem
		Key="Shapes"
		SmallIcon="{StaticResource ShapesIcon}">
		Command="{Binding InsertLastShapeCommand}">

		<actipro:BarMenuItem
			Key="Rectangle"
			Command="{Binding InsertRectangleCommand}"
			/>
	</actipro:BarSplitMenuItem>
	...
</actipro:BarMenuFlyout>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:BarContextMenu>
	<!-- Label is auto-generated from Key -->
	<bars:BarSplitMenuItem
		Key="Shapes"
		SmallImageSource="/Images/Shapes16.png">
		Command="{Binding InsertLastShapeCommand}">

		<bars:BarMenuItem
			Key="Rectangle"
			Command="{Binding InsertRectangleCommand}"
			/>
	</bars:BarSplitMenuItem>
	...
</bars:BarContextMenu>
```
}

## Appearance

There are several appearance-related properties that determine how the controls render.

### Label

The controls have a string `Label` that can be set, which is visible in UI.

The `Label` can be auto-generated based on the control's `Key` property.  For instance, a control with `Key` of `"FormatPainter"` will automatically assign `"Format Painter"` as the `Label` value.  The auto-generated default can be overridden by setting the `Label` property.

The [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[Label](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.Label) is rendered on the button when it is in a `Medium` or `Large` variant size.  When using a `Large` variant size button, the label will wrap words to two lines to minimize overall width.  In cases where a run of label text should not be broken up into two lines, use a non-breaking space character (ASCII code 160) in place of any whitespace, like this:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:BarSplitButton ... Label="Data&#160;Set" />
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:BarSplitButton ... Label="Data&#160;Set" />
```
}

The [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).[Label](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Label) is rendered on the menu item as its primary content.

### Images

The controls can display images that help identify their function.

@if (avalonia) {
All [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) instances should set a [SmallIcon](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.SmallIcon) at a minimum, which is generally used for `Small` and `Medium` variants, as well as in the [Ribbon Quick Access Toolbar](../ribbon-features/quick-access-toolbar.md) and if the control overflows to a menu.  If the button supports a `Large` variant size, it should also define a [LargeIcon](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.LargeIcon).  When the button is located on a ribbon with `Simplified` layout mode, it will try to use [MediumIcon](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.MediumIcon).

> [!TIP]
> See the [Control Basics](control-basics.md) topic for more detail on the fallback logic and custom data templates for button icons.

[BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) instances can optionally define a [SmallIcon](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.SmallIcon) that appears in the menu's icon column.  When [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) is set to create a large menu item, the [LargeIcon](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.LargeIcon) property is used instead.  When the menu item is checked, a highlight box will appear  around the image.  If no image is specified, a standard check glyph will be used in place of the image.
}
@if (wpf) {
All [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) instances should set a [SmallImageSource](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.SmallImageSource) at a minimum, which is generally used for `Small` and `Medium` variants, as well as in the [Ribbon Quick Access Toolbar](../ribbon-features/quick-access-toolbar.md) and if the control overflows to a menu.  If the button supports a `Large` variant size, it should also define a [LargeImageSource](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.LargeImageSource).  When the button has a `Spacious` UI density, it will try to use [MediumImageSource](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.MediumImageSource).

> [!TIP]
> See the [Control Basics](control-basics.md) topic for more detail on the fallback logic for button images.

[BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) instances can optionally define a [SmallImageSource](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.SmallImageSource) that appears in the menu's icon column.  When [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) is set to create a large menu item, the [LargeImageSource](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.LargeImageSource) property is used instead.  When the menu item is checked, a highlight box will appear  around the image.  If no image is specified, a standard check glyph will be used in place of the image.
}

### Description (BarSplitMenuItem only)

When [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) is set to create a large menu item, the [Description](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Description) property's string value is displayed under the menu item's bold label as an extended description.

### Title

An optional string [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[Title](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.Title) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).[Title](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Title) can be specified, which are intended to override the control's `Label` when displayed in screen tips and customization UI.

### Variant Sizes

[BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) supports multiple variant sizes via its [VariantSize](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.VariantSize) property.  This feature can be used in conjunction with [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) within a ribbon in `Classic` layout mode to achieve various button layouts as available width changes.

When a ribbon is in `Simplified` layout mode, the [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) control will render in a `Small` variant size by default and will collapse to an overflow menu when necessary.  The [ToolBarItemVariantBehavior](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.ToolBarItemVariantBehavior) property can be set to `All` to show a label.  Alternatively, the [ToolBarItemCollapseBehavior](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.ToolBarItemCollapseBehavior) property can be set to `Always` to always have the button in the overflow menu.

While [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) doesn't support variant sizes, it does have a [UseLargeSize](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.UseLargeSize) property that can be set to show a large version of the menu item.  This large version uses a large image and can display an extended [Description](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Description).

See the [Resizing and Variants](../ribbon-features/resizing.md) topic for more information on ribbon's variant sizing features.

@if (wpf) {
### User Interface Density (BarSplitButton only)

The [UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.UserInterfaceDensity) property can alter the appearance of the button, such as its size, padding, and image used.  This property is not generally set on the button instance itself, and is instead meant to be set on the root bar control to inherit down, such as with the [Ribbon.UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.Ribbon.UserInterfaceDensity) property.
}

### Checked State (BarToggleSplitButton and BarSplitMenuItem only)

Whereas [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) implements a non-toggleable button, the [BarSplitToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitToggleButton) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) controls both support a checked state.

Set the [BarSplitToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitToggleButton).[IsChecked](xref:@ActiproUIRoot.Controls.Bars.BarSplitToggleButton.IsChecked) or [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).`IsChecked` property to `true` to render the control in a checked state.

## Key Tips

The controls support key tips.  When a control's key tip is accessed, the popup menu is opened.

The `KeyTipText` can be auto-generated based on the control's `Label` property.  For instance, a control with `Label` of `"Copy"` will automatically assign `"C"` as the `KeyTipText` value.  The auto-generated default can be overridden by setting the `KeyTipText` property.

The [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[KeyTipText](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.KeyTipText) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).[KeyTipText](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.KeyTipText) properties designate the key tip text to use for the control.

See the [Key Tips](../ribbon-features/key-tips.md) topic for more information on key tips.

## Commands and Events

The `ICommand` in the [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[Command](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.Command) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).`Command` properties is executed when the control's button portion is clicked.  The command's can-execute result determines the enabled state of the control's button portion.

A [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[Click](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton.Click) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).`Click` event is also raised in these scenarios.

The [PopupOpeningCommand](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase.PopupOpeningCommand) command, if assigned, is executed prior to the popup opening.  This command can be handled in a view model, allowing for a MVVM way to update the items on the popup prior to display.  The command's can-execute result determines the enabled state of the control's popup portion.

See the [Using Commands](using-commands.md) topic for more information on commands.

## Input Gesture Text

The control can have input gesture text associated with it that describes a related keyboard shortcut, and is displayed in the screen tip for the control or in the menu item itself.

@if (avalonia) {
Input gesture text can be specified in the [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[InputGesture](xref:@ActiproUIRoot.Controls.Bars.BarButton.InputGesture) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).`InputGesture` properties.

Input gesture text may be hidden altogether in UI by setting the [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[IsInputGestureTextVisible](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.IsInputGestureTextVisible) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).[IsInputGestureTextVisible](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.IsInputGestureTextVisible) properties to `false`.
}
@if (wpf) {
This input gesture text is automatically derived from commands that inherit `RoutedCommand` and have a `KeyGesture` set.
Or input gesture text can be specified in the [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[InputGestureText](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton.InputGestureText) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).`InputGestureText` properties.

Input gesture text may be hidden altogether in UI by setting the [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[IsInputGestureTextVisible](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton.IsInputGestureTextVisible) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).[IsInputGestureTextVisible](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.IsInputGestureTextVisible) properties to `false`.
}

## Screen Tips

The controls support screen tips, which are formatted tool tips.

The control's `Title` is used as the default screen tip header, falling back to `Label` if no `Title` is available.  The [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[ScreenTipHeader](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.ScreenTipHeader) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).[ScreenTipHeader](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.ScreenTipHeader) properties can override the default screen tip header value if desired.

If the control's @if (avalonia) { `ToolTip.Tip` }@if (wpf) { `ToolTip` } property is set to a value that doesn't derive from a native `ToolTip` control, such as a string, the value will be used in the screen tip's content area, with the screen tip header becoming bold.  The screen tip's content area is where extended descriptions are displayed.

If the optional [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton).[ScreenTipFooter](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton.ScreenTipFooter) and [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem).[ScreenTipFooter](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.ScreenTipFooter) properties are specified, they will appear in a footer area of the screen tip.

See the [Screen Tips](../ribbon-features/screen-tips.md) topic for more information on screen tips.

## MVVM Support

The optional companion [MVVM Library](../mvvm-support.md) defines [BarSplitButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitButtonViewModel) (regular buttons) and [BarSplitToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitToggleButtonViewModel) (toggle buttons) classes that are intended to be used as view models for split buttons.

These view model classes map over to the appropriate view controls described above based on usage context and configure all necessary bindings between the view models and the view controls.

> [!TIP]
> See the [MVVM Support](../mvvm-support.md) topic for more information on how to use the library's view models and view templates to create and manage your application's bars controls with MVVM techniques.
