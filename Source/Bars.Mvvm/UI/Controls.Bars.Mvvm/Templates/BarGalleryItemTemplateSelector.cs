using ActiproSoftware.UI.Avalonia.Controls.Templates;
using ActiproSoftware.UI.Avalonia.Themes.Bars.Mvvm;
using Avalonia.Controls.Templates;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// An <see cref="IDataTemplate"/> implementation that selects an appropriate gallery item <see cref="IDataTemplate"/>.
	/// </summary>
	public class BarGalleryItemTemplateSelector : IDataTemplateSelector {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public BarGalleryItemTemplateSelector() {
			this.ColorMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemColorMenuItemDataTemplate);
			this.ColorTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemColorDataTemplate);
			this.DefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemDefaultDataTemplate);
			this.FontFamilyTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemFontFamilyDataTemplate);
			this.FontSizeTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemFontSizeDataTemplate);
			this.MenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemMenuItemDataTemplate);
			this.SizeSelectionTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemSizeSelectionDataTemplate);
			this.SymbolDataTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemSymbolDataTemplate);
			this.TextStyleTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemTextStyleDataTemplate);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		private static IDataTemplate? FindDataTemplateResource(string key)
			=> Application.Current?.FindResource(key) as IDataTemplate;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Returns whether the item should prefer menu item appearance, which is only when within a <see cref="BarMenuGallery"/>, 
		/// and either <see cref="BarMenuGallery.UseMenuItemAppearance"/> is set or the item requests it via <see cref="BarGalleryItem.LayoutBehavior"/>.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="container">The container control.</param>
		/// <returns>
		/// <c>true</c> if the item should prefer menu item appearance; otherwise, <c>false</c>.
		/// </returns>
		protected virtual bool PrefersMenuItemAppearance(object? item, Control? container) {
			var prefersMenuItemAppearance = (container is BarGalleryItem galleryItem)
				&& (
					galleryItem.UseMenuItemAppearance 
					|| ((item is IBarGalleryItemViewModel vm) && (vm.LayoutBehavior == BarGalleryItemLayoutBehavior.MenuItem))
				);

			return prefersMenuItemAppearance;
		}

		/// <summary>
		/// Selects an <see cref="IDataTemplate"/> for the specified item and container.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="container">The container control.</param>
		/// <returns>The <see cref="IDataTemplate"/> to use.</returns>
		public virtual IDataTemplate? SelectTemplate(object? item, Control? container) {
			return item switch {
				ColorBarGalleryItemViewModel => PrefersMenuItemAppearance(item, container) ? this.ColorMenuItemTemplate : this.ColorTemplate,
				FontFamilyBarGalleryItemViewModel => this.FontFamilyTemplate,
				FontSizeBarGalleryItemViewModel => this.FontSizeTemplate,
				Size => this.SizeSelectionTemplate,  // Assuming is for a BarSizeSelectionMenuGallery
				SymbolBarGalleryItemViewModel => this.SymbolDataTemplate,
				TextStyleBarGalleryItemViewModel => this.TextStyleTemplate,
				IBarGalleryItemViewModel => PrefersMenuItemAppearance(item, container) ? this.MenuItemTemplate : this.DefaultTemplate,
				_ => null
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC DATATEMPLATE PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="ColorBarGalleryItemViewModel"/> using a menu item appearance.
		/// </summary>
		public IDataTemplate? ColorMenuItemTemplate { get; set; }
		
		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="ColorBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? ColorTemplate { get; set; }
		
		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for an <see cref="IBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? DefaultTemplate { get; set; }

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="FontFamilyBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? FontFamilyTemplate { get; set; }

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="FontSizeBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? FontSizeTemplate { get; set; }

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for an <see cref="IBarGalleryItemViewModel"/> using a menu item appearance.
		/// </summary>
		public IDataTemplate? MenuItemTemplate { get; set; }

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="Size"/>.
		/// </summary>
		public IDataTemplate? SizeSelectionTemplate { get; set; }

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="SymbolBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? SymbolDataTemplate { get; set; }

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="TextStyleBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? TextStyleTemplate { get; set; }

	}

}
