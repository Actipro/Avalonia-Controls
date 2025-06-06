---
title: "MVVM Features"
page-title: "MVVM Features - Docking & MDI Reference"
order: 10
---
# MVVM Features

The Docking & MDI product has been designed to support the popular MVVM pattern.  The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) control's item container generation features and infrastructure are similar to a standard `ItemsControl`, but geared towards the generation of docking windows.

> [!TIP]
> Many of the concepts below are implemented in the MVVM-related samples in the Sample Browser, so be sure to examine those samples.

## Specifying Document and Tool Items

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) includes several properties and methods that mimic the design of the native `ItemsControl`.  Using these properties and methods, the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) can be bound to a collection of view-models for documents and/or tool windows.

### Document Windows

Document windows can be defined by binding the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemsSource) property to a collection of items, such as view-models.  The items will be wrapped in an instance of [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow), which is the container used to present the item.

@if (avalonia) {
A `ControlTheme` can be applied to the generated [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) instances by using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemContainerTheme](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerTheme) property. Alternatively, an implicit `ControlTheme` for the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) type can be defined and placed in the `Application.Resources`.  The `ControlTheme` can be used to bind various properties of the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) to the item.

The `IDataTemplate` used by the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) instances to present the associated item can be specified using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemTemplate](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemTemplate) property. Alternatively, an implicit `IDataTemplate` for the item's type can be defined and placed in the `Application.Resources`.
}
@if (wpf) {
A `Style` can be applied to the generated [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) instances by using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemContainerStyle](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerStyle) or [DocumentItemContainerStyleSelector](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerStyleSelector) properties. Alternatively, an implicit `Style` for the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) type can be defined and placed in the `Application.Resources`.  The `Style` can be used to bind various properties of the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) to the item.

The `DataTemplate` used by the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) instances to present the associated item can be specified using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemTemplate](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemTemplate) or [DocumentItemTemplateSelector](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemTemplateSelector) properties. Alternatively, an implicit `DataTemplate` for the item's type can be defined and placed in the `Application.Resources`.
}

### Tool Windows

Tool windows can be defined by binding the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemsSource) property to a collection of items, such as view-models.  The items will be wrapped in an instance of [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow), which is the container used to present the item.

@if (avalonia) {
A `ControlTheme` can be applied to the generated [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) instances by using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolItemContainerTheme](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerTheme) property. Alternatively, an implicit `ControlTheme` for the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) type can be defined and placed in the `Application.Resources`.  The `ControlTheme` can be used to bind various properties of the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) to the item.

The `IDataTemplate` used by the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) instances to present the associated item can be specified using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolItemTemplate](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemTemplate) property. Alternatively, an implicit `IDataTemplate` for the item's type can be defined and placed in the `Application.Resources`.
}
@if (wpf) {
A `Style` can be applied to the generated [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) instances by using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolItemContainerStyle](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerStyle) or [ToolItemContainerStyleSelector](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerStyleSelector) properties. Alternatively, an implicit `Style` for the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) type can be defined and placed in the `Application.Resources`.  The `Style` can be used to bind various properties of the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) to the item.

The `DataTemplate` used by the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) instances to present the associated item can be specified using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolItemTemplate](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemTemplate) or [ToolItemTemplateSelector](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemTemplateSelector) properties. Alternatively, an implicit `DataTemplate` for the item's type can be defined and placed in the `Application.Resources`.
}

### Container Customization

@if (avalonia) {
The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) includes several virtual methods that can be overridden to customize the containers created based on the bound items. These methods mimic the native `ItemsControl` and includes [ClearContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.ClearContainerForItemOverride*), [CreateContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.CreateContainerForItemOverride*), [NeedsContainerOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.NeedsContainerOverride*), and [PrepareContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrepareContainerForItemOverride*).  Each of these methods works like its `ItemsControl` counterpart, with one exception. Each method includes an additional parameter that specifies whether the container is for a document or tool item.

The [PrepareContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrepareContainerForItemOverride*) method override could be used to bind your view model's properties to the containing docking window's related properties.  The [ClearContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.ClearContainerForItemOverride*) method override would clear those bindings. You don't need to override these methods if you are setting up your bindings in a `ControlTheme` instead that gets used by [DocumentItemContainerTheme](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerTheme), [ToolItemContainerTheme](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerTheme), etc.
}
@if (wpf) {
The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) includes several virtual methods that can be overridden to customize the containers created based on the bound items. These methods mimic the native `ItemsControl` and includes [ClearContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.ClearContainerForItemOverride*), [GetContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.GetContainerForItemOverride*), [IsItemItsOwnContainerOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.IsItemItsOwnContainerOverride*), and [PrepareContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrepareContainerForItemOverride*).  Each of these methods works like its `ItemsControl` counterpart, with one exception. Each method includes an additional parameter that specifies whether the container is for a document or tool item.

The [PrepareContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.PrepareContainerForItemOverride*) method override could be used to bind your view model's properties to the containing docking window's related properties.  The [ClearContainerForItemOverride](xref:@ActiproUIRoot.Controls.Docking.DockSite.ClearContainerForItemOverride*) method override would clear those bindings. You don't need to override these methods if you are setting up your bindings in a `Style` instead that gets used by [DocumentItemContainerStyle](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerStyle), [ToolItemContainerStyle](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerStyle), etc.
}

### Content and Template

As with a standard `ItemsControl` bound to a collection of view model items, each item will be assigned to the `Content` property of the generated container docking window.

@if (avalonia) {
These view model items require that a content template be specified to properly render their data.  As mentioned above, the [DocumentItemTemplate](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemTemplate) and [ToolItemTemplate](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemTemplate) properties can be used to designate the `IDataTemplate` to use for each item.
}
@if (wpf) {
These view model items require that a content template be specified to properly render their data.  As mentioned above, the [DocumentItemTemplate](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemTemplate), [DocumentItemTemplateSelector](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemTemplateSelector), [ToolItemTemplate](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemTemplate), and [ToolItemTemplateSelector](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemTemplateSelector) properties can be used to designate the `DataTemplate` to use for each item.
}

## Styling the Document and Tool Windows

@if (avalonia) {
Custom themes can easily be applied to the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) containers created for the bound items.  The [DocumentItemContainerTheme](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerTheme) or [ToolItemContainerTheme](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerTheme) properties can be used to apply a custom `ControlTheme`. Alternatively, an implicit `ControlTheme` for the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) types can be defined.  The `ControlTheme` can be used to bind various properties of the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) to the item.

These themes pulled from our MVVM samples in the Sample Browser show how you can bind various view model properties to the docking windows.  The resulting `DocumentWindowTheme` and `ToolWindowTheme` resources could then be set to the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemContainerTheme](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerTheme) and [ToolItemContainerTheme](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerTheme) properties respectively:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
xmlns:viewmodels="using:MyApplication.SomeNamespace.ViewModels"
...

<ControlTheme x:Key="DocumentWindowTheme"
	x:DataType="viewmodels:DocumentItemViewModel"
	TargetType="actipro:DocumentWindow"
	BasedOn="{StaticResource {x:Type actipro:DocumentWindow}}">

	<!-- DockingWindow (BaseType) -->
	<Setter Property="Description" Value="{Binding Description, Mode=TwoWay}" />
	<Setter Property="Icon" Value="{Binding Icon, Mode=TwoWay}" />
	<Setter Property="IsFloating" Value="{Binding IsFloating, Mode=TwoWay}" />
	<Setter Property="IsSelected" Value="{Binding IsSelected, Mode=TwoWay}" />
	<Setter Property="SerializationId" Value="{Binding SerializationId, Mode=TwoWay}" />
	<Setter Property="Title" Value="{Binding Title, Mode=TwoWay}" />
	<Setter Property="WindowGroupName" Value="{Binding WindowGroupName, Mode=TwoWay}" />

	<!-- DocumentWindow -->
	<Setter Property="FileName" Value="{Binding FileName, Mode=TwoWay}" />
	<Setter Property="IsReadOnly" Value="{Binding IsReadOnly, Mode=TwoWay}" />

	<!-- IMPORTANT: These properties should be configured last so that other bindings are applied before the window opens -->
	<Setter Property="IsActive" Value="{Binding IsActive, Mode=TwoWay}" />
	<Setter Property="IsOpen" Value="{Binding IsOpen, Mode=TwoWay}" />

</ControlTheme>

<ControlTheme x:Key="ToolWindowTheme"
	x:DataType="viewmodels:ToolItemViewModel"
	TargetType="actipro:ToolWindow"
	BasedOn="{StaticResource {x:Type actipro:ToolWindow}}">

	<!-- DockingWindow (BaseType) -->
	<Setter Property="Description" Value="{Binding Description, Mode=TwoWay}" />
	<Setter Property="Icon" Value="{Binding Icon, Mode=TwoWay}" />
	<Setter Property="IsFloating" Value="{Binding IsFloating, Mode=TwoWay}" />
	<Setter Property="IsSelected" Value="{Binding IsSelected, Mode=TwoWay}" />
	<Setter Property="SerializationId" Value="{Binding SerializationId, Mode=TwoWay}" />
	<Setter Property="Title" Value="{Binding Title, Mode=TwoWay}" />
	<Setter Property="WindowGroupName" Value="{Binding WindowGroupName, Mode=TwoWay}" />

	<!-- ToolWindow -->
	<Setter Property="DefaultDockSide" Value="{Binding DefaultDockSide, Mode=TwoWay}" />
	<Setter Property="State" Value="{Binding State, Mode=TwoWay}" />

	<!-- IMPORTANT: These properties should be configured last so that other bindings are applied before the window opens -->
	<Setter Property="IsActive" Value="{Binding IsActive, Mode=TwoWay}" />
	<Setter Property="IsOpen" Value="{Binding IsOpen, Mode=TwoWay}" />

</ControlTheme>
```

> [!WARNING]
> Properties are updated in the order defined by the theme.  Since setting the [IsActive](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsActive) or [IsOpen](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsOpen) to `true` will open a window, it is important to ensure that all other properties are set first.  Otherwise, important values like [WindowGroupName](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.WindowGroupName), which influence the initial location of a window when opening, may not be properly configured.

}
@if (wpf) {
Custom styles can easily be applied to the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) containers created for the bound items.  The [DocumentItemContainerStyle](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerStyle), [DocumentItemContainerStyleSelector](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerStyleSelector), [ToolItemContainerStyle](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerStyle), or [ToolItemContainerStyleSelector](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerStyleSelector) properties can be used to apply a custom `Style`. Alternatively, an implicit `Style` for the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) types can be defined.  The `Style` can be used to bind various properties of the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) to the item.

These styles pulled from our MVVM samples in the Sample Browser show how you can bind various view model properties to the docking windows.  The resulting `DocumentWindowStyle` and `ToolWindowStyle` properties could then be set to the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemContainerStyle](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemContainerStyle) and [ToolItemContainerStyle](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemContainerStyle) properties respectively:

```xaml
xmlns:common="clr-namespace:ActiproSoftware.ProductSamples.DockingSamples.Common"
xmlns:docking="http://schemas.actiprosoftware.com/winfx/xaml/docking"
...

<common:ToolItemDockSideConverter x:Key="ToolItemDockSideConverter" />
<common:ToolItemStateConverter x:Key="ToolItemStateConverter" />

<Style x:Key="DockingWindowStyle" TargetType="docking:DockingWindow">
	<Setter Property="Description" Value="{Binding Path=Description, Mode=TwoWay}" />
	<Setter Property="ImageSource" Value="{Binding Path=ImageSource, Mode=TwoWay}" />
	<Setter Property="IsActive" Value="{Binding Path=IsActive, Mode=TwoWay}" />
	<Setter Property="IsOpen" Value="{Binding Path=IsOpen, Mode=TwoWay}" />
	<Setter Property="IsSelected" Value="{Binding Path=IsSelected, Mode=TwoWay}" />
	<Setter Property="SerializationId" Value="{Binding Path=SerializationId, Mode=TwoWay}" />
	<Setter Property="Title" Value="{Binding Path=Title, Mode=TwoWay}" />
	<Setter Property="WindowGroupName" Value="{Binding Path=WindowGroupName, Mode=TwoWay}" />
</Style>

<Style x:Key="DocumentWindowStyle" TargetType="docking:DocumentWindow" BasedOn="{StaticResource DockingWindowStyle}">
	<Setter Property="FileName" Value="{Binding Path=FileName, Mode=TwoWay}" />
	<Setter Property="IsReadOnly" Value="{Binding Path=IsReadOnly, Mode=TwoWay}" />
</Style>

<Style x:Key="ToolWindowStyle" TargetType="docking:ToolWindow" BasedOn="{StaticResource DockingWindowStyle}">
	<Setter Property="DefaultDockSide" Value="{Binding Path=DefaultDockSide, Mode=TwoWay, Converter={StaticResource ToolItemDockSideConverter}}" />
	<Setter Property="State" Value="{Binding Path=State, Mode=TwoWay, Converter={StaticResource ToolItemStateConverter}}" />
</Style>
```
}

## Layout Serialization Support

If you plan on [serializing your layout](layout-features/layout-serialization.md) between application execution sessions, you must assign your docking windows a unique `Name` or [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId).  It is recommended that you use the [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) property since, unlike the `Name` property, it can be bound easier and allows any character to be used in the value.

## Opening and Positioning the Docking Windows

The [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) containers created for the bound items will initially be closed and must be opened to appear in the visible layout.

Numerous bindable properties are available on [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) for determining the state and position of docking windows, along with whether they are open and active.  For instance, the [State](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.State) property can be set to designate which state the window should open in.  This property must remain `Document` for any [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow).  To open a docking window and add it to the layout, set its [IsOpen](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsOpen) property to `true`.  To open a docking window (if it isn't already open) and ensure its selected and focused, set its [IsActive](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsActive) property to `true`.  This can be done instead of setting [IsOpen](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsOpen).  Finally, to make sure an already-open docking window's tab is selected, set its [IsSelected](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsSelected) property to `true`.

When a docking window is opened via its [IsOpen](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsOpen) or [IsActive](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsActive) properties, it will open in a default location.  There are numerous properties and events available that allow you to customize this default location prior to opening the docking window.  Some examples are [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).[DefaultDockSide](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.DefaultDockSide), [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[WindowGroupName](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.WindowGroupName), [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowDefaultLocationRequested](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowDefaultLocationRequested), etc.

If you prefer to open, activate, dock, etc. a docking window in code-behind instead, this can be easily accomplished by adding a handler for the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowRegistered](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowRegistered) event.  This event is fired once per [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) and/or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) when it is initially registered with the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).

The following code shows an example of a `WindowRegistered` handler:

```csharp
dockSite.WindowRegistered += this.OnDockSiteWindowRegistered;

// ...

/// <summary>
/// Handles the <c>WindowRegistered</c> event of the <c>DockSite</c> control.
/// </summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">The <see cref="DockingWindowEventArgs"/> instance containing the event data.</param>
private void OnDockSiteWindowRegistered(object sender, DockingWindowEventArgs e) {
	var dockSite = sender as DockSite;
	if (dockSite is null)
		return;

	// Ensure the DockingWindow exists and is generated for an item
	DockingWindow dockingWindow = e.Window;
	if (dockingWindow is null || !dockingWindow.IsContainerForItem)
		return;

	// Open the DockingWindow, if it's not already open
	if (!dockingWindow.IsOpen) {
		// ** Open window in desired location here **
	}
}
```

## Item Containers

The [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[IsContainerForItem](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsContainerForItem) property returns `true` when a docking window is a generated container for an item in the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemsSource) or [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemsSource) collections.

The [ContainerFromItem](xref:@ActiproUIRoot.Controls.Docking.DockSite.ContainerFromItem*) method can be used to retrieve the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) associated with a given item.  If the type of the item is known, then the [ContainerFromDocumentItem](xref:@ActiproUIRoot.Controls.Docking.DockSite.ContainerFromDocumentItem*) or [ContainerFromToolItem](xref:@ActiproUIRoot.Controls.Docking.DockSite.ContainerFromToolItem*) methods can be used instead.

## Item Container Unregistration Triggering Items Source Update

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanUpdateItemsSourceOnUnregister](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanUpdateItemsSourceOnUnregister) property indicates whether to watch the [WindowUnregistered](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowUnregistered) event for an item container [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) to be unregistered.  If that property is `true`, the dock site will try to automatically remove the related item from the appropriate [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemsSource) or [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemsSource) collection.

> [!IMPORTANT]
> The functionality will only succeed if the `ItemsSource` is an `IList` that is not read-only or fixed size.

In scenarios where the above conditions are not met, it is up to you to watch the [WindowUnregistered](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowUnregistered) event and remove the related item from its items source.

## Troubleshooting

See the [Troubleshooting](troubleshooting.md) topic for more information, with some specific details on configuring data contexts properly.
