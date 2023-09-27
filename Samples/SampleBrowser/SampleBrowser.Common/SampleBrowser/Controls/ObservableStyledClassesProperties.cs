using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Defines the "IsWatching" attached property to watch a styled element for changes in classes and then
	/// updates a "ClassesString" attached property with the current classes.
	/// </summary>
	public static class ObservableStyledClassesProperties {

		private static readonly Dictionary<StyledElement, StyledElementClassesCollectionWatcher> _watchedElements = new();

		#region Property Definitions

		public static readonly AttachedProperty<string> ClassesStringProperty
			= AvaloniaProperty.RegisterAttached<StyledElement, string>("ClassesString", typeof(ObservableStyledClassesProperties), defaultValue: string.Empty, defaultBindingMode: Avalonia.Data.BindingMode.OneWay);

		/// <summary>
		/// Defines the <c>IsWatching</c> attached property.
		/// </summary>
		public static readonly AttachedProperty<bool> IsWatchingProperty
			= AvaloniaProperty.RegisterAttached<StyledElement, bool>("IsWatching", typeof(ObservableStyledClassesProperties));

		#endregion Property Definitions


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private class StyledElementClassesCollectionWatcher : IDisposable {

			public event EventHandler? ClassesChanged;

			public StyledElementClassesCollectionWatcher(StyledElement styledElement) {
				this.StyledElement = styledElement ?? throw new ArgumentNullException(nameof(styledElement));
				this.StyledElement.Classes.CollectionChanged += OnClassesCollectionChanged;
			}

			private void OnClassesCollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
				=> ClassesChanged?.Invoke(this, EventArgs.Empty);

			public StyledElement StyledElement { get; }

			public void Dispose()
				=> this.StyledElement.Classes.CollectionChanged -= OnClassesCollectionChanged;

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		static ObservableStyledClassesProperties() {
			IsWatchingProperty.Changed.AddClassHandler<StyledElement>(OnIsWatchingChanged);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private static void OnIsWatchingChanged(AvaloniaObject obj, AvaloniaPropertyChangedEventArgs e) {
			if (obj is StyledElement styledElement) {
				bool isWatching = e.GetNewValue<bool>();
				if (isWatching)
					WatchElement(styledElement);
				else
					UnwatchElement(styledElement);
			}
		}

		private static void OnWatchedElementClassesChanged(object? sender, EventArgs e) {
			if (sender is StyledElementClassesCollectionWatcher watcher) {
				var styledElement = watcher.StyledElement;

				// Get classes other than pseudo classes
				var classes = styledElement.Classes
					.Where(x => !x.StartsWith(':'))
					.ToArray();

				// Pre-sort alphabetically
				Array.Sort(classes);

				// Join with priority (same priority remains alpha sorted)
				var text = string.Join(' ', classes.OrderBy(x => GetClassSortPriority(x)));
				styledElement.SetValue(ClassesStringProperty, text);

				static int GetClassSortPriority(string className) {
					// Themes first
					if (className.StartsWith("theme-"))
						return 0;

					// Text sizing
					if (className.StartsWith("size-"))
						return 1;

					// Variants
					return className switch {
						"accent" => 2,
						"success" => 2,
						"warning" => 2,
						"danger" => 2,

						// Everything else
						_ => 100
					};
				}
			}
		}

		private static void UnwatchElement(StyledElement styledElement) {
			if (_watchedElements.Remove(styledElement, out var watcher)) {
				watcher.ClassesChanged -= OnWatchedElementClassesChanged;
				watcher.Dispose();
			}
		}

		private static void WatchElement(StyledElement styledElement) {
			var watcher = new StyledElementClassesCollectionWatcher(styledElement);
			_watchedElements[styledElement] = watcher;
			watcher.ClassesChanged += OnWatchedElementClassesChanged;
		}


		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Gets if the object is being watched for changes in the Classes collection.
		/// </summary>
		/// <param name="obj">The object to examine.</param>
		/// <returns><c>true</c> if the object is being watched; otherwise, <c>false</c>.</returns>
		/// <seealso cref="SetIsWatching(AvaloniaObject, bool)"/>
		public static bool GetIsWatching(AvaloniaObject obj)
			=> obj.GetValue(IsWatchingProperty);

		/// <summary>
		/// Sets if the object is being watched for changes in the Classes collection.
		/// </summary>
		/// <param name="obj">The target object.</param>
		/// <param name="value">The value to assign.</param>
		/// <seealso cref="GetIsWatching(AvaloniaObject)"/>
		public static void SetIsWatching(AvaloniaObject obj, bool value)
			=> obj.SetValue(IsWatchingProperty, value);

	}

}
