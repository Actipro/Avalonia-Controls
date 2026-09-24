namespace ActiproSoftware.SampleBrowser.Utilities.ThemeResourceBrowser;

public partial class ThemeResourceBrowserView : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public ThemeResourceBrowserView() {
		InitializeComponent();

		ViewModel = new ThemeResourceBrowserViewModel(ActualThemeVariant);
		ActualThemeVariantChanged += OnActualThemeVariantChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnActualThemeVariantChanged(object? sender, EventArgs e) {
		if (ViewModel is { } viewModel)
			viewModel.Theme = ActualThemeVariant;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	public ThemeResourceBrowserViewModel? ViewModel {
		get => DataContext as ThemeResourceBrowserViewModel;
		set => DataContext = value;
	}

}
