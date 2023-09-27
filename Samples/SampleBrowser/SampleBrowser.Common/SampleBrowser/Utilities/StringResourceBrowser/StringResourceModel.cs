using System;
using System.Reflection;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser {

	/// <summary>
	/// A string resource in an assembly.
	/// </summary>
	public record StringResourceModel(Type SRType, object Name) {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// The value of the resource.
		/// </summary>
		public string? Value
			=> SRType.InvokeMember("GetString", BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, new object[] { Name }) as string;

	}

}
