using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Themes;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common;

/// <inheritdoc/>
/// <remarks>
/// The base class has been extended to define additional <see cref="IDataTemplate"/> and resource key properties
/// for common view models used by this sample.
/// </remarks>
public class CustomBarGalleryItemTemplateSelector : BarGalleryItemTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public CustomBarGalleryItemTemplateSelector() {
		EnsureResourcesLoaded();

		BulletTemplate = FindDataTemplateResource(BulletTemplateResourceKey);
		NumberingTemplate = FindDataTemplateResource(NumberingTemplateResourceKey);
		ShapeTemplate = FindDataTemplateResource(ShapeTemplateResourceKey);
		UnderlineTemplate = FindDataTemplateResource(UnderlineTemplateResourceKey);
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private static void EnsureResourcesLoaded() {
		// Make sure resources are loaded into application styles
		if (!CustomBarGalleryItemResources.TryGetCurrent(out _)) {
			if (ModernTheme.TryGetCurrent(out var modernTheme)) {
				var resources = new CustomBarGalleryItemResources();
				modernTheme.Add(resources);
			}
		}
	}

	private static IDataTemplate? FindDataTemplateResource(string key)
		=> Application.Current?.FindResource(key) as IDataTemplate;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc/>
	public override IDataTemplate? SelectTemplate(object? item, Control? container) {
		return item switch {
			BulletBarGalleryItemViewModel => BulletTemplate,
			NumberingBarGalleryItemViewModel => NumberingTemplate,
			ShapeBarGalleryItemViewModel => ShapeTemplate,
			UnderlineBarGalleryItemViewModel => UnderlineTemplate,
			_ => base.SelectTemplate(item, container)
		};
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC DATATEMPLATE PROPERTIES
	// --------------------------------------------------------------------------------------------------

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

	// --------------------------------------------------------------------------------------------------
	// PUBLIC RESOURCEKEY PROPERTIES
	// --------------------------------------------------------------------------------------------------

	public const string BulletTemplateResourceKey = nameof(BulletTemplateResourceKey);
	public const string NumberingTemplateResourceKey = nameof(NumberingTemplateResourceKey);
	public const string ShapeTemplateResourceKey = nameof(ShapeTemplateResourceKey);
	public const string UnderlineTemplateResourceKey = nameof(UnderlineTemplateResourceKey);

}
