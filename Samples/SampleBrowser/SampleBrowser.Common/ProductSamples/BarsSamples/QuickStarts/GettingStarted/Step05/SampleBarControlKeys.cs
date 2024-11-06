/*

RIBBON GETTING STARTED SERIES - STEP 5

STEP SUMMARY:

	Add additional keys for new Bar controls.

CHANGES SINCE LAST STEP:

	Added keys for the Undo/Redo and Cut/Copy/Paste controls.

	Added keys for the Undo and Clipboard groups.

*/

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step05 {

	/// <summary>
	/// Defines the keys used to reference controls used by Ribbon and related menus.
	/// </summary>
	public class SampleBarControlKeys {

		/// <summary>
		/// The key for the Ribbon Group of help resource controls.
		/// </summary>
		public const string GroupHelpResources = nameof(GroupHelpResources);

		/// <summary>
		/// The key for the Ribbon Group of Clipboard controls.
		/// </summary>
		/*(New)*/ public const string GroupClipboard = nameof(GroupClipboard);

		/// <summary>
		/// The key for the Ribbon Group of Undo controls.
		/// </summary>
		/*(New)*/ public const string GroupUndo = nameof(GroupUndo);

		/// <summary>
		/// The key for the Copy control.
		/// </summary>
		/*(New)*/ public const string Copy = nameof(Copy);

		/// <summary>
		/// The key for the Cut control.
		/// </summary>
		/*(New)*/ public const string Cut = nameof(Cut);

		/// <summary>
		/// The key for the Help control.
		/// </summary>
		public const string Help = nameof(Help);

		/// <summary>
		/// The key for the Paste control.
		/// </summary>
		/*(New)*/ public const string Paste = nameof(Paste);

		/// <summary>
		/// The key for the Paste menu control.
		/// </summary>
		/*(New)*/ public const string PasteMenu = nameof(PasteMenu);

		/// <summary>
		/// The key for the Paste Special control.
		/// </summary>
		/*(New)*/ public const string PasteSpecial = nameof(PasteSpecial);

		/// <summary>
		/// The key for the Redo control.
		/// </summary>
		/*(New)*/ public const string Redo = nameof(Redo);

		/// <summary>
		/// The key for the Undo control.
		/// </summary>
		/*(New)*/ public const string Undo = nameof(Undo);

	}

}
