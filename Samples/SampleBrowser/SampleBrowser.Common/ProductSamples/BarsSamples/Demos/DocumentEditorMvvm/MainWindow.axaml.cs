using ActiproSoftware.UI.Avalonia.Controls.Bars;
using Avalonia.Controls;
using Avalonia.VisualTree;
using System;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demos.DocumentEditorMvvm {

	public partial class MainWindow : RibbonWindow {

		public MainWindow() {
			InitializeComponent();

			mainControl.RibbonViewModel!.QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Above;
		}

		protected override void OnOpened(EventArgs e) {
			base.OnOpened(e);

			// Ensure the editor has initial focus when the window is opened
			var textBox = this.GetVisualDescendants().OfType<TextBox>().Where(t => t.AcceptsReturn == true).LastOrDefault();
			textBox?.Focus();
		}

	}

}
