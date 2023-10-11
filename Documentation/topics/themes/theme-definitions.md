---
title: "Theme Definitions"
page-title: "Theme Definitions - Themes Reference"
order: 10
---
# Theme Definitions

A theme definition contains many options that guide a theme generator on how to dynamically generate theme resources.  Options cover a variety of theme aspects, including:

- Font families and base font size.
- Color ramps for accent and other semantic use scenarios (e.g., success, danger).
- Default appearance kind when there are several control theme variations (e.g., outline, solid) available for a control type, such as for buttons, switches, edit controls, etc.
- Corner radii, lengths, paddings, and margins.

> [!TIP]
> The [Getting Started](getting-started.md) topic covers how to set a [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) instance onto a [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) instance.

## Theme Definition Options

Theme definitions are represented by the [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) class, which has a wide variety of properties for directing theme generator output.

### Font Options

| Property | Description |
|-----|-----|
| [BaseFontSize](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.BaseFontSize) | The font size that is used as a basis for building font size ramps. |
| [DefaultFontFamily](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.DefaultFontFamily) | The `FontFamily` used by default. |
| [HeadingFontFamily](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.HeadingFontFamily) | The `FontFamily` used by headings. |
| [CodeFontFamily](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.CodeFontFamily) | The `FontFamily` used for code listings, typically a monospace font. |

### Color Ramp Options

Color ramp name properties should be set to [Hue](xref:@ActiproUIRoot.Themes.Generation.Hue) enumeration value names.

| Property | Description |
|-----|-----|
| [NeutralColorRampName](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.NeutralColorRampName) | The name of the neutral (e.g., grayscale) color ramp in the color palette. |
| [AccentColorRampName](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.AccentColorRampName) | The name of the accent color ramp in the color palette. |
| [InformationColorRampName](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.InformationColorRampName) | The name of the information color ramp in the color palette. |
| [SuccessColorRampName](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.SuccessColorRampName) | The name of the success color ramp in the color palette. |
| [WarningColorRampName](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.WarningColorRampName) | The name of the warning color ramp in the color palette. |
| [DangerColorRampName](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.DangerColorRampName) | The name of the danger color ramp in the color palette. |

### Menu Control Options

| Property | Description |
|-----|-----|
| [MenuItemIconColumnWidth](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.MenuItemIconColumnWidth) | The width for menu item icon columns. |
| [MenuItemPadding](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.MenuItemPadding) | The padding around menu item text. |
| [MenuItemPopupColumnWidth](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.MenuItemPopupColumnWidth) | The width for menu item popup columns. |

### Switch Control Options

| Property | Description |
|-----|-----|
| [UseAccentedSwitches](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.UseAccentedSwitches) | Whether to use the theme accent color for toggled switch controls. |
| [CheckBoxAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.CheckBoxAppearanceKind) | The [SwitchAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.SwitchAppearanceKind) that indicates the default appearance for `CheckBox` controls. |
| [CheckBoxCornerRadius](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.CheckBoxCornerRadius) | The corner radius for checkboxes. |
| [RadioButtonAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.RadioButtonAppearanceKind) | The [SwitchAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.SwitchAppearanceKind) that indicates the default appearance for `RadioButton` controls. |
| [SliderAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.SliderAppearanceKind) | The [SwitchAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.SwitchAppearanceKind) that indicates the default appearance for `Slider` controls. |
| [SwitchBorderWidth](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.SwitchBorderWidth) | The border width for checkbox and radio button controls. |
| [SwitchScale](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.SwitchScale) | The scale factor for checkbox and radio button controls. |
| [ToggleSwitchAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.ToggleSwitchAppearanceKind) | The [SwitchAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.SwitchAppearanceKind) that indicates the default appearance for `ToggleSwitch` controls. |

### Other Control Options

| Property | Description |
|-----|-----|
| [ButtonAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.ButtonAppearanceKind) | The [ButtonAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.ButtonAppearanceKind) that indicates the default appearance for various button controls (e.g., `Button`, `SplitButton`). |
| [EditAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.EditAppearanceKind) | The [EditAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.EditAppearanceKind) that indicates the default appearance for various edit controls (e.g., `ComboBox`, `TextBox`). |
| [ScrollBarThumbMargin](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.ScrollBarThumbMargin) | The margin for scroll bar thumbs. |
| [TabAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.TabAppearanceKind) | The [TabAppearanceKind](xref:@ActiproUIRoot.Themes.Generation.TabAppearanceKind) that indicates the default appearance for various tab controls (e.g., `TabControl`). |

## Custom Theme Generators

The default theme generator implementation fully generates theme resources for all supported theme variants.  Sometimes you may wish to further customize the default output of specific resource values for each theme variant.  In those cases, creation of a custom [ThemeGenerator](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator)-based class is possible.  An instance of the custom generator class can be assigned to the [ThemeDefinition.Generator](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.Generator) property.

See the [Theme Generator](theme-generator.md) topic for detail on how to create and install custom theme generator logic, and when it is appropriate to do so.

## Creating Derived Classes

It's perfectly fine to create a new instance of the [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) class and set its various properties before passing the theme definition to a [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme), as described in the [Getting Started](getting-started.md) topic.

```xaml
<Application ...
	xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
	xmlns:generation="using:ActiproSoftware.UI.Avalonia.Themes.Generation">
	<Application.Styles>

		<actipro:ModernTheme>
			<actipro:ModernTheme.Definition>
				<generation:ThemeDefinition UseAccentedSwitches="True" AccentColorRampName="Indigo" />
			</actipro:ModernTheme.Definition>
		</actipro:ModernTheme>

	</Application.Styles>
</Application>
```

If you configure multiple properties on the theme definition, or if you wish to swap in alternate theme definitions at runtime, you may wish to consider creating a custom class that inherits [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) and sets all of the properties in the constructor.

Here is a custom `MyThemeDefinition` class that demonstrates this concept:

```csharp
public class MyThemeDefinition : ThemeDefinition {

	public MyThemeDefinition() {
		UseAccentedSwitch = true;
		AccentColorRampName = Hue.Indigo.ToString();
	}

}
```

And here is how a `MyThemeDefinition` instance can be used:

```xaml
<Application ... xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui">
	<Application.Styles>

		<actipro:ModernTheme>
			<actipro:ModernTheme.Definition>
				<local:MyThemeDefinition />
			</actipro:ModernTheme.Definition>
		</actipro:ModernTheme>

	</Application.Styles>
</Application>
```

## Changing Theme Definitions

Actipro's themes include light and dark theme variants in the same theme.  Therefore, toggling between light and dark modes is simply a factor of updating the `Application.RequestedThemeVariant` property as described in the [Getting Started](getting-started.md) topic, which does not require an alternate theme definition to be used.

If your application would like to support a different theme appearance in general (e.g., different colors, font size, spacing), that is where creating alternate theme definitions is required.

Let's assume you have created two [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition)-based classes named `MyThemeDefinition` and `MySecondThemeDefinition`.  `MyThemeDefinition` could have been initialized with code as in the previous section.

Here is a custom `MySecondThemeDefinition` class that provides an alternate theme appearance with orange accents:

```csharp
public class MySecondThemeDefinition : ThemeDefinition {

	public MySecondThemeDefinition() {
		AccentColorRampName = Hue.Orange.ToString();
	}

}
```

The following example shows how the [ModernTheme.Definition](xref:@ActiproUIRoot.Themes.ModernTheme.Definition) property can be updated at runtime to a new theme definition, which will immediately update the application appearance.

```csharp
if (ModernTheme.TryGetCurrent(out var theme))
	theme.Definition = new MySecondThemeDefinition();
```
