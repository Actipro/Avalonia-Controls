using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Displays one or more <see cref="ControlExample"/> instances.
	/// </summary>
	[PseudoClasses(pcImmersive)]
	[PseudoClasses(pcWide)]
	public class ControlExampleItemsControl : HeaderedItemsControl {

		private bool _hasDocumentation = false;
		private bool _hasRelatedSamples = false;
		private bool _isWideMeasure;

		private const double WideThreshold = 1200;

		/// <summary>
		/// Defines the <see cref="HasDocumentation"/> property.
		/// </summary>
		public static readonly DirectProperty<ControlExampleItemsControl, bool> HasDocumentationProperty
			= AvaloniaProperty.RegisterDirect<ControlExampleItemsControl, bool>(nameof(HasDocumentation), getter: b => b.HasDocumentation);

		/// <summary>
		/// Defines the <see cref="HasRelatedSamples"/> property.
		/// </summary>
		public static readonly DirectProperty<ControlExampleItemsControl, bool> HasRelatedSamplesProperty
			= AvaloniaProperty.RegisterDirect<ControlExampleItemsControl, bool>(nameof(HasRelatedSamples), getter: b => b.HasRelatedSamples);

		/// <summary>
		/// Defines the <see cref="UseImmersiveView"/> property.
		/// </summary>
		public static readonly StyledProperty<bool> UseImmersiveViewProperty
			= AvaloniaProperty.Register<ControlExampleItemsControl, bool>(nameof(UseImmersiveView));

		// Pseudo classes
		private const string pcImmersive = ":immersive";
		private const string pcWide = ":wide";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		static ControlExampleItemsControl() {
			UseImmersiveViewProperty.Changed.AddClassHandler<ControlExampleItemsControl>((x, _) => x.UpdatePseudoClasses());
		}

		public ControlExampleItemsControl() {
			this.Documentation.CollectionChanged += (_, _) => UpdateHasDocumentation();
			this.Items.CollectionChanged += this.OnItemsCollectionChanged;
			this.RelatedSamples.CollectionChanged += (_, _) => UpdateHasRelatedSamples();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
			if ((e.Action == NotifyCollectionChangedAction.Add) && (e.NewItems is not null)) {
				// Insert
				var insertIndex = e.NewStartingIndex;
				foreach (var addedItem in e.NewItems.OfType<ControlExample>())
					Toc.Insert(insertIndex++, new ControlExampleTocItem(addedItem));
			}
			else {
				// Full replace
				Toc.Clear();
				foreach (var item in Items.OfType<ControlExample>())
					Toc.Add(new ControlExampleTocItem(item));
			}
		}

		private void UpdateHasDocumentation()
			=> HasDocumentation = (this.Documentation.Any(x => x is not null));

		private void UpdateHasRelatedSamples()
			=> HasRelatedSamples = (this.RelatedSamples.Any(x => x is not null));

		private void UpdatePseudoClasses() {
			PseudoClasses.Set(pcImmersive, UseImmersiveView);
			PseudoClasses.Set(pcWide, _isWideMeasure);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The documentation links.
		/// </summary>
		public ObservableCollection<ControlExampleLinkItem> Documentation { get; } = new();

		/// <summary>
		/// Whether there are any documentation links available to display.
		/// </summary>
		public bool HasDocumentation {
			get => _hasDocumentation;
			private set => SetAndRaise(HasDocumentationProperty, ref _hasDocumentation, value);
		}

		/// <summary>
		/// Whether there are any related samples links available to display.
		/// </summary>
		public bool HasRelatedSamples {
			get => _hasRelatedSamples;
			private set => SetAndRaise(HasRelatedSamplesProperty, ref _hasRelatedSamples, value);
		}

		/// <inheritdoc/>
		protected override Size MeasureOverride(Size availableSize) {
			_isWideMeasure = (availableSize.Width >= WideThreshold);
			UpdatePseudoClasses();

			return base.MeasureOverride(availableSize);
		}

		/// <summary>
		/// The related samples links.
		/// </summary>
		public ObservableCollection<ControlExampleLinkItem> RelatedSamples { get; } = new();

		/// <summary>
		/// The table of contents.
		/// </summary>
		public ObservableCollection<ControlExampleTocItem> Toc { get; } = new();

		/// <summary>
		/// Whether to use immersive view.
		/// </summary>
		public bool UseImmersiveView {
			get => GetValue(UseImmersiveViewProperty);
			set => SetValue(UseImmersiveViewProperty, value);
		}

	}

	/// <summary>
	/// A link for the control examples.
	/// </summary>
	public record ControlExampleLinkItem() {

		public void Open() {
			if (!string.IsNullOrWhiteSpace(this.Url)) {
				if (this.Url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
					ApplicationViewModel.Instance.OpenUrlCommand.Execute(this.Url);
				else
					ApplicationViewModel.Instance.NavigateViewToItemInfo(new Uri("https://ActiproSoftware" + this.Url));
			}
		}

		/// <summary>
		/// The item's title.
		/// </summary>
		public string? Title { get; set; }

		/// <summary>
		/// The item's identifier.
		/// </summary>
		public string? Url { get; set; }

	}

	/// <summary>
	/// A <see cref="ControlExample"/> table of contents item.
	/// </summary>
	/// <param name="Example">The related <see cref="ControlExample"/>.</param>
	public record ControlExampleTocItem(ControlExample Example) {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Brings the related example into view.
		/// </summary>
		public void BringIntoView()
			=> Example.BringIntoView();

		/// <summary>
		/// The heading text.
		/// </summary>
		public string Heading
			=> Example.Header?.ToString() ?? "(no heading)";

	}

}
