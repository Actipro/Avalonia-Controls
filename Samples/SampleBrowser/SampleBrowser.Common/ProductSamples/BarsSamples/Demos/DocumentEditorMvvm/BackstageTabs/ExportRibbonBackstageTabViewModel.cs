using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demos.DocumentEditorMvvm {

	/// <summary>
	/// Represents a view model for a the "Export" tab control within a ribbon backstage.
	/// </summary>
	public class ExportRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

		private readonly BarManager _barManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="ExportRibbonBackstageTabViewModel"/> class.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
		public ExportRibbonBackstageTabViewModel(BarManager barManager)
			: base(BarControlKeys.BackstageTabExport, "Export") {

			_barManager = barManager;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarManager.NotImplementedCommand"/>
		public ICommand? NotImplementedCommand => _barManager?.NotImplementedCommand;

	}

}
