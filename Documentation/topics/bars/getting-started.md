---
title: "Getting Started"
page-title: "Getting Started - Bars Reference"
order: 2
---
# Getting Started

Getting up and running with Bars is very easy, especially with the included samples.  The [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control, in particular, provides all the Office-like ribbon user interface features right out of the box, saving you hundreds of hours of work over implementing it yourself.

## Add Assembly References

Once you are ready to add Bars controls to your own application, first add references to the *ActiproSoftware.Shared.@@PlatformAssemblySuffix.dll* and *ActiproSoftware.Bars.@@PlatformAssemblySuffix.dll* assemblies.  Customers who prefer the Model-View-ViewModel (MVVM) pattern and want to use our open source [MVVM library](mvvm-support.md) will also need to add a reference to the *ActiproSoftware.Bars.Mvvm.@@PlatformAssemblySuffix.dll* assembly.

[NuGet packages](../nuget.md) are available for all assemblies and are the recommended way to reference Actipro product assemblies.

@if (wpf) {
For customers who prefer direct references to DLL files in their .NET Framework-based applications, the WPF Controls installer will, by default, install assemblies to the GAC, as well as into a *Program Files* folder whose path is listed in the product's Readme file.
}

## Read the Documentation Topics

This documentation contains a lot of information about using the various controls to their fullest.  Whenever you have specific questions about a feature, please read through its documentation topic to search for answers.

Topics typically contain detailed discussions of features, tips, and sample code to demonstrate usage.

## Getting Started with Ribbon

See the [Controls Overview](controls/index.md) for topics related to individual controls that can be used in multiple contexts (e.g., ribbon, toolbar, or menu).  See the [Ribbon Overview](ribbon-features/index.md) for additional topics on ribbon-specific controls and features.

Ribbon is a complex control, so we encourage ribbon users to start with the sample project's "Getting Started" QuickStart Series.  The sample project includes a series of "Getting Started" QuickStart windows that walk you through the creation of a ribbon for an application window, step by step.

> [!NOTE]
> The "Getting Started" series is based on an MVVM configuration, but the concepts introduced there are still valuable insight for XAML-based configurations.  See the [XAML vs. MVVM Configuration](configuration.md) topic for more details on the configuration options.

The first step starts with the simple creation of a [RibbonWindow](xref:@ActiproUIRoot.Controls.Bars.RibbonWindow) with an empty [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control on it.  The steps proceed to build out view models and other classes to support the ribbon.  Each step builds on the previous step to introduce core concepts with minimal code changes until, by the last step, there is a fully functional ribbon.  Since the code for a ribbon implementation can get quite large, we chose to put most of our sample code in the "Getting Started" series of QuickStarts instead of in this documentation.

The "Getting Started" series is probably the best place to go for quickly getting up and running.  All the "Getting Started" windows can be run from the **Sample Browser** application and their source code (the most important thing) is located in the sample project.

> [!TIP]
> Although each "Getting Started" QuickStart focuses on adding a new piece of functionality to the ribbon and the changes for each step are described within the QuickStarts, it still may be useful to use a "text diff" application to see the exact code differences between each "Getting Started" step.

This code shows the base XAML that you can use to define an empty [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) along with the recommended [RibbonWindow](ribbon-features/ribbon-window.md) and [RibbonContainerPanel](xref:@ActiproUIRoot.Controls.Bars.RibbonContainerPanel) controls:

@if (avalonia) {
```xaml
<actipro:RibbonWindow
	xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
	...
	>
	<actipro:RibbonContainerPanel>

		<!-- Define the ribbon here -->
		<actipro:Ribbon ... >

			<!-- Additional ribbon configuration here -->

		</actipro:Ribbon>

		<!-- Define other window content here, such as a document display area -->

	</actipro:RibbonContainerPanel>
</actipro:RibbonWindow>
```
}
@if (wpf) {
```xaml
<bars:RibbonWindow
	xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
	...
	>
	<bars:RibbonContainerPanel>

		<!-- Define the ribbon here -->
		<bars:Ribbon ... >

			<!-- Additional ribbon configuration here -->

		</bars:Ribbon>

		<!-- Define other window content here, such as a document display area -->

	</bars:RibbonContainerPanel>
</bars:RibbonWindow>
```
}

## Getting Started with Toolbar

See the [Controls Overview](controls/index.md) for topics related to individual controls that can be used in multiple contexts (e.g., ribbon, toolbar, or menu).  See the [Toolbar Overview](toolbar-features/index.md) for additional topics on toolbar-specific controls and features.

This code shows sample XAML that you can use to define a standalone toolbar with buttons:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<DockPanel>

	<actipro:StandaloneToolBar DockPanel.Dock="Top" BorderThickness="0" ... >

		<!-- Define toolbar controls here -->
		<actipro:BarButton Key="Save" SmallIcon="{StaticResource SaveIcon}" Command="{Binding SaveCommand}" />
		...

	</actipro:StandaloneToolBar>

	...
</DockPanel>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<DockPanel>

	<bars:StandaloneToolBar DockPanel.Dock="Top" BorderThickness="0" ... >

		<!-- Define toolbar controls here -->
		<bars:BarButton Key="Save" SmallImageSource="/Images/Save16.png" Command="ApplicationCommands.Save" />
		...

	</bars:StandaloneToolBar>

	...
</DockPanel>
```
}

## Getting Started with Menu

See the [Controls Overview](controls/index.md) for topics related to individual controls that can be used in multiple contexts (e.g., ribbon, toolbar, or menu).  See the [Menu Overview](menu-features/index.md) for additional topics on menu-specific controls and features.

This code sample shows how to define a context menu for a `TextBox`:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<TextBox>
	<TextBox.ContextMenu>
		<actipro:BarContextMenu>

			<!-- Define menu controls here -->
			<actipro:BarMenuItem Key="Save" SmallIcon="{StaticResource SaveIcon}" Command="{Binding SaveCommand}" />
			...

		</actipro:BarContextMenu>
	</TextBox.ContextMenu>
</TextBox>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
...
<TextBox>
	<TextBox.ContextMenu>
		<bars:BarContextMenu>

			<!-- Define menu controls here -->
			<bars:BarMenuItem Key="Save" SmallImageSource="/Images/Save16.png" Command="ApplicationCommands.Save" />
			...

		</bars:BarContextMenu>
	</TextBox.ContextMenu>
</TextBox>
```
}

## Examine the Other QuickStarts

We've spent a lot of time adding numerous QuickStarts for Bars that are in the sample project.  Each QuickStart focuses on a specific feature area and provides some great code that you can look at to use in your own applications.

See the @if (avalonia) { [Samples](../samples.md) }@if (wpf) { [Sample Code and QuickStarts](../quick-starts.md) } topic for more information on accessing the sample project.

## Copy Code from the Sample Project

We encourage you to copy any code from our sample project.  It will really help you build a framework in a matter of minutes instead of hours.

> [!NOTE]
> Please note that some images in the sample project are copyrighted by Microsoft and we include them in our sample project for control demonstration purposes only!
