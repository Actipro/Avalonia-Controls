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

One example of this is storing the layout of a customizable control such as a Docking & MDI [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite), where the end user can customize the layout of tool windows.  The layout needs to be saved and restored between application sessions so that their customizations are kept intact.

@if (avalonia) {
The Shared Library has a complete framework for supporting easy serialization and deserialization of a hierarchy of objects (such as layout data) to XML.  The framework uses a custom serializer to do the actual conversion to and from XML and has numerous extra features, such as the ability to save/load to various targets like `Stream`, `string`, etc.  It can also raise an event any time an object is serialized or deserialized, allowing you to easily store and retrieve custom data anywhere in the serialized output.
}
@if (wpf) {
The Shared Library has a complete framework for supporting easy serialization and deserialization of a hierarchy of objects (such as layout data) to XML.  The framework uses a standard `XmlSerializer` to do the actual conversion to and from XML, but the framework has numerous extra features, such as the ability to save/load to various targets like `Stream`, `string`, etc.  It can also raise an event any time an object is serialized or deserialized, allowing you to easily store and retrieve custom data anywhere in the serialized output.
}

### Creating the Root Serializer

The first step in creating a serializable hierarchy is making the root serializer class.

This class should inherit the base generic class [XmlSerializerBase<TObj, TXmlObj>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2).  The first type parameter (`TObj`) indicates the `Type` of target object represented by the second type parameter's object `Type`.  The second type parameter (`TXmlObj`) indicates the `Type` of the root object that will be serialized and must inherit [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase).

For instance [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite)'s layout serialization class, [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer), is defined as:

```csharp
public class DockSiteLayoutSerializer
	: XmlSerializerBase<DockSite, XmlDockSiteLayout> { ... }
```

The type [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) is the first type parameter (`TObj`) since it is the "real" object affected by the layout, and the type [XmlDockSiteLayout](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlDockSiteLayout) is the second type parameter (`TXmlObj`) since it is the root object that is serialized.

The [RootNode](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RootNode) property stores the root object that will be serialized, and the serializer is responsible for translating properties between the "real" object and the [RootNode](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RootNode).

During serialization, the properties of the "real" object must be transferred to the object that will be serialized and assigned to the [RootNode](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RootNode) property.  Override the [CreateRootNodeFor](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.CreateRootNodeFor*) method to create the root XML node that will be serialized (like an [XmlDockSiteLayout](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlDockSiteLayout)) for the passed object.

During deserialization, the properties of the deserialized object (defined by the [RootNode](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RootNode) property) must be transferred to the "real" object. Override the [ApplyTo](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.ApplyTo*) method with code that examines the [RootNode](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RootNode) property value and updates the passed object (like a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) instance).

The following expands on the example of the [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer) to include some of the additional configuration:

@if (avalonia) {
```csharp
public class DockSiteLayoutSerializer
	: XmlSerializerBase<DockSite, XmlDockSiteLayout> {

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public DockSiteLayoutSerializer(XmlDockSiteLayout? layout = null) : base(layout) {
		// Optionally register serializable objects used by this serializer
	}

	/// <summary>
	/// Applies the information contained within this serializer to the specified object.
	/// </summary>
	/// <param name="obj">The object to update with deserialized information.</param>
	public override void ApplyTo(DockSite obj) {
		if (RootNode == null)
			return;

		// Copy the XmlDockSiteLayout properties from the RootNode to the DockSite instance
	}

	/// <summary>
	/// Creates a root node for the specified object.
	/// </summary>
	/// <param name="obj">The object for which to create a root node.</param>
	public override XmlDockSiteLayout CreateRootNodeFor(DockSite obj) {
		var layout = new XmlDockSiteLayout();
		// Copy the DockSite properties to a new XmlDockSiteLayout object for serialization
		return layout;
	}

	...
}
```
}
@if (wpf) {
```csharp
public class DockSiteLayoutSerializer
	: XmlSerializerBase<DockSite, XmlDockSiteLayout> {

	/// <summary>
	/// Returns the <see cref="XmlSerializer"/> to use for serialization and deserialization.
	/// </summary>
	protected override XmlSerializer GetXmlSerializer() {
		// Configure serializable objects used explicitly by this serializer
		var extraTypes = new List<Type>();
		extraTypes.AddRange(new Type[] {
			typeof(XmlDockSiteLayout),
			typeof(XmlToolWindow),
			...
		});

		// Add any custom types
		if (CustomTypes.Count > 0)
			extraTypes.AddRange(CustomTypes);

		// Create a new XmlSerializer with support of the extra types
		return new XmlSerializer(typeof(XmlDockSiteLayout), extraTypes.ToArray());
	}

	/// <summary>
	/// Applies the information contained within this serializer to the specified object.
	/// </summary>
	/// <param name="obj">The object to update with deserialized information.</param>
	public override void ApplyTo(NavigationBar obj) {
		if (RootNode == null)
			return;

		// Copy the XmlDockSiteLayout properties from the RootNode to the DockSite instance
	}

	/// <summary>
	/// Creates a root node for the specified object.
	/// </summary>
	/// <param name="obj">The object for which to create a root node.</param>
	public override XmlDockSiteLayout CreateRootNodeFor(DockSite obj) {
		var layout = new XmlDockSiteLayout();
		// Copy the DockSite properties to a new XmlDockSiteLayout object for serialization
		return layout;
	}

}
```
}

### Creating the Serializable Objects

Next, create objects that will be part of the hierarchy to serialize.  The objects must inherit [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase).  This base class provides several helper methods like converting `Point`, `Size`, and `Rect` objects to and from strings (e.g., [PointToString](xref:@ActiproUIRoot.Serialization.XmlObjectBase.PointToString*) and [StringToPoint](xref:@ActiproUIRoot.Serialization.XmlObjectBase.StringToPoint*)).  It also defines a [Tag](xref:@ActiproUIRoot.Serialization.XmlObjectBase.Tag) property, useful for persisting custom data via the serialization and deserialization events that are raised (see below).

> [!TIP]
> It is recommended that serializable objects should start with `Xml` as a naming convention and be located within a `Serialization` child namespace to help differentiate them from the "real" objects they represent.

> [!NOTE]
> Use the standard XML serialization attributes on the types and members you define, such as `XmlType`, `XmlElement`, `XmlAttribute`, etc.  These attributes help control the XML output during serialization. @if (wpf) { Remember that `XmlSerializer` will serialize all public properties by default. }

@if (avalonia) {
Unless `XmlSerializer` has been enabled (which is not enabled by default), all serialized properties for a custom object must be explicitly declared.  The [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase) class defines a virtual [GetSerializerProperties](xref:@ActiproUIRoot.Serialization.XmlObjectBase.GetSerializerProperties*) method that you can override to declare any property to be serialized by that object.  This method returns an enumerable of [IXmlSerializerProperty](xref:@ActiproUIRoot.Serialization.IXmlSerializerProperty) instances that describe each property.

For convenience, this [IXmlSerializerProperty](xref:@ActiproUIRoot.Serialization.IXmlSerializerProperty) interface has been implemented on the [XmlSerializerProperty\<TSource,TProperty\>](xref:@ActiproUIRoot.Serialization.XmlSerializerProperty`2) class.  The first type argument (`TSource`) indicates the `Type` of the object which defines the property.  The second type argument (`TProperty`) defines the `Type` of value represented by the property.  The constructor for this class can be passed an expression which identifies the property.  Optionally, a delegate can also be passed which can determine, at run-time, if the property should be serialized.

The following example demonstrates some of the properties supported by the [XmlDockSiteLayout](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlDockSiteLayout) class:

```csharp
public override IEnumerable<IXmlSerializerProperty> GetSerializerProperties() {
	// Make sure properties from the base class are included
	foreach (var property in base.GetSerializerProperties())
		yield return property;

	yield return new XmlSerializerProperty<XmlDockSiteLayout, XmlAutoHideContainers?>(x => x.AutoHideContainers);
	yield return new XmlSerializerProperty<XmlDockSiteLayout, XmlObjectBase?>(x => x.Content);
	yield return new XmlSerializerProperty<XmlDockSiteLayout, List<XmlDockHost>>(x => x.DockHosts, x => x.ShouldSerializeDockHosts());
	yield return new XmlSerializerProperty<XmlDockSiteLayout, List<XmlDocumentWindow>>(x => x.DocumentWindows, x => x.ShouldSerializeDocumentWindows());
	yield return new XmlSerializerProperty<XmlDockSiteLayout, DockSiteSerializationBehavior>(x => x.SerializationFormat);
	yield return new XmlSerializerProperty<XmlDockSiteLayout, List<XmlToolWindow>>(x => x.ToolWindows, x => x.ShouldSerializeToolWindows());
	yield return new XmlSerializerProperty<XmlDockSiteLayout, int>(x => x.Version, x => x.ShouldSerializeVersion());
}
```

> [!IMPORTANT]
> When `XmlSerializer` is enabled by calling the [EnableXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.EnableXmlSerializer*) method, reflection will be used to determine properties instead of the [GetSerializerProperties](xref:@ActiproUIRoot.Serialization.XmlObjectBase.GetSerializerProperties*) method.  The `XmlSerializer` will serialize all public properties by default.
}

@if (avalonia) {
Finally, the root serializer class must be configured to recognize these custom objects.  Within the constructor of the root serializer class, call the base [RegisterType](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RegisterType*) method for each `Type` that needs to be supported.  The `Type` defined by the second type parameter (`TXmlObj` or [XmlDockSiteLayout](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlDockSiteLayout) in these examples) is automatically registered.

The following shows a partial example of how the [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer) class is configured to support serializable objects like [XmlDocumentWindow](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlDocumentWindow) and [XmlToolWindow](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlToolWindow):

```csharp
public class DockSiteLayoutSerializer
	: XmlSerializerBase<DockSite, XmlDockSiteLayout> {

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public DockSiteLayoutSerializer(XmlDockSiteLayout? layout = null) : base(layout) {
		// Register serializable objects used by this serializer
		RegisterType<XmlDocumentWindow>();
		RegisterType<XmlToolWindow>();
		...
	}

	...
}
```
}
@if (wpf) {
Finally, the root serializer class must be configured to recognize these custom objects.  The standard `XmlSerializer` used by the framework requires these types to be passed through the constructor.  Within the root serializer class, override the [GetXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.GetXmlSerializer*) method to return a new instance of `XmlSerializer` that is initialized with the supported `Type`s.  This should include any `Type` explicitly required by the custom serializer as well as any `Type` added to the [CustomTypes](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.CustomTypes) collection, but does not require the root XML object ([XmlDockSiteLayout](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlDockSiteLayout) in these examples) which is automatically included.

The following shows a partial example of how the [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer) class is configured to support serializable objects like [XmlDocumentWindow](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlDocumentWindow) and [XmlToolWindow](xref:@ActiproUIRoot.Controls.Docking.Serialization.XmlToolWindow):

```csharp
/// <summary>
/// Returns the <see cref="XmlSerializer"/> to use for serialization and deserialization.
/// </summary>
protected override XmlSerializer GetXmlSerializer() {
	// Configure serializable objects used explicitly by this serializer
	var extraTypes = new List<Type>();
	extraTypes.AddRange(new Type[] {
		typeof(XmlDocumentWindow),
		typeof(XmlToolWindow),
		...
	});

	// Add any custom types
	if (CustomTypes.Count > 0)
		extraTypes.AddRange(CustomTypes);

	// Create a new XmlSerializer with support of the extra types
	return new XmlSerializer(typeof(XmlDockSiteLayout), extraTypes.ToArray());
}
```
}

### Serializing and Deserializing

@if (avalonia) {
The [XmlSerializerBase<TObj, TXmlObj>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) class provides helper methods for easily serializing objects to and deserializing objects from XML as well as these important members:

| Member | Description |
|-----|-----|
| [LoadFromFile](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.LoadFromFile*) Method | Deserializes an object from the specified file. |
| [LoadFromStream](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.LoadFromStream*) Method | Deserializes an object from the specified `Stream`. |
| [LoadFromString](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.LoadFromString*) Method | Deserializes an object from the specified XML string. |
| [LoadFromXmlReader](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.LoadFromXmlReader*) Method | Deserializes an object from the specified `XmlReader`. |
| [SaveToFile](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.SaveToFile*) Method | Serializes the specified object to XML within a file. |
| [SaveToStream](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.SaveToStream*) Method | Serializes the specified object to XML within a `Stream`. |
| [SaveToString](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.SaveToString*) Method | Serializes the specified object to an XML string. |
| [SaveToXmlWriter](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.SaveToXmlWriter*) Method | Serializes the specified object to XML by using an `XmlWriter`. |
}
@if (wpf) {
The [XmlSerializerBase<TObj, TXmlObj>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) class defines a number of methods that have the similar definitions as those described above for [XamlSerializer](xref:@ActiproUIRoot.Serialization.XamlSerializer).  This means that you can save/load from a `Stream`, string, file, etc. in one line of code.

A difference is that many of the [XmlSerializerBase<TObj, TXmlObj>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) methods get and set its [RootNode](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RootNode) property, which stores the root [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase) object that is serialized and deserialized.
}

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

### Serializing/Deserializing Custom Data

A key benefit of using the Shared Library's XML serialization framework is that custom data can be inserted anywhere within the serialized data via the handling of an event in the application that calls for the serialization.

This is particularly useful when the developer calling the serialization code didn't write it and doesn't have access to change its code.

To enable insertion of custom data, create an event handler that accepts an [ItemSerializationEventArgs](xref:@ActiproUIRoot.Serialization.ItemSerializationEventArgs) argument.  Then attach the event handler to the [XmlSerializerBase<TObj, TXmlObj>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2).[ObjectSerialized](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.ObjectSerialized) event.  When data is serialized, your method will be called after each object in the hierarchy is serialized.

The [Node](xref:@ActiproUIRoot.Serialization.ItemSerializationEventArgs.Node) property in the event arguments provides the [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase) that is being serialized, and that represents the serializable data for the object indicated in the [Item](xref:@ActiproUIRoot.Serialization.ItemSerializationEventArgs.Item) property.  You can set the [Tag](xref:@ActiproUIRoot.Serialization.XmlObjectBase.Tag) property of the [Node](xref:@ActiproUIRoot.Serialization.ItemSerializationEventArgs.Node) to save any custom data with the serialized data.

Deserialization is a similar process.  Create an event handler with the same argument type and attach it to the [XmlSerializerBase<TObj, TXmlObj>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2).[ObjectDeserialized](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.ObjectDeserialized) event.  When an object is deserialized, your method will be called passing the same [Node](xref:@ActiproUIRoot.Serialization.ItemSerializationEventArgs.Node) and [Item](xref:@ActiproUIRoot.Serialization.ItemSerializationEventArgs.Item) arguments.  Use the [Tag](xref:@ActiproUIRoot.Serialization.XmlObjectBase.Tag) property of the [Node](xref:@ActiproUIRoot.Serialization.ItemSerializationEventArgs.Node) read your custom data back in.

@if (avalonia) {
> [!TIP]
> For a good example of serializing custom data, see the "Docking & MDI Layout Serialization" QuickStart.
}
@if (wpf) {
> [!TIP]
> For a good example of serializing custom data, see the "NavigationBar Layout Save/Load" QuickStart.
}

### Custom Data Types

Sometimes you may be using custom data types in the data that is serialized and deserialized.  The serializer needs to know about custom data types so that it can properly map XML tags to .NET types.

@if (avalonia) {
The [XmlSerializerBase<TObj, TXmlObj>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) has a [RegisterType](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RegisterType*) method that allows you to specify custom data types.  Unless `XmlSerializer` has been enabled (which is not enabled by default), the serializable properties for a custom type must also be provided when registering.  These serializable properties are defined using the same [IXmlSerializerProperty](xref:@ActiproUIRoot.Serialization.IXmlSerializerProperty) used by serializable objects that inherit from [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase).

> [!NOTE]
> If the custom data type derives from [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase), it should override the [XmlObjectBase](xref:@ActiproUIRoot.Serialization.XmlObjectBase).[GetSerializerProperties](xref:@ActiproUIRoot.Serialization.XmlObjectBase.GetSerializerProperties*) method instead of providing those properties during registration.  The type must still be registered, but the additional property data does not need to be specified (e.g., `serializer.RegisterType<CustomData>();`)

This sample code shows how to register a sample `CustomData` type with the serializer:

```csharp
serializer.RegisterType<CustomData>(GetCustomDataSerializerProperties());

IEnumerable<IXmlSerializerProperty> GetCustomDataSerializerProperties() {
	yield return new XmlSerializerProperty<CustomData, string>(x => x.MyStringProperty);
	yield return new XmlSerializerProperty<CustomData, int>(x => x.MyIntProperty, x => x.ShouldSerializeMyIntProperty());
	...
}
```
}
@if (wpf) {
The [XmlSerializerBase<TObj, TXmlObj>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) has a [CustomTypes](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.CustomTypes) property that allows you to specify custom data types, thereby preventing any exceptions such as:
```
The type <YourTypeHere> was not expected. Use the XmlInclude or SoapInclude attribute to specify types that are not known statically.
```

This sample code shows how to register a `CustomData` type with the serializer, thereby preventing the above exception when performing serialization:

```csharp
serializer.CustomTypes.Add(typeof(CustomData));
```
}

@if (avalonia) {
### Enable XmlSerializer

By default, the `XmlSerializer` is not used for serialization since it does not support AOT trimming. We recommend all users move away from relying on `XmlSerializer` logic for custom data serialization and adopt the new declarative approach.  If necessary, you can enable an `XmlSerializer`-based solution by calling the [EnableXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.EnableXmlSerializer*) method.  When enabled, reflection will be used to discover serializable properties and additional configuration of properties on registered type (using [IXmlSerializerProperty](xref:@ActiproUIRoot.Serialization.IXmlSerializerProperty)) is not necessary.

> [!WARNING]
> The [EnableXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.EnableXmlSerializer*) method is intended to support backwards compatibility and provide an opportunity for developers to choose the best time to migrate to the new approach.  We plan to deprecate this method and all `XmlSerializer` support in a future release. Please contact support to resolve any conditions with the new serializer that may be blocking a transition away from `XmlSerializer`.

> [!CAUTION]
> An issue has been discovered where Microsoft's .NET implementation of `XmlSerializer` is capable of creating memory leaks, primarily whenever new instances of `XmlSerializer` are created.
>
> To combat this leak, we've implemented some caching code on our end, but also highly recommend that instead of creating a new layout serializer any time you do a layout serialization, you instead keep a reference to a single app-wide instance of the layout serializer and use that for each layout serialization.
}
@if (wpf) {
### Optimal Memory Utilization when Using Layout Serializers

The [XmlSerializerBase<TObj, TXmlObj>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2) class uses an `XmlSerializer` as the core .NET object that reads/writes XML data.  One issue that has been discovered in Microsoft's .NET implementation is that `XmlSerializer` is capable of creating memory leaks, primarily whenever new instances of `XmlSerializer` are created.

To combat this leak, we've implemented some caching code on our end, but also highly recommend that instead of creating a new layout serializer any time you do a layout serialization, you instead keep a reference to a single app-wide instance of the layout serializer and use that for each layout serialization.
}