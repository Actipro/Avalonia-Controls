---
title: "ChromedTitleBar"
page-title: "ChromedTitleBar - Shared Library Controls"
order: 3
---
# ChromedTitleBar

The [ChromedTitleBar](xref:@ActiproUIRoot.Controls.ChromedTitleBar) is a control that renders a customizable window title bar with optional content areas on the left, center, and right.

![Screenshot](../images/chromed-title-bar.png)

*A ChromedTitleBar with content on the left, middle, and right in addition to standard caption buttons on Windows*

> [!NOTE]
> This control class does not inherit the native `TitleBar` class because `TitleBar`
> contains logic that hides itself in certain circumstances, whereas `ChromedTitleBar`
> is designed to be useable in more scenarios.

## Content

The primary `Content` of the title bar is displayed in the center.  Set the [LeftContent](xref:@ActiproUIRoot.Controls.ChromedTitleBar.LeftContent) and [RightContent](xref:@ActiproUIRoot.Controls.ChromedTitleBar.RightContent) properties to optionally display additional content aligned to the left and/or right of the title bar.

## Window Integration
The [ChromedTitleBar](xref:@ActiproUIRoot.Controls.ChromedTitleBar) detects when it is a descendant of a `Window` control and will automatically configure itself and the ancestor `Window` as noted below.

On Windows systems, a `Window` that has a [ChromedTitleBar](xref:@ActiproUIRoot.Controls.ChromedTitleBar) will hide its native title bar and the [ChromedTitleBar](xref:@ActiproUIRoot.Controls.ChromedTitleBar) will provide all title bar functionality in its place.

On non-Windows systems, when the `Window.SystemDecorations` property is explicitly set to `SystemDecorations.None`, [ChromedTitleBar](xref:@ActiproUIRoot.Controls.ChromedTitleBar)'s caption buttons will be displayed.  Otherwise, they will remain hidden, as they would duplicate the caption buttons found in the native title bar.  The other content portions of the [ChromedTitleBar](xref:@ActiproUIRoot.Controls.ChromedTitleBar) will always be displayed regardless.

### Caption Buttons

The **Full Screen**, **Minimize**, **Maximize**, **Restore**, and **Close** caption buttons are automatically displayed, and each button is configured to invoke the corresponding method on the ancestor `Window` (e.g., the **Close** button will invoke `Window.Close`).

Additionally, the **Maximize** caption button will be disabled when `Window.CanResize` is `false`.

Each caption button can optionally be hidden by setting the appropriate property.

| Member | Description |
|-----|-----|
| [IsCloseButtonAllowed](xref:@ActiproUIRoot.Controls.ChromedTitleBar.IsCloseButtonAllowed) Property | Gets or sets if the title bar is allowed to display the close button. The default value is `true`. |
| [IsFullScreenButtonAllowed](xref:@ActiproUIRoot.Controls.ChromedTitleBar.IsFullScreenButtonAllowed) Property | Gets or sets if the title bar is allowed to display the full screen button. The default value is `false`. |
| [IsMaximizeButtonAllowed](xref:@ActiproUIRoot.Controls.ChromedTitleBar.IsMaximizeButtonAllowed) Property | Gets or sets if the title bar is allowed to display the maximize/restore button. The default value is `true`. |
| [IsMinimizeButtonAllowed](xref:@ActiproUIRoot.Controls.ChromedTitleBar.IsMinimizeButtonAllowed) Property | Gets or sets if the title bar is allowed to display the minimize button. The default value is `true`. |

The read-only [HasCaptionButtons](xref:@ActiproUIRoot.Controls.ChromedTitleBar.HasCaptionButtons) property will return `true` if at least one of the caption buttons is displayed.

### Extend Client Area Configuration

On the Windows operating system, the ancestor `Window` is automatically configured with the following properties for ideal display of the client area:
- `Window.ExtendClientAreaChromeHints` set to `ExtendClientAreaChromeHints.NoChrome`.
- `Window.ExtendClientAreaToDecorationsHint` set to `true`.
- `Window.ExtendClientAreaTitleBarHeightHint` set to the final height of [ChromedTitleBar](xref:@ActiproUIRoot.Controls.ChromedTitleBar).

For all other operating systems, the ancestor `Window` is configured as follows:
- `Window.ExtendClientAreaChromeHints` set to `ExtendClientAreaChromeHints.SystemChrome`.
- `Window.ExtendClientAreaToDecorationsHint` set to `false`.
- `Window.ExtendClientAreaTitleBarHeightHint` set to `-1`.

## Pseudo-classes

The following pseudo-classes are added, as appropriate, based on the state of the ancestor `Window` (if any) and can be used when styling the control:

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
| `UICaptionButtonCloseText` | The text displayed as a tooltip for the **Close** caption button. |
| `UICaptionButtonEnterFullScreenText` | The text displayed as a tooltip for the **Full Screen** caption button when not in full screen mode. |
| `UICaptionButtonExitFullScreenText` | The text displayed as a tooltip for the **Full Screen** caption button when in full screen mode. |
| `UICaptionButtonMaximizeText` | The text displayed as a tooltip for the **Maximize** caption button. |
| `UICaptionButtonMinimizeText` | The text displayed as a tooltip for the **Minimize** caption button. |
| `UICaptionButtonRestoreText` | The text displayed as a tooltip for the **Restore** caption button. |

See the [Customizing String Resources](../../customizing-string-resources.md) topic for additional details.