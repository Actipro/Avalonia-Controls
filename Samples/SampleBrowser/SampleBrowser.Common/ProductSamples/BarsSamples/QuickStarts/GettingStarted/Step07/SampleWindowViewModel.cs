/*

RIBBON GETTING STARTED SERIES - STEP 7

STEP SUMMARY:

	Configure the view models to be displayed in the editor context menu.

CHANGES SINCE LAST STEP:

	Added the EditorContextMenuItems property and defined the view models for
	each control to be displayed in the menu.

	Added the SelectAllCommand property that is registered with the bar manager's
	CompositeCommand for select all.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using System.Collections.Generic;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step07 {

	/// <summary>
	/// Defines the view model for this sample.
	/// </summary>
	public class SampleWindowViewModel : ObservableObjectBase {

		private ICommand? _copyCommand;
		private ICommand? _cutCommand;
		private ICommand? _pasteCommand;
		private ICommand? _redoCommand;
		private ICommand? _selectAllCommand;
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
		/// The view models of the menu controls to be displayed in the editor's context menu.
		/// </summary>
		public IEnumerable<object> EditorContextMenuItems {
			get {
				//	SAMPLE NOTE 7.1:
				//		The context menu can use the same control view models as the Ribbon control.
				//
				//		Since this context menu is not expected to change, the property can be based
				//		on IEnumerable<T> instead of ObservableCollection<T>.
				//
				//		The SelectAll view model is new for this step and shows that you can use commands
				//		in a context menu that are not currently displayed in the Ribbon.
				yield return BarManager.ControlViewModels[SampleBarControlKeys.Cut];
				yield return BarManager.ControlViewModels[SampleBarControlKeys.Copy];
				yield return BarManager.ControlViewModels[SampleBarControlKeys.Paste];
				yield return new BarSeparatorViewModel();
				yield return BarManager.ControlViewModels[SampleBarControlKeys.SelectAll];
			}
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
		/// The command for select all operations.
		/// </summary>
		public ICommand? SelectAllCommand {
			get => _selectAllCommand;
			set => UpdateCommandAndCompositeRegistration(ref _selectAllCommand, value, BarManager.SelectAllCommand);
		}

		/// <summary>
		/// The command for undo operations.
		/// </summary>
		public ICommand? UndoCommand {
			get => _undoCommand;
			set => UpdateCommandAndCompositeRegistration(ref _undoCommand, value, BarManager.UndoCommand);
		}

	}

}
