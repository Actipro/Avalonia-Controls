using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	partial class BarManager {

		private ICommand? _insertTableCommand;
		private ICommand? _searchForTextCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the composite command to clear formatting.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ClearFormattingCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for clipboard copy operations.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand CopyCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for clipboard cut operations.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand CutCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for decreasing font size.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand DecreaseFontSizeCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for performing a delete operation.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand DeleteCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for setting flow direction.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand FlowDirectionCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for increasing font size.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand IncreaseFontSizeCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for inserting a symbol.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand InsertSymbolCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the command to insert a table.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		/// <remarks>The command parameter must be a <see cref="Size"/> where the width and height are whole numbers indicating the number of table rows and columns, respectively.</remarks>
		public ICommand InsertTableCommand {
			get {
				if (_insertTableCommand == null) {
					_insertTableCommand = new DelegateCommand<Size>(
						p => {
							MessageBox.Show(
								string.Format("This is where a table of size {0}x{1} would be inserted.", p.Width, p.Height), "Not Implemented", 
								MessageBoxButtons.OK, MessageBoxImage.Information);
						}
					);
				}

				return _insertTableCommand;
			}
		}

		/// <summary>
		/// Gets the composite command for creating a new, blank document.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand NewBlankDocumentCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for creating a new document with default content.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand NewDefaultDocumentCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets a special command associated with controls that are for demonstration purposes only and provide no implemented functionality.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand NotImplementedCommand { get; }
			= ApplicationViewModel.Instance.NotImplementedCommand;

		/// <summary>
		/// Gets the composite command for clipboard paste operations.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand PasteCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for performing a redo operation.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand RedoCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the command executed to perform a text search.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand SearchForTextCommand {
			get {
				if (_searchForTextCommand == null) {
					_searchForTextCommand = new DelegateCommand<object>(
						p => {
							if (this.ControlViewModels[BarControlKeys.SearchForText] is BarTextBoxViewModel viewModel) {
								MessageBox.Show(
									string.Format("Search for the text '{0}' here.", viewModel.Text), "Not Implemented", 
									MessageBoxButtons.OK, MessageBoxImage.Information);
							}
						}
					);
				}

				return _searchForTextCommand;
			}
		}

		/// <summary>
		/// Gets the composite command for performing a select all operation.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SelectAllCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for setting the font color.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetFontColorCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting the font family.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetFontFamilyCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting the font size.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetFontSizeCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting the numbering style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetNumberingCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting text alignment.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetTextAlignmentCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for setting text highlight color.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetTextHighlightColorCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting a text style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetTextStyleCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command for setting an underline style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand SetUnderlineCommand { get; } = new PreviewableCompositeCommand();

		/// <summary>
		/// Gets the composite command to stop text highlighting.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand StopHighlightingCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the visibility of the ribbon application button.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleApplicationButtonCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling if the ribbon application button uses an image for content.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleApplicationButtonImageCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the bold font weight.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleBoldCommand { get; } = new CompositeCommand();
		
		/// <summary>
		/// Gets the composite command for toggling the visibility of the ribbon footer.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleFooterCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the italic font style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleItalicCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the numbering style.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleNumberingCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for toggling the visibility of the ribbon quick access toolbar.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand ToggleQuickAccessToolBarCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for performing an undo operation.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand UndoCommand { get; } = new CompositeCommand();

		/// <summary>
		/// Gets the composite command for handling an undefined font size.
		/// </summary>
		/// <value>A <see cref="CompositeCommand"/>.</value>
		public CompositeCommand UnknownFontSizeCommand { get; } = new CompositeCommand();

	}

}
