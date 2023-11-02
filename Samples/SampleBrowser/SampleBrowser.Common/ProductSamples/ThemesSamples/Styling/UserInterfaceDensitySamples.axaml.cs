using ActiproSoftware.UI.Avalonia.Themes;
using ActiproSoftware.UI.Avalonia.Themes.Generation;
using Avalonia.Controls;
using System.Linq;

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

		private void UpdateUserInterfaceDensityState(ThemeDefinition? definition) {
			if (definition is not null) {
				var density = definition.UserInterfaceDensity;

				var compactDensityButtons = new[] { compactDensityButton1 };
				var normalDensityButtons = new[] { normalDensityButton1 };
				var spaciousDensityButtons = new[] { spaciousDensityButton1 };

				foreach (var button in compactDensityButtons)
					button.IsChecked = (density == UserInterfaceDensity.Compact);
				foreach (var button in normalDensityButtons)
					button.IsChecked = (density == UserInterfaceDensity.Normal);
				foreach (var button in spaciousDensityButtons)
					button.IsChecked = (density == UserInterfaceDensity.Spacious);
				
				foreach (var button in compactDensityButtons.Concat(normalDensityButtons).Concat(spaciousDensityButtons))
					button.Classes.Set("accent", button.IsChecked == true);
			}
		}


	}

}
