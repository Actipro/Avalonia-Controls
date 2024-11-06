using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements a <see cref="TextBox"/> that works around the native control's logic of clearing selection on focus loss.
	/// </summary>
	/// <remarks>
	/// This class can be removed once PR (https://github.com/AvaloniaUI/Avalonia/pull/17195) is merged.
	/// </remarks>
	public class AdvancedTextBox : TextBox {

		/// <inheritdoc/>
		protected override void OnLostFocus(RoutedEventArgs e) {
			var oldCaretIndex = CaretIndex;
			var oldSelectionStart = SelectionStart;
			var oldSelectionEnd = SelectionEnd;

			base.OnLostFocus(e);

			CaretIndex = oldCaretIndex;
			SelectionStart = oldSelectionStart;
			SelectionEnd = oldSelectionEnd;
		}

		/// <inheritdoc/>
		protected override Type StyleKeyOverride 
			=> typeof(TextBox);

	}

}
