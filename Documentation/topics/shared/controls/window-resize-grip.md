---
title: "WindowResizeGrip"
page-title: "WindowResizeGrip - Shared Library Controls"
order: 50
---
# WindowResizeGrip

The [WindowResizeGrip](xref:@ActiproUIRoot.Controls.WindowResizeGrip) is a transparent gripper control that can be placed in the lower-right corner of a `Window` or its status bar, providing a larger area to drag-resize the `Window` both horizontally and vertically.

@if (avalonia) {
> [!IMPORTANT]
> See the [Getting Started](../getting-started.md) topic for details on configuring themes for this control.
}

## Example

The following examples demonstrate how to define a [WindowResizeGrip](xref:@ActiproUIRoot.Controls.WindowResizeGrip) for a `Window`:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<Window>
	<Grid>

		<!-- Other window controls -->

		<!-- WindowResizeGrip is automatically aligned to bottom-right -->
		<actipro:WindowResizeGrip />

	</Grid>
</Window>
```
