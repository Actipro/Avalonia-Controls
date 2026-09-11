using ActiproSoftware.Extensions;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;

/// <summary>
/// Represents a control for rendering a text style preview.
/// </summary>
[ToolboxItem(false)]
public class TextStylePresenter : Decorator {

	#region Dependency Properties

	/// <summary>
	/// Defines the <see cref="Background"/> property.
	/// </summary>
	public static readonly StyledProperty<IBrush> BackgroundProperty
		= AvaloniaProperty.Register<TextStylePresenter, IBrush>(nameof(Background), defaultValue: Brushes.White);

	#endregion Dependency Properties

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	static TextStylePresenter() {
		AffectsRender<TextStylePresenter>(BackgroundProperty);
	}

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public TextStylePresenter() {
		// Flow direction must be left-to-right or else DrawingContext.DrawText will render mirrored text in RTL environments
		FlowDirection = FlowDirection.LeftToRight;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Creates <see cref="FormattedText"/> to render the label.
	/// </summary>
	/// <param name="viewModel">The <see cref="TextStyleBarGalleryItemViewModel"/> to examine.</param>
	private FormattedText CreateFormattedText(TextStyleBarGalleryItemViewModel viewModel) {
		var textStyle = viewModel.Value ?? new TextStyle();
		var fontFamily = new FontFamily(textStyle.FontFamilyName);
		var fontStyle = textStyle.Italic ? FontStyle.Italic : FontStyle.Normal;
		var fontWeight = textStyle.Bold ? FontWeight.Bold : FontWeight.Normal;
		var typeface = new Typeface(fontFamily, fontStyle, fontWeight, FontStretch.Normal);
		var fontSize = textStyle.FontSize;
		var foreground = new SolidColorBrush(textStyle.TextColor);

		var formattedText = new FormattedText(viewModel.Label ?? string.Empty, CultureInfo.CurrentCulture, FlowDirection, typeface, fontSize, foreground);

		return formattedText;
	}

	/// <summary>
	/// Returns the margin based on the current height.
	/// </summary>
	private double GetMarginForHeight()
		=> Bounds.Height >= 40.0 ? 4.0 : 2.0;

	/// <summary>
	/// The view model in the data context.
	/// </summary>
	private TextStyleBarGalleryItemViewModel? ViewModel
		=> DataContext as TextStyleBarGalleryItemViewModel;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="IBrush"/> for the background.
	/// </summary>
	public IBrush Background {
		get => GetValue(BackgroundProperty);
		set => SetValue(BackgroundProperty, value);
	}

	/// <inheritdoc/>
	protected override Size MeasureOverride(Size constraint)
		=> new(100.0, Math.Min(constraint.Height, 60.0));

	/// <inheritdoc/>
	public override void Render(DrawingContext context) {
		if (ViewModel is { } viewModel) {
			// Fill in the entire presenter with a Transparent background
			var bounds = new Rect(new Point(), Bounds.Size);
			context.DrawRectangle(Brushes.Transparent, pen: null, bounds);

			// Deflate by a margin amount appropriate for the presenter height and fill in the background...
			//   This allows any hover/selection highlights from the gallery item container to show in the margin area
			var margin = GetMarginForHeight();
			bounds = bounds.Inflate(-margin);
			context.DrawRectangle(Background, pen: null, bounds);

			var clipBounds = new Rect(
				bounds.Left + Padding.Left,
				bounds.Top + Padding.Top,
				(bounds.Width - Padding.Left - Padding.Right).ClampToNonnegative(),
				(bounds.Height - Padding.Top - Padding.Bottom).ClampToNonnegative()
			);

			var formattedText = CreateFormattedText(viewModel);
			var location = (formattedText.Width > clipBounds.Width)
				? new Point(clipBounds.Left, (Bounds.Height - formattedText.Height) / 2.0)  // Left-align
				: new Point((Bounds.Width - formattedText.Width) / 2.0, (Bounds.Height - formattedText.Height) / 2.0);  // Center

			// Draw the styled text
			using var pushedState = context.PushClip(clipBounds);
			context.DrawText(formattedText, location);
		}
	}

}
