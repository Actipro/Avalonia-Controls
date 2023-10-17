using System.Collections.Generic;
using AP = ActiproSoftware.Properties;

namespace ActiproSoftware.SampleBrowser.Utilities.StringResourceBrowser {

	/// <summary>
	/// The string resource browser view model.
	/// </summary>
	public class StringResourceBrowserViewModel : ObservableObjectBase {

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
					// Empty right now so temporarily excluding
					// new AssemblyStringResourcesModel(typeof(AP.Fundamentals.SR), typeof(AP.Fundamentals.SRName)),
					new AssemblyStringResourcesModel(typeof(AP.Shared.SR), typeof(AP.Shared.SRName)),
				};
			}
		}

	}

}
