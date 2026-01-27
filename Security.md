# Security Policy

## Reporting a Vulnerability

We take the security of Actipro products seriously and appreciate responsible disclosure.

If you believe you have found a security vulnerability affecting:

- **Actipro Avalonia Controls** *(commercial product)*
- Any open-source libraries, samples, utilities, or documentation in this repository

Contact us either through GitHub's **Report a vulnerability** button or one of the **Private Support** options listed on this page:
https://www.actiprosoftware.com/company/contact

> ⚠️ Do NOT open a public GitHub issue or create a public discussion forum thread for security concerns.

This public repository serves as the central location for receiving vulnerability reports for both the commercial **Actipro Avalonia Controls** product and related open-source assets. The commercial product's source code resides in a private repository, but all security reports should begin here.

## Scope

You may report vulnerabilities related to:

- The closed-source commercial **Actipro Avalonia Controls** product
- Open-source components, helpers, or utilities in this repository
- Sample code that could encourage insecure usage patterns  
- Documentation errors that could lead to insecure configuration or deployment  

## Supported Versions

Security updates apply to the following:

| Component | Supported for Security Fixes |
|---|---|
| Commercial Actipro Avalonia Controls | Yes (latest release) |
| Open-source code in this repo | Yes (latest release) |
| Documentation | Yes (latest release) |

Older releases are not maintained for security fixes.

## Our Disclosure Process

When you report a vulnerability, we follow a structured, coordinated workflow designed to protect customers while ensuring timely remediation. The process is the same whether the issue affects the open-source assets in this repository or the related commercial controls product, but the internal handling differs slightly depending on where the affected code lives.

1. **Acknowledgment**  
   We will confirm receipt of your report promptly.
   If additional information is needed to reproduce the issue, we will request it at this stage.

2. **Investigation**  
   We will assess severity, impact, and affected components.
   
   | Issues Related To | Investigation Location |
   |---|---|
   | Commercial products | Handled in the product's private repository. |
   | Open-source *(this repo)* | Triaged in a private GitHub Security Advisory. |

3. **Fix Development**  
   Fixes are developed without undue delay and privately to prevent exploitation before a patch is available.
   We may request additional details from the reporter if needed to ensure full remediation.

4. **Coordinated Disclosure**  
   We follow coordinated disclosure best practices and coordinate timing with the reporter.
   
   | Issues Related To | Disclosure Steps |
   |---|---|
   | Commercial products | Release patched NuGet packages, update documentation if needed, and publish a GitHub Security Advisory summarizing the issue and fix. |
   | Open-source *(this repo)* | Publish a GitHub Security Advisory summarizing the issue and fix. |

5. **Researcher Credit**  
   We are happy to acknowledge reporters unless anonymity is requested.
   We do not offer monetary bounties at this time.

## Security Best Practices for Users

To ensure secure usage of commercial Actipro controls products:

- Keep your Actipro NuGet packages updated to the latest version.
- Use the latest release of this repository's samples and documentation.
- Review Actipro documentation for secure configuration and deployment guidance.

## Non-Security Issues

For general questions, feature requests, or non-security bugs:

➡️ Use an option listed in our [Support](https://github.com/Actipro/.github/blob/main/Support.md) document.
