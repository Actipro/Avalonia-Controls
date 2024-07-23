using ActiproSoftware.SampleBrowser;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.ThemeSamples.NativeControls {

	public partial class MenusSamples : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MenusSamples() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnMenuItemClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
			if (sender is MenuItem menuItem) {
				var headerText = menuItem.Header?.ToString()?.Replace("_", string.Empty);
				if (menuItem.ToggleType == MenuItemToggleType.CheckBox)
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"You {(menuItem.IsChecked == true ? "checked" : "unchecked")} menu item '{headerText}'");
				else if (menuItem.ToggleType == MenuItemToggleType.Radio)
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"You selected menu item '{headerText}' for radio group '{menuItem.GroupName ?? "<NULL>"}'");
				else
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"You selected menu item '{headerText}'");
			}
		}

	}

}
