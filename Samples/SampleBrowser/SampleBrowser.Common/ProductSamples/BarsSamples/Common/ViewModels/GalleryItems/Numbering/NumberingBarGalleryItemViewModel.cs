using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;
using System;
using System.Collections.Generic;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <summary>
	/// Represents a numbering style as a gallery item view model for a bar gallery control.
	/// </summary>
	public class NumberingBarGalleryItemViewModel : BarGalleryItemViewModel<NumberingKind> {

		/// <summary>
		/// The name of the category for the numbering library.
		/// </summary>
		public const string DefaultCategory = "Numbering Library";

		/// <summary>
		/// The string format to use for numbering followed by a dot (e.g., <c>1.</c>, <c>2.</c>, etc.).
		/// </summary>
		public const string DotFormat = "{0}.";

		/// <summary>
		/// The string format to use for numbering followed by a parenthesis (e.g., <c>1)</c>, <c>2)</c>, etc.).
		/// </summary>
		public const string ParenthesisFormat = "{0})";
		
		private string _format = DotFormat;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with a default value and category using dot format.
		/// </summary>
		public NumberingBarGalleryItemViewModel()
			: this(value: default) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and a default category using dot format.
		/// </summary>
		/// <param name="value">The item's value.</param>
		public NumberingBarGalleryItemViewModel(NumberingKind value)
			: this(value, DefaultCategory) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and category using dot format.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public NumberingBarGalleryItemViewModel(NumberingKind value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value, category, and label using dot format.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public NumberingBarGalleryItemViewModel(NumberingKind value, string? category, string? label)
			: base(value, category, label) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		protected override string? CoerceLabel() {
			if (Value != NumberingKind.None) {
				var baseLabel = ConvertEnumValueToString(Value, useAttributes: true);
				if (Format == DotFormat)
					return baseLabel + " with Dots";
				else if (Format == ParenthesisFormat)
					return baseLabel + " with Parenthesis";
			}
			return base.CoerceLabel();
		}

		/// <summary>
		/// Creates a default collection of gallery item view models representing common numbering kinds.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<NumberingBarGalleryItemViewModel> CreateDefaultCollection() {
			return new NumberingBarGalleryItemViewModel[] {
				new NumberingBarGalleryItemViewModel(NumberingKind.None),
				new NumberingBarGalleryItemViewModel(NumberingKind.ArabicNumeral) { Format = DotFormat },
				new NumberingBarGalleryItemViewModel(NumberingKind.ArabicNumeral) { Format = ParenthesisFormat },
				new NumberingBarGalleryItemViewModel(NumberingKind.UpperRomanNumeral),
				new NumberingBarGalleryItemViewModel(NumberingKind.LowerRomanNumeral),
				new NumberingBarGalleryItemViewModel(NumberingKind.UpperAlpha) { Format = DotFormat },
				new NumberingBarGalleryItemViewModel(NumberingKind.UpperAlpha) { Format = ParenthesisFormat },
				new NumberingBarGalleryItemViewModel(NumberingKind.LowerAlpha) { Format = DotFormat },
				new NumberingBarGalleryItemViewModel(NumberingKind.LowerAlpha) { Format = ParenthesisFormat },
			};
		}
		
		/// <summary>
		/// Creates a <see cref="ICollectionView"/> of gallery item view models representing common numbering kinds, allowing for possible categorization and filtering.
		/// </summary>
		/// <param name="categorize">Whether the collection view should support categorization by including a group description based on <see cref="IBarGalleryItemViewModel.Category"/> property values.</param>
		/// <returns>The <see cref="ICollectionView"/> of gallery item view models that was created.</returns>
		public static ICollectionView CreateDefaultCollectionView(bool categorize)
			=> BarGalleryViewModel.CreateCollectionView(CreateDefaultCollection(), categorize);

		/// <summary>
		/// Gets or sets the string format used for displaying the numbering.
		/// </summary>
		/// <value>
		/// A string format value; e.g., <c>{0}.</c> to display a dot after the numbering.
		/// The default value is <see cref="DotFormat"/>.
		/// </value>
		/// <seealso cref="DotFormat"/>
		/// <seealso cref="ParenthesisFormat"/>
		public string Format {
			get => _format;
			set {
				if (_format != value) {
					if (string.IsNullOrEmpty(value))
						throw new ArgumentNullException(nameof(value));

					SetProperty(ref _format, value);
					OnPropertyChanged(nameof(Label));
				}
			}
		}

		/// <inheritdoc/>
		public override string ToString() {
			return $"{this.GetType().FullName}[Value='{this.Value}', Format='{this.Format}']";
		}

	}

}
