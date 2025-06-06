using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a main menu control.
	/// </summary>
	public class BarMainMenuViewModel : ObservableObjectBase, IHasTag {

		private BarControlTemplateSelector _itemContainerTemplateSelector = new();
		private object? _tag;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The collection of items in the control.
		/// </summary>
		public ObservableCollection<object> Items { get; } = [];

		/// <summary>
		/// The <see cref="BarControlTemplateSelector"/> that creates UI controls for bar control view models.
		/// </summary>
		public BarControlTemplateSelector ItemContainerTemplateSelector {
			get => _itemContainerTemplateSelector;
			set => SetProperty(ref _itemContainerTemplateSelector, value);
		}

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object? Tag {
			get => _tag;
			set => SetProperty(ref _tag, value);
		}

		/// <inheritdoc/>
		public override string ToString()
			=> $"{GetType().FullName}[{Items.Count} items]";

	}

}
