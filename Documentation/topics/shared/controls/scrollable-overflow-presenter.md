---
title: "ScrollableOverflowPresenter"
page-title: "ScrollableOverflowPresenter - Shared Library Controls"
order: 35
---
# ScrollableOverflowPresenter

The [ScrollableOverflowPresenter](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter) control displays scroll buttons when its content overflows the available space.  Tap the buttons to scroll through the content or hold down a button to scroll quickly.

![Screenshot](../images/scrollableoverflowpresenter.png)

No scroll buttons will be visible when the child control fits completely within the viewport bounds of the presenter along its primary axis, as set by the [Orientation](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.Orientation) property.

> [!IMPORTANT]
> See the [Getting Started](../getting-started.md) topic for details on configuring themes for this control.

## Important Members

The [ScrollableOverflowPresenter](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter) class has these important members:

| Member | Description |
|-----|-----|
| [Child](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.Child) Property | The child control to render within the presenter.  Scroll buttons display when the child's extent (size along the primary axis) extends beyond the available space. |
| [Orientation](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.Orientation) Property | An `Orientation` value that governs the primary axis along which scrolling may occur. The default value is `Horizontal`. |
| [ScrollButtonTheme](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.ScrollButtonTheme) Property | The `ControlTheme` to apply to scroll buttons.  See the "Scroll Button Theme" topic below for more details.  |

## Example

The following example demonstrates how to define a [ScrollableOverflowPresenter](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter) that contains an `ItemsControl` containing color swatches:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:ScrollableOverflowPresenter Orientation="Horizontal">
	<ItemsControl ItemsSource="{actipro:DelimitedArray '#ffb900, #e74856, #0078d7, #0099bc, #7a7574, #767676, #ff8d00, #e81123, #0063b1, #2d7d9a, #5d5a58, #4c4a48, #f7630c, #ea005e, #8e8cd8, #00b7c3, #68768a, #69797e, #ca5010, #c30052, #6b69d6, #038387, #515c6b, #4a5459, #da3b01, #e3008c, #8764b8, #00b294, #567c73, #647c64, #ef6950, #bf0077, #744da9, #018574, #486860, #525e54, #d13438, #c239b3, #b146c2, #00cc6a, #498205, #847545, #ff4343, #9a0089, #881798, #10893e, #107c10, #7e735f', Type=SolidColorBrush}">
		<ItemsControl.ItemsPanel>
			<ItemsPanelTemplate>
				<StackPanel Margin="5" Orientation="Horizontal" Spacing="5" />
			</ItemsPanelTemplate>
		</ItemsControl.ItemsPanel>
		<ItemsControl.ItemTemplate>
			<DataTemplate DataType="SolidColorBrush">
				<Border Width="100" Height="100" CornerRadius="5"
						Background="{Binding}"
						BorderBrush="{actipro:ThemeResource Container3BorderBrush}" BorderThickness="1" />
			</DataTemplate>
		</ItemsControl.ItemTemplate>
	</ItemsControl>
</actipro:ScrollableOverflowPresenter>
```

## Scroll Button Theme

When overflowed, **Scroll Buttons** appear on the edges of the control to allow scrolling in that direction.  The appearance of these buttons can be customized by setting the [ScrollButtonTheme](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.ScrollButtonTheme) property to a custom `ControlTheme`.

Horizonal orientations set the `.horizontal` style class on both buttons, and the `.left` and `.right` style class appropriately on the two scroll buttons.  Vertical orientations set the `.vertical` style class on both buttons, and the `.up` and `.down` style class appropriately on the two scroll buttons.

The following sample demonstrates how to define a custom theme for **Scroll Buttons**:

```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:ScrollableOverflowPresenter>

	<actipro:ScrollableOverflowPresenter.ScrollButtonTheme>
		<ControlTheme TargetType="RepeatButton" BasedOn="{actipro:ControlTheme ScrollBarLineButton}">

			<!-- Common properties -->
			<Setter Property="BorderBrush" Value="{actipro:ThemeResource Container3BorderBrush}" />
			<Setter Property="CornerRadius" Value="0" />

			<!-- Background and Foreground (must be based on orientation, even if the same) -->
			<Style Selector="^.horizontal, ^.vertical">
				<Setter Property="Background" Value="{actipro:ThemeResource Container1BackgroundBrush}" />
				<Setter Property="Foreground" Value="{actipro:ThemeResource ButtonForegroundBrush}" />

				<Style Selector="^:pointerover">
					<Setter Property="Background" Value="{actipro:ThemeResource Container3BackgroundBrush}" />
					<Setter Property="Foreground" Value="{actipro:ThemeResource ButtonForegroundBrush}" />
				</Style>
				<Style Selector="^:pressed">
					<Setter Property="Background" Value="{actipro:ThemeResource Container4BackgroundBrush}" />
					<Setter Property="Foreground" Value="{actipro:ThemeResource ButtonForegroundBrush}" />
				</Style>
				<Style Selector="^:disabled">
					<Setter Property="Background" Value="{actipro:ThemeResource Container1BackgroundBrush}" />
					<Setter Property="Foreground" Value="{actipro:ThemeResource ButtonForegroundBrushDisabled}" />
				</Style>
			</Style>

			<!-- Apply 1px border on the inside edge of the button (based on scroll direction) -->
			<Style Selector="^.right">
				<Setter Property="BorderThickness" Value="1,0,0,0" />
			</Style>
			<Style Selector="^.left">
				<Setter Property="BorderThickness" Value="0,0,1,0" />
			</Style>
			<Style Selector="^.up">
				<Setter Property="BorderThickness" Value="0,0,0,1" />
			</Style>
			<Style Selector="^.down">
				<Setter Property="BorderThickness" Value="0,1,0,0" />
			</Style>

		</ControlTheme>
	</actipro:ScrollableOverflowPresenter.ScrollButtonTheme>

	...

</actipro:ScrollableOverflowPresenter>
```

> [!IMPORTANT]
> Due to style priority and the fact that the sample above is based on an existing `ScrollBarLineButton` theme, changes to `Background` and `Foreground` must be applied within a selector for `.horizontal` or `.vertical` just like the base style.  Similarly, changes to `ContentTemplate` must be applied within a selector for `.right`, `.left`, `.up`, or `.down`, which correspond to the scroll direction of the button.


## Advanced Usage

Out of the box, the presenter is configured to work like a `ScrollViewer` where it measures its [Child](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.Child) with infinite size and displays scroll buttons when necessary to support scrolling.

There are more advanced usage scenarios where the [Child](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.Child) may have scaling features of its own and may need to be measured with actual available size instead of infinite size.

As an example, consider a specialized tab control where the tabs can shrink in width from their ideal desired width to ensure they fit within the available width.  However, they may also have a minimum width constraint.  The available width could reach a point where it can't hold all the tabs, even at their minimum widths.

The [ScrollableOverflowPresenter](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter) control can handle this scenario by setting its [UseInfiniteConstraint](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.UseInfiniteConstraint) property to `false`.  This will cause the presenter to measure the [Child](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.Child) with actual available size.  For scroll support to function properly in this mode, the presenter needs to be told by the [Child](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.Child) control or one of its descendants what its extent (size along the primary axis) is, since that value is not obtainable via the UI framework.  The presenter needs to compare that extent with the viewport extent, so it knows when overflow occurs.  This child extent value can be set to the [ChildExtent](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.ChildExtent) property.

Revisiting the specialized tab control example above, the tab control's `ItemsPresenter` in its template should be wrapped by a [ScrollableOverflowPresenter](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter) control with [UseInfiniteConstraint](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.UseInfiniteConstraint) set to `false`.  Then the tab control's custom panel's `ArrangeOverride` method should determine the total extent of the tabs, use a visual tree search upward to get the [ScrollableOverflowPresenter](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter) control, and assign its [ChildExtent](xref:@ActiproUIRoot.Controls.ScrollableOverflowPresenter.ChildExtent) property to that extent.

This allows the tab control to:
- Display the tabs at ideal width when there is plenty of space.
- Start shrinking the tabs to fit in available space as the available space decreases.
- Display overflow scroll buttons when the tabs at minimum width can no longer fit in available space.