using ActiproSoftware.UI.Avalonia.Controls.Bars;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.Serialization {

	/// <summary>
	/// Defines a view model for turning options on or off that will be used during Ribbon serialization.
	/// </summary>
	public class SerializerOptionsViewModel : ObservableObjectBase {

		private bool _layoutMode = true;
		private bool _minimizedState = true;
		private bool _quickAccessToolBarAllowLabels = true;
		private bool _quickAccessToolBarItems = true;
		private bool _quickAccessToolBarLocation = true;
		private bool _quickAccessToolBarMode = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The <see cref="RibbonSerializerOptions"/> that are reflected by the current configuration.
		/// </summary>
		public RibbonSerializerOptions Options {
			get {
				// Start with no options enabled
				var options = RibbonSerializerOptions.None;

				// Add each option that is currently turned on
				if (LayoutMode)
					options |= RibbonSerializerOptions.LayoutMode;
				if (MinimizedState)
					options |= RibbonSerializerOptions.MinimizedState;
				if (QuickAccessToolBarAllowLabels)
					options |= RibbonSerializerOptions.QuickAccessToolBarAllowLabels;
				if (QuickAccessToolBarItems)
					options |= RibbonSerializerOptions.QuickAccessToolBarItems;
				if (QuickAccessToolBarLocation)
					options |= RibbonSerializerOptions.QuickAccessToolBarLocation;
				if (QuickAccessToolBarMode)
					options |= RibbonSerializerOptions.QuickAccessToolBarMode;

				return options;
			}
		}

		/// <summary>
		/// The <see cref="RibbonSerializerOptions"/> formatted as an initializer for a C# field.
		/// </summary>
		public string OptionsSampleCodeInitializer {
			get {
				// Split comma-delimited list of configured options
				var options = Options.ToString().Split(',');

				// OR the options together while prefixing with the enum name
				return string.Join(" | ", options.Select(x => $"{nameof(RibbonSerializerOptions)}.{x.Trim()}"));
			}
		}

		/// <summary>
		/// Indicates if <see cref="Ribbon.LayoutMode"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		public bool LayoutMode {
			get => _layoutMode;
			set {
				if (SetProperty(ref _layoutMode, value)) {
					OnPropertyChanged(nameof(Options));
					OnPropertyChanged(nameof(OptionsSampleCodeInitializer));
				}
			} 
		}

		/// <summary>
		/// Indicates if <see cref="Ribbon.IsMinimized"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		public bool MinimizedState {
			get => _minimizedState;
			set {
				if (SetProperty(ref _minimizedState, value)) {
					OnPropertyChanged(nameof(Options));
					OnPropertyChanged(nameof(OptionsSampleCodeInitializer));
				}
			} 
		}

		/// <summary>
		/// Indicates if <see cref="Ribbon.AllowLabelsOnQuickAccessToolBar"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		public bool QuickAccessToolBarAllowLabels {
			get => _quickAccessToolBarAllowLabels;
			set {
				if (SetProperty(ref _quickAccessToolBarAllowLabels, value)) {
					OnPropertyChanged(nameof(Options));
					OnPropertyChanged(nameof(OptionsSampleCodeInitializer));
				}
			}
		}

		/// <summary>
		/// Indicates if the items displayed in <see cref="Ribbon.QuickAccessToolBar"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		public bool QuickAccessToolBarItems {
			get => _quickAccessToolBarItems;
			set {
				if (SetProperty(ref _quickAccessToolBarItems, value)) {
					OnPropertyChanged(nameof(Options));
					OnPropertyChanged(nameof(OptionsSampleCodeInitializer));
				}
			}
		}

		/// <summary>
		/// Indicates if <see cref="Ribbon.QuickAccessToolBarLocation"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		public bool QuickAccessToolBarLocation {
			get => _quickAccessToolBarLocation;
			set {
				if (SetProperty(ref _quickAccessToolBarLocation, value)) {
					OnPropertyChanged(nameof(Options));
					OnPropertyChanged(nameof(OptionsSampleCodeInitializer));
				}
			}
		}

		/// <summary>
		/// Indicates if <see cref="Ribbon.QuickAccessToolBarMode"/> will be processed when serializing or deserializing the Ribbon.
		/// </summary>
		public bool QuickAccessToolBarMode {
			get => _quickAccessToolBarMode;
			set {
				if (SetProperty(ref _quickAccessToolBarMode, value)) {
					OnPropertyChanged(nameof(Options));
					OnPropertyChanged(nameof(OptionsSampleCodeInitializer));
				}
			}
		}

	}
}
