using System.Windows.Input;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides information about a product item.
	/// </summary>
	public class ProductItemInfo {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The blurb text, such as <c>NEW!</c>.
		/// </summary>
		public string? BlurbText { get; set; }
		
		/// <summary>
		/// The item's category.
		/// </summary>
		public string? Category { get; set; }

		/// <summary>
		/// The path to the folder containing the product item.
		/// </summary>
		public string? FolderPath {
			get {
				var path = Path;
				if (!string.IsNullOrEmpty(path)) {
					var lastSlashIndex = path.LastIndexOf('/');
					if (lastSlashIndex != -1)
						path = path[..lastSlashIndex];
				}

				return path;
			}
		}

		/// <summary>
		/// Whether there is any blurb text.
		/// </summary>
		public bool HasBlurbText
			=> !string.IsNullOrEmpty(BlurbText);

		/// <summary>
		/// Whether the item has a custom status bar, which prevents the default status bar from displaying.
		/// </summary>
		public bool HasCustomStatusBar { get; set; }
		
		/// <summary>
		/// Whether this item shows the <see cref="FolderPath"/> in the status bar.
		/// </summary>
		public bool HasFolderPathInStatusBar
			=> !IsProductOverview;

		/// <summary>
		/// Whether this item can be grouped by a category in lists.
		/// </summary>
		public bool IsGroupedByCategory
			=> !string.IsNullOrEmpty(Category) && !IsProductOverview;

		/// <summary>
		/// Whether this item is a private item not intended for inclusion in public projects.
		/// </summary>
		public bool IsPrivate { get; set; }

		/// <summary>
		/// Whether this item is a product overview document.
		/// </summary>
		public bool IsProductOverview
			=> ProductFamily?.OverviewItem == this;

		/// <summary>
		/// The <see cref="ICommand"/> that navigates the view to a <see cref="ProductItemInfo"/>.
		/// </summary>
		public ICommand NavigateViewToItemInfoCommand
			=> ApplicationViewModel.Instance.NavigateViewToItemInfoCommand;

		/// <summary>
		/// The next <see cref="ProductItemInfo"/>, if any.
		/// </summary>
		public ProductItemInfo? NextItem {
			get {
				if (ProductFamily is not null) {
					var index = ProductFamily.Items.IndexOf(this);
					if (index < ProductFamily.Items.Count - 1)
						return ProductFamily.Items[index + 1];
				}

				return null;
			}
		}

		/// <summary>
		/// The file path to the sample.
		/// </summary>
		public string? Path { get; set; }

		/// <summary>
		/// The previous <see cref="ProductItemInfo"/>, if any.
		/// </summary>
		public ProductItemInfo? PreviousItem {
			get {
				if (ProductFamily is not null) {
					var index = ProductFamily.Items.IndexOf(this);
					if (index > 0)
						return ProductFamily.Items[index - 1];
					else if ((index == 0) && (ProductFamily.OverviewItem != null))
						return ProductFamily.OverviewItem;
				}

				return null;
			}
		}

		/// <summary>
		/// The <see cref="ProductFamilyInfo"/> that owns this item.
		/// </summary>
		public ProductFamilyInfo? ProductFamily { get; set; }

		/// <summary>
		/// The item title.
		/// </summary>
		public string? Title { get; set; }

	}

}
