using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.VisualTree;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Displays a sample with a header and optional footer and code sections.
	/// </summary>
	[TemplatePart(CodeExamplePanelPartName, typeof(Panel))]
	[PseudoClasses(pcWide)]
	public class ControlExample : HeaderedContentControl {
	
		private bool _hasCode;
		private bool _isWideMeasure;

		private const double WideThreshold = 800;

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
		/// Defines the <see cref="Options"/> property.
		/// </summary>
		public static readonly StyledProperty<object?> OptionsProperty
			= AvaloniaProperty.Register<ControlExample, object?>(nameof(Options));

		/// <summary>
		/// Defines the <see cref="OptionsTemplate"/> property.
		/// </summary>
		public static readonly StyledProperty<IDataTemplate?> OptionsTemplateProperty
			= AvaloniaProperty.Register<ControlExample, IDataTemplate?>(nameof(OptionsTemplate));
		
		#endregion

		// Part names
		private const string CodeExamplePanelPartName = "PART_CodeExamplePanel";

		// Pseudo classes
		private const string pcWide = ":wide";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		static ControlExample() {
			IsCodeExpandedProperty.Changed.AddClassHandler<ControlExample>((x, e) => x.OnIsCodeExpandedPropertyValueChanged(e));
		}

		public ControlExample() {
			this.CodeExamples.CollectionChanged += (_, _) => UpdateHasCode();
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

		private void OnIsCodeExpandedPropertyValueChanged(AvaloniaPropertyChangedEventArgs e) {
			if (true.Equals(e.NewValue) && IsKeyboardFocusWithin)
				BringCodeExamplesIntoView();
		}

		private void UpdateHasCode()
			=> HasCode = (this.CodeExamples.Any(x => x is not null));
		
		private void UpdatePseudoClasses()
			=> PseudoClasses.Set(pcWide, _isWideMeasure);

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

		/// <inheritdoc/>
		protected override Size MeasureOverride(Size availableSize) {
			_isWideMeasure = (availableSize.Width >= WideThreshold);
			UpdatePseudoClasses();

			return base.MeasureOverride(availableSize);
		}
		
		/// <inheritdoc/>
		protected override void OnApplyTemplate(TemplateAppliedEventArgs e) {
			base.OnApplyTemplate(e);

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
		
	}

}
