using Avalonia.Markup.Xaml;

namespace ActiproSoftware.Properties.Bars.Mvvm {

	/// <summary>
	/// A markup extension that loads an assembly's string resource.
	/// </summary>
	public class SRExtension : SRExtensionBase<SRName> {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="SRExtensionBase{TName}.SRExtensionBase()"/>
		public SRExtension() { }

		/// <inheritdoc cref="SRExtensionBase{TName}.SRExtensionBase(TName)"/>
		public SRExtension(SRName name) : base(name) { }
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc/>
		[ConstructorArgument("name")]
		public override SRName Name { get; set; }

		/// <inheritdoc/>
		public override string? ProvideValue()
			=> SR.GetString(Name);

	}

}
