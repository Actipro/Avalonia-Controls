using Avalonia.Media;
using Avalonia.Metadata;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides information about a product family.
	/// </summary>
	public class ProductFamilyInfo {

		private IEnumerable<IGrouping<string, ProductItemInfo>>? _groupedItems;
		private ProductItemInfo? _overviewItem;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public ProductFamilyInfo() {
			Items.CollectionChanged += OnItemsCollectionChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the items collection has changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="NotifyCollectionChangedEventArgs"/> that contains the event data.</param>
		private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
			// Clear the cached collection
			_groupedItems = null;

			// Wire up the parent product family references
			if (e.NewItems is not null) {
				foreach (var itemInfo in e.NewItems.OfType<ProductItemInfo>()) {
					if (itemInfo is not null)
						itemInfo.ProductFamily = this;
				}
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The blurb text, such as <c>NEW!</c>.
		/// </summary>
		public string? BlurbText { get; set; }

		/// <summary>
		/// The online feature summary URL.
		/// </summary>
		public string FeatureSummaryUrl
			=> string.Format(CultureInfo.InvariantCulture, "https://www.actiprosoftware.com/products/controls/avalonia/{0}", this.ShortTitle?.Replace(" ", string.Empty).ToLowerInvariant());

		/// <summary>
		/// The first <see cref="ProductItemInfo"/> object.
		/// </summary>
		public ProductItemInfo? FirstItem
			=> this.OverviewItem ?? Items.FirstOrDefault();

		/// <summary>
		/// Returns a collection of <see cref="ProductItemInfo"/> objects for all items, grouped by category name.
		/// </summary>
		/// <param name="includePrivateItems">Whether to include private items.</param>
		/// <returns>The collection of grouped items.</returns>
		public IEnumerable<IGrouping<string, ProductItemInfo>> GetGroupedItems(bool includePrivateItems) {
			return from i in Items
				   where i != this.OverviewItem && (includePrivateItems || !i.IsPrivate)
				   group i by i.Category;
		}

		/// <summary>
		/// The collection of <see cref="ProductItemInfo"/> objects for all items that are not private, grouped by category name.
		/// </summary>
		public IEnumerable<IGrouping<string, ProductItemInfo>> GroupedItems
			=> _groupedItems ??= GetGroupedItems(includePrivateItems: false);

		/// <summary>
		/// Whether there is any blurb text.
		/// </summary>
		public bool HasBlurbText
			=> !string.IsNullOrEmpty(this.BlurbText);


		/// <summary>
		/// Whether the product family is a Pro (paid) product.
		/// </summary>
		public bool IsPro { get; set; }

		/// <summary>
		/// The collection of items.
		/// </summary>
		[Content]
		public ObservableCollection<ProductItemInfo> Items { get; } = new();

		/// <summary>
		/// The logo <see cref="IImage"/>.
		/// </summary>
		public IImage? LogoImageSource { get; set; }

		/// <summary>
		/// The <see cref="ICommand"/> that navigates the view to a <see cref="ProductItemInfo"/>.
		/// </summary>
		public ICommand NavigateViewToItemInfoCommand
			=> ApplicationViewModel.Instance.NavigateViewToItemInfoCommand;

		/// <summary>
		/// The collection of news items.
		/// </summary>
		public ObservableCollection<ListItemInfo> News { get; } = new();

		/// <summary>
		/// The news sort order.
		/// </summary>
		public int NewsSortOrder { get; set; }

		/// <summary>
		/// The <see cref="ProductItemInfo"/> object for an overview.
		/// </summary>
		public ProductItemInfo? OverviewItem {
			get => _overviewItem;
			set {
				if (_overviewItem != value) {
					_overviewItem = value;
					if (_overviewItem is not null)
						_overviewItem.ProductFamily = this;
				}
			}
		}

		/// <summary>
		/// The family short title.
		/// </summary>
		public string? ShortTitle { get; set; }

		/// <summary>
		/// The family summary.
		/// </summary>
		public string? Summary { get; set; }

		/// <summary>
		/// The collection of teaser bullet items.
		/// </summary>
		public ObservableCollection<string> TeaserBulletItems { get; } = new();

		/// <summary>
		/// The family title.
		/// </summary>
		public string? Title { get; set; }

	}

}
