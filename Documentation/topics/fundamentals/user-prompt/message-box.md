---
title: "MessageBox"
page-title: "MessageBox - Fundamentals Controls"
order: 2
---
# MessageBox

The [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox) class is intentionally designed to be consistent with the native WPF `MessageBox` API and can be used to quickly display the most common prompts.

![Screenshot](../images/messagebox.png)

*MessageBox dialog with optional status image*

> [!WARNING]
> This functionality should only be used on desktop platforms that support windowing.

> [!TIP]
> See the [Builder Pattern](builder-pattern.md) topic for additional details on showing more advanced user prompts.

## Showing a MessageBox

The [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox).[Show](xref:@ActiproUIRoot.Controls.MessageBox.Show*) method is used to show a basic prompt based on the arguments passed.  The only required argument is the text to be displayed, so the simplest prompt can be shown as follows:

```csharp
await MessageBox.Show("Operation complete.");
```

There are two overloads of the [Show](xref:@ActiproUIRoot.Controls.MessageBox.Show*) method where one allows an owner  `TopLevel` (typically a `Window`) to be passed, and the other doesn't. All other arguments are identical.  The following basic arguments are available:

| Argument | Description |
|-----|-----|
| `messageBoxText` | The primary message text to be displayed. |
| `caption` | The title bar caption of the window. |
| `button` | One or more of the [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons) values for each button to display. |
| `image` | One of the [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage) values indicating a status image to display. |
| `defaultResult` | The default [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult), which must correspond to one of the given `button` values. |

The following code demonstrates prompting the user with a question and storing the result:

```csharp
var result = await MessageBox.Show(
	"The specified file already exists. Do you want to overwrite the file?",
	"Overwrite existing file?",
	MessageBoxButtons.YesNo,
	MessageBoxImage.Question
	MessageBoxResult.No);
```

## Owner

The user prompt must have an owner `TopLevel` (typically a `Window`). One of the [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox).[Show](xref:@ActiproUIRoot.Controls.MessageBox.Show*) overloads allows an owner to be specified.  If one is not specified, a default owner will be determined.  With desktop applications, the default is the currently active `Window`. On single-view applications, the default is the `TopLevel` of the current view.

## Advanced Configuration

Since the [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox) API is intentionally designed to be consistent with the native WPF `MessageBox` API, it only supports basic prompts.  By limiting the functionality, it makes [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox) easy to use.

For more advanced configurations, it is typically best to use the [UserPromptBuilder](builder-pattern.md) instead of [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox). In fact, [UserPromptBuilder](builder-pattern.md) is exactly what [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox).[Show](xref:@ActiproUIRoot.Controls.MessageBox.Show*) uses to display a prompt!

If you still want to use [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox) for advanced configurations, there is an optional `configure` argument to the [Show](xref:@ActiproUIRoot.Controls.MessageBox.Show*) method that allows you to define a callback that will receive the [UserPromptBuilder](builder-pattern.md) used for the [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox) before it is shown.

See the [Builder Pattern](builder-pattern.md) topic for more details on configuration options, including a global configuration callback is always applied to a message box.

The following demonstrates defining a `configure` callback to add a header message:

```csharp
await MessageBox.Show(
	"The project was successfully compiled and deployed to the remote server."
	configure: builder => builder.WithHeaderContent("Deploy successful!")
	);
```