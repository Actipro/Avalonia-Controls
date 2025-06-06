---
title: "Lifecycle and Docking Management"
page-title: "Lifecycle and Docking Management - Docking & MDI Docking Window Features"
order: 3
---
# Lifecycle and Docking Management

The lifecycle of a docking window includes its creation, specifying initial size and/or size restrictions, providing a default open location, opening, moving to various states, docking to various locations, closing, and destruction.  This topic covers how to perform all of those operations.

## Tool Windows vs. Document Windows

Tool and document windows are the two varieties of docking windows that exist in the Docking/MDI product.  While they both share many capabilities, they have several functional differences, and you should choose to use the appropriate kind based on your UI needs.

Tool windows are capable of being docked outside of MDI, auto-hidden to the side of a dock host, or used in the MDI area.  They have no out-of-the-box restrictions on where they can go.  Tool windows are typically used for scenarios where the docking window needs to support all those state locations or needs to frequently be closed and reopened.  They remain registered with the dock site after they are closed, and thus can be readily reopened into the layout in their previous locations.

Document windows, on the other hand, can only be used within the MDI area.  Since document windows are intended to have a limited lifetime, they are destroyed (unregistered from the dock site) by default when they are closed.  See a section below for information on altering this behavior.  Document windows are typically used for scenarios where the docking window is no longer needed after it is closed.

## Creation and Destruction

When a [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) has been associated with a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite), it is considered created.  Note that at this time it may not yet be opened (accessible in the user interface).

After a docking window ceases to be managed by a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite), it is considered destroyed.

While being tracked by a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite), tool windows will be contained in its [ToolWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindows) collection and document windows will be contained in its [DocumentWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentWindows) collection.

### Creating Tool Window

Use this code to create a tool window.  It is recommended to always use a constructor that accepts a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) parameter since this associates the tool window with that [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).

@if (avalonia) {
```csharp
var tb = new TextBox() {
	BorderThickness = new Thickness(0),
};
var window = new ToolWindow(dockSite,
	serializationId: "MyToolWindow1",
	title: "My First Tool Window",
	icon: "avares://SampleBrowser/Images/Icons/Properties16.png",
	content: tb
);
```
}
@if (wpf) {
```csharp
var tb = new TextBox() {
	BorderThickness = new Thickness(0),
};
var window = new ToolWindow(dockSite,
	serializationId: "MyToolWindow1",
	title: "My First Tool Window",
	imageSource: new BitmapImage(new Uri("/Resources/Images/Properties16.png", UriKind.Relative)),
	content: tb
);
```
}

When using the constructor in the example above, the first parameter is the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) that will manage the window.

The second is the value to be assigned to the [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) property.

> [!IMPORTANT]
> A unique `Name` or [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) must be assigned to each tool window if layout serialization will be used in your application.  The `Name` property must be a valid C#/VB identifier (underscore, letter, and number characters only), while the [SerializationId](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SerializationId) is more flexible and can consist of any characters.

The third and fourth parameters are the title text and @if (avalonia) { icon data }@if (wpf) { `ImageSource` } respectively that will be displayed in the UI for the tool window.

The final parameter is the content that will be placed in the tool window.

### Creating Document Windows

Use this code to create a document window.  It is recommended to always use a constructor that accepts a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) parameter since this associates the document window with that [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).

@if (avalonia) {
```csharp
var tb = new TextBox() {
	BorderThickness = new Thickness(0),
};
var window = new DocumentWindow(dockSite,
	serializationId: "MyDocumentWindow1",
	title: "My First Document Window",
	icon: "avares://SampleBrowser/Images/Icons/TextDocument16.png",
	content: tb);
```
}
@if (wpf) {
```csharp
var tb = new TextBox() {
	BorderThickness = new Thickness(0),
};
var window = new DocumentWindow(dockSite,
	serializationId: "MyDocumentWindow1",
	title: "My First Document Window",
	imageSource: new BitmapImage(new Uri("/Resources/Images/TextDocument16.png", UriKind.Relative)),
	content: tb);
```
}

The same notes above regarding constructor parameters apply to document windows as well with one exception.  Since document windows are not normally serialized with a layout, the `serializationId` parameter is not important to pass and may be `null`.

### Destroying Docking Windows

Both tool and document windows may be destroyed by calling the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Destroy](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Destroy*) method.

This closes them out of the UI (if currently open) and removes them from their managing [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).

> [!WARNING]
> By default, document windows auto-destroy themselves when closed.  See the following section for more information.

## Preventing Document Windows from Auto-Destroying on Close

By default, document windows are only alive while open and will destroy themselves after being closed.  This is the default behavior since once a document is closed, it is normally no longer needed unless the end user specifically chooses to open it again, which may or may not ever occur.

You can alter this behavior by changing the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[AreDocumentWindowsDestroyedOnClose](xref:@ActiproUIRoot.Controls.Docking.DockSite.AreDocumentWindowsDestroyedOnClose) property to `false`.  When `false`, document windows will continue to be associated with the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) and will remain within its [DocumentWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentWindows) collection, allowing them to be retrieved for later reopening.

## Opening and Closing

When open, a docking window is accessible in the user interface.  When closed, it is not.

@if (avalonia) {
> [!CAUTION]
> It is very important to *not* set the `DockingWindow.IsVisible` property yourself.  The docking framework itself will manage user interface visibility based on whether a docking window is currently open or not.
}
@if (wpf) {
> [!CAUTION]
> It is very important to *not* set the `DockingWindow.Visibility` property yourself.  The docking framework itself will manage user interface visibility based on whether a docking window is currently open or not.
}

### Opening Docking Windows

Docking windows aren't added to the user interface until specifically opened.  A call to the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Open](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Open*) method will restore the docking window to its previous state and location, if known.  If the docking window doesn't have a prior location breadcrumb available, it will be restored to a default location based on its property settings.

Setting the [IsOpen](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsOpen) property to `true` is the same as calling the parameterless [Open](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Open*) method.  This property can be bound to in MVVM scenarios.

### Closing Docking Windows

Docking windows can be removed from the user interface by a call to their [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Close](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Close*) method.

Setting the [IsOpen](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsOpen) property to `false` is the same as calling the [Close](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Close*) method.  This property can be bound to in MVVM scenarios.

When closed, tool windows remain in the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolWindows) collection until destroyed.  However, when document windows are closed, by default they will be destroyed as well unless the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[AreDocumentWindowsDestroyedOnClose](xref:@ActiproUIRoot.Controls.Docking.DockSite.AreDocumentWindowsDestroyedOnClose) property is set to `false`.

## Activating and Focusing

Activating is a superset of the Open functionality.  When the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Activate](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Activate*) method is called, if the docking window is currently closed, it will be opened.  Additionally, if it is open but in a tabbed scenario where it is not the selected tab, its tab will be selected.

The [Activate](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Activate*) method also has an overload for whether to set focus to the docking window's content.

Setting the [IsActive](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsActive) property to `true` is the same as calling the parameterless [Activate](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Activate*) method.  This property can be bound to in MVVM scenarios.

The [LastActiveDateTime](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.LastActiveDateTime) property is updated every time the docking window is activated, either programmatically or by focus moving within it.  Sorting docking windows by this property value allows you to determine the sequence in which the docking windows were last active.

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ActiveWindow](xref:@ActiproUIRoot.Controls.Docking.DockSite.ActiveWindow) tracks the currently active window.  This property changes whenever a different docking window gains focus, and the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowActivated](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowActivated) event is raised in response.

## Default Initial Size

Docking windows default to a desired initial size of `200,200`.  Sometimes you may wish to set a different default initial size for docking windows.  This can be done by using the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[ContainerDockedSize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ContainerDockedSize) property.

This example shows the property being set that will request that the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) have an initial size of `300,150`:

@if (avalonia) {
```xaml
... <actipro:ToolWindow Title="Wide" ContainerDockedSize="300,150"> ...
```
}
@if (wpf) {
```xaml
... <docking:ToolWindow Title="Wide" ContainerDockedSize="300,150"> ...
```
}

> [!CAUTION]
> Since the docking elements need to dynamically change their size based on their location and available space, the `Width`, `Height`, `MinWidth`, `MinHeight`, `MaxWidth`, and `MaxHeight` properties should *never* be used.

## Minimum and Maximum Size

Docking windows default to a minimum size of `30,30` and a maximum size of positive infinity.  Sometimes you may wish to designate some restrictions on size, such as a tool window's container should always try to be a minimum of `180` high.  This can be done by using the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[ContainerMinSize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ContainerMinSize) and [ContainerMaxSize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ContainerMaxSize) properties.

This example shows how to request a minimum container height of `180`:

@if (avalonia) {
```xaml
... <actipro:ToolWindow Title="Min Height" ContainerMinSize="30,180"> ...
```
}
@if (wpf) {
```xaml
... <docking:ToolWindow Title="Min Height" ContainerMinSize="30,180"> ...
```
}

By setting the minimum size to the same value as the maximum size, you can achieve a fixed size.  This example shows how to request a fixed container height of `180`:

@if (avalonia) {
```xaml
... <actipro:ToolWindow Title="Fixed Height" ContainerMinSize="30,180" ContainerMaxSize="10000,180"> ...
```
}
@if (wpf) {
```xaml
... <docking:ToolWindow Title="Fixed Height" ContainerMinSize="30,180" ContainerMaxSize="10000,180"> ...
```
}

> [!CAUTION]
> Since the docking elements need to dynamically change their size based on their location and available space, the `Width`, `Height`, `MinWidth`, `MinHeight`, `MaxWidth`, and `MaxHeight` properties should *never* be used.

## Default Locations

When a docking window is opened by calling its [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[Open](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Open*) method or by setting the [IsOpen](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.IsOpen) property to `true`, it will first attempt to restore to a previous location where a related breadcrumb was left.  If no breadcrumb is found, it will restore to a default location instead.

This is how a default location is determined:

1. First, if a [WindowGroupName](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.WindowGroupName) value is specified, the docking window will try to find another open docking window that has the same window group name.

1. Second, the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[DefaultLocationRequested](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.DefaultLocationRequested) event is raised with the [DockingWindowDefaultLocationEventArgs](xref:@ActiproUIRoot.Controls.Docking.DockingWindowDefaultLocationEventArgs).[Target](xref:@ActiproUIRoot.Controls.Docking.DockingWindowDefaultLocationEventArgs.Target) property initialized to the docking window found in the same window group, if any.

1. Third, the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[WindowDefaultLocationRequested](xref:@ActiproUIRoot.Controls.Docking.DockSite.WindowDefaultLocationRequested) event is raised with the same event arguments.

1. Both of these events allow you to programmatically indicate the window should float (and optionally a specific floating location), or alternatively customize where the default location for the window will be.  You can indicate a dock target and optional side around which it should dock.  Passing `null` as the side will trigger an attach.

1. If a dock target isn't set by an event handler, the docking window will fall back to opening in a default side specified by the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).[DefaultDockSide](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.DefaultDockSide) property.  If an existing root level container is found at that side, the docking window will attach to that container.

## Programmatically Changing States and Docking

It's very easy to programmatically move document and tool windows to anywhere in the layout.

### State Change and Docking Members

The [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) class has a number of methods that make it easy to programmatically move a docking window to another state.

<table>
<thead>

<tr>
<th>Method</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[Float](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.Float*)

</td>
<td>

Moves the tool or document window into a floating dock host.

One overload of this method restores the docking window back to its default floating bounds, and others let you optionally specify location and size.

> [!NOTE]
> If you wish for the docking window to auto-size itself to its content when floated, update the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[SizeToContentModes](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.SizeToContentModes) property to contain the [SizeToContentModes](xref:@ActiproUIRoot.Controls.Docking.SizeToContentModes).[Floating](xref:@ActiproUIRoot.Controls.Docking.SizeToContentModes.Floating) flag.

This method will keep the docking window in its current [State](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.State), but it will be contained within a new floating dock host.

</td>
</tr>

<tr>
<td>

[MoveToMdi](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.MoveToMdi*)

</td>
<td>

Moves the tool or document window into the MDI area.  Note that [tabbed MDI](../workspace-mdi-features/tabbed-mdi.md) or [standard MDI](../workspace-mdi-features/standard-mdi.md) modes should be active before calling this method.

One overload of this method restores the docking window back to its default document location and the other lets you specify the target dock host.

This method changes the docking window into a [Document](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.Document) state.

</td>
</tr>

</tbody>
</table>

The [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) class defines additional methods that can be used to move a tool window to any state at either a default or target location.

<table>
<thead>

<tr>
<th>Method</th>
<th>Description</th>
</tr>

</thead>
<tbody>

<tr>
<td>

[Attach](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.Attach*)

</td>
<td>

Attaches the tool window to the specified dock target, which could be another open docking window, tabbed MDI container, or tool window container.

This method changes the tool window into the state defined by the dock target.

</td>
</tr>

<tr>
<td>

[AutoHide](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.AutoHide*)

</td>
<td>

Auto-hides the tool window to the outer-edge of the current dock host.

One overload of this method goes to a default dock side, and the other lets you specify the target dock side.

This method changes the tool window into an [AutoHide](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.AutoHide) state.

</td>
</tr>

<tr>
<td>

[Dock](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.Dock*)

</td>
<td>

Docks the tool window within the dock host hierarchy.

One overload of this method restores the tool window back to its default dock location and the other lets you specify a target and side against which to dock.

This method changes the tool window into a [Docked](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.Docked) state.

</td>
</tr>

</tbody>
</table>

The [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) class defines additional methods that can be used to move a document window to a target location.

| Method | Description |
|-----|-----|
| [Attach](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow.Attach*) | Attaches the document window to the specified dock target, which could be another tabbed document window or tabbed MDI container. |
| [Dock](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow.Dock*) | Docks the document window to the side of a specified target, which could be another tabbed document window or tabbed MDI container. |

### Example: Docking to the Top of a Target

In this example we will dock the `solutionExplorerToolWindow` to the top of the `propertiesToolWindow`.

@if (avalonia) {
```csharp
solutionExplorerToolWindow.Dock(propertiesToolWindow, Dock.Top);
```
}
@if (wpf) {
```csharp
solutionExplorerToolWindow.Dock(propertiesToolWindow, Side.Top);
```
}

### Example: Creating a Tabbed Group

In this example we will create a tabbed group of tool windows by "attaching" the `solutionExplorerToolWindow` to the `propertiesToolWindow`.

```csharp
solutionExplorerToolWindow.Attach(propertiesToolWindow);
```

### Example: Floating to a Location

In this example we will float the `solutionExplorerToolWindow` to a particular location.

```csharp
solutionExplorerToolWindow.Float(new Point(100, 100));
```

### Example: Floating and sizing to fit content

In this example we will float the `solutionExplorerToolWindow` to its previous location and have it size-to-content.

```csharp
solutionExplorerToolWindow.SizeToContentModes = SizeToContentModes.Floating;
solutionExplorerToolWindow.Float();
```

## Determining the Location of a Tool Window in Relation to the Workspace

The [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) class has a [GetCurrentSide](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.GetCurrentSide*) method that returns a @if (avalonia) { `Dock` side }@if (wpf) { [Side](xref:@ActiproUIRoot.Controls.Side) } specifying where the tool window currently is in relation to the workspace, if one is in use.

- If the tool window is open in a [Document](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.Document) state, `null` is returned.
- If the tool window is open in an [AutoHide](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.AutoHide) state, a @if (avalonia) { `Dock` side }@if (wpf) { [Side](xref:@ActiproUIRoot.Controls.Side) } indicating the auto-hide dock side is returned.
- If the tool window is open in a [Docked](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.Docked) state and a workspace is in use in the dock site (no tool window inner-fill), a @if (avalonia) { `Dock` side }@if (wpf) { [Side](xref:@ActiproUIRoot.Controls.Side) } indicating the location of the tool window relative to the workspace is returned.
- If the tool window is open in a [Docked](xref:@ActiproUIRoot.Controls.Docking.DockingWindowState.Docked) state and the containing dock host does not have a workspace (tool window inner-fill is active), a @if (avalonia) { `Dock` side }@if (wpf) { [Side](xref:@ActiproUIRoot.Controls.Side) } indicating the location of the tool window's midpoint relative to the dock host's bounds is returned.
- Otherwise, if the tool window is closed, `null` is returned.

## Programmatically Starting a Drag

It is possible to programmatically start a drag operation on a [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow) by calling the [DragMove](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.DragMove*) method.

The only requirements are that the [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) be registered with a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) and that the left pointer button be pressed when the method is called.  This means that the method can even be called on windows that are not currently selected, in the auto-hide state, or in a standard MDI host.  Also note that a @if (avalonia) { `PointerPressedEventArgs` }@if (wpf) { [InputPointerButtonEventArgs](xref:@ActiproUIRoot.Input.InputPointerButtonEventArgs) } must be passed so that the related pointer can be captured.

@if (avalonia) {
In this example we trigger a programmatic drag on a tool window in the `window` variable from within an event handler for its primary pointer button being down:

```csharp
private void OnControlPointerPressed(object sender, PointerPressedEventArgs e) {
	window.DragMove(e);
}
```
}
@if (wpf) {
In this example we trigger a programmatic drag on a tool window in the `window` variable from within an event handler for its primary pointer button being down:

```csharp
private void OnControlMouseDown(object sender, MouseButtonEventArgs e) {
	window.DragMove(new InputPointerButtonEventArgs(e, InputPointerButtonKind.Primary));
}
```
}

## Splitter Drag Kinds

Live splitting is enabled by default, meaning that when a splitter is dragged, the surrounding containers will immediately resize.  This feature can be turned off by setting the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[IsLiveSplittingEnabled](xref:@ActiproUIRoot.Controls.Docking.DockSite.IsLiveSplittingEnabled) property to `false`.  When `false`, highlights are dragged instead, and the containers only resize when the splitter is dropped.

While the live splitting feature is desirable for most scenarios, if your docking window content has very complex element trees that don't measure/arrange fast, turning off live splitting will improve performance when the end user uses splitters.
