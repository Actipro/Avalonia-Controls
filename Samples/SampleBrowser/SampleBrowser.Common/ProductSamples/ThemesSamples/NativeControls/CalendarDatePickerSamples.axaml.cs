using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using Avalonia.Controls;
using System;

namespace ActiproSoftware.ProductSamples.ThemeSamples.NativeControls {

	public partial class CalendarDatePickerSamples : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public CalendarDatePickerSamples() {
			InitializeComponent();

			// Blackout tomorrow's date
			sampleBlackoutTomorrow.IsCheckedChanged += this.OnSampleBlackoutTomorrowIsCheckedChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private static void ClearBlackouts(CalendarDatePicker picker)
			=> picker?.BlackoutDates?.Clear();

		private void OnSampleBlackoutTomorrowIsCheckedChanged(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
			if (sampleBlackoutTomorrow.IsChecked == true) {
				if (!TryBlackoutTomorrow(sample))
					sampleBlackoutTomorrow.IsChecked = false;
			}
			else
				ClearBlackouts(sample);
		}

		private static bool TryBlackoutTomorrow(CalendarDatePicker picker) {
			var tomorrow = DateTime.Today.AddDays(1);

			// Must clear selection before blacking out
			if (picker.SelectedDate == tomorrow) {
				ApplicationViewModel.Instance.MessageService?.ShowError("Cannot blackout a date that is currently selected.");
				return false;
			}

			picker.BlackoutDates?.Add(new CalendarDateRange(tomorrow));
			return true;
		}

	}

}
