/*

RIBBON GETTING STARTED SERIES - STEP 4

STEP SUMMARY:

	A Ribbon organizes controls in Tabs, so this step focuses on defining the Tab view
	models for the Ribbon. The Help control configured in previous steps will be added
	to a Help tab.

CHANGES SINCE LAST STEP:

	A 'RibbonViewModel.Tabs' property was populated with the view models for each configured
	Ribbon Tab. The configuration includes a new Help tab and group that will display the
	previously configured Help control.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step04 {

	/// <summary>
	/// Defines the view model for this sample.
	/// </summary>
	public class SampleWindowViewModel : ObservableObjectBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public SampleWindowViewModel() {

			var barManager = this.BarManager;

			// Initialize the view model for the Ribbon
			this.Ribbon = new RibbonViewModel() {
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,

				//	SAMPLE NOTE 4.1:
				//		The MVVM assembly includes pre-defined view models for common Bar control types. A
				//		RibbonTabViewModel is used to model a Ribbon Tab, and each RibbonTabViewModel
				//		has properties to define the content of each tab.
				//
				//		Initialize the Tabs collection to include a RibbonTabViewModel that will define
				//		a "Help" tab containing a "Resources" control group with a single "Help" button.
				Tabs = {

					//	SAMPLE NOTE 4.2:
					//		Create the Tab view model with the label "Help" and KeyTip of "E" (since KeyTip "H"
					//		is commonly used for a "Home" tab that our sample will add later). The label "Help"
					//		is automatically derived from the key "Help" passed to the constructor. The "KeyTipText"
					//		would normally be automatically derived as "H" (from the label "Help"), but the
					//		derived value is overridden as "E" in this sample.
					//
					//		See SAMPLE NOTE 4.1 in file SampleBarManager.cs for more details on the automatic
					//		generation of a Label from a Key.
					new RibbonTabViewModel("Help") {
						KeyTipText = "E",

						//	SAMPLE NOTE 4.3:
						//		Each Ribbon Tab has one or more groups. The "Help" tab in this sample will have a
						//		single group called "Resources". A unique key was added to 'SampleBarControlKeys'
						//		to refer to this group.
						Groups = {

							//	SAMPLE NOTE 4.4:
							//		The "Help" tab in this sample will have a single group called "Resources".
							//		A unique key was added to 'SampleBarControlKeys' to refer to this group.
							new RibbonGroupViewModel(SampleBarControlKeys.GroupHelpResources, "Resources") {

								//	SAMPLE NOTE 4.5:
								//		A Ribbon Group can contain multiple controls. The most basic implementation
								//		places controls directly on the group. In later steps, a RibbonControlGroupViewModel
								//		will be used to provide more flexibility in how controls are displayed.
								Items = {

									//	SAMPLE NOTE 4.6:
									//		Reuse the control view model registered for the Help control instead of
									//		creating a new one. As a reminder, Help was registered with SampleBarManager to
									//		create a BarButtonViewModel.
									barManager.ControlViewModels[SampleBarControlKeys.Help],
								}

							}

						}

					}

				}

			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The manager for working with the objects used by Ribbon and related menus.
		/// </summary>
		private SampleBarManager BarManager { get; } = new SampleBarManager();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The command for displaying Help.
		/// </summary>
		public ICommand HelpCommand => BarManager.HelpCommand;

		/// <summary>
		/// The view model for the ribbon control.
		/// </summary>
		public RibbonViewModel Ribbon { get; }

	}

}
