using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ApplicationButton {

	public partial class MainControl : UserControl {

		private static ICommand DisabledCommand = new DelegateCommand<object>(_ => { /* no op */ }, _ => false);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the ribbon view model for the "Basic Usage" sample.
		/// </summary>
		private static RibbonViewModel CreateBasicUsageRibbonViewModel() {
			return new RibbonViewModel() {
				ApplicationButton = new RibbonApplicationButtonViewModel(),
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,
				Tabs = {
					new RibbonTabViewModel("Home") {
						Groups = {

							new RibbonGroupViewModel("Documents") {
								Items = {
									new BarButtonViewModel("Open", "Open Document", DisabledCommand) {
										LargeIcon = ImageLoader.GetIcon("Open32.png"),
										SmallIcon = ImageLoader.GetIcon("Open16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									},
									new BarButtonViewModel("Save", "Save Document", DisabledCommand) {
										LargeIcon = ImageLoader.GetIcon("Save32.png"),
										SmallIcon = ImageLoader.GetIcon("Save16.png"),
										ToolBarItemVariantBehavior = ItemVariantBehavior.All,
									},
								},
							},

						}
					},
				}
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The ribbon view model for the "Basic Usage" sample.
		/// </summary>
		public RibbonViewModel BasicUsageRibbonViewModel { get; } = CreateBasicUsageRibbonViewModel();

	}

}
