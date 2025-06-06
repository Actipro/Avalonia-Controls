---
title: "Troubleshooting"
page-title: "Troubleshooting - Docking & MDI Reference"
order: 40
---
# Troubleshooting

This topic contains troubleshooting data specific to the Docking & MDI product.

> [!NOTE]
> For some more troubleshooting information that relates both to this product as well as other Actipro @@PlatformName products, please see the more general [TroubleShooting](../troubleshooting.md) topic.

## Document/Tool Window Content Sizing Is Incorrect

A common mistake is to set the `Width`, `Height`, `MinWidth`, `MinHeight`, `MaxWidth`, or `MaxHeight` properties of a docking window ([ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) or [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow)) instance.  Since the docking elements need to dynamically change their size based on their location, these properties should never be set by you.

Instead, you can set the [DockingWindow](xref:@ActiproUIRoot.Controls.Docking.DockingWindow).[ContainerDockedSize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ContainerDockedSize), [ContainerMinSize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ContainerMinSize), [ContainerMaxSize](xref:@ActiproUIRoot.Controls.Docking.DockingWindow.ContainerMaxSize), and [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).[ContainerAutoHideSize](xref:@ActiproUIRoot.Controls.Docking.ToolWindow.ContainerAutoHideSize) properties as appropriate for your needs.

See the [Lifecycle and Docking Management](docking-window-features/lifecycle-and-docking-management.md) topic for details on using these properties.

## Programmatic Layout Updates Don't Work Until Loaded

Sometimes docking windows are programmatically added to a layout (via opening, docking, layout loading, etc.) within the constructor of a root element like a `Window` or `UserControl` after its `InitializeComponent` method executes.  In situations like this, the code to add the docking windows to the layout needs to be performed at or after the time that root element's `Loaded` event is raised.

When the root element has been loaded, that means that all the templates of the control hierarchy within the dock site have been properly loaded and applied as well.  The dock site and its features are then ready for programmatic interaction.

## Document/ToolWindow DataContext Cleared on Layout Changes

In many common app design scenarios, you set a data context on a root container in your element hierarchy.  Naturally you expect it to inherit down the tree of elements and into the [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow).`DataContext` and [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow).`DataContext` properties.

While that may work on the initial load, the problem is that as soon as the layout changes, such as when an end user drags a tool window to float, the `DataContext` will clear.  This is because the tool window has now moved to a new root container, and the data context is no longer inheriting down.

> [!TIP]
> This issue is not present when using our [MVVM Features](mvvm-features.md) since in that case, the `DataContext` of each docking window becomes the item you bound in the [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ToolItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.ToolItemsSource) or [DocumentItemsSource](xref:@ActiproUIRoot.Controls.Docking.DockSite.DocumentItemsSource) properties.

If the content of your docking windows will rely on a data context that normally would be inherited down, you should explicitly bind your docking window's `DataContext` property to the dock site's `DataContext` like this:

@if (avalonia) {
```xaml
<actipro:DockSite x:Name="dockSite">
	<actipro:SplitContainer>
		<actipro:Workspace />
		<actipro:ToolWindowContainer>
			<actipro:ToolWindow Title="DataContext Test" DataContext="{Binding #dockSite.DataContext}">
				<TextBlock Text="{Binding}" />
			</actipro:ToolWindow>
		</actipro:ToolWindowContainer>
	</actipro:SplitContainer>
</actipro:DockSite>
```
}
@if (wpf) {
```xaml
<docking:DockSite x:Name="dockSite">
	<docking:SplitContainer>
		<docking:Workspace />
		<docking:ToolWindowContainer>
			<docking:ToolWindow Title="DataContext Test" DataContext="{Binding ElementName=dockSite, Path=DataContext}">
				<TextBlock Text="{Binding}" />
			</docking:ToolWindow>
		</docking:ToolWindowContainer>
	</docking:SplitContainer>
</docking:DockSite>
```
}

By doing that, you ensure that the docking window data contexts will still match the dock site's data context, even when any layout changes occur, such as floating windows.

The same concept applies if you have a [Workspace](workspace-mdi-features/workspace.md) that has direct content (no MDI) and that content relies on an inherited data context.  Changing the docking window layout around the workspace will briefly cause the workspace to be removed from and added back into the layout.  Both are actions that temporarily change the inherited data context.  By using a binding as above on the workspace's data context, that can be prevented.

@if (wpf) {

## Active Window is Incorrect with Interop Controls

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[ActiveWindow](xref:@ActiproUIRoot.Controls.Docking.DockSite.ActiveWindow) property might not be correct when clicking into interop (WinForms, ActiveX, etc.) controls.  This is due to issues WPF has with tracking focus that enters `HwndHost` controls, and then properly reporting it back in WPF events.

We include a helper that is generally able to work around the focus tracking issues.  See the [Interop Compatibility](interop-compatibility.md) topic for details.

}

## Implicit Style/DataTemplate Is Not Always Applied

The [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite) leverages one or more `Window` instances that contain a [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost) for floating document and/or tool windows when [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[UseHostedFloatingWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.UseHostedFloatingWindows) is `false` (the default).  Therefore, any implicit @if (avalonia) { `ControlTheme` or `IDataTemplate` }@if (wpf) { `Style` or `DataTemplate`} definitions must be defined in the application resources, so they will be available to the main window and any floating windows.

Alternatively, the implicit @if (avalonia) { `ControlTheme` or `IDataTemplate` }@if (wpf) { `Style` or `DataTemplate`} definitions can be redefined/merged in the resources of a floating [DockHost](xref:@ActiproUIRoot.Controls.Docking.DockHost).  This can be accomplished in a [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[FloatingWindowOpening](xref:@ActiproUIRoot.Controls.Docking.DockSite.FloatingWindowOpening) event handler.
