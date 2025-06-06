using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures.ViewModels {

	/// <summary>
	/// Represents the text document view-model.
	/// </summary>
	public class TextDocumentItemViewModel : DocumentItemViewModel {

		private string? _text;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public TextDocumentItemViewModel() {
			Description = "Text document";
			Icon = ImageLoader.GetIcon("TextDocument16.png");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The text associated with the view-model.
		/// </summary>
		public string? Text {
			get => _text;
			set => SetProperty(ref _text, value);
		}

	}

}
