using Avalonia.Controls.Notifications;
using System;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements a messaging service for samples.
	/// </summary>
	public interface IMessageService {

		/// <summary>
		/// Shows an error message.
		/// </summary>
		/// <param name="message">The error message.</param>
		void ShowError(string message);

		/// <summary>
		/// Shows an error message.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="ex">The exception, if any, associated with the error.</param>
		void ShowError(string message, Exception? ex);

		/// <summary>
		/// Shows a message.
		/// </summary>
		/// <param name="message">The message to be displayed.</param>
		void ShowMessage(string message);

		/// <summary>
		/// Shows a message with the given title.
		/// </summary>
		/// <param name="message">The message to be displayed.</param>
		/// <param name="title">The title, if any, to be associated with the message.</param>
		void ShowMessage(string message, string? title);

		/// <summary>
		/// Shows a message with the given title of the specified type.
		/// </summary>
		/// <param name="message">The message to be displayed.</param>
		/// <param name="title">The title, if any, to be associated with the message.</param>
		/// <param name="messageType">The type of message to be displayed. When not specified, a generic message will be displayed.</param>
		void ShowMessage(string message, string? title, NotificationType? messageType);

	}


}
