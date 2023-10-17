using ActiproSoftware.UI.Avalonia.Media;
using Avalonia;
using Avalonia.Media;

namespace ActiproSoftware.SampleBrowser.Utilities.ThemeResourceBrowser {

	/// <summary>
	/// A theme resource view model.
	/// </summary>
	public class ThemeResourceViewModel : ObservableObjectBase {

		private ThemeResourceReferenceTextKind _referenceTextKind = ThemeResourceReferenceTextKind.XamlDynamicResource;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ThemeResourceViewModel(string name, object? value) {
			this.Name = name;
			this.Value = value;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public string? Kind
			=> Value is Brush ? "Brush" : Value?.GetType()?.Name;

		public string Name { get; }

		public string ResourceReferenceText {
			get {
				return ResourceReferenceTextKind switch {
					ThemeResourceReferenceTextKind.XamlDynamicResource => $"{{actipro:ThemeResource {Name}}}",
					ThemeResourceReferenceTextKind.XamlStaticResource => $"{{StaticResource {{actipro:ThemeResourceKey {Name}}}}}",
					ThemeResourceReferenceTextKind.CSharpToResourceKey => $"ThemeResourceKind.{Name}.ToResourceKey()",
					_ => string.Empty
				};
			}
		}

		public ThemeResourceReferenceTextKind ResourceReferenceTextKind {
			get => _referenceTextKind;
			set {
				if (SetProperty(ref _referenceTextKind, value)) {
					OnPropertyChanged(nameof(ResourceReferenceText));
				}
			}
		}

		public string? ToolTipText {
			get {
				if (Value is not null) {
					if (Value is SolidColorBrush brush)
						return UIColor.FromRgb(brush.Color).ToHexString();
					else if (Value is CornerRadius cornerRadius)
						return cornerRadius.IsUniform ? cornerRadius.TopLeft.ToString() : Value.ToString();
					else if (Value is Thickness thickness)
						return thickness.IsUniform ? thickness.Left.ToString() : Value.ToString();
					else if (Value is Color color)
						return UIColor.FromRgb(color).ToHexString();
					else if ((Value is BoxShadow) || (Value is FontFamily))
						return Value.ToString();
				}
				
				return null;
			}
		}

		public object? Value { get; }

	}

}
