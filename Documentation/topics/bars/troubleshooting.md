---
title: "Troubleshooting"
page-title: "Troubleshooting - Bars Reference"
order: 410
---
# Troubleshooting

This topic contains troubleshooting data specific to the Bars product.

> [!NOTE]
> For some more troubleshooting information that relates both to this product as well as other Actipro @@PlatformName products, please see the more general [Troubleshooting](../troubleshooting.md) topic.

@if (wpf) {
## FileNotFoundException or DirectoryNotFoundException When Specifying Bitmap Image Sources

If you use relative paths for your bitmap image sources, you might encounter a `FileNotFoundException` or `DirectoryNotFoundException` in some cases.  This exception can occur behind the scenes where the image is being reloaded outside of its original XAML context and thus can lose its contextual information for determining a relative path.  Using an absolute path via standard WPF pack syntax allows the image to be loaded properly in any scenario.

Many of the Bars controls like the buttons use the [DynamicImage](../shared/windows-controls/dynamicimage.md) control in their templates, which is a Shared Library control that allows for disabled buttons to render their images in grayscale.  Please see the [DynamicImage](../shared/windows-controls/dynamicimage.md) topic for more detailed information on this exception and exactly how to resolve it.
}

## Ribbon Shows in Designer but Disappears at Run-Time

This scenario could happen if you accidentally set a `Width` or `Height` on the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control or one of its containers that causes the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) to be smaller than the @if (avalonia) { collapse threshold size }@if (wpf) { [CollapseThresholdSize](xref:@ActiproUIRoot.Controls.Bars.Ribbon.CollapseThresholdSize) }.  When the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) becomes smaller than the @if (avalonia) { collapse threshold size }@if (wpf) { [CollapseThresholdSize](xref:@ActiproUIRoot.Controls.Bars.Ribbon.CollapseThresholdSize) } at run-time and [IsCollapsible](xref:@ActiproUIRoot.Controls.Bars.Ribbon.IsCollapsible) is set to `true`, the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) will hide.

This scenario is most likely to occur when you are adjusting control layouts in the designer and adjust the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) or a parent container's size accidentally.

To resolve this issue, simply remove any explicit `Width` or `Height` setting on the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) or a parent container that is causing the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon)'s size be smaller than @if (avalonia) { collapse threshold size }@if (wpf) { [CollapseThresholdSize](xref:@ActiproUIRoot.Controls.Bars.Ribbon.CollapseThresholdSize) }.  Alternatively, change [IsCollapsible](xref:@ActiproUIRoot.Controls.Bars.Ribbon.IsCollapsible) to `false`.

See the [Collapsing](ribbon-features/collapsing.md) topic for more details.

@if (wpf) {
## Menus Are Anchored in the Incorrect Location

This can occur when you are using a tablet PC or drawing tablet. The menu placement behavior will differ depending on whether the tablet support is configured for left-handed or right-handed use. This was designed so that menus do not appear under the user's hand.

To resolve this issue, simply change the tablet support configuration to left-handed.
}
