---
title: "Converting to v26.1"
page-title: "Converting to v26.1 - Conversion Notes"
order: 93
---
# Converting to v26.1

## Avalonia v12.0.0 Updates

The following breaking changes were necessary to support and/or align with changes in Avalonia v12.0.0:
- Renamed [ThemeResourceKind](xref:@ActiproUIRoot.Themes.ThemeResourceKind).`EditWatermarkOpacity` to `EditPlaceholderOpacity`.
- Renamed [ThemeResourceKind](xref:@ActiproUIRoot.Themes.ThemeResourceKind).`EditWatermarkOpacityFocused` to `EditPlaceholderOpacityFocused`.
- Removed [ControlThemeKind](xref:@ActiproUIRoot.Themes.ControlThemeKind).`CaptionButtons` and corresponding `ControlTheme` since the native `CaptionButtons` control was removed.
- Removed [ControlThemeKind](xref:@ActiproUIRoot.Themes.ControlThemeKind).`TitleBar` and corresponding `ControlTheme` since the native `TitleBar` control was removed.
- Any overrides for `OnGotFocus` changed event arguments from `GotFocusEventArgs` to `FocusChangedEventArgs`.
- Any overrides for `OnLostFocus` changed event arguments from `RoutedEventArgs` to `FocusChangedEventArgs`.

## Menu Factory Updates

Several minor updates were made to [Menu Factory](../shared/menu-factory.md) concepts in order to align functionality and capabilities with other Actipro platforms.  Most of these changes only affect those who have implemented their own [IMenuFactory](xref:@ActiproUIRoot.Controls.IMenuFactory) implementations, but the default `MenuItem.Name` of some menu items have also changed.  If any default menus are being customized and the `MenuItem.Name` was used to find existing menu items without using one of the available constants, verify that the correct menu name is still in use.

The following potentially breaking changes were made:

### Shared Library
- The [IMenuFactory](xref:@ActiproUIRoot.Controls.IMenuFactory).`CreateMenuFlyout` method has been renamed as [CreateMenu](xref:@ActiproUIRoot.Controls.IMenuFactory.CreateMenu*).
- The `MenuFactoryCreateMenuFlyoutOptions` class has been renamed to [MenuFactoryMenuOptions](xref:@ActiproUIRoot.Controls.MenuFactoryMenuOptions).
- The `MenuFactoryCreateMenuItemOptions` class has been renamed to [MenuFactoryMenuItemOptions](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions).
  - The `Name` property on this class has been renamed to [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key).
- The `MenuFactory` class has been split into two classes:
  - A static [MenuFactory](xref:@ActiproUIRoot.Controls.MenuFactory) class that stores a reference to the [Current](xref:@ActiproUIRoot.Controls.MenuFactory.Current) and [Default](xref:@ActiproUIRoot.Controls.MenuFactory.Default) factory instances.
  - A generic [MenuFactory\<T,U,V\>](xref:@ActiproUIRoot.Controls.MenuFactory`3) class where generic arguments are used to define the type of menu, menu item, and menu separator created by the factory.
  - The old `MenuFactory.CreateMenuFlyout` virtual method has been replaced by [CreateMenu](xref:@ActiproUIRoot.Controls.MenuFactory`3.CreateMenu*) that is no longer virtual. Override [CreateMenuCore](xref:@ActiproUIRoot.Controls.MenuFactory`3.CreateMenuCore*) to customize the menu.
  - The old `MenuFactory.CreateMenuFlyoutCore` virtual method has been replaced by [CreateMenuItemCore](xref:@ActiproUIRoot.Controls.MenuFactory`3.CreateMenuItemCore*).
  - The old `MenuFactory.CreateMenuSeparator` virtual method has been replaced by [CreateMenuSeparator](xref:@ActiproUIRoot.Controls.MenuFactory`3.CreateMenuSeparator*) that is no longer virtual.  Override [CreateMenuSeparatorCore](xref:@ActiproUIRoot.Controls.MenuFactory`3.CreateMenuSeparatorCore*) to customize the menu separator.
- The [MenuFactory](xref:@ActiproUIRoot.Controls.MenuFactory).`CreateMenuFlyout` method has been renamed as [MenuFactory\<T,U,V\>](xref:@ActiproUIRoot.Controls.MenuFactory`3)

### Bars
- The Bars [IMenuFactory](xref:@ActiproUIRoot.Controls.IMenuFactory) implementation [BarsMenuFactory](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory) has been moved from the `ActiproSoftware.UI.Avalonia.Controls` namespace to the `ActiproSoftware.UI.Avalonia.Controls.Bars` namespace.
- The base class for [BarsMenuFactory](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory) has been changed to the new [MenuFactory\<T,U,V\>](xref:@ActiproUIRoot.Controls.MenuFactory`3) class along with the necessary conversions required by the class.
- A [ScreenTip](xref:@ActiproUIRoot.Controls.Bars.ScreenTip) is closed when a key is pressed.  Previously, the [ScreenTipService](xref:@ActiproUIRoot.Controls.Bars.ScreenTipService).[CurrentScreenTip](xref:@ActiproUIRoot.Controls.Bars.ScreenTipService.CurrentScreenTip) property would remain assigned until all key-related events had completed processing.  Due to backend changes in Avalonia v12, the [ScreenTipService](xref:@ActiproUIRoot.Controls.Bars.ScreenTipService).[CurrentScreenTip](xref:@ActiproUIRoot.Controls.Bars.ScreenTipService.CurrentScreenTip) property will only be populated in `KeyDown` events.  Any logic reliant on [CurrentScreenTip](xref:@ActiproUIRoot.Controls.Bars.ScreenTipService.CurrentScreenTip) that currently resides in a `KeyUp` event will need to be migrated to a `KeyDown` event.  This, for example, might impact any logic that might have been used to show contextual help for a [ScreenTip](xref:@ActiproUIRoot.Controls.Bars.ScreenTip) when the <kbd>F1</kbd> key was pressed.

### Docking

The Docking `MenuItemNames` class has been renamed to [CommandKeys](xref:ActiproSoftware.Properties.Docking.CommandKeys) and moved to the `ActiproSoftware.Properties.Docking` namespace.  Additionally, some constants were renamed as follows:
  - `CloseAllInContainer` renamed to `CloseAll`.
  - `CloseWindow` renamed to `Close`.
  - `FloatAllInContainer` renamed to `FloatAll`.
  - `MakeDockedWindow` renamed to `Dock`.
  - `MakeDocumentWindow` renamed to `MoveToMdi`.
  - `MakeFloatingWindow` renamed to `Float`.
  - `MoveToPrimaryMdiHost` renamed to `MoveToPrimaryMdi`.
  - `ToggleWindowAutoHideState` renamed to `AutoHide`.

The value of each constant was updated to match new naming conventions as follows:

| Old Value | New Value | Related Constant |
| ----- | ----- | ----- |
| `"Activate1DockingMenuItem"` (where `"1"` is a numerical position) | `"Docking.Activate.1"` | `CommandKeys.Activate` (prefix only) |
| `"CloseAllDocumentsDockingMenuItem"` | `"Docking.CloseAllDocuments"` | `CommandKeys.CloseAllDocuments` |
| `"CloseAllInContainerDockingMenuItem"` | `"Docking.CloseAll"` | `CommandKeys.CloseAll` |
| `"CloseOthersDockingMenuItem"` | `"Docking.CloseOthers"` | `CommandKeys.CloseOthers` |
| `"CloseWindowDockingMenuItem"` | `"Docking.Close"` | `CommandKeys.Close` |
| `"FloatAllInContainerDockingMenuItem"` | `"Docking.FloatAll"` | `CommandKeys.FloatAll` |
| `"KeepTabOpenDockingMenuItem"` | `"Docking.KeepTabOpen"` | `CommandKeys.KeepTabOpen` |
| `"MakeDockedWindowDockingMenuItem"` | `"Docking.Dock"` | `CommandKeys.Dock` |
| `"MakeDocumentWindowDockingMenuItem"` | `"Docking.MoveToMdi"` | `CommandKeys.MoveToMdi` |
| `"MakeFloatingWindowDockingMenuItem"` | `"Docking.Float"` | `CommandKeys.Float` |
| `"MoveToNewHorizontalContainerDockingMenuItem"` | `"Docking.MoveToNewHorizontalContainer"` | `CommandKeys.MoveToNewHorizontalContainer` |
| `"MoveToNewVerticalContainerDockingMenuItem"` | `"Docking.MoveToNewVerticalContainer"` | `CommandKeys.MoveToNewVerticalContainer` |
| `"MoveToNextContainerDockingMenuItem"` | `"Docking.MoveToNextContainer"` | `CommandKeys.MoveToNextContainer` |
| `"MoveToPreviousContainerDockingMenuItem"` | `"Docking.MoveToPreviousContainer"` | `CommandKeys.MoveToPreviousContainer` |
| `"MoveToPrimaryMdiHostDockingMenuItem"` | `"Docking.MoveToPrimaryMdi"` | `CommandKeys.MoveToPrimaryMdi` |
| `"PinTabDockingMenuItem"` | `"Docking.PinTab"` | `CommandKeys.PinTab` |
| `"Select1DockingMenuItem"` (where `"1"` is a numerical position) | `"Docking.SelectItem.1"` | `CommandKeys.SelectItem` (prefix only) |
| `"ToggleWindowAutoHideStateDockingMenuItem"` | `"Docking.AutoHide"` | `CommandKeys.AutoHide` |
