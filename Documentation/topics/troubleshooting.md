---
title: "Troubleshooting"
page-title: "Troubleshooting"
order: 45
---
# Troubleshooting

This topic provides several tips on common questions or issues that you may encounter while using Actipro Avalonia UI controls.

## Actipro Controls are Blank, Transparent or Missing Content

If a control does not appear the way it should (especially if it is blank), this can be caused by missing themes.  Actipro Avalonia UI controls require the [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) to be loaded for control templates and resources to be available for the controls.

If [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) is loaded but controls from the `ActiproSoftware.Controls.Avalonia.Pro` NuGet package are still not rendering properly (like the [Fundamentals](fundamentals/index.md) product controls), make sure [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme).[Includes](xref:@ActiproUIRoot.Themes.ModernTheme.Includes) property includes the [Pro](xref:@ActiproUIRoot.Themes.ThemeStyleIncludes.Pro) controls.

See the "Using Actipro Themes" section of the [Themes Getting Started](themes/getting-started.md) topic for more details on loading the theme, including sample code.

## Native Avalonia Controls Missing Actipro Themes

If native themes are missing for basic controls (like `TextBox`), make sure the [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) is loaded since it defines the control templates and resources used for native controls.

If the [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) is loaded but native controls are still not properly themed, verify that the [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme) is loaded *after* any other themes since the last loaded theme will override the themes defined before it.  Also make sure that the [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme).[AreNativeControlThemesEnabled](xref:@ActiproUIRoot.Themes.ModernTheme.AreNativeControlThemesEnabled) property is *not* set to `false` as this would prevent the themes from being loaded.

If basic native controls are themed and native `ColorPicker` or `DataGrid` are not, you may be missing a NuGet package and/or theme include setting.

- Be sure to add a reference to the corresponding `ActiproSoftware.Controls.Avalonia.Themes.ColorPicker` and/or `ActiproSoftware.Controls.Avalonia.Themes.DataGrid` NuGet package to ensure control templates are available.
- When loading [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme), make sure the [ModernTheme](xref:@ActiproUIRoot.Themes.ModernTheme).[Includes](xref:@ActiproUIRoot.Themes.ModernTheme.Includes) property defines the [NativeColorPicker](xref:@ActiproUIRoot.Themes.ThemeStyleIncludes.NativeColorPicker) and/or [NativeDataGrid](xref:@ActiproUIRoot.Themes.ThemeStyleIncludes.NativeDataGrid) controls.

See the "Using Actipro Themes" section of the [Themes Getting Started](themes/getting-started.md) topic for more details on loading the theme, including sample code.

## Some String Resource Customizations Don't Take Effect in UI

If you run into a scenario where some string resource customizations do take effect in the UI but others don't, the problem is most likely the location of your string customization code.

As indicated by a note in the [Customizing String Resources](customizing-string-resources.md) topic, all string resources should be customized immediately at application startup (such as in `Application.Initialize`) and before any UI or control classes are even referenced.  The note in the topic gives more detail on why this is necessary.

## Data Binding Errors at Run-Time

Sometimes there may be some data binding errors that show up in the Visual Studio console window when executing an application that uses an Actipro Avalonia UI control product.  Actipro Avalonia UI has some very large and complex templates for its products' controls and these error messages may show up in the Visual Studio console due to the timing between data binding resolution and visual tree creation.

It is very important to note that the data binding errors are NOT problems in our code.  If they were, the bindings would not work at all at run-time and you would see broken UI functionality.  This is not the case, everything works correctly at run-time after the visual tree has been fully constructed and the bindings have been re-evaluated.  The Avalonia team plans on refactoring the data binding system in the future to prevent these misleading error messages from being logged.

So just to reiterate, the data binding error messages are not problems with our code, and are simple warnings due to data bindings trying to resolve themselves before the targets' visual trees are created.  You may safely ignore these error messages.

## WebAssembly (WASM) Performance Issues

Avalonia UI supports running in the browser with WebAssembly. As of v11.0, the Avalonia UI framework documention is clear to indicate the functionality is not ready for production.  During testing with Actipro Avalonia UI controls in a browser, you may notice less-than-desired performance compared to running natively. This is expected to improve with .NET 8.