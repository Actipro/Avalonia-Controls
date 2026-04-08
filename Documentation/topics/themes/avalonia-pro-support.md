---
title: "Avalonia Pro Support"
page-title: "Avalonia Pro Support - Themes Reference"
order: 100
---
# Avalonia Pro Support

[Avalonia Pro](https://avaloniaui.net/avalonia/) is a set of commercial UI controls created by AvaloniaUI OÜ.  When these controls are used alongside Actipro's themes, their default brush resources may not match the Actipro color palette, causing visual inconsistencies.  Actipro's theme generator can override Avalonia Pro theme resources at generation time so that Avalonia Pro controls render with the same color system as all other Actipro-themed controls.

> [!IMPORTANT]
> Generating overrides of AvaloniaUI OÜ's Avalonia Pro resources requires an [Actipro Avalonia Pro](../licensing.md) license.

## Supported Avalonia Pro Products

The [AvaloniaProResourceIncludes](xref:@ActiproUIRoot.Themes.Generation.AvaloniaProResourceIncludes) flags enumeration identifies which Avalonia Pro NuGet packages should have their theme resources overridden:

| Value | NuGet Package | Description |
|-----|-----|-----|
| `None` | *(none)* | None of AvaloniaUI OÜ's Avalonia Pro resources will be overridden. This is the default. |
| `Markdown` | [Avalonia.Controls.Markdown](https://www.nuget.org/packages/Avalonia.Controls.Markdown) | Overrides resources for the `Markdown` viewer control, including hyperlinks, selection, code blocks, quote blocks, tables, alert blocks, and more. |
| `TreeDataGrid` | [Avalonia.Controls.TreeDataGrid](https://www.nuget.org/packages/Avalonia.Controls.TreeDataGrid) | Overrides resources for the `TreeDataGrid` control, including grid lines, column header states, and selected cell background. |
| `VirtualKeyboard` | [Avalonia.Controls.VirtualKeyboard](https://www.nuget.org/packages/Avalonia.Controls.VirtualKeyboard) | Overrides resources for the virtual keyboard control, including pane background, button appearances, and action button states. |

Since [AvaloniaProResourceIncludes](xref:@ActiproUIRoot.Themes.Generation.AvaloniaProResourceIncludes) is a flags enumeration, multiple values can be combined to enable overrides for several Avalonia Pro products at once.

## Configuring Avalonia Pro Resource Overrides

Avalonia Pro resource overrides are enabled by setting the [AvaloniaProResourceIncludes](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.AvaloniaProResourceIncludes) property on a [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) to one or more [AvaloniaProResourceIncludes](xref:@ActiproUIRoot.Themes.Generation.AvaloniaProResourceIncludes) values.

> [!TIP]
> The [Getting Started](getting-started.md) topic covers how to set a [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) instance onto a [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) instance.

### XAML Configuration

The following example enables resource overrides for several Avalonia Pro products in XAML:

```xaml
<Application ...
	xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
	xmlns:generation="using:ActiproSoftware.UI.Avalonia.Themes.Generation">
	<Application.Styles>

		<actipro:ModernTheme>
			<actipro:ModernTheme.Definition>
				<generation:ThemeDefinition AvaloniaProResourceIncludes="Markdown, TreeDataGrid, VirtualKeyboard" />
			</actipro:ModernTheme.Definition>
		</actipro:ModernTheme>

	</Application.Styles>
</Application>
```

To only override resources for a single product, specify just that value:

```xaml
<generation:ThemeDefinition AvaloniaProResourceIncludes="Markdown" />
```

## How It Works

The [AvaloniaProResourceIncludes](xref:@ActiproUIRoot.Themes.Generation.AvaloniaProResourceIncludes) setting tells the theme generator which resource overrides to inject into the generated theme's resource dictionaries.  Each Avalonia Pro resource override uses a value from the generated Actipro theme.

No changes to control templates are required.  Avalonia Pro controls consume their brushes from named application resources, so simply replacing those resources with Actipro-equivalent values is enough to achieve a consistent appearance.

Because the overrides are regenerated whenever the theme generates (e.g., when the active theme variant changes between light and dark), Avalonia Pro controls automatically update their appearance along with the rest of the application.

## Requirements and Licensing

Generating AvaloniaUI OÜ's Avalonia Pro resource overrides is a feature of the [Actipro Avalonia Pro](../licensing.md) license.  An attempt to use any [AvaloniaProResourceIncludes](xref:@ActiproUIRoot.Themes.Generation.AvaloniaProResourceIncludes) value other than `None` without a valid Actipro Avalonia Pro license will raise a license validation error at runtime.

Actipro Themes itself is included in the free `ActiproSoftware.Controls.Avalonia` NuGet package, but a Pro license is required specifically to unlock generation of AvaloniaUI OÜ's Avalonia Pro resources.

For licensing information, please refer to the [Licensing](../licensing.md) topic.
