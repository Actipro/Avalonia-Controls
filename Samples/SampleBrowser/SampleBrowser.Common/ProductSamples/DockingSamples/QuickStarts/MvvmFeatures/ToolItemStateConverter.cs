using ActiproSoftware.UI.Avalonia.Controls.Docking;
using Avalonia.Data.Converters;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures;

//
// NOTE: This converter and the related ToolItemState enum can be used in scenarios where you don't wish for your models to directly 
//   reference types in the Docking/MDI assembly... it allows you to have a layer of abstraction if desired, but there
//   is nothing wrong with directly referencing DockingWindowState in your VM class to avoid having to use this abstraction layer
//

/// <summary>
/// Represents a value converter that can convert a custom <see cref="ToolItemState"/> enum value
/// one of the Actipro <see cref="DockingWindowState"/> enum type values.
/// </summary>
public sealed class ToolItemStateConverter : IValueConverter {

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	/// <inheritdoc cref="IValueConverter.Convert(object?, Type, object?, CultureInfo)"/>
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if (value is null)
			return null;

		return (ToolItemState)value switch {
			ToolItemState.AutoHide => DockingWindowState.AutoHide,
			ToolItemState.Docked => DockingWindowState.Docked,
			ToolItemState.Document or _ => DockingWindowState.Document
		};
	}

	/// <inheritdoc cref="IValueConverter.ConvertBack(object?, Type, object?, CultureInfo)"/>
	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if (value is null)
			return null;

		return (DockingWindowState)value switch {
			DockingWindowState.AutoHide => ToolItemState.AutoHide,
			DockingWindowState.Docked => ToolItemState.Docked,
			DockingWindowState.Document or _ => ToolItemState.Document
		};
	}

}
