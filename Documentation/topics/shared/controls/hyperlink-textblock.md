---
title: "HyperlinkTextBlock"
page-title: "HyperlinkTextBlock - Shared Library Controls"
order: 20
---
# HyperlinkTextBlock

The [HyperlinkTextBlock](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock) is a `TextBlock` implementation that renders like a hyperlink and behaves like a `Button`.

![Screenshot](../images/hyperlink-text-block-125%.png)

*A HyperlinkTextBlock displayed in the default state*

> [!IMPORTANT]
> See the [Getting Started](../getting-started.md) topic for details on configuring themes for this control.

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

## Font Sizing

An explicit size can be set using one of following style class names:

- `size-xs` - An extra-small font size.
- `size-sm` - A small font size.
- `size-md` - A normal font size (default).
- `size-lg` - A large font size.
- `size-xl` - An extra-large font size.

## Pseudo-classes

The following pseudo-classes are added, as appropriate, based on the current state and can be used when styling the control:

| Class | Description |
|-----|-----|
| `:pressed` | Added when the [IsPressed](xref:@ActiproUIRoot.Controls.HyperlinkTextBlock.IsPressed) property is `true`. |