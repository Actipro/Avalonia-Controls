/*

RIBBON GETTING STARTED SERIES - STEP 2

STEP SUMMARY:

	This view model will serve as the base model for the sample application and
	will be bound to the view. This view model also defines a child view model
	that is bound to the Ribbon control.

	Subsequent steps will continue to build out these view models.

CHANGES SINCE LAST STEP:

	This is the first step containing this file.

*/

using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step02 {

	//	SAMPLE NOTE 2.1:
	//		The ObservableObjectBase class is a simple base class that implements the
	//		INotifyPropertyChanged interface and defines a helper method to raise the
	//		corresponding PropertyChanged event when necessary. You can use the class
	//		in your own application's view models or simply implement the required
	//		INotifyPropertyChanged interface instead.

	/// <summary>
	/// Defines the view model for this sample.
	/// </summary>
	public class SampleWindowViewModel : ObservableObjectBase {

		private ICommand? _helpCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public SampleWindowViewModel() {
			//	SAMPLE NOTE 2.2:
			//		The RibbonViewModel is pre-defined in the MVVM library to easily configure a
			//		Ribbon using MVVM. The QuickAccessToolBarMode property previously configured
			//		directly in XAML has been moved to the RibbonViewModel definition.
			//		Future steps will continue to expand the RibbonViewModel definition.

			// Initialize the view model for the Ribbon
			this.Ribbon = new RibbonViewModel() {
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The command for displaying Help.
		/// </summary>
		public ICommand HelpCommand
			//	SAMPLE NOTE 2.3:
			//		ActiproSoftware.Windows.Input.DelegateCommand<T> is a great class for defining
			//		implementations of ICommand within a view model by delegating the Execute and
			//		optional CanExecute logic.  Our samples will use DelegateCommand, but any type
			//		that implements System.Windows.Input.ICommand can be used. By default, CanExecute
			//		will return 'true', so a delegate is only required if the command is not always
			//		available. Since the Help command is always available, only the Execute delegate
			//		must be implemented.
			=> _helpCommand ??= new DelegateCommand<object>(
				param => {
					// Execute
					MessageBox.Show("This is where contextual Help would be displayed.", "Help", image: MessageBoxImage.Information);
				});

		/// <summary>
		/// The view model for the ribbon control.
		/// </summary>
		public RibbonViewModel Ribbon { get; }

	}

}
