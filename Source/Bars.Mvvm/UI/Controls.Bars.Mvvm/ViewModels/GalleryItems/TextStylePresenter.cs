namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

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

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the class.
		/// </summary>
		static TextStylePresenter() {
			AffectsRender<TextStylePresenter>(BackgroundProperty);
		}

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public TextStylePresenter() {
			// Flow direction must be left-to-right or else DrawingContext.DrawText will render mirrored text in RTL environments
			this.FlowDirection = FlowDirection.LeftToRight;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates <see cref="FormattedText"/> to render the label.
		/// </summary>
		/// <param name="viewModel">The <see cref="TextStyleBarGalleryItemViewModel"/> to examine.</param>
		/// <returns>The <see cref="FormattedText"/> that was created.</returns>
		private FormattedText CreateFormattedText(TextStyleBarGalleryItemViewModel viewModel) {
			var textStyle = viewModel.Value ?? new TextStyle();
			var fontFamily = new FontFamily(textStyle.FontFamilyName);
			var fontStyle = textStyle.Italic ? FontStyle.Italic : FontStyle.Normal;
			var fontWeight = textStyle.Bold ? FontWeight.Bold : FontWeight.Normal;
			var typeface = new Typeface(fontFamily, fontStyle, fontWeight, FontStretch.Normal);
			var fontSize = textStyle.FontSize;
			var foreground = new SolidColorBrush(textStyle.TextColor);

			var formattedText = new FormattedText(viewModel.Label ?? string.Empty, CultureInfo.CurrentCulture, this.FlowDirection, typeface, fontSize, foreground);

			return formattedText;
		}

		/// <summary>
		/// Returns the margin based on the current height.
		/// </summary>
		/// <returns>The margin based on the current height.</returns>
		private double GetMarginForHeight()
			=> this.Bounds.Height >= 40.0 ? 4.0 : 2.0;
		
		/// <summary>
		/// The view model in the data context.
		/// </summary>
		private TextStyleBarGalleryItemViewModel? ViewModel 
			=> this.DataContext as TextStyleBarGalleryItemViewModel;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The <see cref="IBrush"/> for the background.
		/// </summary>
		public IBrush Background {
			get => GetValue(BackgroundProperty);
			set => SetValue(BackgroundProperty, value);
		}
		
		/// <inheritdoc/>
		protected override Size MeasureOverride(Size constraint)
			=> new Size(100.0, Math.Min(constraint.Height, 60.0));

		/// <inheritdoc/>
		public override void Render(DrawingContext context) {
			var viewModel = this.ViewModel;
			if (viewModel != null) {
				// Fill in the entire presenter with a Transparent background
				var bounds = new Rect(new Point(), this.Bounds.Size);
				context.DrawRectangle(Brushes.Transparent, pen: null, bounds);

				// Deflate by a margin amount appropriate for the presenter height and fill in the background...
				//   This allows any hover/selection highlights from the gallery item container to show in the margin area
				var margin = GetMarginForHeight();
				bounds = bounds.Inflate(-margin);
				context.DrawRectangle(this.Background, pen: null, bounds);

				var clipBounds = new Rect(
					bounds.Left + this.Padding.Left, 
					bounds.Top + this.Padding.Top, 
					Math.Max(0.0, bounds.Width - this.Padding.Left - this.Padding.Right), 
					Math.Max(0.0, bounds.Height - this.Padding.Top - this.Padding.Bottom)
				);

				var formattedText = this.CreateFormattedText(viewModel);
				var location = formattedText.Width > clipBounds.Width
					? new Point(clipBounds.Left, (this.Bounds.Height - formattedText.Height) / 2.0)  // Left-align
					: new Point((this.Bounds.Width - formattedText.Width) / 2.0, (this.Bounds.Height - formattedText.Height) / 2.0);  // Center

				// Draw the styled text
				using var pushedState = context.PushClip(clipBounds);
				context.DrawText(formattedText, location);
			}
		}
		
	}

}
