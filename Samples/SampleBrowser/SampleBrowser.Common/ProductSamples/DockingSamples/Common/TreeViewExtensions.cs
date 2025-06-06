using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.DockingSamples.Common {

	/// <summary>
	/// Defines extension methods for <see cref="TreeView"/> and <see cref="TreeViewItem"/>.
	/// </summary>
	public static class TreeViewExtensions {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Expands all <see cref="TreeViewItem"/> controls.
		/// </summary>
		/// <param name="itemsControl">The items control (e.g., <see cref="TreeView"/> or <see cref="TreeViewItem"/>).</param>
		private static void ExpandAllTreeViewItems(this ItemsControl itemsControl) {
			foreach (var item in itemsControl.Items) {
				if (item is null)
					continue;

				var treeViewItem = item as TreeViewItem
					?? itemsControl.ContainerFromItem(item) as TreeViewItem;

				treeViewItem?.ExpandAll();
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Exapnds all items.
		/// </summary>
		/// <param name="treeView">The tree view control.</param>
		/// <remarks>
		/// This extension method was added because <see cref="TreeView.ExpandSubTree(TreeViewItem)"/>
		/// would not consistently expand the entire tree.
		/// </remarks>
		public static void ExpandAll(this TreeView treeView)
			=> ExpandAllTreeViewItems(treeView);

		/// <summary>
		/// Expands the tree view item and its children.
		/// </summary>
		/// <param name="treeViewItem">The tree view item.</param>
		public static void ExpandAll(this TreeViewItem treeViewItem) {
			treeViewItem.IsExpanded = true;
			ExpandAllTreeViewItems(treeViewItem);
		}

	}

}
