using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demos.DocumentEditorMvvm {

	/// <inheritdoc/>
	internal class DocumentEditorRibbonViewModel : RichTextEditorRibbonViewModel {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="RichTextEditorRibbonViewModel(BarManager)"/>
		public DocumentEditorRibbonViewModel(BarManager barManager)
			: base(barManager) {

			// Populate the recent documents
			foreach (var item in RecentDocumentHelper.CreateDefaultItems())
				this.RecentDocuments.Add(item);
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override RibbonBackstageViewModel CreateBackstage(BarManager barManager) {
			return new RibbonBackstageViewModel() {
				Items = {
					new InfoRibbonBackstageTabViewModel(barManager),
					new NewRibbonBackstageTabViewModel(barManager),
					new OpenRibbonBackstageTabViewModel(barManager, this.RecentDocuments),
					new RibbonBackstageHeaderSeparatorViewModel(),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonSave, "Save", barManager.NotImplementedCommand),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonSaveAs, "Save As", "A", barManager.NotImplementedCommand),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonPrint, "Print", barManager.NotImplementedCommand),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonShare, "Share", "Z", barManager.NotImplementedCommand),
					new ExportRibbonBackstageTabViewModel(barManager),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonClose, "Close", barManager.NotImplementedCommand),
					new RibbonBackstageHeaderSeparatorViewModel(RibbonBackstageHeaderAlignment.Bottom),
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonAccount, "Account", "D", barManager.NotImplementedCommand) {
						HeaderAlignment = RibbonBackstageHeaderAlignment.Bottom
					},
					new RibbonBackstageHeaderButtonViewModel(BarControlKeys.BackstageButtonOptions, "Options", "T", barManager.NotImplementedCommand) {
						HeaderAlignment = RibbonBackstageHeaderAlignment.Bottom
					},
				},
			};
		}

	}
}
