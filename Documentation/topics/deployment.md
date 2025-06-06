---
title: "Deployment"
page-title: "Deployment"
order: 33
---
# Deployment

## Deployment Steps

There are two main steps to follow when deploying an application that uses Actipro @@PlatformName control products.

### Configure Licensing

Please read the [Licensing](licensing.md) topic for details on applying purchased licenses for paid products.  Once configured properly, your license data will be applied via application code at run-time. No licensing configuration is necessary for free products.

### Include Redistributable Assemblies

Next, simply copy the appropriate redistributable assemblies to the same folder as your application's executable on the target machine.  Nothing else needs to be deployed.

This table describes where referenced assemblies appear after project compilation:

| Reference Kind | Description |
|-----|-----|
| NuGet package reference | The referenced assemblies will be in your `bin` folder after compilation. |
| Assembly reference | The referenced assemblies will be in your `bin` folder after compilation if the "Copy Local" property on the reference (via the Visual Studio Properties window) is set to true. |

## Redistributable Files

When you use our Actipro Avalonia Free products or own a license for Actipro Avalonia Pro, you are licensed to redistribute certain files with your application.

The lists below show the product assemblies that may be redistributed based on the product(s) for which you own licenses, along with the NuGet packages that contain them.

### Actipro Avalonia Free

The following assemblies may be redistributed for free:
- *ActiproSoftware.Avalonia.Core.dll* - via `ActiproSoftware.Controls.Avalonia` NuGet package.
- *ActiproSoftware.Avalonia.Shared.dll* - via `ActiproSoftware.Controls.Avalonia` NuGet package.

Optional free additional native control theme libraries:
- *ActiproSoftware.Avalonia.Themes.Native.ColorPicker.dll* - via `ActiproSoftware.Controls.Avalonia.Themes.ColorPicker` NuGet package.
- *ActiproSoftware.Avalonia.Themes.Native.DataGrid.dll* - via `ActiproSoftware.Controls.Avalonia.Themes.DataGrid` NuGet package.

### Actipro Avalonia Pro

The following assemblies may be redistributed when you own a license for Actipro Avalonia Pro:
- *ActiproSoftware.Avalonia.Core.dll* - via `ActiproSoftware.Controls.Avalonia` NuGet package.
- *ActiproSoftware.Avalonia.Shared.dll* - via `ActiproSoftware.Controls.Avalonia` NuGet package.
- *ActiproSoftware.Avalonia.Fundamentals.dll* - via `ActiproSoftware.Controls.Avalonia.Pro` NuGet package.
- *ActiproSoftware.Avalonia.Bars.dll* - via `ActiproSoftware.Controls.Avalonia.Pro` NuGet package.
- *ActiproSoftware.Avalonia.Docking.dll* - via `ActiproSoftware.Controls.Avalonia.Pro` NuGet package.

Optional free additional supplemental libraries:
- *ActiproSoftware.Avalonia.Bars.Mvvm.dll* - via `ActiproSoftware.Controls.Avalonia.Bars.Mvvm` NuGet package.

Optional free additional native control theme libraries:
- *ActiproSoftware.Avalonia.Themes.Native.ColorPicker.dll* - via `ActiproSoftware.Controls.Avalonia.Themes.ColorPicker` NuGet package.
- *ActiproSoftware.Avalonia.Themes.Native.DataGrid.dll* - via `ActiproSoftware.Controls.Avalonia.Themes.DataGrid` NuGet package.

## Other Deployment Notes

### No Run-time Royalties

There are no run-time royalties or other distribution charges for our @@PlatformName control products.  We only require that for paid products you have the proper number of licenses for each developer on your project.

See the [Licensing](licensing.md) topic for more information on licensing requirements.