using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.VisualTree;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Displays a sample with a header and optional footer and code sections.
	/// </summary>
	[TemplatePart(CodeExamplePanelPartName, typeof(Panel))]
	[PseudoClasses(pcOptions)]
	[PseudoClasses(pcWide)]
	public class ControlExample : HeaderedContentControl {
	
		private bool _hasCode;
		private bool _isWideMeasure;

		private const double DefaultWideThreshold = 800;

		#region Property Definitions

		/// <summary>
		/// Defines the <see cref="CodeExamples"/> property.
		/// </summary>
		public static readonly DirectProperty<ControlExample, ObservableCollection<CodeExample?>> CodeExamplesProperty
			= AvaloniaProperty.RegisterDirect<ControlExample, ObservableCollection<CodeExample?>>(nameof(CodeExamples), x => x.CodeExamples);

		/// <summary>
		/// Defines the <see cref="Footer"/> property.
		/// </summary>
		public static readonly StyledProperty<object?> FooterProperty
			= AvaloniaProperty.Register<ControlExample, object?>(nameof(Footer));

		/// <summary>
		/// Defines the <see cref="FooterTemplate"/> property.
		/// </summary>
		public static readonly StyledProperty<IDataTemplate?> FooterTemplateProperty
			= AvaloniaProperty.Register<ControlExample, IDataTemplate?>(nameof(FooterTemplate));
		
		/// <summary>
		/// Defines the <see cref="HasCode"/> property.
		/// </summary>
		public static readonly DirectProperty<ControlExample, bool> HasCodeProperty
			= AvaloniaProperty.RegisterDirect<ControlExample, bool>(nameof(HasCode), getter: b => b.HasCode);
		
		/// <summary>
		/// Defines the <see cref="IsCodeExpanded"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> IsCodeExpandedProperty
			= AvaloniaProperty.Register<ControlExample, bool>(nameof(IsCodeExpanded));

		/// <summary>
		/// Defines the <see cref="IsCodeExampleKindSelectorVisible"/> property.
		/// </summary>
		public static readonly StyledProperty<bool?> IsCodeExampleKindSelectorVisibleProperty
			= AvaloniaProperty.Register<ControlExample, bool?>(nameof(IsCodeExampleKindSelectorVisible), coerce: CoerceIsCodeExampleKindSelectorVisibleProperty);

		/// <summary>
		/// Defines the <see cref="MvvmContent"/> property.
		/// </summary>
		public static readonly StyledProperty<object?> MvvmContentProperty
			= AvaloniaProperty.Register<ControlExample, object?>(nameof(MvvmContent));

		/// <summary>
		/// Defines the <see cref="Options"/> property.
		/// </summary>
		public static readonly StyledProperty<object?> OptionsProperty
			= AvaloniaProperty.Register<ControlExample, object?>(nameof(Options));

		/// <summary>
		/// Defines the <see cref="OptionsTemplate"/> property.
		/// </summary>
		public static readonly StyledProperty<IDataTemplate?> OptionsTemplateProperty
			= AvaloniaProperty.Register<ControlExample, IDataTemplate?>(nameof(OptionsTemplate));

		/// <summary>
		/// Defines the <see cref="SelectedCodeExampleKind"/> property.
		/// </summary>
		public static readonly StyledProperty<CodeExampleKind> SelectedCodeExampleKindProperty
			= AvaloniaProperty.Register<ControlExample, CodeExampleKind>(nameof(SelectedCodeExampleKind), defaultValue: CodeExampleKind.Unspecified, coerce: CoerceSelectedCodeExampleKindProperty);

		/// <summary>
		/// Defines the <see cref="WideThreshold"/> property.
		/// </summary>
		public static readonly StyledProperty<double> WideThresholdProperty
			= AvaloniaProperty.Register<ControlExample, double>(nameof(WideThreshold), defaultValue: DefaultWideThreshold);

		/// <summary>
		/// Defines the <see cref="XamlContent"/> property.
		/// </summary>
		public static readonly StyledProperty<object?> XamlContentProperty
			= AvaloniaProperty.Register<ControlExample, object?>(nameof(XamlContent));

		#endregion

		// Part names
		private const string CodeExamplePanelPartName = "PART_CodeExamplePanel";

		// Pseudo classes
		private const string pcOptions = ":options";
		private const string pcWide = ":wide";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		static ControlExample() {
			ContentProperty.OverrideMetadata<ControlExample>(new StyledPropertyMetadata<object?>(coerce: CoerceContentProperty));
			IsCodeExpandedProperty.Changed.AddClassHandler<ControlExample>((x, e) => x.OnIsCodeExpandedPropertyValueChanged(e));
			MvvmContentProperty.Changed.AddClassHandler<ControlExample>((x, _) => x.CoerceValue(ContentProperty));
			OptionsProperty.Changed.AddClassHandler<ControlExample>((x, _) => x.UpdatePseudoClasses());
			SelectedCodeExampleKindProperty.Changed.AddClassHandler<ControlExample>((x, _) => x.OnSelectedCodeExampleKindPropertyValueChanged());
			XamlContentProperty.Changed.AddClassHandler<ControlExample>((x, _) => x.CoerceValue(ContentProperty));
		}

		public ControlExample() {
			this.CodeExamples.CollectionChanged += (_, _) => OnCodeExamplesCollectionChanged();

			CoerceAllProperties();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void BringCodeExamplesIntoView() {
			var codeExamplePanel = CodeExamplePanel;
			if (codeExamplePanel is not null) {
				var itemsControl = codeExamplePanel.FindDescendantOfType<ItemsControl>();
				if (itemsControl is not null) {
					var scrollViewer = this.FindAncestorOfType<ScrollViewer>();
					if (scrollViewer is not null) {
						if (codeExamplePanel.TransformToVisual(scrollViewer) is Matrix transform) {
							var viewport = scrollViewer.Viewport;

							// Ensure the ItemsControl.DesiredSize is initialized
							if (!itemsControl.IsMeasureValid)
								itemsControl.Measure(new Size(codeExamplePanel.Bounds.Width, double.PositiveInfinity));

							// Get the target bounds but don't exceed a reasonable maximum for the amount to scroll down
							const double MaxCodeExampleReveal = 200.0;
							var targetBounds = new Rect(transform.Transform(new Point()), transform.Transform(new Point(0.0, Math.Min(MaxCodeExampleReveal, itemsControl.DesiredSize.Height))));

							// Determine the amount to scroll to try and bring the target bounds fully into view and scroll as needed
							var deltaY = Math.Max(0.0, Math.Min(targetBounds.Top - scrollViewer.Offset.Y, targetBounds.Bottom - viewport.Height));
							if (deltaY > 0.0)
								scrollViewer.Offset = new Vector(scrollViewer.Offset.X, scrollViewer.Offset.Y + deltaY);
						}
					}
				}
			}
		}

		private Panel? CodeExamplePanel { get; set; }

		private void CoerceAllProperties() {
			CoerceValue(ContentProperty);
			CoerceValue(IsCodeExampleKindSelectorVisibleProperty);
			CoerceValue(SelectedCodeExampleKindProperty);
		}

		private static object? CoerceContentProperty(AvaloniaObject obj, object? value) {
			if ((obj is ControlExample control) && (value is null)) {
				return control.SelectedCodeExampleKind switch {
					CodeExampleKind.Mvvm => control.MvvmContent,
					CodeExampleKind.Xaml => control.XamlContent,
					_ => value
				};
			}
			return value;
		}

		private static bool? CoerceIsCodeExampleKindSelectorVisibleProperty(AvaloniaObject obj, bool? value) {
			if ((obj is ControlExample control) && (value is null)) {
				// Visible if MVVM/XAML-specific content is defined or any MVVM or XAML code examples are available
				return control.MvvmContent is not null
					|| control.XamlContent is not null
					|| control.CodeExamples.Any(x => (x is not null) && (x.Kind != CodeExampleKind.Unspecified));
			}
			return true;
		}

		private static CodeExampleKind CoerceSelectedCodeExampleKindProperty(AvaloniaObject obj, CodeExampleKind kind) {
			if ((obj is ControlExample control) && (control.IsCodeExampleKindSelectorVisible == true) && (kind == CodeExampleKind.Unspecified)) {
				// "Unspecified" is not a valid selection when the selector is visible, so coerce to a default value
				return CodeExampleKind.Xaml;
			}
			return kind;
		}

		private void OnCodeExamplesCollectionChanged() {
			UpdateCodeExampleVisibility();
			UpdateHasCode();
			CoerceAllProperties();
		}

		private void OnIsCodeExpandedPropertyValueChanged(AvaloniaPropertyChangedEventArgs e) {
			if (true.Equals(e.NewValue) && IsKeyboardFocusWithin)
				BringCodeExamplesIntoView();
		}

		private void OnSelectedCodeExampleKindPropertyValueChanged() {
			UpdateCodeExampleVisibility();
			CoerceValue(ContentProperty);
		}

		private void UpdateCodeExampleVisibility() {
			foreach (var codeExample in CodeExamples) {
				if ((codeExample is not null) && (codeExample.Kind != CodeExampleKind.Unspecified)) {
					codeExample.SetValue(CodeExample.IsVisibleProperty, (codeExample.Kind == SelectedCodeExampleKind), BindingPriority.Style);
				}
			}
		}

		private void UpdateHasCode()
			=> HasCode = this.CodeExamples.Any(x => x?.IsVisible == true);

		private void UpdatePseudoClasses() {
			PseudoClasses.Set(pcOptions, Options is not null);
			PseudoClasses.Set(pcWide, _isWideMeasure);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The collection of code examples associated with the sample.
		/// </summary>
		public ObservableCollection<CodeExample?> CodeExamples { get; } = new ObservableCollection<CodeExample?>();

		/// <summary>
		/// The footer content.
		/// </summary>
		public object? Footer {
			get => GetValue(FooterProperty);
			set => SetValue(FooterProperty, value);
		}

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for the <see cref="Footer"/> content.
		/// </summary>
		public IDataTemplate? FooterTemplate {
			get => GetValue(FooterTemplateProperty);
			set => SetValue(FooterTemplateProperty, value);
		}
		
		/// <summary>
		/// Whether there is any code available to display.
		/// </summary>
		public bool HasCode {
			get => _hasCode;
			private set => SetAndRaise(HasCodeProperty, ref _hasCode, value);
		}
		
		/// <summary>
		/// Whether code is expanded.
		/// </summary>
		public bool IsCodeExpanded {
			get => GetValue(IsCodeExpandedProperty);
			set => SetValue(IsCodeExpandedProperty, value);
		}

		/// <summary>
		/// Indiates if teh code example kind selector control is visible.
		/// </summary>
		/// <value>
		/// <c>true</c> to force the selector to be visible,
		/// <c>false</c> to force the selector to be hidden, or
		/// <c>null</c> to show the selector only when a <see cref="CodeExample"/> is defined that uses <see cref="CodeExampleKind.Mvvm"/> or <see cref="CodeExampleKind.Xaml"/>.
		/// </value>
		public bool? IsCodeExampleKindSelectorVisible {
			get => GetValue(IsCodeExampleKindSelectorVisibleProperty);
			set => SetValue(IsCodeExampleKindSelectorVisibleProperty, value);
		}

		/// <inheritdoc/>
		protected override Size MeasureOverride(Size availableSize) {
			_isWideMeasure = (availableSize.Width >= WideThreshold);
			UpdatePseudoClasses();

			return base.MeasureOverride(availableSize);
		}
		
		/// <summary>
		/// The content to be displayed when <see cref="SelectedCodeExampleKind"/> is <see cref="CodeExampleKind.Mvvm"/>.
		/// </summary>
		public object? MvvmContent {
			get => GetValue(MvvmContentProperty);
			set => SetValue(MvvmContentProperty, value);
		}

		/// <inheritdoc/>
		protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
			base.OnApplyTemplate(e);

			CoerceAllProperties();

			CodeExamplePanel = e.NameScope.Get<Panel>(CodeExamplePanelPartName);
		}

		/// <summary>
		/// The options content.
		/// </summary>
		public object? Options {
			get => GetValue(OptionsProperty);
			set => SetValue(OptionsProperty, value);
		}
		
		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for the <see cref="Options"/> content.
		/// </summary>
		public IDataTemplate? OptionsTemplate {
			get => GetValue(OptionsTemplateProperty);
			set => SetValue(OptionsTemplateProperty, value);
		}

		/// <summary>
		/// The selected code example kind (e.g., MVVM or XAML).
		/// </summary>
		public CodeExampleKind SelectedCodeExampleKind {
			get => GetValue(SelectedCodeExampleKindProperty);
			set => SetValue(SelectedCodeExampleKindProperty, value);
		}

		/// <summary>
		/// The minimum width of the control to use wide layout.
		/// </summary>
		/// <remarks>
		/// The default value is 800.
		/// </remarks>
		public double WideThreshold {
			get => GetValue(WideThresholdProperty);
			set => SetValue(WideThresholdProperty, value);
		}

		/// <summary>
		/// The content to be displayed when <see cref="SelectedCodeExampleKind"/> is <see cref="CodeExampleKind.Xaml"/>.
		/// </summary>
		public object? XamlContent {
			get => GetValue(XamlContentProperty);
			set => SetValue(XamlContentProperty, value);
		}

	}

}
