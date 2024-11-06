/*

RIBBON GETTING STARTED SERIES - STEP 5

STEP SUMMARY:

	Expand the Ribbon configure to include new view models for commands.

CHANGES SINCE LAST STEP:

	Added a "NotImplementedCommand" implementation to be associated with commands
	that will not perform real logic in the application.

	Added new 'CompositeCommand' properties for Undo/Redo and Cut/Copy/Paste.

	Added control view models and registered images for Undo/Redo and Cut/Copy/Paste
	commands.

	Registered images for Undo and Clipboard control groups.

*/

using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia;
using Avalonia.Media;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step05 {

	/// <summary>
	/// Defines a manager for working with the controls used by Ribbon and related menus.
	/// </summary>
	public class SampleBarManager {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public SampleBarManager() {
			// Initialize view model collection
			this.ControlViewModels = new BarControlViewModelCollection(ImageProvider);

			// Register common images used by view models
			RegisterImages();

			// Register view models for controls
			RegisterControlViewModels();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates an <see cref="IImage"/> based on the given options.
		/// </summary>
		/// <param name="options">The options which define the image to be created.</param>
		/// <param name="smallFileName">The name of the resource file that represents the small image (e.g., 16x16).</param>
		/// <param name="largeFileName">The name of the resource file that represents the large image (e.g., 32x32).</param>
		/// <returns>An <see cref="IImage"/> based on the given options, or <c>null</c> if a corresponding image is not available.</returns>
		/// <seealso cref="RegisterImages"/>
		private static IImage? CreateBitmapImage(BarImageOptions options, string? smallFileName, string? largeFileName) {
			// Determine the name of the resource file that is appropriate for the requested image size
			var fileNameResolved = options.Size switch {
				BarImageSize.Small	=> smallFileName,
				BarImageSize.Large	=> largeFileName,
				_					=> null				// Medium images not supported by this sample
			};
			if (!string.IsNullOrEmpty(fileNameResolved))
				return ImageLoader.GetIcon(fileNameResolved);

			return null;
		}

		/// <summary>
		/// Registers control view models with <see cref="ControlViewModels"/>.
		/// </summary>
		private void RegisterControlViewModels() {

			//	SAMPLE NOTE 5.1:
			//		Several new view models have been added in this step and are designated with the "(New)" comment.

			//	SAMPLE NOTE 5.2:
			//		The view models for several commands are configured with an InputGesture derived from corresponding
			//		properties on the platform-specific hotkey configuration.  These gestures are used for display purposes
			//		only (i.e., on context menus and tooltips) and **DO NOT** create any form of key binding between the
			//		gesture and the command.
			var keymap = Application.Current?.PlatformSettings?.HotkeyConfiguration;

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Copy,
				key => new BarButtonViewModel(key, CopyCommand) {
					Description = "Puts a copy of the selection on the Clipboard so you can paste it elsewhere.",
					InputGesture = keymap?.Copy.FirstOrDefault()
				});

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Cut,
				key => new BarButtonViewModel(key, CutCommand) {
					Description = "Removes the selection and puts it on the Clipboard so you can paste it elsewhere.",
					InputGesture = keymap?.Cut.FirstOrDefault()
				});

			ControlViewModels.Register(SampleBarControlKeys.Help,
				key => new BarButtonViewModel(key, HelpCommand) { Description = "Open application help." });

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Paste,
				key => new BarButtonViewModel(key, PasteCommand) {
					Description = "Adds content from the Clipboard into your document.",
					InputGesture = keymap?.Paste.FirstOrDefault()
				});

			//	SAMPLE NOTE 5.3:
			//		This Paste Menu is a split button. The primary command is "Paste", but it also has a popup menu
			//		with an additional command for "Paste Special". The following note was included in Step 3:
			//
			//			Control models are not created by BarControlViewModelCollection during registration. Only the
			//			factory method is associated with a key. When a view model is requested for the first time,
			//			it will be created as needed by invoking the registered factory method. This is particularly
			//			useful for menus or popup buttons whose view model must also populate a collection of child
			//			view models. Since all view models are registered before any view model is created, the
			//			factory method of a view model can safely refer to other view models.
			//
			//		Note that the "Paste Special" view model has yet to be registered, but it can still be accessed
			//		in the factory method for the "Paste" view model since the factory method is not executed
			//		until all registrations are complete.

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.PasteMenu,
				key => new BarSplitButtonViewModel(key, "Paste", PasteCommand) {
					Description = "Adds content from the Clipboard into your document.",
					InputGesture = keymap?.Paste.FirstOrDefault(),
					MenuItems = {
						ControlViewModels[SampleBarControlKeys.Paste],
						ControlViewModels[SampleBarControlKeys.PasteSpecial],
					}
				});

			//	SAMPLE NOTE 5.4:
			//		This sample application will add several view models to the Ribbon to demonstrate Ribbon configuration,
			//		but the actual commands will not be implemented. The "Paste Special" view model is just one example
			//		of this. These view models will be associated with a custom "NotImplementedCommand" so that the
			//		commands appear enabled in the UI and respond to interaction.

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.PasteSpecial,
				key => new BarButtonViewModel(key, NotImplementedCommand) { KeyTipText = "S" });

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Redo,
				key => new BarButtonViewModel(key, RedoCommand) {
					Description = "Redo the last operation.",
					InputGesture = keymap?.Redo.FirstOrDefault(),
					KeyTipText = "AQ"
				});

			/*(New)*/ ControlViewModels.Register(SampleBarControlKeys.Undo,
				key => new BarButtonViewModel(key, UndoCommand) {
					Description = "Undo the last operation.",
					InputGesture = keymap?.Undo.FirstOrDefault(),
					KeyTipText = "AZ"
				});

		}

		/// <summary>
		/// Registers images with <see cref="ImageProvider"/>.
		/// </summary>
		private void RegisterImages() {
			//	SAMPLE NOTE 5.5:
			//		Several of the commands in this sample do not register large images, only small images.
			//		Most applications will want to register all supported image variants. For the purpose
			//		of this sample application, the commands without large images will be used in
			//		configurations where only small images are necessary.

			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Copy, options => CreateBitmapImage(options, "Copy16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Cut, options => CreateBitmapImage(options, "Cut16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.GroupClipboard, options => CreateBitmapImage(options, "Paste16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.GroupUndo, options => CreateBitmapImage(options, "Undo16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Help, options => CreateBitmapImage(options, "Help16.png", "Help32.png"));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Paste, options => CreateBitmapImage(options, "Paste16.png", "Paste32.png"));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.PasteMenu, options => CreateBitmapImage(options, "Paste16.png", "Paste32.png"));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.PasteSpecial, options => CreateBitmapImage(options, "PasteSpecial16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Redo, options => CreateBitmapImage(options, "Redo16.png", largeFileName: null));
			/*(New)*/ ImageProvider.Register(SampleBarControlKeys.Undo, options => CreateBitmapImage(options, "Undo16.png", largeFileName: null));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The collection of control view models.
		/// </summary>
		public BarControlViewModelCollection ControlViewModels { get; }

		//	SAMPLE NOTE 5.6
		//		A 'CompositeCommand' is a special command that consists of one or more child commands working
		//		together as a single command.  Since the Undo/Redo and Cut/Copy/Paste commands may target
		//		different UI elements based on keyboard focus, the main window view will be responsible for ensuring
		//		that the respective 'CompositeCommand' instances are registered with the corresponding
		//		commands for the currently selected or focused element.  This allows the Bar Manager to provide
		//		high-level support for these command commands without having to know about the specifics of the
		//		current view.

		/// <summary>
		/// The composite command for clipboard copy operations.
		/// </summary>
		/*(New)*/ public CompositeCommand CopyCommand { get; }
			= new CompositeCommand();

		/// <summary>
		/// The composite command for clipboard cut operations.
		/// </summary>
		/*(New)*/ public CompositeCommand CutCommand { get; }
			= new CompositeCommand();

		/// <summary>
		/// The command for displaying Help.
		/// </summary>
		public ICommand HelpCommand { get; }
			= new DelegateCommand<object>(
				param => {
					// Execute
					MessageBox.Show("This is where contextual Help would be displayed.", "Help", image: MessageBoxImage.Information);
				});

		/// <summary>
		/// The provider that will be used to provide images.
		/// </summary>
		public BarImageProvider ImageProvider { get; }
			= new BarImageProvider();

		/// <summary>
		/// A command that will display a message that the logic for the command has yet to be implemented.
		/// </summary>
		public static ICommand NotImplementedCommand { get; }
			//	SAMPLE NOTE 5.7:
			//		This sample command is associated with view models that will appear in the Ribbon
			//		for demonstration purposes only. This command is used instead of NULL so the UI
			//		is enabled and responds to interaction.
			= new DelegateCommand<object>(
				(param) => {
					MessageBox.Show(
						"This control is for user interface demonstration purposes only and no application functionality has been implemented for it.",
						"Not Implemented",
						image: MessageBoxImage.Information);
				}
			);

		/// <summary>
		/// The composite command for clipboard paste operations.
		/// </summary>
		/*(New)*/ public CompositeCommand PasteCommand { get; }
			= new CompositeCommand();

		/// <summary>
		/// The composite command for redo operations.
		/// </summary>
		/*(New)*/ public CompositeCommand RedoCommand { get; }
			= new CompositeCommand();

		/// <summary>
		/// The composite command for undo operations.
		/// </summary>
		/*(New)*/ public CompositeCommand UndoCommand { get; }
			= new CompositeCommand();

	}

}
