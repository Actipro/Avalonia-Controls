using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a quick-access toolbar control within a ribbon.
	/// </summary>
	public class RibbonQuickAccessToolBarViewModel : ObservableObjectBase {

		private bool _isCustomizeButtonVisible = true;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// The collection of common items that should appear in the QAT's Customize menu, from which they can be toggled into and out of <see cref="Items"/>.
		/// </summary>
		public ObservableCollection<object> CommonItems { get; } = [];
		
		/// <summary>
		/// Indicates whether the customize button is visible.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsCustomizeButtonVisible {
			get => _isCustomizeButtonVisible;
			set => SetProperty(ref _isCustomizeButtonVisible, value);
		}

		/// <summary>
		/// The collection of items in the control.
		/// </summary>
		public ObservableCollection<object> Items { get; } = [];
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[{this.Items.Count} items]";

	}

}
