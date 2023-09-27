---
title: "Theme Generator"
page-title: "Theme Generator - Themes Reference"
order: 15
---
# Theme Generator

A theme generator uses options in a [theme definition](theme-definitions.md) to guide it on how to create theme resources like brushes, thicknesses, etc.  Distinct theme resources are generated for both `Light` and `Dark` theme variants, allowing for the theme generator to run a single time regardless of if an application toggles between light and dark modes at run-time.

## Generation Workflow

The [ThemeGenerator](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator) class is what houses the theme generator logic.  Its public [Generate](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.Generate*) method is passed a [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) and interprets the theme definition's options to output a `ResourceDictionary`.  The `ResourceDictionary` contains all the theme resources that were generated.

The [Generate](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.Generate*) method has this general workflow:

1) Multiple theme generator sessions are created.
2) Each session invokes generator methods to create resources, which are then added to the session's `ResourceDictionary`.
3) Each session calls into the generator's [OnSessionCompleted](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.OnSessionCompleted*) method where additional custom resources can optionally be added.
4) The various session `ResourceDictionary` instances are combined into a single `ResourceDictionary` hierarchy, which is what is returned by the [Generate](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.Generate*) method.

## Generator Sessions

A [ThemeGeneratorSession](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession) instance is created for a "common" theme variant, and additional [ThemeGeneratorSession](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession) instances are created for each theme variant (e.g. `Light` and `Dark`).

The [ThemeGeneratorSession](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession) class provides access to important data that is used by the generator.

| Property | Description |
|-----|-----|
| [Definition](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.Definition) | The [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) that initiated the theme generation. |
| [ThemeVariant](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.ThemeVariant) | A `ThemeVariant` indicating the target theme variant, which may be `null` for resources like thicknesses that are shared among all theme variants. |
| [IsDark](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.IsDark) | Whether the `ThemeVariant` is a dark theme variant. |
| [Palette](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.Palette) | The [ColorPalette](xref:@ActiproUIRoot.Themes.Generation.ColorPalette) that provides access to all colors. |
| [ResourceDictionary](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.ResourceDictionary) | The `ResourceDictionary` into which the theme generator session is appending resources. |

The following session properties return the resolved [ColorRamp](xref:@ActiproUIRoot.Themes.Generation.ColorRamp) instances to use for various semantic colors, as specified in the [theme definition](theme-definitions.md).

| Property | Description |
|-----|-----|
| [NeutralColorRamp](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.NeutralColorRamp) | Neutral (e.g., grayscale) colors. |
| [AccentColorRamp](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.AccentColorRamp) | Accent semantic colors. |
| [InformationColorRamp](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.InformationColorRamp) | Information semantic colors. |
| [SuccessColorRamp](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.SuccessColorRamp) | Success semantic colors. |
| [WarningColorRamp](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.WarningColorRamp) | Warning semantic colors. |
| [DangerColorRamp](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession.DangerColorRamp) | Danger semantic colors. |

## Resource Types

Many types of resources can be generated.  Each of the resource types has a [ThemeGenerator](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator) method that is invoked to create the resource value for the specified [ThemeResourceKind](xref:@ActiproUIRoot.Themes.ThemeResourceKind).

The following table shows all of the resource types that are generated, along with the related protected virtual method invoked on the [ThemeGenerator](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator) class.

| Resource Type | Related Method |
|-----|-----|
| `Boolean` | [GetBooleanResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetBooleanResource*) |
| `BoxShadow` | [GetBoxShadowResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetBoxShadowResource*) |
| `Brush` | [GetBrushResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetBrushResource*) |
| `Char` | [GetCharResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetCharResource*) |
| `Color` | [GetColorResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetColorResource*) |
| `CornerRadius` | [GetCornerRadiusResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetCornerRadiusResource*) |
| `Double` | [GetDoubleResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetDoubleResource*) |
| `FontFamily` | [GetFontFamilyResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetFontFamilyResource*) |
| `FontWeight` | [GetFontWeightResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetFontWeightResource*) |
| `Thickness` | [GetThicknessResource](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.GetThicknessResource*) |

## Creating a Customized ThemeGenerator

The [ThemeGenerator](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator) class has numerous methods that are protected virtual and can be overridden for extensibility purposes, such as for adjusting specific predefined resource values or appending custom resources.  If this level of extensibility is necessary, create a class that inherits [ThemeGenerator](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator).

By default, an instance of [ThemeGenerator](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator) is used to generate theme resources.  By setting the [ThemeDefinition.Generator](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.Generator) property to an instance of a custom class that derives [ThemeGenerator](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator), the theme resource generation logic can be tailored for your needs.

The following example shows how a `CustomThemeGenerator` class can be created and installed in a [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) for use.

```csharp
public class CustomThemeGenerator : ThemeGenerator {

	// NOTE: Implement appropriate method overrides here to customize
	//       theme resource generation logic

}
```

```xaml
<Application ... xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui">
	<Application.Styles>

		<actipro:ModernTheme>
			<actipro:ModernTheme.Definition>
				<actipro:ThemeDefinition>
					<actipro:ThemeDefinition.Generator>
						<!-- Custom generator logic installed -->
						<local:CustomThemeGenerator />
					</actipro:ThemeDefinition.Generator>
				</actipro:ThemeDefinition>
			</actipro:ModernTheme.Definition>
		</actipro:ModernTheme>

	</Application.Styles>
</Application>
```

## Customizing Generated Resources

After following the instructions above to create a customized theme generator, override any of the methods listed in the "Resource Types" table above to customize generated resources.

For instance, the following logic could be implemented to alter the default checkerboard brushes:

```csharp
protected override Brush? GetBrushResource(ThemeGeneratorSession session, ThemeResourceKind resourceKind) {
	return resourceKind switch {
		ThemeResourceKind.CheckerboardPrimaryBrush => new SolidColorBrush(Colors.Silver),
		ThemeResourceKind.CheckerboardSecondaryBrush => new SolidColorBrush(Colors.Gray),

		_ => base.GetBrushResource(session, resourceKind)
	};
}
```

## Appending Custom Resources

Once a [ThemeGeneratorSession](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession) has had all of its resources generated, it calls the [ThemeGenerator.OnSessionCompleted](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.OnSessionCompleted*) method, which is a protected internal method that is passed the [ThemeGeneratorSession](xref:@ActiproUIRoot.Themes.Generation.ThemeGeneratorSession) instance.

As an extensibility point, after following the instructions above to create a customized theme generator, override the [ThemeGenerator.OnSessionCompleted](xref:@ActiproUIRoot.Themes.Generation.ThemeGenerator.OnSessionCompleted*) method to append any custom resources you wish into the session.  For instance, the following example shows how to add a new custom `Brush` resource with `Light` and `Dark` variations.

```csharp
protected override void OnSessionCompleted(ThemeGeneratorSession session) {
	// If the session is for a specific theme variant (not the "common" session)...
	if (session.ThemeVariant is not null) {
		// Add a custom "MyBrush" resource brush
		session.ResourceDictionary.Add("MyBrush",
			new SolidColorBrush(session.IsDark ? Colors.LightBlue : Colors.DarkBlue));
	}
}
```
