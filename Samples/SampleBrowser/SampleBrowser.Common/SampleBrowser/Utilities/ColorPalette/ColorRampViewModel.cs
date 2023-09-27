using ActiproSoftware.UI.Avalonia.Themes.Generation;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the <see cref="ColorPalette"/> view-model.
	/// </summary>
	public class ColorRampViewModel : ObservableObjectBase {

		private readonly ColorRamp _colorRamp;
		private readonly int _shadeIncrement;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ColorRampViewModel(ColorRamp colorRamp, int shadeIncrement) {
			_colorRamp = colorRamp ?? throw new ArgumentNullException(nameof(colorRamp));
			_shadeIncrement = shadeIncrement;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private IEnumerable<int> ShadeNumbers {
			get {
				for (var shadeNumber = _shadeIncrement; shadeNumber < 1000; shadeNumber += _shadeIncrement)
					yield return shadeNumber;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public string Name => _colorRamp.Name;

		public IEnumerable<ColorRampShadeViewModel> Shades
			=> ShadeNumbers.Select(shadeNumber => new ColorRampShadeViewModel(this, _colorRamp.Shades.Resolve(shadeNumber)));

		public Color VeryDarkForegroundColor
			=> _colorRamp.Shades.Resolve(900).Color;

		public Color VeryLightForegroundColor
			=> _colorRamp.Shades.Resolve(100).Color;

	}

}
