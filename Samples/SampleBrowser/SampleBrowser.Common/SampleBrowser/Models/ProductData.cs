using Avalonia.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides product data information.
	/// </summary>
	public class ProductData {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Yields all <see cref="ProductItemInfo"/> instances anywhere in the product data.
		/// </summary>
		public IEnumerable<ProductItemInfo> AllItems {
			get {
				foreach (var familyInfo in ProductFamilies) {
					if (familyInfo.OverviewItem is not null)
						yield return familyInfo.OverviewItem;

					foreach (var itemInfo in familyInfo.Items)
						yield return itemInfo;
				}

				if (ReleaseHistory is not null) {
					foreach (var itemInfo in ReleaseHistory.Items)
						yield return itemInfo;
				}

				if (Utilities is not null) {
					foreach (var itemInfo in Utilities.Items)
						yield return itemInfo;
				}
			}
		}
		
		/// <summary>
		/// The options display in the app drawer's drop-down, which includes <c>Home</c>, all <see cref="ProductFamilies"/>, and <see cref="ReleaseHistory"/>.
		/// </summary>
		public IEnumerable<AppDrawerSectionInfo> AppDrawerItems {
			get {
				yield return new AppDrawerSectionInfo() {
					ImageSource = new UI.Avalonia.Images.HomeLogo48(),
					Title = "Home",
				};

				foreach (var productFamily in ProductFamilies) {
					yield return new AppDrawerSectionInfo() {
						ImageSource = productFamily.LogoImageSource,
						ProductFamily = productFamily,
						Title = productFamily.Title,
					};
				}

				if (ReleaseHistory is not null) {
					yield return new AppDrawerSectionInfo() {
						ImageSource = new UI.Avalonia.Images.ReleaseHistoryLogo48(),
						ProductFamily = ReleaseHistory,
						Title = "Release History",
					};
				}
			}
		}

		/// <summary>
		/// The build date text.
		/// </summary>
		public string BuildDateText {
			get {
				// Try to get the attribute
				var attr = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
				if (attr is not null) {
					var index = attr.InformationalVersion.IndexOf(" - ");
					if (index != -1) {
						index += 3;
						return new DateTime(
							Convert.ToInt32(attr.InformationalVersion.Substring(index, 4)),
							Convert.ToInt32(attr.InformationalVersion.Substring(index + 4, 2)),
							Convert.ToInt32(attr.InformationalVersion.Substring(index + 6, 2))
							).ToShortDateString();
					}
				}

				return "(Unknown)";
			}
		}
		
		/// <summary>
		/// The collection of featured sample items.
		/// </summary>
		public ObservableCollection<ListItemInfo> FeaturedSamples { get; } = new ();

		/// <summary>
		/// The collection of product families.
		/// </summary>
		[Content]
		public ObservableCollection<ProductFamilyInfo> ProductFamilies { get; } = new ();

		/// <summary>
		/// The collection of product families with news.
		/// </summary>
		public IEnumerable<ProductFamilyInfo> ProductFamiliesWithNews
			=> from pf in this.ProductFamilies.OfType<ProductFamilyInfo>()
			   where pf.News.Any()
			   orderby pf.NewsSortOrder, pf.News.Count descending
			   select pf;

		/// <summary>
		/// The product version text.
		/// </summary>
		public string ProductVersionText
			=> $"v{Properties.Shared.AssemblyInfo.Instance.VersionText[..4]}";

		/// <summary>
		/// The product version with build text.
		/// </summary>
		public string ProductVersionWithBuildText
			=> $"v{Properties.Shared.AssemblyInfo.Instance.VersionText}";
		
		/// <summary>
		/// The product version and date summary text.
		/// </summary>
		public string ProductVersionAndDateSummaryText
			=> $"{this.ProductVersionWithBuildText} on {this.BuildDateText}";

		/// <summary>
		/// The <see cref="ProductFamilyInfo"/> that contains release histories.
		/// </summary>
		public ProductFamilyInfo? ReleaseHistory { get; set; }

		/// <summary>
		/// The <see cref="ProductFamilyInfo"/> that contains utilities.
		/// </summary>
		public ProductFamilyInfo? Utilities { get; set; }

	}

}
