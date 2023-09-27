#if MS_LOGGING

using ActiproSoftware.Logging;
using Microsoft.Extensions.Logging;
using System;
using IActiproLoggerFatcory = ActiproSoftware.Logging.ILoggerFactory;
using ActiproLoggerFatcory = ActiproSoftware.Logging.LoggerFactory;
using IMSExtensionsLoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;
using MSExtensionsLoggerFactory = Microsoft.Extensions.Logging.LoggerFactory;

namespace ActiproSoftware.SampleBrowser.Logging {

	/// <summary>
	/// Defines an adapter to allow Microsoft's ILoggerFactory to work with Actipro's <see cref="ILoggerFactory"/>.
	/// </summary>
	internal class LoggerFactoryAdapter : IActiproLoggerFatcory {

		private readonly IMSExtensionsLoggerFactory wrappedFactory;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="LoggerFactoryAdapter"/> class.
		/// </summary>
		/// <param name="wrappedFactory">The Microsoft-based ILoggerFactory to be wrapped by this class.</param>
		public LoggerFactoryAdapter(IMSExtensionsLoggerFactory wrappedFactory) {
			this.wrappedFactory = wrappedFactory ?? throw new ArgumentNullException(nameof(wrappedFactory));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IActiproLoggerFatcory.CreateLogger(Type)"/>
		Logger IActiproLoggerFatcory.CreateLogger(Type categoryType) {
			return CreateLogger(categoryType.FullName ?? categoryType.Name);
		}

		/// <inheritdoc cref="IActiproLoggerFatcory.CreateLogger{TCategory}"/>
		Logger IActiproLoggerFatcory.CreateLogger<TCategory>() {
			return CreateLogger(typeof(TCategory).FullName ?? typeof(TCategory).Name);
		}

		/// <summary>
		/// Disposes the object and the wrapped factory.
		/// </summary>
		void IDisposable.Dispose() {
			wrappedFactory?.Dispose();
			GC.SuppressFinalize(this);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Configures Actipro-based logging to use Microsoft extensions for logging.
		/// </summary>
		/// <param name="configure"></param>
		public static void Configure(Action<ILoggingBuilder> configure) {
			var wrappedFactory = MSExtensionsLoggerFactory.Create(configure);
			ActiproLoggerFatcory.DefaultInstance = new LoggerFactoryAdapter(wrappedFactory);
		}

		/// <inheritdoc cref="ILoggerFactory.CreateLogger(string)"/>
		public Logger CreateLogger(string categoryName) {
			return new LoggerAdapter(wrappedFactory.CreateLogger(categoryName));
		}

	}
}

#endif