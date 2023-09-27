---
title: "Customizing Appearance"
page-title: "Customizing Appearance - Fundamentals Controls"
order: 20
---
# Customizing Appearance

Several properties are available to customize the appearance of the control. The following additional customizations are also available:

## Header

The [HeaderBackground](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderBackground), [HeaderForeground](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderForeground), [HeaderFontSize](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderFontSize), [HeaderBorderBrush](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderBorderBrush), [HeaderBorderThickness](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderBorderThickness) properties are available to customize the header.

When either the [HeaderBackground](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderBackground) or [HeaderBorderBrush](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderBorderBrush) properties are populated, the layout of the **Header** and **Status Image** will automatically shift their vertical alignment for a consistent layout.

[UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder) defines equivalent methods to set these properties with the [builder pattern](builder-pattern.md) (e.g., [WithHeaderBackground](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithHeaderBackground*) and [WithHeaderForeground](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithHeaderForeground*)).  For any `IBrush`-based property, one method overload will accept an `IBrush` and the other will accept a `Color` that will automatically be converted to a `SolidColorBrush`.

## Images

Any `IImage` can be set to the [StatusImage](xref:@ActiproUIRoot.Controls.UserPromptControl.StatusImage) or [FooterImage](xref:@ActiproUIRoot.Controls.UserPromptControl.FooterImage) properties for a custom look.  When using the [builder pattern](builder-pattern.md), set the `IImage` using [WithStatusImage](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithStatusImage*) or [WithFooterImage](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithFooterImage*).

The images used by [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage) can also be customized by assigning a custom [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) property. Each value for [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage) corresponds to a key of the same name defined by [SharedImageKeys](xref:@ActiproUIRoot.Media.SharedImageKeys). For example, the image [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage).[Warning](xref:@ActiproUIRoot.Controls.MessageBoxImage.Warning) corresponds to the key [SharedImageKeys](xref:@ActiproUIRoot.Media.SharedImageKeys).[Warning](xref:@ActiproUIRoot.Media.SharedImageKeys.Warning). A custom class which derives from [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) can override the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[GetImageSource](xref:@ActiproUIRoot.Media.ImageProvider.GetImageSource*) method to return a custom `IImage` for one or more of those keys.

## Customize UserPromptWindow

The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) does not have a public constructor and is only created by the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method.  One of the arguments for [ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) allows you to pass an `Action<UserPromptWindow>` which is invoked with a reference to the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) after it is configured, but before it is shown. This callback allows the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) to be customized.

The following code demonstrates using the callback to customize the flow direction of the window:

```csharp
var userPromptControl = new UserPromptControl() { ... };
Window owner = null; // Use default

await UserPromptWindow.ShowDialog(
	userPromptControl,
	"Window Title",
	owner,
	window => {
		window.FlowDirection = FlowDirection.RightToLeft;
	});
```

The [builder pattern](builder-pattern.md) exposes the same customization using the [AfterInitializeWindow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterInitializeWindow*) callback as shown in the following example:

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterInitializeWindow(window => {
		window.FlowDirection = FlowDirection.RightToLeft;
	})
	.Show();
```

Finally, an advanced configuration of [MessageBox](message-box.md) also allows access to the [builder pattern](builder-pattern.md) as shown below:

```csharp
await MessageBox.Show(
	"Use the optional 'configure' parameter to access the UserPromptBuilder."
	configure: builder => builder
		.AfterInitializeWindow(window => {
			window.FlowDirection = FlowDirection.RightToLeft;
		})
	);
```