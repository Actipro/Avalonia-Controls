using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Specifies the kind of underline.
	/// </summary>
	public enum UnderlineKind {

		/// <summary>
		/// No underline.
		/// </summary>
		[Display(Name = "None")]
		None,

		/// <summary>
		/// A single underline.
		/// </summary>
		[Display(Name = "Underline")]
		Underline,

		/// <summary>
		/// A double underline.
		/// </summary>
		[Display(Name = "Double Underline")]
		DoubleUnderline,

		/// <summary>
		/// A thick underline.
		/// </summary>
		[Display(Name = "Thick Underline")]
		ThickUnderline,

		// NOTE: DashStyle.Dot doesn't render properly so excluding for now
		/*
		/// <summary>
		/// A dotted underline.
		/// </summary>
		[Display(Name = "Dotted Underline")]
		DottedUnderline,
		*/

		/// <summary>
		/// A dashed underline.
		/// </summary>
		[Display(Name = "Dashed Underline")]
		DashedUnderline,

		/// <summary>
		/// A dot-dash underline.
		/// </summary>
		[Display(Name = "Dot-Dash Underline")]
		DotDashUnderline,

		/// <summary>
		/// A dot-dot-dash underline.
		/// </summary>
		[Display(Name = "Dot-Dot-Dash Underline")]
		DotDotDashUnderline,

		/// <summary>
		/// A wave underline.
		/// </summary>
		[Display(Name = "Wave Underline")]
		WaveUnderline

	}

}
