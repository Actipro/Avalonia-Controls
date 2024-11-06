using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.StandaloneToolBarIntro {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			BasicUsageStandaloneToolBarViewModel = CreateBasicUsageStandaloneToolBarViewModel();
			basicUsageToolBarMvvm.DataContext = BasicUsageStandaloneToolBarViewModel;

			var verticalUsageBarManager = new BarManager();
			var varticalUsageCommandAdapter = new TextBoxBarManagerCommandAdapter(verticalUsageBarManager, verticalUsageTextBox);
			VerticalUsageStandaloneToolBarViewModel = CreateRichTextStandaloneToolBarViewModel(verticalUsageBarManager);
			verticalUsageToolBarMvvm.DataContext = VerticalUsageStandaloneToolBarViewModel;

			var toolWindowUsageBarManager = new BarManager();
			var toolWindowUsageCommandAdapter = new TextBoxBarManagerCommandAdapter(toolWindowUsageBarManager, toolWindowUsageTextBox);
			ToolWindowStandaloneToolBarViewModel = CreateToolWindowStandaloneToolBarViewModel(toolWindowUsageBarManager);
			toolWindowUsageToolBarMvvm.DataContext = ToolWindowStandaloneToolBarViewModel;

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		private static StandaloneToolBarViewModel CreateBasicUsageStandaloneToolBarViewModel() {
			return new StandaloneToolBarViewModel() {
				Items = {
					new BarButtonViewModel("Undo") { KeyTipText = "AZ", SmallIcon = ImageLoader.GetIcon("Undo16.png") },
					new BarButtonViewModel("Redo") { KeyTipText = "AQ", SmallIcon = ImageLoader.GetIcon("Redo16.png") },
					new BarSeparatorViewModel(),
					new BarButtonViewModel("New") { SmallIcon = ImageLoader.GetIcon("New16.png"), ToolBarItemVariantBehavior = ItemVariantBehavior.AlwaysMedium },
					new BarButtonViewModel("Open") { SmallIcon = ImageLoader.GetIcon("Open16.png"), ToolBarItemVariantBehavior = ItemVariantBehavior.AlwaysMedium },
					new BarButtonViewModel("Save") { SmallIcon = ImageLoader.GetIcon("Save16.png"), ToolBarItemVariantBehavior = ItemVariantBehavior.AlwaysMedium },
					new BarSeparatorViewModel(),
					new BarButtonViewModel("Cut") { KeyTipText = "X", SmallIcon = ImageLoader.GetIcon("Cut16.png") },
					new BarButtonViewModel("Copy") { KeyTipText = "C", SmallIcon = ImageLoader.GetIcon("Copy16.png") },
					new BarSplitButtonViewModel("PasteMenu", "Paste", "V") {
						SmallIcon = ImageLoader.GetIcon("Paste16.png"),
						MenuItems = {
							new BarButtonViewModel("Paste") { SmallIcon = ImageLoader.GetIcon("Paste16.png") },
							new BarButtonViewModel("PasteSpecial") { KeyTipText = "S", SmallIcon = ImageLoader.GetIcon("PasteSpecial16.png") },
						}
					},
					new BarButtonViewModel("FormatPainter") { SmallIcon = ImageLoader.GetIcon("FormatPainter16.png") },
					new BarSeparatorViewModel(),
					new BarToggleButtonViewModel("Bold") { KeyTipText = "1", SmallIcon = ImageLoader.GetIcon("Bold16.png") },
					new BarToggleButtonViewModel("Italic") { KeyTipText = "2", SmallIcon = ImageLoader.GetIcon("Italic16.png") },
					new BarToggleButtonViewModel("Underline") { KeyTipText = "3", SmallIcon = ImageLoader.GetIcon("Underline16.png") },
					new BarSeparatorViewModel(),
					new BarButtonViewModel("IncreaseFontSize") { KeyTipText = "FG", SmallIcon = ImageLoader.GetIcon("GrowFont16.png") },
					new BarButtonViewModel("DecreaseFontSize") { KeyTipText = "FK", SmallIcon = ImageLoader.GetIcon("ShrinkFont16.png") },
					new BarButtonViewModel("Subscript") { KeyTipText = "5", SmallIcon = ImageLoader.GetIcon("Subscript16.png") },
					new BarButtonViewModel("Superscript") { KeyTipText = "6", SmallIcon = ImageLoader.GetIcon("Superscript16.png") },
				}
			};
		}

		/// <summary>
		/// Creates a <see cref="StandaloneToolBarViewModel"/> for a typical rich text editor.
		/// </summary>
		/// <param name="barManager">The associated <see cref="BarManager"/>.</param>
		private static StandaloneToolBarViewModel CreateRichTextStandaloneToolBarViewModel(BarManager barManager) {
			return new StandaloneToolBarViewModel() {
				Items = {
					barManager.ControlViewModels[BarControlKeys.Undo],
					barManager.ControlViewModels[BarControlKeys.Redo],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.PasteMenu],
					barManager.ControlViewModels[BarControlKeys.Cut],
					barManager.ControlViewModels[BarControlKeys.Copy],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.Font],
					barManager.ControlViewModels[BarControlKeys.FontSize],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.Bold],
					barManager.ControlViewModels[BarControlKeys.Italic],
					barManager.ControlViewModels[BarControlKeys.Underline],
					barManager.ControlViewModels[BarControlKeys.LineSpacing],
					barManager.ControlViewModels[BarControlKeys.FontColor],
					barManager.ControlViewModels[BarControlKeys.TextHighlightColor],
				}
			};
		}

		/// <summary>
		/// Creates a <see cref="StandaloneToolBarViewModel"/> for the tool window.
		/// </summary>
		/// <param name="barManager">The associated <see cref="BarManager"/>.</param>
		private static StandaloneToolBarViewModel CreateToolWindowStandaloneToolBarViewModel(BarManager barManager) {
			return new StandaloneToolBarViewModel() {
				Items = {
					barManager.ControlViewModels[BarControlKeys.AlignLeft],
					barManager.ControlViewModels[BarControlKeys.AlignCenter],
					barManager.ControlViewModels[BarControlKeys.AlignRight],
					barManager.ControlViewModels[BarControlKeys.AlignJustify],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.FontColor],
					barManager.ControlViewModels[BarControlKeys.Borders],
					new BarSeparatorViewModel(),
					barManager.ControlViewModels[BarControlKeys.Bullets],
					barManager.ControlViewModels[BarControlKeys.Numbering],
				}
			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The view model for the "Basic usage" sample.
		/// </summary>
		public StandaloneToolBarViewModel BasicUsageStandaloneToolBarViewModel { get; private set; }

		/// <summary>
		/// The view model for the "Vertical usage" sample.
		/// </summary>
		/// <value>A <see cref="StandaloneToolBarViewModel"/>.</value>
		public StandaloneToolBarViewModel VerticalUsageStandaloneToolBarViewModel { get; private set; }

		/// <summary>
		/// The view model for the "Tool window" sample.
		/// </summary>
		/// <value>A <see cref="StandaloneToolBarViewModel"/>.</value>
		public StandaloneToolBarViewModel ToolWindowStandaloneToolBarViewModel { get; private set; }

	}

}
