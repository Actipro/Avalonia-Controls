---
title: "XAML vs. MVVM Configuration"
page-title: "XAML vs. MVVM Configuration - Bars Reference"
order: 3
---
# XAML vs. MVVM Configuration

All of the Bars controls have been built from the ground up to support both XAML-based definitions and the Model-View-ViewModel (MVVM) pattern. The MVVM pattern has many benefits and is what we expect most developers will use for larger applications, but controls can also be defined fully in XAML and may be the better choice for simpler scenarios.

> [!TIP]
> Many of the QuickStart examples provided in the Sample Browser application include individual examples based on XAML or MVVM usage scenarios.

## XAML Configuration

All Bars controls can be defined directly in XAML and are designed to support common usage scenarios like data binding.

### Pros

XAML is familiar to most @@PlatformTitle developers and popular IDEs like Visual Studio provide designers that make it easy to visualize the run-time appearance of controls as their XAML is written. Newer IDEs even support hot reload of XAML definitions to modify a control at run-time and immediately see the impact of a change.  Getting started with XAML can be very quick and the entire control definition can often be contained in a single file.

### Cons

Some controls are used in multiple locations, and using XAML will typically require controls to be redefined everywhere they are used. For instance, a button that appears on a ribbon tab, a quick access toolbar, and a context menu might have to be defined three times.  Care must be taken to ensure the same `Key` value is used for each control instance and common UI-centric properties like labels, tooltips, and icons will need to be repeated and synchronized between each control instance.

@if (avalonia) {
> [!WARNING]
> Some ribbon and toolbar functionality requires cloning a control from one context (like a Ribbon group) to another (like the Quick Access Toolbar). When objects are defined in XAML, every effort is made to create a new object and copy relevant properties to the copy, but the process is not guaranteed to fully clone the control.  This process also requires the use of reflection and may encounter issues if trimming is enabled.  Properties assigned with bindings will typically only clone the current value of the `Binding` (not the `Binding` itself), so clones may not be notified when the value of a `Binding` changes.  Additionally, event handlers (like `Click` events on buttons) will not be cloned.
>
> MVVM does not have this issue since cloned objects can share the same view model.
}

## MVVM Configuration

MVVM is an extremely popular pattern used for @@PlatformTitle and one that is fully supported by Bars controls.

### Pros

View model classes are used to define common controls, and the same view model can be used in multiple places and contexts. For example, the same [BarButtonViewModel](xref:@ActiproUIRoot.Controls.Bars.Mvvm.BarButtonViewModel), when combined with appropriate template selectors, can be used to define both a [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) control in a ribbon/toolbar or a [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) control in a menu. Common properties like labels, tooltips, and icons are all defined once on the view model and bound to each control instance.

An optional open source companion MVVM library is available that implements view models for each kind of control concept, and contains template selectors that generate the appropriate control view and all the appropriate view model bindings for each usage context.

> [!TIP]
> See the [MVVM Support](mvvm-support.md) topic for more information on MVVM support and the pre-built MVVM library of view models and template selectors designed for Bars controls.

In general, the MVVM pattern also has its own inherent benefits, like unit testing without dependency on the user interface, that are not unique to Bars controls and should also be considered when making a configuration decision.

### Cons

The MVVM pattern is naturally more complex than working directly in XAML and involves multiple view model classes, template selectors, @if (avalonia) { themes, }@if (wpf) { styles, } and other classes working together to achieve the desired result.  Our companion [MVVM Library](mvvm-support.md) and extensive samples can get developers moving quickly, but configuring at least one central repository of view models for controls will be necessary.  Testing design changes usually requires running the application since a visual designer may not be able to support the MVVM configuration at design-time.

Getting started with MVVM will typically take longer than XAML even if there are long-term efficiency gains. For simple projects, MVVM benefits may not be worth the extra effort.

## Combine XAML and MVVM

Ultimately, XAML or MVVM use the same advanced Bars controls in the running application. You don't have to choose one configuration over the other.  In fact, even if the XAML configuration is preferred, the [gallery controls](controls/gallery.md) still require that the individual gallery items are implemented as view models for selection tracking in all contexts.

For example, the high-level structure of a [ribbon control](ribbon-features/index.md) could be defined in XAML without using a view model, but the [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).`ItemsSource` property (which defines the ribbon tabs) could still be bound to a collection of view models representing each tab and the controls within it.

Bars controls work great with XAML and MVVM (or both), so organizations can choose the configuration which best meets their individual needs.