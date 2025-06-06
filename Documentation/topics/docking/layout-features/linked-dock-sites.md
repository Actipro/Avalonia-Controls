---
title: "Linked Dock Sites"
page-title: "Linked Dock Sites - Docking & MDI Layout, Globalization, and Accessibility Features"
order: 5
---
# Linked Dock Sites

Dock sites are completely self-contained, but they can be linked, which allows the document and tool windows from one dock site to be dragged/moved to the other dock site.

## Linking/Unlinking Dock Sites

A [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) can be linked with one or more other [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite)s using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[LinkDockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite.LinkDockSite*) method.  This method only needs to be called on one of the dock sites, as both dock sites will be linked to each other.

Two dock sites can be unlinked using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[UnlinkDockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite.UnlinkDockSite*) method.  Like with the `LinkDockSite` method, this method only needs to be called on one of the two linked dock sites.

The list of currently linked dock sites can be obtained using the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[LinkedDockSites](xref:@ActiproUIRoot.Controls.Docking.DockSite.LinkedDockSites) property.

> [!NOTE]
> Dock sites hold weak references to their linked dock sites. This allows dock sites to be implicitly unlinked by allowing a dock site to fall out of scope and garbage collected.

## Programmatically Moving Windows

The [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[MoveToLinkedDockSite](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.MoveToLinkedDockSite*) method can be used to move a specific document or tool window to a linked dock site.  After the method has been called, you can use any programmatic methods to open, dock, move to MDI, etc. the docking window within its new dock site.

## Preventing Windows from Dragging

As soon as the dock sites are linked, the documents and/or tool windows can be moved between them, both programmatically and interactively.

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanDocumentWindowsDragToLinkedDockSites](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanDocumentWindowsDragToLinkedDockSites) can be set to `false` to prevent [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow)s from being interactively dragged between linked dock sites.  To prevent [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow)s from being interactively dragged to a linked dock site, the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[CanToolWindowsDragToLinkedDockSites](xref:@ActiproUIRoot.Controls.Docking.DockSite.CanToolWindowsDragToLinkedDockSites) can be set to false.  Alternatively, the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[CanDragToLinkedDockSites](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.CanDragToLinkedDockSites) property can be used to prevent windows from being dragged to linked dock sites on a window by window basis.

## Nested Dock Sites

When nested dock sites are linked and you are dragging over multiple dock hosts, the dock guides will show for the dock host in the same dock site by default.  Hold the <kbd>Shift</kbd> key while dragging to show the dock guides for an alternate dock host instead, which could be an inner or outer dock host.
