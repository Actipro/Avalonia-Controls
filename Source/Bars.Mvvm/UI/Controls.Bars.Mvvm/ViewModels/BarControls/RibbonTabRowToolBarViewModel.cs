using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a tab row toolbar control within a ribbon.
	/// </summary>
	public class RibbonTabRowToolBarViewModel : ObservableObjectBase, IHasTag {

		private bool _isVisible = true;
		private object? _tag;

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
		public ObservableCollection<object> Items { get; } = new();

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
