using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using System;
using System.Linq;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.ProgrammaticSizing {

	public partial class MainControl : UserControl {

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
		/// Occurs when the <c>Layout.EvenlyDistribute</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeMenuItemClick(object? sender, RoutedEventArgs e) {
			var descendants = dockSite.GetVisualDescendants().OfType<SplitContainer>();
			foreach (var splitContainer in descendants)
				splitContainer.ResizeSlots();
		}

		/// <summary>
		/// Occurs when the <c>Layout.EvenlyDistributeDocumentsOnly</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeDocumentsOnlyMenuItemClick(object? sender, RoutedEventArgs e) {
			if (dockSite.PrimaryDockHost?.Workspace is { } workspace) {
				var descendants = workspace.GetVisualDescendants().OfType<SplitContainer>();
				foreach (var splitContainer in descendants)
					splitContainer.ResizeSlots();
			}
		}

		/// <summary>
		/// Occurs when the <c>Layout.EvenlyDistributeFavorFocused</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeFavorFocusedMenuItemClick(object? sender, RoutedEventArgs e) {
			var descendants = dockSite.GetVisualDescendants().OfType<SplitContainer>();
			foreach (var splitContainer in descendants) {
				// Look for SplitContainers that contain the focused element, and increase the ratios for that slot
				var visualCount = splitContainer.Children.Count;
				var desiredRatios = new double[visualCount];
				for (int i = 0, visibleChildCount = 0; i < visualCount; i++) {
					// Default ratio, must also ensure that we don't pass a ratio that is less than or equal to 0
					desiredRatios[i] = 1;

					// Get the child and verify that it is visible
					var child = splitContainer.Children[i] as Visual;
					if (child?.IsVisible == true) {
						// If the child has the keyboard focus, then increase it's ratio
						if ((child is IInputElement inputElement) && inputElement.IsKeyboardFocusWithin)
							desiredRatios[visibleChildCount] = 3;
						visibleChildCount++;
					}
				}

				splitContainer.ResizeSlots(desiredRatios);
			}
		}

		/// <summary>
		/// Occurs when the <c>Layout.EvenlyDistributeFavorWorkspace</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeFavorWorkspaceMenuItemClick(object? sender, RoutedEventArgs e) {
			var descendants = dockSite.GetVisualDescendants().OfType<SplitContainer>();
			foreach (var splitContainer in descendants) {
				// Look for SplitContainers that contain the Workspace, and increase the ratios for that slot
				var visualCount = splitContainer.Children.Count;
				var desiredRatios = new double[visualCount];
				for (int i = 0, visibleChildCount = 0; i < visualCount; i++) {
					// Default ratio, must also ensure that we don't pass a ratio that is less than or equal to 0
					desiredRatios[i] = 1;

					// Get the child and verify that it is visible
					var child = splitContainer.Children[i] as Visual;
					if (child?.IsVisible == true) {
						// If the child is a Workspace (or contains the Workspace) then increase it's ratio
						if (child.FindDescendantOfType<Workspace>(includeSelf: true) is not null)
							desiredRatios[visibleChildCount] = 3;
						visibleChildCount++;
					}
				}

				splitContainer.ResizeSlots(desiredRatios);
			}
		}

		/// <summary>
		/// Occurs when the <c>Layout.EvenlyDistributeToolsOnly</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutEvenlyDistributeToolsOnlyMenuItemClick(object? sender, RoutedEventArgs e) {
			var descendants = dockSite.GetVisualDescendants().OfType<SplitContainer>();
			foreach (var splitContainer in descendants) {
				bool isForTools = splitContainer.FindAncestorOfType<Workspace>() is null;
				if (isForTools)
					splitContainer.ResizeSlots();
			}
		}

		/// <summary>
		/// Occurs when the <c>Layout.RandomlyDistribute</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutRandomlyDistributeMenuItemClick(object? sender, RoutedEventArgs e) {
			var descendants = dockSite.GetVisualDescendants().OfType<SplitContainer>();
			var random = new Random();
			foreach (var splitContainer in descendants) {
				splitContainer.ResizeSlots(
					random.NextDouble() * 8 + 1,
					random.NextDouble() * 6 + 1,
					random.NextDouble() * 4 + 1,
					random.NextDouble() * 2 + 1);
			}
		}

		/// <summary>
		/// Occurs when the <c>Layout.ReverseAll</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLayoutReverseAllMenuItemClick(object? sender, RoutedEventArgs e) {
			var descendants = dockSite.GetVisualDescendants().OfType<SplitContainer>();
			foreach (var splitContainer in descendants)
				splitContainer.ReverseSlots();
		}

	}

}
