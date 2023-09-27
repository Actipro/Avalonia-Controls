using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Displays one or more <see cref="ControlExample"/> instances.
	/// </summary>
	[PseudoClasses(pcWide)]
	public class ControlExampleItemsControl : HeaderedItemsControl {

		private bool _isWideMeasure;

		private const double WideThreshold = 1200;

		// Pseudo classes
		private const string pcWide = ":wide";

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public ControlExampleItemsControl() {
			this.Items.CollectionChanged += this.OnItemsCollectionChanged;
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
		
		private void UpdatePseudoClasses()
			=> PseudoClasses.Set(pcWide, _isWideMeasure);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc/>
		protected override Size MeasureOverride(Size availableSize) {
			_isWideMeasure = (availableSize.Width >= WideThreshold);
			UpdatePseudoClasses();

			return base.MeasureOverride(availableSize);
		}
		
		/// <summary>
		/// The table of contents.
		/// </summary>
		public ObservableCollection<ControlExampleTocItem> Toc { get; } = new();

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
