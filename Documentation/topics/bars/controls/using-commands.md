---
title: "Using Commands"
page-title: "Using Commands - Bars Controls"
order: 200
---
# Using Commands

All interactive controls in this product are designed to work well with MVVM patterns and the @@PlatformTitle command model.

Controls will execute their command when a primary action occurs on the control, and use the `ICommand.CanExecute` result to determine if the control is enabled.  Commands can be used to support live preview for gallery items as well.

## Control Command Support

@if (avalonia) {
Many controls implement `ICommandSource`, meaning that they have `Command` and `CommandParameter` properties.  The `ICommand` that is assigned to a control can be any command, including those created by Actipro in the [ActiproSoftware.UI.Avalonia.Input](xref:@ActiproUIRoot.Input) namespace, custom commands, or any other third-party commands.
}
@if (wpf) {
Many controls implement `ICommandSource`, meaning that they have `Command`, `CommandParameter`, and `CommandTarget` properties.  The `ICommand` that is assigned to a control can be any command, including those defined in the WPF framework in the `System.Windows.Input` namespace, those created by Actipro in the [ActiproSoftware.Windows.Input](xref:@ActiproUIRoot.Input) namespace, custom commands, or any other third-party commands.
}

### Summary Table

This table summarizes the command support across some of the interactive controls in this product:

| Control | Description |
|-----|-----|
| [Button](button.md) | Executes a command when clicked. |
| [Popup Button](popup-button.md) | Uses the popup opening command to allow for dynamic popup content initialization and to determine control enabled state. |
| [Split Button](split-button.md) | Executes a command when the button portion is clicked. Uses the popup opening command to allow for dynamic popup content initialization and to determine the popup portion's enabled state. |
| [Checkbox](checkbox.md) | Executes a command when clicked. |
| [Gallery](gallery.md) | Executes a command when an item is clicked, passing the gallery item as the command parameter. |
| [Combobox](combobox.md) | Executes a command when text is committed.  Executes another command when text is committed that doesn't match any known gallery items, with the unknown text passed as the parameter. |
| [Textbox](textbox.md) | Executes a command when text is committed. |

See the individual topics for details on each control's command infrastructure.

## Command Interfaces

Two command-related interfaces are fundamental for command support by controls.

### ICommand

The `ICommand` interface provides the foundation of any command and includes support for executing a command and determining whether a command can execute.

Controls will execute the command when their primary action occurs, such as a button being clicked.  Controls will also use the `ICommand.CanExecute` result to determine if the control should be enabled.

Actipro provides useful implementations of the `ICommand` interface that are described below.

### IPreviewableCommand (Live Preview Support)

The [IPreviewableCommand](xref:@ActiproUIRoot.Input.IPreviewableCommand) interface inherits `ICommand` and adds support for starting and then later canceling a live preview.  A live preview can temporarily display the result of a command being executed without actually committing it.  This is often used with [gallery](gallery.md) controls in cases where hovering over a gallery item like a color temporarily displays the applied color.

These are the important methods on the [IPreviewableCommand](xref:@ActiproUIRoot.Input.IPreviewableCommand) interface and are called similarly to `ICommand` methods, where a command parameter is passed:

| Name | Description |
|-----|-----|
| [Preview](xref:@ActiproUIRoot.Input.IPreviewableCommand.Preview*) method | Starts a preview.  The logic should display the state that results if the command is executed. |
| [CancelPreview](xref:@ActiproUIRoot.Input.IPreviewableCommand.CancelPreview*) method | Cancels a preview.  The logic should revert the temporary display of the preview state. |

The caller of these methods should always cancel a previous preview prior to starting a new preview.

Actipro provides useful implementations of the [IPreviewableCommand](xref:@ActiproUIRoot.Input.IPreviewableCommand) interface that are described below.

@if (wpf) {
## Framework Command Types

The WPF framework has a couple built-in types that implement `ICommand`.

### RoutedCommand

`RoutedCommand` defines a command that is routed through the element tree.  The `Execute` and `CanExecute` methods on a `RoutedCommand` do not contain the application logic for the command as is the case with a typical `ICommand`, but rather, these methods raise events that traverse the element tree looking for an object with a `CommandBinding`.  The event handlers attached to the `CommandBinding` contain the command logic.

Screen tips and menu items are able to locate any `KeyBinding` that is on the `RoutedCommand` and can display its keyboard shortcut if no other input gesture text is specified.

> [!IMPORTANT]
> A `RoutedCommand` is dependent upon the visual tree for handling commands.  When working with [Ribbon](../ribbon-features/index.md), the [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage)  and [RibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar) controls are not always visual children of the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control. This means the ideal location to define a `CommandBinding` for a `RoutedCommand` is on the `Window` that contains the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control.

### RoutedUICommand

`RoutedUICommand` inherits `RoutedCommand` and has the same general design, but with the additional benefit that it contains a `Text` property too.
}

## Delegate Command Types

@if (avalonia) {
Delegate commands are passed a delegate for execute, can-execute, etc. logic.
}
@if (wpf) {
Delegate commands are often preferred to routed commands in more complex apps, and those that use MVVM.  Delegate commands are passed a delegate for execute, can-execute, etc. logic.
}

Actipro includes implementations of delegate commands in the Shared Library.

### DelegateCommand\<T\>

The [DelegateCommand\<T\>](xref:@ActiproUIRoot.Input.DelegateCommand`1) class implements a basic delegate command that supports execute and can-execute logic.  The generic type parameter for the class indicates the `Type` of command parameter that is passed.  Use `Object` if any or an unknown type of command parameter can be passed.

The following example shows how a [DelegateCommand\<T\>](xref:@ActiproUIRoot.Input.DelegateCommand`1) can be created to insert a symbol into a document in response to a gallery item click.

```csharp
var insertSymbolCommand = new DelegateCommand<SymbolBarGalleryItemViewModel>(
	executeAction: p => currentDocument?.Insert(p.Symbol),
	canExecuteFunc: p => currentDocument != null,
);
```

### PreviewableDelegateCommand\<T\>

The [PreviewableDelegateCommand\<T\>](xref:@ActiproUIRoot.Input.PreviewableDelegateCommand`1) class inherits [DelegateCommand\<T\>](xref:@ActiproUIRoot.Input.DelegateCommand`1) and augments it by implementing the [IPreviewableCommand](xref:@ActiproUIRoot.Input.IPreviewableCommand) interface for live preview support.  It adds delegates for starting and canceling live preview.

The following example shows how a [PreviewableDelegateCommand\<T\>](xref:@ActiproUIRoot.Input.PreviewableDelegateCommand`1) can be created to apply a text style to the selected text in a document and also support live preview.

```csharp
var setTextStyleCommand = new PreviewableDelegateCommand<TextStyleBarGalleryItemViewModel>(
	executeAction: p => {
		currentDocument?.ApplyTextStyle(p);
	},
	canExecuteFunc: p => currentDocument != null,
	previewAction: p => {
		currentDocument?.StartPreviewMode();
		currentDocument?.ApplyTextStyle(p);
	},
	cancelPreviewAction: p => {
		currentDocument?.CancelPreviewMode();
	}
);
```

## Composite Command Types

Composite commands are those that consist of one or more child commands working together as a single command.  This is useful for scenarios where there may be transient view models or more than one view model that is active at a time.  In those scenarios, and where each view model has a command, it's most effective to have the main window view model contain composite commands and have the child view models register their related commands with the appropriate composite command.  This way the main window view model only knows about a general command functionality provided and doesn't have to iterate through the child view models itself when trying to execute their commands.

Actipro includes implementations of composite commands in the Shared Library.

### CompositeCommand

The [CompositeCommand](xref:@ActiproUIRoot.Input.CompositeCommand) class implements a composite command that supports zero to many registered child commands.

A child command can be registered via the [CompositeCommand](xref:@ActiproUIRoot.Input.CompositeCommand).[RegisterCommand](xref:@ActiproUIRoot.Input.CompositeCommand.RegisterCommand*) or [TryRegisterCommand](xref:@ActiproUIRoot.Input.CompositeCommand.TryRegisterCommand*) methods.  The former will throw an exception if the child command being registered is already registered.  A child command can later be unregistered via the [UnregisterCommand](xref:@ActiproUIRoot.Input.CompositeCommand.UnregisterCommand*) method.

The [CompositeCommand](xref:@ActiproUIRoot.Input.CompositeCommand).[Execute](xref:@ActiproUIRoot.Input.CompositeCommand.Execute*) method will iterate each registered child command and will call their own `Execute` methods.

The [CompositeCommand](xref:@ActiproUIRoot.Input.CompositeCommand).[CanExecute](xref:@ActiproUIRoot.Input.CompositeCommand.CanExecute*) method will iterate registered child commands, however its behavior is influenced by the [RequireAllCommandsCanExecute](xref:@ActiproUIRoot.Input.CompositeCommand.RequireAllCommandsCanExecute) property setting.  If that property is `true`, all registered child commands will be checked to ensure that they all can execute.  If that property is `false`, the logic will only ensure that at least one registered child command can execute.

### PreviewableCompositeCommand

The [PreviewableCompositeCommand](xref:@ActiproUIRoot.Input.PreviewableCompositeCommand) class inherits [CompositeCommand](xref:@ActiproUIRoot.Input.CompositeCommand) and augments it by implementing the [IPreviewableCommand](xref:@ActiproUIRoot.Input.IPreviewableCommand) interface for live preview support.

The [PreviewableCompositeCommand](xref:@ActiproUIRoot.Input.PreviewableCompositeCommand).[Preview](xref:@ActiproUIRoot.Input.PreviewableCompositeCommand.Preview*) method will iterate all registered child commands that implement [IPreviewableCommand](xref:@ActiproUIRoot.Input.IPreviewableCommand), and will call their [Preview](xref:@ActiproUIRoot.Input.IPreviewableCommand.Preview*) methods.

The [PreviewableCompositeCommand](xref:@ActiproUIRoot.Input.PreviewableCompositeCommand).[CancelPreview](xref:@ActiproUIRoot.Input.PreviewableCompositeCommand.CancelPreview*) method will iterate all registered child commands that implement [IPreviewableCommand](xref:@ActiproUIRoot.Input.IPreviewableCommand), and will call their [CancelPreview](xref:@ActiproUIRoot.Input.IPreviewableCommand.CancelPreview*) methods.
