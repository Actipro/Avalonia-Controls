using System.Collections;
using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a combobox control within a bar control.
	/// </summary>
	public class BarComboBoxViewModel : BarGalleryViewModel {

		private string? _description;
		private bool _isEditable;
		private bool _isPreviewEnabledWhenPopupClosed;
		private bool _isReadOnly;
		private bool _isStarSizingAllowed;
		private bool _isTextCompletionEnabled = true;
		private bool _isTextSearchCaseSensitive;
		private bool _isTextSearchEnabled = true;
		private bool _isUnmatchedTextAllowed = true;
		private double _maxPopupHeight = double.PositiveInfinity;
		private string? _placeholderText;
		private double _requestedWidth = 110.0;
		private string _text = string.Empty;
		private ICommand? _unmatchedTextCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <inheritdoc cref="BarButtonViewModel()"/>
		public BarComboBoxViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public BarComboBoxViewModel(string? key)
			: this(key, label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key and items.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
		public BarComboBoxViewModel(string? key, IEnumerable? items)
			: this(key, label: null, items) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public BarComboBoxViewModel(string? key, string? label)
			: this(key, label, keyTipText: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, and items.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
		public BarComboBoxViewModel(string? key, string? label, IEnumerable? items)
			: this(key, label, keyTipText: null, items) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public BarComboBoxViewModel(string? key, string? label, string? keyTipText)
			: this(key, label, keyTipText, items: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, key tip text, and items.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
		/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
		public BarComboBoxViewModel(string? key, string? label, string? keyTipText, IEnumerable? items)
			: this(key, label, keyTipText, command: null, items) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public BarComboBoxViewModel(string? key, ICommand? command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public BarComboBoxViewModel(string? key, string? label, ICommand? command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public BarComboBoxViewModel(string? key, string? label, string? keyTipText, ICommand? command)
			: this(key, label, keyTipText, command, items: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, command, and items.  The label and key tip text are auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="command">The command to attach to the control.</param>
		/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
		public BarComboBoxViewModel(string? key, ICommand? command, IEnumerable? items)
			: this(key, label: null, keyTipText: null, command, items) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, command, and items.  The key tip text is auto-generated.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
		public BarComboBoxViewModel(string? key, string? label, ICommand? command, IEnumerable? items)
			: this(key, label, keyTipText: null, command, items) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified key, label, key tip text, command, and items.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		/// <param name="label">The text label to display, which is auto-generated from the <paramref name="command"/> or <paramref name="key"/> if <c>null</c>.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="command"/> or <paramref name="label"/> if <c>null</c>.</param>
		/// <param name="command">The command to attach to the control.</param>
		/// <param name="items">The collection of gallery items, where the items are typically of type <see cref="IBarGalleryItemViewModel"/>.</param>
		public BarComboBoxViewModel(string? key, string? label, string? keyTipText, ICommand? command, IEnumerable? items)
			: base(key, label, keyTipText, command, items) {

			// Comboboxes default to single column
			this.MaxMenuColumnCount = 1;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.Description"/>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}
		
		/// <summary>
		/// Indictes whether the combobox is editable.
		/// </summary>
		/// <value>
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsEditable {
			get => _isEditable;
			set => SetProperty(ref _isEditable, value);
		}

		/// <summary>
		/// Indicates whether an editable combobox will preview a gallery item if the item is matched by typed text while the popup is closed.
		/// </summary>
		/// <value>
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsPreviewEnabledWhenPopupClosed {
			get => _isPreviewEnabledWhenPopupClosed;
			set => SetProperty(ref _isPreviewEnabledWhenPopupClosed, value);
		}
		
		/// <summary>
		/// Indicates whether the combobox is read-only.
		/// </summary>
		/// <value>
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsReadOnly {
			get => _isReadOnly;
			set => SetProperty(ref _isReadOnly, value);
		}

		/// <summary>
		/// Indicates whether the control can star-size and fill available space when appropriate.
		/// </summary>
		/// <value>
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsStarSizingAllowed {
			get => _isStarSizingAllowed;
			set => SetProperty(ref _isStarSizingAllowed, value);
		}
		
		/// <summary>
		/// Indicates whether the control will attempt to complete typed text with a matching item.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsTextCompletionEnabled {
			get => _isTextCompletionEnabled;
			set => SetProperty(ref _isTextCompletionEnabled, value);
		}

		/// <summary>
		/// Indicates whether case is a condition when searching for items.
		/// </summary>
		/// <value>
		/// <c>true</c> if text searches are case-sensitive; otherwise <c>false</c>.
		/// The default value is <c>false</c>.
		/// </value>
		/// <seealso cref="IsTextSearchEnabled"/>
		/// <seealso cref="UnmatchedTextCommand"/>
		public bool IsTextSearchCaseSensitive {
			get => _isTextSearchCaseSensitive;
			set => SetProperty(ref _isTextSearchCaseSensitive, value);
		}
		
		/// <summary>
		/// Indicates whether known items are matched when text is entered.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		/// <seealso cref="IsTextSearchCaseSensitive"/>
		/// <seealso cref="UnmatchedTextCommand"/>
		public bool IsTextSearchEnabled {
			get => _isTextSearchEnabled;
			set => SetProperty(ref _isTextSearchEnabled, value);
		}

		/// <summary>
		/// Indicates whether committed <see cref="Text"/> that is unable to be matched to a gallery item will raise the <see cref="UnmatchedTextCommand"/> and possibly be allowed.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		/// <seealso cref="UnmatchedTextCommand"/>
		public bool IsUnmatchedTextAllowed {
			get => _isUnmatchedTextAllowed;
			set => SetProperty(ref _isUnmatchedTextAllowed, value);
		}

		/// <summary>
		/// The maximum popup height.
		/// </summary>
		/// <value>
		/// The default value is <c>Double.PositiveInfinity</c>.
		/// </value>
		public double MaxPopupHeight {
			get => _maxPopupHeight;
			set => SetProperty(ref _maxPopupHeight, value);
		}
		
		/// <summary>
		/// The placeholder text to display when the control is empty.
		/// </summary>
		public string? PlaceholderText {
			get => _placeholderText;
			set => SetProperty(ref _placeholderText, value);
		}
		
		/// <summary>
		/// The requested width of the control.
		/// </summary>
		/// <value>
		/// The default value is <c>110</c>.
		/// </value>
		public double RequestedWidth {
			get => _requestedWidth;
			set => SetProperty(ref _requestedWidth, value);
		}
		
		/// <summary>
		/// Selects an item in the gallery whose text representation matches the specified text,
		/// </summary>
		/// <typeparam name="T">The type of <see cref="IBarGalleryItemViewModel"/> to examine.</typeparam>
		/// <param name="getItemTextFunc">A function that examines an item and returns its string value for comparison to <paramref name="text"/>.</param>
		/// <param name="text">The text for which to search and that will be set to <see cref="Text"/>.</param>
		/// <returns>
		/// The <see cref="IBarGalleryItemViewModel"/> that was selected, if any.
		/// </returns>
		public virtual T? SelectItemByTextMatch<T>(Func<T, string> getItemTextFunc, string text) where T : IBarGalleryItemViewModel {
			if (getItemTextFunc == null)
				throw new ArgumentNullException(nameof(getItemTextFunc));

			text ??= string.Empty;

			var matchedItem = base.SelectItemByValueMatch<T>(i => text.Equals(getItemTextFunc(i) ?? string.Empty));

			this.Text = text;

			return matchedItem;
		}
		
		/// <summary>
		/// Selects an item in the gallery that matches the predicate,
		/// alternatively setting the specified fallback <see cref="Text"/> if no match is made.
		/// </summary>
		/// <typeparam name="T">The type of <see cref="IBarGalleryItemViewModel"/> to examine.</typeparam>
		/// <param name="matchPredicate">A predicate that determines when an item matches criteria.</param>
		/// <param name="getMatchedItemTextFunc">A function that examines a matched item and returns the string value to set to <see cref="Text"/>.</param>
		/// <param name="fallbackText">The fallback text to set to <see cref="Text"/> when there is no match.</param>
		/// <returns>
		/// The <see cref="IBarGalleryItemViewModel"/> that was selected, if any.
		/// </returns>
		public virtual T? SelectItemByValueMatch<T>(Func<T, bool> matchPredicate, Func<T, string> getMatchedItemTextFunc, string fallbackText) where T : IBarGalleryItemViewModel {
			if (getMatchedItemTextFunc == null)
				throw new ArgumentNullException(nameof(getMatchedItemTextFunc));

			var matchedItem = base.SelectItemByValueMatch(matchPredicate);

			if (matchedItem != null)
				this.Text = getMatchedItemTextFunc(matchedItem) ?? string.Empty;
			else
				this.Text = fallbackText ?? string.Empty;

			return matchedItem;
		}
		
		/// <summary>
		/// The text to display in the control.
		/// </summary>
		public string Text {
			get => _text;
			set => SetProperty(ref _text, value);
		}
		
		/// <summary>
		/// The <see cref="ICommand"/> to execute when <see cref="Text"/> is committed that is unable to be matched to a gallery item
		/// or when <see cref="IsTextSearchEnabled"/> is <c>false</c>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This command is only used when the <see cref="IsUnmatchedTextAllowed"/> property is <c>true</c>.
		/// The unmatched text string is passed as a command parameter.
		/// </para>
		/// <para>
		/// When the command is <c>null</c> or <see cref="ICommand.CanExecute"/> returns <c>true</c>, the text will be committed; otherwise, it will not be committed.
		/// </para>
		/// </remarks>
		/// <seealso cref="IsUnmatchedTextAllowed"/>
		public ICommand? UnmatchedTextCommand {
			get => _unmatchedTextCommand;
			set => SetProperty(ref _unmatchedTextCommand, value);
		}
		
	}

}
