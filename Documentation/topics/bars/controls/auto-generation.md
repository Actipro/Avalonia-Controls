---
title: "Label and Key Tip Generation"
page-title: "Label and Key Tip Generation - Bars Controls"
order: 210
---
# Label and Key Tip Generation

Many controls have `Key`, `Label`, and `KeyTipText` properties. Since these properties are closely related, controls can auto-generate a `Label` value based on the `Key` if no other `Label` has been explicitly set.  Likewise, a `KeyTipText` value can be auto-generated from a `Label` if no other `KeyTipText` has been expicitly set.

@if (wpf) {
Other contextual values (like certain `ICommand` types) can also be used when auto-generating property values.
}

This time-saving feature helps reduce the need to specify many `Label` and `KeyTipText` values, except in scenarios where a customized value is necessary!

> [!TIP]
> The optional MVVM Library defines multiple view models that utilize implementations of the same [ILabelGenerator](xref:@ActiproUIRoot.Controls.Bars.ILabelGenerator) and [IKeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.IKeyTipTextGenerator) interfaces to auto-generate property values that are not explicitly passed to an object's constructor, so this time-saving feature is available for MVVM configurations, too!  See the [MVVM Support](../mvvm-support.md) topic for additional details on the MVVM Library.

> [!IMPORTANT]
> `Label` and `KeyTipText` are UI-based properties that may need to be localized, but the `Key` property value should not be localized. See the "Localization" section below for additional details.

## Label Generation

Label generation is defined by the [ILabelGenerator](xref:@ActiproUIRoot.Controls.Bars.ILabelGenerator) interface with several convenience methods that can be used to automatically generate a label from various contexts.  This interface is implemented by the [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator) class, and an instance of this class is accessible, by default, from the static [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.BarControlService.LabelGenerator) property.

The [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.BarControlService.LabelGenerator) property can also be set to a custom implementation of [ILabelGenerator](xref:@ActiproUIRoot.Controls.Bars.ILabelGenerator) to extend or replace the default capabilities.

### Label from Key

The default [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator).[FromKey](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator.FromKey*) method implementation will analyze a given `Key` to determine the best label. Supported key values must *only* contain upper- or lower-ASCII letters `A-Z`, digits `0-9`, underscore (`_`), or hypen (`-`). Any other characters in the key will result in a `null` value being returned.

When generating a label, the `Key` must be split into individual words using either word break characters or changes in character casing.

#### Word Break Characters

Any `Key` that includes an underscore (`_`) or hypen (`-`) will treat those characters as word breaks and the actual word break character is ignored. Individual words are always converted to "Title Case" and separated with spaces to form the final label.

For example, the `Key` value `"font-settings"` or `"FONT_SETTINGS"` will both generate the `Label` value `"Font Settings"`.

#### Character Casing

Any `Key` without word break characters will use either "PascalCase" or "camelCase" conventions to recognize words based on changes in character casing.

For example, the `Key` values `"OpenFile"` or `"openFile"` will recognize the words "Open" and "File".

Exception for the first word in "camelCase", all recognized words will use the casing of the original word without converting to "Title Case", so the `Key` value `"ExportToXML"` will generate the label `"Export To XML"` with the original acronym casing unchanged.

> [!IMPORTANT]
> A limitation of "camelCase" is that the casing of the leading word is ambiguous between words and acrynoyms (e.g., `"toUpper"` vs. `"ioException"`), so the first word will always be converted to "Title Case".  For this reason, "PascalCase" is recommended instead.

### Label from Command

@if (avalonia) {
The default [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator).[FromCommand](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator.FromCommand*) method implementation will always return `null`, but this method can be overridden by derived classes to automatically provide labels for known commands.
}
@if (wpf) {
The default [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator).[FromCommand](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator.FromCommand*) method implementation will analyze an `ICommand` to determine the best label.
- The `ApplicationCommands.NotACommand` command is ignored and will always return a value of `null`.
- Any other `RoutedUICommand` will use the `RoutedUICommand.Text` property as the label.
- Any `RoutedCommand` will use the `RoutedCommand.Name` property as the label.

A value of `null` will be returned if a label could not be determined from the command.
}

## Key Tip Generation

Key tip generation is defined by the [IKeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.IKeyTipTextGenerator) interface with several convenience methods that can be used to automatically generate key tip text from various contexts.  This interface is implemented by the [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator) class, and an instance of this class is accessible, by default, from the static [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.BarControlService.KeyTipTextGenerator) property.

The [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.BarControlService.KeyTipTextGenerator) property can also be set to a custom implementation of [IKeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.IKeyTipTextGenerator) to extend or replace the default capabilities.

### Key Tip from Label

The default [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromLabel](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromLabel*) method implementation will analyze a string label to determine the best key tip text.  The label is analyzed against multiple rules (in the order listed below) and the first matching rule, if any, will determine the key tip text.  A value of `null` will be returned if a matching rule is not found.

| Rule | Description |
| ---- | ---- |
| Leading digits | Any label that begins with 1 or 2 digits will use those digits as the key tip text. For example, the label `"10 Column Layout"` will use `"10"` as the key tip text. |
| Significant word upper-case or digit | Any significant word (three or more characters) that starts with an upper-case letter or digit will use the leading character as the key tip text.  For example, the label `"Go To Line"` will use `"L"` as the key tip text. Even though other words start with upper-case letters, "Line" is the first *significant* word. |
| Significant word lower-case | Any significant word (three or more characters) that starts with a lower-case letter will use the leading character as the key tip text.  For example, the label `"My documents"` will use `"D"` as the key tip text. Even though "My" starts with an upper-case letter, "documents" is the more significant word by length. |
| Any word upper-case or digit | Any word that starts with an upper-case letter or digit will use the leading character as the key tip text.  For example, the label `"go to End"` will use `"E"` as the key tip text.
| Any word lower-case | Any word that starts with a lower-case letter will use the leading character as the key tip text.  For example, the label `"go up"` will use `"G"` as the key tip text.
| Everything else | No key tip text is recognized and `null` will be returned.

In the list of rules above, regular expressions are used to locate "words" where `\b` defines a word boundary and the `\w` character class defines the characters recognized as part of a word. Upper-case and lower-case letters are defined by Unicode categories `Lu` and `Ll`, respectively. Digits are defined by the `\d` character class.

### Key Tip from KeyGesture

The default [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromKeyGesture](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromKeyGesture*) method implementation will analyze a `KeyGesture` to determine the best key tip text.

If the `KeyGesture.Key` property defines a `Key` whose string representation is a single letter, that letter will be used as the key tip text.  For example, the `KeyGesture` for <kbd>Ctrl</kbd>+<kbd>V</kbd> (commonly used for the paste command), would return `"V"` as the key tip text.

A value of `null` will be returned if a value for key tip text could not be determined from the `KeyGesture`.

### Key Tip from Command

@if (avalonia) {
The default [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromCommand](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromCommand*) method implementation will always return `null`, but this method can be overridden by derived classes to automatically provide key tips for known commands.
}
@if (wpf) {
The default [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromCommand](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromCommand*) method implementation will analyze an `ICommand` to determine the best key tip text.

If the `ICommand` is a `RoutedCommand`, the `RoutedCommand.InputGestures` collection will be queried for gestures of type `KeyGesture`. The first `KeyGesture` that returns a non-`null` value when passed to [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromKeyGesture](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromKeyGesture*) will be used as the key tip text.

A value of `null` will be returned if a value for key tip text could not be determined from the `ICommand`.
}

## Localization

The most common scenario for the automatic generation of labels and key tip texts is to auto-generate a label from a given key and then use the resulting label to auto-generate the key tip text.  Since the key should not be localized, the default implementation of auto-generated values cannot be used when localized labels or key tip texts are required.  In this scenario, a custom implementation of [ILabelGenerator](xref:@ActiproUIRoot.Controls.Bars.ILabelGenerator) and/or [IKeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.IKeyTipTextGenerator) can be used.

### Localizing Label Generation

Most localization will only need to customize the label that is auto-generated from a key since the key tip generator will use the localized label when auto-generating the key tip text.

The following steps can be used to implement localized label generation:

1. Create a new class that derives from [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator).
1. Override the [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator).[FromKey](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator.FromKey*) method to return a localized label for the given key.
1. Optionally override the [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator).[FromCommand](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator.FromCommand*) method to return a localized label for the given command.@if (wpf) { This is only necessary if the `RoutedUICommand.Text` or `RoutedCommand.Name` properties are not already localized. }
1. During application startup before any UI is initialized, set the [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.BarControlService.LabelGenerator) property to an instance of the custom class.

> [!NOTE]
> As an alternative to deriving from the [LabelGenerator](xref:@ActiproUIRoot.Controls.Bars.LabelGenerator) class, a custom class can also fully implement the [ILabelGenerator](xref:@ActiproUIRoot.Controls.Bars.ILabelGenerator) interface instead.

### Localizing Key Tip Generation

While typically not required, localization may also be necessary when auto-generating key tip text from a label.

The following steps can be used to implement localized key tip generation:

1. Create a new class that derives from [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).
1. Override the [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromLabel](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromLabel*) method to return a localized key tip text for the given label.
1. Optionally override the [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator).[FromCommand](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator.FromCommand*) method to return a localized key tip text for the given command.@if (wpf) { This is only necessary if key tip text cannot be auto-generated from a `KeyGesture` defined within `RoutedCommand.InputGestures`. }
1. During application startup before any UI is initialized, set the [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.BarControlService.KeyTipTextGenerator) property to an instance of the custom class.

> [!NOTE]
> As an alternative to deriving from the [KeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.KeyTipTextGenerator) class, a custom class can also fully implement the [IKeyTipTextGenerator](xref:@ActiproUIRoot.Controls.Bars.IKeyTipTextGenerator) interface instead.
