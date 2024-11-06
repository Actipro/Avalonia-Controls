using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Specifies the kind of bullet.
	/// </summary>
	public enum BulletKind {

		/// <summary>
		/// No bullet.
		/// </summary>
		[Display(Name = "None")]
		None,

		/// <summary>
		/// Circle bullet.
		/// </summary>
		[Display(Name = "Circle")]
		Circle,

		/// <summary>
		/// Filled circle bullet.
		/// </summary>
		[Display(Name = "Filled Circle")]
		FilledCircle,

		/// <summary>
		/// Filled square bullet.
		/// </summary>
		[Display(Name = "Filled Square")]
		FilledSquare,

		/// <summary>
		/// Square bullet.
		/// </summary>
		[Display(Name = "Square")]
		Square,

	}

}
