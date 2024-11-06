/*

RIBBON GETTING STARTED SERIES - STEP 10

STEP SUMMARY:

	Associate the RibbonBackstageViewModel.ContentTemplate with the static resource	defined in XAML.

CHANGES SINCE LAST STEP:

	Updated the configuration of SampleWindowViewModel to include RibbonBackstageViewModel.ContentTemplate.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step10 {

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
			
			// Configure the window view model with commands based on the editor
			ConfigureEditorCommands();

			//	SAMPLE NOTE 10.1:
			//		Configure the backstage to use the TypedDataTemplateSelector defined in XAML so that
			//		the content of backstage tab can be displayed when it is selected.
			if ((ViewModel?.Ribbon.Backstage is RibbonBackstageViewModel backstageViewModel)
				&& (this.TryFindResource("BackstageContentTemplates", out var contentTemplate))) {

				backstageViewModel.ContentTemplate = contentTemplate as IDataTemplate;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void ConfigureEditorCommands() {
			if (ViewModel is null)
				return;

			ViewModel.CopyCommand = new DelegateCommand<object?>(_ => editor.Copy(), _ => editor.CanCopy);
			ViewModel.CutCommand = new DelegateCommand<object?>(_ => editor.Cut(), _ => editor.CanCut);
			ViewModel.PasteCommand = new DelegateCommand<object?>(_ => editor.Paste(), _ => editor.CanPaste);
			ViewModel.RedoCommand = new DelegateCommand<object?>(_ => editor.Redo(), _ => editor.CanRedo);
			ViewModel.SelectAllCommand = new DelegateCommand<object?>(_ => editor.SelectAll());
			ViewModel.UndoCommand = new DelegateCommand<object?>(_ => editor.Undo(), _ => editor.CanUndo);

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