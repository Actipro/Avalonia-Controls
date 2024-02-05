using ActiproSoftware.Extensions;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.SharedSamples.Shapes.RingSliceIntro {
	
	/// <summary>
	/// A value converter that converts a <see cref="Double"/> to an <see cref="Angle"/>.
	/// </summary>
	public class AngleToDecimalConverter : IValueConverter {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IValueConverter.Convert"/>
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
			if (value is Angle angle)
				return (decimal)angle.Degrees;
			else
				return (decimal)0.0;
		}

		/// <inheritdoc cref="IValueConverter.ConvertBack"/>
		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
			if (value.TryConvertToDouble(out var doubleValue))
				return Angle.FromDegrees(doubleValue);
			else
				return Angle.FromDegrees(0.0);
		}

	}

}
