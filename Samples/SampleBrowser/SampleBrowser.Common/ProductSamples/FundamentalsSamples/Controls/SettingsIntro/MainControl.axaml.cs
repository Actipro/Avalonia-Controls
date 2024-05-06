using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.SettingsIntro {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnSettingClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
			// Make sure the source of the Click is a SettingsCard since some Click events can bubble up from content hosted on the card (like a CheckBox)
			if ((e.Source is SettingsCard) && (!e.Handled))
				MessageBox.Show("Handle the 'Click' event for a setting as an alternative to using a 'Command'.", "Setting Click", MessageBoxButtons.OK, MessageBoxImage.Information);
		}

		public DelegateCommand<object> SettingClickedCommand { get; }
			= new DelegateCommand<object>(p => MessageBox.Show($"The setting for '{p}' was clicked.", "Setting Click", MessageBoxButtons.OK, MessageBoxImage.Information));

	}

}
