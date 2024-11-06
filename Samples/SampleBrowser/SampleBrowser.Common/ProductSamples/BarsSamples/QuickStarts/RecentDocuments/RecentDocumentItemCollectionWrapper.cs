using ActiproSoftware.UI.Avalonia.Controls.Bars;
using Avalonia.Media;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.RecentDocuments {

	/// <summary>
	/// Defines a collection which can wrap any collection of source objects as instances of <see cref="RecentDocumentItem"/> that
	/// can be used to populate the items source of a <see cref="RecentDocumentControl"/>.
	/// </summary>
	/// <remarks>
	/// This class uses explicit getter functions and setter actions for interacting with relevant properties on the source object
	/// so that any view model can be used without taking a dependency on a specify type of interface, but using an interface is
	/// a viable alternative for defining how to read/write properties from the source object.
	/// </remarks>
	/// <typeparam name="TSource">The type of the source object.</typeparam>
	public class RecentDocumentItemCollectionWrapper<TSource> : IList, ICollection<RecentDocumentItem>, INotifyCollectionChanged {

		private readonly List<Tuple<TSource, RecentDocumentItem>> _items = new();

		private Func<TSource, string?>? _descriptionGetter;
		private Func<TSource, string?>? _documentNameGetter;
		private Func<TSource, object?>? _largeIconGetter;
		private Func<TSource, object?>? _smallIconGetter;
		private Func<TSource, bool>? _isPinnedGetter;
		private Action<TSource, bool>? _isPinnedSetter;
		private Func<TSource, DateTime>? _lastOpenedDateTimeGetter;
		private Action<TSource, DateTime>? _lastOpenedDateTimeSetter;
		private Func<TSource, Uri?>? _locationGetter;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// EVENTS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="INotifyCollectionChanged.CollectionChanged"/>
		public event NotifyCollectionChangedEventHandler? CollectionChanged;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="sourceCollection">The collection of items to be wrapped.</param>
		public RecentDocumentItemCollectionWrapper(IEnumerable<TSource> sourceCollection) {
			if (sourceCollection is null)
				throw new ArgumentNullException(nameof(sourceCollection));

			foreach (var source in sourceCollection)
				AddWrappedItem(source);

			if (sourceCollection is INotifyCollectionChanged observableCollection)
				observableCollection.CollectionChanged += this.OnSourceCollectionChanged;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// INTERFACE IMPLEMENTATIONS
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		#region ICollection

		int ICollection.Count => _items.Count;

		/// <inheritdoc/>
		bool ICollection.IsSynchronized => false;

		/// <inheritdoc/>
		object ICollection.SyncRoot { get; } = new object();

		/// <inheritdoc/>
		void ICollection.CopyTo(Array array, int index) {
			for (var i = index; i < array.Length && i < _items.Count; i++)
				array.SetValue(_items[i].Item2, i);
		}

		#endregion ICollection

		#region ICollection<T>

		void ICollection<RecentDocumentItem>.Add(RecentDocumentItem item) => throw new NotSupportedException();

		void ICollection<RecentDocumentItem>.Clear() => throw new NotSupportedException();

		bool ICollection<RecentDocumentItem>.Contains(RecentDocumentItem item)
			=> _items.Any(x => Equals(item, x.Item2));

		void ICollection<RecentDocumentItem>.CopyTo(RecentDocumentItem[] array, int arrayIndex)
			=> _items.Select(x => x.Item2).ToList().CopyTo(array, arrayIndex);

		int ICollection<RecentDocumentItem>.Count => ((ICollection)this).Count;

		bool ICollection<RecentDocumentItem>.IsReadOnly => true;

		bool ICollection<RecentDocumentItem>.Remove(RecentDocumentItem item) => throw new NotSupportedException();

		#endregion ICollection<T>

		#region IEnumerable<T>

		IEnumerator<RecentDocumentItem> IEnumerable<RecentDocumentItem>.GetEnumerator()
			=> _items.Select(x => x.Item2).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> ((IEnumerable<RecentDocumentItem>)this).GetEnumerator();

		#endregion IEnumerable<T>

		#region IList

		/// <inheritdoc/>
		object? IList.this[int index] {
			get => _items[index].Item2;
			set => throw new NotSupportedException();
		}

		/// <inheritdoc/>
		int IList.Add(object? value) => throw new NotSupportedException();

		/// <inheritdoc/>
		void IList.Clear() => throw new NotSupportedException();

		bool IList.Contains(object? item)
			=> (item is RecentDocumentItem typedItem) && this.Contains(typedItem);

		/// <inheritdoc/>
		int IList.IndexOf(object? value) {
			if (value is RecentDocumentItem typedValue) {
				for (var index = 0; index < _items.Count; index++) {
					if (Equals(value, _items[index].Item2))
						return index;
				}
			}
			return -1;
		}

		/// <inheritdoc/>
		void IList.Insert(int index, object? value) => throw new NotSupportedException();

		/// <inheritdoc/>
		bool IList.IsFixedSize => true;

		/// <inheritdoc/>
		bool IList.IsReadOnly => true;

		/// <inheritdoc/>
		void IList.Remove(object? value) => throw new NotSupportedException();

		/// <inheritdoc/>
		void IList.RemoveAt(int index) => throw new NotSupportedException();

		#endregion IList

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private Tuple<TSource, RecentDocumentItem> AddWrappedItem(TSource source) {
			var item = CreateWrappedItem(source);

			// Listen for changes in the RecentDocumentItem
			item.PropertyChanged += this.OnItemPropertyChanged;

			// Listen for changes in the source
			if (source is INotifyPropertyChanged observableSource)
				observableSource.PropertyChanged += this.OnSourcePropertyChanged;

			var tuple = new Tuple<TSource, RecentDocumentItem>(source, item);
			_items.Add(tuple);
			return tuple;
		}

		private RecentDocumentItem CreateWrappedItem(TSource source) {
			var item = new RecentDocumentItem() { DataContext = source };
			UpdateItemFromSource(source, item);
			return item;
		}

		private Tuple<TSource, RecentDocumentItem>? GetWrappedItems(RecentDocumentItem item)
			=> _items.FirstOrDefault(x => Equals(x.Item2, item));

		private Tuple<TSource, RecentDocumentItem>? GetWrappedItems(TSource source)
			=> _items.FirstOrDefault(x => Equals(x.Item1, source));

		private void OnItemPropertyChanged(object? sender, Avalonia.AvaloniaPropertyChangedEventArgs e) {
			// Update the source object when relevant properties change on RecentDocumentItem
			if (sender is RecentDocumentItem item) {
				if (e.Property == RecentDocumentItem.IsPinnedProperty) {
					if ((IsPinnedSetter is { } setter) && GetWrappedItems(item) is { } tuple)
						setter.Invoke(tuple.Item1, item.IsPinned);
				}
				else if (e.Property == RecentDocumentItem.LastOpenedDateTimeProperty) {
					if ((LastOpenedDateTimeSetter is { } setter) && GetWrappedItems(item) is { } tuple)
						setter.Invoke(tuple.Item1, item.LastOpenedDateTime);
				}
			}
		}

		private void OnSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
			switch (e.Action) {
				case NotifyCollectionChangedAction.Move:
					// Ignore changes in position
					break;

				case NotifyCollectionChangedAction.Add:
				case NotifyCollectionChangedAction.Remove:
				case NotifyCollectionChangedAction.Replace:
					var oldItems = new List<RecentDocumentItem>();
					if (e.OldItems is not null) {
						foreach (var source in e.OldItems.OfType<TSource>()) {
							var tuple = RemoveWrappedItem(source);
							if (tuple is not null)
								oldItems.Add(tuple.Item2);
						}
					}
					var newItems = new List<RecentDocumentItem>();
					if (e.NewItems is not null) {
						foreach (var source in e.NewItems.OfType<TSource>()) {
							var tuple = AddWrappedItem(source);
							if (tuple is not null)
								newItems.Add(tuple.Item2);
						}
					}
					var args = e.Action switch {
						NotifyCollectionChangedAction.Add => new NotifyCollectionChangedEventArgs(e.Action, newItems),
						NotifyCollectionChangedAction.Remove => new NotifyCollectionChangedEventArgs(e.Action, oldItems),
						NotifyCollectionChangedAction.Replace => new NotifyCollectionChangedEventArgs(e.Action, newItems, oldItems),
						_ => null
					};
					if (args is not null)
						this.CollectionChanged?.Invoke(this, args);
					break;

				case NotifyCollectionChangedAction.Reset:
					// Fully rebuild
					foreach (var source in _items.Select(x => x.Item1).ToArray())
						RemoveWrappedItem(source);
					if (sender is IEnumerable enumerable) {
						foreach (var source in enumerable.OfType<TSource>())
							AddWrappedItem(source);
					}
					this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
					break;
			}
		}

		private void OnSourcePropertyChanged(object? sender, PropertyChangedEventArgs e) {
			// Synchronize the RecentDocumentItem with the source object after a property change
			if ((sender is TSource source) && (GetWrappedItems(source) is { } tuple))
				UpdateItemFromSource(tuple.Item1, tuple.Item2);
		}

		private Tuple<TSource, RecentDocumentItem>? RemoveWrappedItem(TSource source) {
			var tuple = GetWrappedItems(source);
			if (tuple is not null) {

				// Stop listening for property changes
				if (tuple.Item1 is INotifyPropertyChanged observableSource)
					observableSource.PropertyChanged -= this.OnSourcePropertyChanged;
				tuple.Item2.PropertyChanged -= this.OnItemPropertyChanged;

				_items.Remove(tuple);
			}
			return tuple;
		}

		private void UpdateAllWrappedItems(Action<RecentDocumentItem, TSource> action) {
			// Perform the given action on all wrapped items
			foreach (var tuple in _items)
				action.Invoke(tuple.Item2, tuple.Item1);
		}

		private void UpdateItemFromSource(TSource source, RecentDocumentItem item) {
			// Use the defined getters to update the item from current values on the source
			if (DescriptionGetter is not null)
				item.Description = DescriptionGetter.Invoke(source);
			if (LargeIconGetter is not null)
				item.LargeIcon = LargeIconGetter.Invoke(source);
			if (SmallIconGetter is not null)
				item.SmallIcon = SmallIconGetter.Invoke(source);
			if (IsPinnedGetter is not null)
				item.IsPinned = IsPinnedGetter.Invoke(source);
			if (LastOpenedDateTimeGetter is not null)
				item.LastOpenedDateTime = LastOpenedDateTimeGetter.Invoke(source);
			if (LocationGetter is not null)
				item.Location = LocationGetter.Invoke(source);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The function that will get the description from the source object.
		/// </summary>
		public Func<TSource, string?>? DescriptionGetter {
			get => _descriptionGetter;
			set {
				_descriptionGetter = value;
				if (_descriptionGetter is not null)
					UpdateAllWrappedItems((i, s) => i.Description = _descriptionGetter.Invoke(s));
			}
		}

		/// <summary>
		/// The function that will get the document name from the source object.
		/// </summary>
		public Func<TSource, string?>? DocumentNameGetter {
			get => _documentNameGetter;
			set {
				_documentNameGetter = value;
				if (_documentNameGetter is not null)
					UpdateAllWrappedItems((i, s) => i.DocumentName = _documentNameGetter.Invoke(s));
			}
		}

		/// <summary>
		/// The function that will get the large icon from the source object.
		/// </summary>
		public Func<TSource, object?>? LargeIconGetter {
			get => _largeIconGetter;
			set {
				_largeIconGetter = value;
				if (_largeIconGetter is not null)
					UpdateAllWrappedItems((i, s) => i.LargeIcon = _largeIconGetter.Invoke(s));
			}
		}

		/// <summary>
		/// The function that will get the small icon from the source object.
		/// </summary>
		public Func<TSource, object?>? SmallIconGetter {
			get => _smallIconGetter;
			set {
				_smallIconGetter = value;
				if (_smallIconGetter is not null)
					UpdateAllWrappedItems((i, s) => i.SmallIcon = _smallIconGetter.Invoke(s));
			}
		}

		/// <summary>
		/// The function that will get the if the source object is pinned.
		/// </summary>
		public Func<TSource, bool>? IsPinnedGetter {
			get => _isPinnedGetter;
			set {
				_isPinnedGetter = value;
				if (_isPinnedGetter is not null)
					UpdateAllWrappedItems((i, s) => i.IsPinned = _isPinnedGetter.Invoke(s));
			}
		}

		/// <summary>
		/// The action that will set the if the source object is pinned.
		/// </summary>
		public Action<TSource, bool>? IsPinnedSetter {
			get => _isPinnedSetter;
			set {
				_isPinnedSetter = value;
				if (_isPinnedSetter is not null)
					UpdateAllWrappedItems((i, s) => _isPinnedSetter.Invoke(s, i.IsPinned));
			}
		}

		/// <summary>
		/// The function that will get the last opened date/time of the source object.
		/// </summary>
		public Func<TSource, DateTime>? LastOpenedDateTimeGetter {
			get => _lastOpenedDateTimeGetter;
			set {
				_lastOpenedDateTimeGetter = value;
				if (_lastOpenedDateTimeGetter is not null)
					UpdateAllWrappedItems((i, s) => i.LastOpenedDateTime = _lastOpenedDateTimeGetter.Invoke(s));
			}
		}

		/// <summary>
		/// The action that will set the last opened date/time of the source object.
		/// </summary>
		public Action<TSource, DateTime>? LastOpenedDateTimeSetter {
			get => _lastOpenedDateTimeSetter;
			set {
				_lastOpenedDateTimeSetter = value;
				if (_lastOpenedDateTimeSetter is not null)
					UpdateAllWrappedItems((i, s) => _lastOpenedDateTimeSetter.Invoke(s, i.LastOpenedDateTime));
			}
		}

		/// <summary>
		/// The function that will get the location of the source object.
		/// </summary>
		public Func<TSource, Uri?>? LocationGetter {
			get => _locationGetter;
			set {
				_locationGetter = value;
				if (_locationGetter is not null)
					UpdateAllWrappedItems((i, s) => i.Location = _locationGetter.Invoke(s));
			}
		}

	}

	/// <summary>
	/// Defines a collection which can wrap any collection of <see cref="RecentDocumentViewModel"/> view models as instances
	/// of <see cref="RecentDocumentItem"/> that can be used to populate the items source of a <see cref="RecentDocumentControl"/>.
	/// </summary>
	/// <remarks>
	/// This class provides an easy way to predefine the getters/setters used by the base class and could easily be adapted
	/// to use any view model or interface to replace <see cref="RecentDocumentViewModel"/> used by this sample.
	/// </remarks>
	public class RecentDocumentItemCollectionWrapper : RecentDocumentItemCollectionWrapper<RecentDocumentViewModel> {

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		/// <param name="sourceCollection">The collection of items to be wrapped.</param>
		public RecentDocumentItemCollectionWrapper(IEnumerable<RecentDocumentViewModel> sourceCollection)
			: base(sourceCollection) {

			// Define the getters/setters that corresponding to the known interface
			DescriptionGetter = (x) => x.Description;
			DocumentNameGetter = (x) => x.DocumentName;
			IsPinnedGetter = (x) => x.IsPinned;
			IsPinnedSetter = (x, v) => x.IsPinned = v;
			LargeIconGetter = (x) => x.LargeIcon;
			SmallIconGetter = (x) => x.SmallIcon;
			LastOpenedDateTimeGetter = (x) => x.LastOpenedDateTime;
			LastOpenedDateTimeSetter = (x, v) => x.LastOpenedDateTime = v;
			LocationGetter = (x) => x.Location;
		}

	}

}
