using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a gallery item whose value and label based on the value in an enumeration.
	/// </summary>
	/// <typeparam name="TEnum">The enumeration type of the value associated with this gallery item.</typeparam>
	public class EnumBarGalleryItemViewModel<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] TEnum> : BarGalleryItemViewModel<TEnum>
		where TEnum : Enum {

		/// <summary>
		/// The default sort order for enumeration items that do not define an order using the <see cref="DisplayAttribute.Order"/> property.
		/// </summary>
		public const int DefaultOrder = 10000;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public EnumBarGalleryItemViewModel()
			: this(value: default, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value.
		/// </summary>
		/// <param name="value">The item's value.</param>
		public EnumBarGalleryItemViewModel(TEnum? value)
			: this(value, category: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value and category.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		public EnumBarGalleryItemViewModel(TEnum? value, string? category)
			: this(value, category, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified value, category, and label.
		/// </summary>
		/// <param name="value">The item's value.</param>
		/// <param name="category">The item's category, or <c>null</c> if categorization is not supported.</param>
		/// <param name="label">The text label to display, or <c>null</c> if the label can be coerced from the current value.</param>
		public EnumBarGalleryItemViewModel(TEnum? value, string? category, string? label)
			: base(value, category, label) {

			ThrowIfNotEnumType(typeof(TEnum));
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Extracts attribute data for the given enumeration value..
		/// </summary>
		/// <param name="value">The enumeration value to examine.</param>
		/// <param name="label">Outputs the label, if any, that was resolved from the available attributes.</param>
		/// <param name="category">Outputs the category, if any, that was resolved from the available attributes.</param>
		/// <param name="description">Outputs the description, if any, that was resolved from the available attributes.</param>
		/// <param name="order">Outputs the order, if any, that was resolved from the available attributes.</param>
		private static void ExtractAttributeData(TEnum? value, out string? label, out string? category, out string? description, out int? order) {
			var field = FindField(value);
			ExtractAttributeData(field, out label, out category, out description, out order);
		}

		/// <summary>
		/// Extracts attribute data for the given <see cref="FieldInfo"/>.
		/// </summary>
		/// <param name="field">The <see cref="FieldInfo"/> to examine.</param>
		/// <param name="label">Outputs the label, if any, that was resolved from the available attributes.</param>
		/// <param name="category">Outputs the category, if any, that was resolved from the available attributes.</param>
		/// <param name="description">Outputs the description, if any, that was resolved from the available attributes.</param>
		/// <param name="order">Outputs the order, if any, that was resolved from the available attributes.</param>
		private static void ExtractAttributeData(FieldInfo? field, out string? label, out string? category, out string? description, out int? order) {
			// Initialize output arguments
			label = null;
			category = null;
			description = null;
			order = null;

			// Read attribute data
			if (field is not null) {
				var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();

				label = field.GetCustomAttribute<DescriptionAttribute>()?.Description
					?? displayAttribute?.GetName()
					?? displayAttribute?.GetShortName();

				category = displayAttribute?.GetGroupName();
				description = displayAttribute?.GetDescription();
				order = displayAttribute?.GetOrder();
			}
		}

		/// <summary>
		/// Finds the <see cref="FieldInfo"/> of an enumeration which corresponds to the given value.
		/// </summary>
		/// <param name="value">The value to examine.</param>
		/// <returns>The <see cref="FieldInfo"/> which corresponds to the value; otherwise, <c>null</c> if a match was not found.</returns>
		private static FieldInfo? FindField(TEnum? value) {
			if (value is null)
				return null;

			var enumType = typeof(TEnum);
			return GetFields(enumType).FirstOrDefault(f => {
				var fieldValue = f?.GetValue(null);
				return (fieldValue is not null) && ((TEnum)fieldValue).Equals(value);
			});
		}

		/// <summary>
		/// Gets the <see cref="FieldInfo"/> for each value defined for an enumeration.
		/// </summary>
		/// <param name="enumType">The type of the enumeration.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of <see cref="FieldInfo"/>.</returns>
		private static IEnumerable<FieldInfo?> GetFields([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] Type enumType) {
			return enumType.GetFields().Where(f => f?.IsStatic == true);
		}

		/// <summary>
		/// Throws an exception if <typeparamref name="TEnum"/> is not an enumeration since generic type constraints are not specific enough.
		/// </summary>
		/// <param name="enumType">The type to examine.</param>
		/// <exception cref="ArgumentException">Thrown if <typeparamref name="TEnum"/> is not an enumeration.</exception>
		private static void ThrowIfNotEnumType(Type enumType) {
			if (!enumType.IsEnum)
				throw new ArgumentException($"The type {nameof(TEnum)} must be an enumeration.");
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a collection of gallery item view models representing the values defined for an enumeration type.
		/// </summary>
		/// <exception cref="InvalidOperationException"/>
		/// <returns>The collection of gallery item view models that was created.</returns>
		public static IEnumerable<EnumBarGalleryItemViewModel<TEnum>> CreateCollection() {
			var enumType = typeof(TEnum);
			ThrowIfNotEnumType(enumType);

			var collection = new HashSet<EnumBarGalleryItemViewModel<TEnum>>();
			foreach (var field in GetFields(enumType)) {
				if (field is null)
					continue;

				try {
					// Ignore fields explicitly marked as non-browsable
					bool isNonBrowsable = field.GetCustomAttribute<EditorBrowsableAttribute>()?.State == EditorBrowsableState.Never;
					if (isNonBrowsable)
						continue;

					var fieldValue = field.GetValue(null);
					if (fieldValue is null)
						continue;

					var value = (TEnum)fieldValue;

					// Use attribute data to initialize the view model
					ExtractAttributeData(field, out var label, out var category, out var description, out int? order);

					// Coerce values not defined by attributes
					label ??= ConvertEnumValueToString(enumType, value, useAttributes: false);
					order ??= DefaultOrder;

					// Create the view model
					var viewModel = new EnumBarGalleryItemViewModel<TEnum>(value, category, label) {
						Description = description,
						Order = order.Value,
					};
					collection.Add(viewModel);
				}
				catch (InvalidOperationException ex) {
					// Wrap the exception with details on which field generated the error since it can be
					// difficult to debug localization issues on DisplayAttribute without knowing which
					// field generated an exception.
					throw new InvalidOperationException($"Invalid operation creating view model for enumeration field '{enumType.FullName}.{field.Name}'.  {ex.Message}", ex);
				}
			}

			return collection.OrderBy(vm => vm.Order);
		}

		/// <inheritdoc/>
		public override bool IsLabelVisible
			=> true;

		/// <summary>
		/// The sort order for this item, where lower values appear before higher values.
		/// </summary>
		public int Order { get; set; }

		/// <summary>
		/// Refreshes supported properties based the attributes, if any, defined on the enumeration field associated with this
		/// view model's value. Properties are only updated if a non-null value is obtained from the corresponding attribute.
		/// Otherwise, existing property values are unchanged.
		/// </summary>
		/// <remarks>
		/// Call this method after any changes in locale since <see cref="DisplayAttribute"/> supports localization and is used to
		/// populate several properties on this view model.
		/// </remarks>
		public void RefreshFromAttributes() {
			// Read attribute data
			ExtractAttributeData(this.Value, out var label, out var category, out var description, out int? order);

			// Update properties from non-null attribute data
			if (label is not null)
				this.Label = label;
			if (category is not null)
				this.Category = category;
			if (description is not null)
				this.Description = description;
			if (order.HasValue)
				this.Order = order.Value;
		}

	}

}
