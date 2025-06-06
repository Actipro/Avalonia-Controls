namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for image and text content within a ribbon footer.
	/// </summary>
	public class RibbonFooterSimpleContentViewModel : ObservableObjectBase, IHasTag {

		private object? _icon;
		private object? _tag;
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

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object? Tag {
			get => _tag;
			set => SetProperty(ref _tag, value);
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
