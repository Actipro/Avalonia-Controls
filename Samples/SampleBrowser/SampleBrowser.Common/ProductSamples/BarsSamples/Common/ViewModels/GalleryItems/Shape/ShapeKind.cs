using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Specifies the kind of shape.
	/// </summary>
	public enum ShapeKind {

		/// <summary>
		/// Line.
		/// </summary>
		[Display(Name = "Line")]
		Line,

		/// <summary>
		/// Line arrow.
		/// </summary>
		[Display(Name = "Line Arrow")]
		LineArrow,

		/// <summary>
		/// Line arrow: double.
		/// </summary>
		[Display(Name = "Line Arrow: Double")]
		LineArrowDouble,

		/// <summary>
		/// Oval.
		/// </summary>
		[Display(Name = "Oval")]
		Oval,

		/// <summary>
		/// Rectangle.
		/// </summary>
		[Display(Name = "Rectangle")]
		Rectangle,

		/// <summary>
		/// Rectangle: rounded corners.
		/// </summary>
		[Display(Name = "Rectangle: Rounded Corners")]
		RectangleRoundedCorners,

		/// <summary>
		/// Rectangle: single corner snipped.
		/// </summary>
		[Display(Name = "Rectangle: Single Corner Snipped")]
		RectangleSingleCornerSnipped,

		/// <summary>
		/// Right triangle.
		/// </summary>
		[Display(Name = "Right Triangle")]
		RightTriangle,

	}

}
