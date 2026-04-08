using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Docking;
using ActiproSoftware.UI.Avalonia.Controls.Docking.Serialization;
using ActiproSoftware.UI.Avalonia.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.LayoutSerialization {

	public partial class MainControl : UserControl {

		private static string _layoutXml = string.Empty;
		private string _defaultLayoutXml = string.Empty;
		private readonly DockSiteLayoutSerializer _layoutSerializer;

		private const string ProgrammaticToolWindow1Name = "programmaticToolWindow1";
		private const string ProgrammaticToolWindow2Name = "programmaticToolWindow2";

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="DockingWindowDeserializationBehavior"/> property.
		/// </summary>
		public static readonly StyledProperty<DockingWindowDeserializationBehavior> DockingWindowDeserializationBehaviorProperty
			= AvaloniaProperty.Register<MainControl, DockingWindowDeserializationBehavior>(nameof(DockingWindowDeserializationBehavior), defaultValue: DockingWindowDeserializationBehavior.Discard);

		/// <summary>
		/// Defines the <see cref="DockSiteSerializationBehavior"/> property.
		/// </summary>
		public static readonly StyledProperty<DockSiteSerializationBehavior> DockSiteSerializationBehaviorProperty
			= AvaloniaProperty.Register<MainControl, DockSiteSerializationBehavior>(nameof(DockSiteSerializationBehavior), defaultValue: DockSiteSerializationBehavior.ToolWindowsOnly);

		#endregion

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		public MainControl() {
			InitializeComponent();

			// Create a layout serialization and attach a DockingWindowDeserializing event handler, which is called when deserializing all windows (including ones that may not exist yet)
			_layoutSerializer = new DockSiteLayoutSerializer();
			_layoutSerializer.ObjectDeserialized += OnLayoutSerializerObjectDeserialized;
			_layoutSerializer.ObjectSerialized += OnLayoutSerializerObjectSerialized;
			_layoutSerializer.DockingWindowDeserializing += OnLayoutSerializerDockingWindowDeserializing;

			// This sample can serialize custom data with one of the tool windows, so the Type of that custom
			//   data must be known to the serializer.
			_layoutSerializer.RegisterType<CustomToolWindowData>();
			_layoutSerializer.RegisterType<CustomVersion>(GetVersionProperties());

			IEnumerable<IXmlSerializerProperty> GetVersionProperties() {
				yield return new XmlSerializerProperty<CustomVersion, int>(x => x.Major);
				yield return new XmlSerializerProperty<CustomVersion, int>(x => x.Minor);
				yield return new XmlSerializerProperty<CustomVersion, int>(x => x.Build);
				yield return new XmlSerializerProperty<CustomVersion, int>(x => x.Revision, x => x.ShouldSerializeRevision());
			}

			InitializeComponent();

			this.Loaded += (s, e) => {
				// Update the UI
				layoutXmlTextBlock.Text = _layoutXml;

				// Save the default layout
				SaveLayout(saveDefaultLayout: true);

				// Load or save the normal layout depending on if this sample has already been opened
				if (!string.IsNullOrEmpty(_layoutXml))
					LoadLayout(loadDefaultLayout: false);
				else
					SaveLayout(saveDefaultLayout: false);
			};

			this.Unloaded += (s, e) => {
				// Save the layout when moving away from the sample so it can be restored when returning
				SaveLayout(saveDefaultLayout: false);
			};

		}

		// --------------------------------------------------------------------------------------------------
		// NON-PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		/// <summary>
		/// Initializes the "Programmatic Tool Window 1" tool window.
		/// </summary>
		/// <param name="toolWindow">The tool window.</param>
		private static void InitializeProgrammaticToolWindow1(ToolWindow toolWindow) {
			// Create the tool window content
			var textBox = new TextBox() {
				BorderThickness = new Thickness(),
				IsReadOnly = true,
				Text = "This ToolWindow was programmatically created in the code-behind.",
				TextWrapping = TextWrapping.Wrap
			};

			toolWindow.Name = ProgrammaticToolWindow1Name;
			toolWindow.Title = "Programmatic ToolWindow 1";
			toolWindow.Icon = ImageLoader.GetIcon("Properties16.png");
			toolWindow.Content = textBox;
		}

		/// <summary>
		/// Loads the layout from a <see cref="TextBox"/>.
		/// </summary>
		/// <param name="loadDefaultLayout">Whether to load the default layout.</param>
		private void LoadLayout(bool loadDefaultLayout) {
			UpdateSerializerOptions();
			var layout = (loadDefaultLayout ? _defaultLayoutXml : _layoutXml);
			if (!string.IsNullOrEmpty(layout))
				_layoutSerializer.LoadFromString(layout, dockSite);
		}

		/// <summary>
		/// Occurs when the <c>Activate.Programmatic ToolWindow 1</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnActivateProgrammaticToolWindow1Click(object? sender, RoutedEventArgs e) {
			var toolWindow = dockSite.ToolWindows.FirstOrDefault(x => x?.Name == ProgrammaticToolWindow1Name);
			if (toolWindow is null) {
				// Create, initialize, and register the tool window
				toolWindow = new ToolWindow();
				InitializeProgrammaticToolWindow1(toolWindow);
				dockSite.ToolWindows.Add(toolWindow);

				// Change the menu item's label/header
				activeProgrammaticToolWindow1.Label = "Activate Programmatic ToolWindow 1";
			}

			toolWindow.Activate();
		}

		/// <summary>
		/// Occurs when the <c>Activate.Programmatic ToolWindow 2</c> menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnActivateProgrammaticToolWindow2Click(object? sender, RoutedEventArgs e) {
			var toolWindow = dockSite.ToolWindows.FirstOrDefault(x => x?.Name == ProgrammaticToolWindow2Name);
			if (toolWindow is null) {
				// Optionally associate custom data to be associated with the tool window during serialization
				var customData = new CustomToolWindowData() {
					CreationDateTime = DateTime.Now,
					InstanceId = 2,
				};

				// Create and register the tool window
				toolWindow = new CustomToolWindow() {
					Name = ProgrammaticToolWindow2Name,
					CustomData = customData,
				};
				dockSite.ToolWindows.Add(toolWindow);


				// Change the menu item's label/header
				activeProgrammaticToolWindow2.Label = "Activate Programmatic ToolWindow 2";
			}

			toolWindow.Activate();
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnDockSiteSerializationOptionMenuItemClick(object? sender, RoutedEventArgs e) {
			UpdateSerializerOptions();
		}

		/// <summary>
		/// Handles <see cref="DockSiteLayoutSerializer.DockingWindowDeserializing"/> event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DockingWindowDeserializingEventArgs"/> instance containing the event data.</param>
		private void OnLayoutSerializerDockingWindowDeserializing(object? sender, DockingWindowDeserializingEventArgs e) {
			// If windows are auto-creating...
			if (DockingWindowDeserializationBehavior == DockingWindowDeserializationBehavior.AutoCreate
				&& e.Window is ToolWindow toolWindow) {

				// The e.Node property contains the XML data and the e.Window property contains the associated DocumentWindow or ToolWindow, if any...
				//   The window may have been retrieved from the DockSite, or automatically created (when using DockingWindowDeserializationBehavior.AutoCreate)
				if (e.Node.Name == ProgrammaticToolWindow1Name) {
					InitializeProgrammaticToolWindow1(toolWindow);

					// Change the menu item's label/header
					activeProgrammaticToolWindow1.Label = "Activate Programmatic ToolWindow 1";
				}
				else if (e.Node.Name == ProgrammaticToolWindow2Name) {
					// NOTE: We don't need to initialize "programmaticToolWindow2", because it is a custom ToolWindow that sets the appropriate properties when constructed.

					// Change the menu item's label/header
					activeProgrammaticToolWindow2.Label = "Activate Programmatic ToolWindow 2";
				}
			}
		}

		/// <summary>
		/// Handles <c>DockSiteLayoutSerializer.ObjectDeserialized</c> event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void OnLayoutSerializerObjectDeserialized(object? sender, ItemSerializationEventArgs e) {
			if ((e.Node is XmlToolWindow) && (e.Item is CustomToolWindow customToolWindow)) {
				// After deserializing, transfer the deserialzed custom data back to tool window instance
				if (e.Node.Tag is CustomToolWindowData customData)
					customToolWindow.CustomData = customData;
			}
		}

		/// <summary>
		/// Handles <c>DockSiteLayoutSerializer.ObjectSerialized</c> event.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void OnLayoutSerializerObjectSerialized(object? sender, ItemSerializationEventArgs e) {
			if ((e.Node is XmlToolWindow) && (e.Item is CustomToolWindow customToolWindow)) {
				// Before serializing, transfer the custom data from the tool window instance to the object data that will be serialized
				e.Node.Tag = customToolWindow.CustomData;
			}
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoadDefaultLayoutMenuItemClick(object? sender, RoutedEventArgs e) {
			LoadLayout(loadDefaultLayout: true);
			ApplicationViewModel.Instance.MessageService?.ShowMessage("Default layout XML loaded.", "Default Layout Loaded");
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnLoadLayoutMenuItemClick(object? sender, RoutedEventArgs e) {
			LoadLayout(loadDefaultLayout: false);
			ApplicationViewModel.Instance.MessageService?.ShowMessage("Layout XML loaded from static member variable.", "Layout Loaded");
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnSaveLayoutMenuItemClick(object? sender, RoutedEventArgs e) {
			SaveLayout(saveDefaultLayout: false);
			ApplicationViewModel.Instance.MessageService?.ShowMessage("Layout XML saved to static member variable and displayed in document.", "Layout Saved");
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="RoutedEventArgs"/> that contains the event data.</param>
		private void OnWindowDeserializationOptionMenuItemClick(object? sender, RoutedEventArgs e) {
			UpdateSerializerOptions();
		}

		/// <summary>
		/// Saves the layout to a <see cref="TextBox"/>.
		/// </summary>
		/// <param name="saveDefaultLayout">Whether to save the default layout.</param>
		private void SaveLayout(bool saveDefaultLayout) {
			UpdateSerializerOptions();
			var layout = _layoutSerializer.SaveToString(dockSite);
			if (saveDefaultLayout) {
				_defaultLayoutXml = layout;
			}
			else {
				_layoutXml = layout;
				layoutXmlTextBlock.Text = _layoutXml;
			}

		}

		private void UpdateSerializerOptions() {
			// Update dependency properties from UI state
			DockSiteSerializationBehavior = saveToolWindowLayoutOnlyMenuItem.IsChecked ? DockSiteSerializationBehavior.ToolWindowsOnly : DockSiteSerializationBehavior.All;
			if (autoCreateMenuItem.IsChecked)
				DockingWindowDeserializationBehavior = DockingWindowDeserializationBehavior.AutoCreate;
			else if (lazyLoadMenuItem.IsChecked)
				DockingWindowDeserializationBehavior = DockingWindowDeserializationBehavior.LazyLoad;
			else
				DockingWindowDeserializationBehavior = DockingWindowDeserializationBehavior.Discard;

			// Update radio buttons
			autoCreateMenuItem.IsChecked = (DockingWindowDeserializationBehavior == DockingWindowDeserializationBehavior.AutoCreate);
			discardMenuItem.IsChecked = (DockingWindowDeserializationBehavior == DockingWindowDeserializationBehavior.Discard);
			lazyLoadMenuItem.IsChecked = (DockingWindowDeserializationBehavior == DockingWindowDeserializationBehavior.LazyLoad);

			// Push updates to the layout serializer
			_layoutSerializer.SerializationBehavior = DockSiteSerializationBehavior;
			_layoutSerializer.ToolWindowDeserializationBehavior = DockingWindowDeserializationBehavior;
			_layoutSerializer.DocumentWindowDeserializationBehavior = DockingWindowDeserializationBehavior;
		}

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		public DockingWindowDeserializationBehavior DockingWindowDeserializationBehavior {
			get => GetValue(DockingWindowDeserializationBehaviorProperty);
			set => SetValue(DockingWindowDeserializationBehaviorProperty, value);
		}

		public DockSiteSerializationBehavior DockSiteSerializationBehavior {
			get => GetValue(DockSiteSerializationBehaviorProperty);
			set => SetValue(DockSiteSerializationBehaviorProperty, value);
		}

	}

}
