using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.Common {

	/// <summary>
	/// Provides a helper class for demonstrating how to work with recent documents to be used with <see cref="RecentDocumentControl"/>.
	/// </summary>
	public static class RecentDocumentHelper {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private static RecentDocumentItem CreateDemoRecentDocumentItem(Uri location, int documentIndex = -1, bool isPinned = false) {
			// Randomize the last opened date/time to simulate a real-world scenario of documents touched over various days and times
			var random = new Random();
			var dayOffset = -0.3 * (documentIndex == -1 ? random.NextInt64(0, 15) : documentIndex);
			var minOffset = random.NextDouble() * -200;
			var lastOpenedDateTime = DateTime.Now.AddDays(dayOffset).AddMinutes(minOffset);

			ExtractFileDetails(location, out var description, out var smallIcon, out var largeIcon);

			var recentDocumentItem = new RecentDocumentItem() {
				Description = description,
				LargeIcon = largeIcon,
				SmallIcon = smallIcon,
				IsPinned = isPinned,
				LastOpenedDateTime = lastOpenedDateTime,
				Location = location,
			};

			// When running in a browser, the sample paths may not be able to properly extract the document name from
			// the location.  Work around this issue on any platform by explicitly setting the document name for any
			// path that cannot extract just the file name
			if (location.IsAbsoluteUri) {
				var localPath = location.LocalPath;
				if ((Path.GetFileName(localPath) == localPath) && !localPath.EndsWith('\\'))
					recentDocumentItem.DocumentName = localPath.Substring(localPath.LastIndexOf('\\') + 1);
			}

			return recentDocumentItem;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of pre-defined <see cref="RecentDocumentItem"/> instances used for demonstration purposes.
		/// </summary>
		public static ObservableCollection<RecentDocumentItem> CreateDefaultCollection()
			=> new(CreateDefaultItems());

		/// <summary>
		/// Creates an enumerable of pre-defined <see cref="RecentDocumentItem"/> instances used for demonstration purposes.
		/// </summary>
		public static IEnumerable<RecentDocumentItem> CreateDefaultItems() {
			var documentIndex = 0;
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Software\EULA.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Resume.rtf"), documentIndex++, isPinned: true);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Personal\Vacation Planning.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Personal\Investment Notes.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Personal\Holiday Recipes.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Software\Release Notes.rtf"), documentIndex++, isPinned: true);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Financial Report Q4.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Personal\Birthday Gift Ideas.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Financial Report Q3.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Personal\Untitled Novel - A story about long file names and their impact on user interfaces.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Downloads\Actipro Software - Getting Started.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Downloads\How to Deliver Faster with Reusable Components.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Software\Feature Requests.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Financial Report Q2.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Personal\Bathroom Remodel Budget.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Financial Report Q1.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Side Project Domain Names.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Sales Presentation Notes.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Personal\Wish List.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\Work\Privacy Policy.rtf"), documentIndex++);
			yield return CreateDemoRecentDocumentItem(new Uri(@"C:\Documents\TODO List.rtf"), documentIndex++, isPinned: true);
		}

		/// <summary>
		/// Examines a file to output details about that file which can be used for a recent document item.
		/// </summary>
		/// <param name="location">The location of the file to examine.</param>
		/// <param name="description">Outputs a description of the file.</param>
		/// <param name="smallIcon">Outputs a small icon for the file.</param>
		/// <param name="largeIcon">Outputs a large icon for the file.</param>
		public static void ExtractFileDetails(Uri location, out string description, out IImage? smallIcon, out IImage? largeIcon) {
			var fileExt = Path.GetExtension(location.ToString()).ToUpperInvariant();
			switch (fileExt) {
				case ".RTF":
					description = "Rich-text file";
					smallIcon = ImageLoader.GetIcon("RichTextDocument16.png");
					largeIcon = ImageLoader.GetIcon("RichTextDocument32.png");
					break;
				default:
					description = fileExt.Substring(1) + " file";
					smallIcon = ImageLoader.GetIcon("BlankPage16.png");
					largeIcon = ImageLoader.GetIcon("BlankPage32.png");
					break;
			}
		}

		/// <summary>
		/// Provides a helper method for easily updating an existing document reference's last-opened date/time.
		/// If no existing document reference with the specified <see cref="Uri"/> exists, a new <see cref="RecentDocumentItem"/> is created.
		/// </summary>
		/// <param name="collection">The collection of recent documents.</param>
		/// <param name="location">A <see cref="Uri"/> indicating the location of the document.</param>
		/// <returns>The <see cref="RecentDocumentItem"/> that was updated or created.</returns>
		public static RecentDocumentItem NotifyDocumentOpened(ObservableCollection<RecentDocumentItem> collection, Uri location) {
			if (collection is null)
				throw new ArgumentNullException(nameof(collection));
			if (location is null)
				throw new ArgumentNullException(nameof(location));

			var recentDocumentItem = collection.FirstOrDefault(x => x.Location == location);
			if (recentDocumentItem is null) {
				// Create a new recent document item
				ExtractFileDetails(location, out var description, out var smallIcon, out var largeIcon);
				recentDocumentItem = new RecentDocumentItem() {
					Description = description,
					LargeIcon = largeIcon,
					SmallIcon = smallIcon,
					Location = location 
				};
				collection.Add(recentDocumentItem);
			}

			// Ensure "last opened" is updated
			recentDocumentItem.NotifyDocumentOpened();
			
			return recentDocumentItem;
		}

	}

}
