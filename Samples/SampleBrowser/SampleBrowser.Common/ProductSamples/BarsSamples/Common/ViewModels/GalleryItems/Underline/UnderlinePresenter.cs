using ActiproSoftware.Extensions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using System.Globalization;
using System;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Primitives;
using Avalonia.Media.Immutable;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Represents a control for rendering an underline preview.
	/// </summary>
	public class UnderlinePresenter : Decorator {

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="Kind"/> property.
		/// </summary>
		public static readonly StyledProperty<UnderlineKind> KindProperty
			= AvaloniaProperty.Register<UnderlinePresenter, UnderlineKind>(nameof(Kind), defaultValue: UnderlineKind.None);
		
		#endregion
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the class.
		/// </summary>
		static UnderlinePresenter() {
			AffectsRender<UnderlinePresenter>(KindProperty);

			PaddingProperty.OverrideDefaultValue<UnderlinePresenter>(new Thickness(8.0, 5.0, 8.0, 5.0));
		}

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public UnderlinePresenter() {
			// Flow direction must be left-to-right or else DrawingContext.DrawText will render mirrored text in RTL environments
			this.FlowDirection = FlowDirection.LeftToRight;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates <see cref="FormattedText"/> to render the text "None".
		/// </summary>
		/// <returns>The <see cref="FormattedText"/> that was created.</returns>
		private FormattedText CreateNoneFormattedText() {
			var typeface = new Typeface(TextElement.GetFontFamily(this));
			var fontSize = TextElement.GetFontSize(this);
			var foreground = TextElement.GetForeground(this);

			var formattedText = new FormattedText("None", CultureInfo.CurrentCulture, this.FlowDirection, typeface, fontSize, foreground);

			return formattedText;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The kind of underline.
		/// </summary>
		public UnderlineKind Kind {
			get => GetValue(KindProperty);
			set => SetValue(KindProperty, value);
		}

		/// <inheritdoc/>
		protected override Size MeasureOverride(Size constraint) {
			var formattedText = this.CreateNoneFormattedText();
			
			return new Size(
				Math.Ceiling(this.Padding.Left + Math.Max(120.0, formattedText.WidthIncludingTrailingWhitespace) + this.Padding.Right),
				Math.Round(this.Padding.Top + formattedText.Height + this.Padding.Bottom, MidpointRounding.AwayFromZero)
				);
		}

		/// <inheritdoc/>
		public override void Render(DrawingContext drawingContext) {
			var x1 = this.Padding.Left + 0.5;
			var x2 = this.Bounds.Width - this.Padding.Right - 1.0;
			var y = ((this.Bounds.Height - 1.0) / 2.0).Round() + 0.5;

			switch (this.Kind) {
				case UnderlineKind.Underline: {
					var pen = new Pen(TextElement.GetForeground(this), 1.0);
					drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
					break;
				}
				case UnderlineKind.DoubleUnderline: {
					var pen = new Pen(TextElement.GetForeground(this), 1.0);
					drawingContext.DrawLine(pen, new Point(x1, y - 1.0), new Point(x2, y - 1.0));
					drawingContext.DrawLine(pen, new Point(x1, y + 1.0), new Point(x2, y + 1.0));
					break;
				}
				case UnderlineKind.ThickUnderline: {
					var pen = new Pen(TextElement.GetForeground(this), 2.0);
					drawingContext.DrawLine(pen, new Point(x1, y - 0.5), new Point(x2, y - 0.5));
					break;
				}
				// NOTE: DashStyle.Dot doesn't render properly so excluding for now
				/*
				case UnderlineKind.DottedUnderline: {
					var pen = new Pen(TextElement.GetForeground(this), 1.0) { DashStyle = DashStyle.Dot };
					drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
					break;
				}
				*/
				case UnderlineKind.DashedUnderline: {
					var pen = new Pen(TextElement.GetForeground(this), 1.0) { DashStyle = DashStyle.Dash };
					drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
					break;
				}
				case UnderlineKind.DotDashUnderline: {
					var pen = new Pen(TextElement.GetForeground(this), 1.0) { DashStyle = DashStyle.DashDot };
					drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
					break;
				}
				case UnderlineKind.DotDotDashUnderline: {
					var pen = new Pen(TextElement.GetForeground(this), 1.0) { DashStyle = DashStyle.DashDotDot };
					drawingContext.DrawLine(pen, new Point(x1, y), new Point(x2, y));
					break;
				}
				case UnderlineKind.WaveUnderline: {
					var pen = new Pen(TextElement.GetForeground(this), 0.5);
					for (var x = x1 - 0.5; x < x2 - 1.0; x += 2.0) {
						if (x % 4.0 == 0.0) {
							// Draw up diagonal
							drawingContext.DrawLine(pen, new Point(x + 0.5, y + 1.0), new Point(x + 1.5, y));
						}
						else {
							// Draw down diagonal
							drawingContext.DrawLine(pen, new Point(x + 0.5, y - 1.0), new Point(x + 1.5, y));
						}
					}
					break;
				}
				default: {  // None
					var formattedText = this.CreateNoneFormattedText();
					var location = new Point(x1 - 0.5, (this.Bounds.Height - formattedText.Height) / 2.0);

					drawingContext.DrawText(formattedText, location);
					break;
				}
			}
		}

	}

}
