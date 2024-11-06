using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using System;
using System.Globalization;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Represents a control for rendering a numbering preview.
	/// </summary>
	public class NumberingPresenter : Decorator {

		private const double LineVisualSpacer = 3.0;
		private const double TextLineSpacer = 5.0;

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="Kind"/> property.
		/// </summary>
		public static readonly StyledProperty<IBrush?> LineBrushProperty
			= AvaloniaProperty.Register<NumberingPresenter, IBrush?>(nameof(LineBrush));
		
		#endregion
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the class.
		/// </summary>
		static NumberingPresenter() {
			AffectsRender<UnderlinePresenter>(LineBrushProperty);

			PaddingProperty.OverrideDefaultValue<NumberingPresenter>(new Thickness(6.0, 10.0, 6.0, 10.0));
		}

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public NumberingPresenter() {
			// Flow direction must be left-to-right or else DrawingContext.DrawText will render mirrored text in RTL environments
			this.FlowDirection = FlowDirection.LeftToRight;
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates <see cref="FormattedText"/> to render text.
		/// </summary>
		/// <param name="text">The text to display.</param>
		/// <returns>The <see cref="FormattedText"/> that was created.</returns>
		private FormattedText CreateFormattedText(string text) {
			var typeface = new Typeface(TextElement.GetFontFamily(this));
			var fontSize = TextElement.GetFontSize(this);
			var foreground = TextElement.GetForeground(this);

			var formattedText = new FormattedText(text, CultureInfo.CurrentCulture, this.FlowDirection, typeface, fontSize, foreground);

			return formattedText;
		}
		
		/// <summary>
		/// Gets all of the text values that will be displayed in the preview.
		/// </summary>
		/// <param name="kind">The numbering kind whose text will be generating.</param>
		/// <param name="format">The format to be used.</param>
		/// <returns>An array of 3 text values.</returns>
		private string[] GetBulletTexts(NumberingKind kind, string format) {
			switch (kind) {
				case NumberingKind.ArabicNumeral:
					return new string[] { string.Format(format, "1"), string.Format(format, "2"), string.Format(format, "3") };
				case NumberingKind.LowerAlpha:
					return new string[] { string.Format(format, "a"), string.Format(format, "b"), string.Format(format, "c") };
				case NumberingKind.LowerRomanNumeral:
					return new string[] { string.Format(format, "i"), string.Format(format, "ii"), string.Format(format, "iii") };
				case NumberingKind.UpperAlpha:
					return new string[] { string.Format(format, "A"), string.Format(format, "B"), string.Format(format, "C") };
				case NumberingKind.UpperRomanNumeral:
					return new string[] { string.Format(format, "I"), string.Format(format, "II"), string.Format(format, "III") };
				default:
					return new string[] { string.Empty, string.Empty, string.Empty };
			}
		}

		/// <summary>
		/// Gets the view model in the data context.
		/// </summary>
		/// <value>The view model in the data context.</value>
		private NumberingBarGalleryItemViewModel? ViewModel => this.DataContext as NumberingBarGalleryItemViewModel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The <see cref="Brush"/> used for lines.
		/// </summary>
		public IBrush? LineBrush {
			get => (IBrush?)GetValue(LineBrushProperty);
			set => SetValue(LineBrushProperty, value);
		}

		/// <inheritdoc/>
		protected override Size MeasureOverride(Size constraint) {
			var formattedText = this.CreateFormattedText("1");
			var extent = Math.Round(this.Padding.Top + 3 * formattedText.Height + 2 * TextLineSpacer + this.Padding.Bottom, MidpointRounding.AwayFromZero);
			
			return new Size(extent, extent);
		}

		/// <inheritdoc/>
		public override void Render(DrawingContext drawingContext) {
			var viewModel = this.ViewModel ?? new NumberingBarGalleryItemViewModel(NumberingKind.None);
			if (viewModel.Value == NumberingKind.None) {
				if (viewModel.Label is not null) {
					var formattedText = this.CreateFormattedText(viewModel.Label);
					var location = new Point((this.Bounds.Width - formattedText.Width) / 2.0, (this.Bounds.Height - formattedText.Height) / 2.0);
					drawingContext.DrawText(formattedText, location);
				}
			}
			else {
				var bulletTexts = this.GetBulletTexts(viewModel.Value, viewModel.Format);
				var line1FormattedText = this.CreateFormattedText(bulletTexts[0]);
				var line2FormattedText = this.CreateFormattedText(bulletTexts[1]);
				var line3FormattedText = this.CreateFormattedText(bulletTexts[2]);

				var yCenter = Math.Round(this.Bounds.Height / 2.0, MidpointRounding.AwayFromZero);
				var lineVisualPen = new Pen(this.LineBrush, 2.0);

				var location = new Point(this.Padding.Left, yCenter - line2FormattedText.Height / 2.0 - TextLineSpacer - line1FormattedText.Height);
				drawingContext.DrawText(line1FormattedText, location);
				drawingContext.DrawLine(lineVisualPen, 
					new Point(location.X + line1FormattedText.Width + LineVisualSpacer, location.Y + line1FormattedText.Height / 2.0), 
					new Point(this.Bounds.Width - this.Padding.Right, location.Y + line1FormattedText.Height / 2.0));

				location = new Point(this.Padding.Left, yCenter - line2FormattedText.Height / 2.0);
				drawingContext.DrawText(line2FormattedText, location);
				drawingContext.DrawLine(lineVisualPen, 
					new Point(location.X + line2FormattedText.Width + LineVisualSpacer, location.Y + line2FormattedText.Height / 2.0), 
					new Point(this.Bounds.Width - this.Padding.Right, location.Y + line2FormattedText.Height / 2.0));

				location = new Point(this.Padding.Left, yCenter + line2FormattedText.Height / 2.0 + TextLineSpacer);
				drawingContext.DrawText(line3FormattedText, location);
				drawingContext.DrawLine(lineVisualPen, 
					new Point(location.X + line3FormattedText.Width + LineVisualSpacer, location.Y + line3FormattedText.Height / 2.0), 
					new Point(this.Bounds.Width - this.Padding.Right, location.Y + line3FormattedText.Height / 2.0));
			}
		}

	}

}
