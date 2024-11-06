using Avalonia.Markup.Xaml;
using Avalonia.Styling;

namespace ActiproSoftware.UI.Avalonia.Themes.Bars.Mvvm {

	/// <summary>
	/// Includes all of the resources for Actipro's Bars MVVM view models.
	/// </summary>
	public partial class BarsMvvmResources : Styles {

		private static bool _areResourcesLoaded = false;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="serviceProvider">The parent's service provider.</param>
		public BarsMvvmResources(IServiceProvider? serviceProvider = null) {
			// Load the XAML
			AvaloniaXamlLoader.Load(serviceProvider, this);
		}

		/// <summary>
		/// Ensures an instance of <see cref="BarsMvvmResources"/> is loaded and accessible through <see cref="ModernTheme"/>.
		/// </summary>
		/// <returns>
		/// <c>true</c> if resources were already loaded or newly loaded; otherwise,
		/// <c>false</c> if resources could not be loaded (i.e., <see cref="ModernTheme"/> is unavailable).
		/// </returns>
		public static bool EnsureResourcesLoaded() {
			if (_areResourcesLoaded)
				return true;

			// Make sure MVVM resources are loaded into application styles via ModernTheme
			if (ModernTheme.TryGetCurrent(out var modernTheme)) {
				_areResourcesLoaded = true;
				var resources = new BarsMvvmResources();
				modernTheme.Add(resources);
				return true;
			}

			return false;
		}

	}

}
