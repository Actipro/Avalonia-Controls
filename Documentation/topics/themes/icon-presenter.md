---
title: "Icon Presenter"
page-title: "Icon Presenter - Themes Reference"
order: 95
---
# Icon Presenter

The [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter) class is a `ContentPresenter` specifically for presenting icons based on any `object`-based value.  This class defines a [DefaultContentTemplate](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter.DefaultContentTemplate) that is automatically applied when an explicit `ContentTemplate` is unassigned.

Instead of forcing an icon to be based on an `IImage`, this approach allows any value to define an icon provided an appropriate `IDataTemplate` is also defined for non-visual values.

> [!TIP]
> The default control theme for [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter) will set the [DefaultContentTemplate](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter.DefaultContentTemplate) to an instance of [ImageDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.ImageDataTemplateSelector) that is predefined to support `IImage` and `GeometryStream` value types in additional to custom logic for `string` values.

## ImageDataTemplateSelector

The [ImageDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.ImageDataTemplateSelector) class is a special `IDataTemplate` that allows multiple child `IDataTemplate` definitions to be defined for each supported value `Type`.  For a given value, the matching `IDataTemplate` will be determined based on the `Type` of that value, each inherited `Type`, or the `Type` of each implemented interface.  If a match is not available, a [DefaultTemplate](xref:@ActiproUIRoot.Controls.Templates.TypedDataTemplateSelector.DefaultTemplate) can also be defined.

The following example demonstrates how to define an `IDataTemplate` that works for `IImage` and `StreamGeometry` value types:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<actipro:ImageDataTemplateSelector>

	<DataTemplate x:DataType="IImage">
		<actipro:DynamicImage Source="{Binding}" />
	</DataTemplate>

	<DataTemplate x:DataType="StreamGeometry">
		<PathIcon Data="{Binding}" />
	</DataTemplate>

</actipro:ImageDataTemplateSelector>
```

Since `IImage` and `StreamGeometry` are common value types, the `IDataTemplate` entries defined above are available as static resources that can easily be reused.  The following demonstrates how to update the above sample to resuse the default templates:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<actipro:ImageDataTemplateSelector>

	<StaticResource ResourceKey="{x:Static actipro:ImageDataTemplateSelector.DefaultIImageDataTemplateKey}" />
	<StaticResource ResourceKey="{x:Static actipro:ImageDataTemplateSelector.DefaultStreamGeometryDataTemplateKey}" />

</actipro:ImageDataTemplateSelector>
```

> [!TIP]
> It is highly recommended to resuse the existing `IDataTemplate` resources so that any future changes to the default templates will be automatically included in any custom implementations of [ImageDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.ImageDataTemplateSelector).

### Integration with Image Provider

When the [ImageDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.ImageDataTemplateSelector) attempts to select an `IDataTemplate` for a `string` value, it will first attempt to resolve an image from the default [Image Provider](image-provider.md) (as defined by [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default)) by calling the [GetImageSource](xref:@ActiproUIRoot.Media.ImageProvider.GetImageSource*) method overload that accepts a `string` value as a `key`.  If a value is returned by the [Image Provider](image-provider.md), the `IDataTemplate` will be matched based on the `Type` of the returned value.  If a value is not returned, a match will be attempted on the `string` data type.

> [!IMPORTANT]
> [Image Provider](image-provider.md) can support loading relative and absolute resources using a `string` formatted as a path (e.g., `"avares://MyAssembly/Images/Icons/MyIcon.png"` or `/Images/Icons/MyIcon.png`).  To support relative paths, a base `Uri` must be assigned to [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[RelativePathBaseUri](xref:@ActiproUIRoot.Media.ImageProvider.RelativePathBaseUri) or an exception will be thrown. Since only one base `Uri` can be defined, relatives paths can only be used with a single assembly.

## Adding a Custom Icon Data Type

Adding global support for a custom icon data type is as simple as replacing the [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter).[DefaultContentTemplate](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter.DefaultContentTemplate) property with one that defines an `IDataTemplate` for the custom type.

The following example defines an implicit `Style` for [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter) that updates the [DefaultContentTemplate](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter.DefaultContentTemplate) property with one that supports a custom value type.  In this example, `MyIconKind` is expected to be an `enum` value type that identifies an icon, and `MyIconControl` is a `Control` that knows how to render the appropriate icon for the `enum` value.

```xaml
<Application ...
	xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
	xmlns:controlsPrimitives="using:ActiproSoftware.UI.Avalonia.Controls.Primitives"
	xmlns:local="using:MyApplication.Namespace"
	>

	...

	<Application.Styles>

		<!-- Configure custom icon support -->
		<Style Selector="controlsPrimitives|IconPresenter">
			<Setter Property="DefaultContentTemplate">
				<actipro:ImageDataTemplateSelector>

					<!-- Reuse the pre-defined data templates -->
					<StaticResource ResourceKey="{x:Static actipro:ImageDataTemplateSelector.DefaultIImageDataTemplateKey}" />
					<StaticResource ResourceKey="{x:Static actipro:ImageDataTemplateSelector.DefaultStreamGeometryDataTemplateKey}" />

					<!-- Add a custom data template -->
					<DataTemplate x:DataType="local:MyIconKind">
						<local:MyIconControl Kind="{Binding}" />
					</DataTemplate>

				</actipro:ImageDataTemplateSelector>
			</Setter>
		</Style>

	</Application.Styles>

</Application>
```

## Using Icon Presenter

The [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter) is a primitive that derives from `ContentPresenter` and should only be directly used within a `ControlTheme`, but options are available to easily take advantage of [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter) without using it directly.

### ContentControl Theme

Use a `ContentControl` with the `theme-icon-presenter` class style name and a default template will be applied that uses [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter) instead of `ContentPresenter`.

The following example demonstrates how to use a `ContentControl` whose content is bound to a value type supported by [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter):

```xaml
<ContentControl Classes="theme-icon-presenter" Content="{Binding Icon}" />
```

### IconControlConverter

The [IconControlConverter](xref:@ActiproUIRoot.Controls.Converters.IconControlConverter) is a value converter that can be used with bindings to effectively wrap the bound value as the content of a `ContentControl` that will use [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter) to present the value.

The following example demonstrates how to use [IconControlConverter](xref:@ActiproUIRoot.Controls.Converters.IconControlConverter) with the `MenuItem.Icon` property that expects a control as the value:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<UserControl.Resources>
	<actipro:IconControlConverter x:Key="IconControlConverter" />
</UserControl.Resources>
...

<MenuItem Icon="{Binding Icon, Converter={StaticResource IconControlConverter}}" ... />
```