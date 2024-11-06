using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ScreenTips {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// Register an event handler for when screen tips are opening
			ScreenTipService.Current.ScreenTipOpening += this.OnScreenTipOpening;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The button view model used within the "Basic Usage" sample that is bound to the sample's options.
		/// </summary>
		public BarButtonViewModel? BasicUsageBarButtonViewModel => BasicUsageRibbonViewModel.Tabs[0].Groups[0].Items[0] as BarButtonViewModel;

		/// <summary>
		/// The ribbon view model for the "Basic Usage" sample.
		/// </summary>
		public RibbonViewModel BasicUsageRibbonViewModel { get; } = CreateBasicUsageRibbonViewModel();

	}

}
