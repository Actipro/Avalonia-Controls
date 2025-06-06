---
title: "Programmatic Layout"
page-title: "Programmatic Layout - Docking & MDI Layout, Globalization, and Accessibility Features"
order: 8
---
# Programmatic Layout

This topic covers several methods for programmatically arranging and resizing the docking windows.

## Resizing Split Container Slots

The [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) control is used to arrange the various docking elements vertically or horizontally, while also allowing dynamic resizing.  The child elements of the [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) are arranged into slots, which allows the elements to be sized proportionally as the [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) is sized.

The [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer).[ResizeSlots](xref:@ActiproUIRoot.Controls.Docking.SplitContainer.ResizeSlots*) method can be used to resize the associated slots, and therefore the underlying docking elements.  The `ResizeSlots` method accepts zero or more ratios, represented by a `double` array.  The ratios are used to calculate new sizes for the associated slots.

For example, this code shows a `SplitContainer` with two child elements, therefore the `SplitContainer` has two slots:

@if (avalonia) {
```xaml
...
<actipro:SplitContainer Orientation="Horizontal">
	<actipro:TabbedMdiContainer>
		<actipro:DocumentWindow Title="Upper-1">
			<TextBox TextWrapping="Wrap" Text="A document window in the upper-left corner of the tabbed MDI area." />
		</actipro:DocumentWindow>
	</actipro:TabbedMdiContainer>
	<actipro:TabbedMdiContainer>
		<actipro:DocumentWindow Title="Upper-2">
			<TextBox TextWrapping="Wrap" Text="A document window in the upper-right corner of the tabbed MDI area." />
		</actipro:DocumentWindow>
	</actipro:TabbedMdiContainer>
</actipro:SplitContainer>
...
```
}
@if (wpf) {
```xaml
...
<docking:SplitContainer Orientation="Horizontal">
	<docking:TabbedMdiContainer>
		<docking:DocumentWindow Title="Upper-1">
			<TextBox TextWrapping="Wrap" Text="A document window in the upper-left corner of the tabbed MDI area." />
		</docking:DocumentWindow>
	</docking:TabbedMdiContainer>
	<docking:TabbedMdiContainer>
		<docking:DocumentWindow Title="Upper-2">
			<TextBox TextWrapping="Wrap" Text="A document window in the upper-right corner of the tabbed MDI area." />
		</docking:DocumentWindow>
	</docking:TabbedMdiContainer>
</docking:SplitContainer>
...
```
}

This code can be used to evenly distribute the available space between the two elements in the above example:

```csharp
splitContainer.ResizeSlots();
```

This code can be used to give the first element twice as much space as the second element:

```csharp
splitContainer.ResizeSlots(2, 1);
```

The only restriction on the ratios passed is that they are greater than `0`.  If there are more ratios than there are slots, then the extra ratios will be ignored.  If there are more slots than there are ratios, then the last ratio will be repeated for the remaining slots.  As a result, passing a single ratio is equivalent to not passing any ratios, since all the slots will share the same ratio.

There is no way to indicate that one slot should use star (`*`) sizing.  Instead, take into account the current size of the split container and adjust the ratios passed to the [ResizeSlots](xref:@ActiproUIRoot.Controls.Docking.SplitContainer.ResizeSlots*) method to get child controls into their desired sizes.

The above code shows how to call the [ResizeSlots](xref:@ActiproUIRoot.Controls.Docking.SplitContainer.ResizeSlots*) method, but does not illustrate how the reference to the [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) was obtained.

> [!WARNING]
> You should never use XAML `x:Name` references to obtain a specific `SplitContainer`, as these elements are created and destroyed dynamically as changes are made to the docking layout.

@if (wpf) {

The `VisualTreeHelper` and [VisualTreeHelperExtended](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended) can be used to locate the various [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer) elements currently used. [VisualTreeHelperExtended](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended) offers several helper methods for walking up and down the visual tree.

See the [Media](../../shared/windows-media.md) topic for more information on [VisualTreeHelperExtended](xref:@ActiproUIRoot.Media.VisualTreeHelperExtended).

}

## Reversing Split Container Slots

A [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer).[ReverseSlots](xref:@ActiproUIRoot.Controls.Docking.SplitContainer.ReverseSlots*) helper method is available that reverses the order of all the children in the container.

This code reverses the order of the [SplitContainer](xref:@ActiproUIRoot.Controls.Docking.SplitContainer)'s children:

```csharp
splitContainer.ReverseSlots();
```

## Arranging Tabbed MDI Windows

See the [Tabbed MDI](../workspace-mdi-features/tabbed-mdi.md) topic for more information on arranging tabbed MDI windows.

## Arranging Standard MDI Windows

See the [Standard MDI](../workspace-mdi-features/standard-mdi.md) topic for more information on arranging standard MDI windows.

## Setting Docking Window Initial Sizes

See the [Lifecycle and Docking Management](../docking-window-features/lifecycle-and-docking-management.md) topic for more information on setting a different default initial size for docking windows.

## Setting Docking Window Default Locations

See the [Lifecycle and Docking Management](../docking-window-features/lifecycle-and-docking-management.md) topic for more information on setting a different default location for docking windows.
