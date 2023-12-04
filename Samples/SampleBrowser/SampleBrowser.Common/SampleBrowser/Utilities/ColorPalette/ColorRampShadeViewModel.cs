using ActiproSoftware.UI.Avalonia.Media;
using ActiproSoftware.UI.Avalonia.Themes.Generation;
using Avalonia.Media;

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

			// IMPORTANT: When trimming for Native AOT, a template could not bind SolidColorBrush.Color to a
			// UIColor because the implicit cast from UIColor to Color was not working.  Either had to...
			//	A) Create an Avalonia.Media.Color property to use for the SolidColorBrush.Color binding, or
			//	B) Create an IBrush.
			// Used option B to avoid confusion of two color-based properties.
			Brush = new SolidColorBrush(_colorRampShade.Color);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public IBrush Brush { get; }

		public UIColor Color => _colorRampShade.Color;

		public ColorRampViewModel ColorRamp { get; }

		public int Number => _colorRampShade.Number;

	}

}
