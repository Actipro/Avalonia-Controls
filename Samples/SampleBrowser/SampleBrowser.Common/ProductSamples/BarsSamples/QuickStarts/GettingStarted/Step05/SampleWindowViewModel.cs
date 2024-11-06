/*

RIBBON GETTING STARTED SERIES - STEP 5

STEP SUMMARY:

	Configure the view models for a "Home" tab that will contain an "Undo" group
	(with Undo/Redo controls) and a "Clipboard" group (with Cut/Copy/Paste controls).

CHANGES SINCE LAST STEP:

	The "Tabs" collection is initialized with an additional tab for "Home" and the
	view models that define the content of the "Home" tab.

	Removed the 'HelpCommand' declaration that is no longer in use since the "Help"
	button on the main window was removed.

	Prior sample comments were removed/condensed.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step05 {

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

				Tabs = {
					//	SAMPLE NOTE 5.1:
					//		Add a new "Home" tab that will contain the most commonly used controls.
					new RibbonTabViewModel("Home") {
						Groups = {

							//	SAMPLE NOTE:
							//		Create an "Undo" group that will contain the Undo/Redo controls.
							new RibbonGroupViewModel(SampleBarControlKeys.GroupUndo, "Undo") {

								//	SAMPLE NOTE 5.2:
								//		Assign a small icon for the group that will be displayed if the group
								//		is collapsed. A collapsed group will appear as a single popup button
								//		that contains the given icon. Clicking the button will show the available
								//		controls on the popup menu.
								SmallIcon = barManager.ImageProvider.GetImage(SampleBarControlKeys.GroupUndo, BarImageSize.Small),

								Items = {

									//	SAMPLE NOTE 5.3:
									//		Instead of placing the view models directly in the group (like was done
									//		for the "Resources" group of the "Help" tab, a RibbonControlGroupViewModel
									//		will be used to contain the individual controls. The RibbonControlGroupViewModel
									//		allows for more control over the size of controls and how they are displayed
									//		within the group (i.e., stacking behavior).
									//
									//		Most controls can have a Small, Medium and Large variant size. A BarButtonControl
									//		in Classic Ribbon, for example, will appear as a large button with large icon when
									//		using the "Large" variant size. The "Medium" variant size will show a small image
									//		next to a label, and the "Small" variant will show just a small image without a label.
									//
									//		Since there is limited real estate to build a Ribbon UI, consideration must be
									//		given to how controls are displayed.
									//
									//		Since the Undo/Redo controls are widely recognized icons, we can save UI space
									//		by forcing the buttons to a "Small" variant size. The RibbonControlGroupViewModel
									//		will also allow these two controls to be stacked vertically to use the least
									//		amount of space.
									//
									//		Since the Undo/Redo controls will never be displayed in a "Large" variant,
									//		no large images were registered for these view models.
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysSmall,
										Items = {
											barManager.ControlViewModels[SampleBarControlKeys.Undo],
											barManager.ControlViewModels[SampleBarControlKeys.Redo],
										}
									}
								}
							},

							//	SAMPLE NOTE 5.4:
							//		Create a "Clipboard" group that will contain the Cut/Copy/Paste controls.
							new RibbonGroupViewModel(SampleBarControlKeys.GroupClipboard, "Clipboard") {
								SmallIcon = barManager.ImageProvider.GetImage(SampleBarControlKeys.GroupClipboard, BarImageSize.Small),
								Items = {

									//	SAMPLE NOTE 5.5:
									//		A split "Paste Menu" will be displayed (large variant by default) that
									//		contains a drop-down for "Paste" and "Paste Special".
									barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],

									//	SAMPLE NOTE 5.6:
									//		A RibbonControlGroupViewModel will be used to display the Cut/Copy
									//		controls next to the Paste control. These controls will be shown
									//		in a vertical stack that intentionally disallows "Large" variant sizes.
									//		This means the controls will be "Medium" when there is enough space
									//		to display the labels, and "Small" when there is not enough space
									//		for the labels. Even if there is enough space, they will never be "Large"
									//		by design. While there is plenty of room in the current UI for the
									//		"Large" variants, future controls will limit the amount of available space.
									//
									//		Since the Cut/Copy controls will never be displayed in a "Large" variant,
									//		no large images were registered for these view models.
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
										Items = {
											barManager.ControlViewModels[SampleBarControlKeys.Cut],
											barManager.ControlViewModels[SampleBarControlKeys.Copy],
										}
									}
								}
							},
						}
					},
					new RibbonTabViewModel("Help") {
						KeyTipText = "E",
						Groups = {
							new RibbonGroupViewModel(SampleBarControlKeys.GroupHelpResources, "Resources") {
								Items = {
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
		/// The view model for the ribbon control.
		/// </summary>
		public RibbonViewModel Ribbon { get; }

	}

}
