---
title: "CopyButton"
page-title: "CopyButton - Shared Library Controls"
order: 10
---
# CopyButton

The [CopyButton](xref:@ActiproUIRoot.Controls.CopyButton) is a button that, when invoked, copies specified text to the clipboard.

![Screenshot](../images/copybutton-200%.png)

By default, the [CopyButton](xref:@ActiproUIRoot.Controls.CopyButton) control theme displays an icon consistent with the **Copy** clipboard operation.

Set the [CopyText](xref:@ActiproUIRoot.Controls.CopyButton.CopyText) property to the text that should be copied to the clipboard when the button is invoked.

## Feedback

When the button is invoked, feedback can be used to let the end user know the clipboard text was set. Otherwise, the end user might not realize any action was performed.  The following types of feedback are available:

| Kind | Description |
|-----|-----|
| [None](xref:@ActiproUIRoot.Controls.CopyButtonFeedbackKind.None) | No visual feedback is provided. |
| [Inline](xref:@ActiproUIRoot.Controls.CopyButtonFeedbackKind.Inline) | The icon of the button is temporarily changed (e.g., a checkmark is displayed on success).  |
| [Popup](xref:@ActiproUIRoot.Controls.CopyButtonFeedbackKind.Popup) | A flyout is displayed with text (e.g., `"Copied!"` is displayed on success). |
| [All](xref:@ActiproUIRoot.Controls.CopyButtonFeedbackKind.All) | (Default) Both [Inline](xref:@ActiproUIRoot.Controls.CopyButtonFeedbackKind.Inline) and [Popup](xref:@ActiproUIRoot.Controls.CopyButtonFeedbackKind.Popup) are used. |

Feedback is only displayed temporarily and will auto-hide (revert to the default state) after the time specified by the [FeedbackAutoHideDelay](xref:@ActiproUIRoot.Controls.CopyButton.FeedbackAutoHideDelay) property.

> [!IMPORTANT]
> Due to the API of clipboard operations, it is not possible to know, with certainty, if the clipboard was successfully updated.  As a result, the "success" feedback may be displayed in scenarios where the clipboard was not updated.

## Command

By default, the [CopyButton](xref:@ActiproUIRoot.Controls.CopyButton) is pre-configured with a `Command` that copies the [CopyText](xref:@ActiproUIRoot.Controls.CopyButton.CopyText) to the clipboard. This command is automatically enabled only when the [CopyText](xref:@ActiproUIRoot.Controls.CopyButton.CopyText) property is populated with text.

To override the default command, the `Command` property can be assigned to any instance of `ICommand`.  In this scenario, the custom command is responsible for all clipboard operations. If the command throws an exception while executing, the *failure* feedback will be displayed. Otherwise, the operation is assumed to have succeeded and the *success* feedback will be displayed.

## Example

The following example demonstrates how to define a [CopyButton](xref:@ActiproUIRoot.Controls.CopyButton):

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:CopyButton CopyText="This text is copied to the clipboard when invoked." />
```

## Pseudo-classes

The following pseudo-classes are added based on the feedback of the copy operation and can be used when styling the control:

| Class | Description |
|-----|-----|
| `:copy-failure` | The feedback for a failed copy operation is active. |
| `:copy-success` | The feedback for a successful copy operation is active. |

## Customize String Resources

The following string resources are available to localize or customize built-in strings:

| Resource key | Description |
|-----|-----|
| [UICopyButtonSuccessText](xref:ActiproSoftware.Properties.Shared.SRName.UICopyButtonSuccessText) | The text displayed as popup feedback on a successful copy operation. The default value is `"Copied!"`. |
| [UICopyButtonFailureText](xref:ActiproSoftware.Properties.Shared.SRName.UICopyButtonFailureText) | The text displayed as popup feedback on a failed copy operation. The default value is `"Error copying text!"`. |

See the [Customizing String Resources](../../customizing-string-resources.md) topic for additional details.