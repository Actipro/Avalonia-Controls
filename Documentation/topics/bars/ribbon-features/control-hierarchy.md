---
title: "Control Hierarchy"
page-title: "Control Hierarchy - Ribbon Features"
order: 2
---
# Control Hierarchy

Ribbon supports many complex scenarios.  This topic provides an overview of how the ribbon is defined, which properties are populated, and which controls are typically used for those properties.  Each type is linked to its corresponding API documentation.  Where applicable, additional topics are cited for a more detailed discussion of a particular feature.

## RibbonWindow

The following hierarchy shows a high-level structure a window that defines a ribbon:

- [RibbonWindow](xref:@ActiproUIRoot.Controls.Bars.RibbonWindow) : `Window` *(see [Ribbon Window](ribbon-window.md) topic)*
  - [RibbonContainerPanel](xref:@ActiproUIRoot.Controls.Bars.RibbonContainerPanel) : `Panel`
    - [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon)
    - *Other content*

> [!IMPORTANT]
> The `RibbonContainerPanel` should be used as the container for a `Ribbon` and the content that appears below the ribbon. The two controls work together to provide smooth animations based on ribbon state changes. While the `RibbonContainerPanel` is not required, it improves the end-user experience.

### Defining Ribbon Within ContentControl

For composible UI frameworks like Prism, it may not be reasonable to define a `Ribbon` directly as a child of a `RibbonContainerPanel`.  In these scenarios, a `ContentControl` can be used as the direct child of a `RibbonContainerPanel` as long as the `ContentControl.Content` is initialized to an instance of `Ribbon` at run-time.  When hosted in a `ContentControl`, it is recommended to set the `Panel.ZIndex` of the `ContentControl` to `1` for the ribbon's shadow chrome to display correctly.

The following sample demonstrates how a `ContentControl` might be configured within a `RibbonContainerPanel`:

@if (avalonia) {
```xaml
<actipro:RibbonWindow ...
	xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui">

	<actipro:RibbonContainerPanel>

		<!-- Ribbon Placeholder -->
		<ContentControl x:Name="ribbonContent" Panel.ZIndex="1" />

		<!-- Other main window content here -->

	</actipro:RibbonContainerPanel>

</actipro:RibbonWindow>
```
}
@if (wpf) {
```xaml
<bars:RibbonWindow ...
	xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars">

	<bars:RibbonContainerPanel>

		<!-- Ribbon Placeholder -->
		<ContentControl x:Name="ribbonContent" Panel.ZIndex="1" />

		<!-- Other main window content here -->

	</bars:RibbonContainerPanel>

</bars:RibbonWindow>
```
}

In the code-behind, the `ribbonContent.Content` should be assigned an instance of `Ribbon`.

## Ribbon

The following hierarchy shows the definition of a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control and the typical types (with their own hierarchies) that are assigned to important properties.

> [!NOTE]
> Any `ItemsControl` listed below that does not explicitly identify a property as a child in the hierarchy should be assumed to be the `Items` or `ItemsSource` property.  Likewise, the child of a `ContentControl` should be assumed to be the `Content` property.

@if (avalonia) {
- [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) : `ItemsControl`
  - [QuickAccessToolBarContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.QuickAccessToolBarContent) : `object` *(see [Quick Access Toolbar](quick-access-toolbar.md) topic)*
    - [RibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar) : `ItemsControl`
      - [CommonItems](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar.CommonItems) : `IEnumerable`
        - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
      - `ItemsControl.Items`
        - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
  - [ApplicationButtonContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ApplicationButtonContent) : `object` *(see [Application Button](application-button.md) topic)*
    - [RibbonApplicationButton](xref:@ActiproUIRoot.Controls.Bars.RibbonApplicationButton) : `Button`
  - [BackstageContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.BackstageContent) : `object` *(see [Backstage](backstage.md) topic)*
    - [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage) : `TabControl`, `ItemsControl`
      - [RibbonBackstageTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageTabItem) : `TabItem`
        - `ContentControl.Content` *(commonly used backstage controls listed below)*
          - [TaskTabControl](xref:@ActiproUIRoot.Controls.Bars.TaskTabControl) : `TabControl`, `ItemsControl`
            - [TaskTabItem](xref:@ActiproUIRoot.Controls.Bars.TaskTabItem) : `TabItem`
          - [RecentDocumentControl](xref:@ActiproUIRoot.Controls.Bars.RecentDocumentControl) : `ItemsControl` *(see [Recent Documents](recent-documents.md) topic)*
            - [RecentDocumentItem](xref:@ActiproUIRoot.Controls.Bars.RecentDocumentItem) : `Button`
          - `BarButton`, `BarPopupButton` *(with backstage styles applied)*
          - *Any object*
      - [RibbonBackstageHeaderButton](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageHeaderButton) : `Button`
      - [RibbonBackstageHeaderSeparator](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageHeaderSeparator) : `Separator`
  - [FooterContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.FooterContent) : `object` *(see [Footer](footer.md) topic)*
    - [RibbonFooterControl](xref:@ActiproUIRoot.Controls.Bars.RibbonFooterControl) : `ContentControl`
    - *Any object*
  - [TabRowToolBarContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.TabRowToolBarContent) : `object` (see [Tab Row Toolbar](tab-row-toolbar.md) topic)
    - [RibbonTabRowToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonTabRowToolBar) : `ItemsControl`
      - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
  - [ContextualTabGroups](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ContextualTabGroups) : `IEnumerable` *(see [Contextual Tabs](contextual-tabs.md) topic)*
    - [RibbonContextualTabGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonContextualTabGroup) : `Control`
  - `ItemsControl.Items`
    - [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) : `ItemsControl` *(see [Tabs, Groups, and Control Groups](tabs-groups-controlgroups.md) topic)*
      - [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup) : `ItemsControl` *(see [Tabs, Groups, and Control Groups](tabs-groups-controlgroups.md) topic)*
        - [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) : `ItemsControl` *(see [Tabs, Groups, and Control Groups](tabs-groups-controlgroups.md) topic)*
          - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
        - [RibbonMultiRowControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup) : `ItemsControl` *(see [Tabs, Groups, and Control Groups](tabs-groups-controlgroups.md) topic)*
          - [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) *or toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
        - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
}
@if (wpf) {
- [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) : `ItemsControl`
  - [QuickAccessToolBarContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.QuickAccessToolBarContent) : `object` *(see [Quick Access Toolbar](quick-access-toolbar.md) topic)*
    - [RibbonQuickAccessToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar) : `ItemsControl`
      - [CommonItems](xref:@ActiproUIRoot.Controls.Bars.RibbonQuickAccessToolBar.CommonItems) : `IEnumerable`
        - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
      - `ItemsControl.Items`
        - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
  - [ApplicationButtonContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ApplicationButtonContent) : `object` *(see [Application Menu](application-menu.md) topic)*
    - [RibbonApplicationButton](xref:@ActiproUIRoot.Controls.Bars.RibbonApplicationButton) : `Menu`, `ItemsControl`
      - [MenuAdditionalContent](xref:@ActiproUIRoot.Controls.Bars.RibbonApplicationButton.MenuAdditionalContent)
          - [RecentDocumentControl](xref:@ActiproUIRoot.Controls.Bars.RecentDocumentControl) : `ItemsControl` *(suggested content, see [Recent Documents](recent-documents.md) topic)*
            - [RecentDocumentItem](xref:@ActiproUIRoot.Controls.Bars.RecentDocumentItem) : `Button`
        - *Any object*
      - [MenuFooter](xref:@ActiproUIRoot.Controls.Bars.RibbonApplicationButton.MenuFooter)
        - *Any object (`BarButton` recommended for [Key Tips](key-tips.md) support)*
      - `ItemsControl.Items`
        - *Menu controls (e.g., `BarMenuItem`... see "Menu Controls" section)*
  - [BackstageContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.BackstageContent) : `object` *(see [Backstage](backstage.md) topic)*
    - [RibbonBackstage](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstage) : `TabControl`, `ItemsControl`
      - [RibbonBackstageTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageTabItem) : `TabItem`
        - `ContentControl.Content` *(commonly used backstage controls listed below)*
          - [TaskTabControl](xref:@ActiproUIRoot.Controls.Bars.TaskTabControl) : `TabControl`, `ItemsControl`
            - [TaskTabItem](xref:@ActiproUIRoot.Controls.Bars.TaskTabItem) : `TabItem`
          - [RecentDocumentControl](xref:@ActiproUIRoot.Controls.Bars.RecentDocumentControl) : `ItemsControl` *(see [Recent Documents](recent-documents.md) topic)*
            - [RecentDocumentItem](xref:@ActiproUIRoot.Controls.Bars.RecentDocumentItem) : `Button`
          - `BarButton`, `BarPopupButton` *(with backstage styles applied)*
          - *Any object*
      - [RibbonBackstageHeaderButton](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageHeaderButton) : `Button`
      - [RibbonBackstageHeaderSeparator](xref:@ActiproUIRoot.Controls.Bars.RibbonBackstageHeaderSeparator) : `Separator`
  - [FooterContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.FooterContent) : `object` *(see [Footer](footer.md) topic)*
    - [RibbonFooterControl](xref:@ActiproUIRoot.Controls.Bars.RibbonFooterControl) : `ContentControl`
    - *Any object*
  - [TabRowToolBarContent](xref:@ActiproUIRoot.Controls.Bars.Ribbon.TabRowToolBarContent) : `object` (see [Tab Row Toolbar](tab-row-toolbar.md) topic)
    - [RibbonTabRowToolBar](xref:@ActiproUIRoot.Controls.Bars.RibbonTabRowToolBar) : `ItemsControl`
      - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
  - [ContextualTabGroups](xref:@ActiproUIRoot.Controls.Bars.Ribbon.ContextualTabGroups) : `IEnumerable` *(see [Contextual Tabs](contextual-tabs.md) topic)*
    - [RibbonContextualTabGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonContextualTabGroup) : `Control`
  - `ItemsControl.Items`
    - [RibbonTabItem](xref:@ActiproUIRoot.Controls.Bars.RibbonTabItem) : `ItemsControl` *(see [Tabs, Groups, and Control Groups](tabs-groups-controlgroups.md) topic)*
      - [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup) : `ItemsControl` *(see [Tabs, Groups, and Control Groups](tabs-groups-controlgroups.md) topic)*
        - [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) : `ItemsControl` *(see [Tabs, Groups, and Control Groups](tabs-groups-controlgroups.md) topic)*
          - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
        - [RibbonMultiRowControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonMultiRowControlGroup) : `ItemsControl` *(see [Tabs, Groups, and Control Groups](tabs-groups-controlgroups.md) topic)*
          - [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) *or toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
        - *Toolbar controls (e.g., `BarButton`... see "Toolbar Controls" section)*
}
### Toolbar Controls

The following controls can appear within the context of a toolbar and show their respective hierarchies:

- [BarButton](xref:@ActiproUIRoot.Controls.Bars.BarButton) : `Button` *(see [Button](../controls/button.md) topic)*
- [BarPopupButton](xref:@ActiproUIRoot.Controls.Bars.BarPopupButton) : `Menu`, `ItemsControl` *(see [Popup Button](../controls/popup-button.md) topic)*
  - *Menu controls (e.g., `BarMenuItem`... see "Menu Controls" section)*
- [BarToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarToggleButton) : `Button` *(see [Button](../controls/button.md) topic)*
- [BarSplitButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitButton) : `Menu`, `ItemsControl` *(see [Split Button](../controls/split-button.md) topic)*
  - *Menu controls (e.g., `BarMenuItem`... see "Menu Controls" section)*
- [BarSplitToggleButton](xref:@ActiproUIRoot.Controls.Bars.BarSplitToggleButton) : `Menu`, `ItemsControl` *(see [Split Button](../controls/split-button.md) topic)*
  - *Menu controls (e.g., `BarMenuItem`... see "Menu Controls" section)*
- [RibbonGallery](xref:@ActiproUIRoot.Controls.Bars.RibbonGallery) : `ItemsControl` *(see [Gallery](../controls/gallery.md) topic)*
  - [AboveMenuItems](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarMenuGalleryHostBase.AboveMenuItems) : `IEnumerable`
    - *Optional additional menu controls above the gallery items (e.g., `BarMenuItem`... see "Menu Controls" section)*
  - `ItemsControl.Items`
    - [BarGalleryItem](xref:@ActiproUIRoot.Controls.Bars.BarGalleryItem) : `ContentControl`
  - [BelowMenuItems](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarMenuGalleryHostBase.BelowMenuItems) : `IEnumerable`
    - *Optional additional menu controls below the gallery items (e.g., `BarMenuItem`... see "Menu Controls" section)*
- [BarCheckBox](xref:@ActiproUIRoot.Controls.Bars.BarCheckBox) : `Button` *(see [Checkbox](../controls/checkbox.md) topic)*
- [BarComboBox](xref:@ActiproUIRoot.Controls.Bars.BarComboBox) : `ItemsControl` *(see [Combobox](../controls/combobox.md) topic)*
  - [AboveMenuItems](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarMenuGalleryHostBase.AboveMenuItems) : `IEnumerable`
    - *Optional additional menu controls above the gallery items (e.g., `BarMenuItem`... see "Menu Controls" section)*
  - `ItemsControl.Items`
    - [BarGalleryItem](xref:@ActiproUIRoot.Controls.Bars.BarGalleryItem) : `ContentControl`
  - [BelowMenuItems](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarMenuGalleryHostBase.BelowMenuItems) : `IEnumerable`
    - *Optional additional menu controls below the gallery items (e.g., `BarMenuItem`... see "Menu Controls" section)*
- [BarTextBox](xref:@ActiproUIRoot.Controls.Bars.BarTextBox) : `TextBox` *(see [Textbox](../controls/textbox.md) topic)*
- [BarSeparator](xref:@ActiproUIRoot.Controls.Bars.BarSeparator) : `Separator` *(see [Separator](../controls/separator.md) topic)*

### Menu Controls

The following controls can appear within the context of a menu and show their respective hierarchies:

- [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem) : `MenuItem`, `ItemsControl` *(see [Button](../controls/button.md) and [Popup Button](../controls/popup-button.md) topics)*
  - *Optional menu controls for sub menu (e.g., `BarMenuItem`)*
- [BarSplitMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarSplitMenuItem) : `MenuItem`, `ItemsControl` *(see [Split Button](../controls/split-button.md) topic)*
  - *Menu controls for sub menu (e.g., `BarMenuItem`)*
- [BarMenuGallery](xref:@ActiproUIRoot.Controls.Bars.BarMenuGallery) : `ItemsControl` *(see [Gallery](../controls/gallery.md) topic)*
  - [BarGalleryItem](xref:@ActiproUIRoot.Controls.Bars.BarGalleryItem) : `ContentControl`
- [BarMenuHeading](xref:@ActiproUIRoot.Controls.Bars.BarMenuHeading) : `MenuItem` *(see [Heading](../controls/heading.md) topic)*
- [BarMenuSeparator](xref:@ActiproUIRoot.Controls.Bars.BarMenuSeparator) : `Separator` *(see [Separator](../controls/separator.md) topic)*
