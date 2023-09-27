---
title: "Troubleshooting (General)"
page-title: "Troubleshooting (General)"
order: 45
---
# Troubleshooting (General)

This topic provides several tips on common questions or issues that you may encounter while using Actipro Avalonia UI controls.

## Some String Resource Customizations Don't Take Effect in UI

If you run into a scenario where some string resource customizations do take effect in the UI but others don't, the problem is most likely the location of your string customization code.

As indicated by a note in the [Customizing String Resources](customizing-string-resources.md) topic, all string resources should be customized immediately at application startup (such as in `Application.Initialize`) and before any UI or control classes are even referenced.  The note in the topic gives more detail on why this is necessary.

## Data Binding Errors at Run-Time

Sometimes there may be some data binding errors that show up in the Visual Studio console window when executing an application that uses an Actipro Avalonia UI control product.  Actipro Avalonia UI has some very large and complex templates for its products' controls and these error messages may show up in the Visual Studio console due to the timing between data binding resolution and visual tree creation.

It is very important to note that the data binding errors are NOT problems in our code.  If they were, the bindings would not work at all at run-time and you would see broken UI functionality.  This is not the case, everything works correctly at run-time after the visual tree has been fully constructed and the bindings have been re-evaluated.  The Avalonia team plans on refactoring the data binding system in the future to prevent these misleading error messages from being logged.

So just to reiterate, the data binding error messages are not problems with our code, and are simple warnings due to data bindings trying to resolve themselves before the targets' visual trees are created.  You may safely ignore these error messages.
