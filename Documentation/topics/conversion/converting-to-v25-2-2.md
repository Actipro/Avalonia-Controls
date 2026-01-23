---
title: "Converting to v25.2.2"
page-title: "Converting to v25.2.2 - Conversion Notes"
order: 95
---
# Converting to v25.2.2

## WindowTitleBar and OffScreenMargin

The [WindowTitleBar](../shared/controls/chromed-title-bar.md) typically hides the default window chrome to extend the client area (including the custom titlebar) into the native titlebar area.  The `Window.OffScreenMargin` property is used on maximized windows, particularly those with the client area extended into the title bar, to ensure there is enough padding around the window content to avoid having it displayed off screen.

A new [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar).[CanConfigureWindowOffScreenMargin](xref:@ActiproUIRoot.Controls.WindowTitleBar.CanConfigureWindowOffScreenMargin) property has been added (which defaults to `true`).  When enabled and the [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) is hosted on a `Window`, the `Window.OffScreenMargin` property is automatically configured with a one-way binding to the `Window.Padding` property.  As long as `Window.Padding` is used in the template of a `Window` control, any `Window` that uses a [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) will, by default, display correctly when maximized.

When converting to this version, any explit use of the `Window.OffScreenMargin` property on a `Window` that also uses [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar) should be tested to ensure proper layout when maximized.  In most scenarios, the use of `OffScreenMargin` can be removed.  In some cases, this new feature may need to be disabled by setting [WindowTitleBar](xref:@ActiproUIRoot.Controls.WindowTitleBar).[CanConfigureWindowOffScreenMargin](xref:@ActiproUIRoot.Controls.WindowTitleBar.CanConfigureWindowOffScreenMargin) to `false`.

## WindowControl Active Border

In previous releases, the [WindowControl](../fundamentals/controls/window-control.md) `BorderBrush` property was set to an accent color when the control was active.  This could be distracting in some scenarios (like showing a [user prompt](../fundamentals/user-prompt/index.md) as an overlay) where there is only a single  window at a time.  With this release, the accent border is no longer applied by default when active.

The following style can be used to restore the accent brush when active:

```xaml
<Style Selector="actipro|WindowControl:active">
	<Setter Property="BorderBrush" Value="{actipro:ThemeResource ControlBackgroundBrushSolidAccent}" />
</Style>
```

Define the style in `Application.Resources` to make it the default, or define in a control's `Styles` collection (e.g., `Canvas.Styles`) to have it applied only to children of that control.

## Serialization Updates

The serialization framework primarily used by the Docking & MDI product's [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer) has been updated to work without using `XmlSerializer`.  The `XmlSerializer` class uses a reflection-based approach to determine which properties to serialize, and reflection is not compatible with ahead-of-time (AOT) compilation and trimming.  To facility support for AOT and trimming, it has been replaced by a custom serialzer that uses a declarative approach to property discovery while also using the same XML format as `XmlSerializer`.

> [!NOTE]
> Only applications that serialize *custom data* or have classes which derive from the built-in serializer classes will require any of the changes outlined below.  Any previously serialized layout data can be deserialized using the updated serializer.

The following changes were made to [XmlSerializerBase\<T,U\>](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2), which is the base class for [DockSiteLayoutSerializer](xref:@ActiproUIRoot.Controls.Docking.Serialization.DockSiteLayoutSerializer):
- The `CustomTypes` property has been replaced by a [RegisterType](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RegisterType*) method.  Instead of adding a `Type` to the `CustomTypes` collection, pass the type to the [RegisterType](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RegisterType*) method instead.
- The [GetXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.GetXmlSerializer*) method signature has changed.
  - It is no longer `abstract` and provides a default implementation that should satisfy most scenarios.
  - The primary reason this method was previously `abstract` was to allow the `XmlSerializer` to be initialized with custom types. All types used during serialization are now defined using the [RegisterType](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RegisterType*) method, and all of the registered types are passed through a new `registeredTypes` parameter (of type `IEnumerable<Type>`).  This allows the default implementation of the method to fully initialize the `XmlSerializer` without requiring an override.
  - Any custom types that were previously added to `XmlSerializer` in the [GetXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.GetXmlSerializer*) method should, instead, be passed to the [RegisterType](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RegisterType*) method from within the constructor of the class.  See note below about additional configuration necessary for declaring serialized properties.
  - After the above changes, any custom classes that override the [GetXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.GetXmlSerializer*) method should reassess if the override is still necessary.

> [!IMPORTANT]
> Any classes that are passed to the [RegisterType](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.RegisterType*) method (which includes types that were previously added to the `CustomTypes` collection or added to `XmlSerializer` in an override of the [GetXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.GetXmlSerializer*) method) require additional configuration to support the new default serializer since reflection cannot be used to determine which properties to serialize.
>
> Refer to the [Serialization](../shared/windows-serialization.md) topic for details on how to define which properties are serialized for custom types.

> [!TIP]
> We recommend all users move away from relying on `XmlSerializer` logic for custom data serialization and adopt the new declarative approach.  If necessary, you can enable an `XmlSerializer`-based solution by calling the [EnableXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.EnableXmlSerializer*) method.  When enabled, reflection will still be used to discover serializable properties and additional configuration of properties on registered types is not necessary.

> [!WARNING]
> The [EnableXmlSerializer](xref:@ActiproUIRoot.Serialization.XmlSerializerBase`2.EnableXmlSerializer*) method is intended to support backwards compatibility and provide an opportunity for developers to choose the best time to migrate to the new approach.  We plan to deprecate this method and all `XmlSerializer` support in a future release. Please contact support to resolve any conditions with the new serializer that may be blocking a transition away from `XmlSerializer`.