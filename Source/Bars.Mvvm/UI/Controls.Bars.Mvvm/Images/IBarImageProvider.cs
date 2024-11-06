namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides the base requirements for an object that can provide images for bar controls.
	/// </summary>
	public interface IBarImageProvider {

		/// <summary>
		/// Gets an object representing an image for the specified bar control key and size.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <param name="size">A <see cref="BarImageSize"/> that indicates the image size.</param>
		public object? GetImage(string? key, BarImageSize size);

		/// <summary>
		/// Gets an object representing an image for the specified bar control key and set of options.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <param name="options">A <see cref="BarImageOptions"/> that indicates the image options.</param>
		public object? GetImage(string? key, BarImageOptions options);

	}

}
