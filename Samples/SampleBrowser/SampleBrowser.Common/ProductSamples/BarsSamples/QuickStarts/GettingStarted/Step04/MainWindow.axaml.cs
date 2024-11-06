/*

RIBBON GETTING STARTED SERIES - STEP 4

STEP SUMMARY:

	This C# file is unchanged since the last step.

CHANGES SINCE LAST STEP:

	None.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step04 {

	public partial class MainWindow : RibbonWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			// Configure this view with the new view model
			ViewModel = new SampleWindowViewModel();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The view model for this view.
		/// </summary>
		private SampleWindowViewModel? ViewModel {
			get => DataContext as SampleWindowViewModel;
			set => DataContext = value;
		}

	}
}
