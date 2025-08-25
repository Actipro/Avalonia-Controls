---
title: "Extension Methods"
page-title: "Extension Methods - Core Library Reference"
order: 13
---
# Extension Methods

Various extension methods are provided for several common .NET types.

> [!IMPORTANT]
> The `ActiproSoftware.Extensions` namespace must be imported for the extensions described below to be available.

## Assembly Extensions

The [AssemblyExtensions](xref:ActiproSoftware.Extensions.AssemblyExtensions) type contains extension methods for the `Assembly` type.

| Member | Description |
|-----|-----|
| [GetCopyrightMetadata](xref:ActiproSoftware.Extensions.AssemblyExtensions.GetCopyrightMetadata*) | Returns the assembly's copyright metadata from `AssemblyCopyrightAttribute`. |
| [GetDescriptionMetadata](xref:ActiproSoftware.Extensions.AssemblyExtensions.GetDescriptionMetadata*) | Returns the assembly's description metadata from `AssemblyDescriptionAttribute`. |
| [GetFileVersion](xref:ActiproSoftware.Extensions.AssemblyExtensions.GetFileVersion*) | Returns the assembly's file `Version` from `AssemblyFileVersionAttribute`. |
| [GetInformationalVersion](xref:ActiproSoftware.Extensions.AssemblyExtensions.GetInformationalVersion*) | Returns the assembly's informational version from `AssemblyInformationalVersionAttribute`. |
| [GetProductMetadata](xref:ActiproSoftware.Extensions.AssemblyExtensions.GetProductMetadata*) | Returns the assembly's product name metadata from `AssemblyProductAttribute`. |
| [GetTitleMetadata](xref:ActiproSoftware.Extensions.AssemblyExtensions.GetTitleMetadata*) | Returns the assembly's title metadata from `AssemblyTitleAttribute`. |

## DateTime Extensions

The [DateTimeExtensions](xref:ActiproSoftware.Extensions.DateTimeExtensions) type contains extension methods for the `DateTime` type.

| Member | Description |
|-----|-----|
| [IsToday](xref:ActiproSoftware.Extensions.DateTimeExtensions.IsToday*) | Returns whether the specified `DateTime` is today. |
| [IsWeekend](xref:ActiproSoftware.Extensions.DateTimeExtensions.IsWeekend*) | Returns whether the specified `DateTime` is on a weekend. |
| [ToString](xref:ActiproSoftware.Extensions.DateTimeExtensions.ToString*) | Converts the value of a `DateTime` object to its equivalent string representation using the specified format pattern and optional culture-specific format information. |

## DayOfWeek Extensions

The [DayOfWeekExtensions](xref:ActiproSoftware.Extensions.DayOfWeekExtensions) type contains extension methods for the `DayOfWeek` type.

| Member | Description |
|-----|-----|
| [IsWeekend](xref:ActiproSoftware.Extensions.DayOfWeekExtensions.IsWeekend*) | Returns whether the specified `DayOfWeek` is on a weekend. |
| [ToString](xref:ActiproSoftware.Extensions.DayOfWeekExtensions.ToString*) | Converts the value of a `DayOfWeek` object to its equivalent string representation using the specified format pattern and optional culture-specific format information. |

## Double Extensions

The [DoubleExtensions](xref:ActiproSoftware.Extensions.DoubleExtensions) type contains extension methods for the `Double` type.

| Member | Description |
|-----|-----|
| [ClampToNonnegative](xref:ActiproSoftware.Extensions.DoubleExtensions.ClampToNonnegative*) | Returns the value clamped to a nonnegative number greater than or equal to `0.0`. Values less than `0.0` return `0.0`. All other values are unchanged. |
| [ClampToPercentage](xref:ActiproSoftware.Extensions.DoubleExtensions.ClampToPercentage*) | Returns the value clamped to a percentage between `0.0` and `1.0` (inclusive).  Values less than `0.0` return `0.0`, and values greater than `1.0` return `1.0`. All other values are unchanged. |
| [ClampToRange](xref:ActiproSoftware.Extensions.DoubleExtensions.ClampToRange*) | Returns the value clamped between the specified minimum and maximum values.  Values less than the minimum return the minumum, and values greater than the maximum return the maximum. All other values are unchanged. |
| [IsBetween](xref:ActiproSoftware.Extensions.DoubleExtensions.IsBetween*) | Returns whether a value is between the specified minimum and maximum (inclusive or exclusive). |
| [IsCloseTo](xref:ActiproSoftware.Extensions.DoubleExtensions.IsCloseTo*) | Returns whether a value is close enough with another value to be considered equal. |
| [IsGreaterThan](xref:ActiproSoftware.Extensions.DoubleExtensions.IsGreaterThan*) | Determines whether the value is effectively greater than the comparison value. |
| [IsGreaterThanOrCloseTo](xref:ActiproSoftware.Extensions.DoubleExtensions.IsGreaterThanOrCloseTo*) | Determines whether the value is effectively greater than or equal to the comparison value. |
| [IsLessThan](xref:ActiproSoftware.Extensions.DoubleExtensions.IsLessThan*) | Determines whether the value is effectively less than the comparison value. |
| [IsLessThanOrCloseTo](xref:ActiproSoftware.Extensions.DoubleExtensions.IsLessThanOrCloseTo*) | Determines whether the value is effectively less than or equal to the comparison value. |
| [IsNonnegative](xref:ActiproSoftware.Extensions.DoubleExtensions.IsNonnegative*) | Returns if a value is `0.0` or greater. |
| [IsPercentage](xref:ActiproSoftware.Extensions.DoubleExtensions.IsPercentage*) | Returns if a value is between `0.0` and `1.0` (inclusive). |
| [IsZero](xref:ActiproSoftware.Extensions.DoubleExtensions.IsZero*) | Determines whether the specified value is close enough to zero to be considered equal. |
| [NormalizeDegreeAngle](xref:ActiproSoftware.Extensions.DoubleExtensions.NormalizeDegreeAngle*) | Returns a degree angle normalized to a value between `0.0` and `360.0`.  Negative values are converted to their equivalent positive angle (e.g., `-15.0` is normalized as `345.0`).  Values greater than `360.0` are normized to their equivalent position within the first `360.0` degrees (e.g., `460.0` is normalized as `100.0`). |
| [Round](xref:ActiproSoftware.Extensions.DoubleExtensions.Round*) | Returns a number rounded to a specified number of digits using a given [RoundMode](xref:ActiproSoftware.RoundMode). |
| [RoundToMultiple](xref:ActiproSoftware.Extensions.DoubleExtensions.RoundToMultiple*) | Returns a number rounded to the nearest multiple of another number (e.g., `13.0` rounded to the nearest multiple of `5.0` returns `15.0`). |

## Int32 Extensions

The [Int32Extensions](xref:ActiproSoftware.Extensions.Int32Extensions) type contains extension methods for the `Int32` type.

| Member | Description |
|-----|-----|
| [ClampToNonnegative](xref:ActiproSoftware.Extensions.Int32Extensions.ClampToNonnegative*) | Returns the value clamped to a nonnegative number greater than or equal to `0`. Values less than `0` return `0`. All other values are unchanged. |
| [ClampToRange](xref:ActiproSoftware.Extensions.Int32Extensions.ClampToRange*) | Returns the value clamped between the specified minimum and maximum values.  Values less than the minimum return the minumum, and values greater than the maximum return the maximum. All other values are unchanged. |
| [IsBetween](xref:ActiproSoftware.Extensions.Int32Extensions.IsBetween*) | Returns whether a value is between the specified minimum and maximum (inclusive or exclusive). |
| [IsEven](xref:ActiproSoftware.Extensions.Int32Extensions.IsEven*) | Returns whether the value is an even number, including `0`. |
| [IsNonnegative](xref:ActiproSoftware.Extensions.Int32Extensions.IsNonnegative*) | Returns if a value is `0` or greater. |
| [IsOdd](xref:ActiproSoftware.Extensions.Int32Extensions.IsOdd*) | Returns whether the value is an odd number. |
| [IsPositive](xref:ActiproSoftware.Extensions.Int32Extensions.IsPositive*) | Returns whether a value is inclusively between `1`..`Int32.MaxValue`. |
| [RoundToMultiple](xref:ActiproSoftware.Extensions.Int32Extensions.RoundToMultiple*) | Returns a number rounded to the nearest multiple of another number (e.g., `13` rounded to the nearest multiple of `5` returns `15`). |

## List Extensions

The [ListExtensions](xref:ActiproSoftware.Extensions.ListExtensions) type contains extension methods for the `IList<T>` type.

| Member | Description |
|-----|-----|
| [AddRange](xref:ActiproSoftware.Extensions.ListExtensions.AddRange*) | Adds items to the end of the list. |
| [InsertRange](xref:ActiproSoftware.Extensions.ListExtensions.InsertRange*) | Inserts items in a list, starting at a specified index. |
| [RemoveAll](xref:ActiproSoftware.Extensions.ListExtensions.RemoveAll*) | Removes all items that match a specified predicate. |

## Object Extensions

The [ObjectExtensions](xref:ActiproSoftware.Extensions.ObjectExtensions) type contains extension methods for the `Object` type.

| Member | Description |
|-----|-----|
| [TryConvertToDouble](xref:ActiproSoftware.Extensions.ObjectExtensions.TryConvertToDouble*) | Tries to convert an object to a `Double` value.  To be successful, non-`Double` values must implement `IConvertible.ToDouble`. Alternatively, `String` values are processed using `Double.TryParse`. |
