using ActiproSoftware.UI.Avalonia.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.QuickAccessToolBar {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class BasicUsageOptionsViewModel : ObservableObjectBase {

		private bool _allowLabels = false;
		private bool _isCustomizeButtonVisible = true;
		private RibbonQuickAccessToolBarLocation _location = RibbonQuickAccessToolBarLocation.Below;
		private RibbonQuickAccessToolBarMode _mode = RibbonQuickAccessToolBarMode.Visible;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public bool AllowLabels {
			get => _allowLabels;
			set => SetProperty(ref _allowLabels, value);
		}

		public bool IsCustomizeButtonVisible {
			get => _isCustomizeButtonVisible;
			set => SetProperty(ref _isCustomizeButtonVisible, value);
		}

		public RibbonQuickAccessToolBarLocation Location {
			get => _location;
			set => SetProperty(ref _location, value);
		}

		public RibbonQuickAccessToolBarMode Mode {
			get => _mode;
			set => SetProperty(ref _mode, value);
		}

	}

}
