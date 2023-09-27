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

### Show Prompt

Finally, call the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method. This will create the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl), apply the desired configuration, display the prompt, and await a response from the user.

### Example

The following code sample demonstrates how [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) could be used to display a prompt to the user with response buttons of **Yes** or **No**.

```csharp
var result = await UserPromptBuilder.Configure()
	.WithHeaderContent("Overwrite existing file?")
	.WithContent("The specified file already exists. Do you want to overwrite the file?")
	.WithStandardButtons(MessageBoxButtons.YesNo)
	.WithStatusImage(MessageBoxImage.Question)
	.Show();
```

The fluent API allows the entire configuration to be defined as a single statement.

See the [User Prompt Content](user-prompt-content.md) and [User Prompt Buttons](user-prompt-buttons.md) topics for more details and examples.

## Builder Lifecycle and Callbacks

Nothing happens with [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) until the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method is called.  At that point, the following sequence of events will occur:

- Create a [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) and assign to the [Instance](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Instance) property.
- Invoke all [AfterInitialize](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterInitialize*) callbacks.
- Configure properties on [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).
- Invoke all [AfterBuild](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterBuild*) callbacks.
- Configure [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) to host the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).
- Invoke all [AfterInitializeWindow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterInitializeWindow*) callbacks.
- Show the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) and await a response.
- Invoke all [OnResponding](xref:@ActiproUIRoot.Controls.UserPromptBuilder.OnResponding*) callbacks when a response is indicated.
- Invoke all [AfterShow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterShow*) callbacks with the indicated result.
- Return the result.

Callbacks are available at different stages of the build process to support extensibility.

> [!NOTE]
> Adding a callback does not replace any already registered callbacks. Any newly registered callback will be invoked after previously registered callbacks are invoked.

### Instance Property

The [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[Instance](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Instance) is populated with the instance of [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) that is being built. This property will be `null` until the [Show](xref:@ActiproUIRoot.Controls.UserPromptBuilder.Show*) method is called.  All callbacks can safely read this property to interact with the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) being built.

### AfterInitialize Callback

This callback is invoked immediately after an instance of [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) is created and can be used to initialize the control before the builder configuration settings are applied.

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterInitialize(builder => {
		// Define logic here
	})
	.Show();
```

### AfterBuild Callback

This callback is invoked after all builder configuration settings have been applied and can be used to finalize the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) before it is shown.

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterBuild(builder => {
		// Define logic here
	})
	.Show();
```

### AfterInitializeWindow Callback

This callback is invoked to finalize the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) that will host the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) as a modal dialog.  The callback is invoked after the window is initially configured.

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterInitializeWindow(window => {
		// Define logic here
	})
	.Show();
```

### OnResponding Callback

This callback is invoked when the user indicates a response and can be used to confirm and/or cancel the response using the [UserPromptResponseEventArgs](xref:@ActiproUIRoot.Controls.UserPromptResponseEventArgs) that are passed.

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.OnResponding((builder, args) => {
		// Define logic here
	})
	.Show();
```

### AfterShow Callback

This callback is invoked after the prompt is closed and passes the [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterShow((builder, result) => {
		// Define logic here
	})
	.Show();
```
