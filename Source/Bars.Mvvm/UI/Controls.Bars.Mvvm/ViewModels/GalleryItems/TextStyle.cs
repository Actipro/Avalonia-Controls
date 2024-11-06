namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents style attributes of text.
	/// </summary>
	public class TextStyle : ICloneable {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the text style with default values.
		/// </summary>
		public TextStyle()
			: this(fontFamilyName: null, fontSize: null, textColor: null) { }

		/// <summary>
		/// Initializes a new instance of the text style with the specified font family name, font size, and font color.
		/// </summary>
		/// <param name="fontFamilyName">The font family name or <c>null</c> to use the default font family name.</param>
		/// <param name="fontSize">The font size, or <c>null</c> to use the default font size.</param>
		/// <param name="textColor">The text color, or <c>null</c> to use the default text color.</param>
		public TextStyle(string? fontFamilyName, double? fontSize, Color? textColor) {
			this.FontFamilyName = fontFamilyName ?? FontFamilyBarGalleryItemViewModel.DefaultFontFamilyName;
			this.FontSize = fontSize ?? FontSizeBarGalleryItemViewModel.DefaultFontSize;
			this.TextColor = textColor ?? Colors.Black;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		object ICloneable.Clone()
			=> this.Clone();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indicates if bold is active.
		/// </summary>
		public bool Bold { get; set; }

		/// <inheritdoc cref="ICloneable.Clone"/>
		public TextStyle Clone() {
			return new TextStyle() {
				Bold = this.Bold,
				FontFamilyName = this.FontFamilyName,
				FontSize = this.FontSize,
				Italic = this.Italic,
				TextColor = this.TextColor,
			};
		}

		/// <inheritdoc />
		public override bool Equals(object? obj) {
			if (obj is TextStyle other) {
				return this.Bold == other.Bold
					&& this.FontFamilyName == other.FontFamilyName
					&& this.FontSize == other.FontSize
					&& this.Italic == other.Italic
					&& this.TextColor == other.TextColor;
			}
			return false;
		}

		/// <inheritdoc />
		public override int GetHashCode()
			=> HashCode.Combine(Bold, FontFamilyName, FontSize, Italic, TextColor);

		/// <summary>
		/// The font family name.
		/// </summary>
		public string FontFamilyName { get; set; }

		/// <summary>
		/// The font size.
		/// </summary>
		public double FontSize { get; set; }

		/// <summary>
		/// Indicates if italic is active.
		/// </summary>
		public bool Italic { get; set; }

		/// <summary>
		/// The text color.
		/// </summary>
		public Color TextColor { get; set; }

	}

}
