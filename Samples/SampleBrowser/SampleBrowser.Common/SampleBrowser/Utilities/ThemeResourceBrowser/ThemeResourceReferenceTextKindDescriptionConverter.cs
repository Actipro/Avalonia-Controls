using Avalonia.Data.Converters;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ActiproSoftware.SampleBrowser.Utilities.ThemeResourceBrowser {

	/// <summary>
	/// A value converter that converts a <see cref="ThemeResourceReferenceTextKind"/> value to it's description based on the attached <see cref="DescriptionAttribute"/>.
	/// </summary>
	public class ThemeResourceReferenceTextKindDescriptionConverter : IValueConverter {

		/// <inheritdoc/>
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
			if (value is ThemeResourceReferenceTextKind kind) {
				var fieldInfo = typeof(ThemeResourceReferenceTextKind)
					.GetFields()
					.FirstOrDefault(x => x.IsStatic && ((ThemeResourceReferenceTextKind?)x.GetValue(null))?.Equals(kind) == true);
				return fieldInfo?.GetCustomAttribute<DescriptionAttribute>()?.Description;
			}
			return value?.ToString();
		}

		/// <inheritdoc/>
		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
			if (value is string description) {
				var fieldInfo = typeof(ThemeResourceReferenceTextKind)
					.GetFields()
					.FirstOrDefault(x => x.IsStatic && description.Equals(x.GetCustomAttribute<DescriptionAttribute>()?.Description, StringComparison.OrdinalIgnoreCase));
				if (fieldInfo is not null)
					return fieldInfo.GetValue(null);
			}
			return null;
		}
	}

}
