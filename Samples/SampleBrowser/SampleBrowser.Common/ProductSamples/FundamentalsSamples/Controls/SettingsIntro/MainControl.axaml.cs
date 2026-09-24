using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Input;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.SettingsIntro;

public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public MainControl() {
		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnSettingClick(object? sender, RoutedEventArgs e) {
		// Make sure the source of the Click is a SettingsCard since some Click events can bubble up from content hosted on the card (like a CheckBox)
		if (e is { Handled: false, Source: SettingsCard })
			MessageBox.Show("Handle the 'Click' event for a setting as an alternative to using a 'Command'.", "Setting Click", MessageBoxButtons.OK, MessageBoxImage.Information);
	}

	public DelegateCommand<object> SettingClickedCommand { get; } = new DelegateCommand<object>(param =>
		MessageBox.Show($"The setting for '{param}' was clicked.", "Setting Click", MessageBoxButtons.OK, MessageBoxImage.Information));

}
