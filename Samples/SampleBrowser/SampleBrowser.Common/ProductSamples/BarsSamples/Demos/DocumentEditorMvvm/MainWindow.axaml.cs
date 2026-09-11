using ActiproSoftware.UI.Avalonia.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.Demos.DocumentEditorMvvm;

public partial class MainWindow : RibbonWindow {

	public MainWindow() {
		InitializeComponent();

		mainControl.RibbonViewModel!.QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Above;
	}

	protected override void OnOpened(EventArgs e) {
		base.OnOpened(e);

		// Ensure the editor has initial focus when the window is opened
		var textBox = this.GetVisualDescendants().OfType<TextBox>().LastOrDefault(t => t.AcceptsReturn);
		textBox?.Focus();
	}

}
