---
title: "Layout Modes and Density"
page-title: "Layout Modes and Density - Ribbon Features"
order: 10
---
# Layout Modes and Density

Ribbon supports multiple layout modes and user interface densities to easily achieve the desired look and feel for your application.

## Layout Modes

Ribbon supports two different layout modes: `Classic` and `Simplified`.  Use the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[LayoutMode](xref:@ActiproUIRoot.Controls.Bars.Ribbon.LayoutMode) property to set the desired [RibbonLayoutMode](xref:@ActiproUIRoot.Controls.Bars.RibbonLayoutMode).

The ribbon can switch between layout modes at run-time without any change in configuration.  When allowed, users can use the [ribbon options button](options-button.md) to change layout modes.  Set the  [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[CanChangeLayoutMode](xref:@ActiproUIRoot.Controls.Bars.Ribbon.CanChangeLayoutMode) property to `false` to prevent the user from changing layout modes.

### Classic

![Screenshot](../images/ribbon-layout-classic.png)

*Ribbon displayed in classic layout mode*

The `Classic` layout mode, first introduced with Office 2007, uses a taller ribbon with a combination of large buttons and multi-row groups of controls that dynamically adjust to fill as much or as little space as is available.

### Simplified

![Screenshot](../images/ribbon-layout-simplified.png)

*Ribbon displayed in simplified layout mode*

The `Simplified` layout mode is a modern refinement of the ribbon that appears more like a traditional toolbar with a single row of controls but can still dynamically adjust to fill the available space. When necessary, controls that do not have room to appear on the primary ribbon are moved to an overflow menu.

@if (wpf) {
> [!NOTE]
> It is recommended that when using the `Simplified` layout mode, set the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.Ribbon.UserInterfaceDensity) property to `Spacious`, which is more touch-friendly than the `Compact` default.  The `Spacious` density will also use medium size images when available in place of small size images.
}

@if (avalonia) {
> [!WARNING]
> The overflow menu used by the `Simplified` layout requires cloning controls from the ribbon to an equivalent overflowed menu item. This cloning process is limited when using controls are defined using XAML.  Refer to the [XAML vs. MVVM Configuration](../configuration.md) topic for additional details.
}

> [!TIP]
> See the "Layout Modes" Bars Ribbon QuickStart of the Sample Browser application for a demonstration of layout modes and user options.

## User Interface Density

@if (avalonia) {
Actipro Ribbon works with [Actipro Themes](../../themes/index.md) to support multiple levels of density that adjust the padding of user interface elements.  See the [User Interface Density](../../themes/user-interface-density.md) topic of Actipro Themes for more information.
}
@if (wpf) {
Actipro Ribbon supports multiple levels of density that adjust the padding of user interface elements.  Set the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.Ribbon.UserInterfaceDensity) property to one of the following [UserInterfaceDensity](xref:@ActiproUIRoot.Themes.UserInterfaceDensity) values.
- `Compact` is the default and is best for traditional mouse interactions such as when using [RibbonLayoutMode](xref:@ActiproUIRoot.Controls.Bars.RibbonLayoutMode).[Classic](xref:@ActiproUIRoot.Controls.Bars.RibbonLayoutMode.Classic).
- `Spacious` is great for touch interactions and should generally be set for [RibbonLayoutMode](xref:@ActiproUIRoot.Controls.Bars.RibbonLayoutMode).[Simplified](xref:@ActiproUIRoot.Controls.Bars.RibbonLayoutMode.Simplified).
- `Normal` is a little in-between `Compact` and `Spacious` for a more balanced approach.

> [!TIP]
> See the "User Interface Density" Bars Ribbon QuickStart of the Sample Browser application for a demonstration of different density values applied to classic and simplified ribbons.
}