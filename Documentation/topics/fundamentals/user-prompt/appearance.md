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

@if (avalonia) {
Any `object` can be set to the [StatusIcon](xref:@ActiproUIRoot.Controls.UserPromptControl.StatusIcon) or [FooterIcon](xref:@ActiproUIRoot.Controls.UserPromptControl.FooterIcon) properties for a custom look.  When using the [builder pattern](builder-pattern.md), set the `object` using [WithStatusIcon](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithStatusIcon*) or [WithFooterIcon](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithFooterIcon*).

Since icon properties are defined as `object` value types, a corresponding `IDataTemplate` is typically required to define how the value should be presented.  The [StatusIconTemplate](xref:@ActiproUIRoot.Controls.UserPromptControl.StatusIconTemplate) and [FooterIconTemplate](xref:@ActiproUIRoot.Controls.UserPromptControl.FooterIconTemplate) properties can be used to define an appropriate `IDataTemplate`.  If a `IDataTemplate` is not explicitly defined, the [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter).[DefaultContentTemplate](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter.DefaultContentTemplate) will be used, which is pre-configured to handle common value types like `IImage`.  See the [Icon Presenter](../../themes/icon-presenter.md) topic for details on how to customize the data templates or add support for additional data types.

The icons used by [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage) can also be customized by assigning a custom [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) property. Each value for [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage) corresponds to a key of the same name defined by [SharedImageSourceKeys](xref:@ActiproUIRoot.Media.SharedImageSourceKeys). For example, the image [MessageBoxImage](xref:@ActiproUIRoot.Controls.MessageBoxImage).[Warning](xref:@ActiproUIRoot.Controls.MessageBoxImage.Warning) corresponds to the key [SharedImageSourceKeys](xref:@ActiproUIRoot.Media.SharedImageSourceKeys).[Warning](xref:@ActiproUIRoot.Media.SharedImageSourceKeys.Warning). A custom class which derives from [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) can override the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[GetImageSource](xref:@ActiproUIRoot.Media.ImageProvider.GetImageSource*) method to return a custom `IImage` for one or more of those keys.
}
@if (wpf) {
Any `ImageSource` can be set to the [StatusImageSource](xref:@ActiproUIRoot.Controls.UserPromptControl.StatusImageSource) or [FooterImageSource](xref:@ActiproUIRoot.Controls.UserPromptControl.FooterImageSource) properties for a custom look.  When using the [builder pattern](builder-pattern.md), set the `ImageSource` using [WithStatusImage](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithStatusImage*) or [WithFooterImage](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithFooterImage*).

The images used by [UserPromptStandardImage](xref:@ActiproUIRoot.Controls.UserPromptStandardImage) can also be customized by assigning a custom [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) to the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) property. Each value for [UserPromptStandardImage](xref:@ActiproUIRoot.Controls.UserPromptStandardImage) corresponds to a key of the same name defined by [SharedImageSourceKeys](xref:@ActiproUIRoot.Media.SharedImageSourceKeys). For example, the image [UserPromptStandardImage](xref:@ActiproUIRoot.Controls.UserPromptStandardImage).[Warning](xref:@ActiproUIRoot.Controls.UserPromptStandardImage.Warning) corresponds to the key [SharedImageSourceKeys](xref:@ActiproUIRoot.Media.SharedImageSourceKeys).[Warning](xref:@ActiproUIRoot.Media.SharedImageSourceKeys.Warning). A custom class which derives from [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) can override the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[GetImageSource](xref:@ActiproUIRoot.Media.ImageProvider.GetImageSource*) method to return a custom `ImageSource` for one or more of those keys.
}

## Customize UserPromptWindow

The [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) does not have a public constructor and is only created by the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow).[ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) method.  One of the arguments for [ShowDialog](xref:@ActiproUIRoot.Controls.UserPromptWindow.ShowDialog*) allows you to pass an `Action<UserPromptWindow>` which is invoked with a reference to the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) after it is configured, but before it is shown. This callback allows the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) to be customized.

The following code demonstrates using the callback to customize the flow direction of the window:

@if (avalonia) {
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
		window.FlowDirection = FlowDirection.RightToLeft;
	});
```
}

The [builder pattern](builder-pattern.md) exposes the same customization using the [AfterInitializeWindow](xref:@ActiproUIRoot.Controls.UserPromptBuilder.AfterInitializeWindow*) callback as shown in the following example:

@if (avalonia) {
```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterInitializeWindow(window => {
		window.FlowDirection = FlowDirection.RightToLeft;
	})
	.Show();
```
}
@if (wpf) {
```csharp
UserPromptBuilder.Configure()
	// ... other configuration options here
	.AfterInitializeWindow(window => {
		window.FlowDirection = FlowDirection.RightToLeft;
	})
	.Show();
```
}

@if (avalonia) {
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
}
@if (wpf) {
Finally, an advanced configuration of [ThemedMessageBox](message-box.md) also allows access to the [builder pattern](builder-pattern.md) as shown below:

```csharp
ThemedMessageBox.Show(
	"Use the optional 'configure' parameter to access the UserPromptBuilder."
	configure: builder => builder
		.AfterInitializeWindow(window => {
			window.FlowDirection = FlowDirection.RightToLeft;
		})
	);
```
}

@if (avalonia) {
## Dialog Chromed Decorations

When displayed as a dialog, parts of the [UserPromptWindow](xref:@ActiproUIRoot.Controls.UserPromptWindow) can be styled with custom chromed elements.  The following options are available:

| Value | Description |
| ----- | ----- |
| [None](xref:@ActiproUIRoot.Controls.ChromedDecorations.None) | No custom chrome is applied, and the window will appear like a native system window. |
| [TitleBarOnly](xref:@ActiproUIRoot.Controls.ChromedDecorations.TitleBarOnly) | Custom chrome is applied only to the title bar. |
| [TitleBarOnlyPreferred](xref:@ActiproUIRoot.Controls.ChromedDecorations.TitleBarOnlyPreferred) | (Default) Uses [TitleBarOnly](xref:@ActiproUIRoot.Controls.ChromedDecorations.TitleBarOnly) on supported platforms; otherwise, [Full](xref:@ActiproUIRoot.Controls.ChromedDecorations.Full) will be used. |
| [Full](xref:@ActiproUIRoot.Controls.ChromedDecorations.Full) | No system decorations are used, and the entire window (title bar and borders) will use a custom appearance. |

> [!IMPORTANT]
> Avalonia on Linux either requires all system decorations or none.  It is not possible to hide just the title bar and still keep the borders, so [ChromedDecorations](xref:@ActiproUIRoot.Controls.ChromedDecorations).[TitleBarOnly](xref:@ActiproUIRoot.Controls.ChromedDecorations.TitleBarOnly) is not supported on Linux.

The change the global default value for [ChromedDecorations](xref:@ActiproUIRoot.Controls.ChromedDecorations), set [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[DefaultDialogChromedDecorations](xref:@ActiproUIRoot.Controls.UserPromptBuilder.DefaultDialogChromedDecorations) to the desired value.

```csharp
UserPromptBuilder.DefaultDialogChromedDecorations = ChromedDecorations.Full;
```

The [builder pattern](builder-pattern.md) exposes a per-prompt option using the [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder).[WithDialogChromedDecorations](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithDialogChromedDecorations*) method as shown in the following example:

```csharp
await UserPromptBuilder.Configure()
	// ... other configuration options here
	.WithDialogChromedDecorations(ChromedDecorations.None)
	.Show();
```

An advanced configuration of [MessageBox](message-box.md) also allows access to the [builder pattern](builder-pattern.md) as shown below:

```csharp
await MessageBox.Show(
	"Use the optional 'configure' parameter to access the UserPromptBuilder."
	configure: builder => builder
		.WithDialogChromedDecorations(ChromedDecorations.None)
	);
```
}

@if (wpf) {
## Theme Assets

See the [Theme Reusable Assets](../../../themes/reusable-assets.md) topic for more details on using and customizing theme assets.  The following reusable assets are used by [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl):

| Asset Resource Key | Description |
|-----|-----|
| [ContainerForegroundLowestNormalBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowestNormalBrushKey) | Assigned to the following properties: `Foreground`. |
| [ContainerBackgroundLowestBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBackgroundLowestBrushKey) | Assigned to the following properties: `Background`. |
| [ContainerForegroundLowNormalBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowNormalBrushKey) | Assigned to the following properties: [TrayForeground](xref:@ActiproUIRoot.Controls.UserPromptControl.TrayForeground). |
| [ContainerBackgroundLowBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBackgroundLowBrushKey) | Assigned to the following properties: [TrayBackground](xref:@ActiproUIRoot.Controls.UserPromptControl.TrayBackground). |
| [ContainerBorderLowBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBorderLowBrushKey) | Assigned to the following properties: `BorderBrush`. |
| [PrimaryAccentForegroundLowestNormalBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.PrimaryAccentForegroundLowestNormalBrushKey) | Assigned to the following properties: [HeaderForeground](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderForeground). |
| [ExtraLargeFontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ExtraLargeFontSizeDoubleKey) | Assigned to the following properties: [HeaderFontSize](xref:@ActiproUIRoot.Controls.UserPromptControl.HeaderFontSize). |
| [ContainerForegroundLowDisabledBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowDisabledBrushKey) | Used for the focus rectangle of the **Expanded Information** toggle. |
| [ButtonForegroundHoverBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ButtonForegroundHoverBrushKey) | Assigned to the following properties of the **Expanded Information** toggle when the mouse is over the control: `Foreground`. |
| [ButtonForegroundPressedBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ButtonForegroundPressedBrushKey) | Assigned to the following properties of the **Expanded Information** toggle when pressed: `Foreground`. |
| [ContainerForegroundLowDisabledBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowDisabledBrushKey) | Assigned to the following properties of the **Expanded Information** toggle when disabled: `Foreground`. |
}