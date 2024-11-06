using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using System.Collections.Generic;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	partial class BarManager {

		private ICollectionView? _borderGalleryItems;
		private ICollectionView? _bulletGalleryItems;
		private ICollectionView? _fontColorPickerGalleryItems;
		private ICollectionView? _fontFamilyGalleryItems;
		private ICollectionView? _numberingGalleryItems;
		private ICollectionView? _shadingGalleryItems;
		private ICollectionView? _shapeGalleryItems;

		private ColorBarGalleryItemViewModel? _automaticColorGalleryItemViewModel;
		private ColorBarGalleryItemViewModel? _noShadingColorGalleryItemViewModel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets a <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.
		/// </summary>
		/// <value>A <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.</value>
		private ColorBarGalleryItemViewModel AutomaticColorGalleryItemViewModel {
			get {
				if (_automaticColorGalleryItemViewModel == null) {
					_automaticColorGalleryItemViewModel = new ColorBarGalleryItemViewModel(Colors.Black, category: string.Empty, "Automatic") {
						LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
					};
				}

				return _automaticColorGalleryItemViewModel;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="ICollectionView"/> for border gallery item view models.
		/// </summary>
		/// <value>A <see cref="ICollectionView"/>.</value>
		private ICollectionView BorderGalleryItems {
			get {
				if (_borderGalleryItems is null)
					_borderGalleryItems = CreateBorderBarGalleryItemViewModelsCollectionView();
				return _borderGalleryItems;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="ICollectionView"/> for bullet gallery item view models.
		/// </summary>
		/// <value>A <see cref="ICollectionView"/>.</value>
		private ICollectionView BulletGalleryItems {
			get {
				if (_bulletGalleryItems is null)
					_bulletGalleryItems = BulletBarGalleryItemViewModel.CreateDefaultCollectionView(categorize: true);
				return _bulletGalleryItems;
			}
		}

		/// <summary>
		/// Creates a categorized <see cref="ICollectionView"/> of gallery item view models representing a number of border styles, intended for use in a gallery.
		/// </summary>
		/// <returns>The <see cref="ICollectionView"/> of gallery item view models that was created.</returns>
		private ICollectionView CreateBorderBarGalleryItemViewModelsCollectionView() {
			return BarGalleryViewModel.CreateCollectionView(new BorderBarGalleryItemViewModel[] {
				// Edge Borders
				new BorderBarGalleryItemViewModel(BorderKind.Bottom, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Bottom Border")
					{ KeyTipText = "B", Icon = ImageProvider.GetImage(BarControlKeys.BorderBottomGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Top, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Top Border")
					{ KeyTipText = "T", Icon = ImageProvider.GetImage(BarControlKeys.BorderTopGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Left, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Left Border")
					{ KeyTipText = "L", Icon = ImageProvider.GetImage(BarControlKeys.BorderLeftGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Right, BorderBarGalleryItemViewModel.EdgeBordersCategory, "Right Border")
					{ KeyTipText = "R", Icon = ImageProvider.GetImage(BarControlKeys.BorderRightGalleryItem, BarImageSize.Small) },

				// Other Borders
				new BorderBarGalleryItemViewModel(BorderKind.None, BorderBarGalleryItemViewModel.OtherBordersCategory, "No Border")
					{ KeyTipText = "N", Icon = ImageProvider.GetImage(BarControlKeys.BorderNoneGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.All, BorderBarGalleryItemViewModel.OtherBordersCategory, "All Borders")
					{ KeyTipText = "A", Icon = ImageProvider.GetImage(BarControlKeys.BorderAllGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Outside, BorderBarGalleryItemViewModel.OtherBordersCategory, "Outside Borders")
					{ KeyTipText = "O", Icon = ImageProvider.GetImage(BarControlKeys.BorderOutsideGalleryItem, BarImageSize.Small) },
				new BorderBarGalleryItemViewModel(BorderKind.Inside, BorderBarGalleryItemViewModel.OtherBordersCategory, "Inside Borders")
					{ KeyTipText = "I", Icon = ImageProvider.GetImage(BarControlKeys.BorderInsideGalleryItem, BarImageSize.Small) },
			}, categorize: true);
		}

		/// <inheritdoc cref="ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection" />
		private ICollectionView CreateFontColorPickerBarGalleryItemViewModelsCollectionView() {
			return BarGalleryViewModel.CreateCollectionView(
				new ColorBarGalleryItemViewModel[] {
					this.AutomaticColorGalleryItemViewModel
				}.Concat(ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()),
			categorize: true);
		}

		/// <inheritdoc cref="FontFamilyBarGalleryItemViewModel.CreateDefaultCollectionView" />
		private static ICollectionView CreateFontFamilyBarGalleryItemViewModelsCollectionView() {
			const string RecentlyUsedCategory = "Recently-Used Fonts";

			return BarGalleryViewModel.CreateCollectionView(
				new FontFamilyBarGalleryItemViewModel[] {
					new FontFamilyBarGalleryItemViewModel(FontSettings.DefaultFontFamilyName, RecentlyUsedCategory)
				}.Concat(FontFamilyBarGalleryItemViewModel.CreateDefaultCollection()),
			categorize: true);
		}

		/// <inheritdoc cref="ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection" />
		private ICollectionView CreateShadingColorPickerBarGalleryItemViewModelsCollectionView() {
			return BarGalleryViewModel.CreateCollectionView(
				ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()
					.Concat(new ColorBarGalleryItemViewModel[] {
						this.NoShadingColorGalleryItemViewModel
					}),
				categorize: true);
		}

		/// <summary>
		/// Creates a default collection of gallery item view models representing a number of symbols, intended for use in a gallery.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<SymbolBarGalleryItemViewModel> CreateSymbolBarGalleryItemViewModelsCollection() {
			return new SymbolBarGalleryItemViewModel[] {
				new SymbolBarGalleryItemViewModel("\u20AC") { Label = "Euro Sign" },
				new SymbolBarGalleryItemViewModel("\u00A3") { Label = "Pound Sign" },
				new SymbolBarGalleryItemViewModel("\u00A5") { Label = "Yen Sign" },
				new SymbolBarGalleryItemViewModel("\u00A9") { Label = "Copyright Sign" },
				new SymbolBarGalleryItemViewModel("\u00AE") { Label = "Registered Sign" },
				new SymbolBarGalleryItemViewModel("\u2122") { Label = "Trademark Sign" },
				new SymbolBarGalleryItemViewModel("\u00B1") { Label = "Plus-Minus Sign" },
				new SymbolBarGalleryItemViewModel("\u2248") { Label = "Almost Equal To" },
				new SymbolBarGalleryItemViewModel("\u2260") { Label = "Not Equal To" },
				new SymbolBarGalleryItemViewModel("\u2264") { Label = "Less-Than or Equal To" },
				new SymbolBarGalleryItemViewModel("\u2265") { Label = "Greater-Than or Equal To" },
				new SymbolBarGalleryItemViewModel("\u00F7") { Label = "Division Sign" },
				new SymbolBarGalleryItemViewModel("\u00D7") { Label = "Multiplication Sign" },
				new SymbolBarGalleryItemViewModel("\u221E") { Label = "Infinity" },
				new SymbolBarGalleryItemViewModel("\u00B5") { Label = "Micro Sign" },
				new SymbolBarGalleryItemViewModel("\u03B1") { Label = "Greek Small Letter Alpha" },
				new SymbolBarGalleryItemViewModel("\u03B2") { Label = "Greek Small Letter Beta" },
				new SymbolBarGalleryItemViewModel("\u03C0") { Label = "Greek Small Letter Pi" },
				new SymbolBarGalleryItemViewModel("\u2126") { Label = "Olm Sign" },
				new SymbolBarGalleryItemViewModel("\u2211") { Label = "N-Ary Summation" },
			};
		}

		/// <summary>
		/// Creates a default collection of gallery item view models representing a number of text styles, intended for use in a gallery.
		/// </summary>
		/// <returns>The collection of gallery item view models that was created.</returns>
		private static IEnumerable<TextStyleBarGalleryItemViewModel> CreateTextStyleBarGalleryItemViewModelsCollection() {
			return new TextStyleBarGalleryItemViewModel[] {
				new TextStyleBarGalleryItemViewModel() { Label = "Normal", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) },
				new TextStyleBarGalleryItemViewModel() { Label = "Heading 1", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading1FontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) },
				new TextStyleBarGalleryItemViewModel() { Label = "Heading 2", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading2FontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) },
				new TextStyleBarGalleryItemViewModel() { Label = "Heading 3", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading3FontSize, Color.FromArgb(0xff, 0x1f, 0x37, 0x63)) },
				new TextStyleBarGalleryItemViewModel() { Label = "Heading 4", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) { Italic = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Title", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.TitleFontSize, Colors.Black) },
				new TextStyleBarGalleryItemViewModel() { Label = "Subtitle", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x5a, 0x5a, 0x5a)) },
				new TextStyleBarGalleryItemViewModel() { Label = "Subtle Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x40, 0x40, 0x40)) { Italic = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) { Italic = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Intense Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x44, 0x72, 0xc4)) { Italic = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Strong", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) { Bold = true } },
				new TextStyleBarGalleryItemViewModel() { Label = "Quote", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x40, 0x40, 0x40)) { Italic = true } },
			};
		}

		/// <summary>
		/// Gets the categorized <see cref="ICollectionView"/> for font color picker gallery item view models.
		/// </summary>
		/// <value>A <see cref="ICollectionView"/>.</value>
		private ICollectionView FontColorPickerGalleryItems {
			get {
				if (_fontColorPickerGalleryItems is null)
					_fontColorPickerGalleryItems = CreateFontColorPickerBarGalleryItemViewModelsCollectionView();
				return _fontColorPickerGalleryItems;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="ICollectionView"/> for font family gallery item view models.
		/// </summary>
		/// <value>A <see cref="ICollectionView"/>.</value>
		private ICollectionView FontFamilyGalleryItems {
			get {
				if (_fontFamilyGalleryItems is null)
					_fontFamilyGalleryItems = CreateFontFamilyBarGalleryItemViewModelsCollectionView();
				return _fontFamilyGalleryItems;
			}
		}

		/// <summary>
		/// Gets a <see cref="ColorBarGalleryItemViewModel"/> used to represent a no shading color.
		/// </summary>
		/// <value>A <see cref="ColorBarGalleryItemViewModel"/> used to represent a no shading color.</value>
		private ColorBarGalleryItemViewModel NoShadingColorGalleryItemViewModel {
			get {
				if (_noShadingColorGalleryItemViewModel == null) {
					_noShadingColorGalleryItemViewModel = new ColorBarGalleryItemViewModel(Colors.Transparent, category: string.Empty, "No Color") {
						LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
					};
				}

				return _noShadingColorGalleryItemViewModel;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="ICollectionView"/> for numbering gallery item view models.
		/// </summary>
		/// <value>A <see cref="ICollectionView"/>.</value>
		private ICollectionView NumberingGalleryItems {
			get {
				if (_numberingGalleryItems is null)
					_numberingGalleryItems = NumberingBarGalleryItemViewModel.CreateDefaultCollectionView(categorize: true);
				return _numberingGalleryItems;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="ICollectionView"/> for shading gallery item view models.
		/// </summary>
		/// <value>A <see cref="ICollectionView"/>.</value>
		private ICollectionView ShadingGalleryItems {
			get {
				if (_shadingGalleryItems is null)
					_shadingGalleryItems = CreateShadingColorPickerBarGalleryItemViewModelsCollectionView();
				return _shadingGalleryItems;
			}
		}

		/// <summary>
		/// Gets the categorized <see cref="ICollectionView"/> for shape gallery item view models.
		/// </summary>
		/// <value>A <see cref="ICollectionView"/>.</value>
		private ICollectionView ShapeGalleryItems {
			get {
				if (_shapeGalleryItems is null)
					_shapeGalleryItems = ShapeBarGalleryItemViewModel.CreateDefaultCollectionView(categorize: true);
				return _shapeGalleryItems;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets the <see cref="BarGalleryItemTemplateSelector"/> that will be assigned to <see cref="BarGalleryViewModelBase.ItemTemplateSelector"/>
		/// for each registered gallery view model.
		/// </summary>
		/// <value>The <see cref="BarGalleryItemTemplateSelector"/> that picks an <see cref="IDataTemplate"/> used to display the content for each gallery item.</value>
		public BarGalleryItemTemplateSelector GalleryItemTemplateSelector { get; } = new CustomBarGalleryItemTemplateSelector();

	}

}
