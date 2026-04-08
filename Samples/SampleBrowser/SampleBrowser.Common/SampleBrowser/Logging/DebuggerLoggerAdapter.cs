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
	/// <param name="categoryName">The category name of the logger, or <c>null</c> if a category is not used.</param>
	internal class DebuggerLoggerAdapter(string categoryName) : DebuggerLogger(categoryName), IMSExtensionsLogger {

		// --------------------------------------------------------------------------------------------------
		// NESTED TYPES
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Provides an non-null implementation if <see cref="IDisposable"/> to wrap a potentially null instance.
		/// </summary>
		/// <param name="wrapped">The disposable to be wrapped.</param>
		class DisposableWrapper(IDisposable? wrapped) : IDisposable {
			private readonly IDisposable? _wrapped = wrapped;

			/// <inheritdoc/>
			void IDisposable.Dispose() {
				GC.SuppressFinalize(this);
				_wrapped?.Dispose();
			}
		}

		// --------------------------------------------------------------------------------------------------
		// INTERFACE IMPLEMENTATION
		// --------------------------------------------------------------------------------------------------

		/// <inheritdoc/>
		IDisposable IMSExtensionsLogger.BeginScope<TState>(TState state)
			=> new DisposableWrapper(BeginScope()); // Wrapping since interface return value should not be null

		/// <inheritdoc/>
		bool IMSExtensionsLogger.IsEnabled(MSExtensionsLogLevel logLevel)
			=> IsEnabled(logLevel.ToActiproLogLevel());

		/// <inheritdoc/>
		void IMSExtensionsLogger.Log<TState>(MSExtensionsLogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {
			// Quit if not enabled
			if (!IsEnabled(logLevel.ToActiproLogLevel()))
				return;

			// Format the text
			if (formatter is null)
				throw new ArgumentNullException(nameof(formatter));
			string text = formatter(state, exception);

			// Write the log entry
			DebugWriteLine(logLevel.ToActiproLogLevel(), exception, text);
		}

	}

}

#endif