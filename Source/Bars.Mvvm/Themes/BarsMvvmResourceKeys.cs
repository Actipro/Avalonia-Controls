namespace ActiproSoftware.UI.Avalonia.Themes.Bars.Mvvm {

	/// <summary>
	/// Provides access to the keys that identify all reusable resources included in this assembly.
	/// </summary>
	public static class BarsMvvmResourceKeys {

		static BarsMvvmResourceKeys() {
			// Ensure the resources referenced by the keys in this class are loaded
			// NOTE: Keys must be defined as 'static readonly' instead of 'const' for the static constructor to be called
			BarsMvvmResources.EnsureResourcesLoaded();
		}

		// Control themes

		/// <summary>An identifier for the <c>BarGalleryItem</c> control theme.</summary>
		public static readonly string BarGalleryItemControlTheme = nameof(BarGalleryItemControlTheme);
		/// <summary>An identifier for the <c>Ribbon</c> control theme.</summary>
		public static readonly string RibbonControlTheme = nameof(RibbonControlTheme);
		/// <summary>An identifier for the <c>StandaloneToolBar</c> control theme.</summary>
		public static readonly string StandaloneToolBarControlTheme = nameof(StandaloneToolBarControlTheme);

		// Item data templates

		/// <summary>An identifier for a button container template in default context.</summary>
		public static readonly string BarButtonDefaultItemContainerTemplate = nameof(BarButtonDefaultItemContainerTemplate);
		/// <summary>An identifier for a button container template in menu item context.</summary>
		public static readonly string BarButtonMenuItemItemContainerTemplate = nameof(BarButtonMenuItemItemContainerTemplate);
		/// <summary>An identifier for a checkbox container template in default context.</summary>
		public static readonly string BarCheckBoxDefaultItemContainerTemplate = nameof(BarCheckBoxDefaultItemContainerTemplate);
		/// <summary>An identifier for a checkbox container template in menu item context.</summary>
		public static readonly string BarCheckBoxMenuItemItemContainerTemplate = nameof(BarCheckBoxMenuItemItemContainerTemplate);
		/// <summary>An identifier for a combobox container template in default context.</summary>
		public static readonly string BarComboBoxDefaultItemContainerTemplate = nameof(BarComboBoxDefaultItemContainerTemplate);
		/// <summary>An identifier for a combobox container template in menu item context.</summary>
		public static readonly string BarComboBoxMenuItemItemContainerTemplate = nameof(BarComboBoxMenuItemItemContainerTemplate);
		/// <summary>An identifier for a gallery container template in default context.</summary>
		public static readonly string BarGalleryDefaultItemContainerTemplate = nameof(BarGalleryDefaultItemContainerTemplate);
		/// <summary>An identifier for a gallery item container template in default context.</summary>
		public static readonly string BarGalleryItemDefaultItemContainerTemplate = nameof(BarGalleryItemDefaultItemContainerTemplate);
		/// <summary>An identifier for a gallery container template in menu item context.</summary>
		public static readonly string BarGalleryMenuItemItemContainerTemplate = nameof(BarGalleryMenuItemItemContainerTemplate);
		/// <summary>An identifier for a gallery overflow container template in menu item context.</summary>
		public static readonly string BarGalleryOverflowMenuItemItemContainerTemplate = nameof(BarGalleryOverflowMenuItemItemContainerTemplate);
		/// <summary>An identifier for a heading container template in menu item context.</summary>
		public static readonly string BarHeadingMenuItemItemContainerTemplate = nameof(BarHeadingMenuItemItemContainerTemplate);
		/// <summary>An identifier for a popup button container template in default context.</summary>
		public static readonly string BarPopupButtonDefaultItemContainerTemplate = nameof(BarPopupButtonDefaultItemContainerTemplate);
		/// <summary>An identifier for a popup button container template in menu item context.</summary>
		public static readonly string BarPopupButtonMenuItemItemContainerTemplate = nameof(BarPopupButtonMenuItemItemContainerTemplate);
		/// <summary>An identifier for a separator container template in default context.</summary>
		public static readonly string BarSeparatorDefaultItemContainerTemplate = nameof(BarSeparatorDefaultItemContainerTemplate);
		/// <summary>An identifier for a separator container template in menu item context.</summary>
		public static readonly string BarSeparatorMenuItemItemContainerTemplate = nameof(BarSeparatorMenuItemItemContainerTemplate);
		/// <summary>An identifier for a size selection gallery container template in menu item context.</summary>
		public static readonly string BarSizeSelectionGalleryMenuItemItemContainerTemplate = nameof(BarSizeSelectionGalleryMenuItemItemContainerTemplate);
		/// <summary>An identifier for a split button container template in default context.</summary>
		public static readonly string BarSplitButtonDefaultItemContainerTemplate = nameof(BarSplitButtonDefaultItemContainerTemplate);
		/// <summary>An identifier for a split button container template in menu item context.</summary>
		public static readonly string BarSplitButtonMenuItemItemContainerTemplate = nameof(BarSplitButtonMenuItemItemContainerTemplate);
		/// <summary>An identifier for a split toggle button container template in default context.</summary>
		public static readonly string BarSplitToggleButtonDefaultItemContainerTemplate = nameof(BarSplitToggleButtonDefaultItemContainerTemplate);
		/// <summary>An identifier for a split toggle button container template in menu item context.</summary>
		public static readonly string BarSplitToggleButtonMenuItemItemContainerTemplate = nameof(BarSplitToggleButtonMenuItemItemContainerTemplate);
		/// <summary>An identifier for a textbox container template in default context.</summary>
		public static readonly string BarTextBoxDefaultItemContainerTemplate = nameof(BarTextBoxDefaultItemContainerTemplate);
		/// <summary>An identifier for a textbox container template in menu item context.</summary>
		public static readonly string BarTextBoxMenuItemItemContainerTemplate = nameof(BarTextBoxMenuItemItemContainerTemplate);
		/// <summary>An identifier for a toggle button container template in default context.</summary>
		public static readonly string BarToggleButtonDefaultItemContainerTemplate = nameof(BarToggleButtonDefaultItemContainerTemplate);
		/// <summary>An identifier for a toggle button container template in menu item context.</summary>
		public static readonly string BarToggleButtonMenuItemItemContainerTemplate = nameof(BarToggleButtonMenuItemItemContainerTemplate);
		/// <summary>An identifier for a ribbon application button container template in default context.</summary>
		public static readonly string RibbonApplicationButtonDefaultItemContainerTemplate = nameof(RibbonApplicationButtonDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon backstage container template in default context.</summary>
		public static readonly string RibbonBackstageDefaultItemContainerTemplate = nameof(RibbonBackstageDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon backstage header button container template in default context.</summary>
		public static readonly string RibbonBackstageHeaderButtonDefaultItemContainerTemplate = nameof(RibbonBackstageHeaderButtonDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon backstage header separator container template in default context.</summary>
		public static readonly string RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate = nameof(RibbonBackstageHeaderSeparatorDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon backstage tab container template in default context.</summary>
		public static readonly string RibbonBackstageTabDefaultItemContainerTemplate = nameof(RibbonBackstageTabDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon contextual tab group container template in default context.</summary>
		public static readonly string RibbonContextualTabGroupDefaultItemContainerTemplate = nameof(RibbonContextualTabGroupDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon control group container template in default context.</summary>
		public static readonly string RibbonControlGroupDefaultItemContainerTemplate = nameof(RibbonControlGroupDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon footer container template in default context.</summary>
		public static readonly string RibbonFooterDefaultItemContainerTemplate = nameof(RibbonFooterDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon group container template in default context.</summary>
		public static readonly string RibbonGroupDefaultItemContainerTemplate = nameof(RibbonGroupDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon group launcher button container template in default context.</summary>
		public static readonly string RibbonGroupLauncherButtonDefaultItemContainerTemplate = nameof(RibbonGroupLauncherButtonDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon multi-row control group container template in default context.</summary>
		public static readonly string RibbonMultiRowControlGroupDefaultItemContainerTemplate = nameof(RibbonMultiRowControlGroupDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon quick access toolbar container template in default context.</summary>
		public static readonly string RibbonQuickAccessToolBarDefaultItemContainerTemplate = nameof(RibbonQuickAccessToolBarDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon tab container template in default context.</summary>
		public static readonly string RibbonTabDefaultItemContainerTemplate = nameof(RibbonTabDefaultItemContainerTemplate);
		/// <summary>An identifier for a ribbon tab row toolbar container template in default context.</summary>
		public static readonly string RibbonTabRowToolBarDefaultItemContainerTemplate = nameof(RibbonTabRowToolBarDefaultItemContainerTemplate);

		// Gallery item data templates

		/// <summary>An identifier for a color gallery item data template.</summary>
		public static readonly string BarGalleryItemColorDataTemplate = nameof(BarGalleryItemColorDataTemplate);
		/// <summary>An identifier for a color menu item gallery item data template.</summary>
		public static readonly string BarGalleryItemColorMenuItemDataTemplate = nameof(BarGalleryItemColorMenuItemDataTemplate);
		/// <summary>An identifier for a default gallery item data template.</summary>
		public static readonly string BarGalleryItemDefaultDataTemplate = nameof(BarGalleryItemDefaultDataTemplate);
		/// <summary>An identifier for a font family gallery item data template.</summary>
		public static readonly string BarGalleryItemFontFamilyDataTemplate = nameof(BarGalleryItemFontFamilyDataTemplate);
		/// <summary>An identifier for a font size gallery item data template.</summary>
		public static readonly string BarGalleryItemFontSizeDataTemplate = nameof(BarGalleryItemFontSizeDataTemplate);
		/// <summary>An identifier for a menu item gallery item data template.</summary>
		public static readonly string BarGalleryItemMenuItemDataTemplate = nameof(BarGalleryItemMenuItemDataTemplate);
		/// <summary>An identifier for a size selection gallery item data template.</summary>
		public static readonly string BarGalleryItemSizeSelectionDataTemplate = nameof(BarGalleryItemSizeSelectionDataTemplate);
		/// <summary>An identifier for a symbol gallery item data template.</summary>
		public static readonly string BarGalleryItemSymbolDataTemplate = nameof(BarGalleryItemSymbolDataTemplate);
		/// <summary>An identifier for a text style gallery item data template.</summary>
		public static readonly string BarGalleryItemTextStyleDataTemplate = nameof(BarGalleryItemTextStyleDataTemplate);

		// Ribbon footer content data templates

		/// <summary>An identifier for an info bar footer content data template.</summary>
		public static readonly string RibbonFooterContentInfoBarDataTemplate = nameof(RibbonFooterContentInfoBarDataTemplate);
		/// <summary>An identifier for a simple footer content data template.</summary>
		public static readonly string RibbonFooterContentSimpleDataTemplate = nameof(RibbonFooterContentSimpleDataTemplate);

	}

}
