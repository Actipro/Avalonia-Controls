---
title: "Supported Technologies"
page-title: "Supported Technologies"
order: 50
---
# Supported Technologies

Actipro @@PlatformName controls are compatible with a number of different technologies, all described below.

## Frameworks

The products have assemblies available for multiple runtime frameworks, including:

- .NET 8
- .NET 10 or later

### Avalonia Support Matrix

The assemblies have dependencies on the Avalonia framework as follows:

<table>
<thead>

<tr>
<th>Actipro Controls Versions</th>
<th>Avalonia Support</th>
</tr>

</thead>
<tbody>

<tr>
<td>Actipro Controls v26.2.0</td>
<td>

- Avalonia v12.1.2 or later
- Native themes compatible up to Avalonia v12.1.2
- *Minor updates to Avalonia are expected to be supported and will be verified as they are released*

<tr>
<td>Actipro Controls v26.1.0</td>
<td>

- Avalonia v12.0.0 or later
- Native themes compatible up to Avalonia v12.0.5

</td>
</tr>

<tr>
<td>
Actipro Controls v25.2.3
<br/>
Actipro Controls v25.2.2
</td>
<td>

- Avalonia v11.3.7 or later
- Native themes compatible up to Avalonia v11.3.11

</td>
</tr>

<tr>
<td>
Actipro Controls v25.2.1
<br/>
Actipro Controls v25.2.0
</td>
<td>

- Avalonia v11.3.0 or later
- Native themes compatible up to Avalonia v11.3.6

</td>
</tr>

<tr>
<td>Actipro Controls v25.1</td>
<td>

- Avalonia v11.2.0 or later
- Native themes compatible up to Avalonia v11.3.0

</td>
</tr>

<tr>
<td>Actipro Controls v24.2</td>
<td>

- Avalonia v11.1.0 or later
- Native themes compatible up to Avalonia v11.1.0

</td>
</tr>

<tr>
<td>Actipro Controls v24.1</td>
<td>

- Avalonia v11.0.7 or later
- Native themes compatible up to Avalonia v11.0.11

</td>
</tr>

<tr>
<td>Actipro Controls v23.1</td>
<td>

- Avalonia v11.0.5 or later
- Native themes compatible up to Avalonia v11.0.7

</td>
</tr>

</tbody>
</table>

> [!NOTE]
> While they do not change frequently, native themes must be kept in sync with Avalonia control updates and may not work with untested releases. If you encounter any issues with native themes, please contact [Support](support.md).

## Architectures

The products have been tested and are supported under the following architectures:

- Any CPU
- ARM64
- x64
- x86

## Platforms

The products have been tested on the following platforms:

- Windows
- macOS
- Linux (Ubuntu)
- WASM (Browser)

The products may work on other platforms that Avalonia supports as well.

### Known Platform Limitations

#### Windows Subsytem for Linux (WSL)

While Linux is supported, some controls may not work as well with Windows Subsystem for Linux (WSL).  The following are known issues:

- Any window with a transparent background will still have a rectangular outline and drop shadow added by WSL that cannot be removed.
- Some windows, when maximized, may not fully occupy the screen and will render slightly offset from the upper-left corner.  While in this state, pointer hit testing is also offset by the same amount.

#### Linux Wayland

Linux Wayland support is currently flagged as experimental for Avaloina v12.1, and should be considered experimental with Actipro controls as well.  The following are known issues:

- Wayland does not allow a window's position to be explicitly set, so this can cause new windows with a `Manual` startup location to appear in the wrong position since placement is ignored.
- Floating windows used by Docking controls cannot be dragged to move.
  - Linux does not provide feedback about the window/pointer position during a native drag operation, which is critical for showing dock previews during drag.
  - Instead of native drag/move operations, Docking windows capture the pointer during mouse down and manually changes the window position as the pointer moves.  Since Wayland ignores the window position changes, it will not respond to these manual drag requests.
  - We are investigating techniques to provide the best experience possible on Wayland, but X11 provides the best support for Linux.
  - If Wayland must be used, it is recommended to set [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[UseHostedFloatingWindows](xref:@ActiproUIRoot.Controls.Docking.DockSite.UseHostedFloatingWindows) = `true` and [DockSite](xref:@ActiproUIRoot.Controls.Docking.DockSite).[UseHostedPopups](xref:@ActiproUIRoot.Controls.Docking.DockSite.UseHostedPopups) = `true`.  With these settings enabled, Docking will not attempt to use any floating window controls and, instead, will emulate floating content.

## IDEs

The products are compatible with all IDEs supported by @@PlatformName.
