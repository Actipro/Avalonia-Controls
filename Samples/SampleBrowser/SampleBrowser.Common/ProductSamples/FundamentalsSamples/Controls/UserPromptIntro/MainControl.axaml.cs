using ActiproSoftware.ProductSamples.FundamentalsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Input;
using ActiproSoftware.UI.Avalonia.Media;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Documents;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.UserPromptIntro {

	public partial class MainControl : UserControl {

		private IImage? _basicFooterSampleImage;
		private ICommand? _contextualHelpCommand;
		private bool _standardCheckBoxSampleIsChecked = false;

		#region Property Definitions

		public static readonly DirectProperty<MainControl, IImage?> BasicFooterSampleImageProperty
			= AvaloniaProperty.RegisterDirect<MainControl, IImage?>(nameof(BasicFooterSampleImage), x => x.BasicFooterSampleImage);

		public static readonly StyledProperty<bool> ShowBasicFooterSampleImageProperty
			= AvaloniaProperty.Register<MainControl, bool>(nameof(ShowBasicFooterSampleImage), defaultValue: true);

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			UpdateBasicFooterSampleImage();

			// Indicate if dialogs are allowed on the platform
			displayModeWarning.IsVisible = !(Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnGenericHyperlinkClick(object? sender, RoutedEventArgs e) {
			ApplicationViewModel.Instance.MessageService?.ShowMessage("Use this event handler to respond to the hyperlink.", "Hyperlink Clicked");
		}

		/// <summary>
		/// Occurs when a response is provided for a user prompt control.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <see cref="UserPromptResponseEventArgs"/> that contains the event data.</param>
		private void OnUserPromptControlRespondingCancelWhenVerified(object? sender, UserPromptResponseEventArgs e) {
			// NOTE: This event handler must be executed synchronously so the sender will be blocked waiting for the response
			if ((sender is UserPromptControl userPromptControl) && (userPromptControl.IsChecked)) {
				// Cancel the response
				MessageBox.Show($"Cancelling response of '{e.Response}'", "Result Canceled");
				e.Cancel = true;
			}
		}

		private static void ShowContextualHelp(object? context) {
			ApplicationViewModel.Instance.MessageService?.ShowMessage($"Here is where you would show help for context: {context}.", "Help");
		}

		/// <summary>
		/// Configures a default <see cref="UserPromptBuilder"/> specifically for use with these samples.
		/// </summary>
		/// <param name="displayResult"><c>true</c> to display the result after it is selected; otherwise <c>false</c> and the result will not be displayed.</param>
		/// <returns>A <see cref="UserPromptBuilder"/>.</returns>
		private static UserPromptBuilder ConfigureUserPrompt(bool displayResult = true) {
			return UserPromptBuilder.Configure()
				.AfterInitialize(builder => {
					// Set a default caption if one is not already configured
					if (builder.Title is null)
						builder.WithTitle("Actipro Avalonia Controls");
				})
				.AfterShow((builder, result) => {
					if (displayResult) {
						// Notify the user of the response
						ApplicationViewModel.Instance.MessageService?.ShowMessage($"The following result was selected:  {result}", "Result");
					}
				});
		}

		private void UpdateBasicFooterSampleImage()
			=> this.BasicFooterSampleImage = ShowBasicFooterSampleImage ? ImageLoader.GetIcon("Help16.png") : null;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public IImage? BasicFooterSampleImage {
			get => _basicFooterSampleImage;
			private set => SetAndRaise(BasicFooterSampleImageProperty, ref _basicFooterSampleImage, value);
		}

		/// <summary>
		/// Gets the command used for displaying contextual help.
		/// </summary>
		public ICommand ContextualHelpCommand
			=> _contextualHelpCommand ??= new DelegateCommand<object?>(p => ShowContextualHelp(p));

		protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change) {
			if (change.Property == ShowBasicFooterSampleImageProperty)
				UpdateBasicFooterSampleImage();
			base.OnPropertyChanged(change);
		}

		public bool ShowBasicFooterSampleImage {
			get => GetValue(ShowBasicFooterSampleImageProperty);
			set => SetValue(ShowBasicFooterSampleImageProperty, value);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// DIALOG SAMPLE PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private async void OnSampleShowDialogButtonThemeClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Use any theme for buttons
			//

			// Do not assign button label or the explicit value (e.g., "Yes" or "No")
			// will prevent the ControlTheme style from setting the button content.
			await ConfigureUserPrompt()
				.WithHeaderContent("Use any theme for buttons.")
				.WithContent("This sample has applied a custom theme to buttons that changes the overall shape and gives the Yes and No buttons a distinctive color scheme with non-default labels.")
				.WithStandardButtons(MessageBoxButtons.YesNo)
				.WithButtonTheme(this.FindResource("CustomUserPromptButtonTheme") as ControlTheme)
				.Show();
		}

		private async void OnSampleShowDialogCancelResponseClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Cancel response
			//

			await ConfigureUserPrompt()
				.WithHeaderContent("Programmatically cancel the response")
				.WithContent("An event is raised when a response is triggered for a prompt. The result can be changed or set to NULL to cancel the response.")
				.WithCheckBox("Check to cancel the response", isChecked: true)
				.WithStandardButtons(MessageBoxButtons.YesNo)
				.OnResponding((builder, args) => {
					// NOTE: This event handler must be executed synchronously so the sender will be blocked waiting for the response
					if ((builder?.Instance is UserPromptControl userPromptControl) && (userPromptControl.IsChecked)) {
						// Cancel the response
						MessageBox.Show($"Cancelling response of '{args.Response}'", "Result Canceled");
						args.Cancel = true;
					}
				})
				.Show();
		}

		private async void OnSampleShowDialogCopyDetailsToClipboardClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Copy to clipboard
			//

			await ConfigureUserPrompt()
				.WithTitle("Copy to Clipboard")
				.WithHeaderContent("Copy details to the clipboard")
				.WithContent("When displayed as a dialog, the Copy command (CTRL+C on Windows) can be invoked to copy the details of the prompt to the clipboard. This sample demonstrates the clipboard functionality and how to customize the text that is placed on the clipboard for various UI elements.")
				.WithContentClipboardText("This content will be placed on the clipboard instead of the message.")
				.WithCheckBox("This checked state is reflected on the clipboard", isChecked: true)
				.WithFooterContent("Click 'Show Sample as Dialog' button and then press copy shortcut")
				.WithFooterClipboardText($"The '{nameof(UserPromptControl.Footer)}' property is auto-converted to clipboard text, but the footer of this instance is configured to use this custom text instead.")
				.WithFooterImage(ImageProvider.Default.GetImageSource(SharedImageKeys.Question))
				.WithExpandedInformationHeaderText("Show more", "Show less")
				.WithExpandedInformationContent($"Clipboard text can be customized for '{nameof(UserPromptControl.Header)}', '{nameof(UserPromptControl.Content)}', '{nameof(UserPromptControl.Footer)}', '{nameof(UserPromptControl.ButtonItems)}', '{nameof(UserPromptControl.ExpandedInformationContent)}', and '{nameof(UserPromptControl.CheckBoxContent)}'.")
				.WithExpandedInformationContentClipboardText($"Use the '{nameof(UserPromptControl.HeaderClipboardText)}', '{nameof(UserPromptControl.ContentClipboardText)}', '{nameof(UserPromptControl.FooterClipboardText)}', '{nameof(UserPromptControl.ButtonItemsClipboardText)}', '{nameof(UserPromptControl.ExpandedInformationContentClipboardText)}', and '{nameof(UserPromptControl.CheckBoxContentClipboardText)}' properties to explicitly set clipboard text.")
				.WithIsExpanded(true)
				.WithButton(MessageBoxResult.Yes)
				.WithButton(MessageBoxResult.No)
				.WithButton(_ => _
					.WithResult(MessageBoxResult.Cancel)
					.WithContentClipboardText("Quit")
				)
				.Show();
		}

		private async void OnSampleShowDialogCustomAppearanceClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Customize the appearance of a prompt based on the type of message displayed
			//

			// Determine which image is used based on the button that was clicked
			MessageBoxImage image;
			if (sender == customizedAppearanceInformationButton)
				image = MessageBoxImage.Information;
			else if (sender == customizedAppearanceQuestionButton)
				image = MessageBoxImage.Question;
			else if (sender == customizedAppearanceWarningButton)
				image = MessageBoxImage.Warning;
			else if (sender == customizedAppearanceErrorButton)
				image = MessageBoxImage.Error;
			else
				throw new NotImplementedException();

			await ConfigureUserPrompt()
				.WithTitle("Custom Theme Prompt")
				.WithHeaderContent($"Themed {image.ToString().ToLower()} message")
				.WithContent($"The color scheme for this prompt has been adjusted to further emphasize the type of message based on the image used.")
				.WithStatusImage(image)
				.WithStandardButtons(MessageBoxButtons.OKCancel)
				.WithButton(MessageBoxResult.Retry)
				.WithButton(buttonBuilder => buttonBuilder
					.WithResult(MessageBoxResult.Ignore)
					.UseAsCloseResult()
				)
				.WithStatusImageTheme() // <-- Custom extension method to tint resources based on the status icon (e.g., error messages are red)
				.Show();
		}

		private async void OnSampleShowDialogCustomButtonClassesClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Integrate classes for pre-styled buttons
			//

			await ConfigureUserPrompt()
				.WithHeaderContent("Each button can have its own style class.")
				.WithContent("Modern applications often use a mix of button styles to guide a user to a particular response, de-emphasize a less desirable response, or raise awareness about potentially dangerous responses.")
				.WithButton(_ => _
					.WithResult(MessageBoxResult.Cancel)
					.WithContent("_Skip this step")
					.WithClasses("theme-link")
				)
				.WithButton(_ => _
					.WithResult(MessageBoxResult.Yes)
					.WithClasses("theme-solid accent")
					.UseAsDefaultResult()
				)
				.WithButton(_ => _
					.WithResult(MessageBoxResult.No)
					.WithClasses("theme-outline warning")
				)
				.Show();
		}

		private async void OnSampleShowDialogCustomButtonContentClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Use any content for buttons
			//

			var imageSource = ImageLoader.GetIcon("Save16.png");

			// NOTE: This sample utilizes an extension method on UserPromptBuilder.ButtonBuilder to
			//		 easily define an icon for use with a button.  Extension methods are a great
			//		 way to make common configurations reusable.


			await ConfigureUserPrompt()
				.WithHeaderContent("Full support for custom button content.")
				.WithContent("Buttons can have any content, including images. This sample shows images used as content and demonstrates changing the horizontal alignment of all buttons from right (default) to center.")
				.WithButton(_ => _
					.WithResult(MessageBoxResult.CustomButton + 1)
					.WithContent("_Left Image")
					.WithIcon(imageSource, HorizontalAlignment.Left)
				)
				.WithButton(_ => _
					.WithResult(MessageBoxResult.CustomButton + 2)
					.WithContent("_Right Image")
					.WithIcon(imageSource, HorizontalAlignment.Right)
					.UseAsDefaultResult()
				)
				.WithButton(_ => _
					.WithResult(MessageBoxResult.CustomButton + 3)
					.WithContent("_Center Image")
					.WithIcon(imageSource, HorizontalAlignment.Center)
				)
				.WithButtonItemsHorizontalAlignment(HorizontalAlignment.Center)
				.Show();
		}

		private async void OnSampleShowDialogCustomButtonCommandsClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Each button can have its own command
			//

			// Define the custom command to be associated with each button
			var command = new ConfirmationCommand();

			await ConfigureUserPrompt()
				.WithHeaderContent("Each button can have its own command.")
				.WithContent("The default command for a button will notify the UserPromptControl of the response for that button, but you can define your own command instead. This sample demonstrates how to define custom commands by associating each button with a command that will confirm the response before submitting it.")
				.WithButton(MessageBoxResult.Yes, afterInitialize: builder => builder.WithCommand(command, builder.Instance))
				.WithButton(MessageBoxResult.No, afterInitialize: builder => builder.WithCommand(command, builder.Instance))
				.Show();
		}

		private async void OnSampleShowDialogCustomFooterContentClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Customize the footer content
			//

			// Build the footer hyperlink
			var hyperlink = new HyperlinkTextBlock() {
				Text = "here"
			};
			hyperlink.Click += OnGenericHyperlinkClick;

			await ConfigureUserPrompt()
				.WithHeaderContent("Customize the footer content")
				.WithContent("A footer can be used to provide additional context for a prompt. This sample demonstrates using a hyperlink to provide contextual help.")
				.WithFooterContent(new TextBlock() {
					Inlines = new InlineCollection() {
						new Run("Click "),
						hyperlink,
						new Run(" for more information")
					}
				})
				.WithFooterImage(ImageProvider.Default.GetImageSource(SharedImageKeys.Question))
				.WithButton(MessageBoxResult.OK)
				.Show();
		}

		private async void OnSampleShowDialogCustomHeaderAndContentClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Customize the header and content
			//

			await ConfigureUserPrompt()
				// Setting any header background will align the status icon and header content
				.WithHeaderContent("Exporting Project (Sample Project)")
				.WithHeaderForeground(Colors.White)
				.WithHeaderBackground(new LinearGradientBrush() {
					GradientStops = {
						new GradientStop(UIColor.Parse("#094c75"), 0),
						new GradientStop(UIColor.Parse("#066f5c"), 1),
					}
				})
				.WithStatusImage(ImageLoader.GetIcon("Save32.png"))
				.WithStandardButtons(MessageBoxButtons.Cancel)
				.WithContent(new StackPanel() {
					Children = {
						new TextBlock() {
							Inlines = new InlineCollection() {
								new Run("to "),
								new Run("Project Templates") { FontWeight = FontWeight.Bold },
								new Run(@" (C:\Templates\ProjectTemplates)"),
							}
						},
						new TextBlock() {
							Text = "Estimated time remaining: 1 minute",
							Margin = new Thickness(0, 2, 0, 2)
						},
						new ProgressBar() {
							Margin = new Thickness(0, 5, 0, 0),
							Minimum = 0,
							Maximum = 100,
							Value = 25,
							Height = 20,
							Classes = { "success" }
						}
					}
				})
				.WithWindowStartupLocation(WindowStartupLocation.CenterOwner)
				.Show();
		}

		private async void OnSampleShowDialogDisplayModeClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Display mode
			//

			await ConfigureUserPrompt()
				.WithHeaderContent("Display mode")
				.WithStatusImage(MessageBoxImage.Information)
				.WithContent("The same prompt can be consistently shown on different platforms even when windows are not supported.")
				.WithStandardButtons(MessageBoxButtons.YesNoCancel)
				.WithDisplayMode((UserPromptDisplayMode)displayModeSelection.SelectedValue!)
				.Show();
		}

		private async void OnSampleShowDialogExpandedInformationErrorClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Expanded information error
			//

			try {
				// Create an exception
				throw new InvalidOperationException("This exception thrown to demonstrate the exception prompt.");
			}
			catch (Exception ex) {
				// Show the prompt

				// NOTE: This sample utilizes a pre-defined extension method on UserPromptBuilder to easily
				//		 configure a user prompt for showing an exception.  Extension methods on
				//		 the UserPromptBuilder class are a great way to make common configurations
				//		 reusable.

				await UserPromptBuilder.Configure()
					.ForException(ex, "Error processing request.")
					.Show();
			}
		}

		private async void OnSampleShowDialogExpandedInformationInContentClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Use expanded information to toggle details directly within content
			//

			await ConfigureUserPrompt()
				.WithHeaderContent("Overwrite file?")
				.WithStandardButtons(MessageBoxButtons.YesNoCancel)
				.WithDefaultResult(MessageBoxResult.Cancel)
				.WithStatusImage(MessageBoxImage.Question)
				.WithExpandedInformation("Show details", "Hide details", expandedContent: null)
				.WithAutoSize(true)
				.WithContent(instance => {
					// The 'WithContent' method has an overload that allows the UserPromptControl
					// being built to be passed into this delegate. This is necessary here
					// since some properties of the content have bindings to the UserPromptControl.

					// Build the content
					return new StackPanel() {
						Children = {
							new TextBlock() {
								TextWrapping = TextWrapping.Wrap,
								Inlines = new InlineCollection() {
									new Run("A file named "),
									new Run("DeLorean.txt") { FontStyle = FontStyle.Italic},
									new Run(" already exists in this location. Do you want to overwrite the file?"),
								}
							},

							// These details only visible when expanded
							new AnimatedExpanderDecorator() {
								Child = new StackPanel() {
									Children = {
										new TextBlock() {
											Text = "Original File:",
											FontWeight = FontWeight.SemiBold,
											Margin = new Thickness(0, 10, 0, 5),
										},
										new StackPanel() {
											Margin = new Thickness(10, 0, 0, 0),
											Children = {
												new TextBlock() { Text = "File size: 88 MB" },
												new TextBlock() { Text = "Last modified: October 26, 1985" },
											}
										},
										new TextBlock() {
											Text = "Replace With:",
											FontWeight = FontWeight.SemiBold,
											Margin = new Thickness(0, 10, 0, 5),
										},
										new StackPanel() {
											Margin = new Thickness(10, 0, 0, 0),
											Children = {
												new TextBlock() { Text = "File size: 1.21 GB" },
												new TextBlock() { Text = "Last modified: October 21, 2015" },
											}
										},
									}
								},
								[!!AnimatedExpanderDecorator.IsExpandedProperty] = instance[!!UserPromptControl.IsExpandedProperty],
								[!AnimatedExpanderDecorator.IsAnimationEnabledProperty] = instance[!UserPromptControl.IsAnimationEnabledProperty],
							}
						}
					};
				})
				.Show();
		}

		private async void OnSampleShowDialogHelpButtonClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Built-in support for a 'Help' button
			//

			await ConfigureUserPrompt()
				.WithHeaderContent("Built-in support for a 'Help' button")
				.WithContent("A built-in 'Help' button can be displayed prominently in the button tray that, when invoked, can handle displaying contextual help without closing the dialog.")
				.WithStandardButtons(MessageBoxButtons.OK)
				.WithHelpCommand(this.ContextualHelpCommand, "SampleIntegratedHelp")
				.Show();
		}

		private async void OnSampleShowDialogStandardCheckBoxClick(object? sender, RoutedEventArgs e) {
			//
			// SAMPLE: Standard checkbox
			//

			// Check if the user has suppressed the message
			if (_standardCheckBoxSampleIsChecked) {
				var overrideResult = await MessageBox.Show("You indicated you didn't want to see these messages. Show message anyway?", "Show Message", MessageBoxButtons.YesNo, MessageBoxImage.Question);
				if (overrideResult == MessageBoxResult.No)
					return;
			}

			var result = await ConfigureUserPrompt(displayResult: false)
				.WithHeaderContent("Use the checkbox to allow user feedback.")
				.WithContent("A common scenario is to allow the user to opt out of future prompts.")
				.WithCheckBoxContent("_Stop showing messages like this")
				.WithIsChecked(
					getter: () => _standardCheckBoxSampleIsChecked,
					setter: (value) => _standardCheckBoxSampleIsChecked = value
				)
				.WithStandardButtons(MessageBoxButtons.OK)
				.AfterShow(async (builder, result) => {
					if (_standardCheckBoxSampleIsChecked)
						await MessageBox.Show($"You selected '{result}' and elected not to show this message again.", "Result");
					else
						await MessageBox.Show($"You selected '{result}' and will continue to see this message.", "Result");
				})
				.Show();

		}

	}

}
