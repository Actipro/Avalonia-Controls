---
title: "Security"
page-title: "Security"
order: 53
---
# Security

## Security Practices

For information about how Actipro develops, maintains, and secures its products, please see our company-wide [Security Practices Policy](https://www.actiprosoftware.com/company/policies/security-practices).

That page outlines our secure development processes, vulnerability handling procedures, support period commitments, and other practices that help ensure the safety and reliability of our software.

## Vulnerability Reporting and Security Advisories

Security reporting for the Actipro @@PlatformName controls is managed through our public GitHub repository.

You can find the product-specific instructions for reporting vulnerabilities and any published security advisories in the [Actipro @@PlatformName controls repository's Security page](https://github.com/Actipro/Avalonia-Controls/security), which serves as the central location for coordinated vulnerability disclosure for both the commercial Actipro @@PlatformName controls and any related open-source content.

## **EU Cyber Resilience Act (CRA) Compliance**

Actipro @@PlatformName controls are developed and maintained in accordance with the requirements of the EU Cyber Resilience Act (CRA). We follow a secure development lifecycle, provide a defined support period, publish vulnerability reporting instructions, and maintain the technical documentation and evidence required for CE marking under Module A (Internal Control).

## Dynamic Code and Trusted Type Validation

Some product features may attempt to dynamically create and use .NET types from a string type/assembly name.  In accordance with our secure-by-default design, types specified by string type/assembly names will be considered untrusted by default.

All type loading in Actipro logic, like `Type.GetType` and `Activator.CreateInstance` calls, is performed in the [TrustedCodeService](xref:ActiproSoftware.Security.TrustedCodeService) class, which will not load untrusted types.

> [!NOTE]
> Types that are already accessible via a `Type` class instance are considered trusted, since application code external to Actipro logic has already loaded them into the trusted application domain.

### Preemptively Trusting a Type

The [TrustedCodeService](xref:ActiproSoftware.Security.TrustedCodeService) class has these methods for preemptively marking a string type/assembly name as trusted:

- [AddTrustedAssembly](xref:ActiproSoftware.Security.TrustedCodeService.AddTrustedAssembly*) - Marks that all types within a specified `Assembly` are to be trusted.  This is a preferred option when there are multiple types within an assembly that are trusted.
- [AddTrustedType](xref:ActiproSoftware.Security.TrustedCodeService.AddTrustedType*) - Marks that a specific `Type` should be trusted.

### Dynamically Trusting a Type

The [TypeResolutionRequested](xref:ActiproSoftware.Security.TrustedCodeService.TypeResolutionRequested) event is raised whenever a type load for a string type/assembly name is requested.  Its [TypeResolutionEventArgs](xref:ActiproSoftware.Security.TypeResolutionEventArgs) specify the assembly and type names.  The [IsTrusted](xref:ActiproSoftware.Security.TypeResolutionEventArgs.IsTrusted) property is initialized based on if the type is considered trusted, either from one of the trust methods described above or from a previous [TypeResolutionRequested](xref:ActiproSoftware.Security.TrustedCodeService.TypeResolutionRequested) event handler invocation where the type was flagged as trusted.

#### Marking a Type As Trusted

This code snippet demonstrates handling of the event to mark that a type should be trusted and subsequently loaded with a `Type.GetType(typeName)` call:

```csharp
using ActiproSoftware.Security;
...
// Application startup logic
TrustedCodeService.TypeResolutionRequested += OnTrustedCodeServiceTypeResolutionRequested;
...
private void OnTrustedCodeServiceTypeResolutionRequested(object? sender, TypeResolutionEventArgs e) {
	if (!e.IsTrusted) {
		// Flag a specific class as trusted
		if ((e.TypeName == "MyCompany.TrustedClass") && (e.AssemblyName == "MyApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"))
			e.IsTrusted = true;
	}
}
```

> [!TIP]
> Although this is NOT recommended, an application may reproduce the legacy behavior from before [TrustedCodeService](xref:ActiproSoftware.Security.TrustedCodeService) existed by handling the [TypeResolutionRequested](xref:ActiproSoftware.Security.TrustedCodeService.TypeResolutionRequested) event and always setting `e.IsTrusted = true`, effectively bypassing all type and assembly trust checks.

#### Manual Type Loading

In some cases, it may be preferable to manually load and provide an appropriate `Type` instance, which can be done as an alternative to setting the [TypeResolutionEventArgs](xref:ActiproSoftware.Security.TypeResolutionEventArgs).[IsTrusted](xref:ActiproSoftware.Security.TypeResolutionEventArgs.IsTrusted) property to `true`.  When the [ResolvedType](xref:ActiproSoftware.Security.TypeResolutionEventArgs.ResolvedType) property is set to a non-`null` instance, that `Type` will be directly returned by the [Resolve](xref:ActiproSoftware.Security.TrustedCodeService.Resolve*) event and no `Type.GetType(typeName)` call will be made in Actipro logic.

This code snippet shows the concept of manually loading a resolved `Type` with a user-defined `ManuallyLoadType` method and returning the result:

```csharp
using ActiproSoftware.Security;
...
// Application startup logic
TrustedCodeService.TypeResolutionRequested += OnTrustedCodeServiceTypeResolutionRequested;
...
private void OnTrustedCodeServiceTypeResolutionRequested(object? sender, TypeResolutionEventArgs e) {
	if (!e.IsTrusted)
		e.ResolvedType = ManuallyLoadType(e.AssemblyName, e.TypeName);
}
```

### Assembly Name Matching Strategy

The [AssemblyNameMatchingStrategy](xref:ActiproSoftware.Security.TrustedCodeService.AssemblyNameMatchingStrategy) property can be set to an [AssemblyNameMatchingStrategy](xref:ActiproSoftware.Security.AssemblyNameMatchingStrategy) enumeration value that determines how [TrustedCodeService](xref:ActiproSoftware.Security.TrustedCodeService) matches the assembly portion of a string type name with trusted assemblies.  Enumeration values include:

- [FullName](xref:ActiproSoftware.Security.AssemblyNameMatchingStrategy.FullName) - The full assembly name (name, version, culture, and public key token) must match.  This is the default and most secure.
- [NameAndPublicKey](xref:ActiproSoftware.Security.AssemblyNameMatchingStrategy.NameAndPublicKey) - Only the name and public key token are matched.  This allows the version to be ignored.
- [NameOnly](xref:ActiproSoftware.Security.AssemblyNameMatchingStrategy.NameOnly) - Only the name is matched, which is insecure and possibly allows assembly spoofing.  This is not recommended for production usage.

#### Avoiding NameOnly Usage

As an example, assuming a string type name `"MyCompany.MyClass, MyApp"` is being resolved.  Since no matches can be made on version or public key token, only a setting of [NameOnly](xref:ActiproSoftware.Security.AssemblyNameMatchingStrategy.NameOnly) would trust the type.

Instead of using the [NameOnly](xref:ActiproSoftware.Security.AssemblyNameMatchingStrategy.NameOnly) option, it is better to create a [TypeResolutionRequested](xref:ActiproSoftware.Security.TrustedCodeService.TypeResolutionRequested) event handler and add logic in there.  The [TrustLevel](xref:ActiproSoftware.Security.TypeResolutionEventArgs.TrustLevel) property will indicate the level of trust [TrustedCodeService](xref:ActiproSoftware.Security.TrustedCodeService) assigned to the type based on its configuration.

Consider if this application startup code was added to an application named `MyApp`:

```csharp
using ActiproSoftware.Security;
...
// Application startup logic
TrustedCodeService.AddTrustedAssembly(Assembly.GetExecutingAssembly());
TrustedCodeService.TypeResolutionRequested += OnTrustedCodeServiceTypeResolutionRequested;
...
private void OnTrustedCodeServiceTypeResolutionRequested(object? sender, TypeResolutionEventArgs e) {
	// Some type names in deserialization scenarios may only include the pure assembly name
	//   (no version or public key token) for context, so trust those types that are defined in this assembly
	e.IsTrusted |= (e.TrustLevel == StringTypeNameTrustLevel.ConditionallyTrusted) && (e.AssemblyName == Assembly.GetExecutingAssembly().GetName().Name);
}
```

Again, using the example string type name, but with the default [AssemblyNameMatchingStrategy](xref:ActiproSoftware.Security.TrustedCodeService.AssemblyNameMatchingStrategy), the `e.TrustLevel` would be [ConditionallyTrusted](xref:ActiproSoftware.Security.StringTypeNameTrustLevel.ConditionallyTrusted) since the full assembly name couldn't match, but the pure assembly name did match a trusted assembly.  The event handler above verifies conditional trust and checks that the assembly name is the currently executing assembly (`MyApp`).  In that case, the type is marked as trusted.
