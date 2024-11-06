using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.LayoutModes {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			var barManager = new BarManager();

			RibbonViewModel = new RichTextEditorRibbonViewModel(barManager);

			// Don't use an application button or tab row toolbar in the minimal sample since tabs can be hidden
			MinimalRibbonViewModel = new RichTextEditorRibbonViewModel(barManager);
			MinimalRibbonViewModel.IsApplicationButtonVisible = false;
			MinimalRibbonViewModel.QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Hidden;
			MinimalRibbonViewModel.TabRowToolBar = null;

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The ribbon view model for the "Tab visibility and group labels" sample.
		/// </summary>
		public RibbonViewModel MinimalRibbonViewModel { get; }

		/// <summary>
		/// The ribbon view model shared by both ribbons in the "Layout modes" sample.
		/// </summary>
		public RibbonViewModel RibbonViewModel { get; }

	}

}
