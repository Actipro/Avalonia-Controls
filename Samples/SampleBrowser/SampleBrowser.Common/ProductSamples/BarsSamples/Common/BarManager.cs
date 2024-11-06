using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Themes;
using Avalonia;
using Avalonia.Media;
using System;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Manages commands and controls to be populated on a bar control (e.g., ribbon or toolbar).
	/// </summary>
	public partial class BarManager {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="BarManager"/> class.
		/// </summary>
		public BarManager() {
			this.ControlViewModels = new BarControlViewModelCollection(ImageProvider);

			RegisterImages();
			RegisterControlViewModels();
			UpdateGalleryItemSizes();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Occurs when the user interface density has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> containing data related to this event.</param>
		private void OnUserInterfaceDensityChanged(object? sender, EventArgs e) {
			UpdateGalleryItemSizes();
		}

		/// <summary>
		/// Registers control view models with <see cref="ControlViewModels"/>.
		/// </summary>
		private void RegisterControlViewModels() {

			// Access the keymap for platform key gestures
			var keymap = Application.Current?.PlatformSettings?.HotkeyConfiguration;

			var viewModels = ControlViewModels;

			// TIP: The Label and KeyTipText of view models, when not explicitly defined, are automatically generated from the given Key and/or Label,
			//		so explicit values are only necessary if the generated values are not sufficient (i.e. labels must be localized)

			viewModels.Register(BarControlKeys.AlignCenter, key
				=> new BarToggleButtonViewModel(key, SetTextAlignmentCommand) { KeyTipText = "AC", Description = "Center content with the page.", CommandParameter = TextAlignment.Center });

			viewModels.Register(BarControlKeys.AlignJustify, key
				=> new BarToggleButtonViewModel(key, SetTextAlignmentCommand) { KeyTipText = "AJ", Description = "Distribute text equally between the margins.", CommandParameter = TextAlignment.Justify });

			viewModels.Register(BarControlKeys.AlignLeft, key
				=> new BarToggleButtonViewModel(key, SetTextAlignmentCommand) { KeyTipText = "AL", Description = "Align content with the left margin.", CommandParameter = TextAlignment.Left });

			viewModels.Register(BarControlKeys.AlignRight, key
				=> new BarToggleButtonViewModel(key, SetTextAlignmentCommand) { KeyTipText = "AR", Description = "Align content with the right margin.", CommandParameter = TextAlignment.Right });

			viewModels.Register(BarControlKeys.ApplyStyles, key
				=> new BarToggleButtonViewModel(key, "Apply Styles...", NotImplementedCommand) { Description = "Open the Apply Styles dialog." });

			viewModels.Register(BarControlKeys.BlankPageInsert, key
				=> new BarButtonViewModel(key, "Blank Page", "NP", NotImplementedCommand) { Description = "Add a blank page anywhere in your document." });

			viewModels.Register(BarControlKeys.Bold, key
				=> new BarToggleButtonViewModel(key, ToggleBoldCommand) { KeyTipText = "1", Description = "Make your text bold." });

			viewModels.Register(BarControlKeys.BookmarkInsert, key
				=> new BarButtonViewModel(key, "Bookmark", "K", NotImplementedCommand) { Description = "Bookmarks work with links to let you jump to a specific place in your document.", Title = "Insert a Bookmark" });

			viewModels.Register(BarControlKeys.Borders, key
				=> new BarSplitToggleButtonViewModel(key, NotImplementedCommand) {
					Description = "Add or remove borders from the selection.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
					MenuItems = {
						viewModels[BarControlKeys.BordersGallery],
						viewModels[BarControlKeys.BordersShadingDialog],
					}
				});

			viewModels.Register(BarControlKeys.BordersGallery, key
				=> new BarGalleryViewModel(key, "Borders", NotImplementedCommand, BorderGalleryItems) {
					HasCategoryHeaders = false,
					IsSelectionSupported = false,
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					UseMenuItemAppearance = true,
				});

			viewModels.Register(BarControlKeys.BordersShadingDialog, key
				=> new BarButtonViewModel(key, "Borders and Shading...", "S", NotImplementedCommand));

			viewModels.Register(BarControlKeys.Bullets, key
				=> new BarSplitButtonViewModel(key, NotImplementedCommand) {
					Description = "Create a bulleted list.",
					KeyTipText = "U",
					MenuItems = {
						viewModels[BarControlKeys.BulletsGallery],
						viewModels[BarControlKeys.DefineNewBullet],
					}
				});

			viewModels.Register(BarControlKeys.BulletsGallery, key
				=> {
					var bulletsGallery = new BarGalleryViewModel(key, "Bullets", NotImplementedCommand, BulletGalleryItems) {
						ItemTemplateSelector = this.GalleryItemTemplateSelector,
						MinMenuColumnCount = 3,
						MenuResizeMode = ControlResizeMode.Both,
						BelowMenuItems = {
							viewModels[BarControlKeys.DefineNewBullet]
						}
					};
					bulletsGallery.SelectItemByValueMatch<BulletBarGalleryItemViewModel>(i => i.Value == BulletKind.None);
					return bulletsGallery;
				});

			viewModels.Register(BarControlKeys.ChartInsert, key
				=> new BarButtonViewModel(key, "Chart", NotImplementedCommand) { Description = "Insert a chart into the document.", Title = "Add a Chart" });

			viewModels.Register(BarControlKeys.ClearFormatting, key
				=> new BarButtonViewModel(key, "Clear All Formatting", "E", ClearFormattingCommand) {
					Description = "Remove all formatting from the selection, leaving unformatted text.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
				});

			viewModels.Register(BarControlKeys.Copy, key
				=> new BarButtonViewModel(key, CopyCommand) { InputGesture = keymap?.Copy.FirstOrDefault(), Description = "Puts a copy of the selection on the Clipboard so you can paste it elsewhere." });

			viewModels.Register(BarControlKeys.CoverPageInsert, key
				=> new BarButtonViewModel(key, "Cover Page", "V", NotImplementedCommand) { Description = "Use a cover page." });

			viewModels.Register(BarControlKeys.CreateStyle, key
				=> new BarToggleButtonViewModel(key, "Create a Style...", "S", NotImplementedCommand) { Description = "Create a style based on the formatting of the selected text." });

			viewModels.Register(BarControlKeys.CrossReferenceInsert, key
				=> new BarButtonViewModel(key, "Cross-reference", "RF", NotImplementedCommand) { Description = "Refer to specific places in your document, such as headings and tables.", Title = "Insert Cross-reference" });

			viewModels.Register(BarControlKeys.Cut, key
				=> new BarButtonViewModel(key, CutCommand) {
					KeyTipText = "T",
					InputGesture = keymap?.Cut.FirstOrDefault(), 
					Description = "Removes the selection and puts it on the Clipboard so you can paste it elsewhere."
				});

			viewModels.Register(BarControlKeys.DecreaseFontSize, key
				=> new BarButtonViewModel(key, DecreaseFontSizeCommand) {
					KeyTipText = "FK",
					Description = "Make your text a bit smaller.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
				});

			viewModels.Register(BarControlKeys.DecreaseIndent, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { KeyTipText = "AO", Description = "Move your paragraph closer to the margin." });
			
			viewModels.Register(BarControlKeys.DateTimeInsert, key
				=> new BarButtonViewModel(key, "Date & Time", NotImplementedCommand) { Description = "Quickly add the current date and time.", Title = "Insert Date and Time" });

			viewModels.Register(BarControlKeys.DefineNewBullet, key
				=> new BarButtonViewModel(key, "Define New Bullet...", NotImplementedCommand));

			viewModels.Register(BarControlKeys.DefineNewNumberFormat, key
				=> new BarButtonViewModel(key, "Define New Number Format...", NotImplementedCommand));

			viewModels.Register(BarControlKeys.Delete, key
				=> new BarButtonViewModel(key, DeleteCommand) { Description = "Deletes the selection." });

			viewModels.Register(BarControlKeys.DropCapInsert, key
				=> new BarButtonViewModel(key, "Drop Cap", "RC", NotImplementedCommand) { Description = "Create a large capital letter at the beginning of a paragraph.", Title = "Add a Drop Cap" });

			viewModels.Register(BarControlKeys.EquationInsert, key
				=> new BarButtonViewModel(key, "Equation", NotImplementedCommand) { Description = "Add common mathematical equations to your document.", Title = "Insert an Equation" });

			viewModels.Register(BarControlKeys.Find, key
				=> new BarButtonViewModel(key, NotImplementedCommand) {
					KeyTipText = "FD",
					Description = "Find text or other content.",
					ToolBarItemVariantBehavior = ItemVariantBehavior.All,
				});

			viewModels.Register(BarControlKeys.FlowDirection, key
				=> new BarButtonViewModel(key, FlowDirectionCommand) {
					KeyTipText = "D",
					Description = "Toggles flow direction of the control so that you can see how right-to-left mode operates.",
				});

			viewModels.Register(BarControlKeys.Font, key
				=> new BarComboBoxViewModel(key, SetFontFamilyCommand, FontFamilyGalleryItems) {
					Description = "Pick a new font for your text.",
					IsEditable = true,
					IsPreviewEnabledWhenPopupClosed = true,
					IsStarSizingAllowed = true,
					IsUnmatchedTextAllowed = false,
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					KeyTipText = "FF",
					BelowMenuItems = {
						viewModels[BarControlKeys.FontMoreFontsDialog],
					},
					MenuResizeMode = ControlResizeMode.Vertical,
					RequestedWidth = 140,
				});

			viewModels.Register(BarControlKeys.FontColor, key
				=> new BarSplitButtonViewModel(key, SetFontColorCommand) {
					KeyTipText = "FC",
					Description = "Change the color of your text.",
					MenuItems = {
						viewModels[BarControlKeys.FontColorPicker],
						viewModels[BarControlKeys.FontColorMoreColorsDialog],
					}
				});

			viewModels.Register(BarControlKeys.FontColorMoreColorsDialog, key
				=> new BarButtonViewModel(key, "More Colors...", NotImplementedCommand));

			viewModels.Register(BarControlKeys.FontColorPicker, key
				=> new BarGalleryViewModel(key, "Font Colors", SetFontColorCommand, FontColorPickerGalleryItems) {
					ItemSpacing = 4,
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					MaxMenuColumnCount = 10,
					MinMenuColumnCount = 10,
					UseAccentedItemBorder = true,
				});

			viewModels.Register(BarControlKeys.FontMoreFontsDialog, key
				=> new BarButtonViewModel(key, "Search for More Fonts...", NotImplementedCommand));

			viewModels.Register(BarControlKeys.FontSize, key
				=> new BarComboBoxViewModel(key, "Size", "FS", SetFontSizeCommand, FontSizeBarGalleryItemViewModel.CreateDefaultCollection()) {
					Description = "Change the size of your text.",
					IsEditable = true,
					IsTextCompletionEnabled = false,
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					MenuResizeMode = ControlResizeMode.Vertical,
					RequestedWidth = 60,
					Title = "Font Size",
					UnmatchedTextCommand = UnknownFontSizeCommand,
				});

			viewModels.Register(BarControlKeys.FooterInsert, key
				=> new BarButtonViewModel(key, "Footer", NotImplementedCommand) { Description = "Footers repeat content at the bottom of each page.", Title = "Add a Footer" });

			viewModels.Register(BarControlKeys.FormatPainter, key
				=> new BarToggleButtonViewModel(key, NotImplementedCommand) {
					KeyTipText = "FP",
					Description = "Apply the selection's look to other content in the document.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
				});

			viewModels.Register(BarControlKeys.HeaderInsert, key
				=> new BarButtonViewModel(key, "Header", NotImplementedCommand) { Description = "Headers repeat content at the top of each page.", Title = "Add a Header" });

			viewModels.Register(BarControlKeys.Help, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { Description = "Display helpful documentation." });

			viewModels.Register(BarControlKeys.IncreaseFontSize, key
				=> new BarButtonViewModel(key, IncreaseFontSizeCommand) {
					KeyTipText = "FG",
					Description = "Make your text a bit bigger.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
				});

			viewModels.Register(BarControlKeys.IncreaseIndent, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { KeyTipText = "AI", Description = "Move your paragraph farther away from the margin." });

			viewModels.Register(BarControlKeys.Italic, key
				=> new BarToggleButtonViewModel(key, ToggleItalicCommand) { KeyTipText = "2", Description = "Italicize your text." });

			viewModels.Register(BarControlKeys.LineSpacing, key
				=> new BarPopupButtonViewModel(key) {
					KeyTipText = "K",
					Description = "Choose how much space appears between lines of text and between paragraphs.",
					Title = "Line and Paragraph Spacing",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
					MenuItems = {
						viewModels[BarControlKeys.LineSpacingGallery],
						viewModels[BarControlKeys.LineSpacingOptions],
					}
				});

			viewModels.Register(BarControlKeys.LineSpacingGallery, key
				=> new BarGalleryViewModel(key, "Line Spacing", NotImplementedCommand, LineSpacingBarGalleryItemViewModel.CreateDefaultCollection()) {
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					UseMenuItemAppearance = true,
				});

			viewModels.Register(BarControlKeys.LineSpacingOptions, key
				=> new BarButtonViewModel(key, "Line Spacing Options...", NotImplementedCommand));
			
			viewModels.Register(BarControlKeys.LinkInsert, key
				=> new BarButtonViewModel(key, "Insert Link...", NotImplementedCommand));
			
			viewModels.Register(BarControlKeys.LinkInsertMenu, key
				=> new BarSplitButtonViewModel(key, "Link", NotImplementedCommand) {
					Description = "Create a link in your document for quick access to webpages and files.",
					MenuItems = {
						viewModels[BarControlKeys.LinkInsert],
					}
				});

			viewModels.Register(BarControlKeys.New, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { Description = "Create a new blank document." });

			viewModels.Register(BarControlKeys.Numbering, key
				=> new BarSplitToggleButtonViewModel(key, NotImplementedCommand) {
					Description = "Create a numbered list.",
					MenuItems = {
						viewModels[BarControlKeys.NumberingGallery],
						viewModels[BarControlKeys.DefineNewNumberFormat],
					}
				});

			viewModels.Register(BarControlKeys.NumberingGallery, key
				=> {
					var numberingGallery = new BarGalleryViewModel(key, "Numbering", NotImplementedCommand, NumberingGalleryItems) {
						ItemTemplateSelector = this.GalleryItemTemplateSelector,
						MinMenuColumnCount = 3,
						MenuResizeMode = ControlResizeMode.Both,
						BelowMenuItems = {
							viewModels[BarControlKeys.DefineNewNumberFormat]
						}
					};
					numberingGallery.SelectItemByValueMatch<NumberingBarGalleryItemViewModel>(i => i.Value == NumberingKind.None);
					return numberingGallery;
				});

			viewModels.Register(BarControlKeys.ObjectInsert, key
				=> new BarButtonViewModel(key, "Object...", NotImplementedCommand));
			
			viewModels.Register(BarControlKeys.ObjectInsertMenu, key
				=> new BarSplitButtonViewModel(key, "Object", NotImplementedCommand) {
					Description = "Insert an embedded object.",
					MenuItems = {
						viewModels[BarControlKeys.ObjectInsert],
						viewModels[BarControlKeys.TextFileInsert],
					}
				});

			viewModels.Register(BarControlKeys.Open, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { Description = "Open an existing document." });

			viewModels.Register(BarControlKeys.PageBreakInsert, key
				=> new BarButtonViewModel(key, "Page Break", "B", NotImplementedCommand) { Description = "End the current page and move to the next page." });

			viewModels.Register(BarControlKeys.PageNumberInsert, key
				=> new BarButtonViewModel(key, "Page Number", NotImplementedCommand) { KeyTipText = "NU", Description = "Number the pages in your document.", Title = "Add Page Numbers" });

			viewModels.Register(BarControlKeys.Paste, key
				=> new BarButtonViewModel(key, PasteCommand) { InputGesture = keymap?.Paste.FirstOrDefault(), Description = "Adds content from the Clipboard into your document." });

			viewModels.Register(BarControlKeys.PasteMenu, key
				=> new BarSplitButtonViewModel(key, "Paste", "V", PasteCommand) {
					Description = "Adds content from the Clipboard into your document.",
					InputGesture = keymap?.Paste.FirstOrDefault(),
					MenuItems = {
						viewModels[BarControlKeys.Paste],
						viewModels[BarControlKeys.PasteSpecial],
					}
				});

			viewModels.Register(BarControlKeys.PasteSpecial, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { KeyTipText = "S" });

			viewModels.Register(BarControlKeys.PictureInsert, key
				=> new BarButtonViewModel(key, "Picture", NotImplementedCommand) { Description = "Insert a picture from your computer.", Title = "Insert Picture" });

			viewModels.Register(BarControlKeys.QuickPartsInsert, key
				=> new BarButtonViewModel(key, "Quick Parts", NotImplementedCommand) { Description = "A text box brings focus to its content.", Title = "Explore Quick Parts" });

			viewModels.Register(BarControlKeys.QuickStylesGallery, key
				=> new BarGalleryViewModel(key, "Styles", SetTextStyleCommand, CreateTextStyleBarGalleryItemViewModelsCollection()) {
					CanRibbonAutoScrollToSelectedItem = false,
					CollapsedButtonDescription = "Styles give your document a consistent, polished look.",
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					KeyTipText = "L",
					MinMenuColumnCount = 4,
					MenuResizeMode = ControlResizeMode.Both,
					BelowMenuItems = {
						viewModels[BarControlKeys.CreateStyle],
						viewModels[BarControlKeys.ClearFormatting],
						viewModels[BarControlKeys.ApplyStyles],
					}
				});

			viewModels.Register(BarControlKeys.Redo, key
				=> new BarButtonViewModel(key, RedoCommand) { KeyTipText = "AQ", InputGesture = keymap?.Redo.FirstOrDefault() });

			viewModels.Register(BarControlKeys.Replace, key
				=> new BarButtonViewModel(key, NotImplementedCommand) {
					KeyTipText = "R",
					Description = "Search for text you'd like to change and replace it with something else.",
					ToolBarItemVariantBehavior = ItemVariantBehavior.All,
				});

			viewModels.Register(BarControlKeys.Save, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { Description = "Save this document." });

			viewModels.Register(BarControlKeys.SearchForText, key
				=> new BarTextBoxViewModel(key, "Search", "H", SearchForTextCommand) {
					Description = "Search for text in the document.",
					RequestedWidth = 120,
					PlaceholderText = "(search text)",
				});

			viewModels.Register(BarControlKeys.SelectObjects, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { Description = "Select objects that are behind the text." });

			viewModels.Register(BarControlKeys.SelectAll, key
				=> new BarButtonViewModel(key, SelectAllCommand) { KeyTipText = "A", InputGesture = keymap?.SelectAll.FirstOrDefault(), Description = "Select all text and objects." });

			viewModels.Register(BarControlKeys.SelectMenu, key
				=> new BarPopupButtonViewModel(key, "Select", "SL") {
					Description = "Select text or objects.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
					MenuItems = {
						viewModels[BarControlKeys.SelectAll],
						viewModels[BarControlKeys.SelectObjects],
					}
				});

			viewModels.Register(BarControlKeys.Shading, key
				=> new BarSplitToggleButtonViewModel(key, NotImplementedCommand) {
					Description = "Change the color behind the selected text, paragraph, or table cell.",
					KeyTipText = "H",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
					MenuItems = {
						viewModels[BarControlKeys.ShadingColorPicker],
						viewModels[BarControlKeys.ShadingColorMoreColorsDialog],
					}
				});

			viewModels.Register(BarControlKeys.ShadingColorMoreColorsDialog, key
				=> new BarButtonViewModel(key, "More Colors...", NotImplementedCommand));

			viewModels.Register(BarControlKeys.ShadingColorPicker, key
				=> new BarGalleryViewModel(key, "Shading Colors", NotImplementedCommand, ShadingGalleryItems) {
					ItemSpacing = 4,
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					MaxMenuColumnCount = 10,
					MinMenuColumnCount = 10,
					UseAccentedItemBorder = true,
				});

			viewModels.Register(BarControlKeys.ShapesGallery, key
				=> new BarGalleryViewModel(key, "Shapes", NotImplementedCommand, ShapeGalleryItems) {
					IsSelectionSupported = false,
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
				});

			viewModels.Register(BarControlKeys.ShapesMenu, key
				=> new BarPopupButtonViewModel(key, "Shapes", "SH") { 
					Description = "Insert ready-made shapes, such as circles and squares.", 
					Title = "Draw a Shape",
					MenuItems = {
						viewModels[BarControlKeys.ShapesGallery],
					}
				});

			viewModels.Register(BarControlKeys.Share, key
				=> new BarButtonViewModel(key, NotImplementedCommand) { KeyTipText = "E", Description = "Share this document." });

			viewModels.Register(BarControlKeys.ShowApplicationButton, key
				=> new BarCheckBoxViewModel(key, "Application Button", "SA", ToggleApplicationButtonCommand) { Description = "Toggles the visibility of the application button." });

			viewModels.Register(BarControlKeys.ShowApplicationButtonImage, key
				=> new BarCheckBoxViewModel(key, "Application Button Image", "SI", ToggleApplicationButtonImageCommand) { Description = "Toggles if the application button uses an image for content." });

			viewModels.Register(BarControlKeys.ShowFooter, key
				=> new BarCheckBoxViewModel(key, "Footer", "SF", ToggleFooterCommand) { Description = "Toggles the visibility of the ribbon's footer, where notifications can be displayed." });

			viewModels.Register(BarControlKeys.ShowQuickAccessToolBar, key
				=> new BarCheckBoxViewModel(key, "Quick Access Toolbar", "SQ", ToggleQuickAccessToolBarCommand) { Description = "Toggles the visibility of the Quick Access Toolbar." });

			viewModels.Register(BarControlKeys.ShowSymbols, key
				=> new BarToggleButtonViewModel(key, NotImplementedCommand) {
					KeyTipText = "8",
					Description = "Show paragraph marks and other hidden symbols.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
				});

			viewModels.Register(BarControlKeys.SignatureLineInsert, key
				=> new BarButtonViewModel(key, "Signature Line", NotImplementedCommand) { Description = "Insert a signature line that specifies the individual who must sign.", Title = "Add a Signature Line" });

			viewModels.Register(BarControlKeys.SmartArtInsert, key
				=> new BarButtonViewModel(key, "SmartArt", NotImplementedCommand) { KeyTipText = "M", Description = "Insert a SmartArt graphic to visually communicate information.", Title = "Insert a SmartArt Graphic" });

			viewModels.Register(BarControlKeys.Sort, key
				=> new BarButtonViewModel(key, NotImplementedCommand) {
					KeyTipText = "SO",
					Description = "Arrange the selection in alphabetical or numerical order.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
				});

			viewModels.Register(BarControlKeys.StopHighlighting, key
				=> new BarButtonViewModel(key, StopHighlightingCommand));

			viewModels.Register(BarControlKeys.Strikethrough, key
				=> new BarToggleButtonViewModel(key, NotImplementedCommand) {
					KeyTipText = "4",
					Description = "Cross something out by drawing a line through it.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
				});

			viewModels.Register(BarControlKeys.SymbolInsertGallery, key
				=> new BarGalleryViewModel(key, "Symbols", InsertSymbolCommand, CreateSymbolBarGalleryItemViewModelsCollection()) {
					IsSelectionSupported = false,
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					MinMenuColumnCount = 5,
				});

			viewModels.Register(BarControlKeys.SymbolInsertMenu, key
				=> new BarPopupButtonViewModel(key, "Symbol", "U") {
					Description = "Add symbols that are not on your keyboard.", 
					Title = "Insert a Symbol",
					MenuItems = {
						viewModels[BarControlKeys.SymbolInsertGallery],
						viewModels[BarControlKeys.SymbolInsertMoreSymbolsDialog],
					}
				});
			
			viewModels.Register(BarControlKeys.SymbolInsertMoreSymbolsDialog, key
				=> new BarButtonViewModel(key, "More Symbols...", NotImplementedCommand));

			viewModels.Register(BarControlKeys.Subscript, key
				=> new BarToggleButtonViewModel(key, NotImplementedCommand) {
					KeyTipText = "5",
					Description = "Type very small letters just below the line of text.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
				});

			viewModels.Register(BarControlKeys.Superscript, key
				=> new BarToggleButtonViewModel(key, NotImplementedCommand) {
					KeyTipText = "6",
					Description = "Type very small letters just above the line of text.",
					ToolBarItemCollapseBehavior = ItemCollapseBehavior.Always,
				});

			viewModels.Register(BarControlKeys.TableDraw, key
				=> new BarButtonViewModel(key, "Draw Table", NotImplementedCommand));

			viewModels.Register(BarControlKeys.TableInsertDialog, key
				=> new BarButtonViewModel(key, "Insert Table...", NotImplementedCommand));

			viewModels.Register(BarControlKeys.TableInsertGallery, key
				=> new BarSizeSelectionMenuGalleryViewModel(key, "Insert Table", NotImplementedCommand) {
					Command = InsertTableCommand,
					DefaultHeadingText = "Insert Table",
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					SizeHeadingTextFormat = "{0}x{1} Table",
				});

			viewModels.Register(BarControlKeys.TableInsertMenu, key
				=> new BarPopupButtonViewModel(key, "Table") {
					Description = "A table is a great way to organize information within your document.",
					Title = "Add a Table",
					MenuItems = {
						viewModels[BarControlKeys.TableInsertGallery],
						viewModels[BarControlKeys.TableInsertDialog],
						viewModels[BarControlKeys.TableDraw],
					}
				});

			viewModels.Register(BarControlKeys.TextBoxInsert, key
				=> new BarButtonViewModel(key, "Text Box", "X", NotImplementedCommand) { Description = "A text box brings focus to its content.", Title = "Insert a Text Box" });
			
			viewModels.Register(BarControlKeys.TextFileInsert, key
				=> new BarButtonViewModel(key, "Text from File...", NotImplementedCommand));

			viewModels.Register(BarControlKeys.TextHighlightColor, key
				=> new BarSplitButtonViewModel(key, SetTextHighlightColorCommand) {
					KeyTipText = "I",
					Description = "Make your text pop by highlighting it in a bright color.",
					MenuItems = {
						viewModels[BarControlKeys.TextHighlightColorPicker],
						viewModels[BarControlKeys.StopHighlighting],
					}
				});

			viewModels.Register(BarControlKeys.TextHighlightColorPicker, key
				=> new BarGalleryViewModel(key, "Text Highlight Colors", SetTextHighlightColorCommand, ColorBarGalleryItemViewModel.CreateDefaultTextHighlightCollection()) {
					ItemSpacing = 4,
					ItemTemplateSelector = this.GalleryItemTemplateSelector,
					MaxMenuColumnCount = 5,
					MinMenuColumnCount = 5,
					UseAccentedItemBorder = true,
				});

			viewModels.Register(BarControlKeys.Underline, key
				=> new BarSplitToggleButtonViewModel(key, NotImplementedCommand) {
					KeyTipText = "3",
					Description = "Underline your text.",
					MenuItems = {
						viewModels[BarControlKeys.UnderlineGallery],
						viewModels[BarControlKeys.UnderlineMoreUnderlinesDialog],
					}
				});

			viewModels.Register(BarControlKeys.UnderlineGallery, key
				=> {
					var underlineGallery = new BarGalleryViewModel(key, "Underlines", NotImplementedCommand, UnderlineBarGalleryItemViewModel.CreateDefaultCollection()) {
						ItemTemplateSelector = this.GalleryItemTemplateSelector,
						MaxMenuColumnCount = 1,
						MenuResizeMode = ControlResizeMode.Vertical,
						BelowMenuItems = {
							viewModels[BarControlKeys.UnderlineMoreUnderlinesDialog]
						}
					};
					underlineGallery.SelectItemByValueMatch<UnderlineBarGalleryItemViewModel>(i => i.Value == UnderlineKind.None);
					return underlineGallery;
				});

			viewModels.Register(BarControlKeys.UnderlineMoreUnderlinesDialog, key
				=> new BarButtonViewModel(key, "More Underlines...", NotImplementedCommand));

			viewModels.Register(BarControlKeys.Undo, key
				=> new BarButtonViewModel(key, UndoCommand) { KeyTipText = "AZ", InputGesture = keymap?.Undo.FirstOrDefault() });

			viewModels.Register(BarControlKeys.WordArtInsert, key
				=> new BarButtonViewModel(key, "WordArt", NotImplementedCommand) { Description = "Add some artistic flair to your document using WordArt.", Title = "Insert WordArt" });

		}

		/// <summary>
		/// Registers images with <see cref="ImageProvider"/>.
		/// </summary>
		private void RegisterImages() {
			var imageProvider = ImageProvider;

			// IMPORTANT NOTE: By convention, images are registered using the same key as the controls that will use them. This allows view models
			// created by BarControlViewModelCollection to automatically locate and use the appropriate image for the control

			//
			// Standard images
			//

			imageProvider.Register(BarControlKeys.AlignCenter, options => CreateBitmapImage(options, "AlignTextCenter16.png"));
			imageProvider.Register(BarControlKeys.AlignJustify, options => CreateBitmapImage(options, "AlignTextJustify16.png"));
			imageProvider.Register(BarControlKeys.AlignLeft, options => CreateBitmapImage(options, "AlignTextLeft16.png"));
			imageProvider.Register(BarControlKeys.AlignRight, options => CreateBitmapImage(options, "AlignTextRight16.png"));
			imageProvider.Register(BarControlKeys.ApplyStyles, options => CreateBitmapImage(options, "ApplyStyles16.png"));
			imageProvider.Register(BarControlKeys.BackstageTabInfo, options => CreateBitmapImage(options, "HomeMono16.png", mediumFileName: null, "HomeMono32.png"));
			imageProvider.Register(BarControlKeys.BackstageTabNew, options => CreateBitmapImage(options, "New16.png", mediumFileName: null, "New32.png"));
			imageProvider.Register(BarControlKeys.BackstageTabOpen, options => CreateBitmapImage(options, "OpenMono16.png", mediumFileName: null, "OpenMono32.png"));
			imageProvider.Register(BarControlKeys.BlankPageInsert, options => CreateBitmapImage(options, "BlankPage16.png", mediumFileName: null, "BlankPage32.png"));
			imageProvider.Register(BarControlKeys.Bold, options => CreateBitmapImage(options, "Bold16.png"));
			imageProvider.Register(BarControlKeys.BookmarkInsert, options => CreateBitmapImage(options, "Bookmark16.png", mediumFileName: null, "Bookmark32.png"));
			imageProvider.Register(BarControlKeys.BorderAllGalleryItem, options => CreateBitmapImage(options, "BorderAll16.png"));
			imageProvider.Register(BarControlKeys.BorderBottomGalleryItem, options => CreateBitmapImage(options, "BorderBottom16.png"));
			imageProvider.Register(BarControlKeys.BorderInsideGalleryItem, options => CreateBitmapImage(options, "BorderInside16.png"));
			imageProvider.Register(BarControlKeys.BorderLeftGalleryItem, options => CreateBitmapImage(options, "BorderLeft16.png"));
			imageProvider.Register(BarControlKeys.BorderNoneGalleryItem, options => CreateBitmapImage(options, "BorderNone16.png"));
			imageProvider.Register(BarControlKeys.BorderOutsideGalleryItem, options => CreateBitmapImage(options, "BorderOutside16.png"));
			imageProvider.Register(BarControlKeys.BorderRightGalleryItem, options => CreateBitmapImage(options, "BorderRight16.png"));
			imageProvider.Register(BarControlKeys.Borders, options => CreateBitmapImage(options, "BorderBottom16.png"));
			imageProvider.Register(BarControlKeys.BorderTopGalleryItem, options => CreateBitmapImage(options, "BorderTop16.png"));
			imageProvider.Register(BarControlKeys.Bullets, options => CreateBitmapImage(options, "Bullets16.png"));
			imageProvider.Register(BarControlKeys.ChartInsert, options => CreateBitmapImage(options, "Chart16.png", mediumFileName: null, "Chart32.png"));
			imageProvider.Register(BarControlKeys.ClearFormatting, options => CreateBitmapImage(options, "ClearFormatting16.png"));
			imageProvider.Register(BarControlKeys.Copy, options => CreateBitmapImage(options, "Copy16.png"));
			imageProvider.Register(BarControlKeys.CoverPageInsert, options => CreateBitmapImage(options, "CoverPage16.png", mediumFileName: null, "CoverPage32.png"));
			imageProvider.Register(BarControlKeys.CreateStyle, options => CreateBitmapImage(options, "CreateStyle16.png"));
			imageProvider.Register(BarControlKeys.CrossReferenceInsert, options => CreateBitmapImage(options, "CrossReference16.png", mediumFileName: null, "CrossReference32.png"));
			imageProvider.Register(BarControlKeys.Cut, options => CreateBitmapImage(options, "Cut16.png"));
			imageProvider.Register(BarControlKeys.DateTimeInsert, options => CreateBitmapImage(options, "DateTime16.png"));
			imageProvider.Register(BarControlKeys.DecreaseFontSize, options => CreateBitmapImage(options, "ShrinkFont16.png"));
			imageProvider.Register(BarControlKeys.DecreaseIndent, options => CreateBitmapImage(options, "DecreaseIndent16.png"));
			imageProvider.Register(BarControlKeys.Delete, options => CreateBitmapImage(options, "Delete16.png"));
			imageProvider.Register(BarControlKeys.DropCapInsert, options => CreateBitmapImage(options, "DropCap16.png", mediumFileName: null, "DropCap32.png"));
			imageProvider.Register(BarControlKeys.EquationInsert, options => CreateBitmapImage(options, "Equation16.png", mediumFileName: null, "Equation32.png"));
			imageProvider.Register(BarControlKeys.Find, options => CreateBitmapImage(options, "Find16.png", mediumFileName: null, "Find32.png"));
			imageProvider.Register(BarControlKeys.FlowDirection, options => CreateBitmapImage(options, smallFileName: "FlowDirection16.png", mediumFileName: null, "FlowDirection32.png"));
			imageProvider.Register(BarControlKeys.FontColorMoreColorsDialog, options => CreateBitmapImage(options, "ColorPicker16.png"));
			imageProvider.Register(BarControlKeys.FooterInsert, options => CreateBitmapImage(options, "Footer16.png", mediumFileName: null, "Footer32.png"));
			imageProvider.Register(BarControlKeys.FormatPainter, options => CreateBitmapImage(options, "FormatPainter16.png"));
			imageProvider.Register(BarControlKeys.GroupClipboard, options => CreateBitmapImage(options, "Paste16.png", mediumFileName: null, "Paste32.png"));
			imageProvider.Register(BarControlKeys.GroupEditing, options => CreateBitmapImage(options, "Find16.png", mediumFileName: null, "Find32.png"));
			imageProvider.Register(BarControlKeys.GroupFont, options => CreateBitmapImage(options, "FontColor16.png", mediumFileName: null, "FontColor32.png"));
			imageProvider.Register(BarControlKeys.GroupHeaderFooter, options => CreateBitmapImage(options, "Header16.png", mediumFileName: null, "Header32.png"));
			imageProvider.Register(BarControlKeys.GroupIllustrations, options => CreateBitmapImage(options, "Shapes16.png", mediumFileName: null, "Shapes32.png"));
			imageProvider.Register(BarControlKeys.GroupLinks, options => CreateBitmapImage(options, "Hyperlink16.png", mediumFileName: null, "Hyperlink32.png"));
			imageProvider.Register(BarControlKeys.GroupOther, options => CreateBitmapImage(options, "Bold16.png"));
			imageProvider.Register(BarControlKeys.GroupPages, options => CreateBitmapImage(options, "CoverPage16.png", mediumFileName: null, "CoverPage32.png"));
			imageProvider.Register(BarControlKeys.GroupParagraph, options => CreateBitmapImage(options, "AlignTextCenter16.png", mediumFileName: null, "AlignTextCenter32.png"));
			imageProvider.Register(BarControlKeys.GroupShow, options => CreateBitmapImage(options, "ApplicationButtonDefault16.png"));
			imageProvider.Register(BarControlKeys.GroupStyles, options => CreateBitmapImage(options, "QuickStyles16.png"));
			imageProvider.Register(BarControlKeys.GroupSymbols, options => CreateBitmapImage(options, "Symbol16.png", mediumFileName: null, "Symbol32.png"));
			imageProvider.Register(BarControlKeys.GroupTables, options => CreateBitmapImage(options, "Table16.png", mediumFileName: null, "Table32.png"));
			imageProvider.Register(BarControlKeys.GroupText, options => CreateBitmapImage(options, "WordArt16.png", mediumFileName: null, "WordArt32.png"));
			imageProvider.Register(BarControlKeys.GroupUndo, options => CreateBitmapImage(options, "Undo16.png"));
			imageProvider.Register(BarControlKeys.HeaderInsert, options => CreateBitmapImage(options, "Header16.png", mediumFileName: null, "Header32.png"));
			imageProvider.Register(BarControlKeys.Help, options => CreateBitmapImage(options, "Help16.png", mediumFileName: null, "Help32.png"));
			imageProvider.Register(BarControlKeys.IncreaseFontSize, options => CreateBitmapImage(options, "GrowFont16.png"));
			imageProvider.Register(BarControlKeys.IncreaseIndent, options => CreateBitmapImage(options, "IncreaseIndent16.png"));
			imageProvider.Register(BarControlKeys.Italic, options => CreateBitmapImage(options, "Italic16.png"));
			imageProvider.Register(BarControlKeys.LineSpacing, options => CreateBitmapImage(options, "LineSpacing16.png"));
			imageProvider.Register(BarControlKeys.LinkInsert, options => CreateBitmapImage(options, "Hyperlink16.png", mediumFileName: null, "Hyperlink32.png"));
			imageProvider.Register(BarControlKeys.LinkInsertMenu, options => CreateBitmapImage(options, "Hyperlink16.png", mediumFileName: null, "Hyperlink32.png"));
			imageProvider.Register(BarControlKeys.New, options => CreateBitmapImage(options, "New16.png", mediumFileName: null, "New32.png"));
			imageProvider.Register(BarControlKeys.Numbering, options => CreateBitmapImage(options, "Numbering16.png"));
			imageProvider.Register(BarControlKeys.ObjectInsert, options => CreateBitmapImage(options, "Object16.png"));
			imageProvider.Register(BarControlKeys.ObjectInsertMenu, options => CreateBitmapImage(options, "Object16.png"));
			imageProvider.Register(BarControlKeys.Open, options => CreateBitmapImage(options, "Open16.png", mediumFileName: null, "Open32.png"));
			imageProvider.Register(BarControlKeys.PageBreakInsert, options => CreateBitmapImage(options, "PageBreak16.png", mediumFileName: null, "PageBreak32.png"));
			imageProvider.Register(BarControlKeys.PageNumberInsert, options => CreateBitmapImage(options, "PageNumber16.png", mediumFileName: null, "PageNumber32.png"));
			imageProvider.Register(BarControlKeys.Paste, options => CreateBitmapImage(options, "Paste16.png", "Paste24.png", "Paste32.png"));
			imageProvider.Register(BarControlKeys.PasteMenu, options => CreateBitmapImage(options, "Paste16.png", "Paste24.png", "Paste32.png"));
			imageProvider.Register(BarControlKeys.PasteSpecial, options => CreateBitmapImage(options, "PasteSpecial16.png"));
			imageProvider.Register(BarControlKeys.PictureInsert, options => CreateBitmapImage(options, "Picture16.png", mediumFileName: null, "Picture32.png"));
			imageProvider.Register(BarControlKeys.QuickPartsInsert, options => CreateBitmapImage(options, "QuickParts16.png", mediumFileName: null, "QuickParts32.png"));
			imageProvider.Register(BarControlKeys.QuickStylesGallery, options => CreateBitmapImage(options, "QuickStyles16.png", mediumFileName: null, "QuickStyles32.png"));
			imageProvider.Register(BarControlKeys.Redo, options => CreateBitmapImage(options, "Redo16.png"));
			imageProvider.Register(BarControlKeys.Replace, options => CreateBitmapImage(options, "Replace16.png"));
			imageProvider.Register(BarControlKeys.Save, options => CreateBitmapImage(options, "Save16.png", mediumFileName: null, "Save32.png"));
			imageProvider.Register(BarControlKeys.SelectObjects, options => CreateBitmapImage(options, "Select16.png"));
			imageProvider.Register(BarControlKeys.SelectAll, options => CreateBitmapImage(options, "SelectAll16.png"));
			imageProvider.Register(BarControlKeys.SelectMenu, options => CreateBitmapImage(options, "Select16.png"));
			imageProvider.Register(BarControlKeys.Shading, options => CreateBitmapImage(options, "Shading16.png"));
			imageProvider.Register(BarControlKeys.ShadingColorMoreColorsDialog, options => CreateBitmapImage(options, "ColorPicker16.png"));
			imageProvider.Register(BarControlKeys.ShapesMenu, options => CreateBitmapImage(options, "Shapes16.png", mediumFileName: null, "Shapes32.png"));
			imageProvider.Register(BarControlKeys.Share, options => CreateBitmapImage(options, "Share16.png"));
			imageProvider.Register(BarControlKeys.ShowSymbols, options => CreateBitmapImage(options, "ToggleSymbols16.png"));
			imageProvider.Register(BarControlKeys.SignatureLineInsert, options => CreateBitmapImage(options, "SignatureLine16.png"));
			imageProvider.Register(BarControlKeys.SmartArtInsert, options => CreateBitmapImage(options, "SmartArt16.png", mediumFileName: null, "SmartArt32.png"));
			imageProvider.Register(BarControlKeys.Sort, options => CreateBitmapImage(options, "Sort16.png"));
			imageProvider.Register(BarControlKeys.Strikethrough, options => CreateBitmapImage(options, "Strikethrough16.png"));
			imageProvider.Register(BarControlKeys.Subscript, options => CreateBitmapImage(options, "Subscript16.png"));
			imageProvider.Register(BarControlKeys.Superscript, options => CreateBitmapImage(options, "Superscript16.png"));
			imageProvider.Register(BarControlKeys.SymbolInsertMenu, options => CreateBitmapImage(options, "Symbol16.png", mediumFileName: null, "Symbol32.png"));
			imageProvider.Register(BarControlKeys.SymbolInsertMoreSymbolsDialog, options => CreateBitmapImage(options, "Symbol16.png", mediumFileName: null, "Symbol32.png"));
			imageProvider.Register(BarControlKeys.TableDraw, options => CreateBitmapImage(options, "DrawTable16.png"));
			imageProvider.Register(BarControlKeys.TableInsertDialog, options => CreateBitmapImage(options, "Table16.png", mediumFileName: null, "Table32.png"));
			imageProvider.Register(BarControlKeys.TableInsertGallery, options => CreateBitmapImage(options, "Table16.png", mediumFileName: null, "Table32.png"));
			imageProvider.Register(BarControlKeys.TableInsertMenu, options => CreateBitmapImage(options, "Table16.png", mediumFileName: null, "Table32.png"));
			imageProvider.Register(BarControlKeys.TextBoxInsert, options => CreateBitmapImage(options, "TextBox16.png", mediumFileName: null, "TextBox32.png"));
			imageProvider.Register(BarControlKeys.TextFileInsert, options => CreateBitmapImage(options, "TextDocument16.png"));
			imageProvider.Register(BarControlKeys.Underline, options => CreateBitmapImage(options, "Underline16.png"));
			imageProvider.Register(BarControlKeys.Undo, options => CreateBitmapImage(options, "Undo16.png"));
			imageProvider.Register(BarControlKeys.WordArtInsert, options => CreateBitmapImage(options, "WordArt16.png", mediumFileName: null, "WordArt32.png"));

			//
			// Images with contextual color bar
			//

			imageProvider.Register(BarControlKeys.FontColor, options => CreateBitmapImageWithColorBar(options, defaultColor: Colors.Red, "FontColor16.png"));
			imageProvider.Register(BarControlKeys.TextHighlightColor, options => CreateBitmapImageWithColorBar(options, defaultColor: Colors.Yellow, "TextHighlightColor16.png"));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the collection of control view models.
		/// </summary>
		/// <value>A <see cref="BarControlViewModelCollection"/>.</value>
		public BarControlViewModelCollection ControlViewModels { get; }

		/// <summary>
		/// Sets a value based on the current checked state of control view model that implements <see cref="ICheckable"/>.
		/// </summary>
		/// <param name="controlKey">The key which uniquely identifies the control.</param>
		/// <param name="setValueAction">The action to perform when setting the value, which passes <c>true</c> if the control is currently checked or <c>false</c> if it is not checked.</param>
		/// <remarks>No action is performed if a control of the given key is not found or the control does not implement <see cref="ICheckable"/>.</remarks>
		public void SetValueFromControlViewModelCheckedState(string controlKey, Action<bool> setValueAction) {
			if (this.ControlViewModels[controlKey] is ICheckable checkableViewModel)
				setValueAction(checkableViewModel.IsChecked);
		}
		
		/// <summary>
		/// Configures to stop watching for user interface density changed events.
		/// </summary>
		public void UnwatchUserInterfaceDensityChangedEvent() {
			LayoutSettings.Instance.UserInterfaceDensityChanged -= OnUserInterfaceDensityChanged;
		}
		
		/// <summary>
		/// Updates the gallery item sizes based on user interface density changes.
		/// </summary>
		/// <remarks>
		/// This method should be called after any user interface density changes so that gallery items can adjust accordingly.
		/// </remarks>
		public void UpdateGalleryItemSizes() {
			foreach (var galleryViewModel in ControlViewModels.OfType<BarGalleryViewModel>()) {
				switch (galleryViewModel.Key) {
					case BarControlKeys.TextHighlightColorPicker:
						// Text highlight color gallery items have a larger minimum size
						galleryViewModel.MinItemWidth = LayoutSettings.Instance.ScaleDouble(factor: 2, adjustment: 20);
						galleryViewModel.MinItemHeight = galleryViewModel.MinItemWidth;
						break;
					default:
						// Other gallery items have a small minimum size
						galleryViewModel.MinItemWidth = LayoutSettings.Instance.ScaleDouble(factor: 2, adjustment: 8);
						galleryViewModel.MinItemHeight = galleryViewModel.MinItemWidth;
						break;
				}
			}
		}

		/// <summary>
		/// Updates the checked state of a control view model that implements <see cref="ICheckable"/>.
		/// </summary>
		/// <param name="controlKey">The key which uniquely identifies the control.</param>
		/// <param name="currentValueFunc">A function that returns <c>true</c> if the view model should be checked; otherwise, <c>false</c> if it should not be checked.</param>
		public void UpdateControlViewModelCheckedState(string controlKey, Func<bool> currentValueFunc) {
			if (this.ControlViewModels[controlKey] is ICheckable checkableViewModel)
				checkableViewModel.IsChecked = currentValueFunc();
		}

		/// <summary>
		/// Configures to start watching for user interface density changed events, allowing galleries to update item sizes appropriately.
		/// </summary>
		public void WatchUserInterfaceDensityChangedEvent() {
			LayoutSettings.Instance.UserInterfaceDensityChanged += OnUserInterfaceDensityChanged;
		}

	}

}
