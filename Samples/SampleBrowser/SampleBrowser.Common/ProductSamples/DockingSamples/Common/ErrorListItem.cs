using ActiproSoftware.UI.Avalonia.Media;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common {

	/// <summary>
	/// Defines a view model for displaying items in the Error List tool window.
	/// </summary>
	public class ErrorListItem : ObservableObjectBase {

		private string? _description;
		private string? _fileName;
		private string? _iconKey;
		private string? _line;
		private string? _project;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The description of the message.
		/// </summary>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}

		/// <summary>
		/// The file name, if any, related to the message.
		/// </summary>
		public string? FileName {
			get => _fileName;
			set => SetProperty(ref _fileName, value);
		}

		/// <summary>
		/// The key which identifies the icon to represent the item.
		/// </summary>
		/// <seealso cref="SharedImageSourceKeys"/>
		public string? IconKey {

			get => _iconKey;
			set => SetProperty(ref _iconKey, value);
		}

		/// <summary>
		/// The line number, if any, related to the message.
		/// </summary>
		public string? Line {
			get => _line;
			set => SetProperty(ref _line, value);
		}

		/// <summary>
		/// The project name, if any, related to the message.
		/// </summary>
		public string? Project {
			get => _project;
			set => SetProperty(ref _project, value);
		}

	}

}
