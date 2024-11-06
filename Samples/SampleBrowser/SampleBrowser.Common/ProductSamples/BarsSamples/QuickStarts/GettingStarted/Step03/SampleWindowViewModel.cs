/*

RIBBON GETTING STARTED SERIES - STEP 3

STEP SUMMARY:

	The SampleBarManager created during this step is the central point of access to the
	control view models that will be used by the Ribbon, so this view model will also need
	access to SampleBarManager when defining its own view models specific to the Ribbon
	that is associated with this model.

CHANGES SINCE LAST STEP:

	Added a BarManager property initialized to an instance of the new SampleBarManager class.

	Migrated the 'HelpCommand' property to a property on BarManager.

	Prior sample comments were removed/condensed.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step03 {

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
			// Initialize the view model for the Ribbon
			this.Ribbon = new RibbonViewModel() {
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		//	SAMPLE NOTE 3.1:
		//		The SampleBarManager will be used to access view models for individual controls.
		//		While the control view models are not used in this step, a reference will be made to the manager
		//		class for use by future steps.

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
		public ICommand HelpCommand
			//	SAMPLE NOTE 3.2:
			//		The logic from this method has been migrated to BarManager.HelpCommand.
			=> BarManager.HelpCommand;

		/// <summary>
		/// The view model for the ribbon control.
		/// </summary>
		public RibbonViewModel Ribbon { get; }

	}

}
