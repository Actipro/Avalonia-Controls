using ActiproSoftware.UI.Avalonia.Controls.Converters;
using Avalonia;
using Avalonia.Controls;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Presents a scrollable document.
	/// </summary>
	public class DocumentViewer : ContentControl {
		
		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="DocumentMargin"/> property.
		/// </summary>
		public static readonly StyledProperty<Thickness> DocumentMarginProperty
			= AvaloniaProperty.Register<DocumentViewer, Thickness>(nameof(DocumentMargin), new Thickness(50,50,50,30));
		
		/// <summary>
		/// Defines the <see cref="MaxDocumentWidth"/> property.
		/// </summary>
		public static readonly StyledProperty<double> MaxDocumentWidthProperty 
			= AvaloniaProperty.Register<DocumentViewer, double>(nameof(MaxDocumentWidth), 1100.0);

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a <see cref="DocumentViewer"/> for the specified Markdown resource file.
		/// </summary>
		/// <param name="markdownFilePath">The path to the Markdown resource file.</param>
		/// <returns>The <see cref="DocumentViewer"/> that was created.</returns>
		public static DocumentViewer? CreateForMarkdown(string markdownFilePath) {
			// Load the embedded resource file
			string? markdown = null;
			markdownFilePath = ("ActiproSoftware" + markdownFilePath).Replace("/", ".");
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(markdownFilePath)) {
				if (stream != null) {
					using (var reader = new StreamReader(stream)) {
						markdown = reader.ReadToEnd();
					}
				}
			}

			if (!string.IsNullOrEmpty(markdown)) {
				// Create the presenter
				var content = new MarkdownDocumentConverter().Convert(markdown, typeof(Control), null, CultureInfo.CurrentCulture) as Control;
				if (content != null) {
					return new DocumentViewer() {
						Content = content
					};
				}
			}
			
			return null;
		}
		
		/// <summary>
		/// The margin around the document content.
		/// </summary>
		public Thickness DocumentMargin {
			get => GetValue(DocumentMarginProperty);
			set => SetValue(DocumentMarginProperty, value);
		}
		
		/// <summary>
		/// The maximum document width.
		/// </summary>
		public double MaxDocumentWidth {
			get => GetValue(MaxDocumentWidthProperty);
			set => SetValue(MaxDocumentWidthProperty, value);
		}
		
	}

}
