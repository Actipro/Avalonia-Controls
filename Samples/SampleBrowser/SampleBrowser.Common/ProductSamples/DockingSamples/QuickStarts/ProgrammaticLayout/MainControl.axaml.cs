using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia;
using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.ProgrammaticLayout {

	public partial class MainControl : UserControl {

		private int _windowIndex;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the <see cref="DockSite"/>.
		/// </summary>
		private void CreateDockSite() {
			// Make sure tabs are programmatically added to the end
			dockSite.AreNewTabsInsertedBeforeExistingTabs = false;

			// Add a Workspace with Tabbed MDI
			var workspace = new Workspace() {
				Content = new TabbedMdiHost()
			};
			dockSite.Child = workspace;

			// Add a couple tool windows attached to each other on the right that are 300px wide
			var toolWindowR1 = CreateToolWindow("DockedRight-1");
			toolWindowR1.ContainerDockedSize = new Size(300, 200);
			var toolWindowR2 = CreateToolWindow("DockedRight-2");
			toolWindowR1.Dock(dockSite, Dock.Right);
			toolWindowR2.Attach(toolWindowR1);

			// Dock bottom
			var toolWindowB = CreateToolWindow("DockedBottom");
			toolWindowB.Dock(workspace, Dock.Bottom);

			// Auto hide left
			var toolWindowAH = CreateToolWindow("Auto-Hidden");
			toolWindowAH.AutoHide(Dock.Left);

			// Floating
			var toolWindowU = CreateToolWindow("Floating");
			toolWindowU.ContainerDockedSize = new Size(400, 200);
			toolWindowU.Float(new Point(200, 150));

			// Add three documents
			var documentWindow1 = CreateDocumentWindow("Upper-1");
			documentWindow1.Open();
			var documentWindow2 = CreateDocumentWindow("Upper-2");
			documentWindow2.Open();
			var documentWindow3 = CreateDocumentWindow("Lower");
			documentWindow3.Open();
			documentWindow3.MoveToNewHorizontalContainer();

			// Make sure new tabs are inserted before existing tabs again
			dockSite.AreNewTabsInsertedBeforeExistingTabs = true;
		}

		/// <summary>
		/// Creates a new <see cref="DocumentWindow"/>.
		/// </summary>
		/// <param name="title">The title to use.</param>
		/// <returns>The <see cref="DocumentWindow"/> that was created.</returns>
		private DocumentWindow CreateDocumentWindow(string title) {
			var index = ++_windowIndex;

			// Create a TextBox
			var textBox = new TextBox() {
				BorderThickness = new Thickness(),
				Text = $"Document window {index} created at {DateTime.Now}.",
				TextWrapping = TextWrapping.Wrap,
			};

			// Create the window (using this constructor registers the document window with the DockSite)
			var window = new DocumentWindow(dockSite,
				serializationId: $"DocumentWindow{index}",
				title,
				ImageLoader.GetIcon("TextDocument16.png"),
				content: textBox);

			return window;
		}

		/// <summary>
		/// Creates a new <see cref="ToolWindow"/>.
		/// </summary>
		/// <param name="title">The title to use.</param>
		/// <returns>The <see cref="ToolWindow"/> that was created.</returns>
		private ToolWindow CreateToolWindow(string title) {
			var index = ++_windowIndex;

			// Create a TextBox
			var textBox = new TextBox() {
				BorderThickness = new Thickness(),
				Text = $"Tool window {index} created at {DateTime.Now}.",
				TextWrapping = TextWrapping.Wrap,
			};

			// Create the window (using this constructor registers the tool window with the DockSite)
			var window = new ToolWindow(dockSite,
				serializationId: $"ToolWindow{index}",
				title,
				ImageLoader.GetIcon("Properties16.png"),
				content: textBox);

			return window;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		protected override void OnLoaded(RoutedEventArgs e) {
			base.OnLoaded(e);

			if (dockSite.ToolWindows.Count == 0)
				CreateDockSite();
		}

	}

}
