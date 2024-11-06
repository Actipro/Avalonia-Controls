using ActiproSoftware.UI.Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;
using System.ComponentModel;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GalleryInRibbon {

	/// <summary>
	/// Defines configurable options for this sample.
	/// </summary>
	public class OptionsViewModel : ObservableObjectBase {

		private bool _canCategorizeOnMenu = false;
		private bool _canFilterOnMenu = false;
		private int _desiredRowCount = 1;
		private int _itemSpacing = 4;
		private IDataTemplate? _itemTemplate;
		private bool _isSetColorCommandEnabled = true;
		private int _minLargeRibbonColumnCount = 6;
		private int _maxMenuColumnCount = 50;
		private int _maxRibbonColumnCount = 100;
		private int _minMediumRibbonColumnCount = 3;
		private ControlResizeMode _menuResizeMode = ControlResizeMode.Both;
		private int _minMenuColumnCount = 1;
		private bool _useAccentedItemBorder = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// If the gallery is categorized when displayed as a menu.
		/// </summary>
		public bool CanCategorizeOnMenu {
			get => _canCategorizeOnMenu;
			set {
				if (SetProperty(ref _canCategorizeOnMenu, value)) {

					if (!_canCategorizeOnMenu) {
						// Disable filtering if categories are not active
						CanFilterOnMenu = false;
					}
				}
			}
		}

		/// <summary>
		/// If the gallery can be filtered when displayed as a menu.
		/// </summary>
		public bool CanFilterOnMenu {
			get => _canFilterOnMenu;
			set {
				if (SetProperty(ref _canFilterOnMenu, value)) {
					if (_canFilterOnMenu) {
						// Ensure categorization is enabled or filtering has no effect
						CanCategorizeOnMenu = true;
					}
				}
			}
		}

		/// <summary>
		/// The desired number of rows to be displayed as a value between 1 and 3.
		/// </summary>
		public int DesiredRowCount {
			get => _desiredRowCount;
			set {
				if (!(1 <= value && value <= 3))
					throw new ArgumentOutOfRangeException();
				if (SetProperty(ref _desiredRowCount, value)) {
					OnPropertyChanged(nameof(IsUsingOneRowLayout));
					OnPropertyChanged(nameof(IsUsingTwoRowLayout));
					OnPropertyChanged(nameof(IsUsingThreeRowLayout));
				}
			}
		}

		/// <summary>
		/// If the <see cref="SetColorCommand"/> can be executed.
		/// </summary>
		public bool IsSetColorCommandEnabled {
			get => _isSetColorCommandEnabled;
			set => SetProperty(ref _isSetColorCommandEnabled, value);
		}

		/// <summary>
		/// Indicates if the sample is currently configured with the one-row layout.
		/// </summary>
		public bool IsUsingOneRowLayout
			=> DesiredRowCount == 1;

		/// <summary>
		/// Indicates if the sample is currently configured with the three-row layout.
		/// </summary>
		public bool IsUsingThreeRowLayout
			=> DesiredRowCount == 3;

		/// <summary>
		/// Indicates if the sample is currently configured with the two-row layout.
		/// </summary>
		public bool IsUsingTwoRowLayout
			=> DesiredRowCount == 2;

		/// <summary>
		/// The amount of spacing between gallery items.
		/// </summary>
		public int ItemSpacing {
			get => _itemSpacing;
			set => SetProperty(ref _itemSpacing, value);
		}

		/// <summary>
		/// The template used to display items in the gallery.
		/// </summary>
		public IDataTemplate? ItemTemplate {
			get => _itemTemplate;
			set => SetProperty(ref _itemTemplate, value);
		}

		/// <summary>
		/// The maximum number of columns used for gallery items when displayed in a menu.
		/// </summary>
		public int MaxMenuColumnCount {
			get => _maxMenuColumnCount;
			set => SetProperty(ref _maxMenuColumnCount, value);
		}

		/// <summary>
		/// The maximum number of columns used for gallery items when displayed in the ribbon.
		/// </summary>
		public int MaxRibbonColumnCount {
			get => _maxRibbonColumnCount;
			set => SetProperty(ref _maxRibbonColumnCount, value);
		}

		/// <summary>
		/// If a menu can be resized.
		/// </summary>
		public ControlResizeMode MenuResizeMode {
			get => _menuResizeMode;
			set => SetProperty(ref _menuResizeMode, value);
		}

		/// <summary>
		/// The minimum number of columns used for gallery items when displayed in the ribbon with a large variant size.
		/// </summary>
		public int MinLargeRibbonColumnCount {
			get => _minLargeRibbonColumnCount;
			set => SetProperty(ref _minLargeRibbonColumnCount, value);
		}

		/// <summary>
		/// The minimum number of columns used for gallery items when displayed in the ribbon with a medium variant size.
		/// </summary>
		public int MinMediumRibbonColumnCount {
			get => _minMediumRibbonColumnCount;
			set => SetProperty(ref _minMediumRibbonColumnCount, value);
		}

		/// <summary>
		/// The minimum number of columns used for gallery items when displayed in a menu.
		/// </summary>
		public int MinMenuColumnCount {
			get => _minMenuColumnCount;
			set => SetProperty(ref _minMenuColumnCount, value);
		}

		/// <summary>
		/// If an accented border is displayed around gallery items.
		/// </summary>
		public bool UseAccentedItemBorder {
			get => _useAccentedItemBorder;
			set => SetProperty(ref _useAccentedItemBorder, value);
		}

	}

}
