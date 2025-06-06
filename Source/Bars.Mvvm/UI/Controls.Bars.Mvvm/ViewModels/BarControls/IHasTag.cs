namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides the base requirements for a control that can store custom data on a <see cref="Tag"/> property.
	/// </summary>
	public interface IHasTag {
		
		/// <summary>
		/// A user-defined object attached to the control.
		/// </summary>
		object? Tag { get; set; }

	}

}
