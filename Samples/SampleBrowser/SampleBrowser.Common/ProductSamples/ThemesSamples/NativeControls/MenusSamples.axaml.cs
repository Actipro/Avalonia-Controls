using ActiproSoftware.SampleBrowser;

namespace ActiproSoftware.ProductSamples.ThemeSamples.NativeControls;

public partial class MenusSamples : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public MenusSamples() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnMenuItemClicked(object? sender, RoutedEventArgs e) {
		if (sender is MenuItem menuItem) {
			var headerText = menuItem.Header?.ToString()?.Replace("_", string.Empty);
			switch (menuItem.ToggleType) {
				case MenuItemToggleType.CheckBox:
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"You {(menuItem.IsChecked == true ? "checked" : "unchecked")} menu item '{headerText}'");
					break;
				case MenuItemToggleType.Radio:
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"You selected menu item '{headerText}' for radio group '{menuItem.GroupName ?? "<NULL>"}'");
					break;
				default:
					ApplicationViewModel.Instance.MessageService?.ShowMessage($"You selected menu item '{headerText}'");
					break;
			}
		}
	}

}
