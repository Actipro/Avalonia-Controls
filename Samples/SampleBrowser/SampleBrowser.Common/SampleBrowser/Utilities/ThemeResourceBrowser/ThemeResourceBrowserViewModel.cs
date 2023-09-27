using ActiproSoftware.Extensions;
using ActiproSoftware.UI.Avalonia.Themes;
using Avalonia;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActiproSoftware.SampleBrowser.Utilities.ThemeResourceBrowser {

	/// <summary>
	/// The resource browser view model.
	/// </summary>
	public class ThemeResourceBrowserViewModel : ObservableObjectBase {

		private CancellationTokenSource? _cancellationTokenSource;
		private readonly List<ThemeResourceViewModel> _currentThemeResources = new();
		private string _filterText = string.Empty;
		private ThemeVariant _theme;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class with the current application theme.
		/// </summary>
		public ThemeResourceBrowserViewModel()
			: this(Application.Current?.ActualThemeVariant ?? ThemeVariant.Light) { }

		/// <summary>
		/// Initializes a new instance of the class with the given theme.
		/// </summary>
		/// <param name="theme">The theme variant.</param>
		public ThemeResourceBrowserViewModel(ThemeVariant theme) {
			_theme = theme;
			RefreshFilteredResourcesAsync();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private async Task<List<ThemeResourceViewModel>> GetFilteredResourcesViewModels(CancellationToken token) {
			var filterText = FilterText;
			return await Task.Run(() => {
				var filteredResourceList = new List<ThemeResourceViewModel>();

				var filters = new HashSet<string[]>();
				var sections = (filterText ?? string.Empty).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
				foreach (var section in sections) {
					var parts = (section ?? string.Empty).Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
					filters.Add(parts);
				}

				filteredResourceList.AddRange(_currentThemeResources.Where(x => Filter(x.Name)));

				bool Filter(string key) {
					if (!filters.Any())
						return true;

					// At least one section must match all the parts to be included
					if (!string.IsNullOrEmpty(key)) {
						foreach (var parts in filters) {
							if (parts.All(part => key.Contains(part, System.StringComparison.InvariantCultureIgnoreCase)))
								return true;
						}
					}
					return false;
				}

				return filteredResourceList;

			}, token);
		}

		private static IEnumerable<KeyValuePair<string, string>> GetResourceNames() {
			foreach (var resourceKind in Enum.GetValues<ThemeResourceKind>())
				yield return new KeyValuePair<string, string>(resourceKind.ToString(), resourceKind.ToResourceKey());
		}

		private static IEnumerable<ThemeResourceViewModel> GetThemeResourceViewModels(ThemeVariant themeVariant) {
			foreach (var kvp in GetResourceNames().OrderBy(x => x.Key)) {
				if (TryCreateResourceViewModel(themeVariant, kvp.Key, kvp.Value, out var viewModel))
					yield return viewModel;
			}
		}

		private void RefreshFilteredResourcesAsync()
			=> RefreshFilteredResourcesAsync(TimeSpan.Zero, CancellationToken.None);

		private async void RefreshFilteredResourcesAsync(TimeSpan delay, CancellationToken token) {
			if (delay.TotalMilliseconds > 0) {
				try {
					await Task.Delay((int)delay.TotalMilliseconds.Round(RoundMode.Ceiling), token);
				}
				catch (TaskCanceledException) {
					return;
				}
			}

			// Build theme resources if not already built
			if (!_currentThemeResources.Any()) {
				_currentThemeResources.AddRange(GetThemeResourceViewModels(Theme));
				if (token.IsCancellationRequested)
					return;
			}

			var filteredResources = await GetFilteredResourcesViewModels(token);
			if (token.IsCancellationRequested)
				return;

			FilteredResources.Clear();
			foreach (var resource in filteredResources) {
				if (token.IsCancellationRequested)
					break;
				FilteredResources.Add(resource);
			}

		}

		private static bool TryCreateResourceViewModel(ThemeVariant themeVariant, string resourceName, string resourceKey, [NotNullWhen(returnValue: true)] out ThemeResourceViewModel? viewModel) {
			if (TryGetResource(resourceKey, themeVariant, out var resourceValue)) {
				viewModel = new ThemeResourceViewModel(resourceName, resourceValue);
				return true;
			}
			else {
				viewModel = null;
				return false;
			}
		}

		private static bool TryGetResource(string keyName, ThemeVariant theme, [NotNullWhen(returnValue: true)]out object? resource) {
			resource = null;
			return Application.Current?.TryGetResource(keyName, theme, out resource) == true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The collection of resources for the current theme with filtering applied.
		/// </summary>
		public ObservableCollection<ThemeResourceViewModel> FilteredResources { get; } = new();

		/// <summary>
		/// The filter to be applied.
		/// </summary>
		public string FilterText {
			get => _filterText;
			set {
				if (SetProperty(ref _filterText, value)) {
					var newCancellationTokenSource = new CancellationTokenSource();
					var oldCancellationTokenSource = Interlocked.Exchange(ref _cancellationTokenSource, newCancellationTokenSource);
					oldCancellationTokenSource?.Cancel();

					var token = newCancellationTokenSource.Token;

					var delay = (string.IsNullOrEmpty(_filterText)) ? TimeSpan.Zero : TimeSpan.FromMilliseconds(250);
					RefreshFilteredResourcesAsync(delay, token);
				}
			}
		}

		/// <summary>
		/// The current theme variant.
		/// </summary>
		public ThemeVariant Theme {
			get => _theme;
			set {
				if (SetProperty(ref _theme, value)) {
					_currentThemeResources.Clear();
					RefreshFilteredResourcesAsync();
				}
			}
		}

	}

}
