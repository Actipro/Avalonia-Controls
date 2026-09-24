using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Data;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

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

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// A <see cref="ColorBarGalleryItemViewModel"/> used to represent an automatic color.
	/// </summary>
	private ColorBarGalleryItemViewModel AutomaticColorGalleryItemViewModel {
		get => _automaticColorGalleryItemViewModel ??= new ColorBarGalleryItemViewModel(Colors.Black, category: string.Empty, "Automatic") {
			LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
		};
	}

	/// <summary>
	/// The categorized <see cref="ICollectionView"/> for border gallery item view models.
	/// </summary>
	private ICollectionView BorderGalleryItems
		=> _borderGalleryItems ??= CreateBorderBarGalleryItemViewModelsCollectionView();

	/// <summary>
	/// The categorized <see cref="ICollectionView"/> for bullet gallery item view models.
	/// </summary>
	private ICollectionView BulletGalleryItems
		=> _bulletGalleryItems ??= BulletBarGalleryItemViewModel.CreateDefaultCollectionView(categorize: true);

	/// <summary>
	/// Creates a categorized <see cref="ICollectionView"/> of gallery item view models representing a number of border styles, intended for use in a gallery.
	/// </summary>
	private ICollectionView CreateBorderBarGalleryItemViewModelsCollectionView() {
		return BarGalleryViewModel.CreateCollectionView([
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
		], categorize: true);
	}

	/// <inheritdoc cref="ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection" />
	private ICollectionView CreateFontColorPickerBarGalleryItemViewModelsCollectionView() {
		return BarGalleryViewModel.CreateCollectionView(
			new ColorBarGalleryItemViewModel[] {
				AutomaticColorGalleryItemViewModel
			}.Concat(ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()),
			categorize: true);
	}

	/// <inheritdoc cref="FontFamilyBarGalleryItemViewModel.CreateDefaultCollectionView" />
	private static ICollectionView CreateFontFamilyBarGalleryItemViewModelsCollectionView() {
		const string RecentlyUsedCategory = "Recently-Used Fonts";

		return BarGalleryViewModel.CreateCollectionView(
			new FontFamilyBarGalleryItemViewModel[] {
				new(FontSettings.DefaultFontFamilyName, RecentlyUsedCategory)
			}.Concat(FontFamilyBarGalleryItemViewModel.CreateDefaultCollection()),
			categorize: true);
	}

	/// <inheritdoc cref="ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection" />
	private ICollectionView CreateShadingColorPickerBarGalleryItemViewModelsCollectionView() {
		return BarGalleryViewModel.CreateCollectionView(
			ColorBarGalleryItemViewModel.CreateDefaultColorPickerCollection()
				.Concat([NoShadingColorGalleryItemViewModel]),
			categorize: true);
	}

	/// <summary>
	/// Creates a default collection of gallery item view models representing a number of symbols, intended for use in a gallery.
	/// </summary>
	public static IEnumerable<SymbolBarGalleryItemViewModel> CreateSymbolBarGalleryItemViewModelsCollection() {
		return [
			new("\u20AC") { Label = "Euro Sign" },
			new("\u00A3") { Label = "Pound Sign" },
			new("\u00A5") { Label = "Yen Sign" },
			new("\u00A9") { Label = "Copyright Sign" },
			new("\u00AE") { Label = "Registered Sign" },
			new("\u2122") { Label = "Trademark Sign" },
			new("\u00B1") { Label = "Plus-Minus Sign" },
			new("\u2248") { Label = "Almost Equal To" },
			new("\u2260") { Label = "Not Equal To" },
			new("\u2264") { Label = "Less-Than or Equal To" },
			new("\u2265") { Label = "Greater-Than or Equal To" },
			new("\u00F7") { Label = "Division Sign" },
			new("\u00D7") { Label = "Multiplication Sign" },
			new("\u221E") { Label = "Infinity" },
			new("\u00B5") { Label = "Micro Sign" },
			new("\u03B1") { Label = "Greek Small Letter Alpha" },
			new("\u03B2") { Label = "Greek Small Letter Beta" },
			new("\u03C0") { Label = "Greek Small Letter Pi" },
			new("\u2126") { Label = "Olm Sign" },
			new("\u2211") { Label = "N-Ary Summation" },
		];
	}

	/// <summary>
	/// Creates a default collection of gallery item view models representing a number of text styles, intended for use in a gallery.
	/// </summary>
	private static IEnumerable<TextStyleBarGalleryItemViewModel> CreateTextStyleBarGalleryItemViewModelsCollection() {
		return [
			new() { Label = "Normal", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) },
			new() { Label = "Heading 1", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading1FontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) },
			new() { Label = "Heading 2", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading2FontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) },
			new() { Label = "Heading 3", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.Heading3FontSize, Color.FromArgb(0xff, 0x1f, 0x37, 0x63)) },
			new() { Label = "Heading 4", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x2f, 0x54, 0x96)) { Italic = true } },
			new() { Label = "Title", Value = new TextStyle(FontSettings.HeadingFontFamilyName, FontSettings.TitleFontSize, Colors.Black) },
			new() { Label = "Subtitle", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x5a, 0x5a, 0x5a)) },
			new() { Label = "Subtle Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x40, 0x40, 0x40)) { Italic = true } },
			new() { Label = "Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) { Italic = true } },
			new() { Label = "Intense Emphasis", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x44, 0x72, 0xc4)) { Italic = true } },
			new() { Label = "Strong", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Colors.Black) { Bold = true } },
			new() { Label = "Quote", Value = new TextStyle(FontSettings.DefaultFontFamilyName, FontSettings.DefaultFontSize, Color.FromArgb(0xff, 0x40, 0x40, 0x40)) { Italic = true } },
		];
	}

	/// <summary>
	/// The categorized <see cref="ICollectionView"/> for font color picker gallery item view models.
	/// </summary>
	private ICollectionView FontColorPickerGalleryItems
		=> _fontColorPickerGalleryItems ??= CreateFontColorPickerBarGalleryItemViewModelsCollectionView();

	/// <summary>
	/// The categorized <see cref="ICollectionView"/> for font family gallery item view models.
	/// </summary>
	private ICollectionView FontFamilyGalleryItems
		=> _fontFamilyGalleryItems ??= CreateFontFamilyBarGalleryItemViewModelsCollectionView();

	/// <summary>
	/// A <see cref="ColorBarGalleryItemViewModel"/> used to represent a no shading color.
	/// </summary>
	private ColorBarGalleryItemViewModel NoShadingColorGalleryItemViewModel {
		get => _noShadingColorGalleryItemViewModel ??= new ColorBarGalleryItemViewModel(Colors.Transparent, category: string.Empty, "No Color") {
			LayoutBehavior = BarGalleryItemLayoutBehavior.MenuItem
		};
	}

	/// <summary>
	/// The categorized <see cref="ICollectionView"/> for numbering gallery item view models.
	/// </summary>
	private ICollectionView NumberingGalleryItems
		=> _numberingGalleryItems ??= NumberingBarGalleryItemViewModel.CreateDefaultCollectionView(categorize: true);

	/// <summary>
	/// The categorized <see cref="ICollectionView"/> for shading gallery item view models.
	/// </summary>
	private ICollectionView ShadingGalleryItems
		=> _shadingGalleryItems ??= CreateShadingColorPickerBarGalleryItemViewModelsCollectionView();

	/// <summary>
	/// The categorized <see cref="ICollectionView"/> for shape gallery item view models.
	/// </summary>
	private ICollectionView ShapeGalleryItems
		=> _shapeGalleryItems ??= ShapeBarGalleryItemViewModel.CreateDefaultCollectionView(categorize: true);

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="BarGalleryItemTemplateSelector"/> that will be assigned to <see cref="BarGalleryViewModelBase.ItemTemplateSelector"/>
	/// for each registered gallery view model.
	/// </summary>
	/// <value>The <see cref="BarGalleryItemTemplateSelector"/> that picks an <see cref="IDataTemplate"/> used to display the content for each gallery item.</value>
	public BarGalleryItemTemplateSelector GalleryItemTemplateSelector { get; } = new CustomBarGalleryItemTemplateSelector();

}
