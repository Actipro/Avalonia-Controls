using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ActiproSoftware.ProductSamples.SharedSamples.Controls.HyperlinkTextBlockIntro {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			HyperlinkCommand = new DelegateCommand<object>(HyperlinkCommandExecuted, HyperlinkCommandCanExecute);
			commandSample.Command = HyperlinkCommand;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private bool HyperlinkCommandCanExecute(object? parameter)
			=> commandIsEnabledCheckBox.IsChecked == true;

		private void HyperlinkCommandExecuted(object? parameter)
			=> ApplicationViewModel.Instance.MessageService?.ShowMessage("The hyperlink command was invoked.");

		private void OnCommandEnabledCheckBoxClick(object? sender, RoutedEventArgs e)
			=> HyperlinkCommand.RaiseCanExecuteChanged();

		private void OnHyperlinkTextBlockClicked(object? sender, RoutedEventArgs e)
			=> ApplicationViewModel.Instance.MessageService?.ShowMessage("The hyperlink was clicked.");

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public DelegateCommand<object> HyperlinkCommand { get; }

	}

}
