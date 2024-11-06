using System.Collections;
using System.Collections.Generic;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a collection of bar control view models.
	/// </summary>
	public class BarControlViewModelCollection : IReadOnlyCollection<IHasKey>, IEnumerable<IHasKey>, IEnumerable {

		private readonly Dictionary<string, Func<string, IHasKey>> _factories = new ();
		private readonly IBarImageProvider? _imageProvider;
		private readonly HashSet<string> _reentryKeys = new ();
		private readonly Dictionary<string, IHasKey> _viewModels = new ();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public BarControlViewModelCollection() { }

		/// <summary>
		/// Initializes a new instance of the class with the specified <see cref="IBarImageProvider"/>.
		/// </summary>
		/// <param name="imageProvider">The <see cref="IBarImageProvider"/> to use for assigning variant images as view models are created.</param>
		public BarControlViewModelCollection(IBarImageProvider? imageProvider) {
			_imageProvider = imageProvider;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns a view model for the specified string key, creating one as needed if it doesn't already exist.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <returns>The view model for the specified string key.</returns>
		private IHasKey GetOrCreate(string key) {
			if (_viewModels.TryGetValue(key, out var viewModel))
				return viewModel;

			try {
				// Detect re-entry of creating the same key while it is already in the process of being created (circular child dependency).
				if (_reentryKeys.Contains(key))
					throw new InvalidOperationException($"Re-entry detected attempting to create a view model of key '{key}' while it is already in the process of being created.");
				_reentryKeys.Add(key);

				viewModel = Create(key);
			}
			finally {
				_reentryKeys.Remove(key);
			}

			ValidateItem(viewModel);

			_viewModels.Add(key, viewModel);

			return viewModel;
		}

		/// <summary>
		/// Returns whether the specified string key has a registered bar control view model.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		private bool IsRegistered(string key)
			=> _factories.ContainsKey(key);

		/// <summary>
		/// the collection of registered keys.
		/// </summary>
		private IEnumerable<string> Keys {
			get {
				// Keys is intentionally implemented to first return the keys of items whose
				// view models have already been created, then it returns the keys of registered
				// view models that have yet-to-be created. Since accessing any item by key will
				// trigger the creation of that item if it does not already exists, this will
				// ensure that enumerators of this collection (which are based on keys) will
				// first return existing view models before creating new ones. If an enumerator
				// stops before iterating the entire collection, it will prevent the other
				// view models from being created unnecessarily.
				return _viewModels.Keys.Union(_factories.Keys);
			}
		}

		/// <summary>
		/// Validates that a view model's key is not empty.
		/// </summary>
		/// <param name="viewModel">The view model to examine.</param>
		private static void ValidateItem(IHasKey? viewModel) {
			if (string.IsNullOrEmpty(viewModel?.Key))
				throw new ArgumentException($"The view model's key must not be null or empty when creating a view model through {nameof(BarControlViewModelCollection)}.");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The number of bar control view models in the collection.
		/// </summary>
		public int Count => Keys.Count();

		/// <summary>
		/// Creates a bar control view model for the specified key.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <returns>
		/// The bar control view model that was created.
		/// </returns>
		protected virtual IHasKey Create(string key) {
			if (!_factories.TryGetValue(key, out var factory))
				throw new KeyNotFoundException();

			// Create the view model using the factory method
			var viewModel = factory.Invoke(key);

			// If no images have been defined, attempt to initialize from the image provider
			// using the same key as the control
			if (_imageProvider != null
				&& viewModel is IHasVariantImages imageViewModel
				&& imageViewModel.SmallIcon == null
				&& imageViewModel.MediumIcon == null
				&& imageViewModel.LargeIcon == null) {

				imageViewModel.SmallIcon = _imageProvider.GetImage(key, BarImageSize.Small);
				imageViewModel.MediumIcon = _imageProvider.GetImage(key, BarImageSize.Medium);
				imageViewModel.LargeIcon = _imageProvider.GetImage(key, BarImageSize.Large);
			}

			return viewModel;
		}

		/// <summary>
		/// Returns an enumerator for the bar control view models in the collection.
		/// </summary>
		public IEnumerator<IHasKey> GetEnumerator()
			=> Keys.Select(key => GetOrCreate(key)).GetEnumerator();

		/// <summary>
		/// Registers a bar control view model creation factory function for the specified key.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="factory">A function that creates a bar control view model with the specified key.</param>
		public void Register(string key, Func<string, IHasKey> factory) {
			if (string.IsNullOrEmpty(key))
				throw new ArgumentException("The key cannot be null or empty.", nameof(key));
			if (factory is null)
				throw new ArgumentNullException(nameof(factory));

			if (IsRegistered(key))
				throw new InvalidOperationException($"A factory with the key '{key}' has already been registered.");
			_factories[key] = factory;
		}

		/// <summary>
		/// Returns the bar control view model for the specified key.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		public IHasKey this[string key]
			=> GetOrCreate(key);

	}

}
