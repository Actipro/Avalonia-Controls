using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common {

	/// <summary>
	/// Provides common code for working with documents in the various samples.
	/// </summary>
	public static class DocumentHelper {
	
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// Creates a new text <see cref="DocumentWindow"/>.
		/// </summary>
		/// <param name="dockSite">The owner <see cref="DockSite"/>.</param>
		/// <param name="fileName">The file to open; <c>null</c> to create a new document.</param>
		/// <param name="documentIndex">The document index, if a new document is being created.</param>
		/// <param name="defaultText">The default text to be loaded into the document.</param>
		/// <returns>The <see cref="DocumentWindow"/> that was created.</returns>
		private static DocumentWindow CreateTextDocumentWindow(DockSite dockSite, string? fileName, int documentIndex, string? defaultText) {
			ArgumentNullException.ThrowIfNull(dockSite);

			string name, title;
			var text = defaultText;
			if (fileName is not null) {
				// Open an existing document
				if (File.Exists(fileName))
					text = File.ReadAllText(fileName);
				name = Path.GetFileNameWithoutExtension(fileName);
				title = Path.GetFileName(fileName);
			}
			else {
				// Create a new document
				text ??= string.Format("Document {0} created at {1}.", documentIndex, DateTime.Now);
				name = string.Format("Document{0}", documentIndex);
				title = string.Format("Document{0}.txt", documentIndex);
				fileName = title;
			}

			// Create a TextBox
			var textBox = new TextBox() {
				BorderThickness = new Thickness(0),
				Text = text,
				TextWrapping = TextWrapping.Wrap,
				[ScrollViewer.VerticalScrollBarVisibilityProperty] = ScrollBarVisibility.Auto,
			};

			// Create the document
			var documentWindow = new DocumentWindow(dockSite, name, title, ImageLoader.GetIcon("TextDocument16.png"), textBox) {
				Description = "Text document",
				FileName = fileName,
			};

			// Activate the document
			documentWindow.Activate();

			return documentWindow;
		}

		private static IStorageProvider GetStorageProvider(Visual visual)
			=> TopLevel.GetTopLevel(visual)?.StorageProvider
				?? throw new InvalidOperationException("Unable to resolve the storage provider.");

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a new text <see cref="DocumentWindow"/>.
		/// </summary>
		/// <param name="dockSite">The owner <see cref="DockSite"/>.</param>
		/// <param name="documentIndex">The document index.</param>
		/// <returns>The <see cref="DocumentWindow"/> that was created.</returns>
		public static DocumentWindow CreateTextDocumentWindow(DockSite dockSite, int documentIndex, string? text = null)
			=> CreateTextDocumentWindow(dockSite, fileName: null, documentIndex, text);

		/// <summary>
		/// Shows an open file dialog and creates a <see cref="DocumentWindow"/> when a file is picked.
		/// </summary>
		/// <param name="dockSite">The owner <see cref="DockSite"/>.</param>
		/// <returns>The <see cref="DocumentWindow"/> that was created, if any.</returns>
		public async static Task<DocumentWindow?> OpenTextDocumentWindowAsync(DockSite dockSite) {
			var options = new FilePickerOpenOptions() {
				AllowMultiple = false,
				FileTypeFilter = [new FilePickerFileType("Text files (*.txt)") { Patterns = ["*.txt"] }],
			};
			var results = await GetStorageProvider(dockSite).OpenFilePickerAsync(options);
			if (results?.Count == 1) {
				var storageFile = results[0];
				if ((storageFile?.Path is { } path) && path.IsAbsoluteUri && path.IsFile) {
					// Create a document window
					return CreateTextDocumentWindow(dockSite, path.AbsolutePath, documentIndex: 0, defaultText: null);
				}

			}

			return null;
		}
		
	}

}
