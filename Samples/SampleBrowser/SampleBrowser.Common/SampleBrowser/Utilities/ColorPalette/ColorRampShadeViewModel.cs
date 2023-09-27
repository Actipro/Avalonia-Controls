using ActiproSoftware.UI.Avalonia.Media;
using ActiproSoftware.UI.Avalonia.Themes.Generation;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the <see cref="ColorPalette"/> view-model.
	/// </summary>
	public class ColorRampShadeViewModel : ObservableObjectBase {

		private readonly ColorRampShade _colorRampShade;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ColorRampShadeViewModel(ColorRampViewModel colorRamp, ColorRampShade colorRampShade) {
			ColorRamp = colorRamp;
			_colorRampShade = colorRampShade;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public UIColor Color => _colorRampShade.Color;

		public ColorRampViewModel ColorRamp { get; }

		public int Number => _colorRampShade.Number;

	}

}
