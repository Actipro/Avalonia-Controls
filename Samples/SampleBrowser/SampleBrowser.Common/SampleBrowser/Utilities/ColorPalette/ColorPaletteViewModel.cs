using ActiproSoftware.UI.Avalonia.Themes.Generation;
using System.Collections.Generic;
using System.Linq;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the <see cref="ColorPalette"/> view-model.
	/// </summary>
	public class ColorPaletteViewModel : ObservableObjectBase {

		private readonly ColorPalette _colorPalette;
		private bool _includeMoreShades;
		private IEnumerable<ColorRampViewModel>? _ramps;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ColorPaletteViewModel(ColorPalette? colorPalette = null) {
			_colorPalette = colorPalette ?? new DefaultColorPaletteFactory().Create();
		
			UpdateRamps();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		private int ShadeIncrement
			=> _includeMoreShades ? 50 : 100;

		private void UpdateRamps() {
			Ramps = _colorPalette.Ramps.Select(colorRamp => new ColorRampViewModel(colorRamp, ShadeIncrement));
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

		public IEnumerable<ColorRampViewModel>? Ramps {
			get => _ramps;
			set => SetProperty(ref _ramps, value);
		}

	}

}
