using ActiproSoftware.UI.Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ComboBoxAndEditors {

	/// <summary>
	/// Defines configurable options for the Font Colors sample.
	/// </summary>
	public class ComboBoxOptionsViewModel : ObservableObjectBase {

		private bool _canCategorizeOnMenu = true;
		private bool _canFilterOnMenu = false;
		private bool _isEditable = false;
		private bool _isReadOnly = false;
		private bool _isTextCompletionEnabled = true;
		private bool _isTextSearchCaseSensitive = false;
		private bool _isTextSearchEnabled = true;
		private bool _isUnmathcedTextAllowed = true;
		private ControlResizeMode _menuResizeMode = ControlResizeMode.None;
		private string? _placeholderText = "(employee)";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// If the gallery is categorized when displayed as a menu.
		/// </summary>
		public bool CanCategorizeOnMenu {
			get => _canCategorizeOnMenu;
			set {
				if (SetProperty(ref _canCategorizeOnMenu, value)) {

					if (!_canCategorizeOnMenu) {
						// Disable filtering if categories are not active
						CanFilterOnMenu = false;
					}
				}
			}
		}

		/// <summary>
		/// If the gallery can be filtered when displayed as a menu.
		/// </summary>
		public bool CanFilterOnMenu {
			get => _canFilterOnMenu;
			set {
				if (SetProperty(ref _canFilterOnMenu, value)) {
					if (_canFilterOnMenu) {
						// Ensure categorization is enabled or filtering has no effect
						CanCategorizeOnMenu = true;
					}
				}
			}
		}

		/// <summary>
		/// Indicates if the value of the combobox is editable.
		/// </summary>
		public bool IsEditable {
			get => _isEditable;
			set {
				if (SetProperty(ref _isEditable, value))
					OnPropertyChanged(nameof(IsUnmatchedTextSupported));
			}
		}

		/// <summary>
		/// Indicates if the value of the combobox is read-only.
		/// </summary>
		public bool IsReadOnly {
			get => _isReadOnly;
			set => SetProperty(ref _isReadOnly, value);
		}

		/// <summary>
		/// Indicates if text completion is neabled.
		/// </summary>
		public bool IsTextCompletionEnabled {
			get => _isTextCompletionEnabled;
			set => SetProperty(ref _isTextCompletionEnabled, value);
		}

		/// <summary>
		/// Indicates if text search is case-sensitive.
		/// </summary>
		public bool IsTextSearchCaseSensitive {
			get => _isTextSearchCaseSensitive;
			set => SetProperty(ref _isTextSearchCaseSensitive, value);
		}

		/// <summary>
		/// Indicates if text search is enabled.
		/// </summary>
		public bool IsTextSearchEnabled {
			get => _isTextSearchEnabled;
			set => SetProperty(ref _isTextSearchEnabled, value);
		}

		/// <summary>
		/// Indicates if unmatched text is allowed.
		/// </summary>
		public bool IsUnmatchedTextAllowed {
			get => _isUnmathcedTextAllowed;
			set {
				if (SetProperty(ref _isUnmathcedTextAllowed, value))
					OnPropertyChanged(nameof(IsUnmatchedTextSupported));
			} 
		}

		/// <summary>
		/// Indicates if unmatched text is supported.
		/// </summary>
		public bool IsUnmatchedTextSupported
			=> IsEditable && IsUnmatchedTextAllowed;

		/// <summary>
		/// The resize mode of the combobox menu.
		/// </summary>
		public ControlResizeMode MenuResizeMode {
			get => _menuResizeMode;
			set => SetProperty(ref _menuResizeMode, value);
		}

		/// <summary>
		/// The placeholder text of the combobox.
		/// </summary>
		public string? PlaceholderText {
			get => _placeholderText;
			set => SetProperty(ref _placeholderText, value);
		}

	}

}
