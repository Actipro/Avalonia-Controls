using Avalonia.Media;
using System;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents style attributes of rich text.
	/// </summary>
	public class RichTextStyle : ICloneable {
		
		object ICloneable.Clone() => this.Clone();

		/// <summary>
		/// Indicates if bold is active.
		/// </summary>
		public bool Bold { get; set; }
		
		/// <inheritdoc cref="ICloneable.Clone"/>
		public RichTextStyle Clone() {
			return new RichTextStyle() {
				Bold = this.Bold,
				FontColor = this.FontColor,
				FontFamilyName = this.FontFamilyName,
				FontSize = this.FontSize,
				Italic = this.Italic,
				TextAlignment = this.TextAlignment,
				TextHighlightColor = this.TextHighlightColor,
			};
		}

		/// <inheritdoc />
		public override bool Equals(object? obj) {
			if (obj is RichTextStyle other) {
				return this.Bold == other.Bold
					&& this.FontColor == other.FontColor
					&& this.FontFamilyName == other.FontFamilyName
					&& this.FontSize == other.FontSize
					&& this.Italic == other.Italic
					&& this.TextAlignment == other.TextAlignment
					&& this.TextHighlightColor == other.TextHighlightColor;
			}
			return false;
		}
		
		/// <summary>
		/// The font color.
		/// </summary>
		public Color FontColor { get; set; } = Colors.Black;

		/// <summary>
		/// The font family name.
		/// </summary>
		/// <value>A string value, or <c>null</c> for the default font.</value>
		public string? FontFamilyName { get; set; }

		/// <summary>
		/// The font size.
		/// </summary>
		/// <value>A double value, or <c>null</c> for the default size.</value>
		public double? FontSize { get; set; }
		
		/// <inheritdoc />
		public override int GetHashCode()
			=> HashCode.Combine(Bold, FontColor, FontFamilyName, FontSize, Italic, TextAlignment, TextHighlightColor);
		
		/// <summary>
		/// Indicates if italic is active.
		/// </summary>
		public bool Italic { get; set; }

		/// <summary>
		/// The text alignment.
		/// </summary>
		public TextAlignment TextAlignment { get; set; } = TextAlignment.Left;
		
		/// <summary>
		/// The text highlight color.
		/// </summary>
		public Color TextHighlightColor { get; set; } = Colors.White;

	}

}
