using Avalonia;
using Avalonia.Controls.Primitives;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Renders a section heading.
	/// </summary>
	public class NullableStringSettingControl : TemplatedControl {

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="CoerceEmptyAsNull"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> CoerceEmptyAsNullProperty
			= AvaloniaProperty.Register<NullableStringSettingControl, bool>(nameof(CoerceEmptyAsNull));

		/// <summary>
		/// Defines the <see cref="IsCoerceCheckBoxVisible"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> IsCoerceCheckBoxVisibleProperty
			= AvaloniaProperty.Register<NullableStringSettingControl, bool>(nameof(IsCoerceCheckBoxVisible), defaultValue: true);

		/// <summary>
		/// Defines the <see cref="IsWatermarkVisible"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> IsWatermarkVisibleProperty
			= AvaloniaProperty.Register<NullableStringSettingControl, bool>(nameof(IsWatermarkVisible), defaultValue: true);

		/// <summary>
		/// Defines the <see cref="Label"/> property.
		/// </summary>
		public static readonly StyledProperty<string?> LabelProperty
			= AvaloniaProperty.Register<NullableStringSettingControl, string?>(nameof(Label));

		/// <summary>
		/// Defines the <see cref="Text"/> property.
		/// </summary>
		public static readonly StyledProperty<string?> TextProperty
			= AvaloniaProperty.Register<NullableStringSettingControl, string?>(nameof(Text), coerce: CoerceTextPropertyValue);

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public NullableStringSettingControl() {
			CoerceEmptyAsNullProperty.Changed.AddClassHandler<NullableStringSettingControl>((x, _) => x.CoerceValue(TextProperty));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private static string? CoerceTextPropertyValue(AvaloniaObject obj, string? value) {
			if (obj is NullableStringSettingControl control && control.CoerceEmptyAsNull && string.IsNullOrEmpty(value))
				return control.CoerceEmptyAsNull ? null : string.Empty;

			// Don't allow non-coerced null value to be returned
			return value ?? string.Empty;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indicates if the text should be coerced to null when it is an empty string.
		/// </summary>
		public bool CoerceEmptyAsNull {
			get => GetValue(CoerceEmptyAsNullProperty);
			set => SetValue(CoerceEmptyAsNullProperty, value);
		}

		/// <summary>
		/// Indicates if the coerce checkbox should be visible.
		/// </summary>
		public bool IsCoerceCheckBoxVisible {
			get => GetValue(IsCoerceCheckBoxVisibleProperty);
			set => SetValue(IsCoerceCheckBoxVisibleProperty, value);
		}

		/// <summary>
		/// Indicates if the textbox watermark is visible.
		/// </summary>
		public bool IsWatermarkVisible {
			get => GetValue(IsWatermarkVisibleProperty);
			set => SetValue(IsWatermarkVisibleProperty, value);
		}

		/// <summary>
		/// The label.
		/// </summary>
		public string? Label {
			get => GetValue(LabelProperty);
			set => SetValue(LabelProperty, value);
		}

		/// <inheritdoc/>
		protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
			base.OnApplyTemplate(e);

			this.CoerceValue(TextProperty);
		}

		/// <summary>
		/// The text.
		/// </summary>
		public string? Text {
			get => GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

	}

}
