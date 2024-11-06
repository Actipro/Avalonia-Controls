namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides the base requirements for a control that has variant <see cref="IImage"/> properties.
	/// </summary>
	public interface IHasVariantImages {

		/// <summary>
		/// The object representing a large icon, generally <c>32x32</c> size.
		/// </summary>
		object? LargeIcon { get; set; }

		/// <summary>
		/// The object representing a medium icon, generally <c>24x24</c> size.
		/// </summary>
		object? MediumIcon { get; set; }

		/// <summary>
		/// The object representing a small icon, generally <c>16x16</c> size.
		/// </summary>
		object? SmallIcon { get; set; }

	}

}
