using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Themes;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {
	
	/// <inheritdoc/>
	/// <remarks>
	/// The base class has been extended to define additional <see cref="IDataTemplate"/> and resource key properties
	/// for common view models used by this sample.
	/// </remarks>
	public class CustomBarGalleryItemTemplateSelector : BarGalleryItemTemplateSelector {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomBarGalleryItemTemplateSelector"/> class.
		/// </summary>
		public CustomBarGalleryItemTemplateSelector() {
			EnsureResourcesLoaded();

			this.BulletTemplate = FindDataTemplateResource(BulletTemplateResourceKey);
			this.NumberingTemplate = FindDataTemplateResource(NumberingTemplateResourceKey);
			this.ShapeTemplate = FindDataTemplateResource(ShapeTemplateResourceKey);
			this.UnderlineTemplate = FindDataTemplateResource(UnderlineTemplateResourceKey);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		private void EnsureResourcesLoaded() {
			if (!CustomBarGalleryItemResources.TryGetCurrent(out _)) {
				// Make sure resources are loaded into application styles
				var resources = new CustomBarGalleryItemResources();
				if (ModernTheme.TryGetCurrent(out var modernTheme))
					modernTheme.Add(resources);
			}
		}
		
		private static IDataTemplate? FindDataTemplateResource(string key)
			=> Application.Current?.FindResource(key) as IDataTemplate;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc/>
		public override IDataTemplate? SelectTemplate(object? item, Control? container) {
			switch (item) {
				case BulletBarGalleryItemViewModel _:
					return this.BulletTemplate;
				case NumberingBarGalleryItemViewModel _:
					return this.NumberingTemplate;
				case ShapeBarGalleryItemViewModel _:
					return this.ShapeTemplate;
				case UnderlineBarGalleryItemViewModel _:
					return this.UnderlineTemplate;
			}

			return base.SelectTemplate(item, container);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC DATATEMPLATE PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="BulletBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? BulletTemplate { get; set; }

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="NumberingBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? NumberingTemplate { get; set; }

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="ShapeBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? ShapeTemplate { get; set; }

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="UnderlineBarGalleryItemViewModel"/>.
		/// </summary>
		public IDataTemplate? UnderlineTemplate { get; set; }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC RESOURCEKEY PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public const string BulletTemplateResourceKey = nameof(BulletTemplateResourceKey);
		public const string NumberingTemplateResourceKey = nameof(NumberingTemplateResourceKey);
		public const string ShapeTemplateResourceKey = nameof(ShapeTemplateResourceKey);
		public const string UnderlineTemplateResourceKey = nameof(UnderlineTemplateResourceKey);

	}

}
