using ActiproSoftware.UI.Avalonia.Controls.Barcodes;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.DataVisualizationSamples.BarcodeQuickStarts;

public static class BarcodeSymbologies {

	public static IEnumerable<BarcodeSymbologyBase> All {
		get {
			yield return new QrCodeSymbology();
			yield return new MicroQrCodeSymbology();
			yield return new CodabarSymbology();
			yield return new Code39Symbology();
			yield return new Code39ExtendedSymbology();
			yield return new Code93Symbology();
			yield return new Code93ExtendedSymbology();
			yield return new Code128Symbology();
			yield return new GS1128Symbology();
			yield return new Ean13Symbology();
			yield return new Ean8Symbology();
			yield return new Interleaved2of5Symbology();
			yield return new Itf14Symbology();
			yield return new UpcASymbology();
			yield return new UpcESymbology();
		}
	}

}
