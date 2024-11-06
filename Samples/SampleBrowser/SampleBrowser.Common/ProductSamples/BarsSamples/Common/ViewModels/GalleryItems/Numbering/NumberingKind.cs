using System.ComponentModel.DataAnnotations;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Specifies the kind of numbering.
	/// </summary>
	public enum NumberingKind {
		
		/// <summary>
		/// No numbering.
		/// </summary>
		[Display(Name = "None")]
		None,

		/// <summary>
		/// <c>1</c>, <c>2</c>, <c>3</c>, etc.
		/// </summary>
		[Display(Name = "Arabic Numerals")]
		ArabicNumeral,

		/// <summary>
		/// <c>I</c>, <c>II</c>, <c>III</c>, etc.
		/// </summary>
		[Display(Name = "Uppercase Roman Numerals")]
		UpperRomanNumeral,

		/// <summary>
		/// <c>i</c>, <c>ii</c>, <c>iii</c>, etc.
		/// </summary>
		[Display(Name = "Lowercase Roman Numerals")]
		LowerRomanNumeral,

		/// <summary>
		/// <c>A</c>, <c>B</c>, <c>C</c>, etc.
		/// </summary>
		[Display(Name = "Uppercase Letters")]
		UpperAlpha,

		/// <summary>
		/// <c>a</c>, <c>b</c>, <c>c</c>, etc.
		/// </summary>
		[Display(Name = "Lowercase Letters")]
		LowerAlpha,

	}

}
