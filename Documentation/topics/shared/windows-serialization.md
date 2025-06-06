---
title: "Serialization"
page-title: "Serialization - Shared Library Reference"
order: 11
---
# Serialization

@if (avalonia) {
The [ActiproSoftware.Windows.Serialization](xref:@ActiproUIRoot.Serialization) namespace contains several classes that are helpful for persisting hierarchies of data (such as for control layouts) in XML.
}
@if (wpf) {
The [ActiproSoftware.Windows.Serialization](xref:@ActiproUIRoot.Serialization) namespace contains several classes that are helpful for serializing objects to XAML and persisting hierarchies of data (such as for control layouts) in XML.
}

@if (wpf) {
## Using XamlSerializer to Save/Load Objects from XAML

The [XamlSerializer](xref:@ActiproUIRoot.Serialization.XamlSerializer) class provides helper methods for easily serializing objects to and deserializing objects from XAML.

The [XamlSerializer](xref:@ActiproUIRoot.Serialization.XamlSerializer) class has these important members:

| Member | Description |
|-----|-----|
| [LoadFromFile](xref:@ActiproUIRoot.Serialization.XamlSerializer.LoadFromFile*) Method | Deserializes an object from the specified file. |
| [LoadFromStream](xref:@ActiproUIRoot.Serialization.XamlSerializer.LoadFromStream*) Method | Deserializes an object from the specified `Stream`. |
| [LoadFromString](xref:@ActiproUIRoot.Serialization.XamlSerializer.LoadFromString*) Method | Deserializes an object from the specified XAML string. |
| [LoadFromXmlReader](xref:@ActiproUIRoot.Serialization.XamlSerializer.LoadFromXmlReader*) Method | Deserializes an object from the specified `XmlReader`. |
| [SaveToFile](xref:@ActiproUIRoot.Serialization.XamlSerializer.SaveToFile*) Method | Serializes the specified object to XAML within a file. |
| [SaveToStream](xref:@ActiproUIRoot.Serialization.XamlSerializer.SaveToStream*) Method | Serializes the specified object to XAML within a `Stream`. |
| [SaveToString](xref:@ActiproUIRoot.Serialization.XamlSerializer.SaveToString*) Method | Serializes the specified object to a XAML string. |
| [SaveToXmlWriter](xref:@ActiproUIRoot.Serialization.XamlSerializer.SaveToXmlWriter*) Method | Serializes the specified object to XAML by using an `XmlWriter`. |

This sample code shows how to use the [SaveToString](xref:@ActiproUIRoot.Serialization.XamlSerializer.SaveToString*) method to serialize an object named `myobject` to XAML:

```csharp
string xaml = new XamlSerializer().SaveToString(myobject);
```

This sample code shows how to use the [LoadFromString](xref:@ActiproUIRoot.Serialization.XamlSerializer.LoadFromString*) method to later deserialize the from the XAML string:

```csharp
object myobject = new XamlSerializer().LoadFromString(xaml);
```
}

## Saving/Loading Object Hierarchies from XML

There are countless cases where it is useful to persist a hierarchy of data to XML that can be saved and reloaded later.

@if (avalonia) {
One example of this is storing the layout of a customizable control such as a Docking [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite), where the end user can customize the layout of tool windows.  The layout needs to be saved and restored between application sessions so that their customizations are kept intact.
}
@if (wpf) {
One example of this is storing the layout of a customizable control such as a [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar), where the end user can reorder and show/hide panes.  The layout needs to be saved and restored between application sessions so that their customizations are kept intact.
}

The Shared Library has a complete framework for supporting easy serialization and deserialization of a hierarchy of objects (such as layout data) to XML.  The framework uses a standard `XmlSerializer` to do the actual conversion to and from XML, but the framework has numerous extra features, such as that ability to save/load to various targets like `Stream`s, strings, etc.  It also can raise an event any time an object is serialized or deserialized, allowing you to easily store and retrieve custom data anywhere in the serialized output.

### Creating the Root Serializer

The first step in creating a serializable hierarchy is making the root serializer class.

This class should inherit the base generic [XmlSerializerBase<T, U>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) class.  The first type parameter indicates the `Type` of target object represented by the second type parameter's object `Type`.  The second type parameter indicates the `Type` of the root object that will be serialized and must inherit [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase).

@if (avalonia) {
For instance [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite)'s layout serialization class, [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer), is defined as:

```
public class DockSiteLayoutSerializer
	: XmlSerializerBase<DockSite, XmlDockSiteLayout> { ... }
```

The type [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) is the first type parameter since it is the "real" object affected by the layout, and the type [XmlDockSiteLayout](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlDockSiteLayout) is the second type parameter since it is the root object that is serialized.
}
@if (wpf) {
For instance [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar)'s layout serialization class, [NavigationBarLayoutSerializer](xref:@ActiproUIRoot.Controls.Navigation.Serialization.NavigationBarLayoutSerializer), is defined as:

```
public class NavigationBarLayoutSerializer
	: XmlSerializerBase<NavigationBar, XmlNavigationBarLayout> { ... }
```

The type [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar) is the first type parameter since it is the "real" object affected by the layout, and the type [XmlNavigationBarLayout](xref:@ActiproUIRoot.Controls.Navigation.Serialization.XmlNavigationBarLayout) is the second type parameter since it is the root object that is serialized.
}

Next there are three methods to override.  First, override [GetXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.GetXmlSerializer*) to return a standard `XmlSerializer` that specifies the `Type`s that will be serialized/deserialized.

Second, override [ApplyTo](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.ApplyTo*) with code that examines the [RootNode](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RootNode) property value and updates the passed object (like a @if (avalonia) { [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) }@if (wpf) { [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar) } instance).

Third, override [CreateRootNodeFor](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.CreateRootNodeFor*) to create the root XML node that will be serialized (like a @if (avalonia) { [XmlDockSiteLayout](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlDockSiteLayout) }@if (wpf) { [XmlNavigationBarLayout](xref:@ActiproUIRoot.Controls.Navigation.Serialization.XmlNavigationBarLayout) }) for the passed object.

### Creating the Serializable Objects

Next, create the objects that will be part of the hierarchy to serialize.  The objects must inherit [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase).  This base class provides several helper methods like converting `Point`, `Size`, and `Rect` objects to and from strings.  It also defines a [Tag](xref:@ActiproUIRoot.Serialization.XmlObjectBase.Tag) property, useful for persisting custom data via the serialization and deserialization events that are raised (see below).

The serializable objects should start with `Xml` as a naming convention and be located within a `Serialization` child namespace.

> [!NOTE]
> Use the standard XML serialization attributes on the types and members you define, such as `XmlType`, `XmlElement`, `XmlAttribute`, etc.  These attributes help control the XML output during serialization.  Remember that all public properties will be serialized.

### Serializing and Deserializing

@if (avalonia) {
The [XmlSerializerBase<T, U>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) class provides helper methods for easily serializing objects to and deserializing objects from XAML.

The [XmlSerializerBase<T, U>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) class has these important members:

| Member | Description |
|-----|-----|
| [LoadFromFile](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.LoadFromFile*) Method | Deserializes an object from the specified file. |
| [LoadFromStream](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.LoadFromStream*) Method | Deserializes an object from the specified `Stream`. |
| [LoadFromString](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.LoadFromString*) Method | Deserializes an object from the specified XML string. |
| [LoadFromXmlReader](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.LoadFromXmlReader*) Method | Deserializes an object from the specified `XmlReader`. |
| [SaveToFile](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.SaveToFile*) Method | Serializes the specified object to XML within a file. |
| [SaveToStream](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.SaveToStream*) Method | Serializes the specified object to XML within a `Stream`. |
| [SaveToString](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.SaveToString*) Method | Serializes the specified object to a XML string. |
| [SaveToXmlWriter](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.SaveToXmlWriter*) Method | Serializes the specified object to XML by using an `XmlWriter`. |

This sample code shows how to save a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) layout to an XML string:

```csharp
static DockSiteLayoutSerializer? serializer;
...
serializer ??= new DockSiteLayoutSerializer();
string layout = serializer.SaveToString(dockSite);
```

This sample code shows how to load a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) layout from the XML string:

```csharp
static DockSiteLayoutSerializer? serializer;
...
serializer ??= new DockSiteLayoutSerializer();
serializer.LoadFromString(layout, dockSite);
```
}
@if (wpf) {
The [XmlSerializerBase<T, U>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) class defines a number of methods that have the similar definitions as those described above for [XamlSerializer](xref:@ActiproUIRoot.Serialization.XamlSerializer).  This means that you can save/load from a `Stream`, string, file, etc. in one line of code.

A difference is that many of the [XmlSerializerBase<T, U>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) methods get and set its [RootNode](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RootNode) property, which stores the root [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase) object that is serialized and deserialized.

This sample code shows how to save a [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar) layout to an XML string:

```csharp
static NavigationBarLayoutSerializer? serializer;
...
serializer ??= new NavigationBarLayoutSerializer();
string layout = serializer.SaveToString(navBar);
```

This sample code shows how to load a [NavigationBar](xref:@ActiproUIRoot.Controls.Navigation.NavigationBar) layout from the XML string:

```csharp
static NavigationBarLayoutSerializer? serializer;
...
serializer ??= new NavigationBarLayoutSerializer();
serializer.LoadFromString(layout, navBar);
```
}

### Serializing/Deserializing Custom Data

A key benefit of using the Shared Library's XML serialization framework is that custom data can be inserted anywhere within the serialized data via the handling of an event in the application that calls for the serialization.

This is particularly useful when the developer calling the serialization code didn't write it and doesn't have access to change its code.

To enable insertion of custom data, create an event handler that accepts an [ItemSerializationEventArgs](xref:@ActiproUIRoot.Serialization.ItemSerializationEventArgs) argument.  Then attach the event handler to the [XmlSerializerBase<T, U>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2).[ObjectSerialized](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.ObjectSerialized) event.  When you go to serialize data, your method will be called after each object in the hierarchy is serialized.

The `Node` property in the event arguments provides the [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase) that is being serialized, and that represents the serializable data for the object indicated in the `Item` property.  You can set the `Tag` property of the `Node` to save any custom data with the serialized data.

Deserialization is the same process.  Create an event handler with the same argument type and attach it to the [XmlSerializerBase<T, U>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2).[ObjectDeserialized](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.ObjectDeserialized) event.  Your method will be called, passing the same sort of arguments, whenever an object is deserialized.  So here you can read your custom data back in.

@if (wpf) {
> [!NOTE]
> For a good example of serializing custom data, see the "NavigationBar Layout Save/Load" QuickStart.
}

### Custom Data Types

Sometimes you may be using custom data types in the data that is serialized and deserialized.  The XML serializer needs to know about custom data types so that it can properly map XML tags to .NET types.  The [XmlSerializerBase<T, U>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) has a [CustomTypes](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.CustomTypes) property that allows you to specify custom data types, thereby preventing any exceptions such as:

```
The type <YourTypeHere> was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.
```

This sample code shows how to register a `CustomData` type with the serializer, thereby preventing the above exception when performing serialization:

```csharp
serializer.CustomTypes.Add(typeof(CustomData));
```

### Optimal Memory Utilization when Using Layout Serializers

The [XmlSerializerBase<T, U>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) class uses an `XmlSerializer` as the core .NET object that reads/writes XML data.  One issue that has been discovered in Microsoft's .NET implementation is that `XmlSerializer` is capable of creating memory leaks, primarily whenever new instances of `XmlSerializer` are created.

To combat this leak, we've implemented some caching code on our end, but also highly recommend that instead of creating a new layout serializer any time you do a layout serialization, you instead keep a reference to a single app-wide instance of the layout serializer and use that for each layout serialization.