using System.Resources;

namespace ActiproSoftware.Properties.Bars.Mvvm {

	/// <summary>
	/// Provides access to the string resources of this assembly, also allowing for their customization.
	/// </summary>
	/// <remarks>
	/// Call the <see cref="GetString"/> method to return a resolved resource string.
	/// If a custom string has been set for a specified string resource name, it will be returned.
	/// Otherwise, the default string resource value is returned.
	/// <para>
	/// If any of the resource strings are customized via a call to <see cref="SetCustomString"/>,
	/// it is best to do so before any other classes in this assembly are referenced,
	/// such as in the application startup.
	/// </para>
	/// </remarks>
	public sealed class SR : SRBase {

		private static volatile SR? _instance;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private SR() { }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private static SR Instance
			=> _instance ??= new();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="SRBase.ClearCustomStringsCore"/>
		public static void ClearCustomStrings()
			=> Instance.ClearCustomStringsCore();

		/// <inheritdoc cref="SRBase.ContainsCustomStringCore"/>
		public static bool ContainsCustomString(SRName name)
			=> Instance.ContainsCustomStringCore(name.ToString());

		/// <inheritdoc cref="SRBase.GetCustomStringCore"/>
		public static string? GetCustomString(SRName name)
			=> Instance.GetCustomStringCore(name.ToString());

		/// <inheritdoc cref="SRBase.GetStringCore"/>
		public static string? GetString(SRName name, params object[] args)
			=> Instance.GetStringCore(name.ToString(), args);

		/// <inheritdoc cref="SRBase.RemoveCustomStringCore"/>
		public static void RemoveCustomString(SRName name)
			=> Instance.RemoveCustomStringCore(name.ToString());

		/// <inheritdoc/>
		public override ResourceManager ResourceManager
			=> Resources.ResourceManager;

		/// <inheritdoc cref="SRBase.SetCustomStringCore"/>
		public static void SetCustomString(SRName name, string value)
			=> Instance.SetCustomStringCore(name.ToString(), value);

	}

}
