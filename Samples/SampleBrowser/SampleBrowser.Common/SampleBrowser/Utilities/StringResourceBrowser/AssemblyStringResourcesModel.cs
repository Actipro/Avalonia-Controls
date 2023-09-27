using System;
using System.Collections.Generic;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser {

	/// <summary>
	/// String resources in an assembly.
	/// </summary>
	public record AssemblyStringResourcesModel(Type SRType, Type EnumType) {

		private string? _assemblyName;
		private List<StringResourceModel>? _resources;
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		private List<StringResourceModel> CreateResources() {
			var enumValues = Enum.GetValues(EnumType);

			var list = new List<StringResourceModel>(enumValues.Length);
			foreach (var enumValue in enumValues)
				list.Add(new StringResourceModel(SRType, enumValue));

			return list;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The assembly name.
		/// </summary>
		public string AssemblyName {
			get {
				if (string.IsNullOrEmpty(_assemblyName)) {
					var namespacePrefix = "ActiproSoftware.Properties.";

					_assemblyName = SRType.Namespace ?? string.Empty;
					if (_assemblyName.StartsWith(namespacePrefix, StringComparison.InvariantCultureIgnoreCase))
						_assemblyName = _assemblyName.Substring(namespacePrefix.Length);
				}

				return _assemblyName;
			}
		}

		/// <summary>
		/// The collection of string resources.
		/// </summary>
		public IList<StringResourceModel>? Resources
			=> _resources ??= CreateResources();

	}

}
