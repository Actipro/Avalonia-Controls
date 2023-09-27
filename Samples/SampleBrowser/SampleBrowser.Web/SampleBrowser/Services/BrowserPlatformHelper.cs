using Avalonia.Controls.Notifications;
using System;
using System.Runtime.InteropServices.JavaScript;

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
			var uriString = uri.AbsoluteUri;
			Interop.WindowOpen(uriString);
		}

	}
}
