---
title: "Overview"
page-title: "Docking & MDI Layout, Globalization, and Accessibility Features"
order: 1
---
# Overview

Actipro Docking & MDI has an enormous feature set related to layouts, globalization, and accessibility.

## Layout Serialization

The full tool window layout can be saved to XML and restored later, allowing end users to retain their customized layout information across multiple application executions.

See the [Layout Serialization](layout-serialization.md) topic for more information.

## Nested Dock Sites

Dock sites can be nested, where a child [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) is placed within a document window or tool window of an ancestor [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).

See the [Nested Dock Sites](nested-dock-sites.md) topic for more information.

## Side-by-Side Dock Sites

Dock sites are completely self-contained, meaning that you can have two [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) in the same `Window` and tool windows belonging to one can't be moved into other, and vice versa.

See the [Side-by-Side Dock Sites](side-by-side-dock-sites.md) topic for more information.

## Tool Window Inner-Fill

Actipro Docking & MDI supports tool window inner-fill mode, in which all tool windows fill the entire content area of the ancestor [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).

See the [Tool Window Inner-Fill](tool-window-inner-fill.md) topic for more information.

## Programmatic Layout

This topic covers several methods for programmatically arranging and resizing the docking windows.

See the [Programmatic Layout](programmatic-layout.md) topic for more information.

## Flow Direction

Docking & MDI supports both left-to-right and right-to-left layouts via a simple property switch.

See the [Flow Direction](flow-direction.md) topic for more information.

## Localization

All strings that are displayed in the user interface can be customized and localized. @if (wpf) { In addition, all user interface text properties have the `Localizability` attribute applied to them. }

See the [Localization](localization.md) topic for more information.

@if (wpf) {
## UI Automation

This product follows the @@PlatformName accessibility model for UI automation.

See the [UI Automation](ui-automation.md) topic for more information.
}