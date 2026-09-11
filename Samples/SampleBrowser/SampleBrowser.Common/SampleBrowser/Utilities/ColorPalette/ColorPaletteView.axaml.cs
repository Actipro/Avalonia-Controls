namespace ActiproSoftware.SampleBrowser.Utilities.ColorPalette;

public partial class ColorPaletteView : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public ColorPaletteView() {
		InitializeComponent();

		DataContext = new ColorPaletteViewModel();
	}

}
