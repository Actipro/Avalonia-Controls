---
title: "Licensing"
page-title: "Licensing"
order: 31
---
# Licensing

This topic discusses which products require licensing, the various product licensing options available, and walks through how to apply licensing when both evaluating and after a purchase.

## Free Products

The [Actipro Themes](themes/index.md), [Actipro Shared Library](shared/index.md), and [Actipro Core Library](core/index.md) products are free! Use and distribute the related assemblies in any application, including for commercial use, without any license fees or royalties.

For simplicity, no license registration calls need to be made in applications that only use free Actipro products.

> [!TIP]
> Even customers who only use the free products can still pay for Actipro Avalonia Pro to gain access to the XAML source of all control styles/templates in those products and priority support.

## Paid License Types

Actipro Avalonia Pro is available for purchase with several license types.

### Evaluation License

Under the Evaluation License, you may only use the product on a trial basis and for a brief evaluation period, after which you must cease use of the product or purchase a Single Developer License for each individual developer on a project that uses the product.  The evaluation period is typically thirty (30) days but may be extended by Actipro.

Evaluation versions of the products may include some background watermarks or other means of displaying they are not licensed versions, all of which are removed once licenses are purchased and applied.

> [!TIP]
> While evaluating, a license popup will display when a non-free control is first used.  The prompts on the popup allow you to generate and copy a `RegisterActiproLicense` call that can suppress future license popups for several days.  See the "Apply Licensing" section below for information on how to use that method call.

### Single Developer License

Single Developer Licenses must be purchased for *each individual developer* on a project using a product and **may not** be transferred between developers.

This license grants each developer the right to use the purchased products in production applications and to include any files marked as redistributable with their applications.  Each developer will also have access to the XAML source styles/templates for all free and purchased products.

### Site License

Site licenses may be purchased which automatically grant all developers at a physical location a Single Developer License.

### Enterprise License

Enterprise licenses may be purchased which automatically grant all developers in your entire organization and at all sites a Single Developer License.

### Blueprint License (Source Code)

Blueprint licenses enable access to product source code.  The term of Blueprint licenses must be kept in sync with product subscriptions.

> [!IMPORTANT]
> Please read the EULA to see the Blueprint license terms and conditions.

## Free Subscriptions and Free New Products

All of the non-evaluation licenses described above for our paid Avalonia products include **one full year** of free upgrades to any new versions that are released within that timeframe.

In addition, customers with active subscriptions will receive any new products added to the suite for free while the subscription is active.

## No Runtime, Server, Distribution Royalties

Actipro Software control products have **NO** runtime, server, or distribution royalties if you own the proper amount of developer licenses for your development team (see above).

## Purchasing Licenses

Licenses may be purchased directly from the [Actipro web site](https://www.actiprosoftware.com/purchase/cart).  After a purchase has been made and the purchase has been processed, you will be e-mailed information on how to log into your Actipro account on our web site. Use the login information contained within the e-mail to log into the site.

Once you are within your account on our web site, download the latest release and e-mail yourself your license key.  Then be sure to follow the instructions in the following sections to properly license the product for use in your applications.

## Apply Licensing

To apply licensing once you have purchased licenses for Actipro controls you must know the "Licensee" and "License Key" (license information) for the `major.minor` version you have licensed.

> [!NOTE]
> Each `major.minor` version has its own distinct license information that can be obtained for licensed versions on [your Actipro account](https://www.actiprosoftware.com/support/account)'s page for your organization.
>
> The first few characters of a license key indicate the `major.minor` version to which it applies. For example, a license for v23.1 will start with `AVA231`, so make sure you use the correct license key for the version of the controls you are referencing.

To apply the license, an `AppBuilder` extension method is called while building the Avalonia application.  This call registers your license information globally throughout your application.

The following code calls the `AppBuilder.RegisterActiproLicense` extension method using `licensee` and `licenseKey` variables containing string values that must exactly match the license information in your license e-mail:

```csharp
using Avalonia;
...
public static AppBuilder BuildAvaloniaApp() {
	return AppBuilder.Configure<App>()
		// NOTE: Set "licensee" and "licenseKey" variables to your license information
		.RegisterActiproLicense(licensee, licenseKey)
		;
}
```

> [!WARNING]
> It is important to protect your licensee and license key combination from decompilers.  We highly recommend using some form of string encryption on the `licensee` and `licenseKey` values passed into the `AppBuilder.RegisterActiproLicense` extension method.  Many obfuscators include string encryption as an option, or you can use other custom logic to scramble/descramble the strings.

### Notes on Unit Tests

If your application build job runs any unit tests that create Actipro UI controls, the `ActiproLicenseManager.RegisterLicense` method must be called before the Actipro UI controls are created to avoid any licensing-related prompts or exceptions.  `ActiproLicenseManager.RegisterLicense` is the core method invoked by the `AppBuilder.RegisterActiproLicense` extension method.

```csharp
using ActiproSoftware.Licensing;
...
public void OnTestActiproControl() {
	// NOTE: Set "licensee" and "licenseKey" variables to your license information
	ActiproLicenseManager.RegisterLicense(licensee, licenseKey);

	// Unit test logic here that creates an Actipro control
	...
}
```

The license registration could alternatively be placed in a more centralized location, such as in the unit test class constructor.  The important thing is that it is called before Actipro UI controls are created.

## Open-Source Project Licensing Considerations

Product licenses may be purchased for developers working with Actipro Avalonia Pro in open-source projects.  However, this introduces some issues that must be handled with care.  Namely, ensuring that "Licensee" and "License Key" values are not committed in any way to an open-source code repository, and that the proper number of developer licenses are purchased.

### Developers of Open-Source Projects

#### Excluding Licensee and License Key from Commits

> [!WARNING]
> License information is considered secret and cannot be committed to an open-source code repository in any form, encrypted or not.

This makes things tricky in terms of how to register a license at app startup in code, since the "Licensee" and "License Key" values cannot be stored in the committed source code the same way they can in closed source projects.

There are ways to handle this open-source project usage scenario though.  One way would be to include an embedded resource file in a project that, if present, would contain the license information to be loaded into the `licensee` and `licenseKey` variables for an `AppBuilder.RegisterActiproLicense` call.  This embedded resource file must be ignored via a `.gitignore` file entry so that it doesn't ever commit to the open source repository.

The following sample code demonstrates some general logic that could be used:

```csharp
using Avalonia;
...
public static AppBuilder BuildAvaloniaApp() {
	var builder = AppBuilder.Configure<App>();

	string licensee, licenseKey;
	// NOTE: Load licensee and licenseKey variable values here from some mechanism
	//       that is properly excluded via .gitignore

	if (!string.IsNullOrEmpty(licensee) && !string.IsNullOrEmpty(licenseKey))
		builder = builder.RegisterActiproLicense(licensee, licenseKey);

	return builder;
}
```

#### Purchasing Developer Licenses

Confidential license information such as "Licensee" and "License Key" cannot be shared with anyone who is not properly licensed.  If you purchase four developer licenses for your team that works on an open-source project, you are only permitted to share the confidential license information with those four developers.  A fifth developer starting on the project would require an additional developer license to be purchased.

### End Users of Open-Source Applications

As with closed-source applications, when an open-source application is distributed purely in binary form, no additional licenses need to be purchased by end users.

However if an end user is compiling and executing the source code for an open-source application to run it, they are considered to be a developer in terms of licensing and must purchase a developer license.

## Licensing Questions?

If you have any further questions regarding purchasing or licensing, please [contact us](http://www.actiprosoftware.com/company/contact).  We're happy to help.
