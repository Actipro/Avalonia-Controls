using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Themes;
using ActiproSoftware.UI.Avalonia.Themes.Generation;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.ThemeSamples.Styling {

	public partial class UserInterfaceDensitySamples : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public UserInterfaceDensitySamples() {
			InitializeComponent();

			if (ModernTheme.TryGetCurrent(out var theme)) {
				var definition = theme.Definition;
				if (definition is not null) {
					definition.PropertyChanged += this.OnThemeDefinitionPropertyChanged;
					UpdateUserInterfaceDensityState(definition);
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnThemeDefinitionPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(ThemeDefinition.UserInterfaceDensity)) {
				UpdateUserInterfaceDensityState(sender as ThemeDefinition);
			}
		}
		private void OnUiDensitySelectionChanged(object? sender, SelectionChangedEventArgs e) {
			if (uiDensityBar?.SelectedItem is UserInterfaceDensity density)
				ApplicationViewModel.Instance.SetUserInterfaceDensityCommand.Execute(density);
		}

		private void UpdateUserInterfaceDensityState(ThemeDefinition? definition) {
			if (definition is not null) {
				uiDensityBar.SelectedItem = definition.UserInterfaceDensity;
			}
		}

	}

}
