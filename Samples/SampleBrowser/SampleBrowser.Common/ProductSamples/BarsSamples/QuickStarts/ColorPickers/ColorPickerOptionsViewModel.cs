using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using ActiproSoftware.UI.Avalonia.Media;
using Avalonia;
using Avalonia.Media;
using System;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.ColorPickers {

	// IMPORTANT NOTE: This common view model class is used by most samples, but not every property is used by every sample

	/// <summary>
	/// Defines configurable options for various color picker samples.
	/// </summary>
	public class ColorPickerOptionsViewModel : ObservableObjectBase {

		private bool _areSurroundingSeparatorsAllowed = true;
		private string _baseImageSmall = "FontColor16.png";
		private bool _canCategorizeOnMenu = true;
		private bool _canFilterOnMenu = false;
		private Color _livePreviewColor = Colors.Transparent;
		private int _maxColumnCount = 5;
		private int _minColumnCount = 1;
		private int _minItemLength = 16;
		private int _itemSpacing = 4;
		private Color _selectedColor = Colors.Red;
		private ICommand? _setColorCommand;
		private object? _smallIcon;
		private bool _useAccentedItemBorder = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ColorPickerOptionsViewModel() {
			RefreshSmallIcon();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Refreshes the Font image based on the current font color.
		/// </summary>
		private void RefreshSmallIcon() {
			// Update the Font image to show the selected color
			var color = SelectedColor;
			this.SmallIcon = CreateBitmapImageWithColorBar(color, BaseImageSmall);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets or sets if separators are displayed immediately before and after a gallery.
		/// </summary>
		public bool AreSurroundingSeparatorsAllowed {
			get => _areSurroundingSeparatorsAllowed;
			set => SetProperty(ref _areSurroundingSeparatorsAllowed, value);
		}

		/// <summary>
		/// The name of the base image used to create the small image source with the selected color applied.
		/// </summary>
		public string BaseImageSmall {
			get => _baseImageSmall;
			set {
				if (SetProperty(ref _baseImageSmall, value))
					RefreshSmallIcon();
			}
		}

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
		/// Creates an <see cref="IImage"/> where a color bar is added to a pre-defined image.
		/// </summary>
		/// <param name="color">The color to be used when rendering the color bar.</param>
		/// <param name="fileName">The name of the file resource which defines the base image.</param>
		/// <returns>An <see cref="IImage"/> of the composed image.</returns>
		protected static IImage? CreateBitmapImageWithColorBar(Color color, string fileName) {
			// Load the base image
			var imageSource = ImageLoader.GetIcon(fileName);
			if (imageSource is not null) {

				// Determine the bounds of the color bar
				var imageHeight = imageSource.Size.Height;
				var imageWidth = imageSource.Size.Width;
				if (imageHeight > 0 && imageWidth > 0) {
					var colorBarHeight = Math.Max(1, imageHeight / 4);
					var colorBarBounds = new Rect(0, imageHeight - colorBarHeight, imageWidth, colorBarHeight);
					if ((colorBarBounds.Height > 0) && (colorBarBounds.Width > 0)) {
						// Add the color bar to the image
						imageSource = ImageProvider.GetImageSourceWithColorSwatch(imageSource, colorBarBounds, color);
					}
				}

				return imageSource;
			}

			return null;
		}

		/// <summary>
		/// The amount of spacing between gallery items.
		/// </summary>
		public int ItemSpacing {
			get => _itemSpacing;
			set => SetProperty(ref _itemSpacing, value);
		}

		/// <summary>
		/// The live preview color.
		/// </summary>
		public Color LivePreviewColor {
			get => _livePreviewColor;
			set => SetProperty(ref _livePreviewColor, value);
		}

		/// <summary>
		/// The maximum number of columns.
		/// </summary>
		public int MaxColumnCount {
			get => _maxColumnCount;
			set {
				if (SetProperty(ref _maxColumnCount, value))
					MinColumnCount = Math.Min(MinColumnCount, _maxColumnCount);
			}
		}

		/// <summary>
		/// The minimum number of columns.
		/// </summary>
		public int MinColumnCount {
			get => _minColumnCount;
			set {
				if (SetProperty(ref _minColumnCount, value))
					MaxColumnCount = Math.Max(MaxColumnCount, _minColumnCount);
			}
		}

		/// <summary>
		/// The minimum height and width of the gallery item.
		/// </summary>
		public int MinItemLength {
			get => _minItemLength;
			set => SetProperty(ref _minItemLength, value);
		}

		/// <summary>
		/// The selected color.
		/// </summary>
		public Color SelectedColor {
			get => _selectedColor;
			set {
				if (SetProperty(ref _selectedColor, value))
					RefreshSmallIcon();
			}
		}

		/// <summary>
		/// The small icon which can include a swatch of the selected color.
		/// </summary>
		public object? SmallIcon {
			get => _smallIcon;
			set => SetProperty(ref _smallIcon, value);
		}

		/// <summary>
		/// The command to be executed when setting a color.
		/// </summary>
		public ICommand SetColorCommand {
			get {
				// Use PreviewableDelegateCommand to support being notified of when the user moves the mouse over a gallery item (or gives it
				// keyboard focus) to preview the effect; otherwise any ICommand can be used if preview is not desired
				return _setColorCommand ??= new PreviewableDelegateCommand<ColorBarGalleryItemViewModel>(
					executeAction: param => {
						if (param is not null)
							SelectedColor = param.Value;
					},
					canExecuteFunc: _ => true,
					previewAction: param => {
						if (param is not null)
							LivePreviewColor = param.Value;
					},
					cancelPreviewAction: _ => LivePreviewColor = Colors.Transparent
				);
			}
		}

		/// <summary>
		/// Indicates if an accented border is displayed around gallery items.
		/// </summary>
		public bool UseAccentedItemBorder {
			get => _useAccentedItemBorder;
			set => SetProperty(ref _useAccentedItemBorder, value);
		}

	}

}
