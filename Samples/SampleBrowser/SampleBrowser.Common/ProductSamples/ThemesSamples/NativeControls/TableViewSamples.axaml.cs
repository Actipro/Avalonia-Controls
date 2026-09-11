using ActiproSoftware.ProductSamples.ThemesSamples.Common;

namespace ActiproSoftware.ProductSamples.ThemeSamples.NativeControls;

public partial class TableViewSamples : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public TableViewSamples() {
		InitializeComponent();

		// Create the data source
		var fruitList = new ObservableCollection<FruitDataItemModel> {
			new() { Name = "Apple", ColorCategory = "Red", LeadingProducer = "China", ServingCalories = 93, IsPopular = true },
			new() { Name = "Cherry", ColorCategory = "Red", LeadingProducer = "Turkey", ServingCalories = 99, IsPopular = false },
			new() { Name = "Guava", ColorCategory = "Red", LeadingProducer = "India", ServingCalories = 112, IsPopular = false },
			new() { Name = "Banana", ColorCategory = "Yellow/Orange", LeadingProducer = "India", ServingCalories = 105, IsPopular = true },
			new() { Name = "Grapefruit", ColorCategory = "Yellow/Orange", LeadingProducer = "China", ServingCalories = 41, IsPopular = false },
			new() { Name = "Lemon", ColorCategory = "Yellow/Orange", LeadingProducer = "India", ServingCalories = 61, IsPopular = false },
			new() { Name = "Orange", ColorCategory = "Yellow/Orange", LeadingProducer = "Brazil", ServingCalories = 62, IsPopular = true },
			new() { Name = "Pineapple", ColorCategory = "Yellow/Orange", LeadingProducer = "Brazil", ServingCalories = 83, IsPopular = false },
			new() { Name = "Lime", ColorCategory = "Green", LeadingProducer = "China", ServingCalories = 20, IsPopular = false },
			new() { Name = "Kiwi", ColorCategory = "Green", LeadingProducer = "China", ServingCalories = 110, IsPopular = false },
			new() { Name = "Plum", ColorCategory = "Blue/Purple", LeadingProducer = "China", ServingCalories = 76, IsPopular = false },
		};

		// Configure table view
		sample.ItemsSource = fruitList;
	}

}
