using Avalonia.Controls.Documents;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <summary>
/// Represents a control for rendering a bullet preview.
/// </summary>
public class BulletPresenter : Decorator {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	static BulletPresenter() {
		PaddingProperty.OverrideDefaultValue<BulletPresenter>(new Thickness(6.0, 10.0, 6.0, 10.0));
	}

	public BulletPresenter() {
		// Flow direction must be left-to-right or else DrawingContext.DrawText will render mirrored text in RTL environments
		FlowDirection = FlowDirection.LeftToRight;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates <see cref="FormattedText"/> to render text.
	/// </summary>
	/// <param name="text">The text to display.</param>
	private FormattedText CreateFormattedText(string text) {
		var typeface = new Typeface(TextElement.GetFontFamily(this));
		var fontSize = TextElement.GetFontSize(this);
		var foreground = TextElement.GetForeground(this);

		var formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection, typeface, fontSize, foreground);

		return formattedText;
	}

	/// <summary>
	/// The view model in the data context.
	/// </summary>
	private BulletBarGalleryItemViewModel? ViewModel
		=> DataContext as BulletBarGalleryItemViewModel;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void Render(DrawingContext drawingContext) {
		var viewModel = ViewModel ?? new BulletBarGalleryItemViewModel(BulletKind.None);
		var kind = viewModel.Value;
		if (kind == BulletKind.None) {
			if (!string.IsNullOrEmpty(viewModel.Label)) {
				var formattedText = CreateFormattedText(viewModel.Label);
				var location = new Point((Bounds.Width - formattedText.Width) / 2.0, (Bounds.Height - formattedText.Height) / 2.0);
				drawingContext.DrawText(formattedText, location);
			}
		}
		else {
			var foreground = TextElement.GetForeground(this);

			var xCenter = Math.Round(Bounds.Width / 2.0, MidpointRounding.AwayFromZero);
			var yCenter = Math.Round(Bounds.Height / 2.0, MidpointRounding.AwayFromZero);

			const double Radius = 5.0;

			switch (kind) {
				case BulletKind.Circle:
					drawingContext.DrawEllipse(brush: null, new Pen(foreground, 1.0), new Point(xCenter, yCenter), Radius, Radius);
					break;
				case BulletKind.FilledCircle:
					drawingContext.DrawEllipse(foreground, pen: null, new Point(xCenter, yCenter), Radius, Radius);
					break;
				case BulletKind.FilledSquare:
					drawingContext.DrawRectangle(foreground, pen: null, new Rect(xCenter - Radius, yCenter - Radius, 2 * Radius, 2 * Radius));
					break;
				case BulletKind.Square:
					drawingContext.DrawRectangle(brush: null, new Pen(foreground, 1.0), new Rect(xCenter - Radius + 0.5, yCenter - Radius + 0.5, 2 * Radius - 1.0, 2 * Radius - 1.0));
					break;
			}
		}
	}

}
