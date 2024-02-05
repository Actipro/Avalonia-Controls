using ActiproSoftware.UI.Avalonia.Controls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.UserPromptIntro {

	/// <summary>
	/// Defines a sample command which displays a prompt to confirm user selection before submitting it.
	/// </summary>
	public class ConfirmationCommand : ICommand {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="ICommand.CanExecuteChanged"/>
		event EventHandler? ICommand.CanExecuteChanged {
			add { /* Not used */ }
			remove { /* Not used */ }
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Finds the <see cref="UserPromptControl"/> associated with an <see cref="AvaloniaObject"/>.
		/// </summary>
		/// <param name="searchObject">The object from which the search will begin.</param>
		/// <returns>The <see cref="UserPromptControl"/> when found; otherwise, <c>null</c>.</returns>
		private static UserPromptControl? FindUserPromptControl(AvaloniaObject searchObject) {
			if (searchObject is Visual visual)
				return visual?.FindAncestorOfType<UserPromptControl>(includeSelf: true);
			return null;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc cref="ICommand.CanExecute(object)"/>
		public bool CanExecute(object? parameter) {
			// Always allow the command
			return true;
		}

		/// <inheritdoc cref="ICommand.Execute(object)"/>
		public async void Execute(object? parameter) {
			// The button on the user prompt must be passed as a parameter to this command
			var button = parameter as Button;
			if (button != null) {
				// Get the result which is configured for the button
				var buttonResult = UserPromptControl.GetButtonResult(button);

				// Confirm if the user wants to submit this result
				Debug.WriteLine("Execute Confirmation Command");

				var confirmationResult = await MessageBox.Show($"Are you sure you want to respond '{buttonResult}'?", "Confirmation",
					MessageBoxButtons.YesNo, MessageBoxImage.Question);
				if (confirmationResult == MessageBoxResult.Yes) {
					// Find the UserPromptControl that contains the button
					var userPromptControl = FindUserPromptControl(button);
					if (userPromptControl != null) {
						// Assign the result and the dialog will close
						userPromptControl.Result = buttonResult;
						return;
					}
				}
			}

			// NOTE: By not assigning UserPromptControl.Result, the dialog will remain open
		}

	}

}
