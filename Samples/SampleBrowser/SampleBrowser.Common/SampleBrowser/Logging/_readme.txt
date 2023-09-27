# Logging Support

The classes in this folder are used to integrate the Actipro logging framework
with Microsoft Extensions Logging library.

To enable logging, the MS_LOGGING constant must be defined for the project.
When defined, the Sample Browser will include the proper files and configure
logging during app startup.  SDK-style projects will also automatically include
any necessary NuGet packages.

Non-SDK-style projects will need to manually add a reference to the
"Microsoft.Extensions.Logging" NuGet package.