using ActiproSoftware.Logging;
using ActiproSoftware.UI.Avalonia.Media;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Defines the "CodeLanguage" property that can be attached to a SelectableTextBlock to format the text
	/// with syntax highlighting.
	/// </summary>
	public static class CodeTextBlockProperties {

		private static readonly Logger? _logger = LoggerFactory.DefaultInstance.CreateLogger(typeof(CodeTextBlockProperties));
		private static readonly string[] _supportedLanguages = new[] { CSharpLanguageName, XamlLanguageName, XmlLanguageName };

		/// <summary>
		/// The C# language name.
		/// </summary>
		internal const string CSharpLanguageName = "C#";

		/// <summary>
		/// The XAML language name.
		/// </summary>
		internal const string XamlLanguageName = "XAML";

		/// <summary>
		/// The XML language name.
		/// </summary>
		internal const string XmlLanguageName = "XML";

		#region Property Definitions

		/// <summary>
		/// Defines the <c>CodeLanguage</c> attached property.
		/// </summary>
		public static readonly AttachedProperty<string?> CodeLanguageProperty
			= AvaloniaProperty.RegisterAttached<SelectableTextBlock, string?>("CodeLanguage", typeof(CodeTextBlockProperties));

		#endregion Property Definitions

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#region ParserBase

		private abstract class ParserBase {

			private int _position;
			private readonly string _text;
			private readonly int _textLength;

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			public ParserBase(string text) {
				if (string.IsNullOrWhiteSpace(text))
					_text = string.Empty;
				else
					_text = text;
				_textLength = _text.Length;
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// NON-PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			private char? GetCharacter(int position) {
				if (0 <= position && position < _textLength)
					return _text[position];
				return null;
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <summary>
			/// The current character.
			/// </summary>
			protected char? Character
				=> GetCharacter(Position);

			/// <summary>
			/// Gets text from the current position up to the maximum length (if available).
			/// </summary>
			/// <param name="maxLength">The maximum length of the text to return.</param>
			/// <returns>The available text from the current position up to the maximum length.</returns>
			protected string? GetText(int maxLength) {
				if (0 <= Position && Position < _textLength) {
					int endPosition = Math.Min(_textLength, Position + maxLength);
					int length = endPosition - Position;
					try {
						return _text.Substring(Position, length);
					}
					catch (ArgumentOutOfRangeException) {
						return null;
					}
				}
				return null;
			}

			/// <summary>
			/// Tests if the parser is currently positioned on a digit.
			/// </summary>
			protected bool IsAtDigit
				=> !IsAtEnd && char.IsDigit(Character.GetValueOrDefault());

			/// <summary>
			/// Tests if the parser is currently positioned after the last character.
			/// </summary>
			protected bool IsAtEnd
				=> Position >= _textLength;

			/// <summary>
			/// Tests if the parser is currently positioned on a letter.
			/// </summary>
			protected bool IsAtLetter
				=> !IsAtEnd && char.IsLetter(Character.GetValueOrDefault());

			/// <summary>
			/// Tests if the parser is currently positioned on a letter or digit.
			/// </summary>
			protected bool IsAtLetterOrDigit
				=> !IsAtEnd && char.IsLetterOrDigit(Character.GetValueOrDefault());

			/// <summary>
			/// Parses the text into inlines.
			/// </summary>
			/// <returns>An enumerable of each inline.</returns>
			public abstract IEnumerable<Inline> Parse();

			/// <summary>
			/// The current position of the parser.
			/// </summary>
			protected int Position {
				get => _position;
				set => _position = Math.Min(_textLength, Math.Max(0, value));
			}

			/// <summary>
			/// Reads a character and advanced the current position of the parser.
			/// </summary>
			/// <returns>The character that was read; or <c>null</c> if the parser was at the end.</returns>
			protected char? ReadCharacter() {
				if (IsAtEnd)
					return null;
				var c = Character;
				Position++;
				return c;
			}

			/// <summary>
			/// Reads all characters through the end of the line (including line terminators) and advances
			/// the current position of the parser after the end of the line.
			/// </summary>
			/// <returns>The text that was read; or <c>null</c> if the parser was at the end.</returns>
			protected string? ReadLine() {
				if (IsAtEnd)
					return null;

				var startPosition = Position;
				int endPosition = -1;
				do {
					switch (Character) {
						case '\n':
							endPosition = Position;
							break;
						case '\r':
							endPosition = GetText(2) == "\r\n"
								? Position + 1
								: Position;
							break;
						default:
							Position++;
							break;
					}
				} while (!IsAtEnd && endPosition == -1);
				if (IsAtEnd && endPosition == -1)
					endPosition = Position;
				if (endPosition >= startPosition) {
					// Read through the end position
					var length = endPosition - startPosition + 1;
					Position = startPosition;
					var lineText = GetText(length);
					Position = endPosition + 1;
					return lineText;
				}
				else {
					Position = startPosition;
					return null;
				}
			}

		}

		#endregion

		#region CSharpInlineParser

		private  class CSharpInlineParser : ParserBase {

			private readonly SolidColorBrush _commentBrush;
			private readonly SolidColorBrush _controlKeywordBrush;
			private readonly SolidColorBrush _keywordBrush;
			private readonly SolidColorBrush _numberBrush;
			private readonly SolidColorBrush _stringBrush;

			private static readonly string[] _controlKeywords = new[] {
				"case",
				"break",
				"default",
				"continue",
				"do",
				"else",
				"for",
				"foreach",
				"if",
				"return",
				"switch",
				"throw",
				"when",
				"while",
				"yield",
			};

			private static readonly string[] _keywords = new[] {
				"abstract",
				"as",
				"async",
				"await",
				"base",
				"bool",
				"byte",
				"char",
				"class",
				"const",
				"decimal",
				"delegate",
				"double",
				"dynamic",
				"enum",
				"event",
				"extern",
				"false",
				"float",
				"get",
				"global",
				"int",
				"interface",
				"internal",
				"is",
				"long",
				"nameof",
				"new",
				"not",
				"null",
				"object",
				"override",
				"partial",
				"public",
				"private",
				"protected",
				"readonly",
				"record",
				"ref",
				"required",
				"sealed",
				"set",
				"short",
				"static",
				"string",
				"struct",
				"this",
				"true",
				"typeof",
				"using",
				"var",
				"virtual",
				"void",
			};

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			public CSharpInlineParser(string text, ThemeVariant themeVariant)
				: base(text) {
				
				if (themeVariant == ThemeVariant.Dark) {
					// Dark brushes
					_keywordBrush = new(UIColor.Parse("#92caf4")); // Light Blue
					_controlKeywordBrush = new(UIColor.Parse("#d8a0df")); // Lavender
					_commentBrush = new(UIColor.Parse("#57a64a")); // Green
					_numberBrush = new(UIColor.Parse("#b5cea8")); // Light Green
					_stringBrush = new(UIColor.Parse("#d69d85")); // Tan
				}
				else {
					// Light brushes
					_commentBrush = new(UIColor.Parse("Green"));
					_keywordBrush = new(UIColor.Parse("Blue"));
					_controlKeywordBrush = new(UIColor.Parse("#8f08c4")); // Purple
					_numberBrush = new(UIColor.Parse("#74531f")); // Brown
					_stringBrush = new(UIColor.Parse("#a31515")); // Dark Red
				}
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <inheritdoc/>
			public override IEnumerable<Inline> Parse() {

				var inlines = new List<Inline>();
				var tokenText = new StringBuilder();
				Brush? tokenBrush = null;

				while (!IsAtEnd) {
					var c = Character;
					switch (c) {
						case '/' when GetText(2) == "//":
							// Comment
							FinalizeToken();

							tokenBrush = _commentBrush;
							tokenText.Append(ReadLine());
							FinalizeToken();
							continue;

						case '"':
							// String
							FinalizeToken();

							tokenBrush = _stringBrush;
							tokenText.Append(ReadCharacter());

							while (!IsAtEnd && Character != '"') {
								tokenText.Append(ReadCharacter());
								if (Character == '"' && GetText(2) == "\\\"")
									tokenText.Append(ReadCharacter());
							}

							if (Character == '"')
								tokenText.Append(ReadCharacter());

							FinalizeToken();
							continue;

						case ' ':
						case '\t':
							// White space
							FinalizeToken();

							tokenText.Append(ReadCharacter());
							FinalizeToken();
							continue;

						case '\n':
						case '\r' when GetText(2) == "\r\n":
							// Line terminator
							FinalizeToken();

							if (c == '\r')
								tokenText.Append(ReadCharacter());
							tokenText.Append(ReadCharacter());
							continue;

						default:
							if (c == '_' || IsAtLetter) {
								// Word
								FinalizeToken();

								tokenText.Append(ReadCharacter());
								while (Character == '_' || IsAtLetterOrDigit)
									tokenText.Append(ReadCharacter());
								if (_keywords.Contains(tokenText.ToString()))
									tokenBrush = _keywordBrush;
								else if (_controlKeywords.Contains(tokenText.ToString()))
									tokenBrush = _controlKeywordBrush;
								FinalizeToken();
								continue;
							}
							if (IsAtDigit) {
								// Number
								FinalizeToken();

								tokenBrush = _numberBrush;
								while (IsAtDigit)
									tokenText.Append(ReadCharacter());
								FinalizeToken();
								continue;
							}
							break;
					}

					// Default text
					tokenText.Append(ReadCharacter());
				};

				FinalizeToken();

				return inlines;

				void FinalizeToken() {
					if (tokenText?.Length > 0) {
						var run = new Run(tokenText.ToString());
						if (tokenBrush is not null)
							run.Foreground = tokenBrush;
						tokenText.Clear();
						tokenBrush = null;
						inlines.Add(run);
					}
				}
			}
		}

		#endregion

		#region XmlInlineParser

		private class XmlInlineParser : ParserBase {

			private readonly SolidColorBrush _attributeNameBrush;
			private readonly SolidColorBrush _commentBrush;
			private readonly SolidColorBrush _tagDelimiterBrush;
			private readonly SolidColorBrush _tagNameBrush;
			private readonly SolidColorBrush _valueBrush;

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// OBJECT
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			public XmlInlineParser(string text, ThemeVariant themeVariant)
				: base(text) {

				if (themeVariant == ThemeVariant.Dark) {
					// Dark brushes
					_attributeNameBrush = new(UIColor.Parse("#92caf4")); // Light Blue
					_commentBrush = new(UIColor.Parse("#57a64a")); // Green
					_tagNameBrush = new(UIColor.Parse("#569cd6")); // Blue
					_valueBrush = new(UIColor.Parse("#d69d85")); // Tan
				}
				else {
					// Light brushes
					_attributeNameBrush = new(UIColor.Parse("Red"));
					_commentBrush = new(UIColor.Parse("Green"));
					_tagNameBrush = new(UIColor.Parse("Maroon"));
					_valueBrush = new(UIColor.Parse("Blue"));
				}
				_tagDelimiterBrush = new(UIColor.Parse("Gray"));
			}

			/////////////////////////////////////////////////////////////////////////////////////////////////////
			// PUBLIC PROCEDURES
			/////////////////////////////////////////////////////////////////////////////////////////////////////

			/// <inheritdoc/>
			public override IEnumerable<Inline> Parse() {

				var inlines = new List<Inline>();
				var tokenText = new StringBuilder();
				Brush? tokenBrush = null;
				bool isWithinAttributedTag = false;

				while (!IsAtEnd) {
					switch (Character) {
						case '<' when GetText(9) == "<![CDATA[":
							// CDATA Section
							FinalizeToken();
							tokenBrush = _valueBrush;
							tokenText.Append("<![CDATA[");
							Position += 9;
							while (!IsAtEnd) {
								if (Character == ']' && GetText(3) == "]]>") {
									tokenText.Append("]]>");
									Position += 3;
									FinalizeToken();
									break;
								}
								else {
									tokenText.Append(ReadCharacter());
								}
							}
							continue;

						case '<' when GetText(4) == "<!--":
							// Comment
							FinalizeToken();
							tokenBrush = _commentBrush;
							tokenText.Append("<!--");
							Position += 4;
							while (!IsAtEnd) {
								if (Character == '-' && GetText(3) == "-->") {
									tokenText.Append("-->");
									Position += 3;
									FinalizeToken();
									break;
								}
								else {
									tokenText.Append(ReadCharacter());
								}
							}
							continue;

						case '<':
							// Start Tag or Directive
							FinalizeToken();
							tokenBrush = _tagDelimiterBrush;
							tokenText.Append(ReadCharacter());
							switch (Character) {
								case '/':
									// Closing tag
									isWithinAttributedTag = false;
									tokenText.Append(ReadCharacter());
									break;
								case '?':
									// Directive
									isWithinAttributedTag = true;
									tokenText.Append(ReadCharacter());
									break;
								default:
									isWithinAttributedTag = true;
									break;
							}
							FinalizeToken();

							// Read tag name
							tokenBrush = _tagNameBrush;
							while (IsAtLetterOrDigit || Character == ':' || Character == '.')
								tokenText.Append(ReadCharacter());
							FinalizeToken();
							continue;

						case '>':
						case '/' when GetText(2) == "/>":
						case '?' when GetText(2) == "?>":
							// End Tag or Directive
							FinalizeToken();
							tokenBrush = _tagDelimiterBrush;
							if (Character == '>')
								tokenText.Append(ReadCharacter());
							else {
								tokenText.Append(ReadCharacter());
								tokenText.Append(ReadCharacter());
							}
							FinalizeToken();
							isWithinAttributedTag = false;
							continue;

						case '"':
						case '\'':
							// String
							FinalizeToken();

							var endCharacter = Character;

							tokenBrush = _valueBrush;
							tokenText.Append(ReadCharacter());

							while (!IsAtEnd && Character != endCharacter)
								tokenText.Append(ReadCharacter());

							if (Character == endCharacter)
								tokenText.Append(ReadCharacter());

							FinalizeToken();
							continue;

						case '=':
							// Operator
							FinalizeToken();
							tokenText.Append(ReadCharacter());
							FinalizeToken();
							continue;

						default:
							if (isWithinAttributedTag && IsAtLetterOrDigit) {
								// Attribute Name
								FinalizeToken();
								tokenBrush = _attributeNameBrush;
								while (IsAtLetterOrDigit || Character == ':')
									tokenText.Append(ReadCharacter());
								FinalizeToken();
								continue;
							}
							break;
					}

					// Default text
					tokenText.Append(ReadCharacter());
				};

				FinalizeToken();

				return inlines;

				void FinalizeToken() {
					if (tokenText?.Length > 0) {
						var run = new Run(tokenText.ToString());
						if (tokenBrush is not null)
							run.Foreground = tokenBrush;
						tokenText.Clear();
						tokenBrush = null;
						inlines.Add(run);
					}
				}
			}
		}

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		static CodeTextBlockProperties() {
			CodeLanguageProperty.Changed.AddClassHandler<SelectableTextBlock>(OnCodeLanguageChanged);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PRIVATE PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private static void OnCodeLanguageChanged(AvaloniaObject obj, AvaloniaPropertyChangedEventArgs e) {
			if (obj is SelectableTextBlock textBlock) {
				var oldLanguage = NormalizeCodeLanguage(e.OldValue as string);
				var newLanguage = NormalizeCodeLanguage(e.NewValue as string);
				if (oldLanguage == newLanguage)
					return;

				if ((oldLanguage is null) && (newLanguage is not null)) {
					textBlock.PropertyChanged += OnTextBlockPropertyChanged;
					textBlock.ActualThemeVariantChanged += OnTextBlockActualThemeVariantChanged;
				}
				else if ((oldLanguage is not null) && (newLanguage is null)) {
					textBlock.PropertyChanged -= OnTextBlockPropertyChanged;
					textBlock.ActualThemeVariantChanged -= OnTextBlockActualThemeVariantChanged;
				}

				InvalidateInlines(textBlock, newLanguage);
			}
		}

		private static void OnTextBlockActualThemeVariantChanged(object? sender, EventArgs e) {
			if (sender is SelectableTextBlock textBlock)
				InvalidateInlines(textBlock);
		}

		private static void OnTextBlockPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e) {
			if ((sender is SelectableTextBlock textBlock) && (e.Property == TextBlock.TextProperty))
				InvalidateInlines(textBlock);
		}

		private static void InvalidateInlines(SelectableTextBlock textBlock)
			=> InvalidateInlines(textBlock, NormalizeCodeLanguage(GetCodeLanguage(textBlock)));

		private static void InvalidateInlines(SelectableTextBlock textBlock, string? language) {
			Dispatcher.UIThread.InvokeAsync(() => {
				try {
					if (textBlock.Inlines is null)
						textBlock.Inlines = new InlineCollection();
					else
						textBlock.Inlines.Clear();

					var text = textBlock.Text;
					if (!string.IsNullOrEmpty(text)) {
						var themeVariant = textBlock.ActualThemeVariant;
						switch (language) {
							case CSharpLanguageName:
								textBlock.Inlines.AddRange(new CSharpInlineParser(text, themeVariant).Parse());
								break;
							case XamlLanguageName:
							case XmlLanguageName:
								textBlock.Inlines.AddRange(new XmlInlineParser(text, themeVariant).Parse());
								break;
							default:
								textBlock.Inlines.Add(new Run(text));
								break;

						}
					}
				}
				catch (Exception ex) {
					// Quit gracefully if the text could not be parsed
					_logger?.LogError(ex, $"Error building inlines for language '{language}'.");
				}
			});
		}
		
		private static string? NormalizeCodeLanguage(string? languageName) {
			var formalLanguageName = languageName?.ToUpperInvariant();

			return _supportedLanguages.Contains(formalLanguageName) ? formalLanguageName : null;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets the code language assigned to the given object.
		/// </summary>
		/// <param name="obj">The object to examine.</param>
		/// <returns>The defined code language, or <c>null</c> if the value is undefined.</returns>
		/// <seealso cref="SetCodeLanguage(AvaloniaObject, string?)"/>
		public static string? GetCodeLanguage(AvaloniaObject obj)
			=> obj?.GetValue(CodeLanguageProperty);

		/// <summary>
		/// Sets the code language on the given object.
		/// </summary>
		/// <param name="obj">The target object.</param>
		/// <param name="value">The value to assign.</param>
		/// <seealso cref="GetCodeLanguage(AvaloniaObject)"/>
		public static void SetCodeLanguage(AvaloniaObject obj, string? value)
			=> obj?.SetValue(CodeLanguageProperty, value);
		
	}

}
