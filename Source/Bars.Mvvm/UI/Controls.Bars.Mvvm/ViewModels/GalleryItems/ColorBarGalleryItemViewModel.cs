using ActiproSoftware.Properties.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Media;
using System.Collections.Generic;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a gallery item view model for a color.
	/// </summary>
	public class ColorBarGalleryItemViewModel : BarGalleryItemViewModel<Color> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public ColorBarGalleryItemViewModel()  // Parameterless constructor required for XAML support
			: this(default) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified color.
		/// </summary>
		/// <param name="value">The color represented by the gallery item.</param>
		public ColorBarGalleryItemViewModel(Color value)
			: this(value, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified color and category.
		/// </summary>
		/// <param name="value">The color represented by the gallery item.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public ColorBarGalleryItemViewModel(Color value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified color, category, and label.
		/// </summary>
		/// <param name="value">The color represented by the gallery item.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public ColorBarGalleryItemViewModel(Color value, string? category, string? label)
			: base(value, category, label) { }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Appends color shade view models.
		/// </summary>
		/// <param name="viewModels">The collection to update.</param>
		/// <param name="baseViewModels">The base color gallery item view models to examine.</param>
		private static void AppendColorShadeViewModels(IList<ColorBarGalleryItemViewModel> viewModels, IList<ColorBarGalleryItemViewModel>? baseViewModels) {
			var hasColors = baseViewModels?.Any() == true;
			if (hasColors) {
				// Add base colors
				var shadeDictionary = new Dictionary<ColorBarGalleryItemViewModel, IList<NamedColorShade>>();
				foreach (var sourceViewModel in baseViewModels!.Where(x => x is not null)) {
					viewModels.Add(sourceViewModel);

					shadeDictionary[sourceViewModel] = ColorShadeGenerator.Generate(sourceViewModel.Value, sourceViewModel.Label ?? sourceViewModel.Value.ToString());
				}

				// Add shade colors
				var shadeCount = shadeDictionary.First().Value.Count;
				for (var shadeIndex = 0; shadeIndex < shadeCount; shadeIndex++) {
					foreach (var sourceViewModel in baseViewModels!.Where(x => x is not null)) {
						var shade = shadeDictionary[sourceViewModel][shadeIndex];

						var shadeViewModel = new ColorBarGalleryItemViewModel(shade.Color, sourceViewModel.Category, shade.Name);

						shadeViewModel.LayoutBehavior = shadeIndex == 0 
							? BarGalleryItemLayoutBehavior.GroupStart : (
								shadeIndex == shadeCount - 1 
								? BarGalleryItemLayoutBehavior.GroupEnd 
								: BarGalleryItemLayoutBehavior.GroupInner
							);

						viewModels.Add(shadeViewModel);
					}
				}
			}
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of gallery item view models representing a number of standard colors and their various shades, intended for use in a color picker gallery.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<ColorBarGalleryItemViewModel> CreateDefaultColorPickerCollection() {
			var viewModels = new List<ColorBarGalleryItemViewModel>();

			var standardColorsCategory = SR.GetString(SRName.UIGalleryItemCategoryStandardColorsText);
			var themeColorsCategory = SR.GetString(SRName.UIGalleryItemCategoryThemeColorsText);

			AppendColorShadeViewModels(viewModels, new ColorBarGalleryItemViewModel[] {
				new(UIColor.Parse("#ffffff").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorWhiteText)),
				new(UIColor.Parse("#000000").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlackText)),
				new(UIColor.Parse("#e7e6e6").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorLightGrayText)),
				new(UIColor.Parse("#44546a").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlueGrayText)),
				new(UIColor.Parse("#4472c4").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlueText)),
				new(UIColor.Parse("#ed7d31").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorOrangeText)),
				new(UIColor.Parse("#a5a5a5").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorGrayText)),
				new(UIColor.Parse("#ffc000").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorGoldText)),
				new(UIColor.Parse("#5b9bd5").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorBlueText)),
				new(UIColor.Parse("#70ad47").ToRgb(), themeColorsCategory, SR.GetString(SRName.UINamedColorGreenText)),
			});

			viewModels.AddRange(new ColorBarGalleryItemViewModel[] {
				new(UIColor.Parse("#c00000").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorDarkRedText)),
				new(UIColor.Parse("#ff0000").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorRedText)),
				new(UIColor.Parse("#ffc000").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorOrangeText)),
				new(UIColor.Parse("#ffff00").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorYellowText)),
				new(UIColor.Parse("#92d050").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorLightGreenText)),
				new(UIColor.Parse("#00b050").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorGreenText)),
				new(UIColor.Parse("#00b0f0").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorLightBlueText)),
				new(UIColor.Parse("#0070c0").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorBlueText)),
				new(UIColor.Parse("#002060").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorDarkBlueText)),
				new(UIColor.Parse("#7030a0").ToRgb(), standardColorsCategory, SR.GetString(SRName.UINamedColorPurpleText)),
			});

			return viewModels;
		}
		
		/// <summary>
		/// Creates a default collection of gallery item view models representing a number of background highlight colors, intended for use in a text highlight gallery.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<ColorBarGalleryItemViewModel> CreateDefaultTextHighlightCollection() {
			var category = SR.GetString(SRName.UIGalleryItemCategoryTextHighlightColorsText);

			return new ColorBarGalleryItemViewModel[] {
				new(Colors.Yellow, category, SR.GetString(SRName.UINamedColorYellowText)),
				new(Colors.Lime, category, SR.GetString(SRName.UINamedColorBrightGreenText)),
				new(Colors.Cyan, category, SR.GetString(SRName.UINamedColorTurquoiseText)),
				new(Colors.Magenta, category, SR.GetString(SRName.UINamedColorPinkText)),
				new(Colors.Blue, category, SR.GetString(SRName.UINamedColorBlueText)),
				new(Colors.Red, category, SR.GetString(SRName.UINamedColorRedText)),
				new(Colors.Navy, category, SR.GetString(SRName.UINamedColorDarkBlueText)),
				new(Colors.Teal, category, SR.GetString(SRName.UINamedColorTealText)),
				new(Colors.Green, category, SR.GetString(SRName.UINamedColorGreenText)),
				new(Colors.Purple, category, SR.GetString(SRName.UINamedColorVioletText)),
				new(Colors.Maroon, category, SR.GetString(SRName.UINamedColorDarkRedText)),
				new(Colors.Olive, category, SR.GetString(SRName.UINamedColorDarkYellowText)),
				new(Colors.Gray, category, SR.GetString(SRName.UINamedColorGray50Text)),
				new(Colors.Silver, category, SR.GetString(SRName.UINamedColorGray25Text)),
				new(Colors.Black, category, SR.GetString(SRName.UINamedColorBlackText)),
			};
		}

		/// <summary>
		/// Create a collection of gallery item view models representing the shades of colors from the specified base color gallery item view models.
		/// </summary>
		/// <param name="baseViewModels">The base color gallery item view models to examine.</param>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<ColorBarGalleryItemViewModel> CreateShadedCollection(params ColorBarGalleryItemViewModel[] baseViewModels) {
			var viewModels = new List<ColorBarGalleryItemViewModel>();

			AppendColorShadeViewModels(viewModels, baseViewModels);

			return viewModels;
		}
		
	}

}
