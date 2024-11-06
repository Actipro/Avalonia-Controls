---
title: "NuGet Packages and Feeds"
page-title: "NuGet Packages and Feeds"
order: 32
---
# NuGet Packages and Feeds

Actipro provides NuGet packages for its @@PlatformName control assemblies so that they can easily be consumed by .NET projects and later updated.

The NuGet packages may include multiple .NET variations of the assemblies.  When a package is referenced, NuGet will use the best variation for your target framework.  See the [Supported Technologies](supported-technologies.md) topic for a list of supported frameworks.

## Actipro NuGet Packages

The following are Actipro's NuGet packages for @@PlatformName and each one's package dependencies.  See the [Deployment](deployment.md) topic for a mapping of each Actipro product to redistributable assemblies, and their containing NuGet package where appropriate.

### ActiproSoftware.Controls.Avalonia Package

This package references most of the Actipro Avalonia Free product assemblies ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.Avalonia)), which can be used at no cost.

The free themes for the native `ColorPicker` and `DataGrid` controls require separate optional packages listed below.

### ActiproSoftware.Controls.Avalonia.Pro Package

This package references all of the Actipro Avalonia Pro product assemblies ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.Avalonia.Pro)), and is for customers who have licensed the Actipro Avalonia Pro controls.  Dependencies include:

- `ActiproSoftware.Controls.Avalonia`

### ActiproSoftware.Controls.Avalonia.Bars.Mvvm Package

This package includes free classes and themes that support using the MVVM (Model-View-ViewModel) pattern with the Actipro Bars product ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.Avalonia.Bars.Mvvm)), and is for customers who have licensed the Actipro Avalonia Pro controls.  Dependencies include:

- `ActiproSoftware.Controls.Avalonia`
- `ActiproSoftware.Controls.Avalonia.Pro`

### ActiproSoftware.Controls.Avalonia.Themes.ColorPicker Package

This package includes free themes for the native Avalonia `ColorPicker` control that complement other Actipro Avalonia control themes ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.Avalonia.Themes.ColorPicker)).  Dependencies include:

- `ActiproSoftware.Controls.Avalonia`

### ActiproSoftware.Controls.Avalonia.Themes.DataGrid Package

This package includes free themes for the native Avalonia `DataGrid` control that complement other Actipro Avalonia control themes ([view on nuget.org](https://www.nuget.org/packages/ActiproSoftware.Controls.Avalonia.Themes.DataGrid)).  Dependencies include:

- `ActiproSoftware.Controls.Avalonia`

## Using the nuget.org Package Source

Actipro publishes its @@PlatformName control NuGet packages to nuget.org, which is the most popular package source, run by Microsoft and natively supported by Visual Studio.

Actipro's packages can be found at ([https://www.nuget.org/packages?q=ActiproSoftware.Controls.Avalonia](https://www.nuget.org/packages?q=ActiproSoftware.Controls.Avalonia)) since all of Actipro's @@PlatformName control NuGet package names begin with "ActiproSoftware.Controls.Avalonia".

Visual Studio should have the nuget.org package source defined by default, which will allow the Actipro packages to be found.  This can be verified by selecting Visual Studio's **Tools > NuGet Package Manager > Package Manager Settings** menu item and on the dialog that appears, select **Package Sources** in the tree on the left, and the available package sources will be listed on the right.  The **nuget.org** package source entry should point to `https://api.nuget.org/v3/index.json`.  Please make sure this entry exists.

## Using a Local Package Source

Some customers may wish to set up a local package source, such as on a local or network drive.  This is common in scenarios where the company policy is to maintain its own NuGet repository (instead of using nuget.org), you have an offline machine (can't access nuget.org), Actipro provided prerelease versions for testing, etc.

Visual Studio makes it easy to add a package source from a folder.  To add a local package source, select Visual Studio's **Tools > NuGet Package Manager > Package Manager Settings** menu item and on the dialog that appears, select **Package Sources** in the tree on the left, and the available package sources will be listed on the right.  Click the **+** (plus) button above the list and add a new entry.  Provide a **Name** and use the path to your local/network folder that contains the NuGet packages as the **Source**.

The Actipro NuGet packages can be downloaded directly from the package pages linked to at ([https://www.nuget.org/profiles/ActiproSoftware](https://www.nuget.org/profiles/ActiproSoftware)).

## Managing NuGet Packages

To find and install an Actipro package, right click on your solution in Visual Studio's **Solution Explorer** tool window, and select the **Manage NuGet Packages for Solution...** menu item.  In the document that appears, ensure the package source on the upper right points to the package source that contains Actipro NuGet packages (e.g., nuget.org).  Then enter *ActiproSoftware.Controls.Avalonia* in the **Browse** tab's **search** box to see available @@PlatformName control packages.

Select the package in the package list that you wish to add to a project.  On the right side, check the project(s) that should have the target project added.  Choose the version to install and click the **Install** button.

After following the normal NuGet package install flow, the package will be listed as a package reference in the **Solution Explorer**.  This process can be repeated to install other Actipro NuGet packages.

If you wish to remove a package from a project, select the package in the list on the left and the project that references it on the right.  Then click the **Uninstall** button to remove the package reference.

More detailed instructions on using Visual Studio's NuGet Package Manager are available on Microsoft's documentation site ([https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio)).

## Updating NuGet Packages

When new Actipro NuGet package versions are later released, they will appear on the **Updates** tab of the document that appears when you right click on your solution in Visual Studio's **Solution Explorer** tool window and select the **Manage NuGet Packages for Solution...** menu item.

Select the package to update in the list on the left, and the pick the desired version on the right.  Then click the **Update** button to update to the selected version.

> [!IMPORTANT]
> Never mix versions of Actipro @@PlatformName control NuGet packages.  If you reference multiple packages and change to a different version of one, make sure you update all the other Actipro packages to match the same version.

More detailed instructions on using Visual Studio's **NuGet Package Manager** are available on Microsoft's documentation site ([https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio#update-a-package](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio#update-a-package)).
