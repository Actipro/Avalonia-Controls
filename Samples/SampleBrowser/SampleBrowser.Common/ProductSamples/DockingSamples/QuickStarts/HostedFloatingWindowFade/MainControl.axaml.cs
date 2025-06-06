using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.HostedFloatingWindowFade {

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
		/// Creates and floats a tool window.
		/// </summary>
		private void CreateAndFloatToolWindow() {
			var textBox = new TextBox() {
				BorderThickness = new Thickness(),
				Text = "This floating window will fade out when it loses focus.",
				TextWrapping = TextWrapping.Wrap
			};

			var toolWindow = new ToolWindow(dockSite, "tool", "Floating Tool Window", null, textBox);
			toolWindow.Float(new Point(100, 100));
			Dispatcher.UIThread.InvokeAsync(() => toolWindow.Activate());
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		protected override void OnLoaded(RoutedEventArgs e) {
			base.OnLoaded(e);

			if (dockSite.ToolWindows.Count == 0)
				CreateAndFloatToolWindow();
		}

	}

}
