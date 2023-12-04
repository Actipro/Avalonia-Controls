using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the ability to load images.
	/// </summary>
	public static class ImageLoader {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets an <see cref="IImage"/>.
		/// </summary>
		/// <param name="relPath">The path of the resource file relative to the <c>Images</c> folder.</param>
		/// <returns>An <see cref="IImage"/>.</returns>
		[UnconditionalSuppressMessage("Aot", "IL2026:Requires unreferenced code", Justification = "Only local resources are loaded with AvaloniaXamlLoader and this assembly, by design, should not be trimmed.")]
		private static IImage? LoadImageResource(string relPath) {
			if (relPath is null)
				throw new ArgumentNullException(nameof(relPath));

			var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
			string resourceUriText = $"avares://{assemblyName}/Images{relPath}";

			if (resourceUriText.EndsWith(".axaml", StringComparison.OrdinalIgnoreCase))
				return AvaloniaXamlLoader.Load(new Uri(resourceUriText)) as IImage;

			// Assume all other resources are image files
			return new Bitmap(AssetLoader.Open(new Uri(resourceUriText)));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets an <see cref="IImage"/> for an icon.
		/// </summary>
		/// <param name="fileName">The name of the file in the <c>/Images/Icons</c> folder.</param>
		/// <returns>An <see cref="IImage"/>.</returns>
		public static IImage? GetIcon(string fileName) => LoadImageResource("/Icons/" + fileName);

	}

}
