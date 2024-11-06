---
title: "Serialization"
page-title: "Serialization - Ribbon Features"
order: 75
---
# Serialization

Ribbon provides many options to customize the layout.  When users customize the ribbon in an application, they will expect those customizations to be restored the next time the application is launched.

By using the serialization features, it is easy to save the current layout and restore it later.

Developers have full control over which settings are saved and restored during serialization.  This will ensure that if an application is not designed to support a feature, the serializer will ignore those settings even if they are present in the serialized data.

## RibbonSerializer

The [RibbonSerializer](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializer) class is responsible for saving and restoring a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) layout. Layout information persists to an XML string.  This XML string in turn can be saved to a settings file, database, or any other sort of storage mechanism you use for keeping user data.

Call the [RibbonSerializer](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializer).[Serialize](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializer.Serialize*) method to obtain the serialized data and later on, call the [RibbonSerializer](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializer).[Deserialize](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializer.Deserialize*) method to restore the serialized data.

## RibbonSerializerOptions

By default, the `Serialize` and `Deserialize` methods will include all supported options, but each method also defines an overload that accepts [RibbonSerializerOptions](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions).

[RibbonSerializerOptions](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions) is an enumeration that allows multiple flags to be set that determine which options should be processed during the serialization/deserialization process.  The following flags are available:

@if (avalonia) {
| Flag | Description |
| ---- | ---- |
| [LayoutMode](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.LayoutMode) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[LayoutMode](xref:@ActiproUIRoot.Controls.Bars.Ribbon.LayoutMode) property should be processed. |
| [MinimizedState](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.MinimizedState) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[IsMinimized](xref:@ActiproUIRoot.Controls.Bars.Ribbon.IsMinimized) property should be processed. |
| [QuickAccessToolBarAllowLabels](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.QuickAccessToolBarAllowLabels) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[AllowLabelsOnQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.Ribbon.AllowLabelsOnQuickAccessToolBar) property should be processed. |
| [QuickAccessToolBarLocation](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.QuickAccessToolBarLocation) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[QuickAccessToolBarLocation](xref:@ActiproUIRoot.Controls.Bars.Ribbon.QuickAccessToolBarLocation) property should be processed. |
| [QuickAccessToolBarMode](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.QuickAccessToolBarMode) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[QuickAccessToolBarMode](xref:@ActiproUIRoot.Controls.Bars.Ribbon.QuickAccessToolBarMode) property should be processed. |
| [QuickAccessToolBarItems](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.QuickAccessToolBarItems) | Indicates if the ribbon's [RibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar).`Items` collection should be processed. |
}
@if (wpf) {
| Flag | Description |
| ---- | ---- |
| [LayoutMode](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.LayoutMode) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[LayoutMode](xref:@ActiproUIRoot.Controls.Bars.Ribbon.LayoutMode) property should be processed. |
| [MinimizedState](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.MinimizedState) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[IsMinimized](xref:@ActiproUIRoot.Controls.Bars.Ribbon.IsMinimized) property should be processed. |
| [QuickAccessToolBarAllowLabels](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.QuickAccessToolBarAllowLabels) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[AllowLabelsOnQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.Ribbon.AllowLabelsOnQuickAccessToolBar) property should be processed. |
| [QuickAccessToolBarLocation](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.QuickAccessToolBarLocation) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[QuickAccessToolBarLocation](xref:@ActiproUIRoot.Controls.Bars.Ribbon.QuickAccessToolBarLocation) property should be processed. |
| [QuickAccessToolBarMode](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.QuickAccessToolBarMode) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[QuickAccessToolBarMode](xref:@ActiproUIRoot.Controls.Bars.Ribbon.QuickAccessToolBarMode) property should be processed. |
| [QuickAccessToolBarItems](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.QuickAccessToolBarItems) | Indicates if the ribbon's [RibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar).`Items` collection should be processed. |
| [UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.UserInterfaceDensity) | Indicates if the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[UserInterfaceDensity](xref:@ActiproUIRoot.Controls.Bars.Ribbon.UserInterfaceDensity) property should be processed. |
}

## Restoring Quick Access Toolbar Items

When the [RibbonSerializerOptions](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions).[QuickAccessToolBarItems](xref:@ActiproUIRoot.Controls.Bars.RibbonSerializerOptions.QuickAccessToolBarItems) option is defined for saving a layout, the `Key` of each item in the ribbon's quick access toolbar will be included in the layout. If the same option is defined when restoring the layout, the ribbon will attempt to locate an item with the corresponding `Key` and add it to the toolbar.

The [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).[OnQuickAccessToolBarItemAdding](xref:@ActiproUIRoot.Controls.Bars.Ribbon.OnQuickAccessToolBarItemAdding*) event is raised with [RibbonQuickAccessToolBarItemAddingEventArgs](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBarItemAddingEventArgs) data when an item is being added to the ribbon's quick access toolbar.  If an item with the corresponding `Key` was located, the [RibbonQuickAccessToolBarItemAddingEventArgs](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBarItemAddingEventArgs).[Item](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBarItemAddingEventArgs.Item) property will be set to the matching item. If an item could not be found, the [Item](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBarItemAddingEventArgs.Item) property will be `null`.

The [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) will first search any [RibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar).[CommonItems](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar.CommonItems) for a matching `Key`. If not found, the currently configured ribbon tabs and their content will be recursively searched, but not all controls can be found in this manner (like popup-based controls whose popup content is not loaded).

> [!WARNING]
> When an item of the desired `Key` cannot be automatically located, the [RibbonQuickAccessToolBarItemAddingEventArgs](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBarItemAddingEventArgs).[Item](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBarItemAddingEventArgs.Item) property must be manually assigned an appropriate item. If this property remains `null`, the `Key` will be ignored and nothing will be restored.

> [!IMPORTANT]
> If the [RibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar).`Items` collection is populated with view models, the item assigned to the [RibbonQuickAccessToolBarItemAddingEventArgs](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBarItemAddingEventArgs).[Item](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBarItemAddingEventArgs.Item) property should also be a view model. Otherwise, if the `Items` collection is populated with controls, the `Item` should be a control.

> [!TIP]
> See the "Serialization" Bars Ribbon QuickStart of the Sample Browser application for a full demonstration working with serialization features.