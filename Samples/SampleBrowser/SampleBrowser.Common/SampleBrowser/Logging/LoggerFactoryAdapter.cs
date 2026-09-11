#if MS_LOGGING

using ActiproSoftware.Logging;
using Microsoft.Extensions.Logging;
using IActiproLoggerFatcory = ActiproSoftware.Logging.ILoggerFactory;
using ActiproLoggerFatcory = ActiproSoftware.Logging.LoggerFactory;
using IMSExtensionsLoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;
using MSExtensionsLoggerFactory = Microsoft.Extensions.Logging.LoggerFactory;

namespace ActiproSoftware.SampleBrowser.Logging;

/// <summary>
/// Defines an adapter to allow Microsoft's ILoggerFactory to work with Actipro's <see cref="ILoggerFactory"/>.
/// </summary>
/// <param name="wrappedFactory">The Microsoft-based ILoggerFactory to be wrapped by this class.</param>
internal class LoggerFactoryAdapter(IMSExtensionsLoggerFactory wrappedFactory) : IActiproLoggerFatcory {

	private readonly IMSExtensionsLoggerFactory _wrappedFactory = wrappedFactory ?? throw new ArgumentNullException(nameof(wrappedFactory));

	// --------------------------------------------------------------------------------------------------
	// INTERFACE IMPLEMENTATION
	// --------------------------------------------------------------------------------------------------

	Logger IActiproLoggerFatcory.CreateLogger(Type categoryType)
		=> CreateLogger(categoryType.FullName ?? categoryType.Name);

	Logger IActiproLoggerFatcory.CreateLogger<TCategory>()
		=> CreateLogger(typeof(TCategory).FullName ?? typeof(TCategory).Name);

	void IDisposable.Dispose() {
		_wrappedFactory?.Dispose();
		GC.SuppressFinalize(this);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Configures Actipro-based logging to use Microsoft extensions for logging.
	/// </summary>
	/// <param name="configure"></param>
	public static void Configure(Action<ILoggingBuilder> configure) {
		var wrappedFactory = MSExtensionsLoggerFactory.Create(configure);
		ActiproLoggerFatcory.DefaultInstance = new LoggerFactoryAdapter(wrappedFactory);
	}

	/// <inheritdoc cref="ILoggerFactory.CreateLogger(string)"/>
	public Logger CreateLogger(string categoryName)
		=> new LoggerAdapter(_wrappedFactory.CreateLogger(categoryName));

}

#endif