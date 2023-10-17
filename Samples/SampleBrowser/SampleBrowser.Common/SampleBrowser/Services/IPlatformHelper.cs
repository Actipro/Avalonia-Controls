using System;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Defines helper methods whose implementations may be platform-specific.
	/// </summary>
	public interface IPlatformHelper {

		/// <summary>
		/// Opens an external URI in a browser.
		/// </summary>
		/// <param name="uri">The URI to open.</param>
		void OpenExternalUri(Uri uri);

	}

}
