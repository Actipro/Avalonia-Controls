using Avalonia;
using Avalonia.Controls.Primitives;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Renders a section heading.
	/// </summary>
	public class SectionHeading : TemplatedControl {

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="Text"/> property.
		/// </summary>
		public static readonly StyledProperty<string> TextProperty
			= AvaloniaProperty.Register<DocumentViewer, string>(nameof(Text));

		#endregion
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The heading text.
		/// </summary>
		public string Text {
			get => GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

	}

}
