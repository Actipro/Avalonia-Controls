/*

RIBBON GETTING STARTED SERIES - STEP 3

STEP SUMMARY:

	A "Bar Manager" class is used to manage the view models and related logic that will
	support the display of controls in the Ribbon.

CHANGES SINCE LAST STEP:

	This is the first step containing this file.

*/

using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Media;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step03 {

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
			//	SAMPLE NOTE 3.1:
			//		Initialize a collection of control view models using the BarControlViewModelCollection
			//		located in the MVVM assembly. An ImageProvider is passed so that registered images can be
			//		automatically assigned to view models created by the collection. See the 'RegisterImages'
			//		and 'RegisterControlViewModels' methods for additional details.
			this.ControlViewModels = new BarControlViewModelCollection(ImageProvider);

			//	SAMPLE NOTE 3.2:
			//		Register images that will be available to controls. See the 'RegisterImages' method for
			//		additional details.
			RegisterImages();

			//	SAMPLE NOTE 3.3:
			//		Register the view models that will be available for controls. See the 'RegisterControlViewModels'
			//		method for additional details.
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

			//	SAMPLE NOTE 3.4:
			//		This helper method is used as a delegate when registering images with BarImageProvider.
			//
			//		The "Classic" Ribbon layout uses large (e.g., 32x32) and small (e.g., 16x16) images for
			//		controls based on their current variant size or placement. The "Simplified" Ribbon layout
			//		prefers medium (e.g., 24x24) images (instead of large images), but also uses small images.
			//		This helper method is intended to simplify the process of defining the large and small
			//		images for a "Classic" layout and intentionally does not provide explicit images for the
			//		"Simplified" layout.
			//		
			//		Users wanting to support as "Simplified" layout will find that the Ribbon looks best with
			//		explicitly-defined medium images since, if a medium image is not defined, the small image
			//		will be used instead.
			//
			//		This example uses the ImageLoader class (included with the Sample Browser application)
			//		to load an IImage from files stored as resources with the sample application, but any
			//		IImage can be used.
			//		
			//		See the RegisterImages method for more details.

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
			//	SAMPLE NOTE 3.5:
			//		The MVVM assembly includes BarControlViewModelCollection, an implementation of a view model
			//		collection that allows delegates to be externally registered for creation of view models.
			//
			//		The MVVM assembly also includes pre-defined view models for common Bar control types. The
			//		most common control type is BarButtonViewModel that represents a typical button on a Ribbon.
			//
			//		So far, our sample application has defined its own Button (outside of the Ribbon), but we
			//		want to relocate that button to the Ribbon.
			//
			//		For this sample, we will register a BarButtonViewModel to represent the Help button. A
			//		unique key is used to referenced all objects used by the Ribbon and related menus, and an
			//		entry has been defined for the Help control. Now we will register the factory delegate that
			//		will be called to create the view model for the Help control when it is needed.
			//
			//		Control models are not created by BarControlViewModelCollection during registration. Only the
			//		factory method is associated with a key. When a view model is requested for the first time,
			//		it will be created as needed by invoking the registered factory method. This is particularly
			//		useful for menus or popup buttons whose view model must also populate a collection of child
			//		view models. Since all view models are registered before any view model is created, the
			//		factory method of a view model can safely refer to other view models.

			ControlViewModels.Register(
				SampleBarControlKeys.Help,						// The key used to refer to the control in the collection
				key => new BarButtonViewModel(					// Delegate factory creates a new instance of the view model
					key,										// Use the same key when building the view model
					"Help",										// Provide a default label
					"H",										// Provide the key tip used when accessing the control with the keyboard
					HelpCommand) {								// The command associated with the button
						Description = "Open application help."	// The ScreenTip description (i.e. ToolTip.Tip) for the control
					});

			//	SAMPLE NOTE 3.6:
			//		The BarButtonViewModel has properties for SmallIcon, MediumIcon, and LargeIcon that can be explicitly
			//		defined when created. Since we are using an ImageProvider that has icons registered for the Help control,
			//		those properties will be automatically populated by the BarControlViewModelCollection when creating a
			//		view model. Icons can only be automatically assigned if the key used to register the icons match the key
			//		used to register the control!
			//		
			//		If desired, the ImageProvider does not have to be used and appropriate values can be assigned to the
			//		icon properties when the view model is created.
			//		
			//		Even if the ImageProvider is used, icons will only be automatically assigned if the created view
			//		model has a NULL value for each icon property. Otherwise it assumes icons are being explicitly
			//		defined by the view model. This means even if you use ImageProvider you can override the automatic
			//		assignment of icons by explicitly defining an object for any of the icon properties.
		}

		/// <summary>
		/// Registers images with <see cref="ImageProvider"/>.
		/// </summary>
		private void RegisterImages() {
			//	SAMPLE NOTE 3.7:
			//		The ImageProvider can be used to return an image for any key. The BarControlViewModelCollection
			//		(which is also used by this sample) has built-in support to automatically populate the image properties
			//		of a view model if the ImageProvider has an image registered with the same key as the view model.
			//
			//		In this sample, we will use the same keys from SampleBarControlKeys when registering images and
			//		view models to take advantage of automatic image assignment in BarControlViewModelCollection.
			//
			//		ImageProvider is convenient and promotes reuse of images, but does not have to be used. Image properties
			//		can also be explicitly assigned when creating the view model.
			
			ImageProvider.Register(
				SampleBarControlKeys.Help,											// The key used to lookup the image must match the control key for auto-assignment
				options => CreateBitmapImage(options, "Help16.png", "Help32.png"));	// The factory delegate called to return the requested image
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		//	SAMPLE NOTE 3.8:
		//		The Bars.MVVM assembly includes an implementation of a view model collection
		//		that allows delegates to be externally registered for creation of view models.
		//		See the RegisterControlViewModels method in this class for more information.

		/// <summary>
		/// The collection of control view models.
		/// </summary>
		public BarControlViewModelCollection ControlViewModels { get; }


		/// <summary>
		/// The command for displaying Help.
		/// </summary>
		public ICommand HelpCommand { get; }
			//	SAMPLE NOTE 3.9:
			//		This command implementation was migrated from the main window's view model
			//		that was created in a previous step.
			= new DelegateCommand<object>(
				param => {
					// Execute
					MessageBox.Show("This is where contextual Help would be displayed.", "Help", image: MessageBoxImage.Information);
				});


		//	SAMPLE NOTE 3.10:
		//		The Bars.MVVM assembly includes a default implementation of IBarImageProvider
		//		that allows delegates to be externally registered for providing images. See
		//		the RegisterImages method in this class for more information.

		/// <summary>
		/// The provider that will be used to provide images.
		/// </summary>
		public BarImageProvider ImageProvider { get; }
			= new BarImageProvider();

	}

}
