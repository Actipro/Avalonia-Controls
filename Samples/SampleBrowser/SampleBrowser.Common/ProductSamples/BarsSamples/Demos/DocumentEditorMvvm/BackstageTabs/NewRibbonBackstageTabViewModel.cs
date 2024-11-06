using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demos.DocumentEditorMvvm {

	/// <summary>
	/// Represents a view model for a the "New" tab control within a ribbon backstage.
	/// </summary>
	public class NewRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

		private readonly BarManager _barManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="NewRibbonBackstageTabViewModel"/> class.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
		public NewRibbonBackstageTabViewModel(BarManager barManager)
			: base(BarControlKeys.BackstageTabNew, "New") {

			_barManager = barManager;
			
			LargeIcon = barManager.ImageProvider.GetImage(BarControlKeys.BackstageTabNew, BarImageSize.Large);
			SmallIcon = barManager.ImageProvider.GetImage(BarControlKeys.BackstageTabNew, BarImageSize.Small);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarManager.NewBlankDocumentCommand"/>
		public ICommand? NewBlankDocumentCommand => _barManager?.NewBlankDocumentCommand;

		/// <inheritdoc cref="BarManager.NewDefaultDocumentCommand"/>
		public ICommand? NewDefaultDocumentCommand => _barManager?.NewDefaultDocumentCommand;

	}

}
