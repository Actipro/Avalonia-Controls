using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using System;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ScreenTips {

	public partial class MainControl : UserControl {

		private IDisposable? _inputElementKeyDownEventRegistration;

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		public MainControl() {
			InitializeComponent();

			// Register an event handler for when screen tips are opening
			ScreenTipService.Current.ScreenTipOpening += this.OnScreenTipOpening;
		}

		// --------------------------------------------------------------------------------------------------
		// NON-PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Creates the ribbon view model for the "Basic Usage" sample.
		/// </summary>
		private static RibbonViewModel CreateBasicUsageRibbonViewModel() {
			return new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("SampleTab") {
						Groups = {

							new RibbonGroupViewModel("SampleGroup") {
								CanAutoCollapse = false,
								Items = {

									// This view model is bound to the sample's options
									new BarButtonViewModel("BasicUsage", "Label") {
										InputGesture = TextBox.PasteGesture,
										Title = null,
										Description = "When the header is undefined (null), the label is used as the header.",
										LargeIcon = ImageLoader.GetIcon("QuickStart32.png"),
										SmallIcon = ImageLoader.GetIcon("QuickStart16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									},

								},
							},

						}
					},
				}
			};
		}

		private void OnScreenTipOpening(object? sender, CancelRoutedEventArgs e) {
			if ((sender is Control control) && (ToolTip.GetTip(control) is ScreenTip screenTip)) {
				var key = BarControlService.GetKey(control);

				// Customize the ScreenTip for specific controls
				if (key == "DynamicScreenTip") {
					// Dynamically include the time stamp in the footer
					screenTip.Footer = $"Displayed at: {DateTime.Now}";
				}
			}
		}

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// The button view model used within the "Basic Usage" sample that is bound to the sample's options.
		/// </summary>
		public BarButtonViewModel? BasicUsageBarButtonViewModel => BasicUsageRibbonViewModel.Tabs[0].Groups[0].Items[0] as BarButtonViewModel;

		/// <summary>
		/// The ribbon view model for the "Basic Usage" sample.
		/// </summary>
		public RibbonViewModel BasicUsageRibbonViewModel { get; } = CreateBasicUsageRibbonViewModel();

		private void OnInputElementKeyDown(InputElement sender, KeyEventArgs e) {
			// Check for any ScreenTip that might have contextual help
			if (!e.Handled && (e.Key == Key.F1)) {
				
				// The CurrentScreenTip property is assigned when a ScreenTip opens and cleared when
				//   it closes.  Since pressing a key also causes the ScreenTip to close, it is important
				//   to check the CurrentScreenTip property during KeyDown events.
				if (ScreenTipService.Current.CurrentScreenTip is { } screenTip) {

					// This sample uses the ScreenTip.Tag property to store contextual help information.
					//   This could be a unique identifier or URI for online help.
					if (screenTip.Tag is { } helpId) {
						e.Handled = true;

						// This sample will show a message in the application
						var message = "Here is where you can show contextual help for the following:" + Environment.NewLine + Environment.NewLine + helpId;

						// Include target control in the message if it can be determined
						if (screenTip?.FindLogicalAncestorOfType<Popup>()?.PlacementTarget is { } targetControl)
							message += Environment.NewLine + Environment.NewLine + "Target: " + targetControl.GetType();

						ApplicationViewModel.Instance.MessageService?.ShowMessage(message, "Contextual Help", NotificationType.Information);
					}
				}
			}
		}

		protected override void OnLoaded(RoutedEventArgs e) {
			// Listen for F1 key to show contextual help from screen tip
			_inputElementKeyDownEventRegistration = InputElement.KeyDownEvent.AddClassHandler<InputElement>(OnInputElementKeyDown, RoutingStrategies.Bubble, handledEventsToo: false);
			
			base.OnLoaded(e);
		}

		protected override void OnUnloaded(RoutedEventArgs e) {
			// Stop listening for key events
			_inputElementKeyDownEventRegistration?.Dispose();
			_inputElementKeyDownEventRegistration = null;

			base.OnUnloaded(e);
		}

	}

}
