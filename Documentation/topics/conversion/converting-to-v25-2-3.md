---
title: "Converting to v25.2.3"
page-title: "Converting to v25.2.3 - Conversion Notes"
order: 94
---
# Converting to v25.2.3

## Security-Related Improvements

We've introduced the new [TrustedCodeService](xref:ActiproSoftware.Security.TrustedCodeService) class in this version as part of our secure-by-design initiative to protect your applications from potential security vulnerabilities related to dynamic type loading.  This service implements a secure-by-default model where types or their containing assemblies must be explicitly marked as trusted before they can be dynamically resolved or instantiated from string type names.  This prevents untrusted or malicious code from being loaded into your application through deserialization, configuration files, or other string-based type references.  This kind of dynamic type loading is used sparingly and only as needed in our products.

> [!WARNING]
> This is an important change that may require action on your part. If your application uses features that dynamically create types from string names, you may encounter `SecurityException` errors until you configure the [TrustedCodeService](xref:ActiproSoftware.Security.TrustedCodeService) appropriately per the instructions in the [Security](../security.md) topic.

We recommend configuring the service during application startup to ensure seamless operation while maintaining robust security protections.

Features that may use dynamic type loading are:
- Docking/MDI - Deserialization of docking layouts when custom inherited [DocumentWindow](xref:@ActiproUIRoot.Controls.Docking.DocumentWindow) or [ToolWindow](xref:@ActiproUIRoot.Controls.Docking.ToolWindow) classes are used.
