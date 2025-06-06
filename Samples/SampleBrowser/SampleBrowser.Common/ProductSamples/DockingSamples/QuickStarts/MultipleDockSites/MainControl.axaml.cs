using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia.Controls;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MultipleDockSites {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// Link the two dock sites together
			linkedDockSite1.LinkDockSite(linkedDockSite2);

			// Update lists of tool windows as tool windows shift between dock sites
			linkedDockSite1.ToolWindows.CollectionChanged += this.OnLinkedDockSite1ToolWindowsCollectionChanged;
			linkedDockSite2.ToolWindows.CollectionChanged += this.OnLinkedDockSite2ToolWindowsCollectionChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		private void OnLinkedDockSite1ToolWindowsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
			UpdateDockSiteToolWindowNames(linkedDockSite1, LinkedDockSite1ToolWindowNames);
		}

		private void OnLinkedDockSite2ToolWindowsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
			UpdateDockSiteToolWindowNames(linkedDockSite2, LinkedDockSite2ToolWindowNames);
		}

		private static void UpdateDockSiteToolWindowNames(DockSite dockSite, ObservableCollection<string?> names) {
			names.Clear();
			foreach (var toolWindow in dockSite.ToolWindows)
				names.Add(toolWindow.Title);
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ObservableCollection<string?> LinkedDockSite1ToolWindowNames { get; } = [];

		public ObservableCollection<string?> LinkedDockSite2ToolWindowNames { get; } = [];

	}

}
