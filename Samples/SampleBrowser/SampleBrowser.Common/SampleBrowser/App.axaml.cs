#if DEBUG && MS_LOGGING
using ActiproSoftware.SampleBrowser.Logging;
using Microsoft.Extensions.Logging;
using LoggerFactory = ActiproSoftware.Logging.LoggerFactory;
#endif

using ActiproSoftware.Logging;
using ActiproSoftware.UI.Avalonia.Media;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Avalonia.Data.Core.Plugins;
using System.Reflection;
#if FLUENT_THEME
using Avalonia.Themes.Fluent;
#elif SIMPLE_THEME
using Avalonia.Themes.Simple;
#endif

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Represents the Avalonia application.
	/// </summary>
	public partial class App : Application {

		private static readonly Logger? _logger;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		static App() {
			#if DEBUG && MS_LOGGING
			LoggerFactoryAdapter.Configure(builder => {
				builder.AddFilter("ActiproSoftware", Microsoft.Extensions.Logging.LogLevel.Warning);
				builder.AddDebugLogger();
			});
			#endif
			_logger = LoggerFactory.DefaultInstance.CreateLogger<App>();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the application.
		/// </summary>
		public override void Initialize() {
			_logger?.LogInformation("Initializing app from XAML...");
			AvaloniaXamlLoader.Load(this);

			// Configure ImageProvider to use this assembly as the base URI for relative image paths
			ImageProvider.Default.RelativePathBaseUri = new System.Uri("avares://" + Assembly.GetExecutingAssembly().GetName().Name);
			
			#if FLUENT_THEME
			// Fluent theme must be loaded before Actipro themes
			_logger?.LogInformation("Loading Fluent theme...");
			this.Styles.Insert(0, new FluentTheme());
			this.Styles.Insert(1, (Styles)AvaloniaXamlLoader.Load(new System.Uri("avares://Avalonia.Controls.ColorPicker/Themes/Fluent/Fluent.xaml")));
			this.Styles.Insert(1, (Styles)AvaloniaXamlLoader.Load(new System.Uri("avares://Avalonia.Controls.DataGrid/Themes/Fluent.xaml")));
			#elif SIMPLE_THEME
			// Simple theme must be loaded before Actipro themes
			_logger?.LogInformation("Loading Simple theme...");
			this.Styles.Insert(0, new SimpleTheme());
			this.Styles.Insert(1, (Styles)AvaloniaXamlLoader.Load(new System.Uri("avares://Avalonia.Controls.ColorPicker/Themes/Simple/Simple.xaml")));
			this.Styles.Insert(1, (Styles)AvaloniaXamlLoader.Load(new System.Uri("avares://Avalonia.Controls.DataGrid/Themes/Simple.xaml")));
			#endif

			RequestedThemeVariant = ThemeVariant.Light;
			_logger?.LogDebug("Application.Current.ActualThemeVariant = {0}", ActualThemeVariant);

			ImageProvider.Default.ChromaticAdaptationMode = ImageChromaticAdaptationMode.DarkThemes;
			_logger?.LogDebug("ImageProvider.Default.ChromaticAdaptationMode = {0}", ImageProvider.Default.ChromaticAdaptationMode);
		}

		/// <summary>
		/// Occurs after framework initialization has completed.
		/// </summary>
		public override void OnFrameworkInitializationCompleted() {
			
			// Remove Avalonia data validation to avoid duplicate validations
			// from both Avalonia and Windows Community Toolkit
			BindingPlugins.DataValidators.RemoveAt(0);

			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
				// Create a root window for desktop applications
				desktop.MainWindow = new RootWindow();
			}
			else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
				// Create a root view for single view applications
				singleViewPlatform.MainView = new RootView();
			}

			base.OnFrameworkInitializationCompleted();
		}

	}

}
