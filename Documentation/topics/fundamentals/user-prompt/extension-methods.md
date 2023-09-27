---
title: "Extension Methods"
page-title: "Extension Methods - Fundamentals Controls"
order: 15
---
# Extension Methods

One of the benefits of the [builder pattern](builder-pattern.md) is that it's easy to extend the builder class with extension methods.

> [!IMPORTANT]
> Import the `ActiproSoftware.UI.Avalonia.Controls` namespace to include the built-in extension methods.

## Exception Prompt

A common scenario in application development is to catch an `Exception` and prompt the user that an error occurred.

![Screenshot](../images/user-prompt-for-exception.png)

*UserPromptControl configured to display details of an exception with stack trace expanded*

The [ForException](xref:@ActiproUIRoot.Controls.UserPromptExtensions.ForException*) extension method can be used to pre-configure the [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) to display an exception.  This includes the error status image, a summary of the error, and a detailed stack trace in the expanded information with a link to copy the stack trace to the clipboard.

The following sample demonstrates how easily a rich exception prompt can be created and shown to the user:

```csharp
try {
	//
}
catch (Exception ex) {
	await UserPromptBuilder.Configure()
		.ForException(ex, "Custom header text.")
		.Show();
}
```

> [!TIP]
> See the [Localization](localization.md) topic for details on how to customize the string resources used for exception prompt.