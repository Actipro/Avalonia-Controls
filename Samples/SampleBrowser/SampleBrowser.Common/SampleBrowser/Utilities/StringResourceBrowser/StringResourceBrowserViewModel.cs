using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AP = ActiproSoftware.Properties;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser {

	/// <summary>
	/// The string resource browser view model.
	/// </summary>
	public class StringResourceBrowserViewModel : ObservableObjectBase {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		[DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(AP.Shared.SR))] // Required to ensure SR.GetString method is available
		public StringResourceBrowserViewModel() {
			// Explicitly reference the string resource enum so reflection metadata is not trimmed
			_ = Enum.GetValues<AP.Shared.SRName>();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The collection of all assemblies with string resources.
		/// </summary>
		/// <remarks>
		/// This property must be updated whenever string resources are added to a new assembly.
		/// </remarks>
		public IEnumerable<AssemblyStringResourcesModel> All {
			get {
				return new AssemblyStringResourcesModel[] {
					new AssemblyStringResourcesModel(typeof(AP.Bars.SR), typeof(AP.Bars.SRName)),
					new AssemblyStringResourcesModel(typeof(AP.Docking.SR), typeof(AP.Docking.SRName)),
					new AssemblyStringResourcesModel(typeof(AP.Fundamentals.SR), typeof(AP.Fundamentals.SRName)),
					new AssemblyStringResourcesModel(typeof(AP.Shared.SR), typeof(AP.Shared.SRName)),
				};
			}
		}

	}

}
