---
title: "Clipboard Support"
page-title: "Clipboard Support - Fundamentals Controls"
order: 25
---
# Clipboard Support

[UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) supports copying text to the clipboard when the default clipboard keyboard shortcut (e.g., <kbd>Ctrl</kbd>+<kbd>C</kbd> on Windows) is invoked. Since [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) also supports rich content of potentially complex control structures, it may not be possible to accurately translate the various properties to textual clipboard content.

The properties in the following table have a corresponding string-based property that can be used to explicitly define the text to be placed on the system clipboard for that object when the copy command is invoked.

| Property | Clipboard Property |
|-----|-----|
| [ButtonItems](xref:@ActiproUIRoot.Controls.UserPromptControl.ButtonItems) | [ButtonItemsClipboardText](xref:@ActiproUIRoot.Controls.UserPromptControl.ButtonItemsClipboardText) |
| [CheckBoxContent](xref:@ActiproUIRoot.Controls.UserPromptControl.CheckBoxContent) | [CheckBoxContentClipboardText](xref:@ActiproUIRoot.Controls.UserPromptControl.CheckBoxContentClipboardText) |
| `Content` | [ContentClipboardText](xref:@ActiproUIRoot.Controls.UserPromptControl.ContentClipboardText) |
| [ExpandedInformationContent](xref:@ActiproUIRoot.Controls.UserPromptControl.ExpandedInformationContent) | [ExpandedInformationContentClipboardText](xref:@ActiproUIRoot.Controls.UserPromptControl.ExpandedInformationContentClipboardText) |
| [Footer](xref:@ActiproUIRoot.Controls.UserPromptControl.Footer) | [FooterClipboardText](xref:@ActiproUIRoot.Controls.UserPromptControl.FooterClipboardText) |
| `Header` | [HeaderClipboardText](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderClipboardText) |

When using the [builder pattern](builder-pattern.md), a corresponding `With*` method is available for each property (e.g., [WithHeaderClipboardText](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithHeaderClipboardText*) or [WithCheckBoxContentClipboardText](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithCheckBoxContentClipboardText*)).

## Buttons

When [ButtonItemsClipboardText](xref:@ActiproUIRoot.Controls.UserPromptControl.ButtonItemsClipboardText) is not explicitly defined, the value will be coerced from each button.  Use the `UserPromptControl.ButtonClipboardText` attached property to explicitly define the text to represent the button on the clipboard. When this property is undefined, the `Button.Content` will be examined to determine the most appropriate text.

When using the [builder pattern](builder-pattern.md), the attached property can be set using the [WithContentClipboardText](xref:@ActiproUIRoot.Controls.UserPromptButtonBuilder.WithContentClipboardText*) method of the button's builder as shown in the following example:

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(MessageBoxResult.Ignore)
		.WithContentClipboardText("Continue")
	)
	.Show();
```