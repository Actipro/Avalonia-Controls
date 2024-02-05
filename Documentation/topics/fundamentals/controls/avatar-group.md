---
title: "Avatar Group"
page-title: "Avatar Group - Fundamentals Controls"
order: 6
---
# Avatar Group

Use an [AvatarGroup](xref:@ActiproUIRoot.Controls.AvatarGroup) to render multiple [Avatar](avatar.md) controls.

![Screenshot](../images/avatar-group.png)

*AvatarGroup with image-based avatars and some items overflowed*

## Presentation

Use the [ItemLength](xref:@ActiproUIRoot.Controls.AvatarGroup.ItemLength) property to define the width and height of each [Avatar](avatar.md) in the group.

By default, each avatar will slightly overlap with the avatar that appears before it.  Use the [OverlapPercentage](xref:@ActiproUIRoot.Controls.AvatarGroup.OverlapPercentage) property to define the extent of the overlap.  The default value is `0.2` for a 20% overlap.

## Overflow

Individual [Avatar](avatar.md) controls can optionally be overflowed when there is not enough room to display them all inline.  When overflow is necessary, a button is added at the end of the group that, when clicked, will display the additional items in a popup.

Overflow is set to [Popup](xref:@ActiproUIRoot.Controls.AvatarGroupOverflowKind.Popup) by default. Set the [OverflowKind](xref:@ActiproUIRoot.Controls.AvatarGroup.OverflowKind) property to [None](xref:@ActiproUIRoot.Controls.AvatarGroupOverflowKind.None) to disable overflow behavior.

By default, the group will display as many avatars as possible in the space available, but the [MaxInlineCount](xref:@ActiproUIRoot.Controls.AvatarGroup.MaxInlineCount) property can be set to limit how many are displayed before overflowing.

The overflow button indicates the number of overflowed items.  Use the [OverflowStringFormat](xref:@ActiproUIRoot.Controls.AvatarGroup.OverflowStringFormat) property to customize how the count is formatted. The default is `"+{0}"`, where `{0}` is the placeholder for the current value of [OverflowedItemCount](xref:@ActiproUIRoot.Controls.AvatarGroup.OverflowedItemCount) (e.g., `"+9"`).

## Configuring Avatar Items

The [AvatarGroup](xref:@ActiproUIRoot.Controls.AvatarGroup) is an `ItemsControl` for [Avatar](avatar.md) controls.

Individual instances of [Avatar](avatar.md) can be directly defined as the `ItemsSource` as shown below:

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<actipro:AvatarGroup ... >
	<actipro:Avatar Description="Han Solo" />
	<actipro:Avatar Description="Leia Organa" />
	<actipro:Avatar Description="Luke Skywalker" />
	...
</actipro:AvatarGroup>
```
}
@if (wpf) {
```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
...

<shared:AvatarGroup ... >
	<shared:Avatar Description="Han Solo" />
	<shared:Avatar Description="Leia Organa" />
	<shared:Avatar Description="Luke Skywalker" />
	...
</shared:AvatarGroup>
```
}

The `ItemsSource` can also be defined as non-[Avatar](avatar.md) items.  In this scenario, each item is automatically wrapped in an [Avatar](avatar.md) container with the original item as the `DataContext`.  Each [Avatar](avatar.md) instance will need to be configured to bind its properties to the underlying `DataContext`.  This can be done using an @if(avalonia) { `ItemContainerTheme` }@if (wpf) { `ItemContainerStyle` } as shown in the following example where `ItemsSource` is bound to a collection of view models:


@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<actipro:AvatarGroup ItemsSource="{Binding SomeUserCollection}" ... >
	<actipro:AvatarGroup.ItemContainerTheme>
		<ControlTheme TargetType="actipro:Avatar" BasedOn="{StaticResource {x:Type actipro:Avatar}}" x:DataType="someNamespace:SomeUser">
			<Setter Property="Description" Value="{Binding UserName}" />
			<Setter Property="Content" Value="{Binding ProfileImage}" />
		</ControlTheme>
	</actipro:AvatarGroup.ItemContainerTheme>
</actipro:AvatarGroup>
```
}
@if (wpf) {
```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
...

<shared:AvatarGroup ItemsSource="{Binding SomeUserCollection}" ... >
	<shared:AvatarGroup.ItemContainerStyle>
		<Style TargetType="shared:Avatar" BasedOn="{StaticResource {x:Type shared:Avatar}}">
			<Setter Property="Description" Value="{Binding UserName}" />
			<Setter Property="Content" Value="{Binding ProfileImage}" />
		</Style>
	</shared:AvatarGroup.ItemContainerTheme>
</shared:AvatarGroup>
```
}

## Working with Large Datasets

The built-in overflow behavior of [AvatarGroup](xref:@ActiproUIRoot.Controls.AvatarGroup) is not designed for use with large data sets where the number of overflowed items cannot be reasonably displayed in a popup.

For example, consider a scenario where an open source project wants to display the avatars of contributing users. For some projects, that could be thousands of users that would never display properly in a popup!

One option is to limit the number of items included in the dataset. For example, instead of listing all contributors to the  open source project, the dataset could be limited to the top 20 contributors.

Another option is to configure the [AvatarGroup](xref:@ActiproUIRoot.Controls.AvatarGroup) to disable built-in overflow support and present an alternate UI.  The following example disables overflow and places a custom link next to the control which could be used to display a more appropriate view of all the contributors.

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...

<StackPanel Orientation="Horizontal">
	<actipro:AvatarGroup OverflowKind="None" ItemsSource="{Binding Contributors}">
		...
	</actipro:AvatarGroup>
	<actipro:HyperlinkTextBlock Text="+ 58 contributors" Command="{ShowContributorsCommand}"/>
</StackPanel>
```
}
@if (wpf) {
```xaml
xmlns:shared="http://schemas.actiprosoftware.com/winfx/xaml/shared"
...

<StackPanel Orientation="Horizontal">
	<shared:AvatarGroup OverflowKind="None" ItemsSource="{Binding Contributors}">
		...
	</shared:AvatarGroup>
	<TextBlock><Hyperlink ="{ShowContributorsCommand}">+ 58 contributors</Hyperlink></TextBlock>
</StackPanel>
```
}