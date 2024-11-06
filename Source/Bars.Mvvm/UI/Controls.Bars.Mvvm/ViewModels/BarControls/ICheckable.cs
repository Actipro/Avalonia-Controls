namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides the base requirements for a control that is checkable.
	/// </summary>
	public interface ICheckable {
		
		/// <summary>
		/// Indicates whether the control is checked.
		/// </summary>
		bool IsChecked { get; set; }
		
	}

}
