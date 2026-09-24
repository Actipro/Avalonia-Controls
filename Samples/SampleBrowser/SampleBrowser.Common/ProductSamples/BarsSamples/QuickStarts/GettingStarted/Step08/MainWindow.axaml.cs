/*

RIBBON GETTING STARTED SERIES - STEP 8

STEP SUMMARY:

	This C# file is unchanged since the last step.

CHANGES SINCE LAST STEP:

	None.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step08;

public partial class MainWindow : RibbonWindow {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public MainWindow() {
		InitializeComponent();

		// Configure this view with the new view model
		ViewModel = new SampleWindowViewModel();

		// Configure the window view model with commands based on the editor
		ConfigureEditorCommands();

	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void ConfigureEditorCommands() {
		if (ViewModel is not { } viewModel)
			return;

		viewModel.CopyCommand = new DelegateCommand<object?>(_ => editor.Copy(), _ => editor.CanCopy);
		viewModel.CutCommand = new DelegateCommand<object?>(_ => editor.Cut(), _ => editor.CanCut);
		viewModel.PasteCommand = new DelegateCommand<object?>(_ => editor.Paste(), _ => editor.CanPaste);
		viewModel.RedoCommand = new DelegateCommand<object?>(_ => editor.Redo(), _ => editor.CanRedo);
		viewModel.SelectAllCommand = new DelegateCommand<object?>(_ => editor.SelectAll());
		viewModel.UndoCommand = new DelegateCommand<object?>(_ => editor.Undo(), _ => editor.CanUndo);

		editor.PropertyChanged += (s, e) => {
			if (e.Property == TextBox.CanCopyProperty)
				InvalidateCommandCanExecute(viewModel?.CopyCommand);
			else if (e.Property == TextBox.CanCutProperty)
				InvalidateCommandCanExecute(viewModel?.CutCommand);
			else if (e.Property == TextBox.CanPasteProperty)
				InvalidateCommandCanExecute(viewModel?.PasteCommand);
			else if (e.Property == TextBox.CanUndoProperty)
				InvalidateCommandCanExecute(viewModel?.UndoCommand);
			else if (e.Property == TextBox.CanRedoProperty)
				InvalidateCommandCanExecute(viewModel?.RedoCommand);
		};
	}

	private static void InvalidateCommandCanExecute(ICommand? command) {
		if (command is DelegateCommand<object?> delegateCommand)
			delegateCommand.RaiseCanExecuteChanged();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	protected override void OnOpened(EventArgs e) {
		base.OnOpened(e);

		// Ensure the editor has initial focus when the window is opened
		editor.Focus();
	}

	/// <summary>
	/// The view model for this view.
	/// </summary>
	private SampleWindowViewModel? ViewModel {
		get => DataContext as SampleWindowViewModel;
		set => DataContext = value;
	}

}
