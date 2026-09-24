using ActiproSoftware.UI.Avalonia.Media;

namespace ActiproSoftware.SampleBrowser.Utilities.ThemeResourceBrowser;

/// <summary>
/// A theme resource view model.
/// </summary>
public class ThemeResourceViewModel(string name, object? value) : ObservableObjectBase {

	private ThemeResourceReferenceTextKind _referenceTextKind = ThemeResourceReferenceTextKind.XamlDynamicResource;

	// --------------------------------------------------------------------------------------------------
	// PUBLIC PROCEDURES
	// --------------------------------------------------------------------------------------------------

	public string? Kind
		=> Value is Brush ? "Brush" : Value?.GetType()?.Name;

	public string Name { get; } = name;

	public string ResourceReferenceText {
		get => ResourceReferenceTextKind switch {
			ThemeResourceReferenceTextKind.XamlDynamicResource => $"{{actipro:ThemeResource {Name}}}",
			ThemeResourceReferenceTextKind.XamlStaticResource => $"{{StaticResource {{actipro:ThemeResourceKey {Name}}}}}",
			ThemeResourceReferenceTextKind.CSharpToResourceKey => $"ThemeResourceKind.{Name}.ToResourceKey()",
			_ => string.Empty
		};
	}

	public ThemeResourceReferenceTextKind ResourceReferenceTextKind {
		get => _referenceTextKind;
		set {
			if (SetProperty(ref _referenceTextKind, value))
				OnPropertyChanged(nameof(ResourceReferenceText));
		}
	}

	public string? ToolTipText {
		get => Value switch {
			SolidColorBrush brush => UIColor.FromRgb(brush.Color).ToHexString(),
			CornerRadius cornerRadius when cornerRadius.IsUniform => cornerRadius.TopLeft.ToString(),
			Thickness thickness when thickness.IsUniform => thickness.Left.ToString(),
			Color color => UIColor.FromRgb(color).ToHexString(),
			object o => o.ToString(),
			_ => null
		};
	}

	public object? Value { get; } = value;

}
