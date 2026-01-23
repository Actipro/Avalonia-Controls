using ActiproSoftware.UI.Avalonia.Serialization;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.LayoutSerialization {

	/// <summary>
	/// Defines sample data associated with <see cref="CustomToolWindow"/> based on a class designed for serialization.
	/// </summary>
	public class CustomToolWindowData : XmlObjectBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Sample property storing a <c>DateTime</c> value as an XMl attribute.
		/// </summary>
		[XmlAttribute()]
		public DateTime CreationDateTime { get; set; }

		/// <summary>
		/// Returns an enumerable of <see cref="IXmlSerializerProperty"/> instances which define the properties supported by serialization.
		/// </summary>
		/// <remarks>
		/// When not using <see cref="System.Xml.Serialization.XmlSerializer"/>, derived classes should override this method to
		/// define additional properties that should be included by the serializer.
		/// </remarks>
		public override IEnumerable<IXmlSerializerProperty> GetSerializerProperties() {
			// Make sure base properties are included
			foreach (var property in base.GetSerializerProperties())
				yield return property;

			// Define the additional properties
			yield return new XmlSerializerProperty<CustomToolWindowData, DateTime>(x => CreationDateTime);
			yield return new XmlSerializerProperty<CustomToolWindowData, int>(x => InstanceId, x => ShouldSerializeInstanceId());
			yield return new XmlSerializerProperty<CustomToolWindowData, CustomVersion>(x => Version);
		}

		/// <summary>
		/// Sample property storing an <c>Int32</c> value serialized as an XML attribute with a custom name.
		/// </summary>
		[XmlAttribute("Instance")]
		public int InstanceId { get; set; } = -1;

		/// <summary>
		/// Returns if the <see cref="InstanceId"/> property should be serialized.
		/// </summary>
		public bool ShouldSerializeInstanceId()
			=> InstanceId != -1;

		/// <summary>
		/// Sample property storing an custom object value serialized as an XML element.
		/// </summary>
		/// <remarks>
		/// This custom object type requires additional configuration.
		/// </remarks>
		public CustomVersion Version { get; set; } = new(1, 0, 2);

	}

}
