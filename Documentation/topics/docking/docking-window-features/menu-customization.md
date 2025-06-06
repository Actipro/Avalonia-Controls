---
title: "Menu Customization"
page-title: "Menu Customization - Docking & MDI Docking Window Features"
order: 20
---
# Menu Customization

All menus displayed by this product can be customized or replaced entirely before they are shown to the end user.

## The MenuOpening Event

The customization of menus is handled via the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[MenuOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.MenuOpening) event.  This event is raised whenever a menu is displayed for a docking window, when an options button is pressed for a docking window, etc.

The event arguments that are passed are type [DockingMenuEventArgs](xref:@ActiproUIRoot.Controls.Docking.DockingMenuEventArgs), which indicates the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) for which the event was fired.  The [DockingMenuEventArgs](xref:@ActiproUIRoot.Controls.Docking.DockingMenuEventArgs).[Kind](xref:@ActiproUIRoot.Controls.Docking.DockingMenuEventArgs.Kind) property supplies a [DockingMenuKind](xref:@ActiproUIRoot.Controls.Docking.DockingMenuKind) value that can be used to determine the type of menu being constructed.

@if (avalonia) {

The event arguments passes a pre-configured `MenuFlyout` in its [Menu](xref:@ActiproUIRoot.Controls.Docking.DockingMenuEventArgs.Menu) property.

}

@if (wpf) {

The event arguments passes a pre-configured `ContextMenu` in its [Menu](xref:@ActiproUIRoot.Controls.Docking.DockingMenuEventArgs.Menu) property.

}

## Customizing Individual Menu Items

As mentioned above, the event argument's [Menu](xref:@ActiproUIRoot.Controls.Docking.DockingMenuEventArgs.Menu) property includes all the menu items, already initialized.

You can customize the items in that menu, and add or remove new ones as needed.

## Using a Different Context Menu

To use a completely different menu, simply set the new menu to the event argument's [Menu](xref:@ActiproUIRoot.Controls.Docking.DockingMenuEventArgs.Menu) property.  The alternate menu you specify will be displayed instead.

@if (wpf) {

## Context Menu Styles

You can apply a `Style` to the context menu if you have one that matches the rest of your application.  Note that since our default context menus are plain WPF `ContextMenu` controls, they will automatically pick up any `ContextMenu` styles that you set for your application.

}
