using ActiproSoftware.ProductSamples.FundamentalsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.ComponentModel;
using System.Text;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.MessageBoxIntro {

	/// <summary>
	/// Defines a view model for working with the <see cref="MessageBox"/> sample.
	/// </summary>
	/// <remarks>This is NOT a general view model for a MessageBox.</remarks>
	public class MessageBoxSampleViewModel : ObservableObjectBase {

		private bool _addHelpButton = false;
		private MessageBoxButtons _buttons = MessageBoxButtons.OK;
		private string _caption = string.Empty;
		private MessageBoxResult _defaultResult = MessageBoxResult.None;
		private UserPromptDisplayMode _displayMode = UserPromptDisplayMode.DialogPreferred;
		private MessageBoxImage _image = MessageBoxImage.Information;
		private MessageBoxResult _result = MessageBoxResult.None;
		private string _sampleCode = string.Empty;
		private string _text = string.Empty;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MessageBoxSampleViewModel() {
			this.Caption = "Message title";
			this.Text = "This is the message that will be displayed.";
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private string? ResolveCaption() {
			return string.IsNullOrEmpty(this.Caption)
				? null
				: this.Caption;
		}

		private void UpdateSampleCode() {
			bool hasImage = Image != MessageBoxImage.None;
			bool hasDefaultResult = DefaultResult != MessageBoxResult.None;
			bool hasHelpButton = AddHelpButton;

			string indent = new string(' ', 4);
			var sample = new StringBuilder()
				.AppendLine($"var result = await {nameof(MessageBox)}.{nameof(MessageBox.Show)}(")
				.AppendLine(indent + FormatAsString(Text) + ",")
				.AppendLine(indent + FormatAsString(ResolveCaption()) + ",")
				.Append(indent + nameof(MessageBoxButtons) + "." + Buttons);

			var requiresBuilder = (hasHelpButton || (DisplayMode == UserPromptDisplayMode.Overlay));
			if (hasImage || hasDefaultResult || requiresBuilder) {
				sample.AppendLine(",")
					.Append(indent + nameof(MessageBoxImage) + "." + Image);
				if (hasDefaultResult || requiresBuilder) {
					sample.AppendLine(",")
						.Append(indent + nameof(MessageBoxResult) + "." + DefaultResult);
					if (requiresBuilder) {
						sample.AppendLine(",")
							.AppendLine(indent + "(builder) => builder");
						if (DisplayMode == UserPromptDisplayMode.Overlay)
							sample.AppendLine(indent + indent + $".WithDisplayMode({nameof(UserPromptDisplayMode)}.{nameof(UserPromptDisplayMode.Overlay)})");
						if (hasHelpButton) {
							sample.AppendLine(indent + indent + ".WithHelpCommand(() => {")
								.AppendLine(indent + indent + indent + "// Define action to be invoked when Help is clicked")
								.Append(indent + indent + "}");
						}
					}
				}
			}
			sample.AppendLine()
				.AppendLine(");");

			this.SampleCode = sample.ToString();

			static string FormatAsString(string? input) {
				if (input is null)
					return "null";
				return "\"" + input.Replace("\"", "\\\"")
					.Replace("\t", "\\\t")
					.Replace("\n", "\\\n")
					.Replace("\r", "\\\r") + "\"";
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public bool AddHelpButton {
			get => _addHelpButton;
			set {
				if (SetProperty(ref _addHelpButton, value))
					OnPropertyChanged(nameof(ButtonsResolved));
			}
		}

		public MessageBoxButtons Buttons {
			get => _buttons;
			set {
				if (SetProperty(ref _buttons, value)) {
					OnPropertyChanged(nameof(ButtonsResolved));
					OnPropertyChanged(nameof(HasCloseButton));
				}
			} 
		}

		public MessageBoxButtons ButtonsResolved {
			get {
				var buttons = this.Buttons;
				if (AddHelpButton)
					buttons |= MessageBoxButtons.Help;
				return buttons;
			}
		}

		public string Caption {
			get => _caption;
			set => SetProperty(ref _caption, value ?? string.Empty);
		}

		public MessageBoxResult DefaultResult {
			get => _defaultResult;
			set => SetProperty(ref _defaultResult, value);
		}

		public UserPromptDisplayMode DisplayMode {
			get => _displayMode;
			set => SetProperty(ref _displayMode, value);
		}

		public bool HasCloseButton
			=> (this.Buttons == MessageBoxButtons.OK
				|| this.Buttons.HasFlag(MessageBoxButtons.Cancel)
				|| this.Buttons.HasFlag(MessageBoxButtons.Ignore));

		public MessageBoxImage Image {
			get => _image;
			set => SetProperty(ref _image, value);
		}

		public static bool IsDialogAllowed
			=> (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime);

		protected override void OnPropertyChanged(PropertyChangedEventArgs e) {
			base.OnPropertyChanged(e);

			UpdateSampleCode();
		}

		public MessageBoxResult Result {
			get => _result;
			private set => SetProperty(ref _result, value);
		}

		public string Text {
			get => _text;
			set => SetProperty(ref _text, value ?? string.Empty);
		}

		public async void ShowMessageBox() {
			// Prevent exception when requested Dialog mode on unsupported platforms
			if (DisplayMode == UserPromptDisplayMode.Dialog && !IsDialogAllowed) {
				await UserPromptBuilder.Configure().ForDialogDisplayModeNotSupportedNotice().Show();
				return;
			}

			Result = await MessageBox.Show(Text, ResolveCaption(), Buttons, Image, DefaultResult, (builder) => {
				builder.WithDisplayMode(DisplayMode);
				if (AddHelpButton)
					builder.WithHelpCommand(() => MessageBox.Show("Here is where you would show contextual help.", "Help"));
			});
		}

		public string SampleCode {
			get => _sampleCode;
			private set => SetProperty(ref _sampleCode, value ?? string.Empty);
		}

	}

}
