using ActiproSoftware.UI.Avalonia.Themes;
using ActiproSoftware.UI.Avalonia.Themes.Bars.Mvvm;
using ActiproSoftware.UI.Controls.Templates;
using Avalonia.Controls.Templates;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// An <see cref="IDataTemplate"/> implementation that selects an appropriate gallery item <see cref="IDataTemplate"/>.
	/// </summary>
	public class RibbonFooterContentTemplateSelector : IDataTemplateSelector {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes an instance of the class.
		/// </summary>
		public RibbonFooterContentTemplateSelector() {
			this.InfoBarDataTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonFooterContentInfoBarDataTemplate);
			this.SimpleDataTemplate = FindDataTemplateResource(BarsMvvmResourceKeys.RibbonFooterContentSimpleDataTemplate);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private static IDataTemplate? FindDataTemplateResource(string key)
			=> Application.Current?.FindResource(key) as IDataTemplate;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Selects an <see cref="IDataTemplate"/> for the specified item and container.
		/// </summary>
		/// <param name="item">The item to examine.</param>
		/// <param name="container">The container control.</param>
		/// <returns>The <see cref="IDataTemplate"/> to use.</returns>
		public virtual IDataTemplate? SelectTemplate(object? item, Control? container) {
			return item switch {
				RibbonFooterInfoBarContentViewModel => this.InfoBarDataTemplate,
				RibbonFooterSimpleContentViewModel => this.SimpleDataTemplate,
				_ => null
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC DATATEMPLATE PROPERTIES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonFooterInfoBarContentViewModel"/>.
		/// </summary>
		public IDataTemplate? InfoBarDataTemplate { get; set; }
		
		/// <summary>
		/// The <see cref="IDataTemplate"/> to use for a <see cref="RibbonFooterSimpleContentViewModel"/>.
		/// </summary>
		public IDataTemplate? SimpleDataTemplate { get; set; }

	}

}
