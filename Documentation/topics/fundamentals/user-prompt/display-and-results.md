---
title: "Display and Results"
page-title: "Display and Results - Fundamentals Controls"
order: 6
---
# Display and Results

Once the [User Prompt Content](user-prompt-content.md) and [User Prompt Buttons](user-prompt-buttons.md) have been defined, it is time to show the prompt and collect feedback from the user.

## Showing a User Prompt Dialog

On supported platforms, a user prompt is typically displayed as a modal dialog.  The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method can be used to display any [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) in a modal dialog that will automatically close and return a [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) when the user responds.

The following code demonstrates showing a [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) as a modal dialog using [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) and evaluating the result:

```csharp
var userPromptControl = new UserPromptControl() {
	Content = "This file has been modified. Do you want to save your changes before closing?",
	StandardButtons = MessageBoxButtons.YesNo,
};
var result = UserPromptWindow.ShowDialog(userPromptControl, "Save Changes?");
if (result == MessageBoxResult.Yes) {
	// Insert code to save changes here
}
```

When using the [builder pattern](builder-pattern.md), the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method is used to display a prompt based on the configuration and return the result as shown in the sample blow:

```csharp
var result = await UserPromptBuilder.Configure()
	.WithContent("This file has been modified. Do you want to save your changes before closing?")
	.WithStandardButtons(MessageBoxButtons.YesNo)
	.Show();
if (result == MessageBoxResult.Yes) {
	// Insert code to save changes here
}
```

> [!NOTE]
> See the "Configuring and Evaluating Results" section below for more details on working with the result of a prompt.

### Title

The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method optionally accepts an argument that will be used as the title of the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) when displayed.

When using the [builder pattern](builder-pattern.md), the [WithTitle](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithTitle*) method is used to set the title of the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow):

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithTitle("Actipro Avalonia Controls")
	.Show();
```

When a title is undefined or `null`, the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[DefaultTitle](xref:@ActiproUIRoot.Controls.UserPromptWindow.DefaultTitle) will be used. If [DefaultTitle](xref:@ActiproUIRoot.Controls.UserPromptWindow.DefaultTitle) is also `null` the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) property is used to provide a contextually appropriate title. For example, the title is set to `"Warning"` when the status image is [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage).[Warning](xref:@ActiproUIRoot.Controls.MessageBoxImage.Warning).

> [!TIP]
> See the [Localization](localization.md) topic for details on how to customize the string resources used for default titles.

### Close Caption Button

A [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) will only display a **Close** caption button in the title bar if a response can be associated with closing the window. The [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) property is used to define the result associated with closing the window instead of invoking an available button. When set to [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).[None](xref:@ActiproUIRoot.Controls.MessageBoxResult.None), the **Close** caption button will not be displayed.

When [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) is set to `null` and the [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property is used to define the available buttons, an appropriate [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) will be assigned to [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) based on the current value of [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons).

For example, using [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons).[YesNoCancel](xref:@ActiproUIRoot.Controls.MessageBoxButtons.YesNoCancel) will automatically assign [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons).[Cancel](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Cancel) as the [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult). Explicitly set the [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) to [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).[None](xref:@ActiproUIRoot.Controls.MessageBoxResult.None) to prevent the **Close** caption button from automatically displaying.

When using the [builder pattern](builder-pattern.md), the [WithCloseResult](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithCloseResult*) method is used to set the close result:

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithCloseResult(MessageBoxResult.Ignore)
	.Show();
```

## Configuring and Evaluating Results

The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result) property is used to read or write the user's response to a prompt and is typically only used when working with custom buttons.

The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method returns the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result).  If the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) is closed without invoking one of the available buttons, the value of [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) is returned instead.

The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Responding](xref:@ActiproUIRoot.Controls.UserPromptControl.Responding) event is raised when the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result) property is changed. [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) listens to this event to know when to close the dialog.

When using the [builder pattern](builder-pattern.md), the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method returns the same result as [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*).

### Default Button

Typically, the first available button will be the default button that receives initial focus. There are two ways to change this behavior to explicitly indicate which button should be the default: [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) and `Button.IsDefault`.

When standard buttons are configured using the [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property, the default button is defined by setting the [DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) property to the [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) that corresponds to the standard button.

When custom buttons are configured using the [ButtonItems](xref:@ActiproUIRoot.Controls.UserPromptControl.ButtonItems) property, the first `Button` control whose `IsDefault` property is set to `true` will be configured as the default.

When using the [builder pattern](builder-pattern.md), the button builder's [UseAsDefaultResult](xref:@ActiproUIRoot.Controls.UserPromptButtonBuilder.UseAsDefaultResult*) method is used to configure `Button.IsDefault` as `true`.

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(MessageBoxResult.Yes)
		.UseAsDefaultResult()
	)
	.Show();
```

> [!TIP]
> Custom buttons can also use the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) property to define the default button if the desired `Button` has set the `UserPromptControl.ButtonResult` attached property to a [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).

> [!WARNING]
> The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) property is ignored if any button has set `Button.IsDefault` to `true`.

### Responding Event

The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Responding](xref:@ActiproUIRoot.Controls.UserPromptControl.Responding) event is raised when the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result) property is changed. [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) listens to this event to know when to close the dialog.

When using the [builder pattern](builder-pattern.md), the [OnResponding](xref:@ActiproUIRoot.Controls.UserPromptBuilder.OnResponding*) method is used to define a callback that will receive the [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) and [UserPromptResponseEventArgs](xref:@ActiproUIRoot.Controls.UserPromptResponseEventArgs) of the event. The following demonstrates how the callback can be used to cancel a response:

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.OnResponding((builder, args) => {
		if ((builder?.Instance is UserPromptControl userPromptControl) && (userPromptControl.IsChecked)) {
			// Cancel the response
			MessageBox.Show($"Cancelling response of '{args.Response}'", "Result Canceled");
			args.Cancel = true;
		}
	})
	.Show();
```

> [!WARNING]
> The [Responding](xref:@ActiproUIRoot.Controls.UserPromptControl.Responding) event handler or [OnResponding](xref:@ActiproUIRoot.Controls.UserPromptBuilder.OnResponding*) callback must be executed synchronously so the thread will be blocked waiting for the response. Otherwise, the prompt will close before the event/callback has completed executing.

## Owner Window

The user prompt is shown as a modal dialog and must have an owner `Window`. Some of the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) overloads allows an owner to be specified.  If one is not specified, a default owner `Window` will be determined by the currently active `Window`.

When using the [builder pattern](builder-pattern.md), the [WithOwner](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithOwner*) method is used to assign a non-default owner `Window`.

## Startup Location

By default, a [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).`WindowStartupLocation` is set to `CenterScreen`, but this can be changed.

The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method accepts a callback that can be used to configure the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) before it is displayed. The following sample demonstrates changing the startup location to `CenterOwner`:

```csharp
var userPromptControl = new UserPromptControl() { ... };
Window owner = null; // Use default

await UserPromptWindow.ShowDialog(
	userPromptControl,
	"Window Title",
	owner,
	window => {
		window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
	});
```

See the "Customize UserPromptWindow" section of the [Customizing Appearance](appearance.md) topic for more examples of how to set properties like `WindowStartupLocation` on the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).

When using the [builder pattern](builder-pattern.md), the [WithWindowStartupLocation](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithWindowStartupLocation*) method is used to specify a desired startup location.

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithWindowStartupLocation(WindowStartupLocation.CenterOwner)
	.Show();
```

> [!TIP]
> When using the [builder pattern](builder-pattern.md) with auto-sizing, prompts that can be sized within the bounds of their owner `Window` will be configured with `WindowStartupLocation.CenterOwner` and only larger prompts will default to `WindowStartupLocation.CenterScreen`. This dynamic centering, which is the default behavior, is only available if an explicit startup location is not specified.
>
> See the "Auto-Sizing" section below for more details.

## Auto-Sizing

Auto-sizing is a feature only available when using the [builder pattern](builder-pattern.md). By default, this feature is only enabled when a `String` is used as the `Content` of the prompt, but the [WithAutoSize](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithAutoSize*) method can be used to explicitly enable the functionality for non-`String` content as well.

Auto-sizing involves a series of calculations that attempt to generate the ideal width of the prompt.  The goal is to start at a given width and then ensure the prompt does not exceed a relative height. If the prompt is too tall, the width is increased in logical steps until the height is within a desired range.

The minimum width, accessible by [AutoSizeMinimumWidth](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AutoSizeMinimumWidth), is assigned using one of the available arguments passed to the [WithAutoSize](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithAutoSize*) method.

When [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[WindowStartupLocation](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WindowStartupLocation) is `null` (the default), the auto-size logic will first attempt to size the prompt to a width and height that do not exceed the bounds of the owner `Window`. This is important to allow `WindowStartupLocation.CenterOwner` to be used without some parts of the prompt potentially displayed off screen.  Otherwise, the prompt will be sized within the working area of the screen so that `WindowStartupLocation.CenterScreen` can be used to fully display the prompt.

> [!TIP]
> Auto-size logic is also applied to [MessageBox](message-box.md) since it uses the [builder pattern](builder-pattern.md) to create prompts. This mean that, by default, [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox).[Show](xref:@ActiproUIRoot.Controls.MessageBox.Show*) will always attempt to center messages over the owner, but will fall back to center screen if the message is too big.