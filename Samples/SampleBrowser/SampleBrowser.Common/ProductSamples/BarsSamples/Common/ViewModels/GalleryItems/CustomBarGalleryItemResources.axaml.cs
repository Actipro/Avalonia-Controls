using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Includes all of the resources related to the custom <see cref="BarGalleryItem"/> resources objects defined in this assembly.
	/// </summary>
	public partial class CustomBarGalleryItemResources : Styles {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="serviceProvider">The parent's service provider.</param>
		public CustomBarGalleryItemResources(IServiceProvider? serviceProvider = null) {
			// Load the XAML
			AvaloniaXamlLoader.Load(serviceProvider, this);
		}

		/// <summary>
		/// Tries to locate the <see cref="CustomBarGalleryItemResources"/> instance within the <see cref="Application.Styles"/> collection.
		/// </summary>
		/// <param name="resources">Returns the <see cref="CustomBarGalleryItemResources"/> instance that was found, if any.</param>
		/// <returns>
		/// <c>true</c> if a <see cref="CustomBarGalleryItemResources"/> instance was found; otherwise, <c>false</c>.
		/// </returns>
		public static bool TryGetCurrent([NotNullWhen(returnValue: true)] out CustomBarGalleryItemResources? resources) {
			resources = Application.Current?.Styles.OfType<CustomBarGalleryItemResources>().FirstOrDefault();

			return resources is not null;
		}

	}

}
