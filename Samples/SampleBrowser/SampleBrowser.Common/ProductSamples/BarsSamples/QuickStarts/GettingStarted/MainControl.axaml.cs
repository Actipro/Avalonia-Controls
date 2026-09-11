using ActiproSoftware.SampleBrowser;
using Avalonia.Controls.ApplicationLifetimes;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted;

public partial class MainControl : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public MainControl() {
		InitializeComponent();

		// Show notice if external samples cannot be opened
		if (!AreSampleWindowsSupported)
			notSupportedInfoBar.IsOpen = true;

		// Define the steps in the series
		itemsControl.ItemsSource = new List<GettingStartedItemInfo>() {
			new(1, "Step01/MainWindow", "Create a RibbonWindow configured with an empty Ribbon."),
			new(2, "Step02/MainWindow", "Create SampleApplicationViewModel and RibbonViewModel that will be bound to the sample."),
			new(3, "Step03/MainWindow", "Create SampleBarManager to manage working with view models for controls within the Ribbon."),
			new(4, "Step04/MainWindow", "Add the first Tab to the Ribbon."),
			new(5, "Step05/MainWindow", "Expand the current sample to include a TextBox with a more diverse set of commands in the Ribbon."),
			new(6, "Step06/MainWindow", "Configure the Ribbon commands to interact with the TextBox control."),
			new(7, "Step07/MainWindow", "Replace a default ContextMenu with one based on Bars controls."),
			new(8, "Step08/MainWindow", "Add the Quick Access Toolbar."),
			new(9, "Step09/MainWindow", "Add the Backstage with buttons."),
			new(10, "Step10/MainWindow", "Expand the Backstage to include Tabs."),
		};

	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Indicates if opening sample windows is supported.
	/// </summary>
	public bool AreSampleWindowsSupported { get; } = Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime;

	public void OnLaunchButtonClick(object? sender, RoutedEventArgs e) {
		if (
			DataContext is ApplicationViewModel viewModel
			&& sender is Button { DataContext: GettingStartedItemInfo itemInfo }
		) {
			if (AreSampleWindowsSupported) {
				// Open the sample window
				viewModel.OpenExternalSample(itemInfo.Path);
			}
			else {
				// Open the source code on GitHib
				var samplePath = ApplicationViewModel.Instance?.ViewItemInfo?.Path?.Replace("/MainControl", $"/{itemInfo.Path}.axaml");
				ApplicationViewModel.Instance?.OpenGitHubSampleFolderCommand.Execute(samplePath);
			}
		}
	}

}
