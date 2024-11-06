/*

RIBBON GETTING STARTED SERIES - STEP 1

STEP SUMMARY:

	Always refer to the notes in MainWindow.xaml for the most detail about the current step.

	Most of the work for this step is completed in the AXAML file. At this stage of the
	Getting Started series, the code behind for the RibbonWindow does not define any
	additional functionality.
	
	Notes similar to these will also be reflected in other C# code files that accompany each step.

CHANGES SINCE LAST STEP:

	This is the first step. In subsequent steps, check here for notes about what changed since the
	previous step.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step01 {

	public partial class MainWindow : RibbonWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();
		}

	}
}
