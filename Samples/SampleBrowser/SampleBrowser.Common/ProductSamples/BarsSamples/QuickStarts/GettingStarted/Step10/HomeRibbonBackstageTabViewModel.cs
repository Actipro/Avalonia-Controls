/*

RIBBON GETTING STARTED SERIES - STEP 10

STEP SUMMARY:

	This view model will be used for the "Home" tab of the ribbon backstage.
	While it does not currently provide any specific functionality, the data type
	of this view model is important for determining which IDataTemplate will
	be used in the content area of the ribbon backstage when this tab is selected.

CHANGES SINCE LAST STEP:

	This is the first step containing this file.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step10 {

	/// <summary>
	/// Defines the view model for the "Home" tab of the ribbon backstage.
	/// </summary>
	public class HomeRibbonBackstageTabViewModel : RibbonBackstageTabViewModel {

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public HomeRibbonBackstageTabViewModel()
			: base(SampleBarControlKeys.BackstageTabHome, "Home") { }

	}

}
