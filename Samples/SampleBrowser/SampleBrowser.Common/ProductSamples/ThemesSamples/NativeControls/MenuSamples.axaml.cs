using ActiproSoftware.SampleBrowser;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;

namespace ActiproSoftware.ProductSamples.ThemeSamples.NativeControls {

	public partial class MenuSamples : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MenuSamples() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnMenuItemClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
			if (sender is MenuItem menuItem) {
				var headerText = menuItem.Header?.ToString()?.Replace("_", string.Empty);
				if (menuItem.Icon is CheckBox checkBox) {
					checkBox.IsChecked = !checkBox.IsChecked;
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"You {(checkBox.IsChecked == true ? "checked" : "unchecked")} menu item '{headerText}'");
				}
				else if (menuItem.Icon is RadioButton radioButton) {
					radioButton.IsChecked = true;
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"You selected menu item '{headerText}' for radio group '{radioButton.GroupName ?? "<NULL>"}'");
				}
				else
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"You selected menu item '{headerText}'");
			}
		}

	}

}
