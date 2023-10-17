using Avalonia.Controls;
using Avalonia.Threading;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser {

	public partial class StringResourceBrowserView : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void UpdateCodeTextBlock() {
			if (stringResourceListBox.SelectedItem is StringResourceModel resource)
				codeTextBlock.Text = $"{resource.SRType.FullName}.SetCustomString({resource.Name.GetType().FullName}.{resource.Name}, \"{customValueTextBox.Text}\");";
			else
				codeTextBlock.Text = null;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public StringResourceBrowserViewModel? ViewModel {
			get => DataContext as StringResourceBrowserViewModel;
			set => DataContext = value;
		}

	}

}
