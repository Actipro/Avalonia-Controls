---
title: "Overview"
page-title: "Docking & MDI Workspace and MDI Features"
order: 1
---
# Overview

Actipro Docking & MDI supports an optional workspace area, which can contain custom content or multiple types of MDI (multiple document interface).

## Workspace

The workspace is the central area of a dock site hierarchy, around which tool windows are docked.  It can contain optional MDI hosts or can host any other sort of content.

See the [Workspace](workspace.md) topic for more information.

## Tabbed MDI

Tabbed MDI is the default type of MDI found in most modern applications and is fully supported by Actipro Docking & MDI.

See the [Tabbed MDI](tabbed-mdi.md) topic for more information.

## Standard MDI

A standard (windowed) MDI option is implemented by Actipro Docking & MDI via a @if (avalonia) { [Window Control](../../fundamentals/controls/window-control.md) }@if (wpf) { [WindowControl](xref:@ActiproUIRoot.Controls.Docking.WindowControl) } that mimics the functionality of a native window with resizable borders, minimize/maximize/close buttons, etc.

See the [Standard MDI](standard-mdi.md) topic for more information.

## Working with Documents

All documents can be easily managed via [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) properties and methods, regardless of whether tabbed or standard MDI is in use.  This allows your document-related code to remain the same regardless of the style of MDI in use.

See the [Working with Documents](working-with-documents.md) topic for more information.
