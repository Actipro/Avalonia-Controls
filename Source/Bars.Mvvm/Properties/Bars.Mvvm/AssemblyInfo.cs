// Global usings
global using global::Avalonia;
global using global::Avalonia.Controls;
global using global::Avalonia.Media;
global using global::Avalonia.Metadata;
global using global::System;
global using global::System.ComponentModel;
global using global::System.Globalization;
global using global::System.Linq;

using AI = ActiproSoftware.Properties.UIAssemblyInfoBase;


// XAML assembly attributes
[assembly: XmlnsPrefix(AI.ActiproXmlNamespace, "actipro")]
[assembly: XmlnsDefinition(AI.ActiproXmlNamespace, "ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm")]
[assembly: XmlnsDefinition(AI.ActiproXmlNamespace, "ActiproSoftware.UI.Avalonia.Themes.Bars.Mvvm")]

namespace ActiproSoftware.Properties.Bars.Mvvm {

	/// <summary>
	/// Retrieves information about the <c>Bars.Mvvm</c> assembly.
	/// </summary>
	public sealed class AssemblyInfo : UIAssemblyInfoBase {

		private static AssemblyInfo? _instance;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private AssemblyInfo() { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The instance of the <see cref="AssemblyInfo"/> class for this assembly.
		/// </summary>
		public static AssemblyInfo Instance => _instance ??= new AssemblyInfo();

		/// <inheritdoc/>
		public sealed override int ProductId => 0;

	}

}