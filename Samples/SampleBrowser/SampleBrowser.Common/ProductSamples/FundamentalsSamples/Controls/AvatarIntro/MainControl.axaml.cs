using ActiproSoftware.UI.Avalonia.Controls;
using ActiproSoftware.UI.Avalonia.Input;
using Avalonia.Controls;

namespace ActiproSoftware.ProductSamples.FundamentalsSamples.Controls.AvatarIntro {

	public partial class MainControl : UserControl {

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// OBJECT
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public MainControl() {
			InitializeComponent();
		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		
		public DelegateCommand<object> AvatarClickedCommand { get; }
			= new DelegateCommand<object>(p => MessageBox.Show($"The avatar for '{p}' was clicked.", "Avatar Click", MessageBoxButtons.OK, MessageBoxImage.Information));

		public DelegateCommand<object> AvatarGroupClickedCommand { get; }
			= new DelegateCommand<object>(p => MessageBox.Show($"The avatar group was clicked.", "Avatar Group Click", MessageBoxButtons.OK, MessageBoxImage.Information));

	}

}
