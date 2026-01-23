using System.Runtime.Versioning;
using System.Threading.Tasks;
using ActiproSoftware.SampleBrowser;
using Avalonia;
using Avalonia.Browser;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program {

	private static Task Main(string[] args)
		=> BuildAvaloniaApp()

			// NOTE: See the 'Licensing' documentation topic for details on how to register an evaluation or paid license here:
			// .RegisterActiproLicense(licensee, licenseKey)

			// This SkipOptions setting is required if using SVGs with DynamicImage (https://github.com/wieslawsoltes/Svg.Skia/discussions/82)
			.With(new SkiaOptions { UseOpacitySaveLayer = true })

			.WithInterFont()
			.StartBrowserAppAsync("out");

	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>();

}
