#if MS_LOGGING

using ActiproSoftware.Logging;
using Microsoft.Extensions.Logging;
using System;
using IMSExtensionsLogger = Microsoft.Extensions.Logging.ILogger;
using MSExtensionsLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace ActiproSoftware.SampleBrowser.Logging {

	/// <summary>
	/// Defines an adapter of <see cref="DebuggerLogger"/> for use with Microsoft logging.
	/// </summary>
	internal class DebuggerLoggerAdapter : DebuggerLogger, IMSExtensionsLogger {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <param name="categoryName">The category name of the logger, or <c>null</c> if a category is not used.</param>
		public DebuggerLoggerAdapter(string categoryName)
			: base(categoryName) { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IMSExtensionsLogger.BeginScope{TState}(TState)"/>
		IDisposable IMSExtensionsLogger.BeginScope<TState>(TState state)
			=> BeginScope();

		/// <inheritdoc cref="IMSExtensionsLogger.IsEnabled(MSExtensionsLogLevel)"/>
		bool IMSExtensionsLogger.IsEnabled(MSExtensionsLogLevel logLevel)
			=> IsEnabled(logLevel.ToActiproLogLevel());

		/// <inheritdoc cref="IMSExtensionsLogger.Log{TState}(MSExtensionsLogLevel, EventId, TState, Exception?, Func{TState, Exception?, string})"/>
		void IMSExtensionsLogger.Log<TState>(MSExtensionsLogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
			// Quit if not enabled
			if (!IsEnabled(logLevel.ToActiproLogLevel()))
				return;

			// Format the text
			if (formatter == null)
				throw new ArgumentNullException(nameof(formatter));
			string text = formatter(state, exception);

			// Write the log entry
			DebugWriteLine(logLevel.ToActiproLogLevel(), exception, text);
		}

	}

}

#endif