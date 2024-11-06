/*

RIBBON GETTING STARTED SERIES - STEP 6

STEP SUMMARY:

	Update the ribbon's editor commands to interact with the editor textbox control on this view.

CHANGES SINCE LAST STEP:

	The 'ConfigureEditorCommands' method was added that updates editor commands on this window's view
	model with delegate commands that properly interact with a textbox control.

	Prior sample comments were removed/condensed.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using System;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step06 {

	public partial class MainWindow : RibbonWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			// Configure this view with the new view model
			ViewModel = new SampleWindowViewModel();
			
			//	SAMPLE NOTE 6.1:
			//		Configure the window view model with commands based on the editor
			ConfigureEditorCommands();

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void ConfigureEditorCommands() {
			if (ViewModel is null)
				return;

			//	SAMPLE NOTE 6.2
			//		Update the view model with commands that specifically interact with the editor
			//		used by this view.
			ViewModel.CopyCommand = new DelegateCommand<object?>(_ => editor.Copy(), _ => editor.CanCopy);
			ViewModel.CutCommand = new DelegateCommand<object?>(_ => editor.Cut(), _ => editor.CanCut);
			ViewModel.PasteCommand = new DelegateCommand<object?>(_ => editor.Paste(), _ => editor.CanPaste);
			ViewModel.RedoCommand = new DelegateCommand<object?>(_ => editor.Redo(), _ => editor.CanRedo);
			ViewModel.UndoCommand = new DelegateCommand<object?>(_ => editor.Undo(), _ => editor.CanUndo);

			//	SAMPLE NOTE 6.3:
			//		Listen for changes in editor properties that affect commands.
			editor.PropertyChanged += (s, e) => {
				if (e.Property == TextBox.CanCopyProperty)
					InvalidateCommandCanExecute(ViewModel?.CopyCommand);
				else if (e.Property == TextBox.CanCutProperty)
					InvalidateCommandCanExecute(ViewModel?.CutCommand);
				else if (e.Property == TextBox.CanPasteProperty)
					InvalidateCommandCanExecute(ViewModel?.PasteCommand);
				else if (e.Property == TextBox.CanUndoProperty)
					InvalidateCommandCanExecute(ViewModel?.UndoCommand);
				else if (e.Property == TextBox.CanRedoProperty)
					InvalidateCommandCanExecute(ViewModel?.RedoCommand);
			};
		}

		private void InvalidateCommandCanExecute(ICommand? command) {
			if (command is DelegateCommand<object?> delegateCommand)
				delegateCommand.RaiseCanExecuteChanged();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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
}
