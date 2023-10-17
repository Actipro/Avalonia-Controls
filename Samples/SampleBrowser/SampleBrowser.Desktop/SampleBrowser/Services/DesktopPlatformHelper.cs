using System;
using System.Diagnostics;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Defines helper methods specific to a desktop platform.
	/// </summary>
	internal class DesktopPlatformHelper : IPlatformHelper {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IPlatformHelper.OpenExternalUri(Uri)"/>
		public void OpenExternalUri(Uri uri) {
			if (OperatingSystem.IsMacOS())
				Process.Start("open", uri.AbsoluteUri);
			else if (OperatingSystem.IsLinux())
				Process.Start("python3", $"-m webbrowser \"{uri.AbsoluteUri}\"");  // Note that this may yield a "tcgetpgrp failed: Not a tty" error in terminal
			else
				Process.Start(new ProcessStartInfo(uri.AbsoluteUri) { UseShellExecute = true });
		}

	}
}
