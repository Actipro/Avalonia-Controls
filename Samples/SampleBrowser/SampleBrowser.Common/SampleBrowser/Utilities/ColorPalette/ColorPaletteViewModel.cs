using ActiproSoftware.UI.Avalonia.Media;
using ActiproSoftware.UI.Avalonia.Themes;
using ActiproSoftware.UI.Avalonia.Themes.Generation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the <see cref="ColorPalette"/> view-model.
	/// </summary>
	public class ColorPaletteViewModel : ObservableObjectBase {

		private ColorPalette? _colorPalette;
		private bool _includeMoreShades;
		private IList<MidtoneColorViewModel>? _neutralMidtoneColors;
		private IEnumerable<ColorRampViewModel>? _ramps;
		private Hue _selectedAccentColorRampHue = Hue.Blue;
		private MidtoneColorViewModel _selectedNeutralMidtoneColor;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ColorPaletteViewModel() {
			_selectedNeutralMidtoneColor = NeutralMidtoneColors.First();

			// Get the accent color from the current theme
			if (ModernTheme.TryGetCurrent(out var theme) && (theme.Definition is not null)) {
				if (Enum.TryParse<Hue>(theme.Definition.AccentColorRampName, out var hue))
					_selectedAccentColorRampHue = hue;
			}

			UpdatePalette();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		private int ShadeIncrement
			=> _includeMoreShades ? 50 : 100;

		private void UpdatePalette() {
			// Set the palette factory's neutral midtone color
			ModernTheme.TryGetCurrent(out var theme);
			var factory = theme?.Definition?.ColorPaletteFactory as DefaultColorPaletteFactory ?? new DefaultColorPaletteFactory();
			factory.NeutralMidtoneColor = SelectedNeutralMidtoneColor.Color;

			// Update the current theme to use the select neutral midtone color
			if (theme?.Definition is not null) {
				theme.Definition.AccentColorRampName = _selectedAccentColorRampHue.ToString();
				theme.Definition.ColorPaletteFactory = factory;
				theme.RefreshResources();
			}

			// Create a new color palette and update the ramps in this sample
			_colorPalette = factory.Create();
			UpdateRamps();
		}

		private void UpdateRamps() {
			Ramps = _colorPalette?.Ramps.Select(colorRamp => new ColorRampViewModel(colorRamp, ShadeIncrement));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public bool IncludeMoreShades {
			get => _includeMoreShades;
			set {
				SetProperty(ref _includeMoreShades, value);
				UpdateRamps();
			}
		}

		public IList<MidtoneColorViewModel> NeutralMidtoneColors {
			get {
				if (_neutralMidtoneColors is null) {
					ModernTheme.TryGetCurrent(out var theme);
					var factory = theme?.Definition?.ColorPaletteFactory as DefaultColorPaletteFactory ?? new DefaultColorPaletteFactory();

					_neutralMidtoneColors ??= new List<MidtoneColorViewModel> {
						new MidtoneColorViewModel("(From Current Theme)", factory.NeutralMidtoneColor),
						// Subtle tones
						new MidtoneColorViewModel("Gray", UIColor.Parse("#6c7281")),
						new MidtoneColorViewModel("Slate", UIColor.Parse("#64738a")),
						new MidtoneColorViewModel("Zinc", UIColor.Parse("#71717b")),
						new MidtoneColorViewModel("Stone", UIColor.Parse("#79716b")),
						// Vivid tones
						new MidtoneColorViewModel("Blue", UIColor.Parse("#5f86b1")),
						new MidtoneColorViewModel("Green", UIColor.Parse("#527d52")),
						new MidtoneColorViewModel("Purple", UIColor.Parse("#716378"))
					};
				}

				return _neutralMidtoneColors;
			}
		}

		public IEnumerable<ColorRampViewModel>? Ramps {
			get => _ramps;
			private set => SetProperty(ref _ramps, value);
		}
		
		public Hue SelectedAccentColorRampHue {
			get => _selectedAccentColorRampHue;
			set {
				SetProperty(ref _selectedAccentColorRampHue, value);
				UpdatePalette();
			}
		}
		
		public MidtoneColorViewModel SelectedNeutralMidtoneColor {
			get => _selectedNeutralMidtoneColor;
			set {
				SetProperty(ref _selectedNeutralMidtoneColor, value);
				UpdatePalette();
			}
		}

	}

}
