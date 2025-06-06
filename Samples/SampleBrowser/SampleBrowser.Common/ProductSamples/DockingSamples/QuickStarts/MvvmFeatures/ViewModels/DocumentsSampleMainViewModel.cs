using ActiproSoftware.UI.Avalonia;
using ActiproSoftware.UI.Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.DockingSamples.QuickStarts.MvvmFeatures.ViewModels {

	/// <summary>
	/// Represents the main view-model for the document windows sample.
	/// </summary>
	public class DocumentsSampleMainViewModel : ObservableObjectBase {

		private int _documentIndex = 1;
		private int _profilePhotoIndex = 1;
		private readonly DeferrableObservableCollection<DocumentItemViewModel> _documentItems = [];

		private DelegateCommand<object>? _activateNextDocumentCommand;
		private DelegateCommand<object>? _closeActiveDocumentCommand;
		private DelegateCommand<object>? _createNewImageDocumentCommand;
		private DelegateCommand<object>? _createNewTextDocumentCommand;
		private DelegateCommand<object>? _selectFirstDocumentCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public DocumentsSampleMainViewModel() {
			CreateNewTextDocument(activate: false);
			CreateNewTextDocument(activate: false);
			CreateNewImageDocument(activate: false);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private string GetNextProfilePhotoFileName() {
			// Cycle profile photos Woman01.jpg through Woman06.jpg
			if (_profilePhotoIndex > 6)
				_profilePhotoIndex = 1;
			var index = _profilePhotoIndex++;

			return $"Woman0{index}.jpg";
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The "Activate Next Document" command.
		/// </summary>
		public ICommand ActivateNextDocumentCommand
			=> _activateNextDocumentCommand ??= new DelegateCommand<object>(_ => {
				if (_documentItems.Any()) {
					var index = 0;
					var activeDocumentItem = _documentItems.FirstOrDefault(x => x.IsActive);
					if (activeDocumentItem is not null)
						index = _documentItems.IndexOf(activeDocumentItem) + 1;
					if (index >= _documentItems.Count)
						index = 0;

					_documentItems[index].IsActive = true;
				}
			});

		/// <summary>
		/// The "Close Active Document" command.
		/// </summary>
		public ICommand CloseActiveDocumentCommand
			=> _closeActiveDocumentCommand ??= new DelegateCommand<object>(_ => {
				var activeDocumentItem = _documentItems.FirstOrDefault(x => x.IsActive);
				if (activeDocumentItem is not null)
					_documentItems.Remove(activeDocumentItem);
			});

		/// <summary>
		/// Creates a new image document.
		/// </summary>
		/// <param name="activate">Whether to activate the document.</param>
		public void CreateNewImageDocument(bool activate) {
			var index = _documentIndex++;

			// Iterate through a collection of profile photos for each new image document
			string profilePhotoFileName = GetNextProfilePhotoFileName();

			var viewModel = new ImageDocumentItemViewModel() {
				SerializationId = $"Document{index}.jpg",  // NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
				FileName = $"Document{index}.jpg",
				ImageRelativePath = profilePhotoFileName
			};
			viewModel.Title = viewModel.FileName;
			

			_documentItems.Add(viewModel);

			if (activate)
				viewModel.IsActive = true;
			else
				viewModel.IsOpen = true;
		}

		/// <summary>
		/// Creates a new text document.
		/// </summary>
		/// <param name="activate">Whether to activate the document.</param>
		public void CreateNewTextDocument(bool activate) {
			var index = _documentIndex++;
			var viewModel = new TextDocumentItemViewModel() {
				SerializationId = $"Document{index}.txt",  // NOTE: Every docking window must have a unique SerializationId if you wish to use layout serialization
				FileName = $"Document{index}.txt",
				Text = $"Text document {index} dynamically created at {DateTime.Now}."
			};
			viewModel.Title = viewModel.FileName;

			_documentItems.Add(viewModel);

			if (activate)
				viewModel.IsActive = true;
			else
				viewModel.IsOpen = true;
		}

		/// <summary>
		/// The "Create New Image Document" command.
		/// </summary>
		public ICommand CreateNewImageDocumentCommand
			=> _createNewImageDocumentCommand ??= new DelegateCommand<object>(_ => {
				CreateNewImageDocument(activate: true);
			});

		/// <summary>
		/// The "Create New Text Document" command.
		/// </summary>
		public ICommand CreateNewTextDocumentCommand
			=> _createNewTextDocumentCommand ??= new DelegateCommand<object>(_ => {
				CreateNewTextDocument(activate: true);
			});

		/// <summary>
		/// The document items associated with this view-model.
		/// </summary>
		public IList<DocumentItemViewModel> DocumentItems
			=> _documentItems;

		/// <summary>
		/// The "Select First Document" command.
		/// </summary>
		public ICommand SelectFirstDocumentCommand
			=> _selectFirstDocumentCommand ??= new DelegateCommand<object>(_ => {
				var documentItem = _documentItems.FirstOrDefault();
				if (documentItem is not null)
					documentItem.IsSelected = true;
			});

	}

}
