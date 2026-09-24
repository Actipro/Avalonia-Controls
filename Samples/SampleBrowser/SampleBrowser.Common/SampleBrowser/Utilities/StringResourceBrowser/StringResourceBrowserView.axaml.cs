namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser;

public partial class StringResourceBrowserView : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public StringResourceBrowserView() {
		InitializeComponent();

		ViewModel = new StringResourceBrowserViewModel();

		// Select the first item
		assemblyComboBox.SelectionChanged += (sender, e) => {
			Dispatcher.UIThread.InvokeAsync(() => {
				if (stringResourceListBox.SelectedIndex == -1)
					stringResourceListBox.SelectedIndex = 0;
			});
		};
		assemblyComboBox.Loaded += (sender, e) => assemblyComboBox.SelectedIndex = 0;
		stringResourceListBox.SelectionChanged += (sender, e) => UpdateCodeTextBlock();
		customValueTextBox.TextChanged += (sender, e) => UpdateCodeTextBlock();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void UpdateCodeTextBlock() {
		codeTextBlock.Text = (stringResourceListBox.SelectedItem is StringResourceModel resource)
			? $"{resource.SRType.FullName}.SetCustomString({resource.Name.GetType().FullName}.{resource.Name}, \"{customValueTextBox.Text}\");"
			: null;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	public StringResourceBrowserViewModel? ViewModel {
		get => DataContext as StringResourceBrowserViewModel;
		set => DataContext = value;
	}

}
