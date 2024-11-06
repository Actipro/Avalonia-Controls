using Avalonia.Controls.Templates;
using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a standalone toolbar control.
	/// </summary>
	public class StandaloneToolBarViewModel : ObservableObjectBase {

		private bool _isVisible = true;
		private BarControlTemplateSelector _itemContainerTemplateSelector = new();

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <summary>
		/// The collection of items in the control.
		/// </summary>
		/// <value>The collection of items in the control.</value>
		public ObservableCollection<object> Items { get; } = new ();
		
		/// <summary>
		/// The <see cref="BarControlTemplateSelector"/> that creates UI controls for bar control view models.
		/// </summary>
		public BarControlTemplateSelector ItemContainerTemplateSelector {
			get => _itemContainerTemplateSelector;
			set => SetProperty(ref _itemContainerTemplateSelector, value);
		}

		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[{this.Items.Count} items]";

	}

}
