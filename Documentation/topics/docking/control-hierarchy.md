---
title: "Control Hierarchy"
page-title: "Control Hierarchy - Docking & MDI Reference"
order: 4
---
# Control Hierarchy

Since a number of controls are involved in creating a docking window and MDI hierarchy, this topic delves into what can be contained by what in your XAML.

> [!NOTE]
> When creating a layout programmatically (not in XAML), refer to some of the other topics in this documentation.

## DockSite Hierarchy

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) is always the root control in a docking window and/or MDI hierarchy.  It implicitly creates a primary [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost) within its template.  The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) has a single child control that becomes the child control of the primary [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost) in the template.

![Screenshot](images/dock-site-hierarchy.png)

*A DockSite hierarchy diagram*

### Child Content

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[Child](xref:@ActiproUIRoot.Controls.Docking.DockSite.Child) can be one of the following:

- [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer)
- [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer)
- [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace)

Each [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) has an [Orientation](xref:@ActiproUIRoot.Controls.Docking.SplitContainer.Orientation) property that indicates whether the splits are horizontal or vertical. [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) objects can be nested to create complex hierarchies. [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) children can be one of the following:

- [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer)
- [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer)
- [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace)

[ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) controls contain one or more [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) controls and provide a title bar and tabs for selecting between tool windows.

[Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace) defines the area around which tool window can be docked.  If no [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace) is included within the hierarchy, tool window inner-fill mode is activated.  A [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace) control has a single child.  If the [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace)`.Content` is a [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost), tabbed MDI is activated.  If the [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace)`.Content` is a [StandardMdiHost](xref:@ActiproUIRoot.Controls.Docking.StandardMdiHost), standard MDI is activated.

This example shows two attached tool windows docked on the right side of a workspace with empty tabbed MDI:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<actipro:DockSite>
	<actipro:SplitContainer>
		<actipro:Workspace>
			<actipro:TabbedMdiHost />
		</actipro:Workspace>

		<actipro:ToolWindowContainer>
			<actipro:ToolWindow Title="Tool Window 1" />
			<actipro:ToolWindow Title="Tool Window 2" />
		</actipro:ToolWindowContainer>
	</actipro:SplitContainer>
</actipro:DockSite>
```
}
@if (wpf) {
```xaml
xmlns:docking="http://schemas.actiprosoftware.com/winfx/xaml/docking"
...

<docking:DockSite>
	<docking:SplitContainer>
		<docking:Workspace>
			<docking:TabbedMdiHost />
		</docking:Workspace>

		<docking:ToolWindowContainer>
			<docking:ToolWindow Title="Tool Window 1" />
			<docking:ToolWindow Title="Tool Window 2" />
		</docking:ToolWindowContainer>
	</docking:SplitContainer>
</docking:DockSite>
```
}

### Auto-Hide Configuration

To add tool windows into an auto-hide state, use the four auto-hide properties:

- [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[AutoHideLeftContainers](xref:@ActiproUIRoot.Controls.Docking.DockSite.AutoHideLeftContainers)
- [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[AutoHideTopContainers](xref:@ActiproUIRoot.Controls.Docking.DockSite.AutoHideTopContainers)
- [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[AutoHideRightContainers](xref:@ActiproUIRoot.Controls.Docking.DockSite.AutoHideRightContainers)
- [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[AutoHideBottomContainers](xref:@ActiproUIRoot.Controls.Docking.DockSite.AutoHideBottomContainers)

 Each of those accepts one or more [ToolWindowContainer](xref:@ActiproUIRoot.Controls.Docking.ToolWindowContainer) controls, each of which can contain one or more [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) controls.

This example shows two tool windows docked vertically on the right side of a workspace with empty tabbed MDI, and an auto-hidden tool window on the left:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<actipro:DockSite>
	<actipro:DockSite.AutoHideLeftContainers>
		<actipro:ToolWindowContainer>
			<actipro:ToolWindow Title="Tool Window 3" />
		</actipro:ToolWindowContainer>
	</actipro:DockSite.AutoHideLeftContainers>

	<actipro:SplitContainer>
		<actipro:Workspace>
			<actipro:TabbedMdiHost />
		</actipro:Workspace>

		<actipro:SplitContainer Orientation="Vertical">
			<actipro:ToolWindowContainer>
				<actipro:ToolWindow Title="Tool Window 1" />
			</actipro:ToolWindowContainer>
			<actipro:ToolWindowContainer>
				<actipro:ToolWindow Title="Tool Window 2" />
			</actipro:ToolWindowContainer>
		</actipro:SplitContainer>
	</actipro:SplitContainer>
</actipro:DockSite>
```
}
@if (wpf) {
```xaml
xmlns:docking="http://schemas.actiprosoftware.com/winfx/xaml/docking"
...

<docking:DockSite>
	<docking:DockSite.AutoHideLeftContainers>
		<docking:ToolWindowContainer>
			<docking:ToolWindow Title="Tool Window 3" />
		</docking:ToolWindowContainer>
	</docking:DockSite.AutoHideLeftContainers>

	<docking:SplitContainer>
		<docking:Workspace>
			<docking:TabbedMdiHost />
		</docking:Workspace>

		<docking:SplitContainer Orientation="Vertical">
			<docking:ToolWindowContainer>
				<docking:ToolWindow Title="Tool Window 1" />
			</docking:ToolWindowContainer>
			<docking:ToolWindowContainer>
				<docking:ToolWindow Title="Tool Window 2" />
			</docking:ToolWindowContainer>
		</docking:SplitContainer>
	</docking:SplitContainer>
</docking:DockSite>
```
}

## Tabbed MDI Hierarchy

When the [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace)`.Content` is a [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost), tabbed MDI mode is activated.  Tabbed MDI is where the full size of the workspace is filled with one or more tabbed containers, with each tab representing a document. Windows can be cascaded or tiled.

![Screenshot](images/tabbed-mdi-hierarchy.png)

*A tabbed MDI hierarchy diagram*

The [TabbedMdiHost](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiHost) control has a single child, which can be one of the following:

- [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer)
- [TabbedMdiContainer](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiContainer)

Each [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) has an [Orientation](xref:@ActiproUIRoot.Controls.Docking.SplitContainer.Orientation) property that indicates whether the splits are horizontal or vertical. [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) objects can be nested to create complex hierarchies. [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) children can be one of the following:

- [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer)
- [TabbedMdiContainer](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiContainer)

[TabbedMdiContainer](xref:@ActiproUIRoot.Controls.Docking.TabbedMdiContainer) controls contain one or more [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) and/or [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) controls and provide tabs for selecting between the documents.

This example shows a single document open in tabbed MDI:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<actipro:DockSite>
	<actipro:Workspace>
		<actipro:TabbedMdiHost>
			<actipro:TabbedMdiContainer>
				<actipro:DocumentWindow Title="Document1.txt" Description="Text document" FileName="Document1.rtf">
					<TextBox BorderThickness="0" TextWrapping="Wrap" Text="This is a document window." />
				</actipro:DocumentWindow>
			</actipro:TabbedMdiContainer>
		</actipro:TabbedMdiHost>
	</actipro:Workspace>
</actipro:DockSite>
```
}
@if (wpf) {
```xaml
xmlns:docking="http://schemas.actiprosoftware.com/winfx/xaml/docking"
...

<docking:DockSite>
	<docking:Workspace>
		<docking:TabbedMdiHost>
			<docking:TabbedMdiContainer>
				<docking:DocumentWindow Title="Document1.txt" Description="Text document" FileName="Document1.rtf">
					<TextBox BorderThickness="0" TextWrapping="Wrap" Text="This is a document window." />
				</docking:DocumentWindow>
			</docking:TabbedMdiContainer>
		</docking:TabbedMdiHost>
	</docking:Workspace>
</docking:DockSite>
```
}

## Standard MDI Hierarchy

When the [Workspace](xref:@ActiproUIRoot.Controls.Docking.Workspace)`.Content` is a [StandardMdiHost](xref:@ActiproUIRoot.Controls.Docking.StandardMdiHost), standard MDI mode is activated.  Standard MDI is the windowed variation of MDI, where each document is represented by a window that can be moved around within the workspace.  Windows can be cascaded or tiled.

![Screenshot](images/standard-mdi-hierarchy.png)

*A standard MDI hierarchy diagram*

The [StandardMdiHost](xref:@ActiproUIRoot.Controls.Docking.StandardMdiHost) control contains one or more [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) and/or [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) controls.  It renders each document as a window.

This example shows a single document open in standard MDI:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<actipro:DockSite>
	<actipro:Workspace>
		<actipro:StandardMdiHost>
			<actipro:DocumentWindow Title="Document1.txt" Description="Text document" FileName="Document1.rtf">
				<TextBox BorderThickness="0" TextWrapping="Wrap" Text="This is a document window." />
			</actipro:DocumentWindow>
		</actipro:StandardMdiHost>
	</actipro:Workspace>
</actipro:DockSite>
```
}
@if (wpf) {
```xaml
xmlns:docking="http://schemas.actiprosoftware.com/winfx/xaml/docking"
...

<docking:DockSite>
	<docking:Workspace>
		<docking:StandardMdiHost>
			<docking:DocumentWindow Title="Document1.txt" Description="Text document" FileName="Document1.rtf">
				<TextBox BorderThickness="0" TextWrapping="Wrap" Text="This is a document window." />
			</docking:DocumentWindow>
		</docking:StandardMdiHost>
	</docking:Workspace>
</docking:DockSite>
```
}