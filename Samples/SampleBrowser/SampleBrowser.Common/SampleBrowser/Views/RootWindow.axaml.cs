using Avalonia.Controls;
using Avalonia.Input;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the root window for the application.
	/// </summary>
	public partial class RootWindow : Window {

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		public RootWindow() {
			InitializeComponent();
		}

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		protected override void OnKeyDown(KeyEventArgs e) {
			base.OnKeyDown(e);

			// Toggle full-screen on Ctrl+shift+F11 for testing purposes
			if (!e.Handled && (e.KeyModifiers == (KeyModifiers.Control | KeyModifiers.Shift)) && (e.Key == Key.F11))
				WindowState = WindowState == WindowState.FullScreen ? WindowState.Normal : WindowState.FullScreen;
		}

	}

}
