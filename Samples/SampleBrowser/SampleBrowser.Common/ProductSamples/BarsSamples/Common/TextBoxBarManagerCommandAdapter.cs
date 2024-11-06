using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Adapts a <c>TextBox</c> control for use with composite commands on a <see cref="Common.BarManager"/> instance.
	/// </summary>
	public class TextBoxBarManagerCommandAdapter {

		private static readonly Color DefaultFontBackgroundPickerColor = Colors.Yellow;
		private static readonly Color DefaultFontForegroundPickerColor = Colors.Red;

		private RichTextStyle? _originalStyle;

		private DelegateCommand<object?>? _copyCommand;
		private DelegateCommand<object?>? _cutCommand;
		private DelegateCommand<SymbolBarGalleryItemViewModel>? _insertSymbolCommand;
		private DelegateCommand<object?>? _pasteCommand;
		private DelegateCommand<object?>? _redoCommand;
		private PreviewableDelegateCommand<ColorBarGalleryItemViewModel>? _setFontColorCommand;
		private PreviewableDelegateCommand<FontFamilyBarGalleryItemViewModel>? _setFontFamilyCommand;
		private PreviewableDelegateCommand<FontSizeBarGalleryItemViewModel>? _setFontSizeCommand;
		private DelegateCommand<TextAlignment>? _setTextAlignmentCommand;
		private PreviewableDelegateCommand<ColorBarGalleryItemViewModel>? _setTextHighlightColorCommand;
		private PreviewableDelegateCommand<TextStyleBarGalleryItemViewModel>? _setTextStyleCommand;
		private DelegateCommand<object?>? _stopHighlightingCommand;
		private DelegateCommand<object?>? _toggleBoldCommand;
		private DelegateCommand<object?>? _toggleItalicCommand;
		private DelegateCommand<object?>? _undoCommand;
		private DelegateCommand<string?>? _unknownFontSizeCommand;


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		/// <param name="barManager">The instance of <see cref="Common.BarManager"/> where commands will be registered.</param>
		/// <param name="textBox">The <c>TextBox</c> control to be adapated.</param>
		public TextBoxBarManagerCommandAdapter(BarManager barManager, TextBox textBox) {
			BarManager = barManager ?? throw new ArgumentNullException(nameof(barManager));
			TextBox = textBox ?? throw new ArgumentNullException(nameof(textBox));

			// Listen for changes in textbox properties that can impact commands
			textBox.PropertyChanged += this.OnTextBoxPropertyChanged;

			RegisterCommands();

			// Initialize with default formatting
			ClearFormatting();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void ApplyTextStyle(TextStyle textStyle) {
			var currentTextStyle = GetCurrentTextStyle();

			currentTextStyle.Bold = textStyle.Bold;
			currentTextStyle.FontColor = textStyle.TextColor;
			currentTextStyle.FontFamilyName = textStyle.FontFamilyName;
			currentTextStyle.FontSize = textStyle.FontSize;
			currentTextStyle.Italic = textStyle.Italic;

			SetCurrentTextStyle(currentTextStyle);
		}

		private BarManager BarManager { get; }

		private void ClearFormatting()
			=> SetCurrentTextStyle(new RichTextStyle());

		private ICommand CopyCommand
			=> _copyCommand ??= new DelegateCommand<object?>(
				_ => TextBox.Copy(),
				_ => TextBox.CanCopy);

		private Color GetCurrentBackgroundColor()
			=> (TextBox.Background is SolidColorBrush brush ? brush.Color : Colors.White);

		private Color GetCurrentForegroundColor()
			=> (TextBox.Foreground is SolidColorBrush brush ? brush.Color : Colors.Black);

		private RichTextStyle GetCurrentTextStyle()
			=> new RichTextStyle() {
				Bold = TextBox.FontWeight != FontWeight.Normal,
				FontColor = GetCurrentForegroundColor(),
				// Use the full FontFamily.Key and Name as what is stored in the style
				FontFamilyName = TextBox.FontFamily.ToString(),
				FontSize = TextBox.FontSize,
				Italic = TextBox.FontStyle != FontStyle.Normal,
				TextAlignment = TextBox.TextAlignment,
				TextHighlightColor = GetCurrentBackgroundColor(),
			};

		private void SetCurrentTextStyle(RichTextStyle textStyle) {
			TextBox.Background = new SolidColorBrush(textStyle.TextHighlightColor);
			TextBox.FontSize = textStyle.FontSize ?? FontSettings.DefaultFontSize;
			TextBox.FontFamily = string.IsNullOrWhiteSpace(textStyle.FontFamilyName) ? FontSettings.DefaultFontFamily : new FontFamily(textStyle.FontFamilyName);
			TextBox.FontWeight = textStyle.Bold ? FontWeight.SemiBold : FontWeight.Normal;
			TextBox.FontStyle = textStyle.Italic ? FontStyle.Italic : FontStyle.Normal;
			TextBox.Foreground = new SolidColorBrush(textStyle.FontColor);
			TextBox.TextAlignment = textStyle.TextAlignment;			

			UpdateBarControlViewModelsFromCurrentStyle(textStyle);
		}

		private ICommand CutCommand
			=> _cutCommand ??= new DelegateCommand<object?>(
				_ => TextBox.Cut(),
				_ => TextBox.CanCut);

		private void DecreaseFontSize()
			=> UpdateCurrentTextStyle(x => x.FontSize = Math.Max(1.0, (x.FontSize ?? FontSettings.DefaultFontSize) - 1.0));

		private void IncreaseFontSize()
			=> UpdateCurrentTextStyle(x => x.FontSize = (x.FontSize ?? FontSettings.DefaultFontSize) + 1.0);

		private ICommand InsertSymbolCommand
			=> _insertSymbolCommand ??= new DelegateCommand<SymbolBarGalleryItemViewModel>(
				p => {
					TextBox.SelectedText = p?.Value ?? string.Empty;
				});

		private bool IsPreviewMode { get; set; }

		private void OnRequestActivatePreviewMode() {
			// Cache current style and enter preview mode
			if (!IsPreviewMode) {
				_originalStyle = GetCurrentTextStyle();
				IsPreviewMode = true;
			}
		}

		private void OnRequestCancelPreviewMode() {
			// Exit preview, restoring previous style
			if (IsPreviewMode) {
				if (_originalStyle is not null) {
					SetCurrentTextStyle(_originalStyle);
					_originalStyle = null;
				}

				IsPreviewMode = false;
			}
		}

		private void OnRequestSaveAndExitPreviewMode() {
			// Exit preview, keeping current style
			IsPreviewMode = false;
			_originalStyle = null;
		}

		private void OnTextBoxPropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e) {
			if (e.Property == TextBox.CanCopyProperty)
				_copyCommand?.RaiseCanExecuteChanged();
			else if (e.Property == TextBox.CanCutProperty)
				_cutCommand?.RaiseCanExecuteChanged();
			else if (e.Property == TextBox.CanPasteProperty)
				_pasteCommand?.RaiseCanExecuteChanged();
			else if (e.Property == TextBox.CanUndoProperty)
				_undoCommand?.RaiseCanExecuteChanged();
			else if (e.Property == TextBox.CanRedoProperty)
				_redoCommand?.RaiseCanExecuteChanged();
		}

		private ICommand PasteCommand
			=> _pasteCommand ??= new DelegateCommand<object?>(
				_ => TextBox.Paste(),
				_ => TextBox.CanPaste);

		private ICommand RedoCommand
			=> _redoCommand ??= new DelegateCommand<object?>(
				_ => TextBox.Redo(),
				_ => TextBox.CanRedo);

		private void RegisterCommands() {
			// Mapped composite commands
			BarManager.ClearFormattingCommand.RegisterCommand(new DelegateCommand<object?>(_ => ClearFormatting()));
			BarManager.CopyCommand.RegisterCommand(CopyCommand);
			BarManager.CutCommand.RegisterCommand(CutCommand);
			BarManager.DecreaseFontSizeCommand.RegisterCommand(new DelegateCommand<object?>(_ => DecreaseFontSize()));
			BarManager.IncreaseFontSizeCommand.RegisterCommand(new DelegateCommand<object?>(_ => IncreaseFontSize()));
			BarManager.InsertSymbolCommand.RegisterCommand(InsertSymbolCommand);
			BarManager.PasteCommand.RegisterCommand(PasteCommand);
			BarManager.UndoCommand.RegisterCommand(UndoCommand);
			BarManager.RedoCommand.RegisterCommand(RedoCommand);
			BarManager.SelectAllCommand.RegisterCommand(new DelegateCommand<object?>(_ => TextBox.SelectAll()));
			BarManager.SetFontColorCommand.RegisterCommand(SetFontColorCommand);
			BarManager.SetFontFamilyCommand.RegisterCommand(SetFontFamilyCommand);
			BarManager.SetFontSizeCommand.RegisterCommand(SetFontSizeCommand);
			BarManager.SetTextAlignmentCommand.RegisterCommand(SetTextAlignmentCommand);
			BarManager.SetTextHighlightColorCommand.RegisterCommand(SetTextHighlightColorCommand);
			BarManager.SetTextStyleCommand.RegisterCommand(SetTextStyleCommand);
			BarManager.StopHighlightingCommand.RegisterCommand(StopHighlightingCommand);
			BarManager.ToggleBoldCommand.RegisterCommand(ToggleBoldCommand);
			BarManager.ToggleItalicCommand.RegisterCommand(ToggleItalicCommand);
			BarManager.UnknownFontSizeCommand.RegisterCommand(UnknownFontSizeCommand);
		}

		private ICommand SetFontColorCommand {
			get => _setFontColorCommand ??= new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
				executeAction: p => {
					OnRequestSaveAndExitPreviewMode();
					UpdateCurrentTextStyle(x => x.FontColor = p?.Value ?? DefaultFontForegroundPickerColor);

					if (this.BarManager.ControlViewModels[BarControlKeys.FontColor] is BarSplitButtonViewModel buttonViewModel) {
						buttonViewModel.CommandParameter = p;
						buttonViewModel.SmallIcon = BarManager.ImageProvider.GetImage(BarControlKeys.FontColor, new BarImageOptions(BarImageSize.Small) { ContextualColor = GetCurrentForegroundColor() });
					}
				},
				canExecuteFunc: p => true,
				previewAction: p => {
					OnRequestActivatePreviewMode();
					UpdateCurrentTextStyle(x => x.FontColor = p?.Value ?? DefaultFontForegroundPickerColor);
				},
				cancelPreviewAction: p => {
					OnRequestCancelPreviewMode();
				}
			);
		}

		private ICommand SetFontFamilyCommand {
			get => _setFontFamilyCommand ??= new PreviewableDelegateCommand<FontFamilyBarGalleryItemViewModel>(
				executeAction: param => {
					OnRequestSaveAndExitPreviewMode();
					// Use the full FontFamily.Key and Name as what is stored in the style
					UpdateCurrentTextStyle(x => x.FontFamilyName = param?.Value?.ToString() ?? FontSettings.DefaultFontFamilyName);
				},
				canExecuteFunc: _ => true,
				previewAction: param => {
					OnRequestActivatePreviewMode();
					// Use the full FontFamily.Key and Name as what is stored in the style
					UpdateCurrentTextStyle(x => x.FontFamilyName = param?.Value?.ToString() ?? FontSettings.DefaultFontFamilyName);
				},
				cancelPreviewAction: _ => OnRequestCancelPreviewMode()
			);
		}

		private ICommand SetFontSizeCommand {
			get => _setFontSizeCommand ??= new PreviewableDelegateCommand<FontSizeBarGalleryItemViewModel>(
				executeAction: param => {
					OnRequestSaveAndExitPreviewMode();
					UpdateCurrentTextStyle(x => x.FontSize = param?.Value ?? FontSettings.DefaultFontSize);
				},
				canExecuteFunc: _ => true,
				previewAction: param => {
					OnRequestActivatePreviewMode();
					UpdateCurrentTextStyle(x => x.FontSize = param?.Value ?? FontSettings.DefaultFontSize);
				},
				cancelPreviewAction: _ => OnRequestCancelPreviewMode()
			);
		}

		private ICommand SetTextAlignmentCommand
			=> _setTextAlignmentCommand ??= new DelegateCommand<TextAlignment>(param => {
				UpdateCurrentTextStyle(x => x.TextAlignment = param);
			});

		private ICommand SetTextHighlightColorCommand {
			get => _setTextHighlightColorCommand ??= new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
				executeAction: p => {
					OnRequestSaveAndExitPreviewMode();
					UpdateCurrentTextStyle(x => x.TextHighlightColor = p?.Value ?? DefaultFontBackgroundPickerColor);

					if (this.BarManager.ControlViewModels[BarControlKeys.TextHighlightColor] is BarSplitButtonViewModel buttonViewModel) {
						buttonViewModel.CommandParameter = p;
						buttonViewModel.SmallIcon = BarManager.ImageProvider.GetImage(BarControlKeys.TextHighlightColor, new BarImageOptions(BarImageSize.Small) { ContextualColor = GetCurrentBackgroundColor() });
					}
				},
				canExecuteFunc: p => true,
				previewAction: p => {
					OnRequestActivatePreviewMode();
					UpdateCurrentTextStyle(x => x.TextHighlightColor = p?.Value ?? DefaultFontBackgroundPickerColor);
				},
				cancelPreviewAction: p => {
					OnRequestCancelPreviewMode();
				}
			);
		}

		private ICommand SetTextStyleCommand {
			get => _setTextStyleCommand ??= new PreviewableDelegateCommand<TextStyleBarGalleryItemViewModel>(
				executeAction: param => {
					if (param?.Value is TextStyle textStyle) {
						OnRequestSaveAndExitPreviewMode();
						ApplyTextStyle(textStyle);
					}
				},
				canExecuteFunc: _ => true,
				previewAction: param => {
					if (param?.Value is TextStyle textStyle) {
						OnRequestActivatePreviewMode();
						ApplyTextStyle(textStyle);
					}
				},
				cancelPreviewAction: _ => OnRequestCancelPreviewMode()
			);
		}

		private ICommand StopHighlightingCommand
			=> _stopHighlightingCommand ??= new DelegateCommand<object?>(_ => {
				UpdateCurrentTextStyle(x => x.TextHighlightColor = Colors.White);
			});

		private TextBox TextBox { get; }

		private ICommand ToggleBoldCommand
			=> _toggleBoldCommand ??= new DelegateCommand<object?>(_ => {
				this.BarManager.SetValueFromControlViewModelCheckedState(
					BarControlKeys.Bold,
					isChecked => UpdateCurrentTextStyle(x => x.Bold = isChecked)
				);
			});

		private ICommand ToggleItalicCommand
			=> _toggleItalicCommand ??= new DelegateCommand<object?>(_ => {
				this.BarManager.SetValueFromControlViewModelCheckedState(
					BarControlKeys.Italic,
					isChecked => UpdateCurrentTextStyle(x => x.Italic = isChecked)
				);
			});

		private ICommand UndoCommand
			=> _undoCommand ??= new DelegateCommand<object?>(
				_ => TextBox.Undo(),
				_ => TextBox.CanUndo);

		private ICommand UnknownFontSizeCommand
			=> _unknownFontSizeCommand ??= new DelegateCommand<string?>(
				executeAction: p => {
					if (int.TryParse(p, out var fontSize))
						UpdateCurrentTextStyle(x => x.FontSize = fontSize);
				},
				canExecuteFunc: p => int.TryParse(p, out var fontSize) && (byte.MinValue <= fontSize) && (fontSize <= byte.MaxValue)
			);

		private void UpdateCurrentTextStyle(Action<RichTextStyle> action) {
			if (action is not null) {
				var textStyle = GetCurrentTextStyle().Clone();
				action.Invoke(textStyle);
				SetCurrentTextStyle(textStyle);
			}
		}

		private void UpdateBarControlViewModelsFromCurrentStyle(RichTextStyle? textStyle = null) {
			// Ignore updates when in preview mode
			if (IsPreviewMode)
				return;

			textStyle ??= GetCurrentTextStyle();

			// Font color
			if (this.BarManager.ControlViewModels[BarControlKeys.FontColorPicker] is BarGalleryViewModel fontColorGalleryViewModel)
				fontColorGalleryViewModel.SelectItemByValueMatch<ColorBarGalleryItemViewModel>(i => i.Value == textStyle.FontColor);

			// Font family
			var fontFamilyComboBoxViewModel = BarManager.ControlViewModels[BarControlKeys.Font] as BarComboBoxViewModel;
			var fontFamily = TextBox.FontFamily;
			fontFamilyComboBoxViewModel?.SelectItemByValueMatch<FontFamilyBarGalleryItemViewModel>(
				i => i.Value?.Name == fontFamily.Name,
				i => i.Value?.Name ?? FontSettings.DefaultFontFamilyName,
				fontFamily.Name);

			// Font size
			var fontSizeComboBoxViewModel = BarManager.ControlViewModels[BarControlKeys.FontSize] as BarComboBoxViewModel;
			var fontSize = TextBox.FontSize;
			fontSizeComboBoxViewModel?.SelectItemByValueMatch<FontSizeBarGalleryItemViewModel>(
				i => i.Value == fontSize,
				i => i.Value.ToString(),
				fontSize.ToString());

			// Text highlight color
			if (this.BarManager.ControlViewModels[BarControlKeys.TextHighlightColorPicker] is BarGalleryViewModel textHighlightColorGalleryViewModel)
				textHighlightColorGalleryViewModel.SelectItemByValueMatch<ColorBarGalleryItemViewModel>(i => i.Value == textStyle.TextHighlightColor);

			// Quick styles
			if (BarManager.ControlViewModels[BarControlKeys.QuickStylesGallery] is BarGalleryViewModel textStyleGalleryViewModel) {
				textStyleGalleryViewModel.SelectItemByValueMatch<TextStyleBarGalleryItemViewModel>(i =>
					i.Value?.Bold == textStyle.Bold
					&& i.Value?.TextColor == textStyle.FontColor
					&& i.Value?.FontFamilyName == textStyle.FontFamilyName
					&& i.Value?.FontSize == textStyle.FontSize
					&& i.Value?.Italic == textStyle.Italic
				);
			}

			// Bold
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Bold, () => textStyle.Bold);

			// Italic
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.Italic, () => textStyle.Italic);

			// Text alignment
			var textAlignment = TextBox.TextAlignment;
			if (textAlignment != TextAlignment.Center
				&& textAlignment != TextAlignment.Justify
				&& textAlignment != TextAlignment.Right) {

				// Treat all other values like left alignment
				textAlignment = TextAlignment.Left;
			}
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignCenter, () => textAlignment == TextAlignment.Center);
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignJustify, () => textAlignment == TextAlignment.Justify);
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignLeft, () => textAlignment == TextAlignment.Left);
			BarManager.UpdateControlViewModelCheckedState(BarControlKeys.AlignRight, () => textAlignment == TextAlignment.Right);
		}

	}

}
