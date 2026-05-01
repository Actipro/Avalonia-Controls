---
title: "Exporting to Images"
page-title: "Exporting to Images - Data Visualization Barcodes"
order: 6
---

# Exporting Barcodes to Images

The [BarcodeExporter](xref:@ActiproUIRoot.Controls.Barcodes.Implementation.BarcodeExporter) class provides methods to export barcode render data to SVG (Scalable Vector Graphics) format, which can be used for printing, embedding in documents, or further conversion to raster formats.

Most applications render barcodes with the [BarcodePresenter](xref:@ActiproUIRoot.Controls.Barcodes.BarcodePresenter) control. Use [BarcodeExporter](xref:@ActiproUIRoot.Controls.Barcodes.Implementation.BarcodeExporter) when you also need standalone SVG output for workflows such as printing, generating documents, or sending branded QR Codes to external systems.

> [!NOTE]
> SVG is a vector format that scales without quality loss and is ideal for barcodes, which need to maintain sharpness at any size. SVG export also preserves advanced presentation features such as gradients, and QR Code module shape and finder pattern accents.

## Creating Barcode Render Data

To export a barcode, first generate render data with a symbology and a [BarcodeEncodingRequest](xref:@ActiproUIRoot.Controls.Barcodes.Implementation.BarcodeEncodingRequest).

```csharp
// Create or get the symbology; ideally, reuse a shared instance instead
var symbology = new Ean13Symbology();

// Create the encoding request
var request = new BarcodeEncodingRequest("5901234123457") {
	SupplementValue = "51242"
};
```

The request contains the encoded value and optional presentation settings such as brushes and QR Code module shape and finder pattern accents.

```csharp
// Create or get the symbology; ideally, reuse a shared instance instead
var symbology = new QrCodeSymbology() {
	ErrorCorrectionLevel = QrErrorCorrectionLevel.Quartile,
	FinderPatternOuterShapeKind = BarcodeShapeKind.RoundedRectangle,
	FinderPatternInnerShapeKind = BarcodeShapeKind.Circle,
	ModuleScale = 6,
};

// Create the encoding request
var request = new BarcodeEncodingRequest("https://example.com") {
	Background = new SolidColorBrush(Color.Parse("#F5FFF3")),
	Foreground = new SolidColorBrush(Color.Parse("#0C2B0C")),

	// Primarily used for QR Codes:
	ModuleShapeKind = BarcodeShapeKind.InsetCircle,
	PrimaryAccent = new SolidColorBrush(Color.Parse("#FD9929")),
	SecondaryAccent = new SolidColorBrush(Color.Parse("#E37700")),
};

// Obtain the render data
var renderData = symbology.CreateRenderData(request);
```

If your application already defines barcode styling on [BarcodePresenter](xref:@ActiproUIRoot.Controls.Barcodes.BarcodePresenter), apply equivalent brush and shape settings to the [BarcodeEncodingRequest](xref:@ActiproUIRoot.Controls.Barcodes.Implementation.BarcodeEncodingRequest) before export.

> [!TIP]
> Use [CreateRenderDataAsync](xref:@ActiproUIRoot.Controls.Barcodes.BarcodeSymbologyBase.CreateRenderDataAsync*) when generating render data asynchronously fits your workflow better.

## SVG Export

Use [BarcodeExporter.ToSvgString](xref:@ActiproUIRoot.Controls.Barcodes.Implementation.BarcodeExporter.ToSvgString*) to convert the render data to an SVG string:

```csharp
// Assume renderData was obtained from the workflow demonstrated above
var svgContent = BarcodeExporter.ToSvgString(renderData);
```

The generated SVG retains the render data's geometry, text, brushes, and accents.

## Save to File

Use [BarcodeExporter.SaveToSvgFile](xref:@ActiproUIRoot.Controls.Barcodes.Implementation.BarcodeExporter.SaveToSvgFile*) or [BarcodeExporter.SaveToSvgFileAsync](xref:@ActiproUIRoot.Controls.Barcodes.Implementation.BarcodeExporter.SaveToSvgFileAsync*) to write SVG output.

```csharp
// Assume renderData was obtained from the workflow demonstrated above

BarcodeExporter.SaveToSvgFile(renderData, "barcode.svg");

await BarcodeExporter.SaveToSvgFileAsync(renderData, "barcode.svg", CancellationToken.None);
```
