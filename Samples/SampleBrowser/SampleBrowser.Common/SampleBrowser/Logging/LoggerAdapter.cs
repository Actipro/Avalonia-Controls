#if MS_LOGGING

using ActiproSoftware.Logging;
using Microsoft.Extensions.Logging;
using System;
using ActiproLogLevel = ActiproSoftware.Logging.LogLevel;
using IMSExtensionsLogger = Microsoft.Extensions.Logging.ILogger;

namespace ActiproSoftware.SampleBrowser.Logging {

	/// <summary>
	/// Defines an adapter to allow Microsoft's <c>ILogger</c> to work with Actipro's <see cref="Logger"/>.
	/// </summary>
	internal class LoggerAdapter : Logger {

		private readonly IMSExtensionsLogger _wrappedLogger;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerAdapter"/> class.
		/// </summary>
		/// <param name="wrappedLogger">The Microsoft-based logger.</param>
		internal LoggerAdapter(IMSExtensionsLogger wrappedLogger) {
			_wrappedLogger = wrappedLogger ?? throw new ArgumentNullException(nameof(wrappedLogger));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		public override IDisposable BeginScope(string? messageFormat, params object?[] args) {
			return _wrappedLogger.BeginScope(messageFormat ?? string.Empty, args);
		}

		/// <inheritdoc/>
		public override bool IsEnabled(ActiproLogLevel logLevel) {
			return _wrappedLogger.IsEnabled(logLevel.ToMicrosoftLogLevel());
		}

		/// <inheritdoc/>
		public override void Log(ActiproLogLevel logLevel, Exception? exception, string? message, params object?[] args) {
			_wrappedLogger.Log(logLevel.ToMicrosoftLogLevel(), exception, message, args);
		}

		/// <inheritdoc/>
		public override void Log(ActiproLogLevel logLevel, Func<string?> messageFactory, Exception? exception) {
			var msLogLevel = logLevel.ToMicrosoftLogLevel();
			if (_wrappedLogger.IsEnabled(msLogLevel))
				_wrappedLogger.Log(msLogLevel, exception, messageFactory?.Invoke());
		}

	}
}

#endif