namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents an abstract view model base for an observable object that is identified by a unique string key.
	/// </summary>
	public abstract class BarKeyedObjectViewModelBase : ObservableObjectBase, IHasKey {

		private string? _key;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the <see cref="BarKeyedObjectViewModelBase"/> class.
		/// </summary>
		protected BarKeyedObjectViewModelBase()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BarKeyedObjectViewModelBase"/> class.
		/// </summary>
		/// <param name="key">A string that uniquely identifies the control.</param>
		protected BarKeyedObjectViewModelBase(string? key) {
			_key = key;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="IHasKey.Key"/>
		public string? Key {
			get => _key;
			set {
				if (_key != value) {
					if (!string.IsNullOrEmpty(_key))
						throw new ArgumentException("The key cannot be changed once it has been set.", nameof(value));

					this.SetProperty(ref _key, value);
				}
			}
		}
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[Key='{this.Key}']";

	}

}
