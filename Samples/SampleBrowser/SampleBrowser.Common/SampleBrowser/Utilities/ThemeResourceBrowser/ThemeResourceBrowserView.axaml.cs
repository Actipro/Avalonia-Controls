using Avalonia.Controls;

namespace ActiproSoftware.SampleBrowser.Utilities.ThemeResourceBrowser {

	public partial class ThemeResourceBrowserView : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ThemeResourceBrowserView() {
			InitializeComponent();

			ViewModel = new ThemeResourceBrowserViewModel(this.ActualThemeVariant);
			ActualThemeVariantChanged += this.OnActualThemeVariantChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnActualThemeVariantChanged(object? sender, System.EventArgs e) {
			var viewModel = this.ViewModel;
			if (viewModel is not null)
				viewModel.Theme = this.ActualThemeVariant;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ThemeResourceBrowserViewModel? ViewModel {
			get => DataContext as ThemeResourceBrowserViewModel;
			set => DataContext = value;
		}

	}

}
