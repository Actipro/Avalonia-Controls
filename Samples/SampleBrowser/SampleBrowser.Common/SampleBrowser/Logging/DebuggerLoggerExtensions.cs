#if MS_LOGGING

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using ActiproLogLevel = ActiproSoftware.Logging.LogLevel;
using MSExtensionsLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace ActiproSoftware.SampleBrowser.Logging {

	/// <summary>
	/// Defines extension methods to enable the use of Actipro logging with Microsoft logging.
	/// </summary>
	internal static class DebuggerLoggerExtensions {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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
		/// <returns>The equivalent Actipro log level.</returns>
		public static ActiproLogLevel ToActiproLogLevel(this MSExtensionsLogLevel logLevel) {
			switch (logLevel) {
				case MSExtensionsLogLevel.Critical:
					return ActiproLogLevel.Critical;
				case MSExtensionsLogLevel.Debug:
					return ActiproLogLevel.Debug;
				case MSExtensionsLogLevel.Error:
					return ActiproLogLevel.Error;
				case MSExtensionsLogLevel.Information:
					return ActiproLogLevel.Information;
				case MSExtensionsLogLevel.Trace:
					return ActiproLogLevel.Trace;
				case MSExtensionsLogLevel.Warning:
					return ActiproLogLevel.Warning;
				case MSExtensionsLogLevel.None:
					return ActiproLogLevel.None;
				default:
					#if DEBUG
					throw new NotImplementedException(nameof(logLevel));
					#else
					return ActiproLogLevel.None;
					#endif
			}
		}

		/// <summary>
		/// Converts an Actipro log level to the equivalent Microsoft log level.
		/// </summary>
		/// <param name="logLevel">The Actipro log level to convert.</param>
		/// <returns>The equivalent Microsoft log level.</returns>
		public static MSExtensionsLogLevel ToMicrosoftLogLevel(this ActiproLogLevel logLevel) {
			switch (logLevel) {
				case ActiproLogLevel.Critical:
					return MSExtensionsLogLevel.Critical;
				case ActiproLogLevel.Debug:
					return MSExtensionsLogLevel.Debug;
				case ActiproLogLevel.Error:
					return MSExtensionsLogLevel.Error;
				case ActiproLogLevel.Information:
					return MSExtensionsLogLevel.Information;
				case ActiproLogLevel.Trace:
					return MSExtensionsLogLevel.Trace;
				case ActiproLogLevel.Warning:
					return MSExtensionsLogLevel.Warning;
				case ActiproLogLevel.None:
					return MSExtensionsLogLevel.None;
				default:
					#if DEBUG
					throw new NotImplementedException(nameof(logLevel));
					#else
					return MSExtensionsLogLevel.None;
					#endif
			}
		}

	}
}

#endif