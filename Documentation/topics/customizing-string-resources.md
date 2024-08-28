---
title: "Customizing String Resources"
page-title: "Customizing String Resources"
order: 30
---
# Customizing String Resources

## What is a String Resource?

A string resource is a text string that generally can be displayed in the user interface of our products.

For instance, when hovering the mouse over the **Show Password** button of a password textbox, a tooltip whose default text is `"Show Password"` is displayed.  The string `"Show Password"` is defined as a string resource in the Shared assembly.

All Actipro Avalonia controls that have displayable strings will have string resources defined for those items. You can view all the string resources in our products with the [String Resource Browser](utilities/string-resource-browser.md).

## Localization

The default string resources in our products are defined in US English.

Our products have a nice framework that offers you the ability to alter any string resource to use custom text.  This feature can be used in two main cases: simple changing of various strings to other strings that you prefer instead, and for localization.

In the first case, maybe you are building a US English-based application but wish to change one or two of the items to alternate text.

In the second case, you may wish to provide localization on the strings for any language based on the current culture that is running your application.  Here you would want to customize the full set of string resources for each product that has string resources.

## Getting/Setting Custom Strings with the SR Class

Products that have string resources have a class named `SR` (string resource) in their `ActiproSoftware.Properties.<ProductName>` namespace.  For instance, the Shared library product has the `ActiproSoftware.Properties.Shared.SR` class.

The `SR` class contains static methods allowing you to customize any string resources and return the resolved value of a string resource between its default value and any customizations you may have made.  Even if you haven't made any customizations, the `SR` class is ideal for returning the default string resource values via a call to its `GetString` method.

> [!IMPORTANT]
> Any string resource customization must be performed at application startup, such as in `Application.Initialize`, before any UI or controls have been referenced.  This must be done since some string resources are used by static class initializers.  Therefore, your customizations may not be picked up if not set immediately upon application startup before UI classes are referenced.

These are the important members on any `SR` class implementation:

| Member | Description |
|-----|-----|
| `ClearCustomStrings` Method | Removes all custom strings. |
| `ContainsCustomString` Method | Returns whether a custom string is defined for the specified string resource. |
| `GetCustomString` Method | Returns the custom string that is stored for the specified string resource, if any. |
| `GetString` Method | Returns the resolved value of the specified string resource.  There is an overload that takes an optional argument list.  This overload returns the resolved value of the specified string resource, by calling `String.Format` using the supplied arguments. |
| `RemoveCustomString` Method | Removes any custom string that is defined for the specified string resource |
| `SetCustomString` Method | Sets a custom string value for the specified string resource. |

This code shows how to set a custom string for the password textbox's **Show Password** button tooltip:

```csharp
using ActiproSoftware.Properties.Shared;
...
SR.SetCustomString(SRName.UITextBoxButtonShowPasswordText, "Custom Tooltip Text");
```

This code shows how to retrieve the resolved value of a string resource (including any customizations):

```csharp
using ActiproSoftware.Properties.Shared;
...
string text = SR.GetString(SRName.UITextBoxButtonShowPasswordText);
```

> [!NOTE]
> Use the [String Resource Browser](utilities/string-resource-browser.md) in the Sample Browser to browse all available string resources and easily generate string resource customization code.

## The SRName Enumeration

To make discovery and verification of string resource names even easier, each product that has string resources will also have an enumeration named `SRName` defined in the same namespace as `SR`.

The `SRName` enumeration has one value for each string resource name in the product.

## The SRExtension XAML Markup Extension

While the `SR` class makes it simple to access any resolved string resource value from code, we also wanted to provide an easy way to use resolved string resources in XAML, such as in templates for controls.  To accomplish this, we created the `SRExtension` class that is available in any product that has string resources.

The first step in using the markup extension in your XAML is to declare a namespace in your XAML root tag.  The namespace should be declared for the product whose string resources you will be accessing.  For this sample, we will access a string resource from the Shared library:

```xaml
xmlns:actiproPropertiesShared="using:ActiproSoftware.Properties.Shared"
```

Now lets use the `UITextBoxButtonShowPasswordText` resource's text as a `Button`'s tooltip content:

```xaml
<Button ToolTip.Tip="{actiproPropertiesShared:SR UITextBoxButtonShowPasswordText}" />
```

That's all there is to it!