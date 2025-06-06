using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a gallery item within a bar gallery control.
	/// </summary>
	/// <typeparam name="TValue">The type of the value associated with this gallery item.</typeparam>
	public class BarGalleryItemViewModel<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] TValue> : ObservableObjectBase, IBarGalleryItemViewModel {

		private static bool? isEnumValueType;

		private string? _category;
		private string? _description;
		private bool _isVisible = true;
		private string? _keyTipText;
		private string? _label;
		private BarGalleryItemLayoutBehavior _layoutBehavior = BarGalleryItemLayoutBehavior.Default;
		private object? _icon;
		private object? _tag;
		private TValue? _value;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		protected BarGalleryItemViewModel()
			: this(value: default, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value.
		/// </summary>
		/// <param name="value">The item's value.</param>
		protected BarGalleryItemViewModel(TValue? value)
			: this(value, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and category.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		protected BarGalleryItemViewModel(TValue? value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value, category, and label.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		protected BarGalleryItemViewModel(TValue? value, string? category, string? label) {
			_value = value;
			_category = category;
			_label = label;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATION
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		object? IBarGalleryItemViewModel.Value {
			get => this.Value;
			set => this.Value = (value is null ? default : (TValue)value);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Tests if the value type of this class is an enumeration.
		/// </summary>
		/// <value><c>true</c> if the value type of this class is an enumeration; otherwise <c>false</c>.</value>
		private static bool IsEnumValueType => (isEnumValueType ??= typeof(TValue).IsEnum);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IBarGalleryItemViewModel.Category"/>
		public string? Category {
			get => _category;
			set => SetProperty(ref _category, value);
		}

		/// <summary>
		/// Gets the coerced value to use as the <see cref="Label"/> when an explicit value has not been defined.
		/// </summary>
		/// <returns>The coerced text label to display.</returns>
		/// <seealso cref="Label"/>
		protected virtual string? CoerceLabel() {
			return IsEnumValueType
				? ConvertEnumValueToString(typeof(TValue), Value, useAttributes: true)
				: Value?.ToString();
		}

		/// <summary>
		/// Converts the specified enumeration value to a string representation.
		/// </summary>
		/// <param name="enumValue">The enumeration value.</param>
		/// <param name="useAttributes">Whether to use description or display attributes.</param>
		/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
		/// <returns>A string representation of the specified value or <c>null</c> if the value type is not an enumeration.</returns>
		protected static string? ConvertEnumValueToString<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] TEnum>(TEnum enumValue, bool useAttributes)
			where TEnum : Enum {

			return ConvertEnumValueToString(typeof(TEnum), enumValue, useAttributes);
		}

		/// <summary>
		/// Converts the specified enumeration value to a string representation.
		/// </summary>
		/// <param name="enumType">The enumeration <see cref="Type"/> to examine.</param>
		/// <param name="enumValue">The enumeration value.</param>
		/// <param name="useAttributes">Whether to use description or display attributes.</param>
		/// <returns>A string representation of the specified value or <c>null</c> if the value and/or type is not recognized as an enumeration.</returns>
		protected static string? ConvertEnumValueToString([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] Type enumType, object? enumValue, bool useAttributes) {
			// Null values are already null strings
			if (enumValue is null)
				return null;

			// Ignore non-enum types
			if ((enumType is null) || (!enumType.IsEnum))
				return null;

			string? valueText = enumValue.ToString();

			if ((useAttributes) && (!string.IsNullOrEmpty(valueText))) {
				var fieldInfo = enumType.GetField(valueText);
				if (fieldInfo != null) {
					var attributeText = fieldInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;
					if (attributeText is null) {
						var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
						attributeText = displayAttribute?.GetName() ?? displayAttribute?.GetShortName();
					}
					if (!string.IsNullOrEmpty(attributeText))
						return attributeText;
				}
			}

			return valueText;
		}

		/// <inheritdoc cref="IBarGalleryItemViewModel.Description"/>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}

		/// <inheritdoc />
		public override sealed bool Equals(object? obj) {
			if (obj is IBarGalleryItemViewModel other)
				return Equals(other);
			return false;
		}

		/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
		public virtual bool Equals(IBarGalleryItemViewModel? other) {
			return other is not null
				&& this.GetType() == other.GetType()
				&& ObjectsEqual(this.Value, other.Value)
				&& this.Category == other.Category
				&& this.Description == other.Description
				&& this.Icon == other.Icon
				&& this.KeyTipText == other.KeyTipText
				&& this.Label == other.Label
				&& this.LayoutBehavior == other.LayoutBehavior;

			static bool ObjectsEqual(object? left, object? right) {
				if (left is null)
					return right is null;
				else
					return left.Equals(right);
			}
		}

		/// <inheritdoc />
		public override int GetHashCode()
			=> HashCode.Combine(GetType(), Category, Description, Icon, KeyTipText, Label, LayoutBehavior, Value);
		
		/// <inheritdoc cref="IBarGalleryItemViewModel.Icon"/>
		public object? Icon {
			get => _icon;
			set => SetProperty(ref _icon, value);
		}

		/// <inheritdoc cref="IBarGalleryItemViewModel.IsLabelVisible"/>
		public virtual bool IsLabelVisible
			=> _layoutBehavior == BarGalleryItemLayoutBehavior.MenuItem;

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <inheritdoc cref="IBarGalleryItemViewModel.KeyTipText"/>
		public string? KeyTipText {
			get => _keyTipText;
			set => SetProperty(ref _keyTipText, value);
		}

		/// <inheritdoc cref="IBarGalleryItemViewModel.Label"/>
		/// <remarks>If the label is not explicitly defined, the value may be coerced.</remarks>
		/// <see cref="CoerceLabel"/>
		public string? Label {
			get => _label ?? CoerceLabel();
			set => SetProperty(ref _label, value);
		}

		/// <inheritdoc cref="IBarGalleryItemViewModel.LayoutBehavior"/>
		public BarGalleryItemLayoutBehavior LayoutBehavior {
			get => _layoutBehavior;
			set {
				if (SetProperty(ref _layoutBehavior, value))
					this.OnPropertyChanged(nameof(IsLabelVisible));
			}
		}

		/// <summary>
		/// Raises the <see cref="INotifyPropertyChanged.PropertyChanged"/> event
		/// for the <see cref="Value"/> property and any other properties that are dependent on the value.
		/// </summary>
		protected virtual void OnValueChanged() {
			this.OnPropertyChanged(nameof(Value));
			this.OnPropertyChanged(nameof(Label));
		}
		
		/// <inheritdoc cref="IHasTag.Tag"/>
		public object? Tag {
			get => _tag;
			set => SetProperty(ref _tag, value);
		}

		/// <inheritdoc/>
		public override string ToString() {
			// The label is coerced from the value when label is not explicitly defined,
			// so only include the label in properties if it is explicitly defined
			var properties = $"Value='{this.Value?.ToString() ?? "<null>"}'";
			if (_label is not null)
				properties += $", Label='{_label}'";

			return $"{this.GetType().FullName}[{properties}]";
		}

		/// <summary>
		/// The value associated with this view model.
		/// </summary>
		public virtual TValue? Value {
			get => _value;
			set {
				// Ignore if reference type is the same
				if (((_value is null) && (value is null))
					|| ((_value is object oldObject) && (value is object newObject) && (oldObject == newObject)))
					return;

				_value = value;
				OnValueChanged();
			}
		}

	}

}
