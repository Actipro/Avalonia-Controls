/*

RIBBON GETTING STARTED SERIES - STEP 5

STEP SUMMARY:

	Improve user experience by focusing the editor control when the window is opened.

CHANGES SINCE LAST STEP:

	Added the 'WindowBase.OnOpened' override to include logic for setting focus to
	the editor control.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using System;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step05 {

	public partial class MainWindow : RibbonWindow {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public MainWindow() {
			InitializeComponent();

			// Configure this view with the new view model
			ViewModel = new SampleWindowViewModel();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override void OnOpened(EventArgs e) {
			base.OnOpened(e);

			//	SAMPLE NOTE 5.1:
			//		Ensure the editor has initial focus when the window is opened
			editor.Focus();
		}

		/// <summary>
		/// The view model for this view.
		/// </summary>
		private SampleWindowViewModel? ViewModel {
			get => DataContext as SampleWindowViewModel;
			set => DataContext = value;
		}

	}
}
