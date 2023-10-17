using Avalonia;
using Avalonia.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Stores example code for a sample.
	/// </summary>
	public class CodeExample : AvaloniaObject {

		private static readonly Regex SubstitutionPattern = new(@"\$\(([^\)]+)\)");

		private string? _text;
		private bool _textUsesActiproXamlNamespace;

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="Language"/> property.
		/// </summary>
		public static readonly StyledProperty<string?> LanguageProperty
			= AvaloniaProperty.Register<CodeExample, string?>(nameof(Language), defaultValue: CodeTextBlockProperties.XamlLanguageName);

		/// <summary>
		/// Defines the <see cref="Text"/> property.
		/// </summary>
		public static readonly DirectProperty<CodeExample, string?> TextProperty
			= AvaloniaProperty.RegisterDirect<CodeExample, string?>(nameof(Text), x => x.Text);

		/// <summary>
		/// Defines the <see cref="TextUsesActiproXamlNamespace"/> property.
		/// </summary>
		public static readonly DirectProperty<CodeExample, bool> TextUsesActiproXamlNamespaceProperty
			= AvaloniaProperty.RegisterDirect<CodeExample, bool>(nameof(TextUsesActiproXamlNamespace), x => x.TextUsesActiproXamlNamespace);

		/// <summary>
		/// Defines the <see cref="UnformattedText"/> property.
		/// </summary>
		public static readonly StyledProperty<string?> UnformattedTextProperty
			= AvaloniaProperty.Register<CodeExample, string?>(nameof(UnformattedTextProperty));

		#endregion

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		static CodeExample() {
			UnformattedTextProperty.Changed.AddClassHandler<CodeExample>((obj, _) => obj.UpdateTextWithSubstitutions());
			LanguageProperty.Changed.AddClassHandler<CodeExample>((obj, _) => obj.UpdateTextUsesActiproXamlNamespace());
		}

		public CodeExample() {
			Substitutions.CollectionChanged += this.OnSubstitutionsCollectionChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnSubstitutionPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e) {
			if (e.Property == CodeExampleSubstitution.ValueProperty || e.Property == CodeExampleSubstitution.IsEnabledProperty)
				UpdateTextWithSubstitutions();
		}

		private void OnSubstitutionsCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
			if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
				throw new InvalidOperationException("Resetting the collection is not allowed.");

			if (e.OldItems is not null) {
				foreach (var substitution in e.OldItems.OfType<CodeExampleSubstitution>())
					substitution.PropertyChanged -= this.OnSubstitutionPropertyChanged;
			}
			if (e.NewItems is not null) {
				foreach (var substitution in e.NewItems.OfType<CodeExampleSubstitution>())
					substitution.PropertyChanged += this.OnSubstitutionPropertyChanged;
			}
		}

		private void UpdateTextUsesActiproXamlNamespace() {
			this.TextUsesActiproXamlNamespace = this.Language == CodeTextBlockProperties.XamlLanguageName
				&& (this.Text?.Contains("actipro:") == true || this.Text?.Contains("(actipro|") == true);
		}

		private void UpdateTextWithSubstitutions() {
			var textWithSubstitutions = this.UnformattedText?.Trim() ?? string.Empty;

			textWithSubstitutions = SubstitutionPattern.Replace(textWithSubstitutions, match => {
				var keyName = match.Groups[1].Value;
				var substitution = Substitutions.FirstOrDefault(s => s?.Key == keyName);
				if (substitution is not null)
					return substitution.ValueAsString();
				throw new KeyNotFoundException(keyName);
			});

			this.Text = textWithSubstitutions;
			UpdateTextUsesActiproXamlNamespace();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The sample code's language (e.g., <c>XAML</c>, <c>XML</c>).
		/// </summary>
		public string? Language {
			get => GetValue(LanguageProperty);
			set => SetValue(LanguageProperty, value);
		}

		/// <summary>
		/// The collection of substitutions to be applied to the <see cref="Text"/> when producing <see cref="TextWithSubstitutions"/>.
		/// </summary>
		public ObservableCollection<CodeExampleSubstitution?> Substitutions { get; } = new ObservableCollection<CodeExampleSubstitution?>();

		/// <summary>
		/// The sample code text with all formatting applied.
		/// </summary>
		public string? Text {
			get => _text;
			private set => SetAndRaise(TextProperty, ref _text, value);
		}

		/// <summary>
		/// Tests if XAML-based <see cref="Text"/> uses the Actipro namespace (e.g., <c>actipro:ClassName</c>).
		/// </summary>
		public bool TextUsesActiproXamlNamespace {
			get => _textUsesActiproXamlNamespace;
			private set => SetAndRaise(TextUsesActiproXamlNamespaceProperty, ref _textUsesActiproXamlNamespace, value);
		}

		/// <summary>
		/// The unformatted sample code text.
		/// </summary>
		[Content]
		public string? UnformattedText {
			get => GetValue(UnformattedTextProperty);
			set => SetValue(UnformattedTextProperty, value);
		}

	}

}
