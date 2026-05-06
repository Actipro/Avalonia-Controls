using ActiproSoftware.UI.Avalonia.Controls.Barcodes;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.DataVisualizationSamples.BarcodeQuickStarts.PresenterCustomization;

public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public MainControl() {
		InitializeComponent();

		// Select the first symbology by default
		symbologyComboBox.SelectedIndex = 0;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnSymbologyComboBoxSelectionChanged(object? sender, SelectionChangedEventArgs e) {
		if (symbologyComboBox.SelectedItem is BarcodeSymbologyBase selectedSymbology) {
			primaryBarcode.Symbology = selectedSymbology;
			primaryBarcode.Value = selectedSymbology.ExampleValue;
		}
	}

}
