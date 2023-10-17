using Avalonia;
using Avalonia.Media;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// The definition for a substitution within a <see cref="CodeExample"/>.
	/// </summary>
	public class CodeExampleSubstitution : AvaloniaObject {

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="IsEnabled"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> IsEnabledProperty
			= AvaloniaProperty.Register<CodeExampleSubstitution, bool>(nameof(IsEnabled), defaultValue: true);

		/// <summary>
		/// Defines the <see cref="Value"/> property.
		/// </summary>
		public static readonly StyledProperty<object?> ValueProperty
			= AvaloniaProperty.Register<CodeExampleSubstitution, object?>(nameof(Value));

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Returns the current <see cref="Value"/> as a string when <see cref="IsEnabled"/> is <c>true</c>;
		/// otherwise an empty string is returned.
		/// </summary>
		/// <returns>The string representation of the value.</returns>
		internal string ValueAsString() {
			if (!IsEnabled)
				return string.Empty;

			object? resolvedValue = Value switch {
				SolidColorBrush solidColorBrush => solidColorBrush.Color,
				_ => Value
			};

			return resolvedValue?.ToString() ?? string.Empty;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indicates if the substitution is enabled.
		/// </summary>
		/// <remarks>
		/// When <c>false</c>, <see cref="ValueAsString"/> will always return an empty string.
		/// </remarks>
		public bool IsEnabled {
			get => GetValue(IsEnabledProperty);
			set => SetValue(IsEnabledProperty, value);
		}

		/// <summary>
		/// The key.
		/// </summary>
		/// <remarks>
		/// Substitutions are identified in sample code by their key using the format <c>$(Key)</c>; e.g., <c>Width=$(Width)</c>.
		/// </remarks>
		public string? Key { get; set; }

		/// <summary>
		/// The value to be substituted.
		/// </summary>
		public object? Value {
			get => GetValue(ValueProperty);
			set => SetValue(ValueProperty, value);
		}

	}
}
