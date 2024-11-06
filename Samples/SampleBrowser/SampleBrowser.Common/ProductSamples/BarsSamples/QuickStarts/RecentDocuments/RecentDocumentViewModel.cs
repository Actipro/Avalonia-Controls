using ActiproSoftware.ProductSamples.BarsSamples.Common;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.RecentDocuments {

	/// <summary>
	/// Defines a view model for <see cref="ActiproSoftware.UI.Avalonia.Controls.Bars.RecentDocumentItem"/>.
	/// </summary>
	public class RecentDocumentViewModel : ObservableObjectBase {

		private string? _description;
		private string? _documentName;
		private bool _isPinned;
		private object? _largeIcon;
		private object? _smallIcon;
		private DateTime _lastOpenedDateTime = DateTime.Now;
		private Uri? _location;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates a default collection of view models used by samples.
		/// </summary>
		public static ObservableCollection<RecentDocumentViewModel> CreateDefaultCollection()
			=> new ObservableCollection<RecentDocumentViewModel>(CreateDefaultItems());

		/// <summary>
		/// Creates default view models used by samples.
		/// </summary>
		public static IEnumerable<RecentDocumentViewModel> CreateDefaultItems() {
			// To avoid redefining the same information, this sample transforms the default RecentDocumentItem instances
			// used by other samples into view models with the same properties
			return RecentDocumentHelper.CreateDefaultItems().Select(x =>
				new RecentDocumentViewModel() {
					Description = x.Description,
					DocumentName = x.DocumentName,
					IsPinned = x.IsPinned,
					LargeIcon = x.LargeIcon,
					SmallIcon = x.SmallIcon,
					LastOpenedDateTime = x.LastOpenedDateTime,
					Location = x.Location,
				});
		}

		/// <summary>
		/// The description.
		/// </summary>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}

		/// <summary>
		/// The document name.
		/// </summary>
		public string? DocumentName {
			get => _documentName;
			set => SetProperty(ref _documentName, value);
		}

		/// <summary>
		/// Whether the item is pinned to the top of the list.
		/// </summary>
		public bool IsPinned {
			get => _isPinned;
			set => SetProperty(ref _isPinned, value);
		}

		/// <summary>
		/// A value representing an icon for large items, typically 32x32.
		/// </summary>
		public object? LargeIcon {
			get => _largeIcon;
			set => SetProperty(ref _largeIcon, value);
		}

		/// <summary>
		/// The last date/time the item was opened.
		/// </summary>
		public DateTime LastOpenedDateTime {
			get => _lastOpenedDateTime;
			set => SetProperty(ref _lastOpenedDateTime, value);
		}

		/// <summary>
		/// The location of the item.
		/// </summary>
		public Uri? Location {
			get => _location;
			set => SetProperty(ref _location, value);
		}

		/// <summary>
		/// A value representing an icon for large items, typically 16x16.
		/// </summary>
		public object? SmallIcon {
			get => _smallIcon;
			set => SetProperty(ref _smallIcon, value);
		}

	}

}
