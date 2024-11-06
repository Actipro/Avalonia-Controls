using ActiproSoftware.UI.Controls.Templates;
using Avalonia.Controls.Templates;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a footer within a ribbon.
	/// </summary>
	public class RibbonFooterViewModel : ObservableObjectBase {

		private object? _content;
		private IDataTemplate? _contentTemplate;
		private IDataTemplateSelector? _contentTemplateSelector = new RibbonFooterContentTemplateSelector();
		private RibbonFooterKind _kind;
		private Thickness _padding = new (10, 5);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		/// <summary>
		/// The content to display within the ribbon footer.
		/// </summary>
		public object? Content {
			get => _content;
			set {
				if (SetProperty(ref _content, value))
					OnPropertyChanged(nameof(ContentTemplate));
			} 
		}
		
		/// <summary>
		/// The <see cref="IDataTemplate"/> used to display the content.
		/// </summary>
		public IDataTemplate? ContentTemplate {
			get => _contentTemplate ?? ContentTemplateSelector?.SelectTemplate(Content, control: null);
			set => SetProperty(ref _contentTemplate, value);
		}

		/// <summary>
		/// The <see cref="IDataTemplateSelector"/> that picks a <see cref="IDataTemplate"/> used to display the content.
		/// </summary>
		public IDataTemplateSelector? ContentTemplateSelector {
			get => _contentTemplateSelector;
			set => SetProperty(ref _contentTemplateSelector, value);
		}

		/// <summary>
		/// A <see cref="RibbonFooterKind"/> indicating the kind of footer, which determines its appearance.
		/// </summary>
		/// <value>
		/// The default value is <see cref="RibbonFooterKind.Default"/>.
		/// </value>
		public RibbonFooterKind Kind {
			get => _kind;
			set => SetProperty(ref _kind, value);
		}
		
		/// <summary>
		/// The padding inside the control.
		/// </summary>
		/// <value>
		/// The default value is <c>10,5</c>.
		/// </value>
		public Thickness Padding {
			get => _padding;
			set => SetProperty(ref _padding, value);
		}
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[Content='{this.Content}']";

	}

}
