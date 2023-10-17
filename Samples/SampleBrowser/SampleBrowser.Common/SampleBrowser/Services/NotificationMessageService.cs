using Avalonia.Controls.Notifications;
using System;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// A message service based on <see cref="WindowNotificationManager"/>.
	/// </summary>
	public class NotificationMessageService : IMessageService {

		private readonly WindowNotificationManager _notificationManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public NotificationMessageService(WindowNotificationManager notificationManager) {
			_notificationManager = notificationManager ?? throw new ArgumentNullException(nameof(notificationManager));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IMessageService.ShowError(string)"/>
		public void ShowError(string message)
			=> ShowError(message, ex: null);

		/// <inheritdoc cref="IMessageService.ShowError(string, Exception)"/>
		public void ShowError(string message, Exception? ex) {
			if (ex is not null)
				message += Environment.NewLine + Environment.NewLine + ex.ToString();
			ShowMessage(message, title: null, NotificationType.Error);
		}

		/// <inheritdoc cref="IMessageService.ShowMessage(string)"/>
		public void ShowMessage(string message)
			=> ShowMessage(message, title: null, messageType: null);

		/// <inheritdoc cref="IMessageService.ShowMessage(string, string)"/>
		public void ShowMessage(string message, string? title)
			=> ShowMessage(message, title, messageType: null);

		/// <inheritdoc cref="IMessageService.ShowMessage(string, string, NotificationType)"/>
		public void ShowMessage(string message, string? title, NotificationType? messageType) {
			if ((messageType is null) && (string.IsNullOrEmpty(title))) {
				_notificationManager.Show(message);
			}
			else {
				messageType ??= NotificationType.Information;
				if (string.IsNullOrEmpty(title))
					title = messageType.ToString();
				_notificationManager.Show(new Notification(title, message, messageType.Value));
			}
		}

	}

}
