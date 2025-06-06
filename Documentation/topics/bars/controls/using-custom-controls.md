---
title: "Using Custom Controls"
page-title: "Using Custom Controls - Bars Controls"
order: 205
---
# Using Custom Controls

The wide array of controls described in the [Bars Controls](index.md) documentation should cover nearly all of the control types needed for use in ribbons, toolbars, and menus.  In the event that the built-in controls don't fully meet your needs, custom controls can also be used.

@if (avalonia) {
An example is where you may wish to host some native editors, like `NumericUpDown` or `CalendarDatePicker`, within bars.
}
@if (wpf) {
An example is where you may wish to host some edit boxes from the [Actipro Editors](../../editors/index.md) product like [Int32EditBox](../../editors/editboxes/int32editbox.md) within bars.  Even though the edit box is an Actipro control, it wasn't made explicitly for use in bars and therefore is treated as a custom control for the purposes of this topic.
}

## Attached Properties

The [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService) class defines many attached properties that are reused across a number of bars controls.  These properties are commonly assigned to custom controls:

@if (avalonia) {
| Attached Property | Details |
|-----|-----|
| [KeyProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.KeyProperty) | The string key that identifies the control. |
| [HasExternalHeaderProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.HasExternalHeaderProperty) | Whether the custom control has an external header (image/label) that can render in certain scenarios. |
| [SmallIconProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.SmallIconProperty) | The small image for the control, which can show in an external header and in customization UI. |
| [LabelProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.LabelProperty) | The label for the control, which can show in an external header, in screen tips, and in customization UI. |
| [CanCloneToRibbonQuickAccessToolBarProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.CanCloneToRibbonQuickAccessToolBarProperty) | Whether the custom control can clone to the Ribbon [Quick Access ToolBar](../ribbon-features/quick-access-toolbar.md).  The [KeyProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.KeyProperty) must also be set if allowing cloning to the QAT. |
| [PanelSpacingSuggestionProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.PanelSpacingSuggestionProperty) | A [PanelSpacingSuggestion](xref:@ActiproUIRoot.Controls.Bars.PanelSpacingSuggestion) value that gives a hint on what kind of spacing should surround the custom control. |
}
@if (wpf) {
| Attached Property | Details |
|-----|-----|
| [KeyProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.KeyProperty) | The string key that identifies the control. |
| [HasExternalHeaderProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.HasExternalHeaderProperty) | Whether the custom control has an external header (image/label) that can render in certain scenarios. |
| [SmallImageSourceProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.SmallImageSourceProperty) | The small image for the control, which can show in an external header and in customization UI. |
| [LabelProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.LabelProperty) | The label for the control, which can show in an external header, in screen tips, and in customization UI. |
| [CanCloneToRibbonQuickAccessToolBarProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.CanCloneToRibbonQuickAccessToolBarProperty) | Whether the custom control can clone to the Ribbon [Quick Access ToolBar](../ribbon-features/quick-access-toolbar.md).  The [KeyProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.KeyProperty) must also be set if allowing cloning to the QAT. |
| [PanelSpacingSuggestionProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.PanelSpacingSuggestionProperty) | A [PanelSpacingSuggestion](xref:@ActiproUIRoot.Controls.Bars.PanelSpacingSuggestion) value that gives a hint on what kind of spacing should surround the custom control. |
}

## Usage Contexts

### RibbonGroup in a Classic Ribbon

If the custom control is hosted directly within a [RibbonGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonGroup), it will have a large height, roughly three times the height of small controls.

This kind of usage scenario is not typically recommended for custom controls except in special circumstances.

### RibbonControlGroup in a Classic Ribbon

If the custom control is hosted in a [RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) instead, it will use a small height if the control group is arranging children vertically.  It's best to stick to a maximum control height that matches a native `TextBox` control height.

[RibbonControlGroup](xref:@ActiproUIRoot.Controls.Bars.RibbonControlGroup) instances in this scenario will attempt to render the small image and/or label of any child custom controls that are encountered, if the attached [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[HasExternalHeaderProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.HasExternalHeaderProperty) property is set to `true`.  The attached @if (avalonia) { [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[SmallIconProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.SmallIconProperty) }@if (wpf) { [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[SmallImageSourceProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.SmallImageSourceProperty) } and [LabelProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.LabelProperty) set these UI values respectively.

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:Ribbon>
	<actipro:RibbonTabItem Key="Home">
		<actipro:RibbonGroup Key="Range">
			<actipro:RibbonControlGroup ItemVariantBehavior="AlwaysMedium">
				<NumericUpDown
					actipro:BarControlService.HasExternalHeader="True"
					actipro:BarControlService.Key="Minimum"
					actipro:BarControlService.Label="Minimum"
					actipro:BarControlService.PanelSpacingSuggestion="Both"
					FormatString="0"
					Value="{Binding MinimumValue, Mode=TwoWay}"
					/>
				...
			</actipro:RibbonControlGroup>
		</actipro:RibbonGroup>
		...
	</actipro:RibbonTabItem>
</actipro:StandaloneToolBar>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
xmlns:editors="http://schemas.actiprosoftware.com/winfx/xaml/editors"
...
<bars:Ribbon>
	<bars:RibbonTabItem Key="Home">
		<bars:RibbonGroup Key="Range">
			<bars:RibbonControlGroup ItemVariantBehavior="AlwaysMedium">
				<editors:Int32EditBox
					bars:BarControlService.HasExternalHeader="True"
					bars:BarControlService.Key="Minimum"
					bars:BarControlService.Label="Minimum"
					bars:BarControlService.PanelSpacingSuggestion="Both"
					Width="60" MinHeight="24" MaxHeight="30"
					UsageContext="ToolBar"
					themes:ThemeProperties.CornerRadius="3"
					Value="{Binding MinimumValue, Mode=TwoWay}"
					/>
				...
			</bars:RibbonControlGroup>
		</bars:RibbonGroup>
		...
	</bars:RibbonTabItem>
</bars:StandaloneToolBar>
```
}

### Simplified Ribbon

A custom control hosted within a Simplified ribbon will also use a small control height.

### Ribbon Quick Access Toolbar (QAT)

A custom control can be cloned to the [Quick Access Toolbar](../ribbon-features/quick-access-toolbar.md) if the attached [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[CanCloneToRibbonQuickAccessToolBarProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.CanCloneToRibbonQuickAccessToolBarProperty) is set to `true` on the custom control.  Be aware that a custom control cloned to the QAT will use a small control height.

### Toolbar

A custom control hosted within a toolbar will also use a small control height.

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:StandaloneToolBar>
	<NumericUpDown
		actipro:BarControlService.HasExternalHeader="True"
		actipro:BarControlService.Key="Minimum"
		actipro:BarControlService.Label="Minimum"
		actipro:BarControlService.PanelSpacingSuggestion="Both"
		FormatString="0"
		Value="{Binding MinimumValue, Mode=TwoWay}"
		/>
	...
</actipro:StandaloneToolBar>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
xmlns:editors="http://schemas.actiprosoftware.com/winfx/xaml/editors"
...
<bars:StandaloneToolBar>
	<editors:Int32EditBox
		bars:BarControlService.HasExternalHeader="True"
		bars:BarControlService.Key="Minimum"
		bars:BarControlService.Label="Minimum"
		bars:BarControlService.PanelSpacingSuggestion="Both"
		Width="60" MinHeight="24" MaxHeight="30"
		UsageContext="ToolBar"
		themes:ThemeProperties.CornerRadius="3"
		Value="{Binding MinimumValue, Mode=TwoWay}"
		/>
	...
</bars:StandaloneToolBar>
```
}

### Menu

Custom controls don't always work well in menu contexts, but a special @if (avalonia) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarMenuControlWrapper) }@if (wpf) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.BarMenuControlWrapper) } control is available to render an external label for a custom control and align everything with other neighboring menu items.  This is great for controls like an edit box that don't render a label within their own template.

Bars menu controls will automatically wrap child controls that aren't flagged as menu controls with a @if (avalonia) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarMenuControlWrapper) }@if (wpf) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.BarMenuControlWrapper) } instance.  A control can be flagged as a menu control by setting the [BarControlService](xref:@ActiproUIRoot.Controls.Bars.BarControlService).[IsMenuControlProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.IsMenuControlProperty) attached property set to `true`.

The @if (avalonia) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarMenuControlWrapper) }@if (wpf) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.BarMenuControlWrapper) } control inherits native `MenuItem` but has a template that tailors it for hosting other custom controls.  The custom control should be placed in the wrapper's @if (avalonia) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarMenuControlWrapper) }@if (wpf) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.BarMenuControlWrapper) }.`Header` property for proper display, which happens automatically if the custom control is within a Bars menu control.

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:BarMenuFlyout>
	<NumericUpDown
		actipro:BarControlService.HasExternalHeader="True"
		actipro:BarControlService.Key="Minimum"
		actipro:BarControlService.Label="Minimum"
		actipro:BarControlService.PanelSpacingSuggestion="Both"
		actipro:BarControlService.SmallIcon="{StaticResource MinimumIcon}"
		FormatString="0"
		Value="{Binding MinimumValue, Mode=TwoWay}"
		/>
	...
</actipro:BarMenuFlyout>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
xmlns:editors="http://schemas.actiprosoftware.com/winfx/xaml/editors"
...
<bars:BarContextMenu>
	<editors:Int32EditBox
		bars:BarControlService.HasExternalHeader="True"
		bars:BarControlService.Key="Minimum"
		bars:BarControlService.Label="Minimum"
		bars:BarControlService.PanelSpacingSuggestion="Both"
		bars:BarControlService.SmallImageSource="/Images/Minimum16.png"
		Width="60" MinHeight="24" MaxHeight="30"
		UsageContext="Menu"
		themes:ThemeProperties.CornerRadius="3"
		Value="{Binding MinimumValue, Mode=TwoWay}"
		/>
	...
</bars:BarContextMenu>
```
}

When a label and/or small image are applied to a custom control with the attached [LabelProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.LabelProperty) and @if (avalonia) { [SmallIconProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.SmallIconProperty) }@if (wpf) { [SmallImageSourceProperty](xref:@ActiproUIRoot.Controls.Bars.BarControlService.SmallImageSourceProperty) } properties, they will be displayed in the @if (avalonia) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.Primitives.BarMenuControlWrapper) }@if (wpf) { [BarMenuControlWrapper](xref:@ActiproUIRoot.Controls.Bars.BarMenuControlWrapper) } alongside the custom control itself.  The small image will align in the icon column with other menu items.

## Screen Tips

The [ScreenTip](xref:@ActiproUIRoot.Controls.Bars.ScreenTip) class inherits the native `ToolTip` control and therefore can be used anywhere a normal tooltip can, including on custom controls.

@if (avalonia) {
```xaml
xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui"
...
<actipro:StandaloneToolBar>
	<NumericUpDown ... >
		<ToolTip.Tip>
			<actipro:ScreenTip Header="Minimum" Content="Specifies the range's maximum value." />
		</ToolTip.Tip>
	</NumericUpDown>
	...
</actipro:StandaloneToolBar>
```
}
@if (wpf) {
```xaml
xmlns:bars="http://schemas.actiprosoftware.com/winfx/xaml/bars"
xmlns:editors="http://schemas.actiprosoftware.com/winfx/xaml/editors"
...
<bars:StandaloneToolBar>
	<editors:Int32EditBox ... >
		<editors:Int32EditBox.ToolTip>
			<bars:ScreenTip Header="Minimum" Content="Specifies the range's minimum value." />
		</editors:Int32EditBox.ToolTip>
	</editors:Int32EditBox>
	...
</bars:StandaloneToolBar>
```
}

See the [Screen Tips](../ribbon-features/screen-tips.md) topic for more information on screen tips.
