using ActiproSoftware.UI.Avalonia.Controls.Templates;
using ActiproSoftware.UI.Avalonia.Themes.Bars.Mvvm;
using Avalonia.Controls.Templates;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Provides an <see cref="IItemContainerTemplateSelector"/> that is used to select templates that create UI controls for various bar control view models,
	/// generally assigned to root bar controls, like to <see cref="Ribbon"/>'s <see cref="Ribbon.ItemContainerTemplateSelector"/> property.
	/// </summary>
	public class BarControlTemplateSelector : IItemContainerTemplateSelector {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public BarControlTemplateSelector() {
			this.BarButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarButtonDefaultItemContainerTemplate);
			this.BarButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarButtonMenuItemItemContainerTemplate);
			this.BarCheckBoxDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarCheckBoxDefaultItemContainerTemplate);
			this.BarCheckBoxMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarCheckBoxMenuItemItemContainerTemplate);
			this.BarComboBoxDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarComboBoxDefaultItemContainerTemplate);
			this.BarGalleryDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryDefaultItemContainerTemplate);
			this.BarGalleryMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryMenuItemItemContainerTemplate);
			this.BarGalleryOverflowMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryOverflowMenuItemItemContainerTemplate);
			this.BarGalleryItemDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarGalleryItemDefaultItemContainerTemplate);
			this.BarMenuHeadingMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarHeadingMenuItemItemContainerTemplate);
			this.BarPopupButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarPopupButtonDefaultItemContainerTemplate);
			this.BarPopupButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarPopupButtonMenuItemItemContainerTemplate);
			this.BarSizeSelectionGalleryMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSizeSelectionGalleryMenuItemItemContainerTemplate);
			this.BarSeparatorDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSeparatorDefaultItemContainerTemplate);
			this.BarSeparatorMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSeparatorMenuItemItemContainerTemplate);
			this.BarSplitButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSplitButtonDefaultItemContainerTemplate);
			this.BarSplitButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSplitButtonMenuItemItemContainerTemplate);
			this.BarSplitToggleButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSplitToggleButtonDefaultItemContainerTemplate);
			this.BarSplitToggleButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarSplitToggleButtonMenuItemItemContainerTemplate);
			this.BarTextBoxDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarTextBoxDefaultItemContainerTemplate);
			this.BarTextBoxMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarTextBoxMenuItemItemContainerTemplate);
			this.BarToggleButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarToggleButtonDefaultItemContainerTemplate);
			this.BarToggleButtonMenuItemTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.BarToggleButtonMenuItemItemContainerTemplate);
			this.RibbonApplicationButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonApplicationButtonDefaultItemContainerTemplate);
			this.RibbonBackstageDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonBackstageDefaultItemContainerTemplate);
			this.RibbonBackstageHeaderButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonBackstageHeaderButtonDefaultItemContainerTemplate);
			this.RibbonBackstageHeaderSeparatorDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate);
			this.RibbonBackstageTabDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonBackstageTabDefaultItemContainerTemplate);
			this.RibbonContextualTabGroupDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonContextualTabGroupDefaultItemContainerTemplate);
			this.RibbonControlGroupDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonControlGroupDefaultItemContainerTemplate);
			this.RibbonFooterDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonFooterDefaultItemContainerTemplate);
			this.RibbonGroupDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonGroupDefaultItemContainerTemplate);
			this.RibbonGroupLauncherButtonDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonGroupLauncherButtonDefaultItemContainerTemplate);
			this.RibbonMultiRowControlGroupDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonMultiRowControlGroupDefaultItemContainerTemplate);
			this.RibbonQuickAccessToolBarDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonQuickAccessToolBarDefaultItemContainerTemplate);
			this.RibbonTabDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonTabDefaultItemContainerTemplate);
			this.RibbonTabRowToolBarDefaultTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonTabRowToolBarDefaultItemContainerTemplate);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Loads an <see cref="IDataTemplate"/> with the specified key from application resources.
		/// </summary>
		/// <param name="key">The resource key.</param>
		/// <returns>The <see cref="IDataTemplate"/> that was found, if any.</returns>
		protected static IDataTemplate? FindDataTemplateResource(string key)
			=> Application.Current?.FindResource(key) as IDataTemplate;

		/// <inheritdoc cref="IItemContainerTemplateSelector.SelectTemplate"/>
		public virtual IDataTemplate? SelectTemplate(object? item, ItemsControl? parentItemsControl) {
			var isMenuItem = parentItemsControl is not null && BarControlService.GetIsMenuItemHost(parentItemsControl);

			switch (item) {

				// Derived view models must appear first in the switch

				case BarCheckBoxViewModel _:
					return isMenuItem ? this.BarCheckBoxMenuItemTemplate : this.BarCheckBoxDefaultTemplate;
				case BarComboBoxViewModel _: {
						var isOverflowMenuItem = parentItemsControl is not null && BarControlService.GetIsOverflowMenuItemHost(parentItemsControl);
						return isMenuItem
							? (isOverflowMenuItem ? this.BarGalleryOverflowMenuItemTemplate : this.BarGalleryMenuItemTemplate)
							: this.BarComboBoxDefaultTemplate;
					}
				case BarSplitToggleButtonViewModel _:
					return isMenuItem ? this.BarSplitToggleButtonMenuItemTemplate : this.BarSplitToggleButtonDefaultTemplate;
				case BarSplitButtonViewModel _:
					return isMenuItem ? this.BarSplitButtonMenuItemTemplate : this.BarSplitButtonDefaultTemplate;
				case BarToggleButtonViewModel _:
					return isMenuItem ? this.BarToggleButtonMenuItemTemplate : this.BarToggleButtonDefaultTemplate;

				// Core view models

				case BarButtonViewModel _:
					return isMenuItem ? this.BarButtonMenuItemTemplate : this.BarButtonDefaultTemplate;
				case IBarGalleryItemViewModel _:
					return this.BarGalleryItemDefaultTemplate;
				case BarGalleryViewModel _: {
						var isOverflowMenuItem = parentItemsControl is not null && BarControlService.GetIsOverflowMenuItemHost(parentItemsControl);
						return isMenuItem
							? (isOverflowMenuItem ? this.BarGalleryOverflowMenuItemTemplate : this.BarGalleryMenuItemTemplate)
							: this.BarGalleryDefaultTemplate;
					}
				case BarHeadingViewModel _:
					return isMenuItem ? this.BarMenuHeadingMenuItemTemplate : DefaultTemplate;
				case BarSizeSelectionMenuGalleryViewModel _:
					return this.BarSizeSelectionGalleryMenuItemTemplate;
				case BarPopupButtonViewModel _:
					return isMenuItem ? this.BarPopupButtonMenuItemTemplate : this.BarPopupButtonDefaultTemplate;
				case BarSeparatorViewModel _:
					return isMenuItem ? this.BarSeparatorMenuItemTemplate : this.BarSeparatorDefaultTemplate;
				case BarTextBoxViewModel _:
					return isMenuItem ? this.BarTextBoxMenuItemTemplate : this.BarTextBoxDefaultTemplate;
				case RibbonApplicationButtonViewModel _:
					return this.RibbonApplicationButtonDefaultTemplate;
				case RibbonBackstageViewModel _:
					return this.RibbonBackstageDefaultTemplate;
				case RibbonBackstageHeaderButtonViewModel _:
					return this.RibbonBackstageHeaderButtonDefaultTemplate;
				case RibbonBackstageHeaderSeparatorViewModel _:
					return this.RibbonBackstageHeaderSeparatorDefaultTemplate;
				case RibbonBackstageTabViewModel _:
					return this.RibbonBackstageTabDefaultTemplate;
				case RibbonContextualTabGroupViewModel _:
					return this.RibbonContextualTabGroupDefaultTemplate;
				case RibbonControlGroupViewModel _:
					return this.RibbonControlGroupDefaultTemplate;
				case RibbonFooterViewModel _:
					return this.RibbonFooterDefaultTemplate;
				case RibbonGroupViewModel _:
					return this.RibbonGroupDefaultTemplate;
				case RibbonGroupLauncherButtonViewModel _:
					return this.RibbonGroupLauncherButtonDefaultTemplate;
				case RibbonMultiRowControlGroupViewModel _:
					return this.RibbonMultiRowControlGroupDefaultTemplate;
				case RibbonQuickAccessToolBarViewModel _:
					return this.RibbonQuickAccessToolBarDefaultTemplate;
				case RibbonTabRowToolBarViewModel _:
					return this.RibbonTabRowToolBarDefaultTemplate;
				case RibbonTabViewModel _:
					return this.RibbonTabDefaultTemplate;
			}

			return DefaultTemplate;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC DATATEMPLATE PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

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

}
