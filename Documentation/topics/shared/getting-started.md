---
title: "Getting Started"
page-title: "Getting Started - Shared Library Reference"
order: 2
---
# Getting Started

When you're ready to implement the Shared product, the following steps will get you started.

## Add NuGet Package

Shared is included with the `ActiproSoftware.Controls.Avalonia` package ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.Avalonia)), so add a reference to that package.

For additional information on NuGet packages, please refer to the [NuGet Packages and Feeds](../nuget.md) topic.

## Configure Themes

Controls available in the Shared product rely on Actipro's themes.

> [!IMPORTANT]
> If the proper theme is not configured, Actipro controls may be empty or completely invisible in the application!

Actipro's themes can be integrated by adding a special [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) class, which inherits Avalonia's `Styles` class, to an application's `Application.Styles` collection.

The [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) class has several settings that tell it which theme assets to dynamically load into the `Application`, making them available for use.  The Shared product control themes are included by default, so no additional configuration is required unless other products are also used.

The following example demonstrates defining an instance of [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) directly in `Application.Styles` in XAML with support for default controls:

```xaml
<Application
	x:Class="MyCompany.App"
	xmlns="https://github.com/avaloniaui"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
	>
	<Application.Styles>

		<!-- NOTE: Any Styles included above the ModernTheme can be overwritten by the Actipro themes -->

		<actipro:ModernTheme />

		<!-- NOTE: Any Styles included below the ModernTheme cannot be overwritten by the Actipro themes -->

	</Application.Styles>
</Application>
```

See the [Themes Getting Started](../themes/getting-started.md) topic for additional details on working with themes, including how to include non-default control themes.