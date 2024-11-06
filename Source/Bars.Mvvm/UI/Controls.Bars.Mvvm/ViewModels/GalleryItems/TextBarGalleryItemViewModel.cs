namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a gallery item whose value and label are the same text value.
	/// </summary>
	public class TextBarGalleryItemViewModel : BarGalleryItemViewModel<string> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with the specified text as the value and label.
		/// </summary>
		/// <param name="text">The item's value and label.</param>
		public TextBarGalleryItemViewModel(string text)
			: base(text, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified text and category.
		/// </summary>
		/// <param name="text">The item's value and label.</param>
		/// <param name="category">The item's category.</param>
		public TextBarGalleryItemViewModel(string text, string? category)
			: base(text, category) { }

	}

}
