using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Media;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.Backstage {

	/// <summary>
	/// Defines a view model for the options used by the backstage sample.
	/// </summary>
	public class BackstageOptionsViewModel : ObservableObjectBase {

		private ICommand? _backstageHeaderButtonCommand;

		private int _backstageMinHeaderWidth = 0;
		private int _backstageMaxHeaderWidth = 300;
		private bool _canClose = true;
		private bool _canSelectFirstTabOnOpen = true;
		private bool _isBackstageOpen = false;
		private bool _isFirstBackstage = true;
		private bool _sampleButton3CanCloseBackstage = true;
		private string _sampleButton3Label = "Sample Button 3";
		private string _selectedTabKeyOnOpen = "(Previous Selection)";
		private bool _useSampleButtonImages = false;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the command to be executed when one of the sample backstage buttons is invoked.
		/// </summary>
		/// <value>An <see cref="ICommand"/>.</value>
		public ICommand BackstageHeaderButtonCommand {
			get => _backstageHeaderButtonCommand ??= new DelegateCommand<object>(_ => {
				ApplicationViewModel.Instance.MessageService?.ShowMessage(
					"When a RibbonBackstageHeaderButton is invoked and its CanCloseBackstage property is the default of true, the Backstage automatically closes.\r\n\r\nThese buttons are typically associated with commands that perform simple operations like Help, Save, or Close that do not need the additional content area of a RibbonBackstageTabItem.",
					"About RibbonBackstageHeaderButton",
					Avalonia.Controls.Notifications.NotificationType.Information);
				});
		}

		/// <summary>
		/// The maximum width of the backstage header where tabs and buttons are displayed.
		/// </summary>
		public int BackstageMaxHeaderWidth {
			get => _backstageMaxHeaderWidth;
			set {
				if (SetProperty(ref _backstageMaxHeaderWidth, value))
					BackstageMinHeaderWidth = Math.Min(this.BackstageMinHeaderWidth, _backstageMaxHeaderWidth);
			} 
		}

		/// <summary>
		/// The minimum width of the backstage header where tabs and buttons are displayed.
		/// </summary>
		public int BackstageMinHeaderWidth {
			get => _backstageMinHeaderWidth;
			set {
				if (SetProperty(ref _backstageMinHeaderWidth, value))
					BackstageMaxHeaderWidth = Math.Max(this.BackstageMaxHeaderWidth, _backstageMinHeaderWidth);
			}
		}

		/// <summary>
		/// Indicates if the backstage can be closed.
		/// </summary>
		public bool CanClose {
			get => _canClose;
			set => SetProperty(ref _canClose, value);
		}

		/// <summary>
		/// Indicates if the first tab should be selected when the Backstage is opened.
		/// </summary>
		public bool CanSelectFirstTabOnOpen {
			get => _canSelectFirstTabOnOpen;
			set => SetProperty(ref _canSelectFirstTabOnOpen, value);
		}

		/// <summary>
		/// Indicates if the backstage is open.
		/// </summary>
		public bool IsBackstageOpen {
			get => _isBackstageOpen;
			set {
				if (SetProperty(ref _isBackstageOpen, value)) {
					// When the backstage closes, set a flag that the initial backstage is no longer displayed
					if (!_isBackstageOpen)
						IsFirstBackstage = false;
				}
			}
		}

		/// <summary>
		/// Indicates if the backstage should be configured for the initial view where some tabs are larger
		/// and unnecessary buttons are hidden.
		/// </summary>
		public bool IsFirstBackstage {
			get => _isFirstBackstage;
			set {
				if (SetProperty(ref _isFirstBackstage, value))
					OnPropertyChanged(nameof(PrimaryBackstageTabVariantSize));
			}
		}

		/// <summary>
		/// The <see cref="VariantSize"/> to be used for the primary tabs.
		/// </summary>
		/// <remarks>
		/// This property is used to show large variants of the most important tabs when the backstage is initially displayed.
		/// </remarks>
		public VariantSize PrimaryBackstageTabVariantSize
			=> IsFirstBackstage ? VariantSize.Large : VariantSize.Medium;

		/// <summary>
		/// Indicates whether the third sample button can close backstage.
		/// </summary>
		public bool SampleButton3CanCloseBackstage {
			get => _sampleButton3CanCloseBackstage;
			set => SetProperty(ref _sampleButton3CanCloseBackstage, value);
		}

		/// <summary>
		/// The label to be displayed on the third sample button.
		/// </summary>
		public string SampleButton3Label {
			get => _sampleButton3Label;
			set => SetProperty(ref _sampleButton3Label, value);
		}

		/// <summary>
		/// The <see cref="IImage"/>, if any, to be displayed on the sample buttons.
		/// </summary>
		public IImage? SampleButtonImageSource
			=> UseSampleButtonImages ? ImageLoader.GetIcon("QuickStart16.png") : null;

		/// <summary>
		/// Indicates the key of the tab that should be manually selected when the backstage opens.
		/// </summary>
		public string SelectedTabKeyOnOpen {
			get => _selectedTabKeyOnOpen;
			set => SetProperty(ref _selectedTabKeyOnOpen, value);
		}

		/// <summary>
		/// Indicates if images should be displayed on the sample buttons.
		/// </summary>
		public bool UseSampleButtonImages {
			get => _useSampleButtonImages;
			set {
				if (SetProperty(ref _useSampleButtonImages, value))
					OnPropertyChanged(nameof(SampleButtonImageSource));
			}
		}

	}

}
