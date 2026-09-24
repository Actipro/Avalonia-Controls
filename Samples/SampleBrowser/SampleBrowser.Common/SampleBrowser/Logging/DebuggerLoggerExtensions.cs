#if MS_LOGGING

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using ActiproLogLevel = ActiproSoftware.Logging.LogLevel;
using MSExtensionsLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace ActiproSoftware.SampleBrowser.Logging;

/// <summary>
/// Defines extension methods to enable the use of Actipro logging with Microsoft logging.
/// </summary>
internal static class DebuggerLoggerExtensions {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Adds a configuration for <see cref="DebuggerLoggerProvider"/> to the <see cref="ILoggingBuilder"/>
	/// that will enable logging to use <see cref="DebuggerLoggerAdapter"/>.
	/// </summary>
	/// <param name="builder">The <see cref="ILoggingBuilder"/>.</param>
	/// <returns>The <see cref="ILoggingBuilder"/>.</returns>
	public static ILoggingBuilder AddDebugLogger(this ILoggingBuilder builder) {
		builder.Services.TryAddEnumerable(
			ServiceDescriptor.Singleton<ILoggerProvider, DebuggerLoggerProvider>());

		return builder;
	}

	/// <summary>
	/// Converts a Microsoft log level to the equivalent Actipro log level.
	/// </summary>
	/// <param name="logLevel">The Microsoft log level to convert.</param>
	public static ActiproLogLevel ToActiproLogLevel(this MSExtensionsLogLevel logLevel) {
		return logLevel switch {
			MSExtensionsLogLevel.Critical => ActiproLogLevel.Critical,
			MSExtensionsLogLevel.Debug=> ActiproLogLevel.Debug,
			MSExtensionsLogLevel.Error => ActiproLogLevel.Error,
			MSExtensionsLogLevel.Information => ActiproLogLevel.Information,
			MSExtensionsLogLevel.Trace => ActiproLogLevel.Trace,
			MSExtensionsLogLevel.Warning => ActiproLogLevel.Warning,
			MSExtensionsLogLevel.None => ActiproLogLevel.None,
			#if DEBUG
			_ => throw new NotImplementedException(nameof(logLevel))
			#else
			_ => ActiproLogLevel.None
			#endif
		};
	}

	/// <summary>
	/// Converts an Actipro log level to the equivalent Microsoft log level.
	/// </summary>
	/// <param name="logLevel">The Actipro log level to convert.</param>
	public static MSExtensionsLogLevel ToMicrosoftLogLevel(this ActiproLogLevel logLevel) {
		return logLevel switch {
			ActiproLogLevel.Critical => MSExtensionsLogLevel.Critical,
			ActiproLogLevel.Debug=> MSExtensionsLogLevel.Debug,
			ActiproLogLevel.Error => MSExtensionsLogLevel.Error,
			ActiproLogLevel.Information => MSExtensionsLogLevel.Information,
			ActiproLogLevel.Trace => MSExtensionsLogLevel.Trace,
			ActiproLogLevel.Warning => MSExtensionsLogLevel.Warning,
			ActiproLogLevel.None => MSExtensionsLogLevel.None,
			#if DEBUG
			_ => throw new NotImplementedException(nameof(logLevel))
			#else
			_ => MSExtensionsLogLevel.None
			#endif
		};
	}

}

#endif