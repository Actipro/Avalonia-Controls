namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Stores options data for an image used in a bar control.
	/// </summary>
	public struct BarImageOptions {

		/// <summary>
		/// The default image options instance.
		/// </summary>
		public static BarImageOptions Default { get; } = new BarImageOptions(BarImageSize.Small);
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="size">A <see cref="BarImageSize"/> indicating the image size.</param>
		public BarImageOptions(BarImageSize size) {
			this.ContextualColor = null;
			this.Size = size;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The optional contextual <see cref="Color"/>.
		/// </summary>
		public Color? ContextualColor { get; set; }

		/// <summary>
		/// The <see cref="BarImageSize"/> indicating the image size.
		/// </summary>
		public BarImageSize Size { get; set; }

	}

}
