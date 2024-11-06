/*

RIBBON GETTING STARTED SERIES - STEP 4

STEP SUMMARY:

	New key(s) added for newly introduced controls.

CHANGES SINCE LAST STEP:

	Added 'GroupHelpResources' key for the Ribbon Group on the "Help"
	tab that will contain the "Help" button.

*/

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step04 {

	/// <summary>
	/// Defines the keys used to reference controls used by Ribbon and related menus.
	/// </summary>
	public class SampleBarControlKeys {

		/// <summary>
		/// The key for the Ribbon Group of help resource controls.
		/// </summary>
		public const string GroupHelpResources = nameof(GroupHelpResources);

		/// <summary>
		/// The key for the Help control.
		/// </summary>
		public const string Help = nameof(Help);

	}

}
