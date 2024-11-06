using Avalonia.Controls.Templates;
using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a Backstage control within a ribbon.
	/// </summary>
	public class RibbonBackstageViewModel : ObservableObjectBase {

		private IDataTemplate? _contentTemplate;
		private bool _isOpen;
		private object? _selectedItem;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
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
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[{this.Items.Count} items]";

	}

}
