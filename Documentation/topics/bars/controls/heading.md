---
title: "Heading"
page-title: "Heading - Bars Controls"
order: 135
---
# Heading

Headings are simplistic controls that render a text heading.

> [!NOTE]
> This topic extends the [Control Basics](control-basics.md) topic with additional information specific to the control types described below.  Please refer to the base topic for more generalized concepts that apply to all controls, including this one.

## Control Implementations

There are separate separator concept control implementations based on the usage context.

### Ribbon and Toolbar Contexts

Headings are not available in ribbon and toolbar contexts.

### Menu Contexts

Use the [BarMenuHeading](xref:@ActiproUIRoot.Controls.Bars.BarMenuHeading) control to implement a heading within a menu context.

![Screenshot](../images/menu-with-heading.png)

*A heading at the top of a menu*

@if (avalonia) {
| Specification | Details |
|-----|-----|
| Base class | Native `Separator`. |
| Has key | No. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.BarMenuHeading.Label) property. |
| Has image | No. |
| Has popup | No. |
| Is checkable | No. |
| Variant sizes | None. |
| Command support | None. |
| Key tip support | None. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | None. |
| [MVVM Library](../mvvm-support.md) VM | [BarHeadingViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarHeadingViewModel) class. |
}
@if (wpf) {
| Specification | Details |
|-----|-----|
| Base class | Native `MenuItem`. |
| Has key | No. |
| Has label | Yes, via the [Label](xref:@ActiproUIRoot.Controls.Bars.BarMenuHeading.Label) property. |
| Has image | No. |
| Has popup | No. |
| Is checkable | No. |
| Variant sizes | None. |
| Command support | None. |
| Key tip support | None. |
| [Ribbon QAT](../ribbon-features/quick-access-toolbar.md) support | None. |
| UI density support | None. |
| [MVVM Library](../mvvm-support.md) VM | [BarHeadingViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarHeadingViewModel) class. |
}

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:BarContextMenu>
	<actipro:BarMenuHeading Label="Clipboard Operations" />
	<actipro:BarMenuItem Key="Cut" />
	<actipro:BarMenuItem Key="Copy" />
	<actipro:BarMenuItem Key="Paste" />
	...
</actipro:BarContextMenu>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<bars:BarContextMenu>
	<bars:BarMenuHeading Label="Clipboard Operations" />
	<bars:BarMenuItem Key="Cut" />
	<bars:BarMenuItem Key="Copy" />
	<bars:BarMenuItem Key="Paste" />
	...
</bars:BarContextMenu>
```
}

## MVVM Support

The optional companion [MVVM Library](../mvvm-support.md) defines a [BarHeadingViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarHeadingViewModel) class that is intended to be used as a view model for headings.

This view model class maps over to the appropriate view controls described above based on usage context and configure all necessary bindings between the view models and the view controls.

> [!TIP]
> See the [MVVM Support](../mvvm-support.md) topic for more information on how to use the library's view models and view templates to create and manage your application's bars controls with MVVM techniques.
