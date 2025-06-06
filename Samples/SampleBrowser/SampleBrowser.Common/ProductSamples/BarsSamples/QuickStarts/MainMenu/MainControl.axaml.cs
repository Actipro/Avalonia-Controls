using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using Avalonia.Controls;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.MainMenu {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			BasicUsageBarMainMenuViewModel = CreateBasicUsageBarMainMenuViewModel();
			basicUsageMainMenuMvvm.DataContext = BasicUsageBarMainMenuViewModel;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private BarMainMenuViewModel CreateBasicUsageBarMainMenuViewModel() {
			return new BarMainMenuViewModel() {
				Items = {
					new BarPopupButtonViewModel("File") {
						MenuItems = {
							new BarButtonViewModel("New") { SmallIcon = ImageLoader.GetIcon("New16.png"), Command = NotImplementedCommand },
							new BarButtonViewModel("Open") { SmallIcon = ImageLoader.GetIcon("Open16.png"), Command = NotImplementedCommand },
							new BarButtonViewModel("Save") { SmallIcon = ImageLoader.GetIcon("Save16.png"), Command = NotImplementedCommand },
							new BarSeparatorViewModel(),
							new BarButtonViewModel("Exit") { KeyTipText = "X", Command = NotImplementedCommand },
						}
					},
					new BarPopupButtonViewModel("Edit") {
						MenuItems = {
							new BarButtonViewModel("Cut") { KeyTipText = "X", SmallIcon = ImageLoader.GetIcon("Cut16.png"), Command = NotImplementedCommand },
							new BarButtonViewModel("Copy") { KeyTipText = "C", SmallIcon = ImageLoader.GetIcon("Copy16.png"), Command = NotImplementedCommand },
							new BarButtonViewModel("Paste") { KeyTipText = "V", SmallIcon = ImageLoader.GetIcon("Paste16.png"), Command = NotImplementedCommand },
							new BarSeparatorViewModel(),
							new BarButtonViewModel("SelectAll") { SmallIcon = ImageLoader.GetIcon("SelectAll16.png"), Command = NotImplementedCommand },
						}
					},
					new BarPopupButtonViewModel("Format") {
						KeyTipText = "M",
						MenuItems = {
							new BarToggleButtonViewModel("Bold") { KeyTipText = "B", SmallIcon = ImageLoader.GetIcon("Bold16.png"), Command = NotImplementedCommand },
							new BarToggleButtonViewModel("Italic") { KeyTipText = "I", SmallIcon = ImageLoader.GetIcon("Italic16.png"), Command = NotImplementedCommand },
							new BarToggleButtonViewModel("Underline") { KeyTipText = "U", SmallIcon = ImageLoader.GetIcon("Underline16.png"), Command = NotImplementedCommand },
							new BarSeparatorViewModel(),
							new BarButtonViewModel("IncreaseFontSize") { KeyTipText = "SI", SmallIcon = ImageLoader.GetIcon("GrowFont16.png"), Command = NotImplementedCommand },
							new BarButtonViewModel("DecreaseFontSize") { KeyTipText = "SD", SmallIcon = ImageLoader.GetIcon("ShrinkFont16.png"), Command = NotImplementedCommand },
							new BarButtonViewModel("Subscript") { KeyTipText = "SB", SmallIcon = ImageLoader.GetIcon("Subscript16.png"), Command = NotImplementedCommand },
							new BarButtonViewModel("Superscript") { KeyTipText = "SP", SmallIcon = ImageLoader.GetIcon("Superscript16.png"), Command = NotImplementedCommand },
						}
					},
				}
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The view model for the "Basic usage" sample.
		/// </summary>
		public BarMainMenuViewModel BasicUsageBarMainMenuViewModel { get; private set; }
		
		/// <summary>
		/// Gets a special command associated with controls that are for demonstration purposes only and provide no implemented functionality.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand NotImplementedCommand { get; }
			= ApplicationViewModel.Instance.NotImplementedCommand;

	}

}
