#if MS_LOGGING

using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ActiproSoftware.SampleBrowser.Logging;

/// <summary>
/// Defines a provider of <see cref="DebuggerLoggerAdapter"/> for use with Microsoft logging.
/// </summary>
internal class DebuggerLoggerProvider : ILoggerProvider {

	private readonly ConcurrentDictionary<string, DebuggerLoggerAdapter> _loggers = new();

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	ILogger ILoggerProvider.CreateLogger(string categoryName)
		=> _loggers.GetOrAdd(categoryName, name => new DebuggerLoggerAdapter(name));

	void IDisposable.Dispose()
		=> _loggers.Clear();

}

#endif