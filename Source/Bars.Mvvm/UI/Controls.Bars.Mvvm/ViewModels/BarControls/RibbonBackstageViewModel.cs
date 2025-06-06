using Avalonia.Controls.Templates;
using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a Backstage control within a ribbon.
	/// </summary>
	public class RibbonBackstageViewModel : ObservableObjectBase, IHasTag {

		private bool _canClose = true;
		private bool _canSelectFirstTabOnOpen = true;
		private IDataTemplate? _contentTemplate;
		private bool _isOpen;
		private object? _selectedItem;
		private object? _tag;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Indicates whether the Backstage close button should be visible, and whether <c>Esc</c> can close Backstage.
		/// </summary>
		/// <value>
		/// <c>true</c> if the Backstage close button should be visible, and whether <c>Esc</c> can close Backstage; otherwise, <c>false</c>.
		/// The default value is <c>true</c>.
		/// </value>
		/// <remarks>
		/// This feature is useful for scenarios where there is no document open in the main window yet, and Backstage is open, allowing
		/// for an end user to select a new document or open a document.
		/// In this scenario, you don't want the end user to be able to close Backstage until they take some action.
		/// </remarks>
		public bool CanClose {
			get => _canClose;
			set => SetProperty(ref _canClose, value);
		}

		/// <summary>
		/// Indicates whether the first tab should be selected when the Backstage is opened.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanSelectFirstTabOnOpen {
			get => _canSelectFirstTabOnOpen;
			set => SetProperty(ref _canSelectFirstTabOnOpen, value);
		}

		/// <summary>
		/// Closes the Backstage.
		/// </summary>
		public void Close() {
			this.IsOpen = false;
		}

		/// <summary>
		/// The <see cref="IDataTemplate"/> used to display Backstage tab content.
		/// </summary>
		public IDataTemplate? ContentTemplate {
			get => _contentTemplate;
			set => SetProperty(ref _contentTemplate, value);
		}

		/// <summary>
		/// Indicates whether the Backstage is open.
		/// </summary>
		/// <value>
		/// The default value is <c>false</c>.
		/// </value>
		public bool IsOpen {
			get => _isOpen;
			set => SetProperty(ref _isOpen, value);
		}

		/// <summary>
		/// The collection of items to display in the Backstage.
		/// </summary>
		public ObservableCollection<object> Items { get; } = new ();

		/// <summary>
		/// Opens the Backstage.
		/// </summary>
		public void Open()
			=> this.IsOpen = true;

		/// <summary>
		/// The Backstage's selected item, which is a tab item in the <see cref="Items"/> collection.
		/// </summary>
		public object? SelectedItem {
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object? Tag {
			get => _tag;
			set => SetProperty(ref _tag, value);
		}

		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[{this.Items.Count} items]";

	}

}
