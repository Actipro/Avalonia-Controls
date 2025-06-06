---
title: "Ribbon Window"
page-title: "Ribbon Window - Ribbon Features"
order: 5
---
# Ribbon Window

Bars includes a [RibbonWindow](xref:@ActiproUIRoot.Controls.Bars.RibbonWindow) class, which is an implementation of the `Window` class specifically designed to simplify hosting a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control.

It is *always* recommended that a [RibbonWindow](xref:@ActiproUIRoot.Controls.Bars.RibbonWindow) is only used when hosting a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control implementation.

While a [RibbonWindow](xref:@ActiproUIRoot.Controls.Bars.RibbonWindow) can be used without a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon) control at all, the control template includes additional UI elements specific to hosting a [Ribbon](xref:@ActiproUIRoot.Controls.Bars.Ribbon).  The following example demonstrates a minimal configuration:

```xaml
<actipro:RibbonWindow ...
	xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui">

	<actipro:RibbonContainerPanel>

		<!-- Define the ribbon here -->
		<actipro:Ribbon ... >

			<!-- Additional ribbon configuration here -->

		</actipro:Ribbon>

		<!-- Define other window content here, such as a document display area -->

	</actipro:RibbonContainerPanel>

</actipro:RibbonWindow>
```

## ChromedTitleBar Usage

Each [RibbonWindow](xref:@ActiproUIRoot.Controls.Bars.RibbonWindow) has a default [WindowTitleBar](../../shared/controls/chromed-title-bar.md) as part of the control template whose [LeftContent](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.LeftContent) is configured to host the ribbon [Quick Access Toolbar](quick-access-toolbar.md) when it is shown above the ribbon.

### Customizing the Title Bar

The title bar can be customized by applying a `ControlTheme` to the [TitleBarTheme](xref:@ActiproUIRoot.Controls.Bars.RibbonWindow.TitleBarTheme) property.

> [!IMPORTANT]
> Any `ControlTheme` must be based on the default `ControlTheme` or the [LeftContent](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.LeftContent) will no longer be configured to host the [Quick Access Toolbar](quick-access-toolbar.md).

The following example demonstrates using a `ControlTheme` to allow the **Full Screen** caption button and place additional content in the [RightContent](xref:@ActiproUIRoot.Controls.Primitives.ChromedTitleBar.RightContent).

```xaml
<actipro:RibbonWindow ...
	xmlns:actipro="http://schemas.actiprosoftware.com/avaloniaui">

	<actipro:RibbonWindow.TitleBarTheme>
		<ControlTheme TargetType="actipro:WindowTitleBar" BasedOn="{actipro:ControlTheme RibbonWindowTitleBar}">
			<Setter Property="IsFullScreenButtonAllowed" Value="True" />
			<Setter Property="RightContentTemplate">
				<DataTemplate>
					<TextBlock VerticalAlignment="Center">Right content here</TextBlock>
				</DataTemplate>
			</Setter>
		</ControlTheme>
	</actipro:RibbonWindow.TitleBarTheme>

	...

</actipro:RibbonWindow>
```

See the [ChromedTitleBar](../../shared/controls/chromed-title-bar.md) topic for additional information on configuring the title bar.