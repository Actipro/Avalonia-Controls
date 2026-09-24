using Avalonia.Controls.Documents;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;

/// <summary>
/// Represents a control for rendering a symbol preview.
/// </summary>
[ToolboxItem(false)]
public class SymbolPresenter : Decorator {

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public SymbolPresenter() {
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
		var typeface = new Typeface(TextElement.GetFontFamily(this), FontStyle.Normal, FontWeight.Normal, FontStretch.Normal);
		var fontSize = TextElement.GetFontSize(this);
		var foreground = TextElement.GetForeground(this);

		var formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection, typeface, fontSize, foreground);

		return formattedText;
	}

	/// <summary>
	/// The view model in the data context.
	/// </summary>
	private SymbolBarGalleryItemViewModel? ViewModel
		=> DataContext as SymbolBarGalleryItemViewModel;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override void Render(DrawingContext context) {
		var symbol = ViewModel?.Value;
		if (!string.IsNullOrEmpty(symbol)) {
			var formattedText = CreateFormattedText(symbol);
			var location = new Point((Bounds.Width - formattedText.Width) / 2.0, (Bounds.Height - formattedText.Height) / 2.0);
			context.DrawText(formattedText, location);
		}
	}

}
