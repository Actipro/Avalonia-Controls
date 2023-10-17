using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiproSoftware.ProductSamples.ThemesSamples.Common {

	public class FruitDataItemModel : ObservableObjectBase {

		private string? _colorCategory;
		private bool? _isPopular;
		private string? _leadingProducer;
		private string? _name;
		private int? _servingCalories;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The color category.
		/// </summary>
		public string? ColorCategory {
			get => _colorCategory;
			set => SetProperty(ref _colorCategory, value);
		}

		/// <summary>
		/// Indicates if the fruit is popular.
		/// </summary>
		public bool? IsPopular {
			get => _isPopular;
			set => SetProperty(ref _isPopular, value);
		}

		/// <summary>
		/// The leading producer.
		/// </summary>
		public string? LeadingProducer {
			get => _leadingProducer;
			set => SetProperty(ref _leadingProducer, value);
		}

		/// <summary>
		/// The name.
		/// </summary>
		public string? Name {
			get => _name;
			set => SetProperty(ref _name, value);
		}

		/// <summary>
		/// The serving calories.
		/// </summary>
		public int? ServingCalories {
			get => _servingCalories;
			set => SetProperty(ref _servingCalories, value);
		}

	}
}
