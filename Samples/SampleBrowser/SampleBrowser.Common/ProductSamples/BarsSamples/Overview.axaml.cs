using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.ProductSamples.BarsSamples.Demos.DocumentEditorMvvm;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using Avalonia;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples {

	public partial class Overview : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public Overview() {
			InitializeComponent();

			// Use an adapter to register BarManager commands that interact with a TextBox instance
			_ = new TextBoxBarManagerCommandAdapter(BarManager, textBox);

			// Create the view model for the ribbon used by this demo
			RibbonViewModel = new DocumentEditorRibbonViewModel(BarManager);

			// Improve default command states for commands associated with the textbox
			textBox.Text = "Actipro";
			textBox.Text += " Bars"; // Enables Undo
			textBox.SelectAll(); // Enables Cut/Copy

			// Initialize toggle control states
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowApplicationButton, () => this.RibbonViewModel?.IsApplicationButtonVisible ?? false);
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowFooter, () => this.RibbonViewModel?.Footer != null);
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.ShowQuickAccessToolBar, () => this.RibbonViewModel?.QuickAccessToolBarMode == RibbonQuickAccessToolBarMode.Visible);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private BarManager BarManager { get; } = new BarManager();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
			base.OnAttachedToVisualTree(e);

			BarManager.WatchUserInterfaceDensityChangedEvent();
		}

		protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e) {
			base.OnDetachedFromVisualTree(e);

			BarManager.UnwatchUserInterfaceDensityChangedEvent();
		}

		public RibbonViewModel? RibbonViewModel {
			get => ribbon.DataContext as RibbonViewModel;
			set => ribbon.DataContext = value;
		}

	}

}
