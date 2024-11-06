---
title: "Settings Expander"
page-title: "Settings Expander - Fundamentals Controls"
order: 21
---
# SettingsExpander

The [SettingsCard](settings-card.md), [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander), and [SettingsGroup](settings-group.md) controls are used together to organize and present configurable settings.

![Screenshot](../images/settings-examples.png)

*SettingsCard and SettingsExpander displayed within a SettingsGroup*

A [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) has functionality similar to a [SettingsCard](settings-card.md) but can also be expanded to show additional child settings.

![Screenshot](../images/settings-expander.png)

*SettingsExpander with header, description, header icon, and ToggleSwitch content in the expanded state with two SettingsCard children*

## Primary Content Areas

The [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) control is defined by multiple content areas:

- [Header](xref:@ActiproUIRoot.Controls.SettingsExpander.Header) - The primary label for the setting.
- [Description](xref:@ActiproUIRoot.Controls.SettingsExpander.Description) - An additional description for the setting.
- [HeaderIcon](xref:@ActiproUIRoot.Controls.SettingsExpander.HeaderIcon) - The primary icon for the setting.
- `Content` (Editor) - The control used to edit the setting (e.g., `ToggleSwitch`, `ComboBox`).

Each content area, including the icon, can optionally be set to any value supported by `ContentPresenter` and the layout will adjust to only show the areas where content is defined.

> [!NOTE]
> In some scenarios, content may not be automatically detected. For instance, if a `DataTemplate` is used to define content without setting the corresponding content property, the control will not know that content is available.  Use the [IsHeaderVisible](xref:@ActiproUIRoot.Controls.SettingsExpander.IsHeaderVisible), [IsDescriptionVisible](xref:@ActiproUIRoot.Controls.SettingsExpander.IsDescriptionVisible), and [IsHeaderIconVisible](xref:@ActiproUIRoot.Controls.SettingsExpander.IsHeaderIconVisible) properties to manually control the visibility of each content area.

> [!TIP]
> The [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) control has all the same content areas as [SettingsCard](settings-card.md) *except* [ActionIcon](xref:@ActiproUIRoot.Controls.SettingsCard.ActionIcon) since the expansion indicator is displayed in this area.

### Header and Description

The [Header](xref:@ActiproUIRoot.Controls.SettingsExpander.Header) and [Description](xref:@ActiproUIRoot.Controls.SettingsExpander.Description) are typically `string` values describing the setting, and both are optional.  The following demonstrates how to create a [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) with a header and description:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:SettingsExpander Header="Setting name" Description="Use the description for additional context" ... />
```
}
@if (wpf) {
```xaml
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
...
<views:SettingsExpander Header="Setting name" Description="Use the description for additional context" ... />
```
}

Since both properties can be set to any content supported by `ContentPresenter`, either property can be configured with more complex content.  The following example demonstrates how a hyperlink could be used as the [Description](xref:@ActiproUIRoot.Controls.SettingsExpander.Description) @if (avalonia) { (using the [HyperlinkTextBlock](../../shared/controls/hyperlink-textblock.md) control) }:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:SettingsExpander Header="Setting name">

	<actipro:SettingsExpander.Description>
		<actipro:HyperlinkTextBlock Text="Click here for more"
			Command="{Binding SomeCommand}"
			FontSize="{actipro:ThemeResource DefaultFontSizeSmall}"
			FontWeight="DemiBold" />
	</actipro:SettingsExpander.Description>

	...

</actipro:SettingsExpander>
```
}
@if (wpf) {
```xaml
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
xmlns:themes="http://schemas.actiprosoftware.com/winfx/xaml/themes"
...
<views:SettingsExpander Header="Setting name">

	<views:SettingsExpander.Description>
		<TextBlock>
			<Hyperlink
				FontSize="{DynamicResource {x:Static themes:AssetResourceKeys.SmallFontSizeDoubleKey}}"
				FontWeight="DemiBold"
				Command="{Binding SomeCommand}"
				TextDecorations="None">
				Click here for more
			</Hyperlink>
		</TextBlock>
	</views:SettingsExpander.Description>

	...

</views:SettingsExpander>
```
}

### Header Icon

The [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) supports a [HeaderIcon](xref:@ActiproUIRoot.Controls.SettingsExpander.HeaderIcon).  This icon is displayed on the left side of the card with a default size of `24x24`.  This icon is typically related to the value(s) defined by the setting.  For example, a speaker icon might be used for a setting related to output sound volume.

@if (avalonia) {
The following sample demonstrates using a `PathIcon` for defining the icon, but any content supported by [Icon Presenter](../../themes/icon-presenter.md) can be used to define the icon (like `IImage` data or [DynamicImage](../../shared/controls/dynamic-image.md) control):

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:SettingsExpander Header="Setting name">

	<!-- Use a PathIcon as the primary icon -->
	<actipro:SettingsExpander.HeaderIcon>
		<PathIcon Data="M19,13H5V11H19V13Z" />
	</actipro:SettingsExpander.HeaderIcon>

	...

</actipro:SettingsExpander>
```
}
@if (wpf) {
The following sample demonstrates using a `Path` for defining the icon, but any content supported by `ContentPresenter` can be used to define the icon (like `Image` or [DynamicImage](../../shared/windows-controls/dynamicimage.md) controls):

```xaml
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
...
<views:SettingsExpander Header="Setting name">

	<!-- Use a Path as the primary icon -->
	<actipro:SettingsExpander.HeaderIcon>
		<Viewbox Height="24" Width="24">
			<Path Data="M19,13H5V11H19V13Z" Fill="{Binding Path=(TextElement.Foreground), RelativeSource={RelativeSource Mode=Self}}" />
		</Viewbox>
	</actipro:SettingsExpander.HeaderIcon>

	...

</views:SettingsExpander>
```
}

### Content (Editor)

In additional to organizing child settings, a [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) may also use the `Content` property to present a control for an individual setting.  Any content supported by `ContentPresenter` can be used, but a setting is typically defined by common controls like `CheckBox`, `ComboBox`, `Slider`, and `ToggleSwitch`.

> [!IMPORTANT]
> Unlike [SettingsCard](settings-card.md), the default property for [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) is the `Items` collection and *not* the `Content`. Make sure the `Content` property is explicitly used when defining the control.

The following demonstrates defining a [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) that uses a `ToggleSwitch` control as the `Content`:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:SettingsExpander Header="Setting name" ... >

	<actipro:SettingsExpander.Content>
		<ToggleSwitch actipro:ThemeProperties.ToggleSwitchHasFarAffinity="True" IsChecked="{Binding SomeProperty}" />
	</actipro:SettingsExpander.Content>

	<!-- Define child settings here -->

</actipro:SettingsExpander>
```
}
@if (wpf) {
```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
...
<views:SettingsExpanderHeader="Setting name" ... >

	<views:SettingsExpander.Content>
		<shared:ToggleSwitch IsChecked="{Binding SomeProperty}" />
	</views:SettingsExpander.Content>

	<!-- Define child settings here -->

</views:SettingsExpander>
```
}

See the [SettingsCard](settings-card.md) topic for more examples on using different controls for settings.

## Items (Child Settings)

The [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) is an `ItemsControl` that supports defining one or more [SettingsCard](settings-card.md) instances as child settings that are only displayed when the control is expanded.

The following sample demonstrates defining a [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) with multiple child settings and sets the [IsExpanded](xref:@ActiproUIRoot.Controls.SettingsExpander.IsExpanded) property to `true` so the child settings are visible by default:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:SettingsExpander Header="Setting name" IsExpanded="True" ... >

	<actipro:SettingsCard Header="Child setting">
		<ComboBox> ... </ComboBox>
	</actipro:SettingsCard>

	<actipro:SettingsCard Header="Child setting">
		<ToggleSwitch actipro:ThemeProperties.ToggleSwitchHasFarAffinity="True" />
	</actipro:SettingsCard>

</actipro:SettingsExpander>
```
}
@if (wpf) {
```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
...
<views:SettingsExpanderHeader="Setting name" IsExpanded="True" ... >

	<views:SettingsCard Header="Child setting">
		<ComboBox> ... </ComboBox>
	</views:SettingsCard>

	<views:SettingsCard Header="Child setting">
		<shared:ToggleSwitch />
	</views:SettingsCard>

</views:SettingsExpander>
```
}

See the [SettingsCard](settings-card.md) topic for more details.

### Indentation

By default, child settings of a [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) are indented by setting both [IsHeaderIconVisible](xref:@ActiproUIRoot.Controls.SettingsCard.IsHeaderIconVisible) and [IsActionIconVisible](xref:@ActiproUIRoot.Controls.SettingsCard.IsActionIconVisible) to `true`.  This reserves space in the layout for the [HeaderIcon](xref:@ActiproUIRoot.Controls.SettingsCard.HeaderIcon) and [ActionIcon](xref:@ActiproUIRoot.Controls.SettingsCard.ActionIcon) even if those icons are not defined.

To change this behavior, modify the @if (avalonia) { `ItemContainerTheme` }@if (wpf) { `ItemContainerStyle` } property as desired, like demonstrated in the following sample:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:SettingsExpander Header="Setting name" ... >

	<actipro:SettingsExpander.ItemContainerTheme>
		<ControlTheme TargetType="actipro:SettingsCard" BasedOn="{actipro:ControlTheme SettingsCardSettingsExpanderItem}">
			<Setter Property="IsHeaderIconVisible" Value="{x:Null}" />
			<Setter Property="IsActionIconVisible" Value="{x:Null}" />
		</ControlTheme>
	</actipro:SettingsExpander.ItemContainerTheme>

	<!-- Define child settings here -->

</actipro:SettingsExpander>
```
}
@if (wpf) {
```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
xmlns:themes="http://schemas.actiprosoftware.com/winfx/xaml/themes"
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
...
<views:SettingsExpander Header="Setting name" ... >

	<views:SettingsExpander.ItemContainerStyle>
		<Style TargetType="views:SettingsCard" BasedOn="{StaticResource {x:Static themes:ViewsResourceKeys.SettingsCardSettingsExpanderItemStyleKey}}">
			<Setter Property="IsHeaderIconVisible" Value="{x:Null}" />
			<Setter Property="IsActionIconVisible" Value="{x:Null}" />
		</Style>
	</views:SettingsExpander.ItemContainerStyle>

	<!-- Define child settings here -->

</views:SettingsExpander>
```
}

### Working with MVVM

All children of [SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) must be instances of [SettingsCard](settings-card.md). When binding `ItemsSource` to a collection of view models, each view model will automatically be wrapped in a [SettingsCard](settings-card.md) container with the `DataContext` set to the view model.

@if (avalonia) {
Properties on the view model can be bound to properties on the [SettingsCard](settings-card.md) by assigning an `ItemContainerTheme` like shown in the following example:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:SettingsExpander Header="Setting name" ... >

	<actipro:SettingsExpander.ItemContainerTheme>
		<ControlTheme
			TargetType="actipro:SettingsCard"
			BasedOn="{actipro:ControlTheme SettingsCardSettingsExpanderItem}"
			x:DataType="custom:MyViewModelType">

			<!-- Bind to view model properties -->
			<Setter Property="Header" Value="{Binding Header}" />
			<Setter Property="Description" Value="{Binding Description}" />

		</ControlTheme>
	</actipro:SettingsExpander.ItemContainerTheme>

</actipro:SettingsExpander>
```
}
@if (wpf) {
Properties on the view model can be bound to properties on the [SettingsCard](settings-card.md) by assigning an `ItemContainerStyle` like shown in the following example:

```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
xmlns:themes="http://schemas.actiprosoftware.com/winfx/xaml/themes"
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
...
<views:SettingsExpander Header="Setting name" ... >

	<views:SettingsExpander.ItemContainerStyle>
		<Style TargetType="views:SettingsCard" BasedOn="{StaticResource {x:Static themes:ViewsResourceKeys.SettingsCardSettingsExpanderItemStyleKey}}">

			<!-- Bind to view model properties -->
			<Setter Property="Header" Value="{Binding Header}" />
			<Setter Property="Description" Value="{Binding Description}" />

		</Style>
	</views:SettingsExpander.ItemContainerStyle>

	<!-- Define child settings here -->

</views:SettingsExpander>
```
}

## Items Header and Footer

![Screenshot](../images/settings-expander-header-footer.png)

*SettingsExpander showing ItemsHeader and ItemsFooter*

[SettingsExpander](xref:@ActiproUIRoot.Controls.SettingsExpander) allows for additional content to be displayed above and below the child settings.  Any content supported by `ContentPresenter` can be defined in the [ItemsHeader](xref:@ActiproUIRoot.Controls.SettingsExpander.ItemsHeader) or [ItemsFooter](xref:@ActiproUIRoot.Controls.SettingsExpander.ItemsFooter).

The following sample demonstrates adding an informational message in the [ItemsHeader](xref:@ActiproUIRoot.Controls.SettingsExpander.ItemsHeader):

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:SettingsExpander Header="Setting name" ... >

	<actipro:SettingsExpander.ItemsHeader>
		<TextBlock Padding="12">Any content can be displayed above the child settings</TextBlock>
	</actipro:SettingsExpander.ItemsHeader>

	<!-- Define child settings here -->

</actipro:SettingsExpander>
```
}
@if (wpf) {
```xaml
xmlns:views="http://schemas.actiprosoftware.com/winfx/xaml/views"
...
<views:SettingsExpanderHeader="Setting name" ... >

	<views:SettingsExpander.ItemsHeader>
		<TextBlock Padding="12">Any content can be displayed above the child settings</TextBlock>
	</views:SettingsExpander.ItemsHeader>

	<!-- Define child settings here -->

</views:SettingsExpander>
```
}

> [!NOTE]
> In some scenarios, content may not be automatically detected. For instance, if a `DataTemplate` is used to define content without setting the corresponding content property, the control will not know that content is available.  Use the [IsItemsHeaderVisible](xref:@ActiproUIRoot.Controls.SettingsExpander.IsItemsHeaderVisible) and [IsItemsFooterVisible](xref:@ActiproUIRoot.Controls.SettingsExpander.IsItemsHeaderVisible) properties to manually control the visibility of each content area.

## Wrapping

![Screenshot](../images/settings-expander-wrapping.png)

*SettingsExpander displayed in the unwrapped and wrapped states*

If enough space is available, the `Content` (Editor) of the setting is displayed to the right of the [Header](xref:@ActiproUIRoot.Controls.SettingsExpander.Header) and/or [Description](xref:@ActiproUIRoot.Controls.SettingsExpander.Description) with default right alignment.  When the width of the expander is less than or equal to the [WrapThreshold](xref:@ActiproUIRoot.Controls.SettingsExpander.WrapThreshold), the `Content` will be wrapped to the bottom of the expander with default left alignment.

Use the [IsWrapped](xref:@ActiproUIRoot.Controls.SettingsExpander.IsWrapped) property to manually control wrap behavior.  When set to `null` (the default), wrapping is based on the [WrapThreshold](xref:@ActiproUIRoot.Controls.SettingsExpander.WrapThreshold). Set the property to `true` to force wrapping at any width, and `false` to prevent wrapping at any width.

> [!NOTE]
> Wrapping is only applicable if [Header](xref:@ActiproUIRoot.Controls.SettingsExpander.Header) and/or [Description](xref:@ActiproUIRoot.Controls.SettingsExpander.Description) are defined. Otherwise, the `Content` (Editor) will always be aligned left, by default.

## Animation

Fluent animation in the control is enabled by default but can be disabled by setting the [IsAnimationEnabled](xref:@ActiproUIRoot.Controls.SettingsExpander.IsAnimationEnabled) property to `false`.

@if (avalonia) {

## Pseudo-classes

The following pseudo-classes are available and can be used when styling the control:

| Class | Description |
| ----- | ----- |
| `:expanded` | Added when the control is expanded. |
| `:wrapped` | Added when the `Content` (Editor) has wrapped due to the available width being less than or equal to the [WrapThreshold](xref:@ActiproUIRoot.Controls.SettingsExpander.WrapThreshold) or when [IsWrapped](xref:@ActiproUIRoot.Controls.SettingsExpander.IsWrapped) is set to `true`. |
}

@if (avalonia) {
## Theme Resources

The following theme resources are available for customizing the appearance of the control:

| Theme Resource | Description |
| ----- | ----- |
| [Container1BackgroundBrush](xref:@ActiproUIRoot.Themes.ThemeResourceKind.Container1BackgroundBrush) | The default `Background`. |
| [Container2BackgroundBrush](xref:@ActiproUIRoot.Themes.ThemeResourceKind.Container1BackgroundBrush) | The default `Background` of a clickable card when the mouse is over the control. |
| [Container3BackgroundBrush](xref:@ActiproUIRoot.Themes.ThemeResourceKind.Container1BackgroundBrush) | The default `Background` of a clickable card when the control is pressed. |
| [Container1BorderBrush](xref:@ActiproUIRoot.Themes.ThemeResourceKind.Container1BorderBrush) | The default `BorderBrush`. |
| [Container2BorderBrush](xref:@ActiproUIRoot.Themes.ThemeResourceKind.Container1BorderBrush) | The default `BorderBrush` of a clickable card when the mouse is over the control. |
| [Container3BorderBrush](xref:@ActiproUIRoot.Themes.ThemeResourceKind.Container1BorderBrush) | The default `BorderBrush` of a clickable card when the control is pressed. |
| [SettingsCardBorderThickness](xref:@ActiproUIRoot.Themes.ThemeResourceKind.SettingsCardBorderThickness) | The default `BorderThickness`. |
| [SettingsCardCornerRadius](xref:@ActiproUIRoot.Themes.ThemeResourceKind.SettingsCardCornerRadius) | The default `CornerRadius`. |
| [DefaultFontSizeExtraSmall](xref:@ActiproUIRoot.Themes.ThemeResourceKind.DefaultFontSizeExtraSmall) | The default `FontSize` of the [Description](xref:@ActiproUIRoot.Controls.SettingsExpander.Description) content.
| [DefaultForegroundBrush](xref:@ActiproUIRoot.Themes.ThemeResourceKind.DefaultForegroundBrush) | The default `Foreground`. |
| [DefaultForegroundBrushTertiary](xref:@ActiproUIRoot.Themes.ThemeResourceKind.DefaultForegroundBrushTertiary) | The default `Foreground` of the [Description](xref:@ActiproUIRoot.Controls.SettingsExpander.Description) content. |
| [DefaultForegroundBrushDisabled](xref:@ActiproUIRoot.Themes.ThemeResourceKind.DefaultForegroundBrushDisabled) | The default `Foreground` when the control is disabled. |
| [SettingsCardPadding](xref:@ActiproUIRoot.Themes.ThemeResourceKind.SettingsCardPadding) | The default `Padding`. |
| [SettingsCardHeaderIconLength](xref:@ActiproUIRoot.Themes.ThemeResourceKind.SettingsCardHeaderIconLength) | The default `Width` and `Height` of the [HeaderIcon](xref:@ActiproUIRoot.Controls.SettingsExpander.HeaderIcon). |
| [SettingsCardWrapThreshold](xref:@ActiproUIRoot.Themes.ThemeResourceKind.SettingsCardWrapThreshold) | The default [WrapThreshold](xref:@ActiproUIRoot.Controls.SettingsExpander.WrapThreshold). |

See the [Theme Assets](../../themes/theme-assets.md) topic for more details on working with theme resources.
}
@if (wpf) {
## Theme Assets

See the [Theme Reusable Assets](../../themes/reusable-assets.md) topic for more details on using and customizing theme assets.  The following reusable assets are used by [SettingsExpander](xref:@ActiproUIRoot.Controls.Views.SettingsExpander):

| Asset Resource Key | Description |
|-----|-----|
| [ContainerBackgroundLowestBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBackgroundLowestBrushKey) | The default `Background`. |
| [ContainerBackgroundLowerBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBackgroundLowerBrushKey) | The default `Background` when the mouse is over the control. |
| [ContainerBackgroundLowBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBackgroundLowBrushKey) | The default `Background` when the control is pressed. |
| [ContainerBorderLowerBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBorderLowerBrushKey) | The default `BorderBrush`. |
| [ContainerBorderLowBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBorderLowBrushKey) | The default `BorderBrush` of a clickable card when the mouse is over the control. |
| [ContainerBorderMidLowBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerBorderMidLowBrushKey) | The default `BorderBrush` of a clickable card when the control is pressed. |
| [SettingsCardBorderNormalThicknessKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.SettingsCardBorderNormalThicknessKey) | The default `BorderThickness`. |
| [SettingsCardBorderNormalCornerRadiusKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.SettingsCardBorderNormalCornerRadiusKey) | The default [CornerRadius](xref:@ActiproUIRoot.Controls.Views.SettingsExpander.CornerRadius).
| [SmallFontSizeDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.SmallFontSizeDoubleKey) | The default `FontSize` of the [Description](xref:@ActiproUIRoot.Controls.Views.SettingsExpander.Description) content.
| [ContainerForegroundLowestNormalBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowestNormalBrushKey) | The default `Foreground`. |
| [ContainerForegroundLowestSubtleBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowestSubtleBrushKey) | The default `Foreground` of the [Description](xref:@ActiproUIRoot.Controls.Views.SettingsExpander.Description) content. |
| [ContainerForegroundLowestDisabledBrushKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.ContainerForegroundLowestDisabledBrushKey) | The default `Foreground` when the control is disabled. |
| [SettingsCardPaddingNormalThicknessKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.SettingsCardPaddingNormalThicknessKey) | The default `Padding`.
| [SettingsCardHeaderIconLengthDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.SettingsCardHeaderIconLengthDoubleKey) | The default `Width` and `Height` of the [HeaderIcon](xref:@ActiproUIRoot.Controls.Views.SettingsExpander.HeaderIcon).
| [SettingsCardWrapThresholdDoubleKey](xref:@ActiproUIRoot.Themes.AssetResourceKeys.SettingsCardWrapThresholdDoubleKey) | The default [WrapThreshold](xref:@ActiproUIRoot.Controls.Views.SettingsExpander.WrapThreshold).
}