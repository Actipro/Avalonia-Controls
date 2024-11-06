using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demos.DocumentEditorMvvm {

	/// <summary>
	/// Represents a view model for a the "Info" tab control within a ribbon backstage.
	/// </summary>
	public class InfoRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

		private readonly BarManager _barManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="InfoRibbonBackstageTabViewModel"/> class.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
		public InfoRibbonBackstageTabViewModel(BarManager barManager)
			: base(BarControlKeys.BackstageTabInfo, "Info") {

			_barManager = barManager;
			
			LargeIcon = barManager.ImageProvider.GetImage(BarControlKeys.BackstageTabInfo, BarImageSize.Large);
			SmallIcon = barManager.ImageProvider.GetImage(BarControlKeys.BackstageTabInfo, BarImageSize.Small);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarManager.NotImplementedCommand"/>
		public ICommand? NotImplementedCommand => _barManager?.NotImplementedCommand;

	}

}
