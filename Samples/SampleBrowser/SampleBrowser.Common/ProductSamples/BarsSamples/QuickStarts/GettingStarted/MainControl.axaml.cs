using ActiproSoftware.SampleBrowser;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public MainControl() {
			InitializeComponent();

			// Show notice if external samples cannot be opened
			if (!AreSampleWindowsSupported)
				notSupportedInfoBar.IsOpen = true;

			// Define the steps in the series
			itemsControl.ItemsSource = new List<GettingStartedItemInfo>() {
				new GettingStartedItemInfo(1, "Step01/MainWindow", "Create a RibbonWindow configured with an empty Ribbon."),
				new GettingStartedItemInfo(2, "Step02/MainWindow", "Create SampleApplicationViewModel and RibbonViewModel that will be bound to the sample."),
				new GettingStartedItemInfo(3, "Step03/MainWindow", "Create SampleBarManager to manage working with view models for controls within the Ribbon."),
				new GettingStartedItemInfo(4, "Step04/MainWindow", "Add the first Tab to the Ribbon."),
				new GettingStartedItemInfo(5, "Step05/MainWindow", "Expand the current sample to include a TextBox with a more diverse set of commands in the Ribbon."),
				new GettingStartedItemInfo(6, "Step06/MainWindow", "Configure the Ribbon commands to interact with the TextBox control."),
				new GettingStartedItemInfo(7, "Step07/MainWindow", "Replace a default ContextMenu with one based on Bars controls."),
				new GettingStartedItemInfo(8, "Step08/MainWindow", "Add the Quick Access Toolbar."),
				new GettingStartedItemInfo(9, "Step09/MainWindow", "Add the Backstage with buttons."),
				new GettingStartedItemInfo(10, "Step10/MainWindow", "Expand the Backstage to include Tabs."),
			};

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indiates if opening sample windows is supported.
		/// </summary>
		public bool AreSampleWindowsSupported { get; }
			= Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime;

		public void OnLaunchButtonClick(object? sender, RoutedEventArgs e) {
			if ((this.DataContext is ApplicationViewModel viewModel)
				&& (sender is Button button)
				&& (button.DataContext is GettingStartedItemInfo itemInfo)) {

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
}
