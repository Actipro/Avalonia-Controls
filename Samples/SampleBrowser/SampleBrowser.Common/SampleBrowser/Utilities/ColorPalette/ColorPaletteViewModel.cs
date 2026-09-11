using ActiproSoftware.UI.Avalonia.Media;
using ActiproSoftware.UI.Avalonia.Themes;
using ActiproSoftware.UI.Avalonia.Themes.Generation;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides the <see cref="ColorPalette"/> view-model.
/// </summary>
public class ColorPaletteViewModel : ObservableObjectBase {

	private ColorPalette? _colorPalette;
	private bool _includeMoreShades;
	private IList<MidtoneColorViewModel>? _neutralMidtoneColors;
	private IEnumerable<ColorRampViewModel>? _ramps;
	private Hue _selectedAccentColorRampHue = Hue.Blue;
	private double _selectedNeutralDarkness;
	private MidtoneColorViewModel _selectedNeutralMidtoneColor;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public ColorPaletteViewModel() {
		_selectedNeutralMidtoneColor = NeutralMidtoneColors.First();

		// Get the accent color from the current theme
		if (
			ModernTheme.TryGetCurrent(out var theme)
			&& theme.Definition?.AccentColorRampName is { } colorRampName
			&& Enum.TryParse<Hue>(colorRampName, out var hue)
		) {
			_selectedAccentColorRampHue = hue;
		}

		UpdatePalette();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static DefaultColorPaletteFactory ResolveDefaultColorPaletteFactory(ThemeDefinition? definition)
		=> definition?.ColorPaletteFactory as DefaultColorPaletteFactory ?? new DefaultColorPaletteFactory();

	private int ShadeIncrement
		=> _includeMoreShades ? 50 : 100;

	private void UpdatePalette() {
		// Set the palette factory's neutral midtone color
		ModernTheme.TryGetCurrent(out var theme);
		var factory = ResolveDefaultColorPaletteFactory(theme?.Definition);
		factory.NeutralDarkness = SelectedNeutralDarkness;
		factory.NeutralMidtoneColor = SelectedNeutralMidtoneColor.Color;

		// Update the current theme to use the select neutral midtone color
		if (theme?.Definition is { } definition) {
			definition.AccentColorRampName = _selectedAccentColorRampHue.ToString();
			definition.ColorPaletteFactory = factory;
			theme.RefreshResources();
		}

		// Create a new color palette and update the ramps in this sample
		_colorPalette = factory.Create();
		UpdateRamps();
	}

	private void UpdateRamps()
		=> Ramps = _colorPalette?.Ramps.Select(colorRamp => new ColorRampViewModel(colorRamp, ShadeIncrement));

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	public bool IncludeMoreShades {
		get => _includeMoreShades;
		set {
			if (SetProperty(ref _includeMoreShades, value))
				UpdateRamps();
		}
	}

	public IList<MidtoneColorViewModel> NeutralMidtoneColors {
		get {
			if (_neutralMidtoneColors is null) {
				ModernTheme.TryGetCurrent(out var theme);
				var factory = ResolveDefaultColorPaletteFactory(theme?.Definition);

				_neutralMidtoneColors ??= [
					new("(Theme)", factory.NeutralMidtoneColor),
					// Subtle tones
					new("Gray", UIColor.Parse("#6c7281")),
					new("Slate", UIColor.Parse("#64738a")),
					new("Zinc", UIColor.Parse("#71717b")),
					new("Stone", UIColor.Parse("#79716b")),
					// Vivid tones
					new("Blue", UIColor.Parse("#5f86b1")),
					new("Green", UIColor.Parse("#527d52")),
					new("Purple", UIColor.Parse("#716378"))
				];
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
			if (SetProperty(ref _selectedAccentColorRampHue, value))
				UpdatePalette();
		}
	}

	public double SelectedNeutralDarkness {
		get => _selectedNeutralDarkness;
		set {
			if (SetProperty(ref _selectedNeutralDarkness, value))
				UpdatePalette();
		}
	}

	public MidtoneColorViewModel SelectedNeutralMidtoneColor {
		get => _selectedNeutralMidtoneColor;
		set {
			if (SetProperty(ref _selectedNeutralMidtoneColor, value))
				UpdatePalette();
		}
	}

}
