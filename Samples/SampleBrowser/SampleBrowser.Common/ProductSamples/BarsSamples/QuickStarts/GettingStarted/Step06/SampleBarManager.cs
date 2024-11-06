/*

RIBBON GETTING STARTED SERIES - STEP 6

STEP SUMMARY:

	This C# file is fundamentally unchanged for this step.


CHANGES SINCE LAST STEP:

	Prior sample comments were removed/condensed.

*/

using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia;
using Avalonia.Media;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step06 {

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

			// Access the keymap for platform key gestures
			var keymap = Application.Current?.PlatformSettings?.HotkeyConfiguration;

			ControlViewModels.Register(SampleBarControlKeys.Copy,
				key => new BarButtonViewModel(key, CopyCommand) {
					Description = "Puts a copy of the selection on the Clipboard so you can paste it elsewhere.",
					InputGesture = keymap?.Copy.FirstOrDefault()
				});

			ControlViewModels.Register(SampleBarControlKeys.Cut,
				key => new BarButtonViewModel(key, CutCommand) {
					Description = "Removes the selection and puts it on the Clipboard so you can paste it elsewhere.",
					InputGesture = keymap?.Cut.FirstOrDefault()
				});

			ControlViewModels.Register(SampleBarControlKeys.Help,
				key => new BarButtonViewModel(key, HelpCommand) { Description = "Open application help." });

			ControlViewModels.Register(SampleBarControlKeys.Paste,
				key => new BarButtonViewModel(key, PasteCommand) {
					Description = "Adds content from the Clipboard into your document.",
					InputGesture = keymap?.Paste.FirstOrDefault()
				});

			ControlViewModels.Register(SampleBarControlKeys.PasteMenu,
				key => new BarSplitButtonViewModel(key, "Paste", PasteCommand) {
					Description = "Adds content from the Clipboard into your document.",
					InputGesture = keymap?.Paste.FirstOrDefault(),
					MenuItems = {
						ControlViewModels[SampleBarControlKeys.Paste],
						ControlViewModels[SampleBarControlKeys.PasteSpecial],
					}
				});

			ControlViewModels.Register(SampleBarControlKeys.PasteSpecial,
				key => new BarButtonViewModel(key, NotImplementedCommand) { KeyTipText = "S" });

			ControlViewModels.Register(SampleBarControlKeys.Redo,
				key => new BarButtonViewModel(key, RedoCommand) {
					Description = "Redo the last operation.",
					InputGesture = keymap?.Redo.FirstOrDefault(),
					KeyTipText = "AQ"
				});

			ControlViewModels.Register(SampleBarControlKeys.Undo,
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
			ImageProvider.Register(SampleBarControlKeys.Copy, options => CreateBitmapImage(options, "Copy16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Cut, options => CreateBitmapImage(options, "Cut16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.GroupClipboard, options => CreateBitmapImage(options, "Paste16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.GroupUndo, options => CreateBitmapImage(options, "Undo16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Help, options => CreateBitmapImage(options, "Help16.png", "Help32.png"));
			ImageProvider.Register(SampleBarControlKeys.Paste, options => CreateBitmapImage(options, "Paste16.png", "Paste32.png"));
			ImageProvider.Register(SampleBarControlKeys.PasteMenu, options => CreateBitmapImage(options, "Paste16.png", "Paste32.png"));
			ImageProvider.Register(SampleBarControlKeys.PasteSpecial, options => CreateBitmapImage(options, "PasteSpecial16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Redo, options => CreateBitmapImage(options, "Redo16.png", largeFileName: null));
			ImageProvider.Register(SampleBarControlKeys.Undo, options => CreateBitmapImage(options, "Undo16.png", largeFileName: null));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The collection of control view models.
		/// </summary>
		public BarControlViewModelCollection ControlViewModels { get; }

		/// <summary>
		/// The composite command for clipboard copy operations.
		/// </summary>
		public CompositeCommand CopyCommand { get; }
			= new CompositeCommand();

		/// <summary>
		/// The composite command for clipboard cut operations.
		/// </summary>
		public CompositeCommand CutCommand { get; }
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
		public CompositeCommand PasteCommand { get; }
			= new CompositeCommand();

		/// <summary>
		/// The composite command for redo operations.
		/// </summary>
		public CompositeCommand RedoCommand { get; }
			= new CompositeCommand();

		/// <summary>
		/// The composite command for undo operations.
		/// </summary>
		public CompositeCommand UndoCommand { get; }
			= new CompositeCommand();

	}

}
