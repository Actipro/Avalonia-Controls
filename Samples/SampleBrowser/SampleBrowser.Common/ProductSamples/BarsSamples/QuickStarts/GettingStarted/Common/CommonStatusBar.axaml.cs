using ActiproSoftware.SampleBrowser;
using Avalonia;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Common {

	public partial class CommonStatusBar : UserControl {

		#region Property Definitions

		/// <summary>
		/// Identifies the <see cref="SampleCodePath"/> property.
		/// </summary>
		public static readonly StyledProperty<string?> SampleCodePathProperty
			= AvaloniaProperty.Register<CommonStatusBar, string?>(nameof(SampleCodePath));

		#endregion Property Definitions

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes the class.
		/// </summary>
		static CommonStatusBar() {
			SampleCodePathProperty.Changed.AddClassHandler<CommonStatusBar>((x, e) => x.OnSampleCodePathPropertyValueChanged(e));
		}

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public CommonStatusBar() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Occurs when the <see cref="SampleCodePathProperty"/> value is changed.
		/// </summary>
		/// <param name="e">A <see cref="DependencyPropertyChangedEventArgs"/> that contains the event data.</param>
		private void OnSampleCodePathPropertyValueChanged(AvaloniaPropertyChangedEventArgs e) {
			var viewItemInfo = ApplicationViewModel.Instance?.ViewItemInfo;

			// Update the parameter passed when opening sample code
			var newValue = (e.NewValue as string);
			if ((newValue is null) || (viewItemInfo?.Path is null)) {
				viewCodeButton.Command = null;
				viewCodeButton.CommandParameter = null;
			}
			else {
				// The ViewItemInfo.Path will be pointed to the main control at the root of the "GettingStarted" series
				viewCodeButton.Command = ApplicationViewModel.Instance?.OpenGitHubSampleFolderCommand;
				viewCodeButton.CommandParameter = viewItemInfo.Path.Replace("/MainControl", newValue);
			}
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The relative path of the sample code for the current step.
		/// </summary>
		/// <value>A string value (e.g., <c>/Step01/MainWindow</c>).</value>
		public string? SampleCodePath {
			get => GetValue(SampleCodePathProperty);
			set => SetValue(SampleCodePathProperty, value);
		}

	}

}
