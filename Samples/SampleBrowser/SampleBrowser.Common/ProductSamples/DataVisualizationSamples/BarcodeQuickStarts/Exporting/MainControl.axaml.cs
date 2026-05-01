using ActiproSoftware.UI.Avalonia.Controls.Barcodes;
using ActiproSoftware.UI.Avalonia.Controls.Barcodes.Implementation;
using Avalonia;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.DataVisualizationSamples.BarcodeQuickStarts.Exporting;

public partial class MainControl : UserControl {

	#region Property Definitions

	/// <summary>
	/// Defines the <see cref="ExportedSvg"/> property.
	/// </summary>
	public static readonly StyledProperty<string?> ExportedSvgProperty
		= AvaloniaProperty.Register<MainControl, string?>(nameof(ExportedSvg));

	#endregion

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

	private void ExportSvg() {
		var request = new BarcodeEncodingRequest(primaryBarcode.Value!) {
			ValueFontFamily = primaryBarcode.ValueFontFamily,
			ValueFontSize = primaryBarcode.ValueFontSize
		};

		var renderData = primaryBarcode.Symbology!.CreateRenderData(request);

		var svg = BarcodeExporter.ToSvgString(renderData);

		ExportedSvg = svg;
	}

	private void OnSymbologyComboBoxSelectionChanged(object? sender, SelectionChangedEventArgs e) {
		if (symbologyComboBox.SelectedItem is BarcodeSymbologyBase selectedSymbology) {
			primaryBarcode.Symbology = selectedSymbology;
			primaryBarcode.Value = selectedSymbology.ExampleValue;

			ExportSvg();
		}
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	public string? ExportedSvg {
		get => GetValue(ExportedSvgProperty);
		set => SetValue(ExportedSvgProperty, value);
	}

}
