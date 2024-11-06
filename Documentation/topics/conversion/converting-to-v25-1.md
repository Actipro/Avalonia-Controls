---
title: "Converting to v25.1"
page-title: "Converting to v25.1 - Conversion Notes"
order: 97
---
# Converting to v25.1

## Avalonia Dependency

Updated the minimum Avalonia dependency from v11.1.0 to v11.2.0.

## User Prompt Icon Updates

With the addition of [Icon Presenter](../themes/icon-presenter.md) and the flexbility it provides for defining icons, all `IImage`-based properties and methods on the various User Prompt classes (i.e., [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl)  and [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder)) where changed from `IImage?` value types to `object?`.

To ease the transition, the old properties and methods have been deprecated and will be removed in a future release.  While no immediate changes should be necessary for most users, it is recommended to stop using the old properties and methods as soon as possible to avoid future breaking changes when they are officially removed.

The following updates should be made for [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl):
- Replace references to the `FooterImageSource` property with the [FooterIcon](xref:@ActiproUIRoot.Controls.UserPromptControl.FooterIcon) property.
- Replace references to the `StatusImageSource` property with the [StatusIcon](xref:@ActiproUIRoot.Controls.UserPromptControl.StatusIcon) property.
- Replace references to the `HasFooterImage` property with the [IsFooterIconVisible](xref:@ActiproUIRoot.Controls.UserPromptControl.IsFooterIconVisible) property.
- Replace references to the `HasStatusImage` property with the [IsStatusIconVisible](xref:@ActiproUIRoot.Controls.UserPromptControl.IsStatusIconVisible) property.
- Any customized templates for [UserPromptControl](xref:@ActiproUIRoot.Controls.UserPromptControl) should be able to handle `object` value types instead of only `IImage` value types for the status and footer icons.

The following updates should be made for [UserPromptBuilder](xref:@ActiproUIRoot.Controls.UserPromptBuilder):
- Replace references to the `FooterImage` property with the [FooterIcon](xref:@ActiproUIRoot.Controls.UserPromptBuilder.FooterIcon) property.
- Replace references to the `StatusImage` property with the [StatusIcon](xref:@ActiproUIRoot.Controls.UserPromptBuilder.StatusIcon) property.
- Replace calls to the `WithFooterImage` method with the [WithFooterIcon](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithFooterIcon*) method.
- Replace calls to the `WithStatusImage` method with the [WithStatusIcon](xref:@ActiproUIRoot.Controls.UserPromptBuilder.WithStatusIcon*) method.

> [!TIP]
> See the new [Icon Presenter](../themes/icon-presenter.md) topic for more details on how `object`-based images are supported.

## ImageControlConverter Deprecated

The `ImageControlConverter` class has been deprecated and will be removed in a future release.  Use [IconControlConverter](xref:@ActiproUIRoot.Controls.Converters.IconControlConverter) instead.

The `ImageControlConverter` would only convert an `IImage` to a [DynamicImage](xref:@ActiproUIRoot.Controls.DynamicImage).  By default, the [ImageControlConverter](xref:@ActiproUIRoot.Controls.Converters.ImageControlConverter) will convert an `IImage` to a `ContentControl` whose content will be a [DynamicImage](xref:@ActiproUIRoot.Controls.DynamicImage).  [IconControlConverter](xref:@ActiproUIRoot.Controls.Converters.IconControlConverter) also supports other data types, and the default `IDataTemplate` for each value type can easily be customized and extended using the [IconPresenter](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter).[DefaultContentTemplate](xref:@ActiproUIRoot.Controls.Primitives.IconPresenter.DefaultContentTemplate) property.