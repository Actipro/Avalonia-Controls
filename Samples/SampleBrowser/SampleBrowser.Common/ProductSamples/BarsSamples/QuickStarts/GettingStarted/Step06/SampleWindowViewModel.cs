/*

RIBBON GETTING STARTED SERIES - STEP 6

STEP SUMMARY:

	Configure commands on the ribbon to interact with editor.

CHANGES SINCE LAST STEP:

	Added ICommand properties for the editor Undo/Redo and Cut/Copy/Paste commands that, when
	assigned, update the registration of the corresponding composite command in bar manager.

	Prior sample comments were removed/condensed.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step06 {

	/// <summary>
	/// Defines the view model for this sample.
	/// </summary>
	public class SampleWindowViewModel : ObservableObjectBase {

		private ICommand? _copyCommand;
		private ICommand? _cutCommand;
		private ICommand? _pasteCommand;
		private ICommand? _redoCommand;
		private ICommand? _undoCommand;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Initializes a new instance of the class.
		/// </summary>
		public SampleWindowViewModel() {

			var barManager = this.BarManager;

			// Initialize the view model for the Ribbon
			this.Ribbon = new RibbonViewModel() {
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.None,

				Tabs = {
					new RibbonTabViewModel("Home") {
						Groups = {

							new RibbonGroupViewModel(SampleBarControlKeys.GroupUndo, "Undo") {
								SmallIcon = barManager.ImageProvider.GetImage(SampleBarControlKeys.GroupUndo, BarImageSize.Small),
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysSmall,
										Items = {
											barManager.ControlViewModels[SampleBarControlKeys.Undo],
											barManager.ControlViewModels[SampleBarControlKeys.Redo],
										}
									}
								}
							},

							new RibbonGroupViewModel(SampleBarControlKeys.GroupClipboard, "Clipboard") {
								SmallIcon = barManager.ImageProvider.GetImage(SampleBarControlKeys.GroupClipboard, BarImageSize.Small),
								Items = {
									barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.NeverLarge,
										Items = {
											barManager.ControlViewModels[SampleBarControlKeys.Cut],
											barManager.ControlViewModels[SampleBarControlKeys.Copy],
										}
									}
								}
							},
						}
					},
					new RibbonTabViewModel("Help") {
						KeyTipText = "E",
						Groups = {
							new RibbonGroupViewModel(SampleBarControlKeys.GroupHelpResources, "Resources") {
								Items = {
									barManager.ControlViewModels[SampleBarControlKeys.Help],
								}
							}
						}
					}

				}

			};
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The manager for working with the objects used by Ribbon and related menus.
		/// </summary>
		private SampleBarManager BarManager { get; } = new SampleBarManager();

		/// <summary>
		/// Update a command reference and ensure the command is properly registered with an associated composite command.
		/// </summary>
		/// <param name="currentValue">A reference to the field which stores the current value of the command.</param>
		/// <param name="newValue">The new value for the command.</param>
		/// <param name="compositeCommand">The composite command to be updated.</param>
		private void UpdateCommandAndCompositeRegistration(ref ICommand? currentValue, ICommand? newValue, CompositeCommand compositeCommand) {
			// Ignore if the value is not changed
			if (currentValue == newValue)
				return;

			// Unregister any existing command
			if (currentValue is not null)
				compositeCommand.UnregisterCommand(currentValue);

			// Update the value
			currentValue = newValue;

			// Register any new command
			if (currentValue is not null)
				compositeCommand.RegisterCommand(currentValue);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The command for clipboard copy operations.
		/// </summary>
		public ICommand? CopyCommand {
			get => _copyCommand;
			set => UpdateCommandAndCompositeRegistration(ref _copyCommand, value, BarManager.CopyCommand);
		}

		/// <summary>
		/// The command for clipboard cut operations.
		/// </summary>
		public ICommand? CutCommand {
			get => _cutCommand;
			set => UpdateCommandAndCompositeRegistration(ref _cutCommand, value, BarManager.CutCommand);
		}

		/// <summary>
		/// The command for clipboard paste operations.
		/// </summary>
		public ICommand? PasteCommand {
			get => _pasteCommand;
			set => UpdateCommandAndCompositeRegistration(ref _pasteCommand, value, BarManager.PasteCommand);
		}

		/// <summary>
		/// The command for redo operations.
		/// </summary>
		public ICommand? RedoCommand {
			get => _redoCommand;
			set => UpdateCommandAndCompositeRegistration(ref _redoCommand, value, BarManager.RedoCommand);
		}

		/// <summary>
		/// The view model for the ribbon control.
		/// </summary>
		public RibbonViewModel Ribbon { get; }

		/// <summary>
		/// The command for undo operations.
		/// </summary>
		public ICommand? UndoCommand {
			get => _undoCommand;
			set => UpdateCommandAndCompositeRegistration(ref _undoCommand, value, BarManager.UndoCommand);
		}

	}

}
