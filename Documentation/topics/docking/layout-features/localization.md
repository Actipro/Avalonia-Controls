---
title: "Localization"
page-title: "Localization - Docking & MDI Layout, Globalization, and Accessibility Features"
order: 10
---
# Localization

All strings that are displayed in the user interface can be customized and localized. @if (wpf) { In addition, all user interface text properties have the `Localizability` attribute applied to them. }

## String Resources

Many controls utilize built-in strings as part of their presentation, and these strings are defined as resources which can be customized.

@if (avalonia) {
Docking & MDI string resources are customized using the `ActiproSoftware.Properties.Docking.SR` class and an enumeration of available resource names is available using `ActiproSoftware.Properties.Docking.SRName`.

The following example demonstrates how one might customize the `UIToolWindowContainerToggleAutoHideButtonText` string resource:

```csharp
ActiproSoftware.Properties.Docking.SR.SetCustomString(ActiproSoftware.Properties.Docking.SRName.UIToolWindowContainerToggleAutoHideButtonText, "Hide");
```

See the [Customizing String Resources](../../customizing-string-resources.md) topic for additional details.
}
@if (wpf) {
All strings that are displayed in the user interface are available from static methods on the `ActiproSoftware.Products.Docking.SR` class.  Use that class to customize string resources as well.

> [!TIP]
> String resource customization is described in great detail in the general [Customizing String Resources](../../customizing-string-resources.md) topic.  Please see that topic for in-depth information related to localization of string resources.

An enumeration named `SRName` includes a list of all the string resource names.  A XAML markup extension named `SRExtension` is available for use within XAML templates to access string resources.

}

@if (wpf) {

## Localizable Text Properties

The following text properties are flagged as localizable via the `Localizability` attribute:

- [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Description](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Description)

- [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[TabText](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.TabText)

- [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[TabToolTip](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.TabToolTip)

- [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Title](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Title)

- [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[FloatingWindowTitle](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowTitle)

- [WindowControl](xref:@ActiproUIRoot.Controls.Docking.WindowControl).[Title](xref:@ActiproUIRoot.Controls.Docking.WindowControl.Title)

}
