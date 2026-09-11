---
title: "Converting to v26.2"
page-title: "Converting to v26.2 - Conversion Notes"
order: 92
---
# Converting to v26.2

## RoundMode Changes

A new [RoundMode](xref:ActiproSoftware.RoundMode).[None](xref:ActiproSoftware.RoundMode.None) value was added to the enumeration and should be used to express "no rounding" instead of a `null` value combined with `Nullable<RoundMode>`.

This change only affected two properties on the [MeasureAdjuster](xref:@ActiproUIRoot.Controls.MeasureAdjuster) class: [HorizontalRoundMode](xref:@ActiproUIRoot.Controls.MeasureAdjuster.HorizontalRoundMode) and [VerticalRoundMode](xref:@ActiproUIRoot.Controls.MeasureAdjuster.VerticalRoundMode)
- Property `Type` changed from `Nullable<RoundMode>` to `RoundMode`.
- The default value changed from `null` to [None](xref:ActiproSoftware.RoundMode.None).
- Any logic related to a `null` value should be replaced by [None](xref:ActiproSoftware.RoundMode.None) instead.

