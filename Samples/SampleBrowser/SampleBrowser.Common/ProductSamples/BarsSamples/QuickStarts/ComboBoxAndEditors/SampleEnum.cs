namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ComboBoxAndEditors {

	public enum SampleEnum {

		// By default, values without attribute will use their name label
		// NOTE: The default order is 10000, so this item can be moved after the other unordered items by setting a higher value
		[System.ComponentModel.DataAnnotations.Display(Order = 10001)]
		None,

		// EditorBrowsableAttribute can be used to hide an item from the combobox
		[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		Hidden,

		// DescriptionAttribute can be used for label
		[System.ComponentModel.Description("Description Item")]
		DescriptionItem,

		// Use DisplayAttribute for the most control and localization support
		//	Name		-> Label
		//	Description	-> ToolTip
		//	GroupName	-> Category
		[System.ComponentModel.DataAnnotations.Display(Order = 1, Name = "Display Item 1", Description = "Description of Display Item 1", GroupName = "Using DisplayAttribute")]
		DisplayItem1,

		[System.ComponentModel.DataAnnotations.Display(Order = 3, Name = "Display Item 3", Description = "Description of Display Item 3", GroupName = "Using DisplayAttribute")]
		DisplayItem3, // NOTE: This item intentionally defined out of order to demonstrate the Order attribute property

		[System.ComponentModel.DataAnnotations.Display(Order = 2, Name = "Display Item 2", Description = "Description of Display Item 2", GroupName = "Using DisplayAttribute")]
		DisplayItem2,

	}


}
