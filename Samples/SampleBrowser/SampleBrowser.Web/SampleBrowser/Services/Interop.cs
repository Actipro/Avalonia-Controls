using System.Runtime.InteropServices.JavaScript;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides access to browser interop logic.
	/// </summary>
	internal static partial class Interop {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		[JSImport("globalThis.open")]
		public static partial JSObject WindowOpen(string uri);

	}

}
