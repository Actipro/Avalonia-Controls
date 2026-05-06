using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.RangeSliderIntro;

/// <summary>
/// Represents a value converter that converts a total number of minutes since midnight into the time of day.
/// </summary>
public class TotalMinutesToTimeOfDayConverter : IValueConverter {

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
		var timeOfDay = DateTime.Today.AddMinutes(elapsedTime.TotalMinutes);
		return timeOfDay.ToShortTimeString();
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		=> new BindingNotification(new NotImplementedException(), BindingErrorType.Error);

}