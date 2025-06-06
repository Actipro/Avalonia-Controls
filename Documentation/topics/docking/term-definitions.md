---
title: "Term Definitions"
page-title: "Term Definitions - Docking & MDI Reference"
order: 3
---
# Term Definitions

There are some key terms and concepts to understand when working with this product.  This topic defines all of the docking-related terms.

## General Terms

<table>
<thead>

<tr>
<th>Term</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>Docking Window</td>
<td>

A docking window (represented by the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) class) is a general term that could mean either a tool window or a document window, since both of those inherit the base docking window functionality.

</td>
</tr>

<tr>
<td>Dock Site</td>
<td>

A dock site (represented by the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) class) is the root control that contains a tool window hierarchy and/or MDI, and provides management functionality for all contained docking-related controls.

</td>
</tr>

<tr>
<td>Document</td>
<td>A document is a docking window (either a document window or tool window) that is within the MDI area.</td>
</tr>

<tr>
<td>Document Window</td>
<td>

A document window (represented by the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) class) is a docking window that is restricted to only be used within a MDI area.

By default, it is destroyed after it is closed, however this behavior can be changed.

</td>
</tr>

<tr>
<td>Floating</td>
<td>Floating is where you have a floating dock host that contains one or more tool windows or a secondary MDI area, and is able to be moved anywhere above the dock site's control hierarchy.</td>
</tr>

<tr>
<td>MDI</td>
<td>

MDI stands for "Multiple Document Interface", meaning the method with which documents can be visually interacted by the end user.

The two built-in types of MDI are Tabbed MDI and Standard MDI.

</td>
</tr>

<tr>
<td>MVVM</td>
<td>MVVM stands for "Model View ViewModel" and is a design pattern used to build applications where the logic is separated from the user interface, testability is increased, as well as other benefits.</td>
</tr>

<tr>
<td>Nested Dock Sites</td>
<td>

Nested dock sites is when you have one [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) that is contained within a docking window that belongs to a parent [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).

In this scenario, the docking windows may only be moved within their respective owner dock sites.

</td>
</tr>

<tr>
<td>Standard MDI</td>
<td>

Standard MDI (implemented via the [StandardMdiHost](xref:@ActiproUIRoot.Controls.Docking.StandardMdiHost) class and its children) is the windowed variation of MDI, where each document is represented by a window that can be moved around within the workspace.  Windows can be cascaded or tiled.

</td>
</tr>

<tr>
<td>Tabbed MDI</td>
<td>

Tabbed MDI (implemented via the [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost) class and its children) is where the full size of the workspace is filled with one or more tabbed containers, with each tab representing a document. Windows can be cascaded or tiled.

</td>
</tr>

<tr>
<td>Tool Window</td>
<td>

A tool window (represented by the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) class) is a docking window that can be used in a number of states, including within the MDI area.

Tool windows usually continue to exist after being closed and can be reactivated later.

</td>
</tr>

<tr>
<td>Tool Window Inner-Fill</td>
<td>

Tool window inner-fill occurs when there is no [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace) within a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite), and the dock site hierarchy consists only of tool windows that fill its content area.

</td>
</tr>

<tr>
<td>Workspace</td>
<td>

A workspace (represented by the [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace) class) is the area around which tool windows may be docked.

If the workspace's content is a [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost), then tabbed MDI is in use.  If the workspace's content is a [StandardMdiHost](xref:@ActiproUIRoot.Controls.Docking.StandardMdiHost), then standard MDI is in use.  The workspace's content can be set to anything else when MDI is not needed and custom content should be displayed.

By not including a workspace within a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) hierarchy, tool window inner-fill mode is activated.

</td>
</tr>

</tbody>
</table>

## Lifecycle Terms

| Term | Description |
|-----|-----|
| Activate | Ensure that a docking window is open, its contents are visible, and optionally allows focus to be set.  If the activated window is represented by a tab, it will become the selected tab in the parent container. |
| Close | Remove a docking window from the dock site hierarchy so that it is no longer accessible. |
| Closed | When a docking window is not currently accessible in the dock site hierarchy. |
| Open | Ensure that a docking window is accessible in the dock site hierarchy, although its contents may not necessarily be visible such as when it is represented by an unselected tab. |
| Opened | When a docking window is currently accessible in the dock site hierarchy, although its related tab may or may not currently be selected.  In the latter case, clicking the tab ensures the window's contents are visible. |
| Selected | When a docking window is opened and represented by a tab that is currently selected. |

## State Terms

| Term | Description |
|-----|-----|
| Auto-Hide | When a tool window's tab appears along the outer edge of a dock host.  Clicking the tab makes the tool window's content slide into view.  Tool windows that are auto-hidden within a floating dock host are still considered to be in an auto-hide state.  This state only applies tool windows. |
| Docked | When a tool window is docked somewhere within a dock host hierarchy, but not in MDI.  Tool windows that are docked within a floating dock host are still considered to be in a docked state.  This state only applies tool windows. |
| Document | When a docking window is within the MDI area.  Docking windows that are in the MDI area of a floating dock host are still considered to be in a document state.  This state applies to both tool and document windows. |

## Container Terms

<table>
<thead>

<tr>
<th>Term</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>Dock Host</td>
<td>

A dock host (represented by the [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost) class) is the actual root hierarchy container.  A primary dock host is implicitly created within a dock site, while other dock hosts are created behind-the-scenes whenever docking windows are floated.  Dock hosts should never be explicitly created by you.

</td>
</tr>

<tr>
<td>Split Container</td>
<td>

A split container (represented by the [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) class) is used to hold one or more other docking containers and provide splitter-based resizing capabilities between them.  Split containers can be oriented horizontally or vertically.

</td>
</tr>

<tr>
<td>Standard MDI Host</td>
<td>

A standard MDI host (represented by the [StandardMdiHost](xref:@ActiproUIRoot.Controls.Docking.StandardMdiHost) class) is the root container for standard MDI implementation, that is placed as the direct child of a workspace.

</td>
</tr>

<tr>
<td>Tabbed MDI Container</td>
<td>

A tabbed MDI container (represented by the [TabbedMdiContainer](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiContainer) class) is the direct parent of one or more docking windows that are in tabbed MDI.

</td>
</tr>

<tr>
<td>Tabbed MDI Host</td>
<td>

A tabbed MDI host (represented by the [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost) class) is the root container for tabbed MDI implementation, that is placed as the direct child of a workspace.

</td>
</tr>

<tr>
<td>Tool Window Container</td>
<td>

A tool window container (represented by the [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) class) is the direct parent of one or more tool windows that are docked or auto-hidden.

This container provides a title area displaying the selected tool window's title.  When more than one tool windows are included, it also contains tabs for selecting the other tool windows.

</td>
</tr>

</tbody>
</table>
