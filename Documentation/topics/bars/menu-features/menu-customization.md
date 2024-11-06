---
title: "Menu Customization"
page-title: "Menu Customization - Menu Features"
order: 20
---
# Menu Customization

Bars controls like [Ribbon](../ribbon-features/index.md) and [Standalone Toolbars](../toolbar-features/standalone-toolbars.md) define built-in menus and/or auto-generated context menus to support the default functionality of the control.  These menus can be easily customized to add, remove, or update the default menu items.

## MenuOpening Event

Each root control defines a `MenuOpening` event ([Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[MenuOpening](xref:@ActiproUIRoot.Controls.Bars.Ribbon.MenuOpening) and [StandaloneToolBar](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar).[MenuOpening](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar.MenuOpening)) that is raised with [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).  Listen for this event to be raised and the associated [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Menu](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Menu) can be customized before it is displayed.

### Menu Kind

Inspect the [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Kind](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Kind) property to determine the menu associated with the event. This property value is an enum of type [BarMenuKind](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind) that clearly designates the purpose of the menu. For example, the options menu of a [Ribbon](../ribbon-features/index.md) will have a `Kind` of [BarMenuKind](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind).[RibbonOptionsButtonMenu](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind.RibbonOptionsButtonMenu).

### Owner

Some menus, like a [BarMenuKind](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind).[ControlContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind.ControlContextMenu), will set the [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Owner](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Owner) property to the relevant control associated with the menu.  For most menus, this property will be `null`.

## Customizing Default Menus

Several button controls display a default menu, like the **Customize** menu of the [Quick Access ToolBar](../ribbon-features/quick-access-toolbar.md) or the **Overflow** menu of [Standalone Toolbars](../toolbar-features/standalone-toolbars.md).

These button controls are typically implementations of [BarPopupButtonBase](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarPopupButtonBase) which, itself, derives from `Menu`.  Before the popup of the button is displayed, the `MenuOpening` event is raised and can be intercepted to customize these menus.

For example, to customize the **Customize** menu of the [Quick Access ToolBar](../ribbon-features/quick-access-toolbar.md), listen for the  [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[MenuOpening](xref:@ActiproUIRoot.Controls.Bars.Ribbon.MenuOpening) event.  When raised for the **Customize** menu...

- [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Kind](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Kind) will be set to the value [BarMenuKind](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind).[RibbonQuickAccessToolBarCustomizeButtonMenu](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind.RibbonQuickAccessToolBarCustomizeButtonMenu),
- [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Menu](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Menu) will be set to an instance of [RibbonQuickAccessToolBarCustomizeButton](xref:@ActiproUIRoot.Controls.Bars.Primitives.RibbonQuickAccessToolBarCustomizeButton) (which derives from `Menu`), and
- [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Owner](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Owner) will be `null` since the [Menu](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Menu), itself, is the owner.

The [RibbonQuickAccessToolBarCustomizeButton](xref:@ActiproUIRoot.Controls.Bars.Primitives.RibbonQuickAccessToolBarCustomizeButton).`Items` collection will be pre-populated with types like [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) and [BarMenuSeparator](xref:@ActiproUIRoot.Controls.Bars.BarMenuSeparator) that define the default items on the menu.

Existing menu items can be modified/removed, and new menu items can be added.  The following code shows an event handler for the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[MenuOpening](xref:@ActiproUIRoot.Controls.Bars.Ribbon.MenuOpening) event that updates the **Customize** menu to add an additional menu item (via a `CreateCustomMenuItem` method that you would create):

```csharp
private void OnRibbonMenuOpening(object? sender, BarMenuEventArgs e) {
	// Test for the QAT customize menu
	if (e.Kind == BarMenuKind.RibbonQuickAccessToolBarCustomizeButtonMenu
		&& e.Menu != null) {

		// Add a menu item returned from the custom function
		e.Menu.Items.Add(CreateCustomMenuItem());
	}
}
```

> [!IMPORTANT]
> See details below about blocking a context menu from being allowed on dynamic menu items.

## Customizing a Ribbon Context Menu

@if (avalonia) {
The ribbon control auto-generates context menu flyouts for most of the controls in it unless a control has a context menu explicitly assigned to it.  The auto-generated menu contains items for toggling the visibility of the quick access toolbar, minimizing the ribbon, etc.  However, many times it is useful to add custom menu items to a specific control or to certain types of controls.

You can achieve this programmatically by listening for the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[MenuOpening](xref:@ActiproUIRoot.Controls.Bars.Ribbon.MenuOpening) event.  When raised for the context menu of a control...

- [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Kind](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Kind) will be set to the value [BarMenuKind](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind).[ControlContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind.ControlContextMenu),
- [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Menu](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Menu) will be initialized with a [BarMenuFlyout](xref:@ActiproUIRoot.Controls.Bars.BarMenuFlyout), and
- [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Owner](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Owner) will be set to the control whose context menu is opening.

The [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).`Items` collection will be pre-populated with types like [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) and [BarMenuSeparator](xref:@ActiproUIRoot.Controls.Bars.BarMenuSeparator) that define the default context menu.

Existing menu items can be modified/removed, and new menu items can be added.  The following code shows an event handler for the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[MenuOpening](xref:@ActiproUIRoot.Controls.Bars.Ribbon.MenuOpening) event that updates the context menu to add an additional menu item (via a `CreateCustomMenuItem` method that you would create):

```csharp
private void OnRibbonMenuOpening(object? sender, BarMenuEventArgs e) {
	// Test for a control context menu
	if (e.Kind == BarMenuKind.ControlContextMenu
		&& e.Owner != null
		&& e.Menu != null) {

		// Add a menu item returned from the custom function
		e.Items.Add(CreateCustomMenuItem());
	}
}
```
}
@if (wpf) {
The ribbon control auto-generates context menus for most of the controls in it unless a control has a `ContextMenu` explicitly assigned to it.  The auto-generated menu contains items for toggling the visibility of the quick access toolbar, minimizing the ribbon, etc.  However, many times it is useful to add custom menu items to a specific control or to certain types of controls.

You can achieve this programmatically by listening for the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[MenuOpening](xref:@ActiproUIRoot.Controls.Bars.Ribbon.MenuOpening) event.  When raised for the context menu of a control...

- [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Kind](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Kind) will be set to the value [BarMenuKind](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind).[ControlContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarMenuKind.ControlContextMenu),
- [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Menu](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Menu) will be initialized with a [BarContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarContextMenu), and
- [BarMenuEventArgs](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs).[Owner](xref:@ActiproUIRoot.Controls.Bars.BarMenuEventArgs.Owner) will be set to the control whose context menu is opening.

The [BarContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarContextMenu).`Items` collection will be pre-populated with types like [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) and [BarMenuSeparator](xref:@ActiproUIRoot.Controls.Bars.BarMenuSeparator) that define the default context menu.

Existing menu items can be modified/removed, and new menu items can be added.  The following code shows an event handler for the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[MenuOpening](xref:@ActiproUIRoot.Controls.Bars.Ribbon.MenuOpening) event that updates the context menu to add an additional menu item (via a `CreateCustomMenuItem` method that you would create):

```csharp
private void OnRibbonMenuOpening(object? sender, BarMenuEventArgs e) {
	// Test for a control context menu
	if (e.Kind == BarMenuKind.ControlContextMenu
		&& e.Owner != null
		&& e.Menu != null) {

		// Add a menu item returned from the custom function
		e.Menu.Items.Add(CreateCustomMenuItem());
	}
}
```
}

> [!IMPORTANT]
> See details below about blocking a context menu from being allowed on dynamic menu items.

## Blocking the Context Menu on Dynamic Menu Items

As previously mentioned, the ribbon control auto-generates context menus for most of the controls contained within it. This includes all the items displayed in a default menu or context menu.  The default menu items are pre-configured to block a context menu, and dynamically generated menu items will typically need the same configuration.

To block the context menu, set the [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[AllowContextMenuProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.AllowContextMenuProperty) attached property to `false` using the [SetAllowContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarControlService.SetAllowContextMenu*) method.

The following code demonstrates how to configure a menu item to block the context menu:

```csharp
private BarMenuItem CreateCustomMenuItem() {
	var barMenuItem = new BarMenuItem() { ... };

	// Block the built-in context menu (with commands like "Add to Quick Access Toolbar") from
	// being displayed for this menu item.
	BarControlService.SetAllowContextMenu(barMenuItem, false);

	return barMenuItem;
}
```

> [!TIP]
> See the "Customize Built-in Menus" Bars Ribbon QuickStart of the Sample Browser application for a full demonstration of customizing menus.