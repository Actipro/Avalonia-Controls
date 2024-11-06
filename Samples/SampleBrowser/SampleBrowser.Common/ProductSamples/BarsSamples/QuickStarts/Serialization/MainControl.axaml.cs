using ActiproSoftware.ProductSamples.BarsSamples.Common;
using ActiproSoftware.SampleBrowser;
using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Controls.Bars;
using ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace ActiproSoftware.ProductSamples.BarsSamples.QuickStarts.Serialization {

	public partial class MainControl : UserControl {

		private RibbonViewModel? _ribbonViewModel;

		private readonly string? _mvvmOriginalLayout;
		private readonly string? _xamlOriginalLayout;

		private ICommand? _mvvmRestoreLayoutCommand;
		private ICommand? _mvvmRestoreOriginalCommand;
		private ICommand? _mvvmSaveLayoutCommand;
		private ICommand? _xamlRestoreLayoutCommand;
		private ICommand? _xamlRestoreOriginalCommand;
		private ICommand? _xamlSaveLayoutCommand;

		private readonly Dictionary<string, object> _mvvmControls = new(StringComparer.InvariantCultureIgnoreCase);

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NESTED TYPES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Defines keys for controls used by the MVVM Ribbon sample.
		/// </summary>
		public static class BarControlKeys {
			public const string Cut = nameof(Cut);
			public const string Copy = nameof(Copy);
			public const string Paste = nameof(Paste);
			public const string RestoreLayout = nameof(RestoreLayout);
			public const string RestoreOriginal = nameof(RestoreOriginal);
			public const string SaveLayout = nameof(SaveLayout);
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();

			mvvmRibbon.QuickAccessToolBarItemAdding += OnQuickAccessToolBarItemAdding;

			// Try to cache the original XAML layout so it can be restored
			if (TrySaveLayout(xamlRibbon, out var xamlLayout)) {
				_xamlOriginalLayout = xamlLayout;
				this.CurrentXamlLayout = xamlLayout;
			}

			// Try to cache the original MVVM layout so it can be restored
			if (TrySaveLayout(xamlRibbon, out var mvvmLayout)) {
				_mvvmOriginalLayout = mvvmLayout;
				this.CurrentMvvmLayout = mvvmLayout;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// NON-PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Creates the ribbon view model for the MVVM sample.
		/// </summary>
		private RibbonViewModel CreateRibbonViewModel() {

			// Initialize the controls to be used by the MVVM ribbon
			_mvvmControls[BarControlKeys.Cut] = new BarButtonViewModel(BarControlKeys.Cut) { SmallIcon = ImageLoader.GetIcon("Cut16.png") };
			_mvvmControls[BarControlKeys.Copy] = new BarButtonViewModel(BarControlKeys.Copy) { SmallIcon = ImageLoader.GetIcon("Copy16.png") };
			_mvvmControls[BarControlKeys.Paste] = new BarButtonViewModel(BarControlKeys.Paste) { SmallIcon = ImageLoader.GetIcon("Paste16.png") };
			_mvvmControls[BarControlKeys.SaveLayout] = new BarButtonViewModel(BarControlKeys.SaveLayout, MvvmSaveLayoutCommand) {
				LargeIcon = ImageLoader.GetIcon("Save32.png"),
				SmallIcon = ImageLoader.GetIcon("Save16.png"),
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			};
			_mvvmControls[BarControlKeys.RestoreLayout] = new BarButtonViewModel(BarControlKeys.RestoreLayout, MvvmRestoreLayoutCommand) {
				LargeIcon = ImageLoader.GetIcon("Open32.png"),
				SmallIcon = ImageLoader.GetIcon("Open16.png"),
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			};
			_mvvmControls[BarControlKeys.RestoreOriginal] = new BarButtonViewModel(BarControlKeys.RestoreOriginal, MvvmRestoreOriginalCommand) {
				KeyTipText = "O",
				LargeIcon = ImageLoader.GetIcon("OpenMono32.png"),
				SmallIcon = ImageLoader.GetIcon("OpenMono16.png"),
				ToolBarItemVariantBehavior = ItemVariantBehavior.All,
			};

			return new RibbonViewModel() {
				IsApplicationButtonVisible = false,
				QuickAccessToolBarLocation = RibbonQuickAccessToolBarLocation.Below,
				QuickAccessToolBarMode = RibbonQuickAccessToolBarMode.Visible,
				QuickAccessToolBar = new RibbonQuickAccessToolBarViewModel() {
					CommonItems = {
						_mvvmControls[BarControlKeys.Cut],
						_mvvmControls[BarControlKeys.Copy],
						_mvvmControls[BarControlKeys.Paste],
					},
					Items = {
						_mvvmControls[BarControlKeys.Cut],
						_mvvmControls[BarControlKeys.Copy],
						_mvvmControls[BarControlKeys.Paste],
					},
				},
				Tabs = {
					new RibbonTabViewModel("Home") {
						Groups = {

							new RibbonGroupViewModel("Serialization") {
								Items = {
									new RibbonControlGroupViewModel() {
										ItemVariantBehavior = ItemVariantBehavior.AlwaysLarge,
										SeparatorMode = RibbonControlGroupSeparatorMode.Always,
										Items = {
											_mvvmControls[BarControlKeys.SaveLayout],
											_mvvmControls[BarControlKeys.RestoreLayout],
										}
									},
									_mvvmControls[BarControlKeys.RestoreOriginal],
								},
							},

						}
					},
				}
			};
		}

		private void OnQuickAccessToolBarItemAdding(object? sender, RibbonQuickAccessToolBarItemAddingEventArgs e) {
			// This event is raised when an item is being added to the Quick Access Toolbar.
			// The event data defines the Key of the item being added and, if found, the Item
			// that will be added. If the Ribbon is unable to automatically locate an item with
			// the desired key, the Item property will be NULL. When this happens, you can manually
			// assign an Item that corresponds to the given Key. If Item is NULL or Cancel is set
			// to TRUE then nothing will be added.
			//
			// This event can also be used to notify a user if an attempt was made to add an item
			// to the Quick Access Toolbar that might no longer be available.
			if (!e.Cancel) {
				Debug.WriteLine($"Adding QAT Item... Key={e.Key}; Item={e.Item?.ToString() ?? "NULL"}");

				// Attempt to resolve any unspecified items by looking up the control by the key.
				// In more advanced scenarios, a BarManager instance might be used to managed all the controls.
				if ((e.Item is null) && _mvvmControls.TryGetValue(e.Key, out var item)) {
					e.Item = item;
				}

				if (e.Item is null) {
					MessageBox.Show($"Unable to restore the Quick Access Toolbar item '{e.Key}' because the corresponding command could not be found.", "Command Not Found", MessageBoxButtons.OK, MessageBoxImage.Warning);
				}
			}
		}

		/// <summary>
		/// Tries to restore the specified layout data to the Ribbon.
		/// </summary>
		/// <param name="layout">The previously serialized layout data.</param>
		/// <returns><c>true</c> if the layout was successfully restored; otherwise <c>false</c>.</returns>
		private bool TryRestoreLayout(Ribbon ribbon, string layout) {
			try {
				// Initialize the options that will be supported during restore based on current settings
				var options = this.OptionsModel.Options;

				// Deserialize the layout to the Ribbon
				var serializer = new RibbonSerializer();
				serializer.Deserialize(ribbon, layout, options);

				// Indicate success
				return true;
			}
			catch (Exception ex) {
				// Exception during the deserialization process
				Debug.WriteLine(ex);
				_ = UserPromptBuilder.Configure().ForException(ex, "Error restoring layout.").Show();

				// Indicate failure
				return false;
			}
		}

		/// <summary>
		/// Tries to save the current Ribbon layout.
		/// </summary>
		/// <param name="layout">When successful, outputs the layout data.</param>
		/// <returns><c>true</c> if the layout was successfully saved; otherwise <c>false</c>.</returns>
		private bool TrySaveLayout(Ribbon ribbon, out string? layout) {
			try {
				// Initialize the options that will be supported during restore based on current settings
				var options = this.OptionsModel.Options;

				// Serialize the layout from the Ribbon
				var serializer = new RibbonSerializer();
				layout = serializer.Serialize(ribbon, options);

				// Indicate success
				return true;
			}
			catch (Exception ex) {
				// Exception during the serialization process
				Debug.WriteLine(ex);
				_ = UserPromptBuilder.Configure().ForException(ex, "Error saving layout.").Show();

				// Indicate failure
				layout = null;
				return false;
			}
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// The current layout data for the MVVM sample.
		/// </summary>
		/// <value>An XML-formatted string.</value>
		public string? CurrentMvvmLayout {
			get => mvvmEditor.Text;
			set => mvvmEditor.Text = value;
		}

		/// <summary>
		/// The current layout data for the XAML sample.
		/// </summary>
		/// <value>An XML-formatted string.</value>
		public string? CurrentXamlLayout {
			get => xamlEditor.Text;
			set => xamlEditor.Text = value;
		}

		/// <summary>
		/// Gets the command to restore the configured layout for the MVVM sample.
		/// </summary>
		public ICommand MvvmRestoreLayoutCommand {
			get {
				if (_mvvmRestoreLayoutCommand is null) {
					_mvvmRestoreLayoutCommand = new DelegateCommand<object>(param => {
						var currentLayout = this.CurrentMvvmLayout;
						if (string.IsNullOrEmpty(currentLayout)) {
							MessageBox.Show("The current layout is undefined and cannot be restored.", "Restore", MessageBoxButtons.OK, MessageBoxImage.Error);
							return;
						}

						// Attempt to restore the current layout
						TryRestoreLayout(mvvmRibbon, currentLayout);
					});
				}
				return _mvvmRestoreLayoutCommand;
			}
		}

		/// <summary>
		/// Gets the command to restore the original layout for the MVVM sample.
		/// </summary>
		public ICommand MvvmRestoreOriginalCommand {
			get {
				if (_mvvmRestoreOriginalCommand is null) {
					_mvvmRestoreOriginalCommand = new DelegateCommand<object>(param => {
						if (_mvvmOriginalLayout is null) {
							MessageBox.Show("The original layout is undefined and cannot be restored.", "Restore", MessageBoxButtons.OK, MessageBoxImage.Error);
							return;
						}

						// Attempt to restore the original layout
						if (TryRestoreLayout(mvvmRibbon, _mvvmOriginalLayout))
							CurrentMvvmLayout = _mvvmOriginalLayout;
					});
				}
				return _mvvmRestoreOriginalCommand;
			}
		}

		/// <summary>
		/// Gets the command to save the current layout for the MVVM sample.
		/// </summary>
		public ICommand MvvmSaveLayoutCommand {
			get {
				if (_mvvmSaveLayoutCommand is null) {
					_mvvmSaveLayoutCommand = new DelegateCommand<object>(param => {
						// Attempt to save the current layout
						if (TrySaveLayout(mvvmRibbon, out var layout))
							this.CurrentMvvmLayout = layout;
					});
				}
				return _mvvmSaveLayoutCommand;
			}
		}

		/// <summary>
		/// The view model for controlling which options are included when serializing and deserialization.
		/// </summary>
		public SerializerOptionsViewModel OptionsModel { get; } = new();

		/// <summary>
		/// The ribbon view model for the MVVM sample.
		/// </summary>
		public RibbonViewModel RibbonViewModel
			=> _ribbonViewModel ??= CreateRibbonViewModel();

		/// <summary>
		/// Gets the command to restore the configured layout for the XAML sample.
		/// </summary>
		public ICommand XamlRestoreLayoutCommand {
			get {
				if (_xamlRestoreLayoutCommand is null) {
					_xamlRestoreLayoutCommand = new DelegateCommand<object>(param => {
						var currentLayout = this.CurrentXamlLayout;
						if (string.IsNullOrEmpty(currentLayout)) {
							MessageBox.Show("The current layout is undefined and cannot be restored.", "Restore", MessageBoxButtons.OK, MessageBoxImage.Error);
							return;
						}

						// Attempt to restore the current layout
						TryRestoreLayout(xamlRibbon, currentLayout);
					});
				}
				return _xamlRestoreLayoutCommand;
			}
		}

		/// <summary>
		/// Gets the command to restore the original layout for the XAML sample.
		/// </summary>
		public ICommand XamlRestoreOriginalCommand {
			get {
				if (_xamlRestoreOriginalCommand is null) {
					_xamlRestoreOriginalCommand = new DelegateCommand<object>(param => {
						if (_xamlOriginalLayout is null) {
							MessageBox.Show("The original layout is undefined and cannot be restored.", "Restore", MessageBoxButtons.OK, MessageBoxImage.Error);
							return;
						}

						// Attempt to restore the original layout
						if (TryRestoreLayout(xamlRibbon, _xamlOriginalLayout))
							CurrentXamlLayout = _xamlOriginalLayout;
					});
				}
				return _xamlRestoreOriginalCommand;
			}
		}

		/// <summary>
		/// Gets the command to save the current layout for the XAML sample.
		/// </summary>
		public ICommand XamlSaveLayoutCommand {
			get {
				if (_xamlSaveLayoutCommand is null) {
					_xamlSaveLayoutCommand = new DelegateCommand<object>(param => {
						// Attempt to save the current layout
						if (TrySaveLayout(xamlRibbon, out var layout))
							this.CurrentXamlLayout = layout;
					});
				}
				return _xamlSaveLayoutCommand;
			}
		}


	}

}
