---
title: "User Prompt Buttons"
page-title: "User Prompt Buttons - Fundamentals Controls"
order: 5
---
# User Prompt Buttons

A user typically responds to a prompt by invoking a button that corresponds to the desired response. There are two ways to define buttons for a prompt:
1. Use a pre-defined set of one or more standard buttons.
1. Explicitly define each button.

@if (avalonia) {
By default, an [OK](xref:@ActiproUIRoot.Controls.MessageBoxButtons.OK) button will be displayed if no other button configuration is assigned.
}
@if (wpf) {
By default, an [OK](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons.OK) button will be displayed if no other button configuration is assigned.
}

## Standard Buttons

@if (avalonia) {
Most prompts can be built using one or more of the commonly used buttons defined by the [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons) enumeration.  These flags can easily be combined to display a fixed set of buttons to the user and are assigned to the [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property.

> [!TIP]
> Several popular combinations of buttons are predefined for convenience. For instance, the value [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons).[YesNoCancel](xref:@ActiproUIRoot.Controls.MessageBoxButtons.YesNoCancel) can be used instead of `MessageBoxButtons.Yes | MessageBoxButtons.No | MessageBoxButtons.Cancel`.
}
@if (wpf) {
Most prompts can be built using one or more of the commonly used buttons defined by the [UserPromptStandardButtons](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons) enumeration.  These flags can easily be combined to display a fixed set of buttons to the user and are assigned to the [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property.

> [!TIP]
> Several popular combinations of buttons are predefined for convenience. For instance, the value [UserPromptStandardButtons](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons).[YesNoCancel](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons.YesNoCancel) can be used instead of `UserPromptStandardButtons.Yes | UserPromptStandardButtons.No | UserPromptStandardButtons.Cancel`.
}

When using the [builder pattern](builder-pattern.md), the [WithStandardButtons](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithStandardButtons*) method is used to configure the [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property.

@if (avalonia) {
```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithContent("Error processing file. How do you want to proceed?")
	.WithStandardButtons(MessageBoxButtons.YesNo)
	.Show();
```
}
@if (wpf) {
```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithContent("Error processing file. How do you want to proceed?")
	.WithStandardButtons(UserPromptStandardButtons.YesNo)
	.Show();
```
}

> [!WARNING]
> The [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property is ignored if the [ButtonItems](xref:@ActiproUIRoot.Controls.UserPromptControl.ButtonItems) property is explicitly populated as discussed below.

## Explicit Button Definitions

While the [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) can be used for most prompts, some scenarios may require explicitly defining the individual buttons.  Examples include:
- Adding a button that is not available as one of the @if (avalonia) { [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons) }@if (wpf) { [UserPromptStandardButtons](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons) } values.
- Customizing the content of a button (e.g., changing the label or adding an icon).
- Assigning a custom command to the button.

 Collections of explicitly defined buttons are assigned to the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[ButtonItems](xref:@ActiproUIRoot.Controls.UserPromptControl.ButtonItems) property (which can be set to any `IEnumerable`). Each available object will be displayed as a `Button` by the [UserPromptButtonItemsControl](xref:@ActiproUIRoot.Controls.Primitives.UserPromptButtonItemsControl).

When using the [builder pattern](builder-pattern.md), the [WithButton](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithButton*) method is used to configure individual buttons that will be added to the [ButtonItems](xref:@ActiproUIRoot.Controls.UserPromptControl.ButtonItems) property. The simplest overload to [WithButton](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithButton*) accepts a @if (avalonia) { [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) }@if (wpf) { [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult) } that associates a response with the button. The overload also has optional arguments to define an `ICommand` or label.

@if (avalonia) {
```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(MessageBoxResult.Cancel)
	.WithButton(MessageBoxResult.Ignore, label: "Continue")
	.Show();
```
}
@if (wpf) {
```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(UserPromptStandardResult.Cancel)
	.WithButton(UserPromptStandardResult.Ignore, label: "Continue")
	.Show();
```
}

### Button Builder

For more advanced button definitions, a special [UserPromptButtonBuilder](xref:@ActiproUIRoot.Controls.UserPromptButtonBuilder) is utilized to build each button.  Similar to the [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder), the button builder is used to create and configure a `Button` control that is suitable for use on a user prompt.

@if (avalonia) {
The following example demonstrates using the button builder to create a button with a custom label and an alternate theme using Avalonia `Classes`:

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(MessageBoxResult.Ignore)
		.WithContent("Continue")
		.WithClasses("theme-solid")
	)
	.Show();
```
}
@if (wpf) {
The following example demonstrates using the button builder to create a button with a custom label:

```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(UserPromptStandardResult.Ignore)
		.WithContent("Continue")
	)
	.Show();
```
}

## Help Button

A special @if (avalonia) { [Help](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Help) }@if (wpf) { [Help](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons.Help) } button can be included in [StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons).  Invoking the **Help** button will execute the command designated by the [HelpCommand](xref:@ActiproUIRoot.Controls.UserPromptControl.HelpCommand) property.  An additional [HelpCommandParameter](xref:@ActiproUIRoot.Controls.UserPromptControl.HelpCommandParameter) can also be populated that will be passed to the [HelpCommand](xref:@ActiproUIRoot.Controls.UserPromptControl.HelpCommand) when invoked.

When using the [builder pattern](builder-pattern.md), the [WithHelpCommand](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithHelpCommand*) method is used to automatically configure an additional **Help** button and can be used with standard buttons or explicitly defined buttons  This method allows the [HelpCommand](xref:@ActiproUIRoot.Controls.UserPromptControl.HelpCommand) to be directly assigned, or a callback `Action` can be defined instead.  The following shows how to configure a **Help** button using a callback that will invoke a custom method for displaying help:

@if (avalonia) {
```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithHelpCommand(() => OpenHelp())
	.Show();
```
}
@if (wpf) {
```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithHelpCommand(() => OpenHelp())
	.Show();
```
}

> [!IMPORTANT]
> When an `ICommand` or callback `Action` is defined for the **Help** button, the prompt will *not* close when the **Help** button is clicked. Otherwise, the prompt will close and @if (avalonia) { [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).[Help](xref:@ActiproUIRoot.Controls.MessageBoxResult.Help) }@if (wpf) { [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult).[Help](xref:@ActiproUIRoot.Controls.UserPromptStandardResult.Help) } will be returned as the user response just like clicking any other button.

## ButtonResult Attached Property

The `UserPromptControl.ButtonResult` attached property is used to associate a `Button` with a specific @if (avalonia) { [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) }@if (wpf) { [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult) }.  Any `Button` without an explicitly defined `Button.Command` will be automatically assigned a special `ICommand` that, when invoked, will read the `UserPromptControl.ButtonResult` attached property of the `Button` and assign that value to [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result).

The `UserPromptControl.ButtonResult` attached property is also used to identify which `Button` corresponds to [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[DefaultResult](xref:@ActiproUIRoot.Controls.UserPromptControl.DefaultResult) and will receive keyboard focus when shown in a [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).

> [!TIP]
> The static helper methods [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[GetButtonResult](xref:@ActiproUIRoot.Controls.UserPromptControl.GetButtonResult*) and [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[SetButtonResult](xref:@ActiproUIRoot.Controls.UserPromptControl.SetButtonResult*) can be used to read and write the `UserPromptControl.ButtonResult` attached property of a `Button`.

When using the [builder pattern](builder-pattern.md), the button builder's [WithResult](xref:@ActiproUIRoot.Controls.UserPromptButtonBuilder.WithResult*) method is used to define the result.

@if (avalonia) {
```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(MessageBoxResult.Ignore)
	)
	.Show();
```
}
@if (wpf) {
```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(UserPromptStandardResult.Ignore)
	)
	.Show();
```
}

### Standard Button Result

@if (avalonia) {
Each individual value for [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons) has a corresponding value in [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).  For example, the [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons).[OK](xref:@ActiproUIRoot.Controls.MessageBoxButtons.OK) button corresponds to the [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult).[OK](xref:@ActiproUIRoot.Controls.MessageBoxResult.OK) result.  When using the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property to define buttons, the `UserPromptControl.ButtonResult` attached property is automatically populated with the corresponding [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult). Invoking one of the buttons will assign the value to [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result).

> [!NOTE]
> The [MessageBoxButtons](xref:@ActiproUIRoot.Controls.MessageBoxButtons).[Help](xref:@ActiproUIRoot.Controls.MessageBoxButtons.Help) button is primarily used to invoke the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[HelpCommand](xref:@ActiproUIRoot.Controls.UserPromptControl.HelpCommand).  If a [HelpCommand](xref:@ActiproUIRoot.Controls.UserPromptControl.HelpCommand) is defined, it does not assign a [Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result) and thus will not cause the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) to close when invoked.
}
@if (wpf) {
Each individual value for [UserPromptStandardButtons](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons) has a corresponding value in [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult).  For example, the [UserPromptStandardButtons](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons).[OK](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons.OK) button corresponds to the [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult).[OK](xref:@ActiproUIRoot.Controls.UserPromptStandardResult.OK) result.  When using the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[StandardButtons](xref:@ActiproUIRoot.Controls.UserPromptControl.StandardButtons) property to define buttons, the `UserPromptControl.ButtonResult` attached property is automatically populated with the corresponding [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult). Invoking one of the buttons will assign the value to [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result).

> [!NOTE]
> The [UserPromptStandardButtons](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons).[Help](xref:@ActiproUIRoot.Controls.UserPromptStandardButtons.Help) button is primarily used to invoke the [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[HelpCommand](xref:@ActiproUIRoot.Controls.UserPromptControl.HelpCommand).  If a [HelpCommand](xref:@ActiproUIRoot.Controls.UserPromptControl.HelpCommand) is defined, it does not assign a [Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result) and thus will not cause the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) to close when invoked.
}

## Custom Button Result

When populating [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[ButtonItems](xref:@ActiproUIRoot.Controls.UserPromptControl.ButtonItems) with custom `Button` instances, each button can optionally be associated with a specific @if (avalonia) { [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) }@if (wpf) { [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult) } using the `UserPromptControl.ButtonResult` attached property.  Any `Button` without an explicitly defined `Button.Command` will be automatically assigned a special `ICommand` that, when invoked, will read the `UserPromptControl.ButtonResult` attached property of the `Button` and assign that value to [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result).

> [!IMPORTANT]
> Any `Button` which defines its own `Button.Command` must manually assign an appropriate value to [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl).[Result](xref:@ActiproUIRoot.Controls.UserPromptControl.Result) when invoked.

@if (avalonia) {
The following code demonstrates how to assign a [MessageBoxResult](xref:@ActiproUIRoot.Controls.MessageBoxResult) to a custom button when building a [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl):

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(MessageBoxResult.CustomButton)
		.WithContent("Custom")
	)
	.Show();
```

If multiple custom buttons are required, a value can be added to [CustomButton](xref:@ActiproUIRoot.Controls.MessageBoxResult.CustomButton) to distinguish one custom button from another as shown below:

```csharp
var result = await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(MessageBoxResult.CustomButton + 1)
		.WithContent("Custom 1")
	)
	.WithButton(_ => _
		.WithResult(MessageBoxResult.CustomButton + 2)
		.WithContent("Custom 2")
	)
	.Show();

if (result == (MessageBoxResult.CustomButton + 1)) {
	// Custom 1 was selected
}
else if (result == (MessageBoxResult.CustomButton + 2)) {
	// Custom 2 was selected
}
```
}
@if (wpf) {
The following code demonstrates how to assign a [UserPromptStandardResult](xref:@ActiproUIRoot.Controls.UserPromptStandardResult) to a custom button when building a [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl):

```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(UserPromptStandardResult.CustomButton)
		.WithContent("Custom")
	)
	.Show();
```

If multiple custom buttons are required, a value can be added to [CustomButton](xref:@ActiproUIRoot.Controls.UserPromptStandardResult.CustomButton) to distinguish one custom button from another as shown below:

```csharp
var result = UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithButton(_ => _
		.WithResult(UserPromptStandardResult.CustomButton + 1)
		.WithContent("Custom 1")
	)
	.WithButton(_ => _
		.WithResult(UserPromptStandardResult.CustomButton + 2)
		.WithContent("Custom 2")
	)
	.Show();

if (result == (UserPromptStandardResult.CustomButton + 1)) {
	// Custom 1 was selected
}
else if (result == (UserPromptStandardResult.CustomButton + 2)) {
	// Custom 2 was selected
}
```
}