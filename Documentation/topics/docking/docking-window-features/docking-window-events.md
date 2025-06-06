---
title: "Docking Window Events"
page-title: "Docking Window Events - Docking & MDI Docking Window Features"
order: 5
---
# Docking Window Events

There are a number of events to which you can attach to know when various actions occur to docking windows.

## DockSite Events

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) that owns the docking window receiving an action has these events:

| Member | Description |
|-----|-----|
| [FloatingWindowOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowOpening) Event | Occurs when a floating window is opening, allowing for customization before it is displayed. |
| [MdiKindChanged](xref:@ActiproUIRoot.Controls.Docking.DockSite.MdiKindChanged) Event | Occurs when the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[MdiKind](xref:@ActiproUIRoot.Controls.Docking.DockSite.MdiKind) property is changed. |
| [MenuOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.MenuOpening) Event | Occurs when a docking-related context menu is opening, allowing for customization before it is displayed. |
| [NewWindowRequested](xref:@ActiproUIRoot.Controls.Docking.DockSite.NewWindowRequested) Event | Occurs when a new [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) is requested, generally via a user click on a **New Tab** button. |
| [PrimaryDocumentChanged](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrimaryDocumentChanged) Event | Occurs when the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[PrimaryDocument](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrimaryDocument) property has changed. |
| [WindowActivated](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowActivated) Event | Occurs after a [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) has been activated. |
| [WindowAutoHidePopupClosed](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowAutoHidePopupClosed) Event | Occurs after an auto-hide popup has been closed that displayed a [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow). |
| [WindowAutoHidePopupOpened](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowAutoHidePopupOpened) Event | Occurs after an auto-hide popup has been opened that displays a [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow). |
| [WindowDeactivated](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowDeactivated) Event | Occurs after a [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) has been deactivated. |
| [WindowDefaultLocationRequested](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowDefaultLocationRequested) Event | Occurs when a [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow)'s default location is requested. |
| [WindowRegistered](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowRegistered) Event | Occurs after a [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) has been registered. |
| [WindowsAutoHiding](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsAutoHiding) Event | Occurs before one or more [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls are auto-hidden, allowing for side customization. |
| [WindowsClosed](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsClosed) Event | Occurs after one or more [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls have been closed. |
| [WindowsClosing](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsClosing) Event | Occurs before one or more [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls are closed, allowing for cancellation of the close. |
| [WindowsDockHostChanged](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsDockHostChanged) Event | Occurs after one or more [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls' [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.DockHost) properties have changed. |
| [WindowsDragged](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsDragged) Event | Occurs after one or more [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls are dragged by the end user. |
| [WindowsDragging](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsDragging) Event | Occurs before one or more [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls are dragged by the end user. |
| [WindowsDragOver](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsDragOver) Event | Occurs when one or more docking windows are dragged over a new dock target, allowing for certain dock guides to be hidden. |
| [WindowsOpened](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsOpened) Event | Occurs after one or more [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls have been opened. |
| [WindowsOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsOpening) Event | Occurs before one or more [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls are opened. |
| [WindowsStateChanged](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsStateChanged) Event | Occurs after one or more [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls' states have changed. |
| [WindowUnregistered](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowUnregistered) Event | Occurs after a [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) has been unregistered. |

Most of the event arguments pass references to the related [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) controls.

## Displaying a StatusBar Message During Drags

It is a common practice to display a status bar message during tool window drags to indicate to the end user that they can move over the dock guides to dock a tool window.

To accomplish this, handle the [WindowsDragging](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsDragging) and display a message like this in your status bar:

`"Use the guide diamond to choose a docking location.  To prevent docking, hold down CTRL."`

Handle the [WindowsDragged](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsDragged) event and restore the status bar message back to something like `"Ready"`.

## Customizing Menus

The [MenuOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.MenuOpening) event is raised before any menu is displayed for a window or button, allowing you to customize it or replace it with your own.

See the [Menu Customization](menu-customization.md) topic for details on handling this event.
