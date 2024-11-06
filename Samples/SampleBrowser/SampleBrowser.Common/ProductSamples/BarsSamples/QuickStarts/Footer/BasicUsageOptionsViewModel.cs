using ActiproSoftware.UI.Avalonia.Controls.Bars;
using Avalonia;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.Footer {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class BasicUsageOptionsViewModel : ObservableObjectBase {

		private RibbonFooterKind _footerKind = RibbonFooterKind.Warning;
		private bool _isFooterVisible = true;
		private RibbonQuickAccessToolBarLocation _qatLocation = RibbonQuickAccessToolBarLocation.Below;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The footer kind.
		/// </summary>
		public RibbonFooterKind FooterKind {
			get => _footerKind;
			set => SetProperty(ref _footerKind, value);
		}

		/// <summary>
		/// Indicates if the footer is visible.
		/// </summary>
		public bool IsFooterVisible {
			get => _isFooterVisible;
			set => SetProperty(ref _isFooterVisible, value);
		}

		/// <summary>
		/// The location of the Quick Access Toolbar.
		/// </summary>
		public RibbonQuickAccessToolBarLocation QuickAccessToolBarLocation {
			get => _qatLocation;
			set => SetProperty(ref _qatLocation, value);
		}

	}

}
