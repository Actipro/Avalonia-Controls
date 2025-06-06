---
title: "Getting Started"
page-title: "Getting Started - Docking & MDI Reference"
order: 2
---
# Getting Started

Getting up and running with Docking & MDI controls is extremely easy.

This topic's information will assume you are using Visual Studio to write your XAML code for control that will contain docking windows and/or MDI.

@if (avalonia) {

## Add NuGet Package

Docking &amp; MDI is included with the `ActiproSoftware.Controls.Avalonia.Pro` package ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.Avalonia.Pro)), so add a reference to that package. This should automatically include its transient dependencies as well.

For additional information on NuGet packages, please refer to the [NuGet Packages and Feeds](../nuget.md) topic.

}
@if (wpf) {

## Add Assembly References

First, add references to the *ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll* and *ActiproSoftware.Docking.@@PlatformAssemblySuffix.dll* assemblies.  They should have been installed in the GAC during the control installation process.  However, they also will be located in the appropriate *Program Files* folders.  See the product's Readme for details on those locations.

}

@if (avalonia) {

## Configure Themes

Controls available in the Docking &amp; MDI product rely on Actipro's themes.

> [!IMPORTANT]
> If the proper theme is not configured, Actipro controls may be empty, be completely invisible in the application, or raise exceptions about missing template parts!

Actipro's themes can be integrated by adding a special [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) class, which inherits Avalonia's `Styles` class, to an application's `Application.Styles` collection.

The [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) class has several settings that tell it which theme assets to dynamically load into the `Application`, making them available for use.  By default, the "Pro" control themes are not loaded and must be explicitly included when defining the theme.

The following example demonstrates defining an instance of [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) directly in `Application.Styles` in XAML with support for Pro controls:

```xaml
<Application
	x:Class="MyCompany.App"
	xmlns="https://github.com/avaloniaui"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
	>
	<Application.Styles>

		<!-- NOTE: Any Styles included above the ModernTheme can be overwritten by the Actipro themes -->

		<actipro:ModernTheme Includes="Pro" />

		<!-- NOTE: Any Styles included below the ModernTheme cannot be overwritten by the Actipro themes -->

	</Application.Styles>
</Application>
```

See the [Themes Getting Started](../themes/getting-started.md) topic for additional details on working with themes.

}

@if (avalonia) {

## Evaluate or Register a License

Docking is a paid product that is included with a Pro license.  During the evaluation phase, a popup will be shown the first time most Pro controls are used.  Follow the "Hide This Prompt" steps on the prompt for details on how to temporarily suppress the prompt during evaluation.

Once a license is purchased (or a short-term evaluation license is obtained), the license must be registered during application startup to prevent the licensing prompt from being displayed.

See the section [Licensing](../licensing.md) topic for more details on licensing, including how to apply a license.

}

## Getting Started with Docking & MDI

@if (avalonia) {

This `xmlns` declaration in your root XAML control allows access to the various controls in this product:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
```

This code shows the base XAML that you can use to create a simple docking window layout with several windows:

```xaml
<actipro:DockSite>
	<actipro:SplitContainer>
		<actipro:Workspace>
			<actipro:TabbedMdiHost>
				<actipro:TabbedMdiContainer>
					<actipro:DocumentWindow Title="Document1.txt" Description="Text document" FileName="Document1.rtf">
						<TextBox BorderThickness="0" TextWrapping="Wrap" Text="This is a document window." />
					</actipro:DocumentWindow>
				</actipro:TabbedMdiContainer>
			</actipro:TabbedMdiHost>
		</actipro:Workspace>
		<actipro:ToolWindowContainer>
			<actipro:ToolWindow Title="Tool Window 1" />
			<actipro:ToolWindow Title="Tool Window 2" />
		</actipro:ToolWindowContainer>
	</actipro:SplitContainer>
</actipro:DockSite>
```

This code shows the base XAML that you can use to create a simple docking window layout that leverages MVVM support:

```xaml
<actipro:DockSite
	DocumentItemsSource="{Binding DocumentItems}"
	DocumentItemContainerTheme="..."
	ToolItemsSource="{Binding ToolItems}"
	ToolItemContainerTheme="...">
	<actipro:Workspace>
		<actipro:TabbedMdiHost />
	</actipro:Workspace>
</actipro:DockSite>
```

}
@if (wpf) {

This `xmlns` declaration in your root XAML control allows access to the various controls in this product:

```xaml
xmlns:docking="http://schemas.actiprosoftware.com/winfx/xaml/docking"
```

This code shows the base XAML that you can use to create a simple docking window layout with several windows:

```xaml
<docking:DockSite>
	<docking:SplitContainer>
		<docking:Workspace>
			<docking:TabbedMdiHost>
				<docking:TabbedMdiContainer>
					<docking:DocumentWindow Title="Document1.txt" Description="Text document" FileName="Document1.rtf">
						<TextBox BorderThickness="0" TextWrapping="Wrap" Text="This is a document window." />
					</docking:DocumentWindow>
				</docking:TabbedMdiContainer>
			</docking:TabbedMdiHost>
		</docking:Workspace>
		<docking:ToolWindowContainer>
			<docking:ToolWindow Title="Tool Window 1" />
			<docking:ToolWindow Title="Tool Window 2" />
		</docking:ToolWindowContainer>
	</docking:SplitContainer>
</docking:DockSite>
```

This code shows the base XAML that you can use to create a simple docking window layout that leverages MVVM support:

```xaml
<docking:DockSite DocumentItemsSource="{Binding DocumentItems}" DocumentItemContainerStyle="..."
			ToolItemsSource="{Binding ToolItems}" ToolItemContainerStyle="...">
	<docking:Workspace>
		<docking:TabbedMdiHost />
	</docking:Workspace>
</docking:DockSite>
```

}

Several ways of opening the windows are shown in the MVVM examples found in our Sample Browser.

@if (wpf) {

## The Visual Studio Item Templates

If you have Visual Studio, several item templates named "Docking & Standard MDI Window (WPF)", "Docking & Standard MDI Page (WPF)", "Docking & Tabbed MDI Window (WPF)", "Docking & Tabbed MDI Page (WPF)", "Docking Inner Fill Window (WPF)", and "Docking Inner Fill Page (WPF)" should have been installed during the WPF Studio installation procedure.

When you wish to create a new `Window` or `Page` with a tabbed MDI, standard MDI, or inner fill in your application, simply choose **Add New Item** in Visual Studio and select the appropriate item template.  A `DockSite` with several tool and documents already defined will be added to your project and opened.

The use of item templates is the fastest way to get started with our products in your own applications.

}

## Further Study

There are fair number of controls and concepts to understand when working with this product.  We encourage you to, at a minimum, read through the [Term Definitions](term-definitions.md) and [Control Hierarchy](control-hierarchy.md) topics since they cover some essential things to know.

Also, please run through the feature documentation and also look at the numerous QuickStarts located in the sample project.  The documentation is very thorough and the sample project demonstrates almost every feature of the controls.

See the @if (avalonia) { [Samples](../samples.md) }@if (wpf) { [Sample Code and QuickStarts](../quick-starts.md) } topic for more information on accessing the sample project.

If you require further assistance after looking through those, please visit our support forum for the product.