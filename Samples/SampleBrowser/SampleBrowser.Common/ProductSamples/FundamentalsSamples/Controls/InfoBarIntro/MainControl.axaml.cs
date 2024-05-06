using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.InfoBarIntro {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnInfoBarCloseButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
			// If the click event is handled, the CloseButtonCommand will not be invoked and the defautl behavior
			// will be ignored (i.e. the InfoBar will remain open).
			e.Handled = (closeCommandHandleClickCheckBox.IsChecked == true);

			if (e.Handled)
				ApplicationViewModel.Instance.MessageService?.ShowMessage("Setting 'RoutedEventArgs.Handled = true' from 'CloseButtonClick' event so the 'CloseButtonCommand' is not executed.", "Click Handled", Avalonia.Controls.Notifications.NotificationType.Warning);
		}

		private void OnInfoBarClosing(object? sender, InfoBarClosingEventArgs e) {
			if ((e.Reason == InfoBarCloseReason.CloseButton) && (cancelCloseButtonCheckBox.IsChecked == true)
				|| (e.Reason == InfoBarCloseReason.Programmatic) && (cancelProgrammaticCloseCheckBox.IsChecked == true)) {

				ApplicationViewModel.Instance.MessageService?.ShowMessage($"Closing of the info bar for reason '{e.Reason}' canceled within the 'Closing' event.", "InfoBar Close Canceled", Avalonia.Controls.Notifications.NotificationType.Warning);

				// Prevent the info bar from closing
				e.Cancel = true;
			}
			else {
				ApplicationViewModel.Instance.MessageService?.ShowMessage($"Info bar allowed to close for reason '{e.Reason}'.", "InfoBar Close Allowed", Avalonia.Controls.Notifications.NotificationType.Success);
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ICommand ActionCommand { get; } = new DelegateCommand<object>(
			_ => {
				ApplicationViewModel.Instance.MessageService?.ShowMessage("Processing of a custom action was requested by an info bar.", "InfoBar Action Requested");
			});

		public ICommand CloseButtonCommand { get; } = new DelegateCommand<object>(
			async (param) => {
				if (param is InfoBar infoBar) {
					var result = await MessageBox.Show($"Are you sure you want to close the '{infoBar.Title}' info bar?",
						"Close Info Bar?",
						button: MessageBoxButtons.YesNo,
						image: MessageBoxImage.Question);

					if (result == MessageBoxResult.Yes) {
						// Close the info bar
						infoBar.IsOpen = false;
					}
				}
			});

		public ICommand NoCloseCommand { get; } = new DelegateCommand<object>(
			_ => {
				// The info bar will stay open unless the InfoBar.IsOpen property is set to false
				ApplicationViewModel.Instance.MessageService?.ShowMessage("This sample shows the InfoBar close button for illustration purposes only and will keep the info bar open.", "InfoBar Demo");
			});

	}

}
