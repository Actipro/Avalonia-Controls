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

			.WithInterFont()
			.StartBrowserAppAsync("out");

	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>();

}
