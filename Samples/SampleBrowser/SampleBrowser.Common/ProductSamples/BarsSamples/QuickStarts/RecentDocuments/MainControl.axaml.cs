using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.RecentDocuments {

	public partial class MainControl : UserControl {

		private ICommand? _clearCommandMvvm;
		private ICommand? _openCommand;
		private ICommand? _openCommandMvvm;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			// Use a helper class to predefine a collection of recently opened documents
			RecentDocuments = RecentDocumentHelper.CreateDefaultCollection();
			RecentDocumentsForBackstage = RecentDocumentHelper.CreateDefaultCollection();

			// Preview the view models and collection for the "MVVM usage" sample
			RecentDocumentViewModels = RecentDocumentViewModel.CreateDefaultCollection();
			RecentDocumentsForMvvm = new RecentDocumentItemCollectionWrapper(RecentDocumentViewModels);

			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private IStorageProvider GetStorageProvider() {
			return TopLevel.GetTopLevel(this)?.StorageProvider
				?? throw new InvalidOperationException("Unable to resolve the storage provider.");
		}

		private async void OpenFilePicker() {
			var storageFileList = await GetStorageProvider().OpenFilePickerAsync(new FilePickerOpenOptions() { AllowMultiple = false });
			if (storageFileList?.Count == 1) {
				using var storageFile = storageFileList[0];
				
				// Use a helper class to update an existing entry or create a new one
				RecentDocumentHelper.NotifyDocumentOpened(RecentDocuments, storageFile.Path);
			}
		}

		private async void OpenFilePickerMvvm() {
			var storageFileList = await GetStorageProvider().OpenFilePickerAsync(new FilePickerOpenOptions() { AllowMultiple = false });
			if (storageFileList?.Count == 1) {
				using var storageFile = storageFileList[0];
				var location = storageFile.Path;

				// Create a recent document entry for this file if it is not already in the list
				var recentDocumentViewModel = RecentDocumentViewModels.FirstOrDefault(x => x.Location == location);
				if (recentDocumentViewModel is null) {
					RecentDocumentHelper.ExtractFileDetails(location, out var description, out var smallIcon, out var largeIcon);
					recentDocumentViewModel = new RecentDocumentViewModel() {
						Description = description,
						SmallIcon = smallIcon,
						LargeIcon = largeIcon,
						Location = location,
					};

					// Add the view model to the collection
					RecentDocumentViewModels.Add(recentDocumentViewModel);
				}
				else {
					// Update the last opened date/time for the existing view model
					recentDocumentViewModel.LastOpenedDateTime = DateTime.Now;
				}

			}
		}

		private void OpenRecentDocument(RecentDocumentItem recentDocumentItem) {
			// Process opening a document from the recent document control
			MessageBox.Show($"This is where you would open the following document:\r\n\r\n{recentDocumentItem.Location}\r\n\r\nThe {nameof(RecentDocumentItem.LastOpenedDateTime)} property will now be updated to the current time.", caption: "Open Document", image: MessageBoxImage.Information);
			recentDocumentItem.NotifyDocumentOpened();
		}

		private void OpenRecentDocumentMvvm(RecentDocumentViewModel recentDocumentViewModel) {
			// Process opening a document from the recent document control
			MessageBox.Show($"This is where you would open the following document:\r\n\r\n{recentDocumentViewModel.Location}\r\n\r\nThe {nameof(RecentDocumentItem.LastOpenedDateTime)} property will now be updated to the current time.", caption: "Open Document", image: MessageBoxImage.Information);
			recentDocumentViewModel.LastOpenedDateTime = DateTime.Now;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The command that, when invoked, will clear the collection of view models used by the "MVVM usage" sample.
		/// </summary>
		public ICommand ClearCommandMvvm
			=> _clearCommandMvvm ??= new DelegateCommand<object>(_ => {
				RecentDocumentViewModels.Clear();
			});

		/// <summary>
		/// The command to be invoked when opening a recent document. If a <see cref="RecentDocumentItem"/> is not
		/// passed as a parameter, the user will be prompted to pick a file.
		/// </summary>
		/// <remarks>
		/// This command instance is used by the "Basic usage" sample.
		/// </remarks>
		public ICommand OpenCommand
			=> _openCommand ??= new DelegateCommand<object>(param => {
				if (param is RecentDocumentItem recentDocumentItem)
					OpenRecentDocument(recentDocumentItem);
				else
					OpenFilePicker();
			});

		/// <summary>
		/// The command to be invoked when opening a recent document. If a <see cref="RecentDocumentItem"/> is not
		/// passed as a parameter, the user will be prompted to pick a file.
		/// </summary>
		/// <remarks>
		/// This command instance is used by the "MVVM usage" sample.
		/// </remarks>
		public ICommand OpenCommandMvvm
			=> _openCommandMvvm ??= new DelegateCommand<object>(param => {
				if (param is RecentDocumentItem recentDocumentItem
					&& recentDocumentItem.DataContext is RecentDocumentViewModel recentDocumentViewModel) {

					OpenRecentDocumentMvvm(recentDocumentViewModel);
				}
				else
					OpenFilePickerMvvm();
			});

		/// <summary>
		/// The collection of recent documents for use with the "Basic usage" sample.
		/// </summary>
		public ObservableCollection<RecentDocumentItem> RecentDocuments { get; }

		/// <summary>
		/// The collection of recent documents for use with the "Backstage usage" sample.
		/// </summary>
		public ObservableCollection<RecentDocumentItem> RecentDocumentsForBackstage { get; }

		/// <summary>
		/// The collection of recent documents for use with the "MVVM usage" sample.
		/// </summary>
		public IEnumerable<RecentDocumentItem> RecentDocumentsForMvvm { get; }

		/// <summary>
		/// The collection of recent documents view models for use with the "MVVM usage" sample.
		/// </summary>
		public ObservableCollection<RecentDocumentViewModel> RecentDocumentViewModels { get; }

	}

}
