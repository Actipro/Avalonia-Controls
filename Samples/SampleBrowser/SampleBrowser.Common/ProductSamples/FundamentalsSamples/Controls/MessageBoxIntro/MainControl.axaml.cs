using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.MessageBoxIntro {

	public partial class MainControl : UserControl {

		private MessageBoxSampleViewModel? _messageBoxSampleViewModel;

		#region Property Definitions

		public static readonly DirectProperty<MainControl, MessageBoxSampleViewModel?> MessageBoxSampleViewModelProperty
			= AvaloniaProperty.RegisterDirect<MainControl, MessageBoxSampleViewModel?>(nameof(MessageBoxSampleViewModel), x => x.MessageBoxSampleViewModel);

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			this.MessageBoxSampleViewModel = new MessageBoxSampleViewModel();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private async void OnSampleShowDialogCustomizationButtonClick(object? sender, RoutedEventArgs e) {
			bool stopShowing = false;
			var result = await MessageBox.Show(
				"This MessageBox has been customized to include a CheckBox.",
				"Customize Sample",
				MessageBoxButtons.OK,
				MessageBoxImage.Information,
				configure: (builder) => builder
					.WithCheckBox("_Stop showing messages like this.")
					.WithIsChecked(
						getter: () => stopShowing,
						setter: (value) => stopShowing = value
					)
				);

			if (stopShowing)
				await MessageBox.Show($"You selected '{result}' and elected not to show this message again.", "Result");
			else
				await MessageBox.Show($"You selected '{result}' and will continue to see this message.", "Result");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MessageBoxSampleViewModel? MessageBoxSampleViewModel {
			get => _messageBoxSampleViewModel;
			private set => SetAndRaise(MessageBoxSampleViewModelProperty, ref _messageBoxSampleViewModel, value);
		}

	}

}
