using Avalonia.Media;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Represents a color midtone option.
	/// </summary>
	public class MidtoneColorViewModel {

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		public MidtoneColorViewModel(string name, Color color) {
			Name = name;
			Color = color;
		}

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		public Color Color { get; }

		public string Name { get; }

	}

}
