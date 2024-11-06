using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.CustomizeBuiltInMenus {

	public partial class MainControl : UserControl {

		private readonly IImage? _customCommandSmallImage;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			// Configure the sample to use a common view model with pre-populated commands
			RibbonViewModel = new RichTextEditorRibbonViewModel(BarManager);
			RibbonViewModel.IsApplicationButtonVisible = false;

			InitializeComponent();

			// Cache image for use with the custom action
			_customCommandSmallImage = ImageLoader.GetIcon("QuickStartGreen16.png");

			// Listen for ribbon menus are opening
			ribbon.MenuOpening += this.OnRibbonMenuOpening;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private BarManager BarManager { get; } = new BarManager();

		/// <summary>
		/// Creates a new <see cref="BarMenuHeading"/> for use in the menu.
		/// </summary>
		/// <param name="label">The label to be set for the heading.</param>
		private static BarMenuHeading CreateBarMenuHeading(string label) {
			var barMenuHeading = new BarMenuHeading(label);

			// Block the built-in context menu (with commands like "Add to Quick Access Toolbar") from
			// being displayed for this menu item.
			BarControlService.SetAllowContextMenu(barMenuHeading, false);

			return barMenuHeading;
		}

		/// <summary>
		/// Creates a new <see cref="BarMenuSeparator"/> for use in the menu.
		/// </summary>
		private static BarMenuSeparator CreateBarMenuSeparator() {
			var barMenuSeparator = new BarMenuSeparator();

			// Block the built-in context menu (with commands like "Add to Quick Access Toolbar") from
			// being displayed for this menu item.
			BarControlService.SetAllowContextMenu(barMenuSeparator, false);

			return barMenuSeparator;
		}

		/// <summary>
		/// Creates a new <see cref="BarMenuItem"/> for use in the menu that will trigger a custom action when invoked.
		/// </summary>
		private BarMenuItem CreateCustomActionBarMenuItem(object? owner) {
			var customActionMenuItem = new BarMenuItem() {
				Command = CustomCommand,
				CommandParameter = owner,
				KeyTipText = "C",
				Label = $"Custom Action (Created @ {DateTime.Now.ToLongTimeString()})",
				ScreenTipHeader = "Custom Action",
				SmallIcon = _customCommandSmallImage,
			};

			// Block the built-in context menu (with commands like "Add to Quick Access Toolbar") from
			// being displayed for this menu item.
			BarControlService.SetAllowContextMenu(customActionMenuItem, false);

			return customActionMenuItem;
		}

		/// <summary>
		/// Gets the command that will be associated with the custom menu item.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		private ICommand CustomCommand { get; }
			= new DelegateCommand<object>(param => {
				var message = (param is null)
					? "You clicked the programmatically-added custom menu item."
					: $"You clicked the programmatically-added custom menu item for:{Environment.NewLine}{Environment.NewLine}{param}";
				MessageBox.Show(message, "Custom Action", MessageBoxButtons.OK, MessageBoxImage.Information);
			});

		/// <summary>
		/// Customizes a context menu.
		/// </summary>
		/// <param name="e">A <see cref="BarMenuEventArgs"/> that contains the event data.</param>
		private void CustomizeMenu(BarMenuEventArgs e) {
			var items = e.Items;
			if (items is null)
				return;

			// Add custom menu items at the bottom
			items.Add(CreateBarMenuSeparator());
			items.Add(CreateBarMenuHeading($"Customization of {GetMenuKindDescription(e.Kind)}"));
			items.Add(CreateCustomActionBarMenuItem(e.Owner));

			// Customize the pre-defined 'Add to Quick Access Toolbar' and 'Remove from Quick Access Toolbar' menu items
			// by disabling the menu item associated with the corresponding commands
			if (e.Kind == BarMenuKind.ControlContextMenu) {
				foreach (var menuItem in items.OfType<BarMenuItem>()) {
					if (menuItem.Command == ribbon.AddToQuickAccessToolBarCommand || menuItem.Command == ribbon.RemoveFromQuickAccessToolBarCommand) {

						// Disable the menu item
						menuItem.IsEnabled = false;

						// Update the label to indicate why the commands are disabled
						menuItem.Label += " (Programmatically Disabled)";

						// IMPORTANT NOTE:
						//
						// While this sample demonstrates that built-in command labels can be changed programmatically,
						// this is not the method that would typically be used to customize or localize strings.
						// Refer to "Customizing String Resources" documentation for additional information
						//
						// https://www.actiprosoftware.com/docs/controls/avalonia/customizing-string-resources
					}
				}
			}
		}

		/// <summary>
		/// Gets a description for a menu kind.
		/// </summary>
		/// <param name="kind">The menu kind to examine.</param>
		/// <returns>A string describing the menu kind.</returns>
		private static string GetMenuKindDescription(BarMenuKind kind) {
			switch (kind) {
				case BarMenuKind.ControlContextMenu:
					return "Control Context Menu";
				case BarMenuKind.RibbonOptionsButtonMenu:
					return "Options Menu";
				case BarMenuKind.RibbonQuickAccessToolBarCustomizeButtonMenu:
					return "Quick Access Toolbar Customize Menu";
				case BarMenuKind.RibbonTabItemOverflowButtonMenu:
					return "Tab Overflow Menu";
				case BarMenuKind.RibbonGroupOverflowButtonMenu:
					return "Group Overflow Menu";
				default:
					return "Unknown Menu";
			}
		}

		/// <summary>
		/// Logs the details of the menu.
		/// </summary>
		/// <param name="e">A <see cref="BarMenuEventArgs"/> that contains the event data.</param>
		private void LogMenuDetails(BarMenuEventArgs e) {
			var items = e.Items;
			if (items is null)
				return;

			// Generate some details about the current menu
			var sb = new StringBuilder()
				.AppendLine("MenuOpening...")
				.AppendLine($"    Kind = {e.Kind.GetType().Name}.{e.Kind}")
				.AppendLine($"    Type = {e.Menu?.GetType().FullName}")
				.AppendLine($"    Owner = {(e.Owner?.ToString() ?? "<NULL>")}");
			var menuItemCount = items.Count;
			for (var i = 0; i < menuItemCount; i++)
				sb.AppendLine($"    Item[{i}] = {items[i]}");

			// Display the current details in the text box
			outputTextBox.Text = sb.ToString();
			outputTextBox.ScrollToLine(0);
		}

		private void OnRibbonMenuOpening(object? sender, BarMenuEventArgs e) {
			// Log the details of any menu
			LogMenuDetails(e);

			// Customize built-in menus
			bool isBuiltInMenu = !(e.Kind == BarMenuKind.PopupButtonMenu || e.Kind == BarMenuKind.MenuItemSubmenu);
			if (isBuiltInMenu)
				CustomizeMenu(e);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The ribbon view model for the MVVM sample.
		/// </summary>
		public RibbonViewModel RibbonViewModel { get; }

	}

}
