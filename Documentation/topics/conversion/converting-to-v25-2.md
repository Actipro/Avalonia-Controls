---
title: "Converting to v25.2"
page-title: "Converting to v25.2 - Conversion Notes"
order: 96
---
# Converting to v25.2

## New .NET Target

Updated the .NET target from .NET 6 to .NET 8 since the former is out of support.

## Avalonia Dependency

Updated the minimum Avalonia dependency from v11.2.0 to v11.3.0.

## ChromedTitleBar and WindowTitleBar

The `ChromedTitleBar` control's tight `Window` integration was limiting the ability to use the control in scenarios where integration with a `Window` was not desired.  To improve usability, the control was split into a base [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) primitive control with all the core functionality and the derived [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) control that is designed to integrate with a `Window`.  [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) was also moved to the `Primitives` sub-namespace.

To enable more usage scenarios, the **Maximize** and **Restore** caption buttons are implemented as distinct controls instead of a single control that toggled between the two states.  A new [IsRestoreButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsRestoreButtonAllowed) property has been added to control availability of the **Restore** caption button that appears in the minimized or maximized window states.

In this release, the macOS operation system joins Windows in support for extending into the native title bar area.  This capability is enabled by default.  To restore the original functionality on macOS, set [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar).[CanConfigureWindowClientArea](xref:@ActiproUIRoot.Controls.WindowTitleBar.CanConfigureWindowClientArea) to `false`.

The following updates should be made for [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar):
- Replace [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) with [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) everywhere that integration with the parent `Window` is desired.
- If not integrating with the parent `Window`, continue to use [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) with the updated `ActiproSoftware.UI.Avalonia.Controls.Primitives` namespace.

The following updates should be made for any custom types that derive from [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar):
- Derived types that integrate with `Window` should be updated to derive from `WindowTitleBar` instead of `ChromedTitleBar`.
- Derived types that do not integrate with `Window` should continue to derive from `ChromedTitleBar` and remove the `IsIntegratedWithParentWindow` override that is no longer necessary.
- The virtual `OnClose`, `OnMinimize`, `OnToggleMaximized`, and `OnToggleFullScreen` methods have be replaced with related `ICommand` properties (e.g., [CloseCommand](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.CloseCommand) and [MaximizeCommand](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.MaximizeCommand)).  Any custom logic previously associated with method overrides should be encapsulated as an `ICommand` and assigned to the corresponding property.  The [DelegateCommand\<T\>](xref:@ActiproUIRoot.Input.DelegateCommand`1) class is helpful in this regard.

Finally, the following updates may be necessary when using either [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) or [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar).
- If [IsMaximizeButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsMaximizeButtonAllowed) is set to `false`, the [IsRestoreButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsRestoreButtonAllowed) typically should also be set to `false`.
- The `AllowWindowDragOnPointerPressed` property has been renamed as [CanDrag](xref:@ActiproUIRoot.Controls.WindowTitleBar.CanDrag).
- A default context menu has been added that is displayed when right-clicking the title bar or left-clicking the [Icon](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.Icon).  If a context menu is not desired, set [HasDefaultMenu](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.HasDefaultMenu) to `false`.
- If the [LeftContent](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.LeftContent) property was used to add an icon, consider migrating to the new [Icon](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.Icon) property instead.
- The `Content` property of [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) is, by default, bound to the `Title` of the parent `Window`.  If the default title is not desired, explicitly set the `Content` to `null`.

## ChromedTitleBar Caption Buttons

[ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) will now alter the appearance of its caption buttons based on the current platform: Windows, macOS, or Linux.

## Bars MVVM Tag Property

To simplify storing user-defined data with [Bars view models](../bars/mvvm-support.md) without the need to create custom derived classes, each view model has a new [Tag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag.Tag) property exposed using the [IHasTag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag) interface.  The existing [IBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel) interface was updated to also import the new [IHasTag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag) interface, so this is a breaking change for any user-defined types that implement [IBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel) directly without deriving from [BarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemViewModel`1).

The following updates should be made to any class that implements [IBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel):
- Add a declaration and implementation for the [IHasTag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag).[Tag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag.Tag) property.

## Theme Generator Updates

The [ThemeGenerator.GetBrushResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetBrushResource*) method was updated to return `IBrush` instead of `Brush`.