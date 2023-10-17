---
title: "User Prompt Content"
page-title: "User Prompt Control - Fundamentals Controls"
order: 4
---
# User Prompt Content

To build a user prompt, you can create a new instance of [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) and set the appropriate properties just like any other `Control`. The control can be defined in XAML, but prompts often have varying content that is composed, as needed, in code.

![Screenshot](../images/user-prompt.png)

*UserPromptControl with optional content areas labeled, OK/Cancel buttons, Information status image, and optional footer image*

## Content

The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) is comprised of the following content areas:
- Message - The primary content of the prompt, which is usually one or more sentences of text.
- Header - An optional header at the top of the prompt that might define the main instruction.
- Footer - An optional footer shown at the bottom of the prompt.
- Checkbox - An optional checkbox that allows the user to toggle an option.
- Expanded information - An optional area below the footer that shows more information to the user but is collapsed until the user expands it.

While typically set to `String`-based text values, each content area can support any content allowed by `ContentPresenter`.  This allows for complex content that includes additional controls like progress bars, radio buttons, or formatted text.

### Using Builder Pattern

When using the [builder pattern](builder-pattern.md), various `With*` methods are available to set the content of each area (e.g., [WithContent](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithContent*) or [WithHeaderContent](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithHeaderContent*)).  Each method has three overloads:
1. `With*(object? content)` - Directly set any `Object` as content, typically a `String`.
1. `With*(Func<object?>? object)` - Define a factory method that, when invoked, will return the content for the area.
1. `With*(Func<UserPromptControl, object?>? object)` - Define a factory method that, when invoked, is passed the instance of [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) being configured and returns the content for the area.

The last overload is particularly useful when the content needs to reference the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl). For example, custom content might only be visible when the prompt is expanded and would need to bind it's `IsVisible` property to the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[IsExpanded](xref:@ActiproUIRoot.Controls.UserPromptControl.IsExpanded) property.

> [!NOTE]
> Each `With*` content method overload supports a `null` reference, and passing `null` will effectively clear any previously defined content.

### Message (Primary Content)

The `Content` property defines the primary message of the prompt. While typically set to a `String`-based value, this is also the most appropriate content area to add progress bars, radio buttons, or other controls.

Any `TextBlock` controls used within `Content` will wrap text by default.

When using the [builder pattern](builder-pattern.md), the [WithContent](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithContent*) method is used to configure the `Content` property.

```csharp
// Simple message
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithContent("Simple string-based content")
	.Show();

// Complex content
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithContent(
		new StackPanel() {
			Children = {
				new TextBlock() { Text = "Copying..." },
				new ProgressBar() { Minimum = 0, Maximum = 100, Value = 25 }
			}
		}
	)
	.Show();
```

#### Scrollbars
The default [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) template defines `Content` within a `ScrollViewer`. Use the [HorizontalScrollBarVisibility](xref:@ActiproUIRoot.Controls.UserPromptControl.HorizontalScrollBarVisibility) and [VerticalScrollBarVisibility](xref:@ActiproUIRoot.Controls.UserPromptControl.VerticalScrollBarVisibility) properties to manage scroll bar visibility.

When using the [builder pattern](builder-pattern.md), the [WithScrollBarVisibility](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithScrollBarVisibility*) method is used to configure both scrollbars.

### Header

The `Header` property can be defined to prominently display content at the top of the prompt.

Any `TextBlock` controls used within `Header` will wrap text by default. Any `TextElement` will also have the `FontSize` and `Foreground` properties default to the [HeaderFontSize](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderFontSize) and [HeaderForeground](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderForeground) property values, respectively.

> [!TIP]
> Consider using `Header` as an alternative to a dialog window title when captioning a prompt since it is more prominently displayed to the user.

When using the [builder pattern](builder-pattern.md), the [WithHeaderContent](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithHeaderContent*) method is used to configure the `Header` property.

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithHeaderContent("Export complete")
	.WithContent("Your project has been successfully exported.")
	.Show();
};
```

### Footer

The `Footer` property can be defined to display content at the bottom of the prompt. Adding a hyperlink to a footer that links to more information is also a common practice.

Any `TextBlock` controls used within `Footer` will wrap text by default.

Use the [FooterImage](xref:@ActiproUIRoot.Controls.UserPromptControl.FooterImage) property to optionally display a 16x16 image next to the `Footer` content. Any `IImage` is supported.

When using the [builder pattern](builder-pattern.md), the [WithFooterContent](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithFooterContent*) method is used to configure the [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptControl.Footer) property and the [WithFooterImage](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithFooterImage*) method configures the [FooterImage](xref:@ActiproUIRoot.Controls.UserPromptControl.FooterImage) property.

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithFooterContent("Click 'Help' button for more information")
	.WithFooterImage(GetImage("/Images/Icons/Help16.png"))
	.Show();
```

### CheckBox

The [CheckBoxContent](xref:@ActiproUIRoot.Controls.UserPromptControl.CheckBoxContent) property can be defined to display a `CheckBox` control near the bottom of the prompt. Content is typically a `String`-based value with full support for `AccessText`. This `CheckBox` is commonly used to allow users to indicate they no longer want to receive a particular prompt.

The [IsChecked](xref:@ActiproUIRoot.Controls.UserPromptControl.IsChecked) property indicates the checked state of the `CheckBox` control.

When using the [builder pattern](builder-pattern.md), the [WithCheckBoxContent](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithCheckBoxContent*) method is used to configure the [CheckBoxContent](xref:@ActiproUIRoot.Controls.UserPromptControl.CheckBoxContent) property and the [WithIsChecked](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithIsChecked*) method is used to configure the [IsChecked](xref:@ActiproUIRoot.Controls.UserPromptControl.IsChecked) property:

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithCheckBoxContent("_Stop showing messages like this")
	.WithIsChecked(true)
	.Show();
```

> [!NOTE]
> The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) only displays the `CheckBox` and manages the state. The developer is responsible for persisting settings and suppressing any prompts the user has requested not to be displayed.

When using the [builder pattern](builder-pattern.md), an overload of [WithIsChecked](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithIsChecked*) allows one delegate to be passed as the "getter" of the initial checked state, and another as the "setter" of the final checked state. This can be used to easily synchronize the checked state of the prompt with a local variable as shown below:

```csharp
var isChecked = false;
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithCheckBoxContent("_Stop showing messages like this")
	.WithIsChecked(
		getter: () => isChecked,
		setter: (value) => isChecked = value
	)
	.Show();
if (isChecked && (result == MessageBoxResult.OK)) {
	// Persist user request to stop prompting
}
```

### Expanded Information

The [ExpandedInformationContent](xref:@ActiproUIRoot.Controls.UserPromptControl.ExpandedInformationContent) property can be defined to enable the user to toggle the display of additional content that might not be ideal to display all the time.

Any `TextBlock` controls used within `ExpandedInformationContent` will wrap text by default.

When [ExpandedInformationContent](xref:@ActiproUIRoot.Controls.UserPromptControl.ExpandedInformationContent) is defined, a toggle control will appear at the bottom of the prompt that the user can invoke to display the additional information.  Use the [ExpandedInformationCollapsedHeaderText](xref:@ActiproUIRoot.Controls.UserPromptControl.ExpandedInformationCollapsedHeaderText) property to specify the header of the toggle when it is collapsed, and the [ExpandedInformationExpandedHeaderText](xref:@ActiproUIRoot.Controls.UserPromptControl.ExpandedInformationExpandedHeaderText) property to specify the header when expanded.  The [IsExpanded](xref:@ActiproUIRoot.Controls.UserPromptControl.IsExpanded) property tracks the expansion state.

When using the [builder pattern](builder-pattern.md), the [WithExpandedInformationContent](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithExpandedInformationContent*) method is used to set the [ExpandedInformationContent](xref:@ActiproUIRoot.Controls.UserPromptControl.ExpandedInformationContent) property, the [WithExpandedInformationHeaderText](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithExpandedInformationHeaderText*) method is used to set both the [ExpandedInformationCollapsedHeaderText](xref:@ActiproUIRoot.Controls.UserPromptControl.ExpandedInformationCollapsedHeaderText) and [ExpandedInformationExpandedHeaderText](xref:@ActiproUIRoot.Controls.UserPromptControl.ExpandedInformationExpandedHeaderText) properties, and the [WithIsExpanded](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithIsExpanded*) method is used to configure the initial value of the [IsExpanded](xref:@ActiproUIRoot.Controls.UserPromptControl.IsExpanded) property:

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithExpandedInformationHeaderText("Show more", "Show less")
	.WithExpandedInformationContent("This content will be hidden until the user clicks 'Show more'.")
	.Show();
```

## Status Image

An image can be used to visually communicate the status or classification of the prompt displayed. Users can quickly differentiate an error from a warning based on the image displayed with the prompt.

The [StatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StatusImage) property can be set to any 32x32 `IImage`.

Several common images, like those for an error or warning, are defined by the [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage) enumeration.  Instead of populating the [StatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StatusImage) property, set the [StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) property to one of the [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage) values.

> [!WARNING]
> The [StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) property is ignored if the [StatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StatusImage) property is explicitly populated.

When using the [builder pattern](builder-pattern.md), the [WithStatusImage](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithStatusImage*) has one overload that accepts a custom `IImage` and another for a standard [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage):

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithStatusImage(MessageBoxImage.Information)
	.Show();
};
```

