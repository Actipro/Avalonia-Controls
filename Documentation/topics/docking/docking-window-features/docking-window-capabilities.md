---
title: "Docking Window Capabilities"
page-title: "Docking Window Capabilities - Docking & MDI Docking Window Features"
order: 4
---
# Docking Window Capabilities

One of the best features of Actipro Docking & MDI is the number of options that are available to you as a developer.  Many options control the behaviors and capabilities of the docking windows and can be set at a global level as well as at a docking window instance level.

## Global Default Options

A number of global default options appear on the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) and [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost) classes.  By default, docking windows will use these options unless their instance settings override them.

<table>
<thead>

<tr>
<th>Member</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[AreDocumentWindowsDestroyedOnClose](xref:@ActiproUIRoot.Controls.Docking.DockSite.AreDocumentWindowsDestroyedOnClose) Property

</td>
<td>

Gets or sets whether document windows are automatically destroyed (removed from the `DockSite`) when closed.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[AreNewTabsInsertedBeforeExistingTabs](xref:@ActiproUIRoot.Controls.Docking.DockSite.AreNewTabsInsertedBeforeExistingTabs) Property

</td>
<td>

Gets or sets whether new tabs are inserted before existing tabs when they are added, such as when dragging and attaching a tool window to an existing tool window container.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[CanDocumentsAttach](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.CanDocumentsAttach) Property

</td>
<td>

Gets or sets the global setting for whether documents may be attached to another document, creating a tabbed grouping.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[CanDocumentsCloseOnMiddleClick](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.CanDocumentsCloseOnMiddleClick) Property

</td>
<td>

Gets or sets a value indicating whether documents can be closed by clicking the tab with the middle mouse button.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[CanDocumentTabsDrag](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.CanDocumentTabsDrag) Property

</td>
<td>

Gets or sets the global setting for whether document tabs may be dragged to another location.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanDocumentWindowsClose](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanDocumentWindowsClose) Property

</td>
<td>

Gets or sets the global setting for whether document windows can be closed.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsDragToFloatingDockHostsWithWorkspaces](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsDragToFloatingDockHostsWithWorkspaces) Property

</td>
<td>

Gets or sets the global setting for whether tool windows may be dragged to floating dock hosts that contain a workspace.  The default value is `true`.  When `false`, tool windows can still be dragged and docked against floating dock hosts that only contain tool windows.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanDocumentWindowsDragToLinkedDockSites](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanDocumentWindowsDragToLinkedDockSites) Property

</td>
<td>

Gets or sets the global setting for whether document windows may be dragged to a linked dock site.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanDocumentWindowsFloat](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanDocumentWindowsFloat) Property

</td>
<td>

Gets or sets the global setting for whether document windows may be contained in a floating window.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanFloatingDockHostsHideOnDockSiteUnload](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanFloatingDockHostsHideOnDockSiteUnload) Property

</td>
<td>

Gets or sets whether [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingDockHosts](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingDockHosts) should hide when the `DockSite` is unloaded, such as in nested `DockSite` scenarios when the parent tab is deselected.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsAttach](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsAttach) Property

</td>
<td>

Gets or sets the global setting for whether the end user can edit document window titles in-place when a document window tab is double-clicked.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsAutoHide](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsAutoHide) Property

</td>
<td>

Gets or sets the global setting for whether tool windows may be placed in auto-hide mode.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsBecomeDocuments](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsBecomeDocuments) Property

</td>
<td>

Gets or sets the global setting for whether tool windows may be placed in a document state.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsClose](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsClose) Property

</td>
<td>

Gets or sets the global setting for whether tool windows can be closed.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsCloseOnMiddleClick](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsCloseOnMiddleClick) Property

</td>
<td>

Gets or sets a value indicating whether tool windows can be closed by clicking the tab with the middle mouse button.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsDock](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsDock) Property

</td>
<td>

Gets or sets the global setting for whether tool windows can be docked.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowTabsDrag](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowTabsDrag) Property

</td>
<td>

Gets or sets the global setting for whether tool window tabs may be dragged to another location.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsDragToLinkedDockSites](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsDragToLinkedDockSites) Property

</td>
<td>

Gets or sets the global setting for whether tool windows may be dragged to a linked dock site.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsFloat](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsFloat) Property

</td>
<td>

Gets or sets the global setting for whether tool windows may be floated into separate top-level containers.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ClosePerContainer](xref:@ActiproUIRoot.Controls.Docking.DockSite.ClosePerContainer) Property

</td>
<td>

Gets or sets the global setting for whether tool windows may be attached to another tool window, creating a tabbed grouping.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ContainersHaveNewTabButtons](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.ContainersHaveNewTabButtons) Property

</td>
<td>

Gets or sets whether containers have **New Tab** buttons.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingToolWindowContainersHaveMaximizeButtons](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingToolWindowContainersHaveMaximizeButtons) Property

</td>
<td>

Gets or sets whether floating [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) controls can have title bar maximize buttons.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingToolWindowContainersHaveMinimizeButtons](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingToolWindowContainersHaveMinimizeButtons) Property

</td>
<td>

Gets or sets whether floating [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) controls can have title bar minimize buttons.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingToolWindowContainerTitleBarDoubleClickMode](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingToolWindowContainerTitleBarDoubleClickMode) Property

</td>
<td>

Gets or sets what happens when floating [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) controls' title bars are double-clicked.  The default value is [ToggleMaximized](xref:@ActiproUIRoot.Controls.Docking.FloatingWindowTitleBarDoubleClickMode.ToggleMaximized).

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[HasTabCloseButtons](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.HasTabCloseButtons) Property

</td>
<td>

Gets or sets whether close buttons should appear on document tabs.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[IsDockGuideAnimationEnabled](xref:@ActiproUIRoot.Controls.Docking.DockSite.IsDockGuideAnimationEnabled) Property

</td>
<td>

Gets or sets whether dock guide animation is enabled.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[IsFloatingWindowSnapToScreenEnabled](xref:@ActiproUIRoot.Controls.Docking.DockSite.IsFloatingWindowSnapToScreenEnabled) Property

</td>
<td>

Gets or sets whether floating windows are snapped onto the closest screen when displayed via a method other than being dragged.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[IsLiveSplittingEnabled](xref:@ActiproUIRoot.Controls.Docking.DockSite.IsLiveSplittingEnabled) Property

</td>
<td>

Gets or sets whether live splitting of docking windows is enabled.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[IsTabLayoutAnimationEnabled](xref:@ActiproUIRoot.Controls.Docking.DockSite.IsTabLayoutAnimationEnabled) Property

</td>
<td>

Gets or sets whether animation effects are applied during tab layout, such as when tabs are added or removed.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[SingleTabLayoutBehavior](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.SingleTabLayoutBehavior) Property

</td>
<td>

Gets or sets the global setting for the behavior used when a single docking window is in the MDI host.  The default value is [Show](xref:@ActiproUIRoot.Controls.Docking.SingleTabLayoutBehavior.Show).

</td>
</tr>

<tr>
<td>

[TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[TabOverflowBehavior](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.TabOverflowBehavior) Property

</td>
<td>

Gets or sets the global setting for the overflow behavior of the document tabs.  The default value is [Menu](xref:@ActiproUIRoot.Controls.Docking.TabOverflowBehavior.Menu).

</td>
</tr>

<tr>
<td>

[TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[TabStripPlacement](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.TabStripPlacement) Property

</td>
<td>

@if (avalonia) {
Gets or sets a `Dock` side indicating the side upon which the tabs are located.  The default value is `Dock.Top`.
}
@if (wpf) {
Gets or sets a [Side](xref:@ActiproUIRoot.Controls.Side) indicating the side upon which the tabs are located.  The default value is [Top](xref:@ActiproUIRoot.Controls.Side.Top).
}

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsHaveCloseButtons](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsHaveCloseButtons) Property

</td>
<td>

Gets or sets whether [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) controls can have title bar close buttons.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsHaveOptionsButtons](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsHaveOptionsButtons) Property

</td>
<td>

Gets or sets whether [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) controls can have title bar options buttons.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsHaveTitleBarIcons](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsHaveTitleBarIcons) Property

</td>
<td>

Gets or sets the global setting for whether tool window title bars display the icon of the selected tool window.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsHaveTitleBars](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsHaveTitleBars) Property

</td>
<td>

Gets or sets the global setting for whether tool windows display a title bar when not in MDI.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsHaveToggleAutoHideButtons](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsHaveToggleAutoHideButtons) Property

</td>
<td>

Gets or sets whether [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) controls can have title bar toggle auto-hide buttons.  The default value is `true`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsSingleTabLayoutBehavior](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsSingleTabLayoutBehavior) Property

</td>
<td>

Gets or sets the global setting for the behavior used when a single tool window is in a container.  The default value is [Hide](xref:@ActiproUIRoot.Controls.Docking.SingleTabLayoutBehavior.Hide).

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsTabOverflowBehavior](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsTabOverflowBehavior) Property

</td>
<td>

Gets or sets the global setting for the overflow behavior of the tool window tabs.  The default value is [Shrink](xref:@ActiproUIRoot.Controls.Docking.TabOverflowBehavior.Shrink).

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsTabStripPlacement](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsTabStripPlacement) Property

</td>
<td>

Gets or sets the global setting for the side upon which the tool window tabs are located.  The default value is @if (avalonia) { `Dock.Bottom` }@if (wpf) { [Bottom](xref:@ActiproUIRoot.Controls.Side.Bottom) }.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[UseDragFloatPreviews](xref:@ActiproUIRoot.Controls.Docking.DockSite.UseDragFloatPreviews) Property

</td>
<td>

Gets or sets whether to use float previews when dragging windows instead of instantly creating floating windows.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[UseHostedFloatingWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.UseHostedFloatingWindows) Property

</td>
<td>

Gets or sets whether floating windows should be hosted within the bounds of the dock site.  The default value is `false`.

</td>
</tr>

<tr>
<td>

[DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[UseHostedPopups](xref:@ActiproUIRoot.Controls.Docking.DockSite.UseHostedPopups) Property

</td>
<td>

Gets or sets whether popups should be hosted within the bounds of the dock site.  The default value is `true`.

</td>
</tr>

</tbody>
</table>

## Docking Window Instance Options

All of the global default options listed above will apply to docking windows by default.  Related docking window instance options are all nullable booleans with default values of `null`, indicating that they will inherit the global defaults.

| Member | Description |
|-----|-----|
| [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanAttach](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanAttach) Property | Gets or sets whether the window may be attached to another window, creating a tabbed grouping. |
| [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).[CanAutoHide](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.CanAutoHide) Property | Gets or sets whether the tool window may be placed in auto-hide mode. |
| [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).[CanBecomeDocument](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.CanBecomeDocument) Property | Gets or sets whether the tool window may be placed in a document state. |
| [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanClose](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanClose) Property | Gets or sets whether the window may be closed. |
| [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanDock](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanDock) Property | Gets or sets whether the tool window may be docked. |
| [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanDragTab](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanDragTab) Property | Gets or sets whether the window's tab may be dragged to another location. |
| [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanDragToLinkedDockSites](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanDragToLinkedDockSites) Property | Gets or sets whether the window may be dragged to a linked dock site. |
| [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanFloat](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanFloat) Property | Gets or sets whether the window may be floated into a separate top-level container. |
| [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanSerialize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanSerialize) Property | Gets or sets whether the window's layout information may be serialized. |
| [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanStandardMdiMaximize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanStandardMdiMaximize) Property | Gets or sets whether the window may be maximized when hosted in a [StandardMdiHost](xref:@ActiproUIRoot.Controls.Docking.StandardMdiHost). |
| [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanStandardMdiMinimize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanStandardMdiMinimize) Property | Gets or sets whether the window may be minimized when hosted in a [StandardMdiHost](xref:@ActiproUIRoot.Controls.Docking.StandardMdiHost). |
| [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).[HasOptionsButton](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.HasOptionsButton) Property | Gets or sets whether the window has an options menu available. |
| [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).[HasTitleBar](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.HasTitleBar) Property | Gets or sets whether the window displays a title bar when docked. |

## How to Allow Tabs to Be Reordered, But Not Dragged Elsewhere

In some situations, you may wish to keep docking windows in their current container but still want to allow them to be reordered.

To accomplish this, turn all the appropriate options off such as [CanDock](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanDock) and [CanFloat](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanFloat), etc. but leave [CanDragTab](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanDragTab) on.  This will allow the user to reorder tabs by dragging them within their container but will prevent the docking window from moving outside of the container.

## Limiting the Allowed Dock Guides While Dragging

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowsDragOver](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowsDragOver) event is raised while dragging docking windows over a new drop target.  This event is passed an instance of [DockingWindowsDragOverEventArgs](xref:@ActiproUIRoot.Controls.Docking.DockingWindowsDragOverEventArgs), which indicates the [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost) being dragged, the docking windows inside that [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost), and the [IDockTarget](xref:@ActiproUIRoot.Controls.Docking.IDockTarget) control that the pointer is currently over.

The [DockingWindowsDragOverEventArgs](xref:@ActiproUIRoot.Controls.Docking.DockingWindowsDragOverEventArgs).[SupportedDockGuideKinds](xref:@ActiproUIRoot.Controls.Docking.DockingWindowsDragOverEventArgs.SupportedDockGuideKinds) property lists the dock guide kinds (a [DockGuideKinds](xref:@ActiproUIRoot.Controls.Docking.DockGuideKinds) flags enumeration value) that are supported with this particular combination and will be shown by default.

If you wish to prevent certain supported dock guides from being displayed, change the [DockingWindowsDragOverEventArgs](xref:@ActiproUIRoot.Controls.Docking.DockingWindowsDragOverEventArgs).[AllowedDockGuideKinds](xref:@ActiproUIRoot.Controls.Docking.DockingWindowsDragOverEventArgs.AllowedDockGuideKinds) property value.  It defaults to the [SupportedDockGuideKinds](xref:@ActiproUIRoot.Controls.Docking.DockingWindowsDragOverEventArgs.SupportedDockGuideKinds) property value.

For instance, if you'd only like docking windows to be able to dock left, right, or center (attach), you could set the [AllowedDockGuideKinds](xref:@ActiproUIRoot.Controls.Docking.DockingWindowsDragOverEventArgs.AllowedDockGuideKinds) property to `OuterLeft | InnerLeft | Center | InnerRight | OuterRight`.  Any values that you set to this property that aren't "supported" at the time will be ignored.

You can use complex logic to tailor the allowed dock guides based on your application design and its current state.

## Magnetism

Magnetism allows floating windows to be "snapped" together when moving or resizing.  This makes it easier for the end-user to line up the windows into rows or columns.

Magnetism can be configured using [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[MagnetismSnapDistance](xref:@ActiproUIRoot.Controls.Docking.DockSite.MagnetismSnapDistance) and [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[MagnetismGapDistance](xref:@ActiproUIRoot.Controls.Docking.DockSite.MagnetismGapDistance).  The snap distance indicates the distance at which magnetism begins to snap windows being dragged.  The gap distance indicates the distance between windows that are snapped together via magnetism.  Magnetism can be disabled by setting the snap distance to `0`.

## Images on Tabs Option

@if (avalonia) {
The image defined by [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Icon](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Icon) can appear on docking window tabs when tab images are enabled.

You can force the image to display on the tool window tabs by setting the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsHaveTabIcons](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsHaveTabIcons) property to `true`, or on tabbed MDI tabs by setting the [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[HasTabIcons](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.HasTabIcons) property to `true`.
}
@if (wpf) {
The [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[ImageSource](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ImageSource) image can appear on docking window tabs when tab images are enabled.  Several themes, such as the non-Metro themes, turn this feature on by default.

You can force the image to display on the tool window tabs by setting the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsHaveTabImages](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsHaveTabImages) property to `true`, or on tabbed MDI tabs by setting the [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[HasTabImages](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.HasTabImages) property to `true`.
}

## Single Tab Layout Behavior

### Tool Windows

By default, when a [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) only has a single [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow), then the tabs are hidden.  This mimics the behavior seen in Visual Studio but can be customized.  The tab for the single window can be hidden or shown.

The behavior can be customized using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindowsSingleTabLayoutBehavior](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindowsSingleTabLayoutBehavior) property, whose default value is [Hide](xref:@ActiproUIRoot.Controls.Docking.SingleTabLayoutBehavior.Hide).

@if (avalonia) {
![Screenshot](../images/single-tab-layout-behavior.png)
}
@if (wpf) {
![Screenshot](../images/single-tab-layout-behavior-hide.png)![Screenshot](../images/single-tab-layout-behavior-show.png)
}

*A ToolWindowContainer with a single window using Hide and Show behaviors*

### Tabbed MDI

By default, tabs are always displayed in a [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).  For some applications, it might be desirable to hide the tabs when there is only one [TabbedMdiContainer](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiContainer) in a [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost) and that container only has a single [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).

The behavior can be customized using the [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost).[SingleTabLayoutBehavior](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost.SingleTabLayoutBehavior) property, whose default value is [Show](xref:@ActiproUIRoot.Controls.Docking.SingleTabLayoutBehavior.Show).

## Tinting Tabs

Tabs can be tinted towards a color by setting the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[TabTintColor](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.TabTintColor) property.  The tab will have the same general visual appearance, but the colors will be skewed towards the tint color.

![Screenshot](../images/tinted-tabs.png)

*Several tinted tabs*

## Flashing Tabs

Tabs can enter a flashing mode by setting the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[TabFlashMode](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.TabFlashMode) property to either [Blink](xref:@ActiproUIRoot.Controls.Docking.TabFlashMode.Blink) or [Smooth](xref:@ActiproUIRoot.Controls.Docking.TabFlashMode.Smooth).
- The [Blink](xref:@ActiproUIRoot.Controls.Docking.TabFlashMode.Blink) mode will cause a tab to instantly toggle between flashing and normal appearances, holding each for a brief duration, until the mode is changed back to [None](xref:@ActiproUIRoot.Controls.Docking.TabFlashMode.None).
- The [Smooth](xref:@ActiproUIRoot.Controls.Docking.TabFlashMode.Smooth) mode will do a smooth transition between the flashing and normal appearances until the mode is changed back to [None](xref:@ActiproUIRoot.Controls.Docking.TabFlashMode.None).

The [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[TabFlashColor](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.TabFlashColor) property specifies the color used to tint the tab for its flash effect.

Flashing is best used to grab the end user's attention and let them know that a tab should be clicked.  Watch the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowActivated](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowActivated) event to see when the docking window is activated.  At that point, set the [TabFlashMode](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.TabFlashMode) property back to [None](xref:@ActiproUIRoot.Controls.Docking.TabFlashMode.None) to disable flashing.

## ToolWindowContainer Title Font

The [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) control has several properties that can be set to alter its title font appearance:

- [TitleFontFamily](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer.TitleFontFamily)
- [TitleFontSize](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer.TitleFontSize)
- [TitleFontWeight](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer.TitleFontWeight)

Properties like these should never be set directly on specific instances of container controls since containers are created and destroyed dynamically at run-time with layout changes.  Instead, if you wish to change these properties, use an implicit `Style` that targets [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) in your application's @if (avalonia) { `Styles` }@if (wpf) { `Resources` }:

@if (avalonia) {
```xaml
<Style Selector="actipro|ToolWindowContainer">
	<Setter Property="TitleFontFamily" Value="Verdana" />
	<Setter Property="TitleFontSize" Value="14" />
	<Setter Property="TitleFontWeight" Value="Bold" />
</Style>
```
}
@if (wpf) {
```xaml
<Style TargetType="docking:ToolWindowContainer">
	<Setter Property="TitleFontFamily" Value="Verdana" />
	<Setter Property="TitleFontSize" Value="14" />
	<Setter Property="TitleFontWeight" Value="Bold" />
</Style>
```
}

That implicit `Style` will update all instances of the container within your application.
