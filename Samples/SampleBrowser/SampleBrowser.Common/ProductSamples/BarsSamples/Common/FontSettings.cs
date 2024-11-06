using Avalonia.Media;
using System;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Defines default font-related settings.
	/// </summary>
	public static class FontSettings {

		public static readonly string DefaultFontFamilyName = OperatingSystem.IsBrowser() ? "fonts:Inter#Inter" : "Segoe UI";
		public const double DefaultFontSize = 14.0;
		
		public static readonly string HeadingFontFamilyName = DefaultFontFamilyName;
		public const double Heading1FontSize = DefaultFontSize + 5;
		public const double Heading2FontSize = DefaultFontSize + 2;
		public const double Heading3FontSize = DefaultFontSize + 1;
		public const double TitleFontSize = 28.0;

		public static readonly FontFamily DefaultFontFamily = new FontFamily(DefaultFontFamilyName);
		public static readonly FontFamily HeadingFontFamily = new FontFamily(HeadingFontFamilyName);

	}

}
