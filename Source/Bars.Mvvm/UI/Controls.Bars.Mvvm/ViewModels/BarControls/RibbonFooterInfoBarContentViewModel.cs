using Avalonia.Controls.Templates;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for image and text content within a ribbon footer.
	/// </summary>
	public class RibbonFooterInfoBarContentViewModel : ObservableObjectBase, IHasTag {

		private object? _action;
		private IDataTemplate? _actionTemplate;
		private bool _canClose = true;
		private object? _content;
		private IDataTemplate? _contentTemplate;
		private object? _icon;
		private bool _isIconVisible = true;
		private string? _message;
		private Thickness _padding = new Thickness(10, 5, 10, 5);
		private InfoBarSeverity _severity = InfoBarSeverity.Information;
		private object? _tag;
		private string? _title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The optional action to be displayed in the info bar.
		/// </summary>
		public object? Action {
			get => _action;
			set => SetProperty(ref _action, value);
		}

		/// <summary>
		/// The <see cref="IDataTemplate"/> used to display the <see cref="Action"/>.
		/// </summary>
		public IDataTemplate? ActionTemplate {
			get => _actionTemplate;
			set => SetProperty(ref _actionTemplate, value);
		}

		/// <summary>
		/// Whether the info bar can be closed by the user.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool CanClose {
			get => _canClose;
			set => SetProperty(ref _canClose, value);
		}

		/// <summary>
		/// A value representing the icon.
		/// </summary>
		public object? Icon {
			get => _icon;
			set => SetProperty(ref _icon, value);
		}

		/// <summary>
		/// The optional content to be displayed in the info bar.
		/// </summary>
		public object? Content {
			get => _content;
			set => SetProperty(ref _content, value);
		}

		/// <summary>
		/// The <see cref="IDataTemplate"/> used to display the <see cref="Content"/>.
		/// </summary>
		public IDataTemplate? ContentTemplate {
			get => _contentTemplate;
			set => SetProperty(ref _contentTemplate, value);
		}

		/// <summary>
		/// Whether the icon is visible.
		/// </summary>
		/// <value>
		/// The default value is <c>true</c>.
		/// </value>
		public bool IsIconVisible {
			get => _isIconVisible;
			set => SetProperty(ref _isIconVisible, value);
		}

		/// <summary>
		/// The message content.
		/// </summary>
		public string? Message {
			get => _message;
			set => SetProperty(ref _message, value);
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
		
		/// <summary>
		/// The severity of the info bar.
		/// </summary>
		/// <value>
		/// The default value is <see cref="InfoBarSeverity.Information"/>.
		/// </value>
		public InfoBarSeverity Severity {
			get => _severity;
			set => SetProperty(ref _severity, value);
		}

		/// <inheritdoc cref="IHasTag.Tag"/>
		public object? Tag {
			get => _tag;
			set => SetProperty(ref _tag, value);
		}

		/// <summary>
		/// The title content.
		/// </summary>
		public string? Title {
			get => _title;
			set => SetProperty(ref _title, value);
		}
		
		/// <inheritdoc/>
		public override string ToString()
			=> $"{this.GetType().FullName}[Title='{this.Title}']";

	}

}
