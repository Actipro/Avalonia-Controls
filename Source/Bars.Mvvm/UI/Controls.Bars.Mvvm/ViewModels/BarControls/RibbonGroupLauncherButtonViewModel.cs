﻿using System.Windows.Input;

namespace ActiproSoftware.UI.Avalonia.Controls.Bars.Mvvm {

	/// <summary>
	/// Represents a view model for a launcher button control within a ribbon group.
	/// </summary>
	public class RibbonGroupLauncherButtonViewModel : BarKeyedObjectViewModelBase {

		private ICommand? _command;
		private object? _commandParameter;
		private string? _description;
		private bool _isVisible = true;
		private string? _keyTipText;
		private string? _label;
		private object? _smallIcon;
		private string? _title;

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel()"/>
		public RibbonGroupLauncherButtonViewModel()  // Parameterless constructor required for XAML support
			: this(key: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string)"/>
		public RibbonGroupLauncherButtonViewModel(string? key)
			: this(key, label: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string)"/>
		public RibbonGroupLauncherButtonViewModel(string? key, string? label)
			: this(key, label, keyTipText: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string)"/>
		public RibbonGroupLauncherButtonViewModel(string? key, string? label, string? keyTipText)
			: this(key, label, keyTipText, command: null) { }

		/// <inheritdoc cref="BarButtonViewModel(string, ICommand)"/>
		public RibbonGroupLauncherButtonViewModel(string? key, ICommand? command)
			: this(key, label: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, ICommand)"/>
		public RibbonGroupLauncherButtonViewModel(string? key, string? label, ICommand? command)
			: this(key, label, keyTipText: null, command) { }

		/// <inheritdoc cref="BarButtonViewModel(string, string, string, ICommand)"/>
		public RibbonGroupLauncherButtonViewModel(string? key, string? label, string? keyTipText, ICommand? command)
			: base(key) {

			_label = label ?? BarControlService.LabelGenerator.FromCommand(command) ?? BarControlService.LabelGenerator.FromKey(key);
			_keyTipText = keyTipText ?? BarControlService.KeyTipTextGenerator.FromCommand(command) ?? BarControlService.KeyTipTextGenerator.FromLabel(this._label);
			_command = command;
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		/// <inheritdoc cref="BarButtonViewModel.Command"/>
		public ICommand? Command {
			get => _command;
			set => SetProperty(ref _command, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.CommandParameter"/>
		public object? CommandParameter {
			get => _commandParameter;
			set => SetProperty(ref _commandParameter, value);
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Description"/>
		public string? Description {
			get => _description;
			set => SetProperty(ref _description, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.IsVisible"/>
		public bool IsVisible {
			get => _isVisible;
			set => SetProperty(ref _isVisible, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.KeyTipText"/>
		public string? KeyTipText {
			get => _keyTipText;
			set => SetProperty(ref _keyTipText, value);
		}

		/// <inheritdoc cref="BarButtonViewModel.Label"/>
		public string? Label {
			get => _label;
			set => SetProperty(ref _label, value);
		}
		
		/// <inheritdoc cref="BarButtonViewModel.SmallIcon"/>
		public object? SmallIcon {
			get => _smallIcon;
			set => SetProperty(ref _smallIcon, value);
		}
		
		/// <inheritdoc cref="BarButtonViewModel.Title"/>
		public string? Title {
			get => _title;
			set => SetProperty(ref _title, value);
		}

	}

}