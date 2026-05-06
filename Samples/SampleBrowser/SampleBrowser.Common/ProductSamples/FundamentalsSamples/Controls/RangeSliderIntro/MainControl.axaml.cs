using ActiproSoftware.Extensions;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.RangeSliderIntro;

public partial class MainControl : UserControl {

	private ICommand? _addMultiValueCommand;
	private ICommand? _clearMultiValuesCommand;
	private DelegateCommand<double?>? _removeMultiValueCommand;

	/// <summary>
	/// Defines the <see cref="Values"/> property.
	/// </summary>
	public static readonly StyledProperty<ObservableCollection<double>> MultiValuesProperty
		= AvaloniaProperty.Register<MainControl, ObservableCollection<double>>(nameof(MultiValues));

	// --------------------------------------------------------------------------------------------------
	// OBJECT
	// --------------------------------------------------------------------------------------------------

	public MainControl() {
		// Initialize the collection of multiple values
		MultiValues = [ 25, 50, 75 ];

		InitializeComponent();
	}

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <summary>
	/// The command to add a random new value to <see cref="MultiValues"/>.
	/// </summary>
	public ICommand AddMultiValueCommand
		=> _addMultiValueCommand ??= new DelegateCommand<object>(_ => {
			// Create a new value that is within the allowed range
			double newValue = Random.Shared.Next(
				(int)multiSlider.Minimum.Round(RoundMode.Ceiling),
				(int)multiSlider.Maximum.Round(RoundMode.Floor));

			MultiValues.Add(newValue);
		});

	/// <summary>
	/// The command to clear all values from <see cref="MultiValues"/>.
	/// </summary>
	public ICommand ClearMultiValuesCommand
		=> _clearMultiValuesCommand ??= new DelegateCommand<object>(_ => MultiValues.Clear());

	/// <summary>
	/// A collection of all the values used in the multi values sample.
	/// </summary>
	public ObservableCollection<double> MultiValues {
		get => GetValue(MultiValuesProperty);
		set => SetValue(MultiValuesProperty, value);
	}

	/// <summary>
	/// The command to remove a value <see cref="MultiValues"/>.
	/// </summary>
	public ICommand RemoveMultiValueCommand
		=> _removeMultiValueCommand ??= new DelegateCommand<double?>(
			executeAction: param => {
				if (param.HasValue)
					MultiValues.Remove(param.Value);
			},
			canExecuteFunc: param => param.HasValue
		);

}