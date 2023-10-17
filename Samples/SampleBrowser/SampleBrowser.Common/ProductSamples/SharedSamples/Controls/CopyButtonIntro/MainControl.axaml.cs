using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ActiproSoftware.ProductSamples.SharedSamples.Controls.CopyButtonIntro {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			CopyCommand = new DelegateCommand<object>(CopyCommandExecuted, CopyCommandCanExecute);
			commandSample.Command = CopyCommand;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private bool CopyCommandCanExecute(object? parameter)
			=> commandIsEnabledCheckBox.IsChecked == true;

		private void CopyCommandExecuted(object? parameter) {
			// This sample is configured with an option to simulate an error when copying text
			if (simulateErrorOnCopyCheckBox.IsChecked == true)
				throw new ApplicationException("Sample configured to throw error on copy.");

			// Get the platform's clipboard
			var clipboard = TopLevel.GetTopLevel(this)?.Clipboard
				?? throw new NotSupportedException("Clipboard is not supported on this platform.");

			// The CopyButton.Command must execute synchronously on the calling thread for raised
			// exceptions to display an error notification if the copy operation fails.
			clipboard.SetTextAsync($"This text is defined by the copy command at {DateTime.Now:hh:mm.ss tt}.");
		}

		private void OnCommandEnabledCheckBoxIsCheckedChanged(object? sender, RoutedEventArgs e)
			=> CopyCommand.RaiseCanExecuteChanged();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public DelegateCommand<object> CopyCommand { get; }

		public bool SimulateCopyCommandError { get; set; }

	}

}
