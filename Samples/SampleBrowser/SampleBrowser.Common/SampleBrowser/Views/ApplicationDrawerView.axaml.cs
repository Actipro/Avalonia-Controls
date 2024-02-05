using Avalonia.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the user control for the home page.
	/// </summary>
	public partial class ApplicationDrawerView : UserControl {

		private bool _isSelectingDrawerSection;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ApplicationDrawerView() {
			InitializeComponent();

			Debug.Assert(ApplicationViewModel.Instance.ProductData is not null);
			drawerSectionComboBox.ItemsSource = ApplicationViewModel.Instance.ProductData?.AppDrawerItems;
			drawerSectionComboBox.SelectionChanged += OnDrawerSectionComboBoxSelectionChanged;

			itemListBox.ContainerPrepared += OnItemListBoxContainerPrepared;
			itemListBox.SelectionChanged += OnItemListBoxSelectionChanged;

			SynchronizeViewItemInfo();
			ApplicationViewModel.Instance.PropertyChanged += OnViewModelPropertyChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		private void OnDrawerSectionComboBoxSelectionChanged(object? sender, SelectionChangedEventArgs e) {
			// Update the product items list to match the selection
			RebuildItemsSource();
		}

		private void OnItemListBoxContainerPrepared(object? sender, ContainerPreparedEventArgs e) {
			// Category items should be disabled
			e.Container.IsEnabled = e.Container.DataContext is ProductItemInfo;
		}

		private void OnItemListBoxSelectionChanged(object? sender, SelectionChangedEventArgs e) {
			var itemInfo = e.AddedItems?.OfType<ProductItemInfo>().FirstOrDefault();
			if ((itemInfo is not null) && (ApplicationViewModel.Instance.ViewItemInfo != itemInfo))
				ApplicationViewModel.Instance.NavigateViewToItemInfo(itemInfo);
		}

		private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(ApplicationViewModel.ViewItemInfo))
				SynchronizeViewItemInfo();
			else if (e.PropertyName == nameof(ApplicationViewModel.ShowPrivateItems))
				RebuildItemsSource();
		}

		private void RebuildItemsSource() {
			var itemsSource = new List<object>();
			var selectedDrawerSection = SelectedDrawerSection;
			if (selectedDrawerSection is not null) {
				// OverviewItem is not part of the grouped collection
				if (selectedDrawerSection.ProductFamily?.OverviewItem is ProductItemInfo overviewItem)
					itemsSource.Add(overviewItem);

				// Add categorized items
				if (selectedDrawerSection.ProductFamily?.GetGroupedItems(ApplicationViewModel.Instance.ShowPrivateItems) is IEnumerable<IGrouping<string, ProductItemInfo>> groupedItems) {
					foreach (var grouping in groupedItems) {
						// Add the category
						if (!string.IsNullOrEmpty(grouping.Key))
							itemsSource.Add(grouping.Key);

						// Add each item in the category
						foreach (var itemInfo in grouping)
							itemsSource.Add(itemInfo);
					}
				}
			}

			// Set the items source
			itemListBox.ItemsSource = itemsSource;

			// User-initiated change in selection should auto-select the first item from the family
			if (!_isSelectingDrawerSection) {
				var itemInfo = selectedDrawerSection?.ProductFamily?.FirstItem;
				if (itemInfo is not null)
					ApplicationViewModel.Instance.NavigateViewToItemInfo(itemInfo);
				else
					ApplicationViewModel.Instance.NavigateViewToHome(transitionForward: true);
			}
		}

		private AppDrawerSectionInfo? SelectedDrawerSection {
			get => drawerSectionComboBox.SelectedItem as AppDrawerSectionInfo;
			set {
				if (SelectedDrawerSection != value) {
					_isSelectingDrawerSection = true;
					try {
						drawerSectionComboBox.SelectedItem = value;
					}
					finally {
						_isSelectingDrawerSection = false;
					}
				}
			}
		}

		private void SynchronizeViewItemInfo() {
			var currentItemInfo = ApplicationViewModel.Instance.ViewItemInfo;
			var currentProductFamily = ApplicationViewModel.Instance.ViewUsesHomeDrawer ? null : currentItemInfo?.ProductFamily;
			var currentDrawerSection = drawerSectionComboBox.ItemsView.OfType<AppDrawerSectionInfo>().FirstOrDefault(i => i.ProductFamily == currentProductFamily);

			// Sync the combobox
			if ((drawerSectionComboBox.SelectedItem != currentDrawerSection) && drawerSectionComboBox.ItemsView.Contains(currentDrawerSection))
				SelectedDrawerSection = currentDrawerSection;

			// Sync the listbox
			if ((itemListBox.SelectedItem != currentItemInfo) && itemListBox.ItemsView.Contains(currentItemInfo))
				itemListBox.SelectedItem = currentItemInfo;
		}

	}

}
