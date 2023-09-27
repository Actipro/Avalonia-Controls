#if MS_LOGGING

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace ActiproSoftware.SampleBrowser.Logging {

	/// <summary>
	/// Defines a provider of <see cref="DebuggerLoggerAdapter"/> for use with Microsoft logging.
	/// </summary>
	internal class DebuggerLoggerProvider : ILoggerProvider {

		private readonly ConcurrentDictionary<string, DebuggerLoggerAdapter> _loggers = new ();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="ILoggerProvider.CreateLogger(string)"/>
		ILogger ILoggerProvider.CreateLogger(string categoryName) {
			return _loggers.GetOrAdd(categoryName, name => new DebuggerLoggerAdapter(name));
		}

		/// <summary>
		/// Disposes the object and removes references to all loggers.
		/// </summary>
		void IDisposable.Dispose() {
			_loggers.Clear();
		}
	}
}

#endif