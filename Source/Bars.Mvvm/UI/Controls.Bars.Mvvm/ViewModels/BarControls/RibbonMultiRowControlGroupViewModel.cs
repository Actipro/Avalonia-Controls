using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a multi-row control group control within a ribbon group.
	/// </summary>
	public class RibbonMultiRowControlGroupViewModel : ObservableObjectBase {

		private bool _isVisible = true;
		private ICollection<int>? _threeRowItemSortOrder = new Collection<int>();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <summary>
		/// The collection of items in the control.
		/// </summary>
		public ObservableCollection<object> Items { get; } = new();
		
		/// <summary>
		/// A collection of integers that indicates the indices of how items should be sorted when in a three-row layout.
		/// </summary>
		public ICollection<int>? ThreeRowItemSortOrder {
			get => _threeRowItemSortOrder;
			set => SetProperty(ref _threeRowItemSortOrder, value);
		}
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[{this.Items.Count} item(s)']";

	}

}
