---
title: "Floating Dock Hosts"
page-title: "Floating Dock Hosts - Docking & MDI Docking Window Features"
order: 11
---
# Floating Dock Hosts

Docking windows can be dragged to float, meaning appear in a floating dock host that can move independently above the dock site's primary dock host.

## Floating Dock Host Overview

Any time a docking window is floated, a floating [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost) is created to contain it.  If a document window is floated, the dock host will contain its own workspace and MDI host in which the floated document window will appear.  If a tool window is floated, the dock host will contain a single tool window container.

For floating dock hosts with a workspace, other documents can be added to the floating dock host's MDI host and other tool windows can be docked around the workspace, or even auto-hidden.  For floating dock hosts that do not have a workspace and only have tool window containers, other tool windows can be attached or docked next to the tool window containers.

## Allowing Floating Windows

Both tool and document windows can be floated, but only tool windows have floating enabled by default.

A global [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsFloat](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsFloat) property (defaults to `true`) determines the default value for whether tool windows have that capability, and a global [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanDocumentWindowsFloat](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanDocumentWindowsFloat) property (defaults to `false`) determines the default value for whether document windows have that capability.  The properties can be overridden at an instance-level by setting the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanFloat](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanFloat) property.

## Programmatically Floating a Docking Window

A docking window can be programmatically floated by calling the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Float](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Float*) method.  Overloads of the method are available that allow you to specify the location or size of the floating dock host.

## Preventing Tool Windows from Docking Against Floating Dock Hosts Containing Workspaces

In some scenarios, you might only wish to allow tool windows to be dragged and docked against floating dock hosts that only contain tool windows, and don't have a workspace.  To enable this feature, set the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsDragToFloatingDockHostsWithWorkspaces](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsDragToFloatingDockHostsWithWorkspaces) property to `false`.

## Determining if a Docking Window is on a Floating Dock Host

When the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[IsOpen](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsOpen) property returns `true`, the docking window is open in the layout and is nested within a [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost).  The dock host can be retrieved via the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[DockHost](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.DockHost) property.  If the dock host is in the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingDockHosts](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingDockHosts) collection, then it is a floating dock host, and thus the docking window is "floating".

This code shows how to do this check:

```csharp
var isOnFloatingDockHost = (window.IsOpen) && (window.DockSite.FloatingDockHosts.Contains(window.DockHost));
```

The [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[IsFloating](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsFloating) property makes this even easier:

```csharp
var isOnFloatingDockHost = (window.IsOpen) && (window.IsFloating);
```

Note that the [DockingWindowState](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState) enumeration doesn't have a `Floating` item.  This is due to the fact that Docking/MDI's advanced floating dock host features allow tool windows to be in a docked, auto-hidden, or in document state even when they are in a floating dock host.

For instance, if a tool window is floated, it will show up in a single [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) inside of a floating dock host and its state will be [Docked](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.Docked).  If a document window is floated, it will show up in an MDI area inside of a floating dock host and its state will be [Document](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.Document).  A tool window could then be dragged and docked next to that MDI area.  Once that is done, it could be auto-hidden, and its state would be [AutoHide](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.AutoHide).  These examples show how various docking window states can exist within a floating dock host.

## Floating Window Icon and Title

A floating dock host can have its icon and title set via the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingWindowIcon](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowIcon) and [FloatingWindowTitle](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowTitle) properties respectively.  It's generally a good practice to use an application icon as the icon and the application title as the title.

The floating dock host's title will also combine with the dock host's primary document's title (if there is a document in the dock host), which gives better clarity as to its contents in the @if (avalonia) { operation system }@if (wpf) { Windows } taskbar and [switchers](switchers.md) screens.  The primary document's title will appear before the [FloatingWindowTitle](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowTitle) value and will be delimited by the [FloatingWindowTitleDelimiter](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowTitleDelimiter) value when both are present.

The virtual [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[GetFloatingDockHostTitle](xref:@ActiproUIRoot.Controls.Docking.DockSite.GetFloatingDockHostTitle*) method does the work of generating the resolved title text for the floating dock host and can be overridden if custom logic should be used instead.

## Using Drag Float Previews

By default, dragging a docking window will instantly float it, giving it more of a live feel.  The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[UseDragFloatPreviews](xref:@ActiproUIRoot.Controls.Docking.DockSite.UseDragFloatPreviews) property, which defaults to `false`, can be set to `true` to prevent this behavior and only show a float preview highlight while dragging.

This property should be set to `true` when docking window content contains a large number of elements that may not measure/arrange quickly.  When this property is `true`, fewer layout cycles occur since a float preview is used to show where the floating window will be created instead of instantly moving the dragged windows into a floating window.

## Snap to Screen Threshold

When the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[IsFloatingWindowSnapToScreenEnabled](xref:@ActiproUIRoot.Controls.Docking.DockSite.IsFloatingWindowSnapToScreenEnabled) property is `true`, which is the default, floating windows are snapped onto the closest screen when displayed via a method other than being dragged.  This feature ensures that floating windows are fully visible on-screen when being programmatically displayed, such as when a layout is loaded.

The minimum length of a floating window that must be visible over a screen's edge to avoid snap-to-screen can be set by the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingWindowSnapToScreenThreshold](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowSnapToScreenThreshold) property.  If a floating window is completely beyond the screen edge, it will be fully snapped back onto the screen.  If a floating window is only partially beyond the screen edge, but the visible portion is less than this property's value, it will be snapped partially back onto the screen.

## Size-to-Content

If you wish for a docking window to auto-size itself to its content when floated, update its [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[SizeToContentModes](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SizeToContentModes) property to contain the [SizeToContentModes](xref:@ActiproUIRoot.Controls.Docking.SizeToContentModes).[Floating](xref:@ActiproUIRoot.Controls.Docking.SizeToContentModes.Floating) flag.

## Limiting Floating Window Initial Sizes

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingWindowOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowOpening) event is raised whenever a floating window is opening, allowing for customization before it is displayed.  It is passed arguments of type [FloatingWindowOpeningEventArgs](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowOpeningEventArgs) that indicate the child control of the floating dock host that will be created, along with the default size of the floating window.

The [FloatingWindowOpeningEventArgs](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowOpeningEventArgs).[Size](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowOpeningEventArgs.Size) property can be altered in the event handler to limit the size of the floating window.  This feature is useful since sometimes when a MDI area fills most of the screen, dragged-to-float documents would fully obscure what is behind them.

This code shows an event handler implementation that ensures that floating windows are never longer than `600` on their long side, and `300` on their short side.

```csharp
private void OnDockSiteFloatingWindowOpening(object sender, FloatingWindowOpeningEventArgs e) {
	// Make sure the long side is no longer than 600, and the short side is no longer than 300
	if (e.Size.Width > e.Size.Height)
		e.Size = new Size(Math.Min(600.0, e.Size.Width), Math.Min(300.0, e.Size.Height));
	else
		e.Size = new Size(Math.Min(300.0, e.Size.Width), Math.Min(600.0, e.Size.Height));
}
```

## Double-Clicking Floating Tool Window Container Title Bars

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingToolWindowContainerTitleBarDoubleClickMode](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingToolWindowContainerTitleBarDoubleClickMode) property determines the behavior for double-clicks on floating tool window container title bars.  It can be set to one of these two values:

- [ToggleMaximized](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowTitleBarDoubleClickMode.ToggleMaximized) (default) - The floating window toggles between maximized and restored states.
- [Dock](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowTitleBarDoubleClickMode.Dock) - The floating window docks back into another dock host.

## Non-Hosted vs. Hosted Floating Windows

Non-hosted floating windows are used by default in desktop applications, which means that when a floating dock host is created, it will be contained by its own `Window` that can be independently moved anywhere.  Drags of non-hosted floating windows make use of Windows' Snap features, meaning you can drag to the top of a screen to maximize it, to the side to make it take up half the screen, or to a corner to make it take up a quarter of the screen.

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[UseHostedFloatingWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.UseHostedFloatingWindows) property can be set to `true` to force floating dock hosts to use a hosted floating window instead.  In this scenario, a @if (avalonia) { [WindowControl](xref:@ActiproUIRoot.Controls.WindowControl) } @if (wpf) { [WindowControl](xref:@ActiproUIRoot.Controls.Docking.WindowControl) } is used as a wrapper around the floating dock host and is allowed to move anywhere within the bounds of the dock site, or [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[HostedFloatingWindowContainer](xref:@ActiproUIRoot.Controls.Docking.DockSite.HostedFloatingWindowContainer) if specified.

When using hosted floating windows, the floating dock host is embedded within the same root `Window` as the dock site.  Hosted floating windows have a subtle @if (avalonia) { shadow }@if (wpf) { outer glow } that gives them some depth and distinguishes them from the content dock site underneath.

## Hosted Floating Window Inactivity Animations

A great feature implemented by hosted floating windows that cannot be implemented in normal floating windows is inactivity animation.

Basically, after the pointer has not moved over a hosted floating window for a while and it doesn't have focus, it will begin to slowly fade out to partial transparency.  You have total control over whether this feature is enabled and how long it takes to fade, etc.

These [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) properties control the inactivity behavior:

| Member | Description |
|-----|-----|
| [InactiveFloatingWindowFadeDelay](xref:@ActiproUIRoot.Controls.Docking.DockSite.InactiveFloatingWindowFadeDelay) Property | Gets or sets the duration of inactivity that must take place for a floating window before it starts to fade out.  The default value is `20` seconds. |
| [InactiveFloatingWindowFadeDuration](xref:@ActiproUIRoot.Controls.Docking.DockSite.InactiveFloatingWindowFadeDuration) Property | Gets or sets the duration over which an inactive floating window will fade.  The default value is `20` seconds. |
| [InactiveFloatingWindowFadeOpacity](xref:@ActiproUIRoot.Controls.Docking.DockSite.InactiveFloatingWindowFadeOpacity) Property | Gets or sets the target opacity to which inactive floating windows to fade.  The default value is `0.25` (25% opacity). |
| [IsInactiveFloatingWindowFadeEnabled](xref:@ActiproUIRoot.Controls.Docking.DockSite.IsInactiveFloatingWindowFadeEnabled) Property | Gets or sets whether floating windows will fade out following inactivity.  The default value is `true`. |

## Hosted Floating Window Container

To ensure that a portion of the hosted floating windows are always visible to the end user, their bounds are coerced such that part of the title bar is always visible.  The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[HostedFloatingWindowContainer](xref:@ActiproUIRoot.Controls.Docking.DockSite.HostedFloatingWindowContainer) property can be set to any visual ancestor of the `DockSite`, and hosted floating window bounds will be constrained to the bounds of the specified container.  If a container is not specified, the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) will be used as the container.

## Determining Which Floating Windows Show in the TaskBar

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingWindowShowInTaskBarMode](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowShowInTaskBarMode) property can be used to determine which kinds of non-hosted floating windows are displayed in the @if (avalonia) { operating system }@if (wpf) { Windows } taskbar.  The default behavior is for any floating window that contains a "workspace" (such as a floating tabbed MDI document) to show up in the taskbar and not be "owned" by the main window.  This means that the floating window can appear behind the main window when the main window is activated, but it is ok since the floating window can be accessed via the taskbar.  Note that the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingWindowOwnerMode](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowOwnerMode) property can alter the default owner behavior.

Change the property to [FloatingWindowShowInTaskBarMode](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowShowInTaskBarMode).[Never](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowShowInTaskBarMode.Never) to prevent all floating windows from showing in the taskbar.  In this scenario, all floating windows will be "owned" by the main window so that they remain accessible.

Change the property to [FloatingWindowShowInTaskBarMode](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowShowInTaskBarMode).[Always](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowShowInTaskBarMode.Always) to force all floating windows to show in the taskbar.  In this scenario, no floating windows will be "owned" by the main window and they all will be able to appear behind it when the main window is activated, unless this behavior is altered by the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingWindowOwnerMode](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowOwnerMode) property.

@if (wpf) {

## Use With Ribbons or ToolBars on the Main Window

WPF will move focus to the last-focused control within a `Window` when the `Window` is activated.  This can be a problem when working with floating docking windows and wanting to use a main `Window`'s ribbon or toolbar to execute commands on the active floating docking window.  As soon as a ribbon or toolbar's button is clicked, the main `Window` is activated, which then can focus another docking window within the main `Window`, causing the button's command to work on it instead of the intended floating docking window.

The Actipro [WindowChrome](../../themes/windowchrome.md) class has a [CanMouseActivateOverToolBar](xref:@ActiproUIRoot.Themes.WindowChrome.CanMouseActivateOverToolBar) property that can be set to `false` to prevent toolbar clicks from activating the `Window`.  A toolbar is considered any control with the `WindowChrome.ElementKind` attached property set to [WindowChromeElementKind.ToolBar](xref:@ActiproUIRoot.Themes.WindowChromeElementKind.ToolBar).  Bars' [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) and [StandaloneToolBar](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar) controls have this attached property set by default.

.NET also has a couple properties that should be applied in app startup that help prevent `Window` activation when working with menus,
such as a menu that opens when pressing a [BarPopupButton](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton).

When these features are combined, controls on main `Window` ribbons and toolbars can execute commands on active floating document windows.

### Configuring WindowChrome

A regular `Window` can be configured through [WindowChrome](../../themes/windowchrome.md) to prevent mouse activation over any control with the `WindowChrome.ElementKind` attached property set to [WindowChromeElementKind.ToolBar](xref:@ActiproUIRoot.Themes.WindowChromeElementKind.ToolBar):

```xaml
<Window ...>
	<themes:WindowChrome.Chrome>
		<themes:WindowChrome CanMouseActivateOverToolBar="False" />
	</themes:WindowChrome.Chrome>
	...
</Window>
```

Or if using a Bars [RibbonWindow](../../bars/ribbon-features/ribbon-window.md):

```xaml
<bars:RibbonWindow ...>
	<themes:WindowChrome.Chrome>
		<themes:RibbonWindowChrome CanMouseActivateOverToolBar="False" />
	</themes:WindowChrome.Chrome>
	...
</bars:RibbonWindow>
```

### Preventing Mouse Activation on a Native ToolBar

If a [WindowChrome](../../themes/windowchrome.md) is applied to the main `Window` as above, an attached property can be set on a `ToolBar` to prevent mouse activation of the ancestor `Window` when the toolbar is clicked.

```xaml
<ToolBar ... themes:WindowChrome.ElementKind="ToolBar">
	...
</ToolBar>
```

### Additional WPF Properties

Two WPF properties were added in .NET Framework 4.0 to allow Visual Studio to support a toolbar system that functioned with floating docking windows:

- `HwndSource.DefaultAcquireHwndFocusInMenuMode` - Whether a `Window` can acquire Win32 focus for the WPF containing window when the user interacts with menus.
- `Keyboard.DefaultRestoreFocusMode` - Determines the behavior when WPF restores focus.

When attempting to use main `Window` ribbons or toolbars with floating docking windows, these two properties should be set to the following values in application startup logic:

```csharp
HwndSource.DefaultAcquireHwndFocusInMenuMode = false;
Keyboard.DefaultRestoreFocusMode = RestoreFocusMode.None;
```

}