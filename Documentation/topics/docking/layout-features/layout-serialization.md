---
title: "Layout Serialization"
page-title: "Layout Serialization - Docking & MDI Layout, Globalization, and Accessibility Features"
order: 2
---
# Layout Serialization

The full window layout can be saved to XML and restored later, allowing end users to retain their customized layout information across multiple application executions.

Window layout data stores the location, size, state, and other related information for all document and tool windows currently being managed by the dock site.

## Requirements for Docking Windows to Serialize Properly

When serializing a layout, each [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) and [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) must have either its [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) or `Name` property set to a unique value.  These properties are used to restore the layout later by matching the docking windows in the layout with already-registered docking windows in the dock site, meaning tool windows in the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindows) collection and document windows in the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[DocumentWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentWindows) collection.

The `Name` property is only used for matching if the [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) property is not specified.  Thus, [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) has a higher priority than `Name` in terms of which property is used to match layout data with registered docking window instances.

The [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) property is the preferred property to use for matching layout data with docking windows for two reasons.  First, it can accept any text characters as its value, whereas the `Name` property must be a valid .NET identifier (i.e., underscore, letter, and number characters only).  Second, it supports bindings in ways where the `Name` property might sometimes be restricted from supporting a binding.

> [!CAUTION]
> Make sure every docking window has either a unique [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) or `Name` set as described above, or else layout deserialization will not work properly.

Docking windows can be individually prevented from serializing within a layout by setting their [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanSerialize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanSerialize) property to `false`.  This property defaults to `true`, meaning that all applicable docking windows will normally participate in the serialized layout.

## Saving Layout Data

Dock site layout data can be persisted in XML format and loaded at a later time.  Probably the two most common ways to store layouts are in files and in a database.

The [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer) class fully implements the XML object hierarchy serialization framework described in the [Serialization](../../shared/windows-serialization.md) topic.  Please see that topic for a list of methods that can be called for saving to files, string, etc.

This sample code shows how to save a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) layout to an XML string:

```csharp
static DockSiteLayoutSerializer? serializer;
...
serializer ??= new DockSiteLayoutSerializer();
string layout = serializer.SaveToString(dockSite);
```

### Saving the Entire Layout (Including Document Windows and MDI)

By default, only layout information of tool windows is serialized. The full layout, including document windows and the MDI layout, can be saved by setting the [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer).[SerializationBehavior](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer.SerializationBehavior) to [DockSiteSerializationBehavior](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteSerializationBehavior).`All`.

## Loading Layout Data

When loaded, dock site layout data restores the location, size, and state of all document and/or tool windows.  Layouts that include the full layout information (as described above), will restore document windows, tool windows, and the MDI layout.

The layout data loading code matches the [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) (or `Name`) of windows that were stored in the layout data to the [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) (or `Name`) of windows currently registered with the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).

The [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer) class fully implements the XML object hierarchy serialization framework described in the [Serialization](../../shared/windows-serialization.md) topic.  Please see that topic for a list of methods that can be called for loading from files, string, etc.

This sample code shows how to load a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) layout from the XML string:

```csharp
static DockSiteLayoutSerializer? serializer;
...
serializer ??= new DockSiteLayoutSerializer();
serializer.LoadFromString(layout, dockSite);
```

## Window Deserialization Behaviors

When loading a previously saved layout, any document or tool windows that are missing will not have their layout restored.  So, if a tool window with the name `"TW1"` has size and position information in the layout string, but is not currently registered with the `DockSite`, then it's layout information is ignored.

There are two other behaviors supported, which allow the layout information to be retained and then applied to the associated window.  These behaviors can be configured using the [DocumentWindowDeserializationBehavior](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer.DocumentWindowDeserializationBehavior) and/or [ToolWindowDeserializationBehavior](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer.ToolWindowDeserializationBehavior) properties on [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer).

### Lazy Loading

The window layout information will be applied to the `DocumentWindow` and/or `ToolWindow` later, when they are registered with the `DockSite`.  When loading the layout data, the information for any missing document or tool windows is saved in the `DockSite` and applied if and when the window is ever created.

This allows the layout information to be properly restored, without having to immediately create the associated document or tool windows.

When using this feature, the `DocumentWindow` and/or `ToolWindow` must specify a unique [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) or `Name` so that its associated layout data can be found.  In addition, when creating a `DocumentWindow` and/or `ToolWindow` its [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) or `Name` must be specified *before* it is registered with the `DockSite`. If this is not done, then the layout data will not be restored.

This sample code *will not* properly load the layout data, because the `ToolWindow` is registered before it `SerializationId` property is set:

```csharp
// INCORRECT
var tw = new ToolWindow(this.dockSite) {
	SerializationId = "MyToolWindow",
};
```

This sample code *will* properly load the layout data, because the `ToolWindow` has its `SerializationId` property set before it is registered:

```csharp
// CORRECT
var tw = new ToolWindow() {
	SerializationId = "MyToolWindow",
};
this.dockSite.ToolWindows.Add(tw);
```

This sample code will also properly load the layout data, because the `ToolWindow` has its `SerializationId` property value passed to the constructor:

```csharp
// CORRECT
var tw = new ToolWindow(this.dockSite, "MyToolWindow", "Title", null, "Content");
```

Normally when a layout is serialized, the dock site's unused lazy load data for docking windows that have not yet been registered will be discarded in the serialized layout.  This prevents the data from accumulating over time if you have a scenario where you dynamically create different docking windows with limited lifetime.  In some cases, you may wish for this unused lazy load data to persist in the serialized layout.  To enable this feature, set the [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer).[CanSerializeUnusedLazyLoadData](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer.CanSerializeUnusedLazyLoadData) property to `true`.

### Auto Creation

Alternatively, the missing document or tool windows can be automatically created and registered with the `DockSite`.  During this process, certain information must be manually configured, such as the title, image, and content. There are two ways that this can be done.  First, by using the [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer).[DockingWindowDeserializing](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer.DockingWindowDeserializing) or the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowsOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsOpening) event.  Second, by using a custom class that derives from `DocumentWindow` or `ToolWindow`.

The [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer).[DockingWindowDeserializing](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer.DockingWindowDeserializing) is fired before any document and/or tool window is deserialized. The event arguments are of type [DockingWindowDeserializingEventArgs](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockingWindowDeserializingEventArgs), which include the XML node from the layout data and the associated `DockingWindow`.  When using auto-creation, the docking window is a new instance created during the deserialization process.  The properties on this window can then be explicitly set, as needed.  The event handler attached to `DockingWindowDeserializing` simply needs to check the name of the XML node to know the window for which the event was fired.

If the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowsOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsOpening) event is used instead, check to see if any of the windows specified in the event arguments have been initialized yet.  If not, then set appropriate properties like title, image, and content on the uninitialized windows.

When using a custom class that derives from `DocumentWindow` or `ToolWindow`, the properties can be assigned in the constructor of the class. In addition, a XAML file with code-behind can be leveraged to allow the window to be designed in the Visual Studio designer.

## Keeping All Existing Documents Open

When deserializing document layouts, all documents are closed and only those document windows that are listed in the layout data are re-opened.  This means that if a document was open prior to the layout deserialization, but it is not listed within the layout data, it will be closed (and destroyed if [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[AreDocumentWindowsDestroyedOnClose](xref:@ActiproUIRoot.Controls.Docking.DockSite.AreDocumentWindowsDestroyedOnClose) is set).

Sometimes this is not desired, and you may wish to keep documents open that were already open prior to layout deserialization but aren't in the layout data.  This can be achieved by setting the [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer).[CanKeepExistingDocumentWindowsOpen](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer.CanKeepExistingDocumentWindowsOpen) property to `true`.

## Persisting Custom Data in the Layout Data

One benefit of our XML object hierarchy serialization framework is that you can insert and later retrieve information anywhere within the serialized data.  Any time an object is serialized or deserialized, an event is raised.  You can intercept this event and save/load custom data.

The [Serialization](../../shared/windows-serialization.md) topic explains how to do this.

## Optimal Memory Utilization when Using Layout Serializers

The layout serializer uses an `XmlSerializer` as the core .NET object that reads/writes XML data.  One issue that has been discovered in Microsoft's .NET implementation is that `XmlSerializer` is capable of creating memory leaks, primarily whenever new instances of `XmlSerializer` are created.

To combat this leak, we've implemented some caching code on our end, but also highly recommend that instead of creating a new layout serializer any time you do a layout serialization, you instead keep a reference to a single app-wide instance of the layout serializer and use that for each layout serialization.

## Disabling Floating Window Snap-to-Screen

By default, when a floating window is created and displayed for reasons other than a drag operation, such as when a layout containing floating windows is loaded, the floating windows will attempt to snap back onto their closest screen.  This ensures that they are fully visible to the user and prevents scenarios where the user might have moved a floating window to a location where it is no longer visible on a screen due to screen resolution changes, etc.

This feature can be disabled by setting the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[IsFloatingWindowSnapToScreenEnabled](xref:@ActiproUIRoot.Controls.Docking.DockSite.IsFloatingWindowSnapToScreenEnabled) property to `false`.
