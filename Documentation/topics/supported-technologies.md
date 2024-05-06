---
title: "Supported Technologies"
page-title: "Supported Technologies"
order: 50
---
# Supported Technologies

Actipro @@PlatformName controls are compatible with a number of different technologies, all described below.

## Frameworks

The products have assemblies available for multiple runtime frameworks, including:

- .NET 6 or later

The assemblies have the following dependencies on UI frameworks:

- Avalonia UI v11.0.7 or later
- Native themes compatible up to Avalonia UI v11.0.10

> [!NOTE]
> While they do not change frequently, native themes must be kept in sync with Avalonia control updates and may not work with untested releases. If you encounter any issues with native themes, please contact [Support](support.md).

## Architectures

The products have been tested and are supported under the following architectures:

- Any CPU
- ARM64 (see note below)
- x64
- x86

> [!WARNING]
> Avalonia has a known issue where rendering on a Windows ARM64 device such as the Windows Dev Kit 2023 (Project Volterra) is currently not functional.  This was last validated on Avalonia v11.0.4.

## Platforms

The products have been tested on the following platforms:

- Windows
- macOS
- Linux (Ubuntu)
- WASM (Browser)

The products may work on other platforms that Avalonia supports as well.

## IDEs

The products are compatible with all IDEs supported by @@PlatformName.
