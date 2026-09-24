using System.Collections.Specialized;

namespace ActiproSoftware.SampleBrowser;

/// <summary>
/// Provides information about a product family.
/// </summary>
public class ProductFamilyInfo {

	private IEnumerable<IGrouping<string?, ProductItemInfo>>? _groupedItems;
	private ProductItemInfo? _overviewItem;

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public ProductFamilyInfo() {
		Items.CollectionChanged += OnItemsCollectionChanged;
	}

	// --------------------------------------------------------------------------------------------------
	// NON-PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// Occurs when the items collection has changed.
	/// </summary>
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

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The blurb text, such as <c>NEW!</c>.
	/// </summary>
	public string? BlurbText { get; set; }

	/// <summary>
	/// The online feature summary URL.
	/// </summary>
	public string FeatureSummaryUrl
		=> string.Format(CultureInfo.InvariantCulture, "https://www.actiprosoftware.com/products/controls/avalonia/{0}", ShortTitle?.Replace(" ", string.Empty).ToLowerInvariant());

	/// <summary>
	/// The first <see cref="ProductItemInfo"/> object.
	/// </summary>
	public ProductItemInfo? FirstItem
		=> OverviewItem ?? Items.FirstOrDefault();

	/// <summary>
	/// Returns a collection of <see cref="ProductItemInfo"/> objects for all items, grouped by category name.
	/// </summary>
	/// <param name="includePrivateItems">Whether to include private items.</param>
	public IEnumerable<IGrouping<string?, ProductItemInfo>> GetGroupedItems(bool includePrivateItems)
		=> Items.Where(i => i != OverviewItem && (includePrivateItems || !i.IsPrivate)).GroupBy(i => i.Category);

	/// <summary>
	/// The collection of <see cref="ProductItemInfo"/> objects for all items that are not private, grouped by category name.
	/// </summary>
	public IEnumerable<IGrouping<string?, ProductItemInfo>> GroupedItems
		=> _groupedItems ??= GetGroupedItems(includePrivateItems: false);

	/// <summary>
	/// Whether there is any blurb text.
	/// </summary>
	public bool HasBlurbText
		=> !string.IsNullOrEmpty(BlurbText);

	/// <summary>
	/// Whether the product family is a Pro (paid) product.
	/// </summary>
	public bool IsPro { get; set; }

	/// <summary>
	/// The collection of items.
	/// </summary>
	[Content]
	public ObservableCollection<ProductItemInfo> Items { get; } = [];

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
	public ObservableCollection<ListItemInfo> News { get; } = [];

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
	public ObservableCollection<string> TeaserBulletItems { get; } = [];

	/// <summary>
	/// The family title.
	/// </summary>
	public string? Title { get; set; }

	/// <summary>
	/// The family version.
	/// </summary>
	public Version? Version { get; set; }

}
