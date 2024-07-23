using System;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Defines helper methods specific to a browser platform.
	/// </summary>
	internal class BrowserPlatformHelper : IPlatformHelper {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IPlatformHelper.OpenExternalUri(Uri)"/>
		public void OpenExternalUri(Uri uri) {
			// NOTE: An issue with ILauncher in Avalonia v11.1.0 prevents it from opening URI's from browser applications,
			//  so this workaround is necessary until the issue is fixed.
			var uriString = uri.AbsoluteUri;
			Interop.WindowOpen(uriString);
		}

	}
}
