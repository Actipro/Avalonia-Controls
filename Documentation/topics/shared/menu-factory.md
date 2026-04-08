---
title: "Menu Factory"
page-title: "Menu Factory - Shared Library Reference"
order: 12
---
# Menu Factory

Several Actipro products include built-in contextual menus.  These menus are primarily based on the native menu controls of the @@PlatformName platform, and some controls expose events where these menus can be customized and/or cancelled.

While this approach is effective, it can be burdensome to identify and respond to all the appropriate events just to customize a menu.  This is especially true if the @@PlatformName native menu controls need to be replaced by alternate menus (like those used by an application's main menu) since end-users expect all menus in an application to be consistent.

## Replacing the Default Menus

All Actipro products will create built-in contextual menus using the menu factory currently assigned to [MenuFactory](xref:@ActiproUIRoot.Controls.MenuFactory).[Current](xref:@ActiproUIRoot.Controls.MenuFactory.Current).  By default, this value is initialized to a factory object that creates @@PlatformName native menu controls.  This default instance is always available from the [MenuFactory](xref:@ActiproUIRoot.Controls.MenuFactory).[Default](xref:@ActiproUIRoot.Controls.MenuFactory.Default) property.

To change the factory object used by Actipro controls, set the [MenuFactory](xref:@ActiproUIRoot.Controls.MenuFactory).[Current](xref:@ActiproUIRoot.Controls.MenuFactory.Current) property to an instance of any class that implements the [IMenuFactory](xref:@ActiproUIRoot.Controls.IMenuFactory) interface.

### Integrating with Actipro Bars

Actipro offers a [Bars](../bars/index.md) product, so we have made it easy to configure all Actipro products to use our own product.  To change from the @@PlatformName native menus to @if (avalonia or wpf) { [Bars Context Menus](../bars/menu-features/context-menus.md), }@if (winforms) { [Bars Popup Menus](../bars/controls/popup-menus.md), } simply set [MenuFactory](xref:@ActiproUIRoot.Controls.MenuFactory).[Current](xref:@ActiproUIRoot.Controls.MenuFactory.Current) to a new instance of [BarsMenuFactory](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory).

@if(avalonia) {
```csharp
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
...

public partial class App : Application {

	public override void Initialize() {
		// Configure BarsMenuFactory before initializing UI controls
		MenuFactory.Current = new BarsMenuFactory();

		...
		AvaloniaXamlLoader.Load(this);
	}
	...
}
```
}
@if (winforms) {
```csharp
using ActiproSoftware.UI.WinForms.Controls;
using ActiproSoftware.UI.WinForms.Controls.Bars;
...

public partial class MainForm : Form {
	private BarManager _barManager;

	public MainForm() {
		InitializeComponent();

		// Configure BarsMenuFactory using BarManager hosted on the form
		MenuFactory.Current = new BarsMenuFactory(_barManager);

		...
	}
	...
}
```
}
@if (wpf) {
```csharp
using ActiproSoftware.Windows.Controls;
using ActiproSoftware.Windows.Controls.Bars;
...

public partial class App : Application {

	protected override void OnStartup(StartupEventArgs e) {
		// Configure BarsMenuFactory before initializing UI controls
		MenuFactory.Current = new BarsMenuFactory();

		...
		base.OnStartup(e);
	}
	...
}
```
}

### Integrating with 3rd Party Controls

@if (avalonia or wpf) {
Any implementation of [IMenuFactory](xref:@ActiproUIRoot.Controls.IMenuFactory) requires the base control types to derive from the @@PlatformName native menu controls:
}
@if (winforms) {
Any implementation of [IMenuFactory](xref:@ActiproUIRoot.Controls.IMenuFactory) requires the base control types to implement the following interfaces:
}
@if (avalonia) {
- `Avalonia.Controls.MenuFlyout` - The base type of contextual menus.
- `Avalonia.Controls.MenuItem` - The base type of items within a menu.
- `Avalonia.Controls.Separator` - The base type of separators between menu items.
}
@if (winforms) {
- [IMenu](xref:@ActiproUIRoot.Controls.IMenu) - Defines the base requirements for a contextual menu control.
- [IMenuItem](xref:@ActiproUIRoot.Controls.IMenuItem) - Defines the base requirements for items within a menu.
- [IMenuSeparator](xref:@ActiproUIRoot.Controls.IMenuSeparator) - Defines the base requirements for separators between menu items.
}
@if (wpf) {
- `System.Windows.Controls.ContextMenu` - The base type of contextual menus.
- `System.Windows.Controls.MenuItem` - The base type of items within a menu.
- `System.Windows.Controls.Separator` - The base type of separators between menu items.
}

The easiest way to implement [IMenuFactory](xref:@ActiproUIRoot.Controls.IMenuFactory) is to derive from [MenuFactory\<T,U,V\>](xref:@ActiproUIRoot.Controls.MenuFactory`3), where each of the generic type arguments identify the type to be used for menus, menu items, and menu separators.  As needed, override the [CreateMenuCore](xref:@ActiproUIRoot.Controls.MenuFactory`3.CreateMenuCore*), [CreateMenuItemCore](xref:@ActiproUIRoot.Controls.MenuFactory`3.CreateMenuItemCore*), and [CreateMenuSeparatorCore](xref:@ActiproUIRoot.Controls.MenuFactory`3.CreateMenuSeparatorCore*) methods to instantiate and configure each respective control, as needed.

@if (avalonia or wpf) {
> [!TIP]
> If you have custom menu-based controls that derive from @@PlatformName native controls and no other customization is necessary, you can easily configure the menu factory to use the custom types simply by specifying those types as the type arguments for the built-in [MenuFactory\<T,U,V\>](xref:@ActiproUIRoot.Controls.MenuFactory`3) class, for example:
> ```csharp
> MenuFactory.Current = new MenuFactory<CustomMenuType, CustomMenuItemType, CustomSeparatorType>();
> ```
}
@if (winforms) {
> [!TIP]
> If you have custom menu-based controls that derive from @@PlatformName native controls, those controls can easily be wrapped in their required [IMenu](xref:@ActiproUIRoot.Controls.IMenu), [IMenuItem](xref:@ActiproUIRoot.Controls.IMenuItem), and [IMenuSeparator](xref:@ActiproUIRoot.Controls.IMenuSeparator) interfaces by calling [MenuFactory](xref:@ActiproUIRoot.Controls.MenuFactory).[WrapMenu](xref:@ActiproUIRoot.Controls.MenuFactory.WrapMenu*), [MenuFactory](xref:@ActiproUIRoot.Controls.MenuFactory).[WrapMenuItem](xref:@ActiproUIRoot.Controls.MenuFactory.WrapMenuItem*), or [MenuFactory](xref:@ActiproUIRoot.Controls.MenuFactory).[WrapMenuSeparator](xref:@ActiproUIRoot.Controls.MenuFactory.WrapMenuSeparator*), respectively.
}

## Working with Icons

The built-in [MenuFactory\<T,U,V\>](xref:@ActiproUIRoot.Controls.MenuFactory`3) class, and any class that derives from it, provides several options for working with icons.

By default, menu items can support the display of icons if one is available.  To force menu items to display without icons, set [MenuFactory\<T,U,V\>](xref:@ActiproUIRoot.Controls.MenuFactory`3).[AllowIcons](xref:@ActiproUIRoot.Controls.MenuFactory`3.AllowIcons) to `false`.

@if (avalonia or wpf) {
### Image Provider
An [Image Provider](../themes/image-provider.md) can be used to automatically associate icons for menu items.  When a menu item is created, the calling routine passes an instance of [MenuFactoryMenuItemOptions](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions) that describe the required menu item.  If the options do not specify an [Icon](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Icon)@if(avalonia) {or [IconTemplate](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.IconTemplate)} but do provide a [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key), that key will be passed to an overload of [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[GetImageSource](xref:@ActiproUIRoot.Media.ImageProvider.GetImageSource*) that looks up an image based on a key.  If the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider) returns an image for that key, it will be used for the menu item.

The [MenuFactory\<T,U,V\>](xref:@ActiproUIRoot.Controls.MenuFactory`3).[ImageProvider](xref:@ActiproUIRoot.Controls.MenuFactory`3.ImageProvider) property is used to configure which  image provider is used.  When `null`, the [ImageProvider](xref:@ActiproUIRoot.Media.ImageProvider).[Default](xref:@ActiproUIRoot.Media.ImageProvider.Default) instance will be used.  This property is set to `null` by default.

To disable looking up icons through an image provider, set [MenuFactory\<T,U,V\>](xref:@ActiproUIRoot.Controls.MenuFactory`3).[CanResolveIconsThroughImageProvider](xref:@ActiproUIRoot.Controls.MenuFactory`3.CanResolveIconsThroughImageProvider) to `false`.
}

## Command Keys

Built-in contextual menus will request a specific key to be associated with each menu item, and constants are declared which define these keys.  When attempting to programatically identify a menu item, it is always recommended to compare it to one of the available constants since the actual values could change over time.  The following classes are available for command key constants, with some being specific to their respective assemblies:

@if (avalonia) {
- `ActiproSoftware.Properties.Shared`.[CommandKeys](xref:ActiproSoftware.Properties.Shared.CommandKeys)
- `ActiproSoftware.Properties.Docking`.[CommandKeys](xref:ActiproSoftware.Properties.Docking.CommandKeys)
}
@if (winforms) {
- `ActiproSoftware.Properties.Shared`.[CommandKeys](xref:ActiproSoftware.Properties.Shared.CommandKeys)
- `ActiproSoftware.Properties.Docking`.[CommandKeys](xref:ActiproSoftware.Properties.Docking.CommandKeys)
- `ActiproSoftware.Properties.Navigation`.[CommandKeys](xref:ActiproSoftware.Properties.Navigation.CommandKeys)
}
@if (wpf) {
- `ActiproSoftware.Properties.Shared`.[CommandKeys](xref:ActiproSoftware.Properties.Shared.CommandKeys)
- `ActiproSoftware.Properties.Docking`.[CommandKeys](xref:ActiproSoftware.Properties.Docking.CommandKeys)
- `ActiproSoftware.Properties.Navigation`.[CommandKeys](xref:ActiproSoftware.Properties.Navigation.CommandKeys)
}

### Identifying a Menu Item

@if (avalonia) {
The [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key) will be assigned to `MenuItem.Name` and the attached `AutomationIdProperty`.

When using [BarsMenuFactory](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory), the [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key) will also be assigned to [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[Key](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Key).
}
@if (winforms) {
When using the default menu factory for native controls, the [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key) will be assigned to `ToolStripMenuItem.Name`.

When using [BarsMenuFactory](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory), the [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key) will be parsed into separate category and name values based on the format `"Category.Name"` (e.g., `"Edit.Copy"`).  These values are then assigned to [BarCommand](xref:@ActiproUIRoot.Controls.Bars.BarCommand).[Category](xref:@ActiproUIRoot.Controls.Bars.BarCommand.Category) and `BarCommand.`[Name](xref:@ActiproUIRoot.Controls.Commands.Command.Name), respectively.  Then end result is that the value of [BarCommand](xref:@ActiproUIRoot.Controls.Bars.BarCommand).[FullName](xref:@ActiproUIRoot.Controls.Bars.BarCommand.FullName), a read-only property that combines of the `Category` and `Name` properties), will exactly match the original value for [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key).

> [!IMPORTANT]
> If the [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key) does not contain exactly one dot (`.`) separator, [BarsMenuFactory](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory) will assign a default category of `"IMenuFactory"` and the name will be the full value of the [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key).  This default behavior can be modified by creating a new class that derives from [BarsMenuFactory](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory) and overrides [TryParseCommandFullName](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory.TryParseCommandFullName*) to generate the desired result.
}
@if (wpf) {
The [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key) will be assigned to the attached `AutomationIdProperty`.

Since `MenuItem.Name` *must* be a valid identifier name (i.e., alpha-numeric or underscore characters only and cannot start with a digit), any requested [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key) will first be converted to a valid identifier name before it is assigned to `MenuItem.Name`  (e.g., `"Edit.Copy"` will become `"Edit_Copy"`).

When using [BarsMenuFactory](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory), the [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key) will also be assigned to [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[Key](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Key).

> [!WARNING]
> Most of the built-in command keys include a dot (`.`) character that makes them *invalid identifiers*, so the `MenuItem.Name` property *will not* exactly match the defined constant.

Instead of comparing `MenuItem.Name`, use the attached `AutomationIdProperty` instead.  This property does not have to be a valid identifier and will always exactly match the value of the defined constant.

```csharp
if (AutomationProperties.GetAutomationId(menuItem) == CommandKeys.Edit.Copy) { ... }
```

Alternatively, use [BarsMenuFactory](xref:@ActiproUIRoot.Controls.Bars.BarsMenuFactory) and the requested [Key](xref:@ActiproUIRoot.Controls.MenuFactoryMenuItemOptions.Key) will always exactly match the [BarMenuItem](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem).[Key](xref:@ActiproUIRoot.Controls.Bars.BarMenuItem.Key).
}

## Using Menu Factory

While the menu factory classes were created to help ensure developers could easily customize the default menus created by Actipro products, there is no reason they cannot be used by anyone wanting a centralized way to manage their contextual menus.  The following demonstrates how the current menu factory could be used to create a show a simple context menu:

@if (avalonia) {
```csharp
using ActiproSoftware.Properties.Shared;
...
var menuFactory = MenuFactory.Current;
var menu = menuFactory.CreateMenu();
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Cut, Text = "Cu_t", Command = MyCommands.Cut } ));
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Copy, Text = "_Copy", Command = MyCommands.Copy } ));
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Paste, Text = "_Paste", Command = MyCommands.Paste } ));
menu.Items.Add(menuFactory.CreateMenuSeparator());
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Delete, Text = "_Delete", Command = MyCommands.Delete } ));
menu.ShowAt(myControl);
```
}
@if (winforms) {
```csharp
using ActiproSoftware.Properties.Shared;
...
var menuFactory = MenuFactory.Current;
var menu = menuFactory.CreateMenu();
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Cut, Text = "Cu&t", Command = MyCommands.Cut } ));
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Copy, Text = "&Copy", Command = MyCommands.Copy } ));
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Paste, Text = "&Paste", Command = MyCommands.Paste } ));
menu.Items.Add(menuFactory.CreateMenuSeparator());
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Delete", Text = "&Delete", Command = MyCommands.Delete } ));
menu.Show(myControl, new Point(x, y));
```
}
@if (wpf) {
```csharp
using ActiproSoftware.Properties.Shared;
...
var menuFactory = MenuFactory.Current;
var menu = menuFactory.CreateMenu();
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Cut, Text = "Cu_t", Command = ApplicationCommands.Cut } ));
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Copy, Text = "_Copy", Command = ApplicationCommands.Copy } ));
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Paste, Text = "_Paste", Command = ApplicationCommands.Paste } ));
menu.Items.Add(menuFactory.CreateMenuSeparator());
menu.Items.Add(menuFactory.CreateMenuItem(new MenuFactoryMenuItemOptions() { Key = CommandKeys.Edit.Delete, Text = "_Delete", Command = ApplicationCommands.Delete } ));
menu.Placement = PlacementMode.Bottom;
menu.PlacementTarget = myControl;
menu.IsOpen = true;
```
}
