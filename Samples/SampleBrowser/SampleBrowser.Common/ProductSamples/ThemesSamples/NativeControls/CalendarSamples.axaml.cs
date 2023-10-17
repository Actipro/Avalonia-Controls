using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace ActiproSoftware.ProductSamples.ThemeSamples.NativeControls {

	public partial class CalendarSamples : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public CalendarSamples() {
			InitializeComponent();

			sampleBlackoutSeveralDays.IsCheckedChanged += this.OnSampleBlackoutSeveralDaysCheckedChanged;
			sampleBlackoutSeveralDays.IsChecked = true;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private static void BlackoutSeveralDays(Calendar calendar) {
			var blackoutStartDate = DateTime.Today.AddDays(2);

			// Reset selection to today before blacking out
			if (calendar.SelectedDate.HasValue)
				calendar.SelectedDate = DateTime.Today;

			calendar.BlackoutDates.Add(new CalendarDateRange(blackoutStartDate, blackoutStartDate.AddDays(3)));
		}

		private static void ClearBlackouts(Calendar calendar)
			=> calendar?.BlackoutDates.Clear();

		private void OnSampleBlackoutSeveralDaysCheckedChanged(object? sender, RoutedEventArgs e) {
			if (sampleBlackoutSeveralDays.IsChecked == true)
				BlackoutSeveralDays(sample);
			else
				ClearBlackouts(sample);
		}

	}

}
