---
title: "MeasureAdjuster"
page-title: "MeasureAdjuster - Shared Library Controls"
order: 7
---
# MeasureAdjuster

The [MeasureAdjuster](xref:@ActiproUIRoot.Controls.MeasureAdjuster) decorator can measure as zero size or round the measurement of its child content to integer values, even to the nearest even or odd numbers.  This ensures elements are measured and arranged as desired.

## Important Members

The [MeasureAdjuster](xref:@ActiproUIRoot.Controls.MeasureAdjuster) class has these important members:

| Member | Description |
|-----|-----|
| [CanMeasureHeight](xref:@ActiproUIRoot.Controls.MeasureAdjuster.CanMeasureHeight) Property | Gets or sets whether the decorator can return a non-zero height measurement. The default value is `true`. |
| [CanMeasureWidth](xref:@ActiproUIRoot.Controls.MeasureAdjuster.CanMeasureWidth) Property | Gets or sets whether the decorator can return a non-zero width measurement. The default value is `true`. |
| [HorizontalChildAlignment](xref:@ActiproUIRoot.Controls.MeasureAdjuster.HorizontalChildAlignment) Property | Gets or sets the `HorizontalAlignment` to apply to the `Decorator.Child`, commonly used when [CanMeasureWidth](xref:@ActiproUIRoot.Controls.MeasureAdjuster.CanMeasureWidth) is `false`. The default value is `HorizontalAlignment.Stretch`. |
| [HorizontalRoundMode](xref:@ActiproUIRoot.Controls.MeasureAdjuster.HorizontalRoundMode) Property | Gets or sets a nullable `RoundMode` indicating how to round the horizontal measurement of the child content. The default value is `null`, meaning do not perform any rounding. |
| [IdealSize](xref:@ActiproUIRoot.Controls.MeasureAdjuster.IdealSize) Property | Gets the ideal size of the decorator, which is only different from `Layoutable.DesiredSize` when [CanMeasureWidth](xref:@ActiproUIRoot.Controls.MeasureAdjuster.CanMeasureWidth) or [CanMeasureHeight](xref:@ActiproUIRoot.Controls.MeasureAdjuster.CanMeasureHeight) are used. |
| [VerticalChildAlignment](xref:@ActiproUIRoot.Controls.MeasureAdjuster.VerticalChildAlignment) Property | Gets or sets the `VerticalAlignment` to apply to the `Decorator.Child`, commonly used when [CanMeasureHeight](xref:@ActiproUIRoot.Controls.MeasureAdjuster.CanMeasureHeight) is `false`. The default value is `VerticalAlignment.Stretch`. |
| [VerticalRoundMode](xref:@ActiproUIRoot.Controls.MeasureAdjuster.VerticalRoundMode) Property | Gets or sets a nullable `RoundMode` indicating how to round the vertical measurement of the child content. The default value is `null`, meaning do not perform any rounding. |

## Rounding

The `Layoutable.DesiredSize` of the `Decorator.Content` can optionally round the width and/or height values using of one of the [RoundMode](xref:ActiproSoftware.RoundMode) enumeration values (e.g., [RoundMode](xref:ActiproSoftware.RoundMode).[Nearest](xref:ActiproSoftware.RoundMode.Nearest) will round to the nearest integer value, [RoundMode](xref:ActiproSoftware.RoundMode).[CeilingToEven](xref:ActiproSoftware.RoundMode.CeilingToEven) will round to the current or next highest even integer value).
- To round the width, set [HorizontalRoundMode](xref:@ActiproUIRoot.Controls.MeasureAdjuster.HorizontalRoundMode) to one of the [RoundMode](xref:ActiproSoftware.RoundMode) values.
- To round the height, set [VerticalRoundMode](xref:@ActiproUIRoot.Controls.MeasureAdjuster.VerticalRoundMode) to one of the [RoundMode](xref:ActiproSoftware.RoundMode) values.

### Centering Within a Parent

Say that you are trying to horizontally center an `Image` that is 32x32 pixels within a parent `StackPanel` that also has a `TextBlock` in it.  The `TextBlock` could lead to an unpredictable width based on the font size and content.

By wrapping the `TextBlock` with a [MeasureAdjuster](xref:@ActiproUIRoot.Controls.MeasureAdjuster) and setting [HorizontalRoundMode](xref:@ActiproUIRoot.Controls.MeasureAdjuster.HorizontalRoundMode) to [CeilingToEven](xref:ActiproSoftware.RoundMode.CeilingToEven), you ensure that the entire content of the `TextBlock` is visible, and that it forces the `StackPanel`'s width to an even number, allowing the `Image` to be centered horizontally directly on integer boundaries within the `StackPanel`.

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<StackPanel>

    <Image Source="/Images/Icons/Save32.png" Width="32" Height="32" />

    <!-- The TextBlock will always measure to an even integer width -->
    <actipro:MeasureAdjuster HorizontalRoundMode="CeilingToEven">
        <TextBlock Text="Save Current File" />
    </actipro:MeasureAdjuster>

</StackPanel>
```

## Zero-Size Measurement

The [MeasureAdjuster](xref:@ActiproUIRoot.Controls.MeasureAdjuster) can optionally return a width and/or height of zero during the measuring pass.  This is useful in scenarios where the content should take up some space, but you don't want the layout routines to consider that space during its measuring pass.

- To return a width of zero, set [CanMeasureWidth](xref:@ActiproUIRoot.Controls.MeasureAdjuster.CanMeasureWidth) to `false`.
- To return a height of zero, set [CanMeasureHeight](xref:@ActiproUIRoot.Controls.MeasureAdjuster.CanMeasureHeight) to `false`.

Content will still be arranged at the [IdealSize](xref:@ActiproUIRoot.Controls.MeasureAdjuster.IdealSize) even if width and/or height are zero.