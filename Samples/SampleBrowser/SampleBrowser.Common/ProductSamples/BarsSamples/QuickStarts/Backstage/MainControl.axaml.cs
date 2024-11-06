using ActiproSoftware.UI.Avalonia.Controls.Bars;
using Avalonia;
using Avalonia.Controls;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.Backstage {

	public partial class MainControl : UserControl {

		private BackstageOptionsViewModel? _backstageOptionsViewModel;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnBackstageIsOpenChanged(object? sender, AvaloniaPropertyChangedEventArgs e) {
			// Optionally pre-select the 'Options' tab when opening the backstage
			if (sender is RibbonBackstage backstage
				&& backstage.IsOpen
				&& !BackstageOptions.CanSelectFirstTabOnOpen
				&& !string.IsNullOrWhiteSpace(BackstageOptions.SelectedTabKeyOnOpen)) {

				// Find the desired tab to select
				var tab = backstage.Items.OfType<RibbonBackstageTabItem>()
					.FirstOrDefault(tabItem => tabItem.Key == BackstageOptions.SelectedTabKeyOnOpen);

				// Configure the backstage selection
				if (tab is not null)
					backstage.SelectedItem = tab;
			}
		}

		private void OnBackstagePropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e) {
			if (e.Property == RibbonBackstage.IsOpenProperty)
				OnBackstageIsOpenChanged(sender, e);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public BackstageOptionsViewModel BackstageOptions
			=> _backstageOptionsViewModel ??= new BackstageOptionsViewModel();

	}

}
