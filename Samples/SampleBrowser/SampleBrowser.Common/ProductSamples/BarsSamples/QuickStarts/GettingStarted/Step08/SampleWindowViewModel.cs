/*

RIBBON GETTING STARTED SERIES - STEP 8

STEP SUMMARY:

	Support the Quick Access Toolbar by adding and configuring new properties on the RibbonViewModel.

CHANGES SINCE LAST STEP:

	Updated QuickAccessToolBarMode from None to Visible and configured QuickAccessToolBarLocation.
	Added QuickAccessToolBarViewModel configuration for Items and CommonItems.

	Prior sample comments were removed/condensed.

*/

using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using System.Collections.Generic;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.GettingStarted.Step08 {

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
				//	SAMPLE NOTE 8.1:
				//		Since the Quick Access Toolbar is finally being populated, it should no
				//		longer be hidden on the Ribbon. It is visible by default, so this property
				//		assignment will be removed in a future step.
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Visible,

				//	SAMPLE NOTE 8.2:
				//		By default, the Quick Access Toolbar (QAT) is displayed above by the Ribbon and
				//		combined with the window title bar. This sample will configure the QAT
				//		below the Ribbon to illustrate the capability. At run-time, the QAT customize menu
				//		can be used to change the location above or below the Ribbon.
				QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,

				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					//	SAMPLE NOTE 8.3:
					//		Define the collection of view models that are commonly displayed in the
					//		Quick Access Toolbar. When customizing the Quick Access Toolbar, these
					//		common items are always displayed in the context menu to be easily
					//		toggled on or off.
					//		
					//		These view models DO NOT have to be currently displayed, and including
					//		a view model in the common list will not make the view model actively
					//		visible.
					CommonItems = {
						barManager.ControlViewModels[SampleBarControlKeys.Cut],
						barManager.ControlViewModels[SampleBarControlKeys.Copy],
						barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
					},

					//	SAMPLE NOTE 8.4:
					//		Define the initial collection of view models that will be displayed in the
					//		Quick Access Toolbar. Note that Undo/Redo are not part of the CommonItems
					//		collection but can still be included in the Quick Access Toolbar.
					Items = {
						barManager.ControlViewModels[SampleBarControlKeys.Undo],
						barManager.ControlViewModels[SampleBarControlKeys.Redo],
						barManager.ControlViewModels[SampleBarControlKeys.Cut],
						barManager.ControlViewModels[SampleBarControlKeys.Copy],
						barManager.ControlViewModels[SampleBarControlKeys.PasteMenu],
					}

				},

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
