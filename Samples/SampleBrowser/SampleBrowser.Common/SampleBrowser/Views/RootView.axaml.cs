using ActiproSoftware.UI.Avalonia.Themes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.ComponentModel;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Provides the root view for the application.
	/// </summary>
	public partial class RootView : UserControl {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public RootView() {
			InitializeComponent();

			ViewModel = ApplicationViewModel.Instance;
			ViewModel.PropertyChanged += OnApplicationViewModelPropertyChanged;
		}
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnApplicationViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e) {
			if (e.PropertyName == nameof(ApplicationViewModel.ViewTransitionForward)) {
				if (transitionControl.PageTransition is DirectionalPageSlide transition)
					transition.UseForwardDirection = ViewModel?.ViewTransitionForward ?? true;
			}
		}
		
        private void OnViewMenuOpening(object? sender, EventArgs e) {
			if (ModernTheme.TryGetCurrent(out var theme)) {
				var definition = theme.Definition;
				if (definition is not null) {
					compactDensityMenuItem.Icon = (definition.UserInterfaceDensity == UserInterfaceDensity.Compact ? new CheckBox() { IsChecked = true } : null);
					normalDensityMenuItem.Icon = (definition.UserInterfaceDensity == UserInterfaceDensity.Normal ? new CheckBox() { IsChecked = true } : null);
					spaciousDensityMenuItem.Icon = (definition.UserInterfaceDensity == UserInterfaceDensity.Spacious ? new CheckBox() { IsChecked = true } : null);
				}
			}
        }

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
			// Configure the view model with a message service
			var notificationManager = new WindowNotificationManager(TopLevel.GetTopLevel(this));
			if ((notificationManager is not null) && (ViewModel is not null))
				ViewModel.MessageService = new NotificationMessageService(notificationManager);

			base.OnAttachedToVisualTree(e);
		}

		protected override void OnLoaded(RoutedEventArgs e) {
			base.OnLoaded(e);

			// Validate a Pro license here as the app's root view first loads
			Properties.Fundamentals.AssemblyInfo.Instance.ValidateLicense();
		}

		protected override void OnPointerReleased(PointerReleasedEventArgs e) {
			base.OnPointerReleased(e);

			if (!e.Handled) {
				switch (e.InitialPressMouseButton) {
					case MouseButton.XButton1:
						ViewModel?.NavigateViewBackward();
						break;
					case MouseButton.XButton2:
						ViewModel?.NavigateViewForward();
						break;
				}
			}
		}

		public ApplicationViewModel? ViewModel {
			get => DataContext as ApplicationViewModel;
			set => DataContext = value;
		}

    }

}
