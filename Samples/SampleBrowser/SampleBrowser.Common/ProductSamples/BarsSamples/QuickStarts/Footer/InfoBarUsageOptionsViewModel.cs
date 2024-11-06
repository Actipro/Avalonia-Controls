using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using Avalonia;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.Footer {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class InfoBarUsageOptionsViewModel : ObservableObjectBase {

		private bool _canClose = true;
		private bool _isIconVisible = true;
		private Thickness _padding = new(10, 5);
		private RibbonQuickAccessToolBarLocation _qatLocation = RibbonQuickAccessToolBarLocation.Below;
		private InfoBarSeverity _severity = InfoBarSeverity.Success;
		private ICommand? _showFooterMvvmCommand;
		private ICommand? _showFooterXamlCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indicates if the info bar can be closed.
		/// </summary>
		public bool CanClose {
			get => _canClose;
			set => SetProperty(ref _canClose, value);
		}

		/// <summary>
		/// Indicates if the info bar icon is visible.
		/// </summary>
		public bool IsIconVisible {
			get => _isIconVisible;
			set => SetProperty(ref _isIconVisible, value);
		}

		/// <summary>
		/// The padding for the info bar.
		/// </summary>
		public Thickness Padding {
			get => _padding;
			set => SetProperty(ref _padding, value);
		}

		/// <summary>
		/// Gets or sets the location of the Quick Access Toolbar.
		/// </summary>
		/// <value>One of the <see cref="RibbonQuickAccessToolBarLocation"/> values.</value>
		public RibbonQuickAccessToolBarLocation QuickAccessToolBarLocation {
			get => _qatLocation;
			set => SetProperty(ref _qatLocation, value);
		}

		/// <summary>
		/// The severity of the info bar.
		/// </summary>
		public InfoBarSeverity Severity {
			get => _severity;
			set => SetProperty(ref _severity, value);
		}

		/// <summary>
		/// The command that will be executed to show the MVVM-based footer.
		/// </summary>
		public ICommand? ShowFooterMvvmCommand {
			get => _showFooterMvvmCommand;
			set => SetProperty(ref _showFooterMvvmCommand, value);
		}

		/// <summary>
		/// The command that will be executed to show the XAML-based footer.
		/// </summary>
		public ICommand? ShowFooterXamlCommand {
			get => _showFooterXamlCommand;
			set => SetProperty(ref _showFooterXamlCommand, value);
		}

	}

}
