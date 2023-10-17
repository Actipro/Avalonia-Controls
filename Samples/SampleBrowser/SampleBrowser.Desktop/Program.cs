using Avalonia;
using Avalonia.Dialogs;
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
		public static void Main(string[] args)
			=> BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

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
				.LogToTrace()
				.AfterSetup(x => {
					// ApplicationViewModel platform-specific configuration
					ApplicationViewModel.Instance.PlatformHelper = new DesktopPlatformHelper();
				});

	}

}
