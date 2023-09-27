using System.Runtime.Versioning;
using System.Threading.Tasks;
using ActiproSoftware.SampleBrowser;
using Avalonia;
using Avalonia.Browser;

[assembly: SupportedOSPlatform("browser")]

internal partial class Program {

	private static async Task Main(string[] args)
		=> await BuildAvaloniaApp()

			// NOTE: See the 'Licensing' documentation topic for details on how to register an evaluation or paid license here:
			// .RegisterActiproLicense(licensee, licenseKey)

			.WithInterFont()
			.AfterSetup(x => {
				// ApplicationViewModel platform-specific configuration
				ApplicationViewModel.Instance.PlatformHelper = new BrowserPlatformHelper();
			})
			.StartBrowserAppAsync("out");

	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>();

}
