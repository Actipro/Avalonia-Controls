using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using Avalonia.Layout;
using System;
using System.Linq;

namespace ActiproSoftware.ProductSamples.ThemeSamples.NativeControls {

	public partial class OtherControlsSamples : UserControl {

		private WindowNotificationManager? _notificationManager;

		// --------------------------------------------------------------------------------------------------
		// OBJECT
		// --------------------------------------------------------------------------------------------------

		public OtherControlsSamples() {
			InitializeComponent();

			carouselSample.PageTransition = new PageSlide(TimeSpan.FromSeconds(0.25), PageSlide.SlideAxis.Horizontal);

			InitializeNavigationPageSample();
		}

		// --------------------------------------------------------------------------------------------------
		// NON-PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		private void InitializeNavigationPageSample() {
			var gotoPage2Button = new Button() {
				Content = "Next Page",
				HorizontalAlignment = HorizontalAlignment.Center
			};

			gotoPage2Button.Click += (s, e) => {
				var openModalPage3Button = new Button() {
					Content = "Open Modal",
					HorizontalAlignment = HorizontalAlignment.Center
				};

				openModalPage3Button.Click += (s, e) => {
					var closeModalButton = new Button() {
						Content = "Close Modal",
						HorizontalAlignment = HorizontalAlignment.Center
					};

					closeModalButton.Click += (s, e) => {
						navigationPageSample.PopModalAsync();
					};

					var page3 = new ContentPage() {
						Header = "Modal Page Header",
						Content = new StackPanel() {
							Spacing = 20,
							HorizontalAlignment = HorizontalAlignment.Center,
							VerticalAlignment = VerticalAlignment.Center,
							Children = {
								new TextBlock {
									Text = "This is a third ContentPage within a NavigationPage that is opened modally.",
									HorizontalAlignment = HorizontalAlignment.Center,
									VerticalAlignment = VerticalAlignment.Center,
								},
								closeModalButton,
							}
						}
					};

					NavigationPage.SetHasBackButton(page3, true);
					NavigationPage.SetHasNavigationBar(page3, true);

					navigationPageSample.PushModalAsync(page3);
				};

				var page2 = new ContentPage() {
					Header = "Second Page Header",
					Content = new StackPanel() {
						Spacing = 20,
						HorizontalAlignment = HorizontalAlignment.Center,
						VerticalAlignment = VerticalAlignment.Center,
						Children = {
							new TextBlock {
								Text = "This is the second ContentPage within a NavigationPage.",
							},
							openModalPage3Button,
						},
					},
				};

				// For testing CommandBars
				/*
				NavigationPage.SetTopCommandBar(page2, new CommandBar() {
					PrimaryCommands = {
							new CommandBarButton() {
								Label = "Top"
							},
						},
					SecondaryCommands = {
							new CommandBarButton() {
								Label = "Settings"
							},
						}
				});

				NavigationPage.SetBottomCommandBar(page2, new CommandBar() {
					PrimaryCommands = {
							new CommandBarButton() {
								Label = "Bottom"
							},
						},
					SecondaryCommands = {
							new CommandBarButton() {
								Label = "Settings"
							},
						}
				});
				//*/

				NavigationPage.SetHasBackButton(page2, true);
				NavigationPage.SetHasNavigationBar(page2, true);

				navigationPageSample.PushAsync(page2);
			};

			var page1 = new ContentPage() {
				Header = "First Page Header",
				Content = new StackPanel() {
					Spacing = 20,
					HorizontalAlignment = HorizontalAlignment.Center,
					VerticalAlignment = VerticalAlignment.Center,
					Children = {
						new TextBlock {
							Text = "This is the first ContentPage within a NavigationPage.",
						},
						gotoPage2Button,
					}
				}
			};

			NavigationPage.SetHasBackButton(page1, true);
			NavigationPage.SetHasNavigationBar(page1, true);
			navigationPageSample.PushAsync(page1);
		}

		private void OnCarouselNextButtonClick(object? sender, RoutedEventArgs e) {
			carouselSample.Next();
		}

		private void OnCarouselPageNextButtonClick(object? sender, RoutedEventArgs e) {
			if (carouselPageSample.SelectedIndex < carouselPageSample.Pages?.Count() - 1)
				carouselPageSample.SelectedIndex++;
		}

		private void OnCarouselPagePreviousButtonClick(object? sender, RoutedEventArgs e) {
			if (carouselPageSample.SelectedIndex > 0)
				carouselPageSample.SelectedIndex--;
		}

		private void OnCarouselPreviousButtonClick(object? sender, RoutedEventArgs e) {
			carouselSample.Previous();
		}

		private void OnNotificationCardButtonClicked(object? sender, RoutedEventArgs e) {
			if (_notificationManager is not null) {
				if ((sender is Button button) && (button.Tag is NotificationType notificationType))
					_notificationManager?.Show(new Notification($"{notificationType} Notification", $"This message has a notification type of {notificationType} and was generated at {DateTime.Now:HH:mm:ss}.", notificationType));
				else
					_notificationManager?.Show("This message is a simple text string.");
			}
		}

		// --------------------------------------------------------------------------------------------------
		// PUBLIC PROCEDURES
		// --------------------------------------------------------------------------------------------------

		protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
			_notificationManager ??= new WindowNotificationManager(TopLevel.GetTopLevel(this));
			base.OnAttachedToVisualTree(e);
		}

	}

}
