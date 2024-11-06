using ActiproSoftware.Properties.Bars.Mvvm;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for an application button control within a ribbon.
	/// </summary>
	public class RibbonApplicationButtonViewModel : ObservableObjectBase {

		private string? _keyTipText;
		private string? _label;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public RibbonApplicationButtonViewModel()  // Parameterless constructor required for XAML support
			: this(label: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified label.
		/// </summary>
		/// <param name="label">The text label to display.</param>
		public RibbonApplicationButtonViewModel(string? label)
			: this(label, keyTipText: null) { }

		/// <summary>
		/// Initializes a new instance of the class with the specified label and key tip text.
		/// </summary>
		/// <param name="label">The text label to display.</param>
		/// <param name="keyTipText">The key tip text, which is auto-generated from the <paramref name="label"/> if <c>null</c>.</param>
		public RibbonApplicationButtonViewModel(string? label, string? keyTipText) {
			_label = label ?? SR.GetString(SRName.UIApplicationButtonText);
			_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromLabel(this._label);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.KeyTipText"/>
		public string? KeyTipText {
			get => _keyTipText;
			set => SetProperty(ref _keyTipText, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string? Label {
			get => _label;
			set => SetProperty(ref _label, value);
		}
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[Label='{this.Label}']";

	}

}
