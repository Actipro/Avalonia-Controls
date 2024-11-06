---
title: "Display and Results"
page-title: "Display and Results - Fundamentals Controls"
order: 6
---
# Display and Results

Once the [User Prompt Content](user-prompt-content.md) and [User Prompt Buttons](user-prompt-buttons.md) have been defined, it is time to show the prompt and collect feedback from the user.

@if (avalonia) {
## Display Mode

When using the [builder pattern](builder-pattern.md), the following options are available for displaying a user prompt (as defined by [UserPromptDisplayMode](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode)):

| Value | Description |
| ----- | ----- |
| [Dialog](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Dialog) | On supported platforms, the prompt is hosted in a `Window` and shown as a dialog. Using this mode on unsupported platforms will throw `PlatformNotSupportedException`. |
| [Overlay](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Overlay) | The prompt is hosted on an overlay that is displayed over the owning `TopLevel` (e.g., `Window` or browser view). |
| [DialogPreferred](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.DialogPreferred) | [Dialog](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Dialog) will be used on supported platforms; otherwise, [Overlay](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Overlay) will be used where dialogs are not supported. (Default) |

The [DialogPreferred](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.DialogPreferred) mode is the default, but the static [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[DefaultDisplayMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.DefaultDisplayMode) property can be used to change the default to a different value.

The [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[WithDisplayMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithDisplayMode*) method can be used to configure the [RequestedDisplayMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.RequestedDisplayMode) of an individual builder instance to a non-default value.

> [!WARNING]
> Requesting [Dialog](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Dialog) mode on unsupported platforms will throw `PlatformNotSupportedException`. Generally, [DialogPreferred](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.DialogPreferred) should be requested instead.

The actual display mode used may differ from the display mode requested! For example, if an existing user prompt is currently displayed as an overlay, additional user prompts will also display as an overlay even if dialogs are supported and requested.  Before the prompt is shown, the [ActualDisplayMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.ActualDisplayMode) property will be set to either [Dialog](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Dialog) or [Overlay](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Overlay) based on the display mode being used.
}

## Showing a User Prompt Dialog

The [builder pattern](builder-pattern.md) is recommended for showing user prompts, but is not required.  @if (avalonia) { On supported platforms, a user prompt is typically displayed as a modal dialog. } The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method can be used to display any [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) in a modal dialog that will automatically close and return a @if (avalonia) { [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) }@if (wpf) { [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult) } when the user responds.

The following code demonstrates showing a [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) as a modal dialog using [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) and evaluating the result:

@if (avalonia) {
```csharp
var userPromptControl = new UserPromptControl() {
	Content = "This file has been modified. Do you want to save your changes before closing?",
	StandardButtons = MessageBoxButtons.YesNo,
};
var result = await UserPromptWindow.ShowDialog(userPromptControl, "Save Changes?");
if (result == MessageBoxResult.Yes) {
	// Insert code to save changes here
}
```
}
@if (wpf) {
```csharp
var userPromptControl = new UserPromptControl() {
	Content = "This file has been modified. Do you want to save your changes before closing?",
	StandardButtons = UserPromptStandardButtons.YesNo,
};
var result = UserPromptWindow.ShowDialog(userPromptControl, "Save Changes?");
if (result == UserPromptStandardResult.Yes) {
	// Insert code to save changes here
}
```
}

When using the [builder pattern](builder-pattern.md), the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method is used to display a prompt based on the configuration and return the result as shown in the sample blow:

@if (avalonia) {
```csharp
var result = await UserPromptBuilder.Configure()
	.WithContent("This file has been modified. Do you want to save your changes before closing?")
	.WithStandardButtons(MessageBoxButtons.YesNo)
	.Show();
if (result == MessageBoxResult.Yes) {
	// Insert code to save changes here
}
```
}
@if (wpf) {
```csharp
var result = UserPromptBuilder.Configure()
	.WithContent("This file has been modified. Do you want to save your changes before closing?")
	.WithStandardButtons(UserPromptStandardButtons.YesNo)
	.Show();
if (result == UserPromptStandardResult.Yes) {
	// Insert code to save changes here
}
```
}

@if (avalonia) {
> [!WARNING]
> The return value of [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) and [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) is a `Task<MessageBoxResult>` that must be awaited to prevent the calling thread from proceeding before the user responds.  Attempting to read the `Task.Result` without awaiting its completion can result in thread deadlock.
}

> [!NOTE]
> See the "Configuring and Evaluating Results" section below for more details on working with the result of a prompt.

### Title

All `Window` instances should have a title, so the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method optionally accepts an argument that will be used as the title of the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) when displayed.

When using the [builder pattern](builder-pattern.md), the [WithTitle](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithTitle*) method is used to set the title associated with the user prompt:

@if (avalonia) {
```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithTitle("Actipro Avalonia Controls")
	.Show();
```
}
@if (wpf) {
```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithTitle("Actipro WPF Controls")
	.Show();
```
}

@if (avalonia) {
#### Overlay Title Mode

When using the [builder pattern](builder-pattern.md) and displaying a prompt as an [Overlay](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Overlay), the following modes are available for displaying titles (as defined by [UserPromptOverlayTitleMode](xref:@ActiproUIRoot.Controls.UserPromptOverlayTitleMode)):

| Value | Description |
| ----- | ----- |
| [AlwaysHide](xref:@ActiproUIRoot.Controls.UserPromptOverlayTitleMode.AlwaysHide) | The overlay title is always hidden, even when [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[Title](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Title) is configured. |
| [AlwaysShow](xref:@ActiproUIRoot.Controls.UserPromptOverlayTitleMode.AlwaysShow) | The overlay title is always shown. |
| [ShowWhenNoHeader](xref:@ActiproUIRoot.Controls.UserPromptOverlayTitleMode.ShowWhenNoHeader) | An overlay title is only shown when [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).`Header` is not defined since a header and title are typically redundant. (Default) |

The [ShowWhenNoHeader](xref:@ActiproUIRoot.Controls.UserPromptOverlayTitleMode.ShowWhenNoHeader) mode is the default, but the static [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[DefaultOverlayTitleMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.DefaultOverlayTitleMode) property can be used to change the default to a different value.

The [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[WithOverlayTitleMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithOverlayTitleMode*) method can be used to configure the [OverlayTitleMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.OverlayTitleMode) of an individual builder instance to a non-default value.
}

#### Inferred and Fallback Titles

@if (avalonia) {
When a title is undefined or `null`, the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardStatusIcon](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusIcon) property is used to infer a contextually appropriate title. For example, the title is set to `"Warning"` when the status icon is [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage).[Warning](xref:@ActiproUIRoot.Controls.MessageBoxImage.Warning).
}
@if (wpf) {
When a title is undefined or `null`, the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) property is used to infer a contextually appropriate title. For example, the title is set to `"Warning"` when the status image is [UserPromptStandardImage](xref:@ActiproUIRoot.Controls.UserPromptStandardImage).[Warning](xref:@ActiproUIRoot.Controls.UserPromptStandardImage.Warning).
}

If a title cannot be inferred, the static [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[FallbackTitle](xref:@ActiproUIRoot.Controls.UserPromptBuilder.FallbackTitle) value will be used.

If a title is still undefined, the `TitleAttribute` metadata of the entry assembly will be used.

> [!TIP]
> See the [Localization](localization.md) topic for details on how to customize the string resources used for inferred titles.

### Close Caption Button

@if (avalonia) {
A [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) will only display a **Close** caption button in the title bar if a response can be associated with closing the window. The [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) property is used to define the result associated with closing the window instead of invoking an available button. When set to [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).[None](xref:@ActiproUIRoot.Controls.MessageBoxResult.None), the **Close** caption button will not be displayed.

When [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) is set to `null` and the [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property is used to define the available buttons, an appropriate [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) will be assigned to [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) based on the current value of [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons).

For example, using [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons).[YesNoCancel](xref:@ActiproUIRoot.Controls.MessageBoxButtons.YesNoCancel) will automatically assign [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons).[Cancel](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Cancel) as the [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult). Explicitly set the [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) to [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).[None](xref:@ActiproUIRoot.Controls.MessageBoxResult.None) to prevent the **Close** caption button from automatically displaying.
}
@if (wpf) {
A [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) will only display a **Close** caption button in the title bar if a response can be associated with closing the window. The [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) property is used to define the result associated with closing the window instead of invoking an available button. When set to [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult).[None](xref:@ActiproUIRoot.Controls.UserPromptStandardResult.None), the **Close** caption button will not be displayed.

When [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) is set to `null` and the [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property is used to define the available buttons, an appropriate [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult) will be assigned to [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) based on the current value of [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons).

For example, using [UserPromptStandardButtons](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons).[YesNoCancel](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons.YesNoCancel) will automatically assign [UserPromptStandardButtons](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons).[Cancel](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons.Cancel) as the [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult). Explicitly set the [CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) to [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult).[None](xref:@ActiproUIRoot.Controls.UserPromptStandardResult.None) to prevent the **Close** caption button from automatically displaying.
}

When using the [builder pattern](builder-pattern.md), the [WithCloseResult](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithCloseResult*) method is used to set the close result:

@if (avalonia) {
```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithCloseResult(MessageBoxResult.Ignore)
	.Show();
```
}
@if (wpf) {
```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithCloseResult(UserPromptStandardResult.Ignore)
	.Show();
```
}

## Configuring and Evaluating Results

The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result) property is used to read or write the user's response to a prompt and is typically only used when working with custom buttons.

@if (avalonia) {
The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method returns a `Task` that, once completed, will return the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result).  If the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) is closed without invoking one of the available buttons, the value of [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) is used instead.
}
@if (wpf) {
The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method returns the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result).  If the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) is closed without invoking one of the available buttons, the value of [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[CloseResult](xref:@ActiproUIRoot.Controls.UserPromptControl.CloseResult) is returned instead.
}

The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Responding](xref:@ActiproUIRoot.Controls.UserPromptControl.Responding) event is raised when the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result) property is changed. [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) listens to this event to know when to close the dialog.

When using the [builder pattern](builder-pattern.md), the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method behaves the same as
[UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*).

@if (avalonia) {
> [!WARNING]
> The various `Show` and `ShowDialog` methods return a `Task<MessageBoxResult>` that must be awaited to prevent the calling thread from proceeding before the user responds.  Attempting to read the `Task.Result` without awaiting its completion can result in thread deadlock.

The following example demonstrates the correct way to evaluate the result:

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.Show();
```
}

### Default Button

Typically, the first available button will be the default button that receives initial focus. There are two ways to change this behavior to explicitly indicate which button should be the default: [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) and `Button.IsDefault`.

When standard buttons are configured using the [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property, the default button is defined by setting the [DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) property to the @if (avalonia) { [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) }@if (wpf) { [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult) } that corresponds to the standard button.

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
> Custom buttons can also use the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) property to define the default button if the desired `Button` has set the `UserPromptControl.ButtonResult` attached property to a @if (avalonia) { [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) }@if (wpf) { [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult) }.

> [!WARNING]
> The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) property is ignored if any button has set `Button.IsDefault` to `true`.

### Responding Event

The [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Responding](xref:@ActiproUIRoot.Controls.UserPromptControl.Responding) event is raised when the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result) property is changed. [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) listens to this event to know when to close the dialog.

When using the [builder pattern](builder-pattern.md), the [OnResponding](xref:@ActiproUIRoot.Controls.UserPromptBuilder.OnResponding*) method is used to define a callback that will receive the [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) and [UserPromptResponseEventArgs](xref:@ActiproUIRoot.Controls.UserPromptResponseEventArgs) of the event. The following demonstrates how the callback can be used to cancel a response:

@if (avalonia) {
```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.OnResponding((builder, args) => {
		if ((builder?.Instance is UserPromptControl userPromptControl) && (userPromptControl.IsChecked)) {
			// Cancel the response
			args.Cancel = true;
		}
	})
	.Show();
```
}
@if (wpf) {
```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.OnResponding((builder, args) => {
		if ((builder?.Instance is UserPromptControl userPromptControl) && (userPromptControl.IsChecked)) {
			// Cancel the response
			args.Cancel = true;
		}
	})
	.Show();
```
}

> [!WARNING]
> The [Responding](xref:@ActiproUIRoot.Controls.UserPromptControl.Responding) event handler or [OnResponding](xref:@ActiproUIRoot.Controls.UserPromptBuilder.OnResponding*) callback must be executed synchronously so the thread will be blocked waiting for the response. Otherwise, the prompt will close before the event/callback has completed executing.

## Owner

When the user prompt is shown as a modal dialog, it must have an owner `Window`. Some of the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) overloads allow an owner to be specified.  If one is not specified, a default owner `Window` will be determined by the currently active `Window`.

When using the [builder pattern](builder-pattern.md), the [WithOwner](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithOwner*) method is used to assign a non-default owner. Since the builder pattern supports displaying dialogs or overlays, the owner is specified as a `TopLevel` (which, on desktop applications, is typically a `Window`).

## Startup Location

By default, a [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).`WindowStartupLocation` is set to `CenterScreen`, but this can be changed.

The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method accepts a callback that can be used to configure the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) before it is displayed. The following sample demonstrates changing the startup location to `CenterOwner`:

@if (avalonia) {
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
}
@if (wpf) {
```csharp
var userPromptControl = new UserPromptControl() { ... };
Window owner = null; // Use default

UserPromptWindow.ShowDialog(
	userPromptControl,
	"Window Title",
	owner,
	window => {
		window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
	});
```
}

See the "Customize UserPromptWindow" section of the [Customizing Appearance](appearance.md) topic for more examples of how to set properties like `WindowStartupLocation` on the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).

When using the [builder pattern](builder-pattern.md), the [WithWindowStartupLocation](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithWindowStartupLocation*) method is used to specify a desired startup location.

@if (avalonia) {
```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithWindowStartupLocation(WindowStartupLocation.CenterOwner)
	.Show();
```
}
@if (wpf) {
```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithWindowStartupLocation(WindowStartupLocation.CenterOwner)
	.Show();
```
}

> [!TIP]
> When using the [builder pattern](builder-pattern.md) with auto-sizing, prompts that can be sized within the bounds of their owner `Window` will be configured with `WindowStartupLocation.CenterOwner` and only larger prompts will default to `WindowStartupLocation.CenterScreen`. This dynamic centering, which is the default behavior, is only available if an explicit startup location is not specified.
>
> See the "Auto-Sizing" section below for more details.

## Auto-Sizing

Auto-sizing is a feature only available when using the [builder pattern](builder-pattern.md). By default, this feature is only enabled when a `String` is used as the `Content` of the prompt, but the [WithAutoSize](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithAutoSize*) method can be used to explicitly enable the functionality for non-`String` content as well.

Auto-sizing involves a series of calculations that attempt to generate the ideal width of the prompt.  The goal is to start at a given width and then ensure the prompt does not exceed a relative height. If the prompt is too tall, the width is increased in logical steps until the height is within a desired range.

The minimum width, accessible by [AutoSizeMinimumWidth](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AutoSizeMinimumWidth), is assigned using one of the available arguments passed to the [WithAutoSize](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithAutoSize*) method.

When [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[WindowStartupLocation](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WindowStartupLocation) is `null` (the default), the auto-size logic will first attempt to size the prompt to a width and height that do not exceed the bounds of the owner. This is important to allow `WindowStartupLocation.CenterOwner` to be used without some parts of the prompt potentially displayed off screen.  Otherwise, if it is not displayed as an overlay, the prompt will be sized within the working area of the screen so that `WindowStartupLocation.CenterScreen` can be used to fully display the prompt.

@if (avalonia) {
> [!TIP]
> Auto-size logic is also applied to [MessageBox](message-box.md) since it uses the [builder pattern](builder-pattern.md) to create prompts. This mean that, by default, [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox).[Show](xref:@ActiproUIRoot.Controls.MessageBox.Show*) will always attempt to center messages over the owner, but will fall back to center screen if the message is too big.
}
@if (wpf) {
> [!TIP]
> Auto-size logic is also applied to [ThemedMessageBox](message-box.md) since it uses the [builder pattern](builder-pattern.md) to create prompts. This mean that, by default, [ThemedMessageBox](xref:@ActiproUIRoot.Controls.ThemedMessageBox).[Show](xref:@ActiproUIRoot.Controls.ThemedMessageBox.Show*) will always attempt to center messages over the owner, but will fall back to center screen if the message is too big.
}

@if (wpf) {
## Playing a SystemSound

When a native `MessageBox` is displayed, an associated `System.Media.SystemSound` may be played based on the value of `MessageBoxImage` used.  Set [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[SystemSound](xref:@ActiproUIRoot.Controls.UserPromptControl.SystemSound) to any `SystemSound` and it will be played when the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) is shown.

When [SystemSound](xref:@ActiproUIRoot.Controls.UserPromptControl.SystemSound) is set to `null` and the [StandardStatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardStatusImage) property is used to define the status image, an appropriate `SystemSound` will be automatically assigned based on the image as shown in the following table:

| StandardStatusImage | SystemSound |
|-----|-----|
| [None](xref:@ActiproUIRoot.Controls.UserPromptStandardImage.None) | None |
| [Error](xref:@ActiproUIRoot.Controls.UserPromptStandardImage.Error) | `System.Media.SystemSounds.Hand` |
| [Information](xref:@ActiproUIRoot.Controls.UserPromptStandardImage.Information) | `System.Media.SystemSounds.Asterisk` |
| [Question](xref:@ActiproUIRoot.Controls.UserPromptStandardImage.Question) | `System.Media.SystemSounds.Question` |
| [Warning](xref:@ActiproUIRoot.Controls.UserPromptStandardImage.Warning) | `System.Media.SystemSounds.Exclamation` |

}