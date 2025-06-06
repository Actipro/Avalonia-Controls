using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Docking;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using Avalonia.VisualTree;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.DockingSamples.Demos.SimpleIde {

	public partial class MainControl : UserControl {

		private int _documentIndex = 0;

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="IsExternalSampleOptionVisible"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> IsExternalSampleOptionVisibleProperty
			= AvaloniaProperty.Register<MainControl, bool>(nameof(IsExternalSampleOptionVisible), defaultValue: true);

		/// <summary>
		/// Defines the <see cref="IsWindowActivationEventOutputEnabled"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> IsWindowActivationEventOutputEnabledProperty
			= AvaloniaProperty.Register<MainControl, bool>(nameof(IsWindowActivationEventOutputEnabled), defaultValue: true);

		/// <summary>
		/// Defines the <see cref="IsWindowRegistrationEventOutputEnabled"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> IsWindowRegistrationEventOutputEnabledProperty
			= AvaloniaProperty.Register<MainControl, bool>(nameof(IsWindowRegistrationEventOutputEnabled));

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			// Hide the external sample option when not supported
			if (!ApplicationViewModel.AreExternalSamplesSupported)
				IsExternalSampleOptionVisible = false;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Appends a message to the events <see cref="ListBox"/>.
		/// </summary>
		/// <param name="text">The text to append.</param>
		private void AppendMessage(string text) {
			Dispatcher.UIThread.InvokeAsync(() => {
				var isKeyboardFocusWithin = eventsListBox.IsKeyboardFocusWithin;

				var item = new ListBoxItem() { Content = text };
				eventsListBox.Items.Add(item);
				eventsListBox.SelectedItem = item;
				
				if (eventsListBox.IsAttachedToVisualTree()) {
					eventsListBox.ScrollIntoView(item);

					if (isKeyboardFocusWithin)
						item.Focus();
				}
			}, DispatcherPriority.Send);
		}

		/// <summary>
		/// Occurs when a floating window is opening, allowing for customization before it is displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteFloatingWindowOpening(object sender, FloatingWindowOpeningEventArgs e) {
			if (limitFloatingWindowInitialSizeMenuItem.IsChecked) {
				// Make sure the long side is no longer than 600, and the short side is no longer than 300
				if (e.Size.Width > e.Size.Height)
					e.Size = new Size(Math.Min(600.0, e.Size.Width), Math.Min(300.0, e.Size.Height));
				else
					e.Size = new Size(Math.Min(300.0, e.Size.Width), Math.Min(600.0, e.Size.Height));
			}
		}

		/// <summary>
		/// Occurs when the primary document is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteMdiKindChanged(object sender, RoutedEventArgs e)
			=> AppendMessage($"{nameof(DockSite.MdiKindChanged)}: Kind={dockSite.MdiKind}");

		/// <summary>
		/// Occurs when a docking-related context menu is opening, allowing for customization before it is displayed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingMenuEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteMenuOpening(object sender, DockingMenuEventArgs e) {
			var sb = new StringBuilder($"{nameof(DockSite.MenuOpening)}: Kind={e.Kind}");

			if (e.Window is not null) {
				sb.Append($", Title={e.Window.Title} ");

				if ((e.Window == outputToolWindow) && (e.Menu is not null)) {
					e.Menu.Items.Add(new Separator());

					e.Menu.Items.Add(new MenuItem() {
						Header = "Activation Events",
						ToggleType = MenuItemToggleType.CheckBox,
						[!!MenuItem.IsCheckedProperty] = this[!!IsWindowActivationEventOutputEnabledProperty],
					});

					e.Menu.Items.Add(new MenuItem() {
						Header = "Registration Events",
						ToggleType = MenuItemToggleType.CheckBox,
						[!!MenuItem.IsCheckedProperty] = this[!!IsWindowRegistrationEventOutputEnabledProperty],
					});
				}
			}

			AppendMessage(sb.ToString());
		}

		/// <summary>
		/// Occurs when a new docking window is requested, generally via a user click on a new tab button.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteNewWindowRequested(object sender, RoutedEventArgs e)
			=> AppendMessage(nameof(DockSite.NewWindowRequested));

		/// <summary>
		/// Occurs when the primary document is changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSitePrimaryDocumentChanged(object sender, DockingWindowEventArgs e) {
			if (e.Window is null)
				AppendMessage($"{nameof(DockSite.PrimaryDocumentChanged)}: (none)");
			else
				AppendMessage($"{nameof(DockSite.PrimaryDocumentChanged)}: Title={e.Window.Title}");
		}

		/// <summary>
		/// Occurs when a docking window is activated.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowActivated(object sender, DockingWindowEventArgs e) {
			if (IsWindowActivationEventOutputEnabled)
				AppendMessage($"{nameof(DockSite.WindowActivated)}: Title={e.Window?.Title ?? "NULL"}");
		}

		/// <summary>
		/// Occurs when an auto-hide popup has been closed that displayed a tool window.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowAutoHidePopupClosed(object sender, DockingWindowEventArgs e)
			=> AppendMessage($"{nameof(DockSite.WindowAutoHidePopupClosed)}: Title={e.Window?.Title ?? "NULL"}");

		/// <summary>
		/// Occurs when an auto-hide popup has been opened that displays a tool window.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowAutoHidePopupOpened(object sender, DockingWindowEventArgs e)
			=> AppendMessage($"{nameof(DockSite.WindowAutoHidePopupOpened)}: Title={e.Window?.Title ?? "NULL"}");

		/// <summary>
		/// Occurs when a docking window is deactivated.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowDeactivated(object sender, DockingWindowEventArgs e) {
			if (IsWindowActivationEventOutputEnabled)
				AppendMessage($"{nameof(DockSite.WindowDeactivated)}: Title={e.Window?.Title ?? "NULL"}");
		}

		/// <summary>
		/// Occurs when a docking window is registered.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowRegistered(object sender, DockingWindowEventArgs e) {
			if (IsWindowRegistrationEventOutputEnabled)
				AppendMessage($"{nameof(DockSite.WindowRegistered)}: Title={e.Window?.Title ?? "NULL"}");
		}

		/// <summary>
		/// Occurs before one or more docking windows are auto-hidden, allowing for side customization.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsAutoHidingEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsAutoHiding(object sender, DockingWindowsAutoHidingEventArgs e) {
			if (forceAutoHideToBottomMenuItem.IsChecked)
				e.Side = Dock.Bottom;

			var count = e.Windows.Count();
			if (count == 1)
				AppendMessage($"{nameof(DockSite.WindowsAutoHiding)}: Title={e.Windows.First().Title}, Side={e.Side}");
			else
				AppendMessage($"{nameof(DockSite.WindowsAutoHiding)}: Count={count}, Side={e.Side}");
		}

		/// <summary>
		/// Occurs after one or more docking windows have been closed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsClosed(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				AppendMessage($"{nameof(DockSite.WindowsClosed)}: Title={e.Windows.First().Title}");
			else
				AppendMessage($"{nameof(DockSite.WindowsClosed)}: Count={count}");
		}

		/// <summary>
		/// Occurs before one or more docking windows are closed, allowing for cancellation of the close.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsClosing(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				AppendMessage($"{nameof(DockSite.WindowsClosing)}: Title={e.Windows.First().Title}");
			else
				AppendMessage($"WindowsClosing: Count={count}");
		}

		/// <summary>
		/// Occurs after one or more docking windows are dragged by the end user.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsDragged(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				AppendMessage($"{nameof(DockSite.WindowsDragged)}: Title={e.Windows.First().Title}");
			else
				AppendMessage($"{nameof(DockSite.WindowsDragged)}: Count={count}");
		}

		/// <summary>
		/// Occurs before one or more docking windows are dragged by the end user.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsDragging(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				AppendMessage($"{nameof(DockSite.WindowsDragging)}: Title={e.Windows.First().Title}");
			else
				AppendMessage($"{nameof(DockSite.WindowsDragging)}: Count={count}");
		}

		/// <summary>
		/// Occurs when one or more docking windows are dragged over a new dock target, allowing for certain dock guides to be hidden.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsDragOverEventArgs</c> that contains the event data.</param>
		private void OnDockSiteWindowsDragOver(object sender, DockingWindowsDragOverEventArgs e) {
			// If this option is checked, prevent all dragged windows from being dropped anywhere other than in MDI
			// or in a floating window by themselves
			if (restrictDraggedWindowsMenuItem.IsChecked) {
				if ((e.Target is TabbedMdiHost) || (e.Target is TabbedMdiContainer))
					e.AllowedDockGuideKinds = DockGuideKinds.Inner | DockGuideKinds.Center;
				else
					e.AllowedDockGuideKinds = DockGuideKinds.None;
			}

			// NOTE: You could create other restrictions here like only allowing left/right or top/bottom dock guides via AllowedDockGuideKinds too
		}

		/// <summary>
		/// Occurs after one or more docking windows have been opened.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsOpened(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				AppendMessage($"{nameof(DockSite.WindowsOpened)}: Title={e.Windows.First().Title}");
			else
				AppendMessage($"{nameof(DockSite.WindowsOpened)}: Count={count}");
		}

		/// <summary>
		/// Occurs before one or more docking windows are opened.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowsEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsOpening(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				AppendMessage($"{nameof(DockSite.WindowsOpening)}: Title={e.Windows.First().Title}");
			else
				AppendMessage($"{nameof(DockSite.WindowsOpening)}: Count={count}");
		}

		/// <summary>
		/// Occurs after one or more docking windows' states have changed.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowsStateChanged(object sender, DockingWindowsEventArgs e) {
			var count = e.Windows.Count();
			if (count == 1)
				AppendMessage($"{nameof(DockSite.WindowsStateChanged)}: Title={e.Windows.First().Title}, State={e.Windows.First().State}");
			else
				AppendMessage($"{nameof(DockSite.WindowsStateChanged)}: Count={count}, State={e.Windows.First().State}");
		}

		/// <summary>
		/// Occurs when a docking window is unregistered.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>DockingWindowEventArgs</c> that contains data related to this event.</param>
		private void OnDockSiteWindowUnregistered(object sender, DockingWindowEventArgs e) {
			if (IsWindowRegistrationEventOutputEnabled)
				AppendMessage($"{nameof(DockSite.WindowUnregistered)}: Title={e.Window?.Title ?? "NULL"}");
		}

		/// <summary>
		/// Occurs when the menu item is clicked.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">The <c>RoutedEventArgs</c> that contains data related to this event.</param>
		private void OnNewDocumentMenuItemClick(object sender, RoutedEventArgs e) {
			var menuItem = (MenuItem)sender;
			var extension = menuItem.Tag as string ?? ".txt";
			this.CreateEditorDocument(extension, null, null);
		}

		/// <summary>
		/// Creates and activates a new editor document.
		/// </summary>
		/// <param name="extension">The file extension.</param>
		/// <param name="fileName">The file name.</param>
		/// <param name="text">The optional text to use.</param>
		private void CreateEditorDocument(string extension, string? fileName = null, string? text = null, TabLayoutKind layoutKind = TabLayoutKind.Normal) {
			if (fileName is not null) {
				// Load the file's text
				try {
					if (File.Exists(fileName))
						text = File.ReadAllText(fileName);
				}
				catch {
					text = string.Empty;
				}
			}
			else {
				// Ensure a file name has been set
				fileName = string.Format($"Document{++_documentIndex}{extension.ToLowerInvariant()}");
			}

			string? description;
			IImage? icon;
			switch (extension.ToLowerInvariant()) {
				case ".txt":
					description = "A text file";
					icon = ImageLoader.GetIcon("TextDocument16.png");
					break;
				case ".rtf":
					description = "A rich text file";
					icon = ImageLoader.GetIcon("RichTextDocument16.png");
					break;
				default:
					description = $"A {extension.ToUpperInvariant()} file";
					icon = ImageLoader.GetIcon("BlankPage16.png");
					break;
			}

			// Create the document window
			var documentWindow = new DocumentWindow() {
				Title = fileName,
				Description = description,
				FileName = fileName,
				Icon = icon,
				TabbedMdiLayoutKind = layoutKind,
				Content = new TextBox() {
					BorderThickness = new Thickness(0),
					Text = text,
					TextWrapping = TextWrapping.Wrap,
				}
			};

			// Add the document
			dockSite.DocumentWindows.Add(documentWindow);

			// Activate the document
			documentWindow.Activate();
		}



		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public bool IsExternalSampleOptionVisible {
			get => GetValue(IsExternalSampleOptionVisibleProperty);
			set => SetValue(IsExternalSampleOptionVisibleProperty, value);
		}

		/// <summary>
		/// Indicates whether docking window activation event output is enabled.
		/// </summary>
		public bool IsWindowActivationEventOutputEnabled {
			get => GetValue(IsWindowActivationEventOutputEnabledProperty);
			set => SetValue(IsWindowActivationEventOutputEnabledProperty, value);
		}

		/// <summary>
		/// Indicates whether docking window registration event output is enabled.
		/// </summary>
		public bool IsWindowRegistrationEventOutputEnabled {
			get => GetValue(IsWindowRegistrationEventOutputEnabledProperty);
			set => SetValue(IsWindowRegistrationEventOutputEnabledProperty, value);
		}

		/// <inheritdoc/>
		protected override void OnLoaded(RoutedEventArgs e) {
			base.OnLoaded(e);

			// Load additional documents for testing
			for (var i = 0; i < 2; i++)
				CreateEditorDocument(".txt");

			CreateEditorDocument(".txt", "Preview.txt", "This is a preview document", TabLayoutKind.Preview);

			CreateEditorDocument(".txt", "About.txt", @"This demo shows off a simple example of building an IDE using tool windows and a multiple document interface (MDI), powerful UI mechanisms made available with the Actipro Docking & MDI control product. Everything from floating MDI containers and pinned/preview tabs to complete MVVM support and much more is available.

Browse through this demo and the rest of the included samples to discover more about the enormous feature set this product provides.", layoutKind: TabLayoutKind.Pinned);

		}

		public ICommand OpenExternalWindowCommand { get; }
			= new DelegateCommand<object>(_ => {
				new MainWindow().Show();
			});

	}

}
