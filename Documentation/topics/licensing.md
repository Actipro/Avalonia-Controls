---
title: "Licensing"
page-title: "Licensing"
order: 31
---
# Licensing

This topic discusses which products require licensing, the various product licensing options available, and walks through how to apply licensing when both evaluating and after a purchase.

## Free Products

The [Actipro Themes](themes/index.md), [Actipro Shared Library](shared/index.md), and [Actipro Core Library](core/index.md) products are free! Use and distribute the related assemblies in any application, including for commercial use, without any license fees or royalties.

> [!TIP]
> Even customers who only use the free products can still pay for Actipro Avalonia UI Pro to gain access to the XAML source of all control styles/templates in those products and priority support.

## Paid License Types

Actipro Avalonia UI Pro is available for purchase with several license types.

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

All of the non-evaluation licenses described above for our paid Avalonia UI products include **one full year** of free upgrades to any new versions that are released within that timeframe.

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

## Licensing Questions?

If you have any further questions regarding purchasing or licensing, please [contact us](http://www.actiprosoftware.com/company/contact).  We're happy to help.
