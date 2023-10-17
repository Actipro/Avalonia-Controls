---
title: "Getting Started"
page-title: "Getting Started - Fundamentals Reference"
order: 2
---
# Getting Started

When you're ready to implement the Fundamentals product, the following steps will get you started.

## Add NuGet Package

Fundamentals is included with the `ActiproSoftware.Controls.Avalonia.Pro` package ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.Avalonia.Pro)), so add a reference to that package. This should automatically include its transient dependencies as well.

For additional information on NuGet packages, please refer to the [NuGet Packages and Feeds](../nuget.md) topic.

## Configure Themes

Controls available in the Fundamentals product rely on Actipro's themes.

> [!IMPORTANT]
> If the proper theme is not configured, Actipro controls may be empty or completely invisible in the application!

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

## Evaluate or Register a License

Fundamentals is a paid product that is included with a Pro license.  During the evaluation phase, a popup will be shown the first time most Pro controls are used.  Follow the "Hide This Prompt" steps on the prompt for details on how to temporarily suppress the prompt during evaluation.

Once a license is purchased (or a short-term evaluation license is obtained), the license must be registered during application startup to prevent the licensing prompt from being displayed.

See the section [Licensing](../licensing.md) topic for more details on licensing, including how to apply a license.