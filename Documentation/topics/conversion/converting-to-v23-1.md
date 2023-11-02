---
title: "Converting to v23.1"
page-title: "Converting to v23.1 - Conversion Notes"
order: 10
---
# Converting to v23.1

While v23.1.0 was the first public release version, some updates were made in maintenance releases that are described below.

## User Interface Density Changes

v23.1.1 introduced the concept of user interface density, a configurable way to set how spacious a user interface layout should appear.  v23.1.0 shipped with what is effectively now the `Spacious` density option, which is very touch friendly.  v23.1.1 has updated the default to be the `Normal` density option, resulting in a slightly more compact layout out of the box.

The [User Interface Density](../themes/user-interface-density.md) topic discusses the density feature in detail and how to change it both initially and at any point later at run-time.  Use code in that topic to adjust the density if you prefer the old `Spacious` density option or the even more dense desktop-oriented `Compact` density option for your application.

## Theme Definition Changes

### ScrollBar Thumbs

v23.1.0 used to have a `ScrollBarThumbMargin` property on [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition) that would keep the thumb ascent relative to the containing scrollbar track size.  Scrollbar thumb sizing has been refactored in v23.1.1 and is much more flexible.  The new design allows for fixed thumb thicknesses, or alternatively having the thumb size relative to the containing track size.  Thumbs now default to a slightly thinner fixed `4.0` thickness instead of the previous effective value of `6.0`.  Set both the [ScrollBarThumbMinAscent](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.ScrollBarThumbMinAscent) and [ScrollBarThumbMaxAscent](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition.ScrollBarThumbMaxAscent) properties to `6.0` if the previous appearance is desired.

### MenuItem Padding

v23.1.0 used to have a `MenuItemPadding` property on [ThemeDefinition](xref:@ActiproUIRoot.Themes.Generation.ThemeDefinition), but that property was removed in v23.1.1 since the padding applied to menu items now automatically scales based on UI density.