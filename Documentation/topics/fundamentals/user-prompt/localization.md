---
title: "Localization"
page-title: "Localization - Fundamentals Controls"
order: 30
---
# Localization

The string resources below are available to localize or customize built-in strings utilized by user prompt. Each string resource is defined in a specific assembly and corresponding namespace.

 See the [Customizing String Resources](../../customizing-string-resources.md) topic for additional details.

## Shared Library String Resources

The following string resources are defined in the Shared Library assembly and are accessible through types defined in the `ActiproSoftware.Properties.Shared` namespace:

| Resource key | Description |
|-----|-----|
| [UIButtonAbortText](xref:ActiproSoftware.Properties.Shared.SRName.UIButtonAbortText) | The label of the standard [Abort](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Abort) button. The default value is `"_Abort"`. |
| [UIButtonCancelText](xref:ActiproSoftware.Properties.Shared.SRName.UIButtonCancelText) | The label of the standard [Cancel](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Cancel) button. The default value is `"Cancel"`. |
| [UIButtonCloseText](xref:ActiproSoftware.Properties.Shared.SRName.UIButtonCloseText) | The label of the standard [Close](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Close) button. The default value is `"C_lose"`. |
| [UIButtonHelpText](xref:ActiproSoftware.Properties.Shared.SRName.UIButtonHelpText) | The label of the standard [Help](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Help) button. The default value is `"Help"`. |
| [UIButtonIgnoreText](xref:ActiproSoftware.Properties.Shared.SRName.UIButtonIgnoreText) | The label of the standard [Ignore](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Ignore) button. The default value is `"_Ignore"`. |
| [UIButtonNoText](xref:ActiproSoftware.Properties.Shared.SRName.UIButtonNoText) | The label of the standard [No](xref:@ActiproUIRoot.Controls.MessageBoxButtons.No) button. The default value is `"_No"`. |
| [UIButtonOKText](xref:ActiproSoftware.Properties.Shared.SRName.UIButtonOKText) | The label of the standard [OK](xref:@ActiproUIRoot.Controls.MessageBoxButtons.OK) button. The default value is `"OK"`. |
| [UIButtonRetryText](xref:ActiproSoftware.Properties.Shared.SRName.UIButtonRetryText) | The label of the standard [Retry](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Retry) button. The default value is `"_Retry"`. |
| [UIButtonYesText](xref:ActiproSoftware.Properties.Shared.SRName.UIButtonYesText) | The label of the standard [Yes](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Yes) button. The default value is `"_Yes"`. |
| [UICaptionButtonCloseText](xref:ActiproSoftware.Properties.Shared.SRName.UICaptionButtonCloseText) | The text displayed as the tooltip for the **Close** caption button used by [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow). The default value is `"Close"`. |
| [UICopyButtonFailureText](xref:ActiproSoftware.Properties.Shared.SRName.UICopyButtonFailureText) | The text displayed on the [exception prompt](extension-methods.md) if the [CopyButton](xref:@ActiproUIRoot.Controls.CopyButton) fails to copy the stack trace.  The default value is `"Error copying text!"`. |
| [UICopyButtonSuccessText](xref:ActiproSoftware.Properties.Shared.SRName.UICopyButtonSuccessText) | The text displayed on the [exception prompt](extension-methods.md) if the [CopyButton](xref:@ActiproUIRoot.Controls.CopyButton) successfully copies the stack trace.  The default value is `"Copied!"`. |
| [UIDialogTitleErrorText](xref:ActiproSoftware.Properties.Shared.SRName.UIDialogTitleErrorText) | The default title used by [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) when an explicit title is not defined and [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) is set to [Error](xref:@ActiproUIRoot.Controls.MessageBoxImage.Error). The default value is `"Error"`. |
| [UIDialogTitleIndeterminateText](xref:ActiproSoftware.Properties.Shared.SRName.UIDialogTitleIndeterminateText) | The default title used by [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) when an explicit title is not defined and [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) is set to [None](xref:@ActiproUIRoot.Controls.MessageBoxImage.None) or [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StatusImage) is populated. The default value is `String.Empty`. |
| [UIDialogTitleInformationText](xref:ActiproSoftware.Properties.Shared.SRName.UIDialogTitleInformationText) | The default title used by [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) when an explicit title is not defined and [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) is set to [Information](xref:@ActiproUIRoot.Controls.MessageBoxImage.Information). The default value is `"Information"`. |
| [UIDialogTitleQuestionText](xref:ActiproSoftware.Properties.Shared.SRName.UIDialogTitleQuestionText) | The default title used by [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) when an explicit title is not defined and [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) is set to [Question](xref:@ActiproUIRoot.Controls.MessageBoxImage.Question). The default value is `"Question"`. |
| [UIDialogTitleWarningText](xref:ActiproSoftware.Properties.Shared.SRName.UIDialogTitleWarningText) | The default title used by [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) when an explicit title is not defined and [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) is set to [Warning](xref:@ActiproUIRoot.Controls.MessageBoxImage.Warning). The default value is `"Warning"`. |

This code shows how to set custom values for string resources.

```csharp
ActiproSoftware.Properties.Shared.SR.SetCustomString(ActiproSoftware.Properties.Shared.SRName.UIButtonRetryText, "T_ry Again");
ActiproSoftware.Properties.Shared.SR.SetCustomString(ActiproSoftware.Properties.Shared.SRName.UIButtonIgnoreText, "Cont_inue");
```

## Fundamentals String Resources

The following string resources are defined in the Fundamentals assembly and are accessible through types defined in the `ActiproSoftware.Properties.Fundamentals` namespace:

| Resource key | Description |
|-----|-----|
| [UIExceptionPromptCopyButtonText](xref:ActiproSoftware.Properties.Fundamentals.SRName.UIExceptionPromptCopyButtonText) | The label of the [CopyButton](xref:@ActiproUIRoot.Controls.CopyButton) displayed on the [exception prompt](extension-methods.md) to copy the stack trace. The default value is `"Copy to Clipboard"`. |
| [UIExceptionPromptDetailsLabelText](xref:ActiproSoftware.Properties.Fundamentals.SRName.UIExceptionPromptDetailsLabelText) | The text of the label displayed on the [exception prompt](extension-methods.md) above the stack trace details.  The default value is `"_Stack Trace:"`. |
| [UIExceptionPromptToggleDetailsCollapsedText](xref:ActiproSoftware.Properties.Fundamentals.SRName.UIExceptionPromptToggleDetailsCollapsedText) | The text of the expanded information toggle on the [exception prompt](extension-methods.md) when the stack trace is collapsed.  The default value is `"Show _details"`. |
| [UIExceptionPromptToggleDetailsExpandedText](xref:ActiproSoftware.Properties.Fundamentals.SRName.UIExceptionPromptToggleDetailsExpandedText) | The text of the expanded information toggle on the [exception prompt](extension-methods.md) when the stack trace is expanded.  The default value is `"Hide _details"`. |

This code shows how to set custom values for string resources.

```csharp
ActiproSoftware.Properties.Fundamentals.SR.SetCustomString(ActiproSoftware.Properties.Fundamentals.SRName.UIExceptionPromptCopyButtonText, "Copy Stack Trace");
```

