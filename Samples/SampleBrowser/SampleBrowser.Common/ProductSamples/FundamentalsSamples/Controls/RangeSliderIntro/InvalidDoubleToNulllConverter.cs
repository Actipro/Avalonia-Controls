using ActiproSoftware.Extensions;
using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.RangeSliderIntro;

/// <summary>
/// Represents a value converter that converts <c>NaN</c>, <c>PositiveInfinity</c>, and <c>NegativeInfinith</c> to <c>null</c>.
/// </summary>
public class InvalidDoubleToNulllConverter : IValueConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if (value is not double doubleValue)
			return new BindingNotification(new ArgumentException($"Value must be of type {nameof(Double)}.", nameof(value)), BindingErrorType.Error);

		if (double.IsNaN(doubleValue) || double.IsInfinity(doubleValue))
			return null;

		return doubleValue;
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if (value is null)
			return double.NaN;
		else if (value.TryConvertToDouble(out var doubleValue))
			return doubleValue;
		return new BindingNotification(new NotImplementedException(), BindingErrorType.Error);
	}

}