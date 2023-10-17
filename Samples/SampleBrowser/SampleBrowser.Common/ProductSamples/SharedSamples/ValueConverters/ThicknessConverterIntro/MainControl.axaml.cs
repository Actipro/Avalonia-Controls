using ActiproSoftware.UI.Avalonia;
using ActiproSoftware.UI.Avalonia.Controls.Converters;
using Avalonia;
using Avalonia.Controls;
using System;

namespace ActiproSoftware.ProductSamples.SharedSamples.ValueConverters.ThicknessConverterIntro {

	public partial class MainControl : UserControl {

		private string _filterSampleCode = string.Empty;
		private IDisposable? _clearBinding = null;

		#region Property definitions

		/// <summary>
		/// Defines the <see cref="FilterSampleCode"/> property.
		/// </summary>
		public static readonly DirectProperty<MainControl, string> FilterSampleCodeProperty
			= AvaloniaProperty.RegisterDirect<MainControl, string>(nameof(FilterSampleCode), x => x.FilterSampleCode);

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			filterLeftCheckBox.IsCheckedChanged += this.OnFilterCheckBoxIsCheckedChanged;
			filterTopCheckBox.IsCheckedChanged += this.OnFilterCheckBoxIsCheckedChanged;
			filterRightCheckBox.IsCheckedChanged += this.OnFilterCheckBoxIsCheckedChanged;
			filterBottomCheckBox.IsCheckedChanged += this.OnFilterCheckBoxIsCheckedChanged;

			ConfigureBinding();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void ConfigureBinding(ThicknessConverter? converter = null) {
			// Remove any previous binding
			_clearBinding?.Dispose();

			// Configure new binding
			converter ??= GetConverter();
			_clearBinding = basicSampleBorder.BindToProperty(Border.BorderThicknessProperty, basicSample, TextBox.TextProperty, Avalonia.Data.BindingMode.OneWay, converter);
		}

		private string FilterSampleCode {
			get => _filterSampleCode;
			set => SetAndRaise(FilterSampleCodeProperty, ref _filterSampleCode, value);
		}

		private ThicknessConverter? GetConverter() {
			if (this.TryFindResource(nameof(ThicknessConverter), out var resource)
				&& resource is ThicknessConverter converter) {

				return converter;
			}
			return null;
		}

		private Sides GetFilterSides() {
			var sides = Sides.None;
			if (filterLeftCheckBox.IsChecked == true)
				sides |= Sides.Left;
			if (filterTopCheckBox.IsChecked == true)
				sides |= Sides.Top;
			if (filterRightCheckBox.IsChecked == true)
				sides |= Sides.Right;
			if (filterBottomCheckBox.IsChecked == true)
				sides |= Sides.Bottom;
			return sides;
		}

		private void OnFilterCheckBoxIsCheckedChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
			var converter = GetConverter();
			if (converter is not null) {

				converter.Filter = GetFilterSides();
				ConfigureBinding(converter);

				this.FilterSampleCode = (converter.Filter == Sides.All) ? string.Empty : converter.Filter.ToString();
			}
		}

	}

}
