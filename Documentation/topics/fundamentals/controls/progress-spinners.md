---
title: "Progress Spinners"
page-title: "Progress Spinners - Fundamentals Controls"
order: 40
---
# Progress Spinners

Progress spinners are used when some form of processing is occurring to tell the end user that something is happening.

![Screenshot](../images/ring-spinner-large.png)

*A large RingSpinner control*

@if (avalonia) {
> [!IMPORTANT]
> See the [Getting Started](../getting-started.md) topic for details on configuring themes for these controls.
}

## RingSpinner

The [RingSpinner](xref:@ActiproUIRoot.Controls.RingSpinner) control renders an animated ring where the two ring segment ends chase each other around the circle.

![Screenshot](../images/ring-spinner-examples.png)

*A couple RingSpinner controls*

### Animation

The [IsSpinning](xref:@ActiproUIRoot.Controls.RingSpinner.IsSpinning) property must be set to `true` to activate animated spinning.

### Layout

The outer radius of the ring will be auto-calculated based on the `Width` and `Height` of the control, both of which should always be set.  If `Width` and `Height` are `16` and there is no `Padding` set, the ring's calculated [Radius](xref:@ActiproUIRoot.Controls.RingSpinner.Radius) will be `8`.

### Appearance

The `Foreground` and [LineThickness](xref:@ActiproUIRoot.Controls.RingSpinner.LineThickness) properties specify the `Brush` and thickness of the ring, extending inward from the outer radius.  The [LineCap](xref:@ActiproUIRoot.Controls.RingSpinner.LineCap) property (defaults to `Round`) designates the shape of the line ends.

> [!TIP]
> Use a [LineThickness](xref:@ActiproUIRoot.Controls.RingSpinner.LineThickness) value that is the same as the [Radius](xref:@ActiproUIRoot.Controls.RingSpinner.Radius) to render a pie.  The [LineCap](xref:@ActiproUIRoot.Controls.RingSpinner.LineCap) property should be set to `Flat` when rendering as a pie.

The `Background` property can be set to a `Brush` that will render the ring's track positioned behind the animated ring.  It is not specified by default, meaning there is no visible track unless the `Background` property is set.

### Usage Example

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<actipro:RingSpinner Width="24" Height="24" Classes="accent"
	Background="{actipro:ThemeResource Container5BackgroundBrush}"
	LineThickness="3" LineCap="Flat" IsSpinning="True" />
```
