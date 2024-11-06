using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Media;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GalleryInRibbon {

	/// <summary>
	/// Converts a <see cref="ColorBarGalleryItemViewModel"/> into a <see cref="SolidColorBrush"/>
	/// that provides contrast against the color defined by the view model (i.e., white text for dark
	/// background colors and black text for light background colors).
	/// </summary>
	public class TextForegroundColorConverter : IValueConverter {

		/// <inheritdoc cref="IValueConverter.Convert"/>
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
			if (value is null)
				return null;

			if (value is ColorBarGalleryItemViewModel colorViewModel) {
				// Assume black text by default
				var foreColor = UIColor.FromRgb(Colors.Black);

				// Adapt the color to the background
				foreColor = foreColor.ToChromaticAdaptation(colorViewModel.Value, isHighContrast: false);

				// Create a brush from the adapted color
				return new SolidColorBrush(foreColor);
			}

			// Value could not be converted
			throw new NotSupportedException();
		}

		/// <inheritdoc cref="IValueConverter.ConvertBack"/>
		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
			=> new BindingNotification(new NotImplementedException(), BindingErrorType.Error);

	}

}
