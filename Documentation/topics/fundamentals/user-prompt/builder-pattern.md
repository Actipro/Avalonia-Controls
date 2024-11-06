---
title: "Builder Pattern"
page-title: "Builder Pattern - Fundamentals Controls"
order: 3
---
# Builder Pattern

The [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) class is used to configure and show a [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).  While a [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) can be instantiated and configured just like any other `Control`, the [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) can simplify the process.

> [!TIP]
> See the [MessageBox](message-box.md) topic for additional details on showing basic user prompts.

## Basic Usage

The basic flow for [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) is as follows:
1. Create a new [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).
2. Use `With*` methods to configure properties to be applied to the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) when it is built.
3. Show the prompt.

[UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) utilizes a fluent API, which means each method returns the class instance so additional methods can be called on the same instance. This is also referred to as method-chaining.

### Create Builder

To get started, call [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[Configure](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Configure*).  This method will create and return a new instance of [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) to be configured.

### Configure Properties

The [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) instance has multiple methods that can be called to specify which property values will be populated on the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) it creates.  The naming convention for most methods is `With<PropertyName>` where `<PropertyName>` is the name of the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) property to be configured.  For example, set the [DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) property by calling the [WithDefaultResult](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithDefaultResult*) method.

Use method-chaining to apply all desired configuration options.

> [!IMPORTANT]
> Each configured property on [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) is `nullable`, and each `With*` method accepts a `null` reference.  Only properties configured with non-`null` values will be configured on [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) when it is built.  Calling a `With*` method and passing `null` will effectively clear the configuration for that property.

#### Global Configuration

There are two methods available to register callbacks that are always invoked to configure each new instance of [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).

##### All User Prompts

The [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[RegisterGlobalConfigureCallback](xref:@ActiproUIRoot.Controls.UserPromptBuilder.RegisterGlobalConfigureCallback*) method is used to register a callback that is invoked immediately after creating a new instance of [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder). Use this method to define an application-wide configuration that will be applied to all user prompts without having to configure each builder instance individually.

@if (avalonia) {
The following example demonstrates how a global callback can be registered to use the `theme-solid accent` style classes on the default button (if any) for every prompt:

```csharp
UserPromptBuilder.RegisterGlobalConfigureCallback(_ => _
	.WithDefaultButtonClasses("theme-solid accent")
);
```
}
@if (wpf) {
The following example demonstrates how a global callback can be registered to customize the header for every prompt:

```csharp
UserPromptBuilder.RegisterGlobalConfigureCallback(_ => _
	.WithHeaderFontSize(16)
    .WithHeaderForeground(new SolidColorBrush(Colors.Blue))
);
```
}

@if (avalonia) {
##### MessageBox Only

The [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox).[RegisterGlobalBuilderConfigureCallback](xref:@ActiproUIRoot.Controls.MessageBox.RegisterGlobalBuilderConfigureCallback*) method is used to register a callback specifically for the builder used by [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox), and this callback is invoked after the previously mentioned global callback for all user prompts.  Use this method to specifically define application-wide configurations for the prompts displayed by [MessageBox](xref:@ActiproUIRoot.Controls.MessageBox).

The following example demonstrates how a global callback can be registered that alters the default behavior by displaying a message box title in the header of the prompt:

```csharp
MessageBox.RegisterGlobalBuilderConfigureCallback(_ => _
	.AfterBuild(builder => {
		// Configure UserPromptControl.Header with the Title
		builder.Instance!.Header = builder.Title;

		// Clear the Title configuration to avoid it appearing elsewhere
		builder.WithTitle(null);
	})
);
```
}
@if (wpf) {
##### ThemedMessageBox Only

The [ThemedMessageBox](xref:@ActiproUIRoot.Controls.ThemedMessageBox).[RegisterGlobalBuilderConfigureCallback](xref:@ActiproUIRoot.Controls.ThemedMessageBox.RegisterGlobalBuilderConfigureCallback*) method is used to register a callback specifically for the builder used by [ThemedMessageBox](xref:@ActiproUIRoot.Controls.ThemedMessageBox), and this callback is invoked after the previously mentioned global callback for all user prompts.  Use this method to specifically define application-wide configurations for the prompts displayed by [ThemedMessageBox](xref:@ActiproUIRoot.Controls.ThemedMessageBox).

The following example demonstrates how a global callback can be registered that alters the default behavior by displaying a message box title in the header of the prompt:

```csharp
ThemedMessageBox.RegisterGlobalBuilderConfigureCallback(_ => _
	.AfterBuild(builder => {
		// Configure UserPromptControl.Header with the Title
		builder.Instance!.Header = builder.Title;

		// Clear the Title configuration to avoid it appearing elsewhere
		builder.WithTitle(null);
	})
);
```
}

### Show Prompt

Finally, call the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method. This will create the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl), apply the desired configuration, display the prompt, and await a response from the user.

### Example

The following code sample demonstrates how [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) could be used to display a prompt to the user with response buttons of **Yes** or **No**.

@if (avalonia) {
```csharp
var result = await UserPromptBuilder.Configure()
	.WithHeaderContent("Overwrite existing file?")
	.WithContent("The specified file already exists. Do you want to overwrite the file?")
	.WithStandardButtons(MessageBoxButtons.YesNo)
	.WithStatusImage(MessageBoxImage.Question)
	.Show();
```
}
@if (wpf) {
```csharp
var result = UserPromptBuilder.Configure()
	.WithHeaderContent("Overwrite existing file?")
	.WithContent("The specified file already exists. Do you want to overwrite the file?")
	.WithStandardButtons(UserPromptStandardButtons.YesNo)
	.WithStatusImage(UserPromptStandardImage.Question)
	.Show();
```
}
The fluent API allows the entire configuration to be defined as a single statement.

@if (avalonia) {
> [!WARNING]
> The return value of [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) is a `Task<MessageBoxResult>` that must be awaited to prevent the calling thread from proceeding before the user responds.
}

See the [User Prompt Content](user-prompt-content.md) and [User Prompt Buttons](user-prompt-buttons.md) topics for more details and examples.

## Builder Lifecycle and Callbacks

Nothing happens with [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) until the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method is called.  At that point, the following sequence of events will occur:

@if (avalonia) {
- Create a [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) and assign to the [Instance](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Instance) property.
- Invoke all [AfterInitialize](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterInitialize*) callbacks.
- Assign properties on [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl). After this point, the [Instance](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Instance) property is fully initialized any changes to builder properties that affect the configuration of the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) *will not* be applied.
- Invoke all [AfterBuild](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterBuild*) callbacks.
- Use the [RequestedDisplayMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.RequestedDisplayMode) to determine how the prompt will be displayed and assign the [ActualDisplayMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.ActualDisplayMode) property to the mode that is used.
- Invoke all [BeforeShow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.BeforeShow*) callbacks.
- When displayed as a [Dialog](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Dialog)...
  - Configure [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) to host the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).
  - Invoke all [AfterInitializeWindow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterInitializeWindow*) callbacks.
  - Show the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) as a dialog and await a response.
- When displayed as an [Overlay](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Overlay)...
  - Configure an overlay to host the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).
  - Show the overlay and await a response.
- Invoke all [OnResponding](xref:@ActiproUIRoot.Controls.UserPromptBuilder.OnResponding*) callbacks when a response is indicated.
- Invoke all [AfterShow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterShow*) callbacks with the indicated result.
- Return the result.
}
@if (wpf) {
- Create a [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) and assign to the [Instance](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Instance) property.
- Invoke all [AfterInitialize](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterInitialize*) callbacks.
- Assign properties on [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl). After this point, the [Instance](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Instance) property is fully initialized any changes to builder properties that affect the configuration of the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) *will not* be applied.
- Invoke all [AfterBuild](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterBuild*) callbacks.
- Invoke all [BeforeShow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.BeforeShow*) callbacks.
- Configure [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) to host the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).
- Invoke all [AfterInitializeWindow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterInitializeWindow*) callbacks.
- Show the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) as a dialog and await a response.
- Invoke all [OnResponding](xref:@ActiproUIRoot.Controls.UserPromptBuilder.OnResponding*) callbacks when a response is indicated.
- Invoke all [AfterShow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterShow*) callbacks with the indicated result.
- Return the result.
}

Callbacks are available at different stages of the build process to support extensibility.

> [!NOTE]
> Adding a callback does not replace any already registered callbacks. Any newly registered callback will be invoked after previously registered callbacks are invoked.

### Instance Property

The [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[Instance](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Instance) is populated with the instance of [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) that is being built. This property will be `null` until the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method is called.  All callbacks can safely read this property to interact with the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) being built *except* the global configuration callbacks.

> [!TIP]
> To interact with the [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[Instance](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Instance) from one of the global configuration callbacks, use the global configuration to add a callback for one of the other callbacks (like [AfterInitialize](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterInitialize*)) that will be invoked after the instance is defined.

### Tag Property

The [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[Tag](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Tag) property can be used to store an arbitrary object value on the builder, and this can be used to store data that is accessibile from multiple callback methods.  Use the [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[WithTag](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithTag*) method to assign values to this property.

### AfterInitialize Callback

This callback is invoked immediately after an instance of [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) is created and can be used to initialize the control or further customize the builder before the builder configuration settings are applied.

@if (avalonia) {
```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterInitialize(builder => {
		// Define logic here
	})
	.Show();
```
}
@if (wpf) {
```csharp
var result = UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterInitialize(builder => {
		// Define logic here
	})
	.Show();
```
}

### AfterBuild Callback

This callback is invoked after all builder configuration settings have been applied and can be used to finalize the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) before it is shown.

@if (avalonia) {
```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterBuild(builder => {
		// Define logic here
	})
	.Show();
```
}
@if (wpf) {
```csharp
var result = UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterBuild(builder => {
		// Define logic here
	})
	.Show();
```
}

> [!IMPORTANT]
> At this stage in the lifecycle, the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) defined by [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[Instance](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Instance) is fully configured by the builder. Any changes made to the builder configuration at this point *will not* be applied to the control.

### BeforeShow Callback

@if (avalonia) {
This callback is invoked before attempting to show the prompt. At this point, the [RequestedDisplayMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.RequestedDisplayMode) has been evaluated and the [ActualDisplayMode](xref:@ActiproUIRoot.Controls.UserPromptBuilder.ActualDisplayMode) is assigned. This callback could be used to further customize the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) based on how it will be displayed.
}
@if (wpf) {
This callback is invoked before attempting to show the prompt. This callback could be used to further customize the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) before it is displayed.
}

@if (avalonia) {
### AfterInitializeWindow Callback (Dialogs Only)

When displayed as a [Dialog](xref:@ActiproUIRoot.Controls.UserPromptDisplayMode.Dialog), this callback is invoked to finalize the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) that will host the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) as a modal dialog.  The callback is invoked after the window is initially configured.

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterInitializeWindow(window => {
		// Define logic here
	})
	.Show();
```
}
@if (wpf) {
### AfterInitializeWindow Callback

This callback is invoked to finalize the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) that will host the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) as a modal dialog.  The callback is invoked after the window is initially configured.

```csharp
var result = UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterInitializeWindow(window => {
		// Define logic here
	})
	.Show();
```
}

### OnResponding Callback

This callback is invoked when the user indicates a response and can be used to confirm and/or cancel the response using the [UserPromptResponseEventArgs](xref:@ActiproUIRoot.Controls.UserPromptResponseEventArgs) that are passed.

@if (avalonia) {
```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.OnResponding((builder, args) => {
		// Define logic here
	})
	.Show();
```
}
@if (wpf) {
```csharp
var result = UserPromptBuilder.Configure()
	// ... other configuration options here
	.OnResponding((builder, args) => {
		// Define logic here
	})
	.Show();
```
}

### AfterShow Callback

@if (avalonia) {
This callback is invoked after the prompt is closed and passes the [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterShow((builder, result) => {
		// Define logic here
	})
	.Show();
```
}
@if (wpf) {
This callback is invoked after the prompt is closed and passes the [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult).

```csharp
var result = UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterShow((builder, result) => {
		// Define logic here
	})
	.Show();
```
}