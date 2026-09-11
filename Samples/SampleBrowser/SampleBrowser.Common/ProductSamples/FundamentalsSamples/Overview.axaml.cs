using ActiproSoftware.UI.Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples;

public partial class Overview : UserControl {

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public Overview() {
		InitializeComponent();

		updateIntervalSegmentedBar.SelectionChanged += OnUpdateIntervalSegmentedBarSelectionChanged;
		UpdateNextUpdateText();
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	private void OnUpdateIntervalSegmentedBarSelectionChanged(object? sender, SelectionChangedEventArgs e)
		=> UpdateNextUpdateText();

	private void UpdateNextUpdateText() {
		if (updateIntervalSegmentedBar.SelectedItem is SegmentedBarItem { Tag: string text })
			nextUpdateTextPresenter.Text = text;
	}

}
