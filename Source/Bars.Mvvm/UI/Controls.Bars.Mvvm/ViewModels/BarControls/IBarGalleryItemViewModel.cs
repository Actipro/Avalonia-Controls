namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a gallery item within a bar gallery control.
	/// </summary>
	public interface IBarGalleryItemViewModel : INotifyPropertyChanged, IEquatable<IBarGalleryItemViewModel>, IHasTag {

		/// <summary>
		/// The item's category.
		/// </summary>
		string? Category { get; set; }

		/// <summary>
		/// The text description to display in screen tips.
		/// </summary>
		string? Description { get; set; }
		
		/// <summary>
		/// The value representing the icon to display, if the item's template supports an icon.
		/// </summary>
		object? Icon { get; set; }

		/// <summary>
		/// Indicates whether the <see cref="Label"/> is visible.
		/// </summary>
		bool IsLabelVisible { get; }

		/// <summary>
		/// Indicates whether the control is currently visible.
		/// </summary>
		bool IsVisible { get; }

		/// <summary>
		/// The key tip text used to access the control.
		/// </summary>
		string? KeyTipText { get; set; }

		/// <summary>
		/// The text label to display.
		/// </summary>
		string? Label { get; set; }

		/// <summary>
		/// A <see cref="BarGalleryItemLayoutBehavior"/> indicating how the gallery item should be visually displayed.
		/// </summary>
		/// <value>
		/// The default value is <see cref="BarGalleryItemLayoutBehavior.Default"/>.
		/// </value>
		BarGalleryItemLayoutBehavior LayoutBehavior { get; }
		
		/// <summary>
		/// The value associated with this view model.
		/// </summary>
		object? Value { get; set; }

	}

}
