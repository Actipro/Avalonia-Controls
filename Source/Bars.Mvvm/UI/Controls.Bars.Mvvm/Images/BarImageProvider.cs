using System.Collections.Generic;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a service that can provide images for bar controls.
	/// </summary>
	public class BarImageProvider : IBarImageProvider {

		private readonly Dictionary<string, Func<BarImageOptions, object?>> _factories = new();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns whether there is a registration for the specified key.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <returns>
		/// <c>true</c> if there is a registration for the specified key; otherwise, <c>false</c>.
		/// </returns>
		private bool IsRegistered(string key)
			=> _factories.ContainsKey(key);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IBarImageProvider.GetImage(string, BarImageSize)"/>
		public object? GetImage(string? key, BarImageSize size)
			=> GetImage(key, new BarImageOptions(size));

		/// <inheritdoc cref="IBarImageProvider.GetImage(string, BarImageOptions)"/>
		public object? GetImage(string? key, BarImageOptions options) {
			if ((key is not null) && (_factories.TryGetValue(key, out var factory)))
				return factory.Invoke(options);

			return null;
		}

		/// <summary>
		/// Registers an image factory function for a key.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <param name="factory">A factory method that examines <see cref="BarImageOptions"/> to return a value representing an image.</param>
		public void Register(string key, Func<BarImageOptions, object?> factory) {
			if (string.IsNullOrEmpty(key))
				throw new ArgumentException("The key cannot be null or empty.", nameof(key));
			if (factory is null)
				throw new ArgumentNullException(nameof(factory));

			if (IsRegistered(key))
				throw new InvalidOperationException($"A factory with the key '{key}' has already been registered.");
			_factories[key] = factory;
		}

		/// <summary>
		/// Unregisters an image factory function for a key.
		/// </summary>
		/// <param name="key">A string that uniquely identifies a bar control.</param>
		/// <returns>
		/// <c>true</c> if an image factory function was removed; otherwise, <c>false</c>.
		/// </returns>
		public bool Unregister(string key)
			=> _factories.Remove(key);

	}

}
