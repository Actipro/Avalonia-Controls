using System.Xml.Serialization;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.LayoutSerialization {

	/// <summary>
	/// Defines a simple class that is used by this sample to define serializable version information.
	/// </summary>
	public class CustomVersion {

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public CustomVersion() { } // Parameterless constructor required for serialization support

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="major">The major component of the version number.</param>
		/// <param name="minor">The minor component of the version number.</param>
		/// <param name="build">The build component of the version number.</param>
		/// <param name="revision">The revision component of the version number.</param>
		public CustomVersion(int major, int minor, int build, int revision = 0) {
			Major = major;
			Minor = minor;
			Build = build;
			Revision = revision;
		}

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// The major component of the version number.
		/// </summary>
		[XmlAttribute()]
		public int Major { get; set; }

		/// <summary>
		/// The minor component of the version number.
		/// </summary>
		[XmlAttribute()]
		public int Minor { get; set; }

		/// <summary>
		/// The build component of the version number.
		/// </summary>
		[XmlAttribute()]
		public int Build { get; set; }

		/// <summary>
		/// The revision component of the version number.
		/// </summary>
		[XmlAttribute()]
		public int Revision { get; set; }

		/// <summary>
		/// Indicates if the <see cref="Revision"/> property should be serialized.
		/// </summary>
		public bool ShouldSerializeRevision()
			=> Revision != 0;

	}

}
