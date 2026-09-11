using ActiproSoftware.UI.Avalonia.Controls.Templates;
using ActiproSoftware.UI.Avalonia.Themes.Bars.Mvvm;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;

/// <summary>
/// Provides an <see cref="IItemContainerTemplateSelector"/> that is used to select templates that create UI controls for various bar control view models,
/// generally assigned to root bar controls, like to <see cref="Ribbon"/>'s <see cref="Ribbon.ItemContainerTemplateSelector"/> property.
/// </summary>
public class BarControlTemplateSelector : IItemContainerTemplateSelector {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Initializes an instance of the class.
	/// </summary>
	public BarControlTemplateSelector() {
		BarButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarButtonDefaultItemContainerTemplate);
		BarButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarButtonMenuItemItemContainerTemplate);
		BarCheckBoxDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarCheckBoxDefaultItemContainerTemplate);
		BarCheckBoxMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarCheckBoxMenuItemItemContainerTemplate);
		BarComboBoxDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarComboBoxDefaultItemContainerTemplate);
		BarGalleryDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryDefaultItemContainerTemplate);
		BarGalleryMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryMenuItemItemContainerTemplate);
		BarGalleryOverflowMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryOverflowMenuItemItemContainerTemplate);
		BarGalleryItemDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemDefaultItemContainerTemplate);
		BarMenuHeadingMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarHeadingMenuItemItemContainerTemplate);
		BarPopupButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarPopupButtonDefaultItemContainerTemplate);
		BarPopupButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarPopupButtonMenuItemItemContainerTemplate);
		BarSizeSelectionGalleryMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSizeSelectionGalleryMenuItemItemContainerTemplate);
		BarSeparatorDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSeparatorDefaultItemContainerTemplate);
		BarSeparatorMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSeparatorMenuItemItemContainerTemplate);
		BarSplitButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSplitButtonDefaultItemContainerTemplate);
		BarSplitButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSplitButtonMenuItemItemContainerTemplate);
		BarSplitToggleButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSplitToggleButtonDefaultItemContainerTemplate);
		BarSplitToggleButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSplitToggleButtonMenuItemItemContainerTemplate);
		BarTextBoxDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarTextBoxDefaultItemContainerTemplate);
		BarTextBoxMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarTextBoxMenuItemItemContainerTemplate);
		BarToggleButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarToggleButtonDefaultItemContainerTemplate);
		BarToggleButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarToggleButtonMenuItemItemContainerTemplate);
		RibbonApplicationButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonApplicationButtonDefaultItemContainerTemplate);
		RibbonBackstageDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonBackstageDefaultItemContainerTemplate);
		RibbonBackstageHeaderButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonBackstageHeaderButtonDefaultItemContainerTemplate);
		RibbonBackstageHeaderSeparatorDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate);
		RibbonBackstageTabDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonBackstageTabDefaultItemContainerTemplate);
		RibbonContextualTabGroupDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonContextualTabGroupDefaultItemContainerTemplate);
		RibbonControlGroupDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonControlGroupDefaultItemContainerTemplate);
		RibbonFooterDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonFooterDefaultItemContainerTemplate);
		RibbonGroupDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonGroupDefaultItemContainerTemplate);
		RibbonGroupLauncherButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonGroupLauncherButtonDefaultItemContainerTemplate);
		RibbonMultiRowControlGroupDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonMultiRowControlGroupDefaultItemContainerTemplate);
		RibbonQuickAccessToolBarDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonQuickAccessToolBarDefaultItemContainerTemplate);
		RibbonTabDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonTabDefaultItemContainerTemplate);
		RibbonTabRowToolBarDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonTabRowToolBarDefaultItemContainerTemplate);
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Loads an <see cref="IDataTemplate"/> with the specified key from application resources.
	/// </summary>
	/// <param name="key">The resource key.</param>
	/// <returns>The <see cref="IDataTemplate"/> that was found, if any.</returns>
	protected static IDataTemplate? FindDataTemplateResource(string key)
		=> Application.Current?.FindResource(key) as IDataTemplate;

	/// <inheritdoc cref="IItemContainerTemplateSelector.SelectTemplate"/>
	public virtual IDataTemplate? SelectTemplate(object? item, ItemsControl? parentItemsControl) {
		var isMenuItem = (parentItemsControl is not null)
			&& BarControlService.GetIsMenuItemHost(parentItemsControl);

		switch (item) {

			// Derived view models must appear first in the switch

			case BarCheckBoxViewModel:
				return isMenuItem ? BarCheckBoxMenuItemTemplate : BarCheckBoxDefaultTemplate;
			case BarComboBoxViewModel: {
				var isOverflowMenuItem = (parentItemsControl is not null)
					&& BarControlService.GetIsOverflowMenuItemHost(parentItemsControl);
				return isMenuItem
					? (isOverflowMenuItem ? BarGalleryOverflowMenuItemTemplate : BarGalleryMenuItemTemplate)
					: BarComboBoxDefaultTemplate;
			}
			case BarSplitToggleButtonViewModel:
				return isMenuItem ? BarSplitToggleButtonMenuItemTemplate : BarSplitToggleButtonDefaultTemplate;
			case BarSplitButtonViewModel:
				return isMenuItem ? BarSplitButtonMenuItemTemplate : BarSplitButtonDefaultTemplate;
			case BarToggleButtonViewModel:
				return isMenuItem ? BarToggleButtonMenuItemTemplate : BarToggleButtonDefaultTemplate;

			// Core view models

			case BarButtonViewModel:
				return isMenuItem ? BarButtonMenuItemTemplate : BarButtonDefaultTemplate;
			case IBarGalleryItemViewModel:
				return BarGalleryItemDefaultTemplate;
			case BarGalleryViewModel: {
				var isOverflowMenuItem = (parentItemsControl is not null)
					&& BarControlService.GetIsOverflowMenuItemHost(parentItemsControl);
				return isMenuItem
					? (isOverflowMenuItem ? BarGalleryOverflowMenuItemTemplate : BarGalleryMenuItemTemplate)
					: BarGalleryDefaultTemplate;
			}
			case BarHeadingViewModel:
				return isMenuItem ? BarMenuHeadingMenuItemTemplate : DefaultTemplate;
			case BarSizeSelectionMenuGalleryViewModel:
				return BarSizeSelectionGalleryMenuItemTemplate;
			case BarPopupButtonViewModel:
				return isMenuItem ? BarPopupButtonMenuItemTemplate : BarPopupButtonDefaultTemplate;
			case BarSeparatorViewModel:
				return isMenuItem ? BarSeparatorMenuItemTemplate : BarSeparatorDefaultTemplate;
			case BarTextBoxViewModel:
				return isMenuItem ? BarTextBoxMenuItemTemplate : BarTextBoxDefaultTemplate;
			case RibbonApplicationButtonViewModel:
				return RibbonApplicationButtonDefaultTemplate;
			case RibbonBackstageViewModel:
				return RibbonBackstageDefaultTemplate;
			case RibbonBackstageHeaderButtonViewModel:
				return RibbonBackstageHeaderButtonDefaultTemplate;
			case RibbonBackstageHeaderSeparatorViewModel:
				return RibbonBackstageHeaderSeparatorDefaultTemplate;
			case RibbonBackstageTabViewModel:
				return RibbonBackstageTabDefaultTemplate;
			case RibbonContextualTabGroupViewModel:
				return RibbonContextualTabGroupDefaultTemplate;
			case RibbonControlGroupViewModel:
				return RibbonControlGroupDefaultTemplate;
			case RibbonFooterViewModel:
				return RibbonFooterDefaultTemplate;
			case RibbonGroupViewModel:
				return RibbonGroupDefaultTemplate;
			case RibbonGroupLauncherButtonViewModel:
				return RibbonGroupLauncherButtonDefaultTemplate;
			case RibbonMultiRowControlGroupViewModel:
				return RibbonMultiRowControlGroupDefaultTemplate;
			case RibbonQuickAccessToolBarViewModel:
				return RibbonQuickAccessToolBarDefaultTemplate;
			case RibbonTabRowToolBarViewModel:
				return RibbonTabRowToolBarDefaultTemplate;
			case RibbonTabViewModel:
				return RibbonTabDefaultTemplate;
		}

		return DefaultTemplate;
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC DATATEMPLATE PROPERTIES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarButtonViewModel"/>.
	/// </summary>
	public IDataTemplate? BarButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarButtonViewModel"/> used in a menu item context.
	/// </summary>
	public IDataTemplate? BarButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarCheckBoxViewModel"/>.
	/// </summary>
	public IDataTemplate? BarCheckBoxDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarCheckBoxViewModel"/> used in a menu item context.
	/// </summary>
	public IDataTemplate? BarCheckBoxMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarComboBoxViewModel"/>.
	/// </summary>
	public IDataTemplate? BarComboBoxDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarGalleryViewModel"/>.
	/// </summary>
	public IDataTemplate? BarGalleryDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="IBarGalleryItemViewModel"/>.
	/// </summary>
	public IDataTemplate? BarGalleryItemDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarGalleryViewModel"/> used in a menu context.
	/// </summary>
	public IDataTemplate? BarGalleryMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarGalleryViewModel"/> used in a menu context.
	/// </summary>
	public IDataTemplate? BarGalleryOverflowMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarHeadingViewModel"/> used in a menu context.
	/// </summary>
	public IDataTemplate? BarMenuHeadingMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarPopupButtonViewModel"/>.
	/// </summary>
	public IDataTemplate? BarPopupButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarPopupButtonViewModel"/> used in a menu item context.
	/// </summary>
	public IDataTemplate? BarPopupButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarSizeSelectionMenuGalleryViewModel"/> used in a menu item context.
	/// </summary>
	public IDataTemplate? BarSizeSelectionGalleryMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarSeparatorViewModel"/>.
	/// </summary>
	public IDataTemplate? BarSeparatorDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarSeparatorViewModel"/> used in a menu item context.
	/// </summary>
	public IDataTemplate? BarSeparatorMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarSplitButtonViewModel"/>.
	/// </summary>
	public IDataTemplate? BarSplitButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarSplitButtonViewModel"/> used in a menu item context.
	/// </summary>
	public IDataTemplate? BarSplitButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarSplitToggleButtonViewModel"/>.
	/// </summary>
	public IDataTemplate? BarSplitToggleButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarSplitToggleButtonViewModel"/> used in a menu item context.
	/// </summary>
	public IDataTemplate? BarSplitToggleButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarTextBoxViewModel"/>.
	/// </summary>
	public IDataTemplate? BarTextBoxDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarTextBoxViewModel"/>.
	/// </summary>
	public IDataTemplate? BarTextBoxMenuItemTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarToggleButtonViewModel"/>.
	/// </summary>
	public IDataTemplate? BarToggleButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="BarToggleButtonViewModel"/> used in a menu item context.
	/// </summary>
	public IDataTemplate? BarToggleButtonMenuItemTemplate { get; set; }

	/// <summary>
	/// The default <see cref="IDataTemplate"/> that will be used if there is no <see cref="Type"/>-based match.
	/// </summary>
	public IDataTemplate? DefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonApplicationButtonViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonApplicationButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonBackstageViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonBackstageDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonBackstageHeaderButtonViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonBackstageHeaderButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonBackstageHeaderSeparatorViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonBackstageHeaderSeparatorDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonBackstageTabViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonBackstageTabDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonContextualTabGroupViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonContextualTabGroupDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonControlGroupViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonControlGroupDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonFooterViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonFooterDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonGroupLauncherButtonViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonGroupLauncherButtonDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonGroupViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonGroupDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonMultiRowControlGroupViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonMultiRowControlGroupDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonQuickAccessToolBarViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonQuickAccessToolBarDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonTabViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonTabDefaultTemplate { get; set; }

	/// <summary>
	/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonTabRowToolBarViewModel"/>.
	/// </summary>
	public IDataTemplate? RibbonTabRowToolBarDefaultTemplate { get; set; }

}
