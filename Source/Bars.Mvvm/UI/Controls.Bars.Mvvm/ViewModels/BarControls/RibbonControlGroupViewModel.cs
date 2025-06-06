using Avalonia.Layout;
using System.Collections.ObjectModel;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a control group control within a ribbon group.
	/// </summary>
	public class RibbonControlGroupViewModel : ObservableObjectBase, IHasTag {

		private HorizontalAlignment _horizontalContentAlignment = HorizontalAlignment.Left;
		private bool _isVisible = true;
		private ItemVariantBehavior _itemVariantBehavior = ItemVariantBehavior.All;
		private RibbonControlGroupSeparatorMode _separatorMode = RibbonControlGroupSeparatorMode.Default;
		private object? _tag;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// A <see cref="HorizontalAlignment"/> that indicates how items stacked vertically should be aligned horizontally.
		/// </summary>
		/// <value>
		/// The default value is <see cref="HorizontalAlignment.Left"/>.
		/// </value>
		public HorizontalAlignment HorizontalContentAlignment {
			get => _horizontalContentAlignment;
			set => SetProperty(ref _horizontalContentAlignment, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <summary>
		/// The collection of items in the control.
		/// </summary>
		public ObservableCollection<object> Items { get; } = new ();
		
		/// <summary>
		/// An <see cref="Bars.ItemVariantBehavior"/> that indicates how variant sizes should be applied to items.
		/// </summary>
		/// <value>
		/// An <see cref="Bars.ItemVariantBehavior"/> that indicates how variant sizes should be applied to items.
		/// The default value is <see cref="ItemVariantBehavior.All"/>.
		/// </value>
		public ItemVariantBehavior ItemVariantBehavior {
			get => _itemVariantBehavior;
			set => SetProperty(ref _itemVariantBehavior, value);
		}
		
		/// <summary>
		/// A <see cref="RibbonControlGroupSeparatorMode"/> indicating how separators should be positioned around the control.
		/// </summary>
		/// <value>
		/// The default value is <see cref="RibbonControlGroupSeparatorMode.Default"/>.
		/// </value>
		public RibbonControlGroupSeparatorMode SeparatorMode {
			get => _separatorMode;
			set => SetProperty(ref _separatorMode, value);
		}

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object? Tag {
			get => _tag;
			set => SetProperty(ref _tag, value);
		}

		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[{this.Items.Count} item(s)']";

	}

}
