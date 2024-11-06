using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demos.DocumentEditorMvvm {

	/// <summary>
	/// Represents a view model for a the "Open" tab control within a ribbon backstage.
	/// </summary>
	public class OpenRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

		private readonly BarManager _barManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenRibbonBackstageTabViewModel"/> class.
		/// </summary>
		/// <param name="barManager">The <see cref="BarManager"/> associated with the view model.</param>
		public OpenRibbonBackstageTabViewModel(BarManager barManager, ObservableCollection<RecentDocumentItem> recentDocuments)
			: base(BarControlKeys.BackstageTabOpen, "Open") {

			_barManager = barManager;
			
			RecentDocuments = recentDocuments;
			
			LargeIcon = barManager.ImageProvider.GetImage(BarControlKeys.BackstageTabOpen, BarImageSize.Large);
			SmallIcon = barManager.ImageProvider.GetImage(BarControlKeys.BackstageTabOpen, BarImageSize.Small);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarManager.NotImplementedCommand"/>
		public ICommand? NotImplementedCommand => _barManager?.NotImplementedCommand;

		/// <summary>
		/// The collection of recently-opened documents.
		/// </summary>
		public ObservableCollection<RecentDocumentItem> RecentDocuments { get; }

	}

}
