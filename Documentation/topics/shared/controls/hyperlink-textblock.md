---
title: "HyperlinkTextBlock"
page-title: "HyperlinkTextBlock - Shared Library Controls"
order: 6
---
# HyperlinkTextBlock

The [HyperlinkTextBlock](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock) is a `TextBlock` implementation that renders like a hyperlink and behaves like a `Button`.

![Screenshot](../images/hyperlink-text-block-125%.png)

*A HyperlinkTextBlock displayed in the default state*

The [HyperlinkTextBlock](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock) class has these important members:

| Member | Description |
|-----|-----|
| [Click](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock.Click) Event | Raised when the control is clicked. |
| [Command](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock.Command) Property | Gets or sets an `ICommand` to be executed when the control is clicked. |
| [CommandParameter](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock.CommandParameter) Property | Gets or sets an `Object` passed as a parameter to the `CanExecute` and `Execute` methods of the associated [Command](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock.Command). |
| [IsPressed](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock.IsPressed) Property | Gets if the hyperlink is currently pressed. |

## Example

The following examples demonstrate how to define a [HyperlinkTextBlock](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock):

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<!-- Click event (assumes code-behind defines click handler) -->
<actipro:HyperlinkTextBlock Click="OnHyperlinkTextBlockClicked" Text="This is a hyperlink" />

<!-- Command model (assumes a view model with a command is the DataContext -->
<actipro:HyperlinkTextBlock Command="{Binding HyperlinkCommand}" Text="This is a hyperlink" />

<!-- Used as an inline with other text -->
<TextBlock>
	<Run>Click </Run>
	<InlineUIContainer>
		<actipro:HyperlinkTextBlock Click="OnHyperlinkTextBlockClicked">here</actipro:HyperlinkTextBlock>
	</InlineUIContainer>
	<Run> for more information</Run>
</TextBlock>
```

## Pseudo-classes

The following pseudo-classes are added, as appropriate, based on the current state and can be used when styling the control:

| Class | Description |
|-----|-----|
| `:pressed` | Added when the [IsPressed](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock.IsPressed) property is `true`. |