---
title: "ChromedTitleBar"
page-title: "ChromedTitleBar - Shared Library Controls"
order: 7
---
# ChromedTitleBar

The [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) is a control that renders a customizable window title bar with optional content areas on the left, center, and right.

![Screenshot](../images/chromed-title-bar.png)

*A ChromedTitleBar with content on the left, middle, and right in addition to standard caption buttons on Windows*

> [!NOTE]
> This control class does not inherit the native `TitleBar` class because `TitleBar`
> is specifically designed for use on a `Window` whereas `ChromedTitleBar`
> is designed to be useable anywhere.

> [!IMPORTANT]
> See the [Getting Started](../getting-started.md) topic for details on configuring themes for this control.

## ChromedTitleBar vs. WindowTitleBar

The [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) is a primitive control that is used to mimic a `Window`-like title bar, but can be used independently.  Actipro [Window Control](../../fundamentals/controls/window-control.md) is an example of a non-`Window` control that uses [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).

The [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) control extends [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) and is specifically designed to be used on and integrated with a `Window` control.  Actipro [User Prompt](../../fundamentals/user-prompt/index.md) uses [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) when prompts are displayed as dialogs.

## Content

The primary `Content` of the title bar is displayed in the center.  Set the [LeftContent](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.LeftContent) and [RightContent](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.RightContent) properties to optionally display additional content aligned to the left and/or right of the title bar.

> [!IMPORTANT]
> The [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) control is automatically configured to bind the `Content` property to the `Title` property of the ancestor `Window` unless it is determined that a native title bar is still being used by the `Window`.  Explicitly set the `Content` to `x:Null` (or any other value) if the title should not be displayed.

### Caption Buttons

The **Full Screen**, **Minimize**, **Maximize**, **Restore**, and **Close** caption buttons are automatically displayed based on the current [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[WindowState](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.WindowState) property.  The **Maximize** and **Restore** buttons are automatically disabled when the [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[CanResize](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.CanResize) property is set to `false`.

> [!IMPORTANT]
> The [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) control automatically binds the [WindowState](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.WindowState) and [CanResize](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.CanResize) properties to the corresponding `Window.WindowState` and `Window.CanResize` properties of the ancestor `Window` control.

Each caption button is configured to invoke a corresponding command (e.g., the **Close** button invokes the [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[CloseCommand](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.CloseCommand)).  Most caption buttons change the value of the [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[WindowState](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.WindowState) property.


> [!IMPORTANT]
> The [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) control configures the [CloseCommand](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.CloseCommand) to invoke `Window.Close` on the ancestor `Window`.

Each caption button can optionally be hidden by setting the appropriate property.

| Member | Description |
|-----|-----|
| [IsCloseButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsCloseButtonAllowed) Property | Gets or sets if the title bar is allowed to display the close button. The default value is `true`. |
| [IsFullScreenButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsFullScreenButtonAllowed) Property | Gets or sets if the title bar is allowed to display the full screen button. The default value is `false`. |
| [IsMaximizeButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsMaximizeButtonAllowed) Property | Gets or sets if the title bar is allowed to display the maximize button. The default value is `true`. |
| [IsMinimizeButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsMinimizeButtonAllowed) Property | Gets or sets if the title bar is allowed to display the minimize button. The default value is `true`. |
| [IsRestoreButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsRestoreButtonAllowed) Property | Gets or sets if the title bar is allowed to display the restore button. The default value is `true`. |

The read-only [HasCaptionButtons](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.HasCaptionButtons) property will return `true` if at least one of the caption buttons is displayed.

> [!WARNING]
> The **Maximize** and **Restore** buttons are configured independently.  When setting [IsMaximizeButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsMaximizeButtonAllowed) to `false` on a [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar), it is typically required to also set [IsRestoreButtonAllowed](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsRestoreButtonAllowed) to `false`.  While the **Restore** button may never be visible if a `Window` cannot be maximized, the **Restore** command might still appear in the default title bar context menu.

## Default Context Menu

The [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar) can display a default context menu when right-clicking on the titlebar or left-clicking on the **Icon**.  The context menu contains common commands that are consistent with the available caption buttons.

The [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[HasDefaultMenu](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.HasDefaultMenu) property determines if the default menu is available.  This value defaults to `true` on Linux and Windows operating systems; otherwise, the default value is `false`.

The [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[HasDefaultMenuIcons](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.HasDefaultMenuIcons) property determines if menu items on the default context menu have icons.  This value defaults to `true` on Windows operating system; otherwise, the default is `false`.

Use the [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[MenuOpening](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.MenuOpening) event to customize or cancel the default context menu before it is displayed.

## Icon

The [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[Icon](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.Icon) property can be set to any value supported by [Icon Presenter](../../themes/icon-presenter.md) in order to display an **Icon** in the title bar.  The visibility of the icon can be controlled using the [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[IsIconVisible](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.IsIconVisible) property.

By default, double-clicking the **Icon** will invoke [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[CloseCommand](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.CloseCommand).  This functionality can be disabled by setting [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[CanCloseOnIconDoubleTapped](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.CanCloseOnIconDoubleTapped) to `false`.

## Window Integration with WindowTitleBar

The [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) should only be used as a descendant of a `Window` control and will automatically configure itself and the ancestor `Window` as noted below.

On Windows and macOS operating systems, a `Window` that has a [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) will hide its native title bar and the [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) will provide all title bar functionality in its place.

On other operating systems (e.g., Linux), when the `Window.SystemDecorations` property is explicitly set to `SystemDecorations.None`, [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar)'s caption buttons will be displayed.  Otherwise, they will remain hidden, as they would duplicate the caption buttons found in the native title bar.  The other content portions of the [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) will always be displayed regardless.

### Extend Client Area Configuration

Various `Window` client area extension properties are set if the [CanConfigureWindowClientArea](xref:@ActiproUIRoot.Controls.WindowTitleBar.CanConfigureWindowClientArea) property has its default value of `true`.  The property can be set to `false` before the [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) is added to the visual tree in scenarios where the various client area extension properties need to be manually configured.

#### Operating System Settings When Enabled

On the Windows and macOS operating systems, the ancestor `Window` is automatically configured with the following properties for ideal display of the client area:
- `Window.ExtendClientAreaChromeHints` set to `ExtendClientAreaChromeHints.NoChrome`.
- `Window.ExtendClientAreaToDecorationsHint` set to `true`.
- `Window.ExtendClientAreaTitleBarHeightHint` set to the final height of [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar).

For all other operating systems (e.g., Linux), the ancestor `Window` is configured as follows:
- `Window.ExtendClientAreaChromeHints` set to `ExtendClientAreaChromeHints.SystemChrome`.
- `Window.ExtendClientAreaToDecorationsHint` set to `false`.
- `Window.ExtendClientAreaTitleBarHeightHint` set to `-1`.

## Pseudo-classes

The following pseudo-classes are added, as appropriate, based on the [ChromedTitleBar](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar).[WindowState](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.WindowState) and can be used when styling the control:

| Class | Description |
|-----|-----|
| `:active` | The `Window` is active. |
| `:fullscreen` | The `Window` is in full-screen mode. |
| `:maximized` | The `Window` is maximized. |
| `:minimized` | The `Window` is minimized. |
| `:normal` | The `Window` is in normal windowed mode. |


## Customize String Resources

The following string resources are available to localize or customize built-in strings:

| Resource key | Description |
|-----|-----|
| `UICaptionButtonCloseText` | The text displayed as a tooltip for the **Close** caption button and menu item on the default context menu. |
| `UICaptionButtonEnterFullScreenText` | The text displayed as a tooltip for the **Full Screen** caption button when not in full screen mode. |
| `UICaptionButtonExitFullScreenText` | The text displayed as a tooltip for the **Full Screen** caption button when in full screen mode. |
| `UICaptionButtonMaximizeText` | The text displayed as a tooltip for the **Maximize** caption button and menu item on the default context menu. |
| `UICaptionButtonMinimizeText` | The text displayed as a tooltip for the **Minimize** caption button and menu item on the default context menu. |
| `UICaptionButtonRestoreText` | The text displayed as a tooltip for the **Restore** caption button and menu item on the default context menu. |

See the [Customizing String Resources](../../customizing-string-resources.md) topic for additional details.