using Avalonia;
using System;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Defines the core program for the application.
	/// </summary>
	internal class Program {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Provides the main entry point of the application.
		/// </summary>
		/// <param name="args">The string arguments.</param>
		[STAThread]
		public static void Main(string[] args) {
			BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

			#if DEBUG && !USE_DEV_TOOLS
			// Make sure legacy built-in dev tools are attached to all windows when not using professional dev tools
			Avalonia.Controls.Control.LoadedEvent.AddClassHandler<Avalonia.Controls.Window>((s, _) => s.AttachDevTools(), Avalonia.Interactivity.RoutingStrategies.Bubble, handledEventsToo: true);
			#endif
		}

		/// <summary>
		/// Creates an Avalonia application builder.
		/// </summary>
		/// <returns>The <see cref="AppBuilder"/> object that was created.</returns>
		public static AppBuilder BuildAvaloniaApp()
			=> AppBuilder.Configure<App>()

				// NOTE: See the 'Licensing' documentation topic for details on how to register an evaluation or paid license here:
				// .RegisterActiproLicense(licensee, licenseKey)

				.WithInterFont()
				.UsePlatformDetect()
				.LogToTrace();

	}

}
