namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides information about an app drawer item.
	/// </summary>
	public class AppDrawerSectionInfo : ListItemInfo {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Whether the product family is a Pro (paid) product.
		/// </summary>
		public bool IsPro
			=> ProductFamily?.IsPro ?? false;

		/// <summary>
		/// The <see cref="ProductFamilyInfo"/> related to this item.
		/// </summary>
		public ProductFamilyInfo? ProductFamily { get; set; }

	}

}
