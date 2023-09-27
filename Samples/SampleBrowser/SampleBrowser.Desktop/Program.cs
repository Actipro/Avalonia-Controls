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

				.UsePlatformDetect()
				.UseManagedSystemDialogs() // Forces managed file picker dialogs to validate themes
				.LogToTrace()
				// TODO: Resolved in v11.0.5. Replace this AfterPlatformServicesSetup block with WithInterFont call after font family issue is resolved in core Avalonia
				.AfterPlatformServicesSetup(x => {
					if (OperatingSystem.IsLinux())
						x.WithInterFont();
				})
				.AfterSetup(x => {
					// ApplicationViewModel platform-specific configuration
					ApplicationViewModel.Instance.PlatformHelper = new DesktopPlatformHelper();
				});

	}

}
