---
title: "MeshGradientPresenter"
page-title: "MeshGradientPresenter - Shared Library Controls"
order: 30
---
# MeshGradientPresenter

The [MeshGradientPresenter](xref:@ActiproUIRoot.Controls.MeshGradientPresenter) control renders a gradient background that consists of multiple stacked radial gradients positioned at various locations.

![Screenshot](../images/meshgradientpresenter.png)

## Defining Colors

The base color is defined by the [Background](xref:@ActiproUIRoot.Controls.MeshGradientPresenter.Background) property, which can be any `IBrush`.  Then one or more [MeshGradientNode](xref:@ActiproUIRoot.Controls.MeshGradientNode) instances are added to the [Nodes](xref:@ActiproUIRoot.Controls.MeshGradientPresenter.Nodes) collection, each one defining a radial gradient with a [Color](xref:@ActiproUIRoot.Controls.MeshGradientNode.Color) that will be stacked with its [Center](xref:@ActiproUIRoot.Controls.MeshGradientNode.Center) at a specific position.

## Example

The following example demonstrates how to define a [MeshGradientPresenter](xref:@ActiproUIRoot.Controls.MeshGradientPresenter) using four nodes, some with the same color:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:MeshGradientPresenter Height="80" Width="400" Background="#ffed8fea" CornerRadius="10">
	<actipro:MeshGradientNode Center="1%, 95%" Color="#ffb781fd" />
	<actipro:MeshGradientNode Center="30%, 0%" Color="#ffb781fd" />
	<actipro:MeshGradientNode Center="99%, 37%" Color="#ffeec6ad" />
	<actipro:MeshGradientNode Center="83%, 95%" Color="#ffeec6ad" />
</actipro:MeshGradientPresenter>
```