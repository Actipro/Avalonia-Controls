using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.RangeSliderIntro;

/// <summary>
/// Represents a value converter that converts a total number of minutes into a string.
/// </summary>
public class TotalMinutesToElapsedTimeConverter : IValueConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IValueConverter.Convert"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if (value is not double doubleValue)
			return new BindingNotification(new ArgumentException($"Value must be of type {nameof(Double)}.", nameof(value)), BindingErrorType.Error);

		if (double.IsNaN(doubleValue) || double.IsInfinity(doubleValue))
			return null;

		var elapsedTime = TimeSpan.FromMinutes(doubleValue);
		if (elapsedTime.Hours > 0)
			return $"{elapsedTime.Hours}h {elapsedTime.Minutes}m";
		else
			return $"{elapsedTime.Minutes}m";
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		=> new BindingNotification(new NotImplementedException(), BindingErrorType.Error);

}