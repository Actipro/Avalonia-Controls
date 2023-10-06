using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using System;

namespace ActiproSoftware.ProductSamples.ThemeSamples.NativeControls {

	public partial class OtherControlsSamples : UserControl {

		private WindowNotificationManager? _notificationManager;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public OtherControlsSamples() {
			InitializeComponent();

			carouselSample.PageTransition = new PageSlide(TimeSpan.FromSeconds(0.25), PageSlide.SlideAxis.Horizontal);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnCarouselNextButtonClick(object? sender, RoutedEventArgs e) {
			carouselSample.Next();
		}

		private void OnCarouselPreviousButtonClick(object? sender, RoutedEventArgs e) {
			carouselSample.Previous();
		}

		private void OnNotificationCardButtonClicked(object? sender, RoutedEventArgs e) {
			if (_notificationManager is not null) {
				if ((sender is Button button) && (button.Tag is NotificationType notificationType))
					_notificationManager?.Show(new Notification($"{notificationType} Notification", $"This message has a notification type of {notificationType} and was generated at {DateTime.Now:HH:mm:ss}.", notificationType));
				else
					_notificationManager?.Show("This message is a simple text string.");
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
			_notificationManager ??= new WindowNotificationManager(TopLevel.GetTopLevel(this));
			base.OnAttachedToVisualTree(e);
		}

	}

}
