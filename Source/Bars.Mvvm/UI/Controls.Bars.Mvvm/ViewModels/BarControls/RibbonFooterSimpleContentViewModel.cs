namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for image and text content within a ribbon footer.
	/// </summary>
	public class RibbonFooterSimpleContentViewModel : ObservableObjectBase {

		private object? _icon;
		private string? _text;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// A value representing the icon.
		/// </summary>
		public object? Icon {
			get => _icon;
			set => SetProperty(ref _icon, value);
		}
		
		/// <summary>
		/// The text content.
		/// </summary>
		public string? Text {
			get => _text;
			set => SetProperty(ref _text, value);
		}
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[Text='{this.Text}']";

	}

}
