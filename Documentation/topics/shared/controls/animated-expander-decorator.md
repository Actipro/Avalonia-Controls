---
title: "AnimatedExpanderDecorator"
page-title: "AnimatedExpanderDecorator - Shared Library Controls"
order: 4
---
# AnimatedExpander

The [AnimatedExpanderDecorator](xref:@ActiproUIRoot.Controls.AnimatedExpanderDecorator) is a `Decorator` that expands and contracts content using animation.

The [AnimatedExpanderDecorator](xref:@ActiproUIRoot.Controls.AnimatedExpanderDecorator) class has these important members:

| Member | Description |
|-----|-----|
| [ExpandDirection](xref:@ActiproUIRoot.Controls.AnimatedExpanderDecorator.ExpandDirection) Property | Gets or sets the direction in which to expand. The default value is `ExpandDirection.Down`. |
| [IsAnimationEnabled](xref:@ActiproUIRoot.Controls.AnimatedExpanderDecorator.IsAnimationEnabled) Property | Gets or sets whether animation is enabled. The default value is `true`. |
| [IsExpanded](xref:@ActiproUIRoot.Controls.AnimatedExpanderDecorator.IsExpanded) Property | Gets or sets if the decorator is expanded. The default value is `false`. |

> [!NOTE]
> The [AnimatedExpanderDecorator](xref:@ActiproUIRoot.Controls.AnimatedExpanderDecorator) is used in the Actipro Themes control template for the `Expander` control to provide the animation behavior, and is ideal for use in many other content reveal scenarios as well.

The following example demonstrates how the decorator can be used to animate the reveal of additional content:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<StackPanel>
	<CheckBox x:Name="showMoreCheckBox">Show more content</CheckBox>
	<actipro:AnimatedExpanderDecorator IsExpanded="{Binding #showMoreCheckBox.IsChecked}">
		<TextBlock>
			Define additional content here and it will animate into view when the checkbox is checked.
		</TextBlock>
	</actipro:AnimatedExpanderDecorator>
</StackPanel>
```
