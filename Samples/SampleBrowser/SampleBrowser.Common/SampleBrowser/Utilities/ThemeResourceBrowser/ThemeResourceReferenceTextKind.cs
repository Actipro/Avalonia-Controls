using System.ComponentModel;

namespace ActiproSoftware.SampleBrowser.Utilities.ThemeResourceBrowser {

	/// <summary>
	/// The kind of reference text available for each theme resource.
	/// </summary>
	public enum ThemeResourceReferenceTextKind {
		[Description("XAML Dynamic Resource")]
		XamlDynamicResource,

		[Description("XAML Static Resource")]
		XamlStaticResource,

		[Description("C# Resource Key")]
		CSharpToResourceKey,
	}

}
