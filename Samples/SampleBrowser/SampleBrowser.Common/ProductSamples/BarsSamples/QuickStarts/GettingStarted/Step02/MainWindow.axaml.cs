/*

RIBBON GETTING STARTED SERIES - STEP 2

STEP SUMMARY:

	Create a view model that will be bound to this view and associate a button with a
	command on the view model. The view model will be expanded in future steps to support
	additional functionality.

CHANGES SINCE LAST STEP:

	Added SampleWindowViewModel class to be used as the view model for this view.

	Added a ViewModel property that, when set, also assigns the view model to the
	window's DataContext.

	Added a Help button that executes a corresponding HelpCommand on the ViewModel.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step02 {

	public partial class MainWindow : RibbonWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			//	SAMPLE NOTE 2.1:
			//		Configure this view with the new view model
			ViewModel = new SampleWindowViewModel();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The view model for this view.
		/// </summary>
		private SampleWindowViewModel? ViewModel {
			//	SAMPLE NOTE 2.2:
			//		The view model is assigned directly to the DataContext of this window to easily support bindings.

			get => DataContext as SampleWindowViewModel;
			set => DataContext = value;
		}

	}
}
