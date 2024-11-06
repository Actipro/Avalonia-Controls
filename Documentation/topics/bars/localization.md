---
title: "Localization"
page-title: "Localization - Bars Reference"
order: 405
---
# Localization

@@PlatformName provides several built-in features for localizing applications to different locales. Bars also provides additional features and capabilities to assist in localization.

## String Resources

Many controls utilize built-in strings as part of their presentation, and these strings are defined as resources which can be customized. For example, the [Ribbon Options Button](ribbon-features/options-button.md) displays several commands whose labels are based on string resources.

@if (avalonia) {
Bars string resources are customized using the `ActiproSoftware.Properties.Bars.SR` class and an enumeration of available resource names is available using `ActiproSoftware.Properties.Bars.SRName`.

> [!NOTE]
> For those working with [MVVM](mvvm-support.md), additional string resources for that library can be customized using the `ActiproSoftware.Properties.Bars.Mvvm.SR` class and `ActiproSoftware.Properties.Bars.MVVM.SRName` enumeration.

The following example demonstrates how one might customize the `UIRibbonMenuItemAlwaysShowRibbonText` string resource:

```csharp
ActiproSoftware.Properties.Bars.SR.SetCustomString(ActiproSoftware.Properties.Bars.SRName.UIRibbonMenuItemAlwaysShowRibbonText, "Show Full Ribbon");
```
}
@if (wpf) {
Bars string resources are customized using the `ActiproSoftware.Products.Bars.SR` class and an enumeration of available resource names is available using `ActiproSoftware.Products.Bars.SRName`.

> [!NOTE]
> For those working with [MVVM](mvvm-support.md), additional string resources for that library can be customized using the `ActiproSoftware.Products.Bars.Mvvm.SR` class and `ActiproSoftware.Products.Bars.MVVM.SRName` enumeration.

The following example demonstrates how one might customize the `UIRibbonMenuItemAlwaysShowRibbonText` string resource:

```csharp
ActiproSoftware.Products.Bars.SR.SetCustomString(ActiproSoftware.Products.Bars.SRName.UIRibbonMenuItemAlwaysShowRibbonText.ToString(), "Show Full Ribbon");
```
}

See the [Customizing String Resources](../customizing-string-resources.md) topic for additional details.

## MVVM Support

Bars has extensive [MVVM Support](mvvm-support.md). While @@PlatformName provides many capabilities for localizing properties on a `DependencyObject`, the same support is not extended to other object types like those used as the base class for view models.  It is the responsibility of the application developer to localize any properties on view models and update those properties in response to run-time changes in locale.

## Generated Labels and Key Tips

Most Bars controls are defined with a string-based `Key` to identify the control, and, in many cases, that key can be used to automatically generate the corresponding `Label` and `KeyTipText` properties.  The key value should not be localized, but labels and key tips typically are.

For application developers who want to utilize auto-generated labels and key tips while still supporting localization, the built-in generator classes can be extended or fully replaced to meet individual needs.  See the [Label and Key Tip Generation](controls/auto-generation.md) topic (specifically, the "Localization" section) for additional details.

## DisplayAttribute Usage

.NET defines `System.ComponentModel.DataAnnotations.DisplayAttribute` which lets you specify localizable strings for types and members.  For supported types, this attribute can be used to assist with localization.

### Localizing BarGalleryItemViewModel\<T\> for Enum Types

If [BarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemViewModel`1) is used with an `enum` type and the corresponding [Label](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemViewModel`1.Label) property is not explicitly assigned, the `DisplayAttribute` (when present) will be used to automatically coerce a label from the `Name` or `ShortName` attribute values. This coercion will happen each time the property is accessed.

> [!IMPORTANT]
> Since the `Label` property is coerced when it is accessed, any change in locale that would result in the `Label` being coerced to a different value will not raise the corresponding `INotifyPropertyChanged.PropertyChanged` event which informs the UI to be updated.  Either explicly assign the `Label` property a new value based on the current locale or use [EnumBarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1) for additional support to refresh properties when the current locale changes.

### Localizing EnumBarGalleryItemViewModel\<T\>

The [EnumBarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1) class derives from [BarGalleryItemViewModel\<T\>](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarGalleryItemViewModel`1) and is recommended for working with `enum` types.  The corresponding [CreateCollection](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1.CreateCollection*) method can be used to automatically generate a collection of view models for each declared `enum` value on a type and uses `DisplayAttribute` (when present) to populate several properties.

> [!IMPORTANT]
> Since these properties are assigned at the time of creation, you must call [RefreshFromAttributes](xref:@ActiproUIRoot.Controls.Bars.Mvvm.EnumBarGalleryItemViewModel`1.RefreshFromAttributes*) to re-evaluate `DisplayAttribute` values and update related properties if the locale changes. Any property whose value changes because of the refresh will raise the corresponding `INotifyPropertyChanged.PropertyChanged` event.