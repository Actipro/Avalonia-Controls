using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

partial class BarManager {

	private ICommand? _insertTableCommand;
	private ICommand? _searchForTextCommand;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The composite command to clear formatting.
	/// </summary>
	public CompositeCommand ClearFormattingCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for clipboard copy operations.
	/// </summary>
	public CompositeCommand CopyCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for clipboard cut operations.
	/// </summary>
	public CompositeCommand CutCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for decreasing font size.
	/// </summary>
	public CompositeCommand DecreaseFontSizeCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for performing a delete operation.
	/// </summary>
	public CompositeCommand DeleteCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for setting flow direction.
	/// </summary>
	public CompositeCommand FlowDirectionCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for increasing font size.
	/// </summary>
	public CompositeCommand IncreaseFontSizeCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for inserting a symbol.
	/// </summary>
	public CompositeCommand InsertSymbolCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The command to insert a table.
	/// </summary>
	/// <remarks>The command parameter must be a <see cref="Size"/> where the width and height are whole numbers indicating the number of table rows and columns, respectively.</remarks>
	public ICommand InsertTableCommand {
		get => _insertTableCommand ??= new DelegateCommand<Size>(p => {
			MessageBox.Show(
				string.Format("This is where a table of size {0}x{1} would be inserted.", p.Width, p.Height), "Not Implemented",
				MessageBoxButtons.OK, MessageBoxImage.Information);
		});
	}

	/// <summary>
	/// The composite command for creating a new, blank document.
	/// </summary>
	public CompositeCommand NewBlankDocumentCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for creating a new document with default content.
	/// </summary>
	public CompositeCommand NewDefaultDocumentCommand { get; } = new CompositeCommand();

	/// <summary>
	/// Gets a special command associated with controls that are for demonstration purposes only and provide no implemented functionality.
	/// </summary>
	public ICommand NotImplementedCommand { get; }
		= ApplicationViewModel.Instance.NotImplementedCommand;

	/// <summary>
	/// The composite command for clipboard paste operations.
	/// </summary>
	public CompositeCommand PasteCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for performing a redo operation.
	/// </summary>
	public CompositeCommand RedoCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The command executed to perform a text search.
	/// </summary>
	public ICommand SearchForTextCommand {
		get => _searchForTextCommand ??= new DelegateCommand<object>(_ => {
			if (ControlViewModels[BarControlKeys.SearchForText] is BarTextBoxViewModel viewModel) {
				MessageBox.Show(
					string.Format("Search for the text '{0}' here.", viewModel.Text), "Not Implemented",
					MessageBoxButtons.OK, MessageBoxImage.Information);
			}
		});
	}

	/// <summary>
	/// The composite command for performing a select all operation.
	/// </summary>
	public CompositeCommand SelectAllCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for setting the font color.
	/// </summary>
	public CompositeCommand SetFontColorCommand { get; } = new PreviewableCompositeCommand();

	/// <summary>
	/// The composite command for setting the font family.
	/// </summary>
	public CompositeCommand SetFontFamilyCommand { get; } = new PreviewableCompositeCommand();

	/// <summary>
	/// The composite command for setting the font size.
	/// </summary>
	public CompositeCommand SetFontSizeCommand { get; } = new PreviewableCompositeCommand();

	/// <summary>
	/// The composite command for setting the numbering style.
	/// </summary>
	public CompositeCommand SetNumberingCommand { get; } = new PreviewableCompositeCommand();

	/// <summary>
	/// The composite command for setting text alignment.
	/// </summary>
	public CompositeCommand SetTextAlignmentCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for setting text highlight color.
	/// </summary>
	public CompositeCommand SetTextHighlightColorCommand { get; } = new PreviewableCompositeCommand();

	/// <summary>
	/// The composite command for setting a text style.
	/// </summary>
	public CompositeCommand SetTextStyleCommand { get; } = new PreviewableCompositeCommand();

	/// <summary>
	/// The composite command for setting an underline style.
	/// </summary>
	public CompositeCommand SetUnderlineCommand { get; } = new PreviewableCompositeCommand();

	/// <summary>
	/// The composite command to stop text highlighting.
	/// </summary>
	public CompositeCommand StopHighlightingCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for toggling the visibility of the ribbon application button.
	/// </summary>
	public CompositeCommand ToggleApplicationButtonCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for toggling if the ribbon application button uses an image for content.
	/// </summary>
	public CompositeCommand ToggleApplicationButtonImageCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for toggling the bold font weight.
	/// </summary>
	public CompositeCommand ToggleBoldCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for toggling the visibility of the ribbon footer.
	/// </summary>
	public CompositeCommand ToggleFooterCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for toggling the italic font style.
	/// </summary>
	public CompositeCommand ToggleItalicCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for toggling the numbering style.
	/// </summary>
	public CompositeCommand ToggleNumberingCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for toggling the visibility of the ribbon quick access toolbar.
	/// </summary>
	public CompositeCommand ToggleQuickAccessToolBarCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for performing an undo operation.
	/// </summary>
	public CompositeCommand UndoCommand { get; } = new CompositeCommand();

	/// <summary>
	/// The composite command for handling an undefined font size.
	/// </summary>
	public CompositeCommand UnknownFontSizeCommand { get; } = new CompositeCommand();

}
