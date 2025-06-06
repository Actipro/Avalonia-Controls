---
title: "MVVM Support"
page-title: "MVVM Support - Bars Reference"
order: 400
---
# MVVM Support

This topic discusses the overall MVVM-friendly product architecture of Bars controls, along with the optional open source companion Bars MVVM library.

The MVVM library contains a full set of view model classes, template selector classes, and related view templates that support easy MVVM-based configuration and management of Bars controls.  It is demonstrated in the main Bars "Document Editor" demo in the sample project, where a very large and complex ribbon hierarchy is constructed using MVVM techniques.

## Bars Product Architecture

All of the Bars controls have been purposefully designed to be compatible with MVVM techniques.

Before getting into the contents of the MVVM library itself, let's examine the overall Bars product architecture and how its controls work with view models, template selectors, and view templates.

### Items Controls

Many Bars controls, especially those in the ribbon hierarchy, inherit the native `ItemsControl` class.  This is key to the principle of supporting MVVM because it allows you to bind a collection of items (generally view models) to a control's `ItemsSource`.  The control's item container generator will then examine each item and if it is not an allowed "container" element for the `ItemsControl`, it will ask for a "container" element to be generated.  This is where @if (avalonia) { classes that implement [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector) }@if (wpf) { `ItemContainerTemplateSelector` classes } come into play.

### Item Container Template Selectors

@if (avalonia) {
Bars controls that derive from `ItemsControl` make heavy use of classes that implement [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector).  This interface is designed so that an item, usually a view model, is passed into a [SelectTemplate](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector.SelectTemplate*) method along with a reference to the owner `ItemsControl`, and the method will return an `IDataTemplate` to be used for that item.

The point of an [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector) is to generate a "container" element for an `ItemsControl`'s item.  The [SelectTemplate](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector.SelectTemplate*) method returns an `IDataTemplate` that contains the element hierarchy to generate.  The root element generated from that `IDataTemplate` will be used as the "container" for the item, and the item will be set as that "container" element's `DataContext`.
}
@if (wpf) {
Bars controls that derive from `ItemsControl` make heavy use of `ItemContainerTemplateSelector` classes.  This lesser-known class type works similarly to the `DataTemplateSelector` class, where an item (usually a view model) is passed into a `SelectTemplate` method, along with a control reference.  The `DataTemplateSelector.SelectTemplate` method passes in the container control instance as the control reference, whereas the `ItemContainerTemplateSelector.SelectTemplate` method passes in the owner `ItemsControl` as the control reference.

The point of an `ItemContainerTemplateSelector` is to generate a "container" element for an `ItemsControl`'s item.  The `SelectTemplate` method returns an `ItemContainerTemplate` (a class that inherits `DataTemplate`) that contains the element hierarchy to generate.  The root element generated from that `ItemContainerTemplate` will be used as the "container" for the item, and the item will be set as that "container" element's `DataContext`.
}

### Ribbon Example

As an example to the above process, let's consider the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control.  It inherits @if (avalonia) { `SelectingItemsControl`, }@if (wpf) { `Selector`, } which in turn inherits native `ItemsControl`.  It's set up to call into the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ItemContainerTemplateSelector) that is currently assigned (if any) to try and generate a [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) "container" element for any item in its `ItemsSource` that doesn't derive from [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem).

Now say that a custom ribbon tab view model class (call it `RibbonTabViewModel`) is the type of item bound to the ribbon's `ItemsSource`, and there is one instance of the class for each tab in the ribbon.

@if (avalonia) {
It's the responsibility of the [SelectTemplate](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector.SelectTemplate*) method in the [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector) assigned to [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ItemContainerTemplateSelector) to look at the `RibbonTabViewModel` and return an `IDataTemplate` that contains a [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) control at its root.  The [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) XAML in the `IDataTemplate` should have bindings configured from the view model to the control.
}
@if (wpf) {
It's the responsibility of the `SelectTemplate` method in the `ItemContainerTemplateSelector` assigned to [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ItemContainerTemplateSelector) to look at the `RibbonTabViewModel` and return an `ItemContainerTemplate` that contains a [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) control at its root.  The [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) XAML in the `ItemContainerTemplate` should have bindings configured from the view model to the control.
}

> [!NOTE]
> Bars controls are set up to pass an @if (avalonia) { [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector) }@if (wpf) { `ItemContainerTemplateSelector` } that is assigned down to any child controls.  For instance, setting the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ItemContainerTemplateSelector) property will pass that @if (avalonia) { [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector) }@if (wpf) { `ItemContainerTemplateSelector` } down through tabs, groups, and even into popup menus.  In this way, the same template selector instance can be reused throughout the control hierarchy.

With all of that in place, when a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) sees a `RibbonTabViewModel` item in its `ItemsSource`, it will generate a [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) "container" that will have its `DataContext` set as the `RibbonTabViewModel` instance, and the tab will appear in the ribbon UI.

Properties on the `RibbonTabViewModel` may be modified, and as long as `INotifyPropertyChanged` is implemented on the view model class, the property changes will flow into the generated [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) via the bindings that were set up in the @if (avalonia) { `IDataTemplate`. }@if (wpf) { `ItemContainerTemplate`. }

> [!TIP]
> View model classes can inherit the @if (avalonia) { [ObservableObjectBase](xref:ActiproSoftware.ObservableObjectBase) class from the Core Library }@if (wpf) { [ObservableObjectBase](xref:@ActiproUIRoot.ObservableObjectBase) class from the Shared Library } to easily implement `INotifyPropertyChanged`.

### Gallery Item Content

[RibbonGallery](xref:@ActiproUIRoot.Controls.Bars.RibbonGallery) and [BarMenuGallery](xref:@ActiproUIRoot.Controls.Bars.BarMenuGallery) are graphically-rich list controls that support selection of anything from colors to text styles.  Even when a ribbon hierarchy is constructed in pure XAML, gallery controls must still use MVVM for their items.

While galleries can use logic like above with an @if (avalonia) { [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector) }@if (wpf) { `ItemContainerTemplateSelector` } to generate [BarGalleryItem](xref:@ActiproUIRoot.Controls.Bars.BarGalleryItem) "container" elements for their items, the actual items should always be some sort of view model.  The visual content within a displayed gallery item is selected by a custom @if (avalonia) { [IDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IDataTemplateSelector) }@if (wpf) { `DataTemplateSelector` } instance set to the gallery's `ItemTemplateSelector` property.  The @if (avalonia) { [IDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IDataTemplateSelector).[SelectTemplate](xref:@ActiproUIRoot.Controls.Templates.IDataTemplateSelector.SelectTemplate*) }@if (wpf) { `DataTemplateSelector.SelectTemplate` } method is passed the view model item and returns @if (avalonia) { an `IDataTemplate` }@if (wpf) { a `DataTemplate` } to use as the UI for the gallery item.

The @if (avalonia) { `IDataTemplate` }@if (wpf) { `DataTemplate` } may contain the complete UI for the gallery item, or it may include a custom element that knows how to measure and render itself in code-behind.  The `DataContext` for the elements in the @if (avalonia) { `IDataTemplate` }@if (wpf) { `DataTemplate` } will be the gallery item view model, allowing for the view model's properties to be bound into any elements within the @if (avalonia) { `IDataTemplate`. }@if (wpf) { `DataTemplate`. }

See the [Gallery](controls/gallery.md) topic for more information on galleries.

### Backstage Tab Content

A [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage) control is effectively a tab control where each item generates a [RibbonBackstageTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageTabItem) "container" element if one isn't used directly as the item.  Note that buttons and separators can also be used as backstage items.

@if (avalonia) {
If the [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage).`ItemsSource` is set to a collection of view models (`InfoRibbonBackstageTabViewModel`, `NewRibbonBackstageTabViewModel`, etc.), the [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage).`ContentTemplate` property can define the user interface for each tab's content.

> [!IMPORTANT]
> When using the MVVM library, the [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage).`ContentTemplateProperty` is typically assigned by setting the respective [RibbonBackstageViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageViewModel).[ContentTemplate](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageViewModel.ContentTemplate) property on the view model.

One option is to assign a specialized `IDataTemplate` implementation to [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage).`ContentTemplate` that can examine a backstage tab view model and provide an appropriate `DataTemplate` instance to be used.  For example, the [TypedDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.TypedDataTemplateSelector) class can define implicit `DataTemplate` instances for each backstage tab with their `x:DataType` attributes set to the related view model type.  The following example demonstrates defining a [TypedDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.TypedDataTemplateSelector) as a static resource in XAML and then assigning that resource to [RibbonBackstageViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageViewModel).[ContentTemplate](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel.ContentTemplate) in the code-behind:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:RibbonWindow>
	<actipro:RibbonWindow.Resources>
		<actipro:TypedDataTemplateSelector x:Key="BackstageContentTemplates">

			<DataTemplate DataType="{x:Type local:InfoRibbonBackstageTabViewModel}">
				<!-- 'Info' tab content here -->
			</DataTemplate>

			<DataTemplate DataType="{x:Type local:NewRibbonBackstageTabViewModel}">
				<!-- 'New' tab content here -->
			</DataTemplate>

		</actipro:TypedDataTemplateSelector>
	</actipro:RibbonWindow.Resources>

	<actipro:RibbonContainerPanel>
		<actipro:Ribbon x:Name="ribbon" ... />
	</actipro:RibbonContainerPanel>

	...

</actipro:RibbonWindow>
```
```csharp
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
...

public partial class RootWindow : RibbonWindow {

	public RootWindow() {
		InitializeComponent();

		// Lookup the IDataTemplate for backstage content and assign
		// to the RibbonBackstageViewModel.ContentTemplate property
		if (ribbon.DataContext is RibbonViewModel ribbonViewModel
			&& ribbonViewModel.Backstage is not null
			&& this.TryFindResource("BackstageContentTemplates", out var contentTemplate)) {

			ribbonViewModel.Backstage.ContentTemplate = contentTemplate as IDataTemplate;
		}
	}

	...
}
```

When using the MVVM library, another option is to set either [RibbonBackstageTabViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel).[Content](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel.Content) or [RibbonBackstageTabViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel).[ContentTemplate](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel.ContentTemplate) of each individual tab to the desired content or `IDataTemplate` for that tab.
}
@if (wpf) {
If the [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage).`ItemsSource` is set to a collection of view models, the [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage).`ContentTemplateSelector` can be set to a `DataTemplateSelector` that will return the `DataTemplate` for each tab's content.  It's up to the `DataTemplateSelector.SelectTemplate` method to examine the backstage tab view model and return the appropriate `DataTemplate` for the related backstage tab's content.

Alternatively, if distinct view model types (`InfoRibbonBackstageTabViewModel`, `NewRibbonBackstageTabViewModel`, etc.) are used as the backstage tab view models, the `Resources` of the [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage) control or the containing `Window` can contain implicit `DataTemplate` instances with their `DataType` attributes set to the related view model type, like this:

```xaml
<DataTemplate DataType="{x:Type local:InfoRibbonBackstageTabViewModel}">
	<!-- 'Info' tab content here -->
</DataTemplate>

<DataTemplate DataType="{x:Type local:NewRibbonBackstageTabViewModel}">
	<!-- 'New' tab content here -->
</DataTemplate>
```

When using implicit `DataTemplate` resources, no `ContentTemplateSelector` is necessary.
}

See the [Backstage](ribbon-features/backstage.md) topic for more information on backstage.

### Ribbon Footer Content

@if (avalonia) {
The [RibbonFooterControl](xref:@ActiproUIRoot.Controls.Bars.RibbonFooterControl) is a `ContentControl` and supports standard `IDataTemplate`-based content selection.
}
@if (wpf) {
The [RibbonFooterControl](xref:@ActiproUIRoot.Controls.Bars.RibbonFooterControl) is a `ContentControl` and supports both `DataTemplateSelector` and implicit `DataTemplate`-based content selection.
}

See the [Footer](ribbon-features/footer.md) topic for more information on ribbon footers.

### Container Element Summary

This table summarizes most of the `ItemsControl`-based controls in Bars and which "container" elements they expect to be generated from an @if (avalonia) { [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector): }@if (wpf) { `ItemContainerTemplateSelector`: }

@if (avalonia) {
| ItemsControl | Expected Container Element(s) |
|-----|-----|
| [BarMainMenu](xref:@ActiproUIRoot.Controls.Bars.BarMainMenu) | [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem). |
| [BarMenuFlyout](xref:@ActiproUIRoot.Controls.Bars.BarMenuFlyout) | Various [Bars controls](controls/index.md) for menu context. |
| [BarMenuGallery](xref:@ActiproUIRoot.Controls.Bars.BarMenuGallery) | [BarGalleryItem](xref:@ActiproUIRoot.Controls.Bars.BarGalleryItem). |
| [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) | Various [Bars controls](controls/index.md) for menu context. |
| [BarPopupButton](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton) | Various [Bars controls](controls/index.md) for menu context. |
| [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) | Various [Bars controls](controls/index.md) for menu context. |
| [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) | Various [Bars controls](controls/index.md) for menu context. |
| [BarSplitToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitToggleButton) | Various [Bars controls](controls/index.md) for menu context. |
| [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) | [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem). |
| [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage) | [RibbonBackstageTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageTabItem), [RibbonBackstageHeaderButton](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageHeaderButton), or [RibbonBackstageHeaderSeparator](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageHeaderSeparator). |
| [RibbonContextualTabGroupItemsControl](xref:@ActiproUIRoot.Controls.Bars.Primitives.RibbonContextualTabGroupItemsControl) | [RibbonContextualTabGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonContextualTabGroup). |
| [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) | Various [Bars controls](controls/index.md) for ribbon context. |
| [RibbonGallery](xref:@ActiproUIRoot.Controls.Bars.RibbonGallery) | [BarGalleryItem](xref:@ActiproUIRoot.Controls.Bars.BarGalleryItem). |
| [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup) | [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup), [RibbonMultiRowControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup), or various [Bars controls](controls/index.md) for ribbon context. |
| [RibbonMultiRowControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup) | [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) or various [Bars controls](controls/index.md) for ribbon context. |
| [RibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar) | Various [Bars controls](controls/index.md) for ribbon context. |
| [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) | [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup). |
| [RibbonTabRowToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonTabRowToolBar) | Various [Bars controls](controls/index.md) for toolbar context. |
| [StandaloneToolBar](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar) | Various [Bars controls](controls/index.md) for toolbar context. |
| [TaskTabControl](xref:@ActiproUIRoot.Controls.Bars.TaskTabControl) | [TaskTabItem](xref:@ActiproUIRoot.Controls.Bars.TaskTabItem). |
}
@if (wpf) {
| ItemsControl | Expected Container Element(s) |
|-----|-----|
| [BarContextMenu](xref:@ActiproUIRoot.Controls.Bars.BarContextMenu) | Various [Bars controls](controls/index.md) for menu context. |
| [BarMainMenu](xref:@ActiproUIRoot.Controls.Bars.BarMainMenu) | [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem). |
| [BarMenuGallery](xref:@ActiproUIRoot.Controls.Bars.BarMenuGallery) | [BarGalleryItem](xref:@ActiproUIRoot.Controls.Bars.BarGalleryItem). |
| [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) | Various [Bars controls](controls/index.md) for menu context. |
| [BarPopupButton](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton) | Various [Bars controls](controls/index.md) for menu context. |
| [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) | Various [Bars controls](controls/index.md) for menu context. |
| [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) | Various [Bars controls](controls/index.md) for menu context. |
| [BarSplitToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitToggleButton) | Various [Bars controls](controls/index.md) for menu context. |
| [DockableToolBar](xref:@ActiproUIRoot.Controls.Bars.DockableToolBar) | Various [Bars controls](controls/index.md) for toolbar context. |
| [DockableToolBarHost](xref:@ActiproUIRoot.Controls.Bars.DockableToolBarHost) | [DockableToolBar](xref:@ActiproUIRoot.Controls.Bars.DockableToolBar). |
| [MiniToolBar](xref:@ActiproUIRoot.Controls.Bars.MiniToolBar) | Various [Bars controls](controls/index.md) for toolbar context. |
| [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) | [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem). |
| [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage) | [RibbonBackstageTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageTabItem), [RibbonBackstageHeaderButton](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageHeaderButton), or [RibbonBackstageHeaderSeparator](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageHeaderSeparator). |
| [RibbonContextualTabGroupItemsControl](xref:@ActiproUIRoot.Controls.Bars.Primitives.RibbonContextualTabGroupItemsControl) | [RibbonContextualTabGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonContextualTabGroup). |
| [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) | Various [Bars controls](controls/index.md) for ribbon context. |
| [RibbonGallery](xref:@ActiproUIRoot.Controls.Bars.RibbonGallery) | [BarGalleryItem](xref:@ActiproUIRoot.Controls.Bars.BarGalleryItem). |
| [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup) | [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup), [RibbonMultiRowControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup), or various [Bars controls](controls/index.md) for ribbon context. |
| [RibbonMultiRowControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup) | [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) or various [Bars controls](controls/index.md) for ribbon context. |
| [RibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar) | Various [Bars controls](controls/index.md) for ribbon context. |
| [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) | [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup). |
| [RibbonTabRowToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonTabRowToolBar) | Various [Bars controls](controls/index.md) for toolbar context. |
| [StandaloneToolBar](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar) | Various [Bars controls](controls/index.md) for toolbar context. |
| [TaskTabControl](xref:@ActiproUIRoot.Controls.Bars.TaskTabControl) | [TaskTabItem](xref:@ActiproUIRoot.Controls.Bars.TaskTabItem). |
}
## Open Source MVVM Library

The full source of the helpful companion MVVM library that contains Bars-related view model classes, template selector classes, and related view templates is available in GitHub at:

@if (avalonia) {
https://github.com/Actipro/Avalonia-Controls/tree/main/Source/Bars.Mvvm
}
@if (wpf) {
https://github.com/Actipro/WPF-Controls/tree/main/Source/Bars.Mvvm
}

If you wish for your Bars controls to be managed with MVVM, but don't wish to use our pre-built MVVM library, its source code is still an excellent reference for how you can integrate custom view models, template selectors, and templates with Bars controls.

## MVVM Library Contents

The MVVM library contains many types and templates to assist in managing Bars control hierarchies using MVVM techniques.

### Template Selectors

As described earlier in this topic, the Bars product has been designed to make use of @if (avalonia) { [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector) and [IDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IDataTemplateSelector) }@if (wpf) { `ItemContainerTemplateSelector` and `DataTemplateSelector` } instances to associate view models with related view templates.  The MVVM library includes numerous view model types and offers several template selectors classes that map the view models to predefined view templates.

#### For Bar Controls

The [BarControlTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarControlTemplateSelector) class @if (avalonia) { implements [IItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IItemContainerTemplateSelector) }@if (wpf) { inherits `ItemContainerTemplateSelector` } and helps generate "container" elements for all of the bar control view models described later in this topic.

@if (avalonia) {
When using the MVVM library with Bars controls, an instance of [BarControlTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarControlTemplateSelector) should be set to the root bar control's `ItemContainerTemplateSelector` property, such as [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ItemContainerTemplateSelector), [BarMainMenu](xref:@ActiproUIRoot.Controls.Bars.BarMainMenu).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.BarMainMenu.ItemContainerTemplateSelector), or [StandaloneToolBar](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar.ItemContainerTemplateSelector).  The template selector will propagate down the hierarchy of bar controls automatically.
}
@if (wpf) {
When using the MVVM library with Bars controls, an instance of [BarControlTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarControlTemplateSelector) should be set to the root bar control's `ItemContainerTemplateSelector` property, such as [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ItemContainerTemplateSelector), [DockableToolBarHost](xref:@ActiproUIRoot.Controls.Bars.DockableToolBarHost).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.DockableToolBarHost.ItemContainerTemplateSelector), [BarMainMenu](xref:@ActiproUIRoot.Controls.Bars.BarMainMenu).`ItemContainerTemplateSelector`, or [StandaloneToolBar](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar).[ItemContainerTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar.ItemContainerTemplateSelector).  The template selector will propagate down the hierarchy of bar controls automatically.
}

#### For Ribbon Footer Content

The [RibbonFooterContentTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterContentTemplateSelector) class @if (avalonia) { implements [IDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IDataTemplateSelector) }@if (wpf) { inherits `DataTemplateSelector` } and provides the view @if (avalonia) { `IDataTemplate` }@if (wpf) { `DataTemplate` } to use for ribbon footer content view models described later in this topic that are passed into the [RibbonFooterViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterViewModel).[Content](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterViewModel.Content) property.  An instance of the template selector class is automatically assigned to the [RibbonFooterViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterViewModel).[ContentTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterViewModel.ContentTemplateSelector) property.

#### For Gallery Items

The [BarGalleryItemTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemTemplateSelector) class @if (avalonia) { implements [IDataTemplateSelector](xref:@ActiproUIRoot.Controls.Templates.IDataTemplateSelector) }@if (wpf) { inherits `DataTemplateSelector` } and provides the view @if (avalonia) { `IDataTemplate` }@if (wpf) { `DataTemplate` } to use for gallery item view models described later in this topic.  An instance of the template selector needs to be assigned to each gallery control, which can be done through the [BarGalleryViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryViewModel).[ItemTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryViewModelBase.ItemTemplateSelector) property.

### View Models

The MVVM library contains many view models that can drive the various Bars controls via MVVM.

#### Bar Control View Models

The following table shows various bar control view model types defined in the MVVM library, and maps them to the control concepts they are pre-configured to generate via a [BarControlTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarControlTemplateSelector) instance.

@if (avalonia) {
| Name | Description |
|-----|-----|
| [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) | Regular [button](controls/button.md) control. |
| [BarCheckBoxViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarCheckBoxViewModel) | [Checkbox](controls/checkbox.md) control. |
| [BarComboBoxViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarComboBoxViewModel) | [Combobox](controls/combobox.md) control. |
| [BarGalleryViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryViewModel) | [Gallery](controls/gallery.md) control. |
| [BarHeadingViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarHeadingViewModel) | [Heading](controls/heading.md) control. |
| [BarKeyedObjectViewModelBase](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarKeyedObjectViewModelBase) | Abstract base class for a [control with a string keys](controls/control-basics.md). |
| [BarMainMenuViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarMainMenuViewModel) | [Main Menu](menu-features/main-menu.md) control. |
| [BarPopupButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarPopupButtonViewModel) | [Popup button](controls/popup-button.md) control. |
| [BarSeparatorViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSeparatorViewModel) | [Separator](controls/separator.md) control. |
| [BarSizeSelectionMenuGalleryViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSizeSelectionMenuGalleryViewModel) | Size-selection [gallery](controls/gallery.md) control. |
| [BarSplitButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitButtonViewModel) | Regular [split button](controls/split-button.md) control. |
| [BarSplitToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitToggleButtonViewModel) | Toggle [split button](controls/split-button.md) control. |
| [BarTextBoxViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarTextBoxViewModel) | [Textbox](controls/textbox.md) control. |
| [BarToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarToggleButtonViewModel) | Toggle [button](controls/button.md) control. |
| [RibbonApplicationButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonApplicationButtonViewModel) | [Application button](ribbon-features/application-button.md) control. |
| [RibbonBackstageHeaderButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageHeaderButtonViewModel) | [Backstage](ribbon-features/backstage.md) header button control. |
| [RibbonBackstageHeaderSeparatorViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageHeaderSeparatorViewModel) | [Backstage](ribbon-features/backstage.md) header separator control. |
| [RibbonBackstageTabViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel) | [Backstage](ribbon-features/backstage.md) tab control. |
| [RibbonContextualTabGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonContextualTabGroupViewModel) | [Contextual tab group](ribbon-features/contextual-tabs.md) control. |
| [RibbonControlGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonControlGroupViewModel) | [Control group](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonFooterViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterViewModel) | [Footer](ribbon-features/footer.md) control. |
| [RibbonGroupLauncherButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonGroupLauncherButtonViewModel) | [Group launcher button](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonGroupViewModel) | [Group](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonMultiRowControlGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonMultiRowControlGroupViewModel) | [Multi-row control group](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonQuickAccessToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonQuickAccessToolBarViewModel) | [Quick access toolbar](ribbon-features/quick-access-toolbar.md) control. |
| [RibbonTabRowToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonTabRowToolBarViewModel) | [Tab row toolbar](ribbon-features/tab-row-toolbar.md) control. |
| [RibbonTabViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonTabViewModel) | [Tab](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonViewModel) | [Ribbon](ribbon-features/index.md) control. |
| [StandaloneToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.StandaloneToolBarViewModel) | [Standalone toolbar](toolbar-features/standalone-toolbars.md) control. |
}
@if (wpf) {
| Name | Description |
|-----|-----|
| [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) | Regular [button](controls/button.md) control. |
| [BarCheckBoxViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarCheckBoxViewModel) | [Checkbox](controls/checkbox.md) control. |
| [BarComboBoxViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarComboBoxViewModel) | [Combobox](controls/combobox.md) control. |
| [BarGalleryViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryViewModel) | [Gallery](controls/gallery.md) control. |
| [BarHeadingViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarHeadingViewModel) | [Heading](controls/heading.md) control. |
| [BarKeyedObjectViewModelBase](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarKeyedObjectViewModelBase) | Abstract base class for a [control with a string keys](controls/control-basics.md). |
| [BarMainMenuViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarMainMenuViewModel) | [Main Menu](menu-features/main-menu.md) control. |
| [BarPopupButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarPopupButtonViewModel) | [Popup button](controls/popup-button.md) control. |
| [BarSeparatorViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSeparatorViewModel) | [Separator](controls/separator.md) control. |
| [BarSizeSelectionMenuGalleryViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSizeSelectionMenuGalleryViewModel) | Size-selection [gallery](controls/gallery.md) control. |
| [BarSplitButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitButtonViewModel) | Regular [split button](controls/split-button.md) control. |
| [BarSplitToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarSplitToggleButtonViewModel) | Toggle [split button](controls/split-button.md) control. |
| [BarTextBoxViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarTextBoxViewModel) | [Textbox](controls/textbox.md) control. |
| [BarToggleButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarToggleButtonViewModel) | Toggle [button](controls/button.md) control. |
| [DockableToolBarHostViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.DockableToolBarHostViewModel) | [Dockable Toolbar](toolbar-features/dockable-toolbars.md) host control. |
| [DockableToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.DockableToolBarViewModel) | [Dockable Toolbar](toolbar-features/dockable-toolbars.md) control. |
| [MiniToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.MiniToolBarViewModel) | [Mini-toolbar](toolbar-features/mini-toolbars.md) control. |
| [RibbonApplicationButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonApplicationButtonViewModel) | [Application button](ribbon-features/application-button.md) control. |
| [RibbonBackstageHeaderButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageHeaderButtonViewModel) | [Backstage](ribbon-features/backstage.md) header button control. |
| [RibbonBackstageHeaderSeparatorViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageHeaderSeparatorViewModel) | [Backstage](ribbon-features/backstage.md) header separator control. |
| [RibbonBackstageTabViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonBackstageTabViewModel) | [Backstage](ribbon-features/backstage.md) tab control. |
| [RibbonContextualTabGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonContextualTabGroupViewModel) | [Contextual tab group](ribbon-features/contextual-tabs.md) control. |
| [RibbonControlGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonControlGroupViewModel) | [Control group](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonFooterViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterViewModel) | [Footer](ribbon-features/footer.md) control. |
| [RibbonGroupLauncherButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonGroupLauncherButtonViewModel) | [Group launcher button](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonGroupViewModel) | [Group](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonMultiRowControlGroupViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonMultiRowControlGroupViewModel) | [Multi-row control group](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonQuickAccessToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonQuickAccessToolBarViewModel) | [Quick access toolbar](ribbon-features/quick-access-toolbar.md) control. |
| [RibbonTabRowToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonTabRowToolBarViewModel) | [Tab row toolbar](ribbon-features/tab-row-toolbar.md) control. |
| [RibbonTabViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonTabViewModel) | [Tab](ribbon-features/tabs-groups-controlgroups.md) control. |
| [RibbonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonViewModel) | [Ribbon](ribbon-features/index.md) control. |
| [StandaloneToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.StandaloneToolBarViewModel) | [Standalone toolbar](toolbar-features/standalone-toolbars.md) control. |
}

> [!NOTE]
> Some view models may generate a different control based on the usage context.  For instance, a [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) will generate a [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) control when in a ribbon/toolbar context, and a [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) control when in a menu context.

#### Ribbon Footer Content View Models

The following table shows the ribbon footer content view model types defined in the MVVM library, which are supported by the [RibbonFooterContentTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterContentTemplateSelector) class out of the box.  Instances of these view model types can be assigned to the [RibbonFooterViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterViewModel).[Content](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterViewModel.Content) property when a footer message should be displayed.

| Name | Description |
|-----|-----|
| [RibbonFooterInfoBarContentViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterInfoBarContentViewModel) | Configures an @if (avalonia) { [InfoBar](../fundamentals/controls/info-bar.md) }@if (wpf) { [InfoBar](../shared/windows-controls/info-bar.md) } to render in the ribbon [footer](ribbon-features/footer.md) |
| [RibbonFooterSimpleContentViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonFooterSimpleContentViewModel) | Specifies an @if (avalonia) { `IImage` }@if (wpf) { `ImageSource` } and text message to render in the ribbon [footer](ribbon-features/footer.md). |

#### Gallery Item View Models

The following table shows the [gallery item](controls/gallery.md) view model types defined in the MVVM library, which are supported by the [BarGalleryItemTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemTemplateSelector) class out of the box.  Instances of these view model types can be bound into the `ItemsSource` of any [gallery](controls/gallery.md) control.

| Name | Description |
|-----|-----|
| [IBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel) | An interface that provides the common requirements of a gallery item view model. |
| [BarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemViewModel`1) | Has a generic type parameter for a related value.  Can be used directly or inherited for specialized gallery item view model types. |
| [ColorBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.ColorBarGalleryItemViewModel) | Represents a `Color` value, used for any kind of color picker gallery. |
| [EnumBarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1) | Has a generic type parameter for a related enumeration value.  Can be used directly or inherited for specialized gallery item view model types.  See "Using EnumBarGaleryItemViewModel" section below for more details. |
| [FontFamilyBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.FontFamilyBarGalleryItemViewModel) | Represents a font family name, used for font comboboxes. |
| [FontSizeBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.FontSizeBarGalleryItemViewModel) | Represents a font size, used for font size comboboxes. |
| [SymbolBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.SymbolBarGalleryItemViewModel) | Represents a symbol (e.g., copyright sign) string, used to insert a symbol into a document.  Rendered by a [SymbolPresenter](xref:@ActiproUIRoot.Controls.Bars.Mvvm.SymbolPresenter) element. |
| [TextBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.TextBarGalleryItemViewModel) | Represents a simple text string. |
| [TextStyleBarGalleryItemViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.TextStyleBarGalleryItemViewModel) | Represents a text style for a document, which stores information like font, color, bold, etc.  Rendered by a [TextStylePresenter](xref:@ActiproUIRoot.Controls.Bars.Mvvm.TextStylePresenter) element. |

#### Using EnumBarGalleryItemViewModel

Instances of the [EnumBarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1) class (where `T` is an enumeration type) are typically generated by the [EnumBarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1).[CreateCollection](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1.CreateCollection*) method.  This static method uses reflection to automatically generate a view model for every value that is defined for the enumeration.

By default, the [Label](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel.Label) and [Value](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel.Value) are populated from the declaration, but attributes can also be used to define additional information, customize the label, or indicate a value should be skipped.

| Attribute Class | Property Name | Description |
| --- | --- | --- |
| `System.ComponentModel.EditorBrowsableAttribute` | `State` | When set to `EditorBrowsableState.Never`, the value will be ignored when generating the collection. |
| `System.ComponentModel.DescriptionAttribute` | `Description` | Defines a custom [Label](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel.Label) replacing the declared name. |
| `System.ComponentModel.DataAnnotations.DisplayAttribute` | `Name` | Defines a custom [Label](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel.Label) replacing the declared name. If `DescriptionAttribute` is also defined, this attribute property is ignored.
| `System.ComponentModel.DataAnnotations.DisplayAttribute` | `ShortName` | Defines a custom [Label](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel.Label) replacing the declared name. If either `DescriptionAttribute` or `DisplayAttribute.Name` is also defined, this attribute property is ignored.
| `System.ComponentModel.DataAnnotations.DisplayAttribute` | `GroupName` | Populates the [Category](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel.Category) property that is used for grouping.
| `System.ComponentModel.DataAnnotations.DisplayAttribute` | `Description` | Populates the [Description](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarGalleryItemViewModel.Description) property that is used for tooltips.
| `System.ComponentModel.DataAnnotations.DisplayAttribute` | `Order` | (Default = `10000`) Used to determine the order in which items appear in the collection. Values lower than the default will appear before the items with default order, and those with values higher than the default will appear after the items with default order. |

> [!IMPORTANT]
> Since the [CreateCollection](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1.CreateCollection*) method uses `DisplayAttribute` to initialize some view model properties and that attribute supports localization, any view models created by this method will reflect the locale at the time they were created. If the locale changes, call [EnumBarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1).[RefreshFromAttributes](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1.RefreshFromAttributes*) to update properties from current attribute data.

> [!TIP]
> Use the [EnumBarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1).[CreateCollection](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1.CreateCollection*) method to easily populate a [Combobox](controls/combobox.md) with all the available values of an enumeration.

@if (avalonia) {
### Root Bar Control Themes

The [BarsMvvmResourceKeys](xref:@ActiproUIRoot.Themes.Bars.Mvvm.BarsMvvmResourceKeys) class provides access to keys of several pre-configured `ControlTheme` instances in `Resources` that bind root bar controls to their related view model.  It is assumed that the appropriate view model is set to the bar control's `DataContext` property in your code.

| Property Name | Description |
|-----|-----|
| [RibbonControlTheme](xref:@ActiproUIRoot.Themes.Bars.Mvvm.BarsMvvmResourceKeys.RibbonControlTheme) | Sets up bindings on a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control to a [RibbonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonViewModel) instance. |
| [BarMainMenuControlTheme](xref:@ActiproUIRoot.Themes.Bars.Mvvm.BarsMvvmResourceKeys.BarMainMenuControlTheme) | Sets up bindings on a [BarMainMenu](xref:@ActiproUIRoot.Controls.Bars.BarMainMenu) control to a [BarMainMenuViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarMainMenuViewModel) instance. |
| [StandaloneToolBarControlTheme](xref:@ActiproUIRoot.Themes.Bars.Mvvm.BarsMvvmResourceKeys.StandaloneToolBarControlTheme) | Sets up bindings on a [StandaloneToolBar](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar) control to a [StandaloneToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.StandaloneToolBarViewModel) instance. |

> [!TIP]
> It is recommended that these control themes are used on the root bar controls when using the MVVM library.

This example shows how to use a root bar control `ControlTheme` on a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon):

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:Ribbon x:Name="ribbon"
	DataContext="{Binding Ribbon}"
	Theme="{StaticResource {x:Static actipro:BarsMvvmResourceKeys.RibbonControlTheme}}"
	/>
```
}
@if (wpf) {

### Root Bar Control Styles

The [BarsMvvmResourceKeys](xref:@ActiproUIRoot.Themes.BarsMvvmResourceKeys) class provides access to several pre-configured `Style` instances in `Resources` that bind root bar controls to their related view model.  It is assumed that the appropriate view model is set to the bar control's `DataContext` property in your code.

| Property Name | Description |
|-----|-----|
| [RibbonStyle](xref:@ActiproUIRoot.Themes.BarsMvvmResourceKeys.RibbonStyle) | Sets up bindings on a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control to a [RibbonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.RibbonViewModel) instance. |
| [BarMainMenuStyle](xref:@ActiproUIRoot.Themes.BarsMvvmResourceKeys.BarMainMenuStyle) | Sets up bindings on a [BarMainMenu](xref:@ActiproUIRoot.Controls.Bars.BarMainMenu) control to a [BarMainMenuViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarMainMenuViewModel) instance. |
| [DockableToolBarHostStyle](xref:@ActiproUIRoot.Themes.BarsMvvmResourceKeys.DockableToolBarHostStyle) | Sets up bindings on a [DockableToolBarHost](xref:@ActiproUIRoot.Controls.Bars.DockableToolBarHost) control to a [DockableToolBarHostViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.DockableToolBarHostViewModel) instance. |
| [StandaloneToolBarStyle](xref:@ActiproUIRoot.Themes.BarsMvvmResourceKeys.StandaloneToolBarStyle) | Sets up bindings on a [StandaloneToolBar](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar) control to a [StandaloneToolBarViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.StandaloneToolBarViewModel) instance. |

> [!TIP]
> It is recommended that these styles are used on the root bar controls when using the MVVM library.

This example shows how to use a root bar control `Style` on a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon):

```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
xmlns:themes="http://schemas.actiprosoftware.com/winfx/xaml/themes"
...
<bars:Ribbon x:Name="ribbon"
	DataContext="{Binding Ribbon}"
	Style="{StaticResource {x:Static themes:BarsMvvmResourceKeys.RibbonStyle}}"
	/>
```
}

### Bar Image Provider

The [IBarImageProvider](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarImageProvider) interface defines the base requirements for a class that can provide images for a given set of options.

The [BarImageOptions](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageOptions) class designates a desired image size via its [Size](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageOptions.Size) property, whose values can be `Small` (16x16), `Medium` (24x24), or `Large` (32x32).  It also designates an optional [ContextualColor](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageOptions.ContextualColor) that some implementations may use to show a swatch color on top of an image.  This feature can be used for images that show the current text color.

The [BarImageProvider](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageProvider) class implements [IBarImageProvider](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IBarImageProvider) and allows you to [Register](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageProvider.Register*) an image factory function for a string key.  The key should generally match the key for the related bar control.

@if (avalonia) {
The factory function is called when the [GetImage](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageProvider.GetImage*) method is invoked and passes a [BarImageOptions](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageOptions) instance, while expecting an `IImage` in return.
}
@if (wpf) {
The factory function is called when the [GetImageSource](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageProvider.GetImageSource*) method is invoked and passes a [BarImageOptions](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageOptions) instance, while expecting an `ImageSource` in return.
}

This example demonstrates a registration, and assumes your application has an `ImageLoader.GetIcon` method that loads the image with the specified filename:

```csharp
imageProvider.Register("AlignCenter", options => ImageLoader.GetIcon("AlignTextCenter16.png"));
```

Call the [Unregister](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarImageProvider.Unregister*) method to later unregister a factory function for a particular key if needed.

## Customizing View Models and Template Selectors

Any of the template selectors described above can be augmented in case the built-in functionality is not enough, or new view model classes are used.

### Adding a View Model Property

For instance, say the [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) class is missing a `Cursor` property that you wish to set to alter the default button cursor.  Create a class that inherits [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel) and adds a `Cursor` property.  Update the [BarControlTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarControlTemplateSelector).[BarButtonDefaultTemplate](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarControlTemplateSelector.BarButtonDefaultTemplate) property to point to a `DataTemplate` that is a clone of the default `DataTemplate`, but also adds a binding of the [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton).`Cursor` property to the view model's `Cursor` property.

> [!TIP]
> Each view model also has a [Tag](xref:@ActiproUIRoot.Controls.Bars.Mvvm.IHasTag.Tag) property that can be used to store user-defined custom data.

### Third-Party Control Support

In another scenario, perhaps you wish to introduce a brand-new bar control view model class for a third-party control.  Create a view model class with the properties you wish to bind to the control.  Next, create a class that inherits [BarControlTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarControlTemplateSelector) and override its `SelectTemplate` method to handle the new view model as an item.  It should return a new @if (avalonia) { `IDataTemplate` }@if (wpf) { `DataTemplate` } that contains the third-party control and appropriate bindings to the view model.  Be sure to call the base `SelectTemplate` method for other bar control view model types so they are still handled properly.  Set an instance of the custom [BarControlTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarControlTemplateSelector)-based class to all root bar controls like [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) or [StandaloneToolBar](xref:@ActiproUIRoot.Controls.Bars.StandaloneToolBar).

### New Gallery Item Kind Support

The most common customization is likely for gallery items since these tend to be more application specific.  When creating a new kind of gallery item, create a view model class that inherits [BarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemViewModel`1) and has whatever other properties are necessary for the gallery item to be identifiable and capable of rendering in UI.  Next, create a class that inherits [BarGalleryItemTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemTemplateSelector) and override its `SelectTemplate` method to handle the new view model as an item.  It should return a new @if (avalonia) { `IDataTemplate` }@if (wpf) { `DataTemplate` } that contains the UI elements used to render the gallery item, along with appropriate bindings to the view model.  Be sure to call the base `SelectTemplate` method for other gallery item view model types so they are still handled properly.  Set an instance of the custom [BarGalleryItemTemplateSelector](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemTemplateSelector)-based class to any galleries that host the new kind of gallery item.
