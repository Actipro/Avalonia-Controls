---
title: "Samples"
page-title: "Samples"
order: 34
---
# Samples

The **Sample Browser** is the main sample project for evaluating Actipro Avalonia UI controls, and the [full source is available on GitHub](open-source.md).  It is designed to provide information, comprehensive demos, and detailed samples for all the Actipro @@PlatformName products.

We pride ourselves in providing more full-source samples than our competition, allowing you to learn about our products more easily and avoid
having to rewrite common logic and design patterns.

This source code is invaluable for learning how to use the various control products.

## Downloading the Sample Browser

Visit [https://github.com/Actipro/Avalonia-Controls](https://github.com/Actipro/Avalonia-Controls) and download our repository's source code.  The repository contains the full source for the **Sample Browser** project.

GitHub offers several download options including:
- [Cloning the repo with Git](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository) using this URL:
  - `git clone https://github.com/Actipro/Avalonia-Controls.git`
- Downloading a ZIP of the source code's current state.

> [!TIP]
> It is recommended to clone the repository with the URL so that you can easily pull in future updates.

## Running the Sample Browser

Once the repository's source code has been downloaded to a folder, open the `Samples/SampleBrowser/SampleBrowser.Desktop.sln` file in Visual Studio.

The projects in that sample are preconfigured to reference our product [NuGet packages](nuget.md), as well as NuGet packages for Avalonia.  Run the solution to open the **Sample Browser** application.

### Changing the Actipro Product Version

The `Samples/SampleBrowser/References/ActiproSoftware.References.props` file contains an `ActiproVersion` property that defines the version of the Actipro product NuGet packages to reference.  Update that value if you wish to use an alternate version.

### Changing the Avalonia Version

The `Samples/SampleBrowser/References/Avalonia.References.props` file contains an `AvaloniaVersion` property that defines the version of the Avalonia packages to reference.  Update that value if you wish to use an alternate version.

## Types of Samples

Most samples involve one or more simple examples that are focused on a particular feature area or show how to perform a certain task. These kinds of samples are the place to quickly learn how to get a specific feature working without having to navigate around the unrelated code needed to support other features.

> [!TIP]
> Most simple samples include snippets of example code that are updated in real-time based on configurable options. This interactive model can be used to quickly understand how to achieve a desired result then copy the snippet for use directly in your own application.

On the other hand, a demo is a large sample that shows the combination of many product features together to create a finished product. Demos are intended to show some ideas of what you can do with our products when you use them in your own applications.

## Reusing Sample Source Code

The full source for all samples is in the sample project and you are free to reuse the sample source code.

We encourage you to copy what you need to help you get started with using the product in your own applications.
