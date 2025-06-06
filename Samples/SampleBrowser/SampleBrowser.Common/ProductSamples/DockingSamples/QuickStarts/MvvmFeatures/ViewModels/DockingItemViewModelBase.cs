using Avalonia.Media;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures.ViewModels {

	/// <summary>
	/// Represents a base class for all docking item view-models.
	/// </summary>
	public abstract class DockingItemViewModelBase : ObservableObjectBase {

		private string? _description;
		private IImage? _icon;
		private bool _isActive;
		private bool _isFloating;
		private bool _isOpen;
		private bool _isSelected;
		private string? _serializationId;
		private string? _title;
		private string? _windowGroupName;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The description associated with the view-model.
		/// </summary>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}

		/// <summary>
		/// The icon associated with the view-model.
		/// </summary>
		public IImage? Icon {
			get => _icon;
			set => SetProperty(ref _icon, value);
		}

		/// <summary>
		/// Whether the view is currently active.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view is currently active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive {
			get => _isActive;
			set => SetProperty(ref _isActive, value);
		}

		/// <summary>
		/// Whether the view is floating.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view is floating; otherwise, <c>false</c>.
		/// </value>
		public bool IsFloating {
			get => _isFloating;
			set => SetProperty(ref _isFloating, value);
		}

		/// <summary>
		/// Whether the view is currently open.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view is currently open; otherwise, <c>false</c>.
		/// </value>
		public bool IsOpen {
			get => _isOpen;
			set => SetProperty(ref _isOpen, value);
		}

		/// <summary>
		/// Whether the view is currently selected in its parent container.
		/// </summary>
		/// <value>
		/// <c>true</c> if the view is currently selected in its parent container; otherwise, <c>false</c>.
		/// </value>
		public bool IsSelected {
			get => _isSelected;
			set => SetProperty(ref _isSelected, value);
		}

		/// <summary>
		/// Indicates whether the container generated for this view model should be a tool window.
		/// </summary>
		/// <value>
		/// <c>true</c> if the container generated for this view model should be a tool window; otherwise, <c>false</c>.
		/// </value>
		public abstract bool IsTool { get; }

		/// <summary>
		/// The name that uniquely identifies the view-model for layout serialization.
		/// </summary>
		public string? SerializationId {
			get => _serializationId;
			set => SetProperty(ref _serializationId, value);
		}

		/// <summary>
		/// The title associated with the view-model.
		/// </summary>
		public string? Title {
			get => _title;
			set => SetProperty(ref _title, value);
		}

		/// <summary>
		/// The window group name associated with the view-model.
		/// </summary>
		public string? WindowGroupName {
			get => _windowGroupName;
			set => SetProperty(ref _windowGroupName, value);
		}

	}

}
