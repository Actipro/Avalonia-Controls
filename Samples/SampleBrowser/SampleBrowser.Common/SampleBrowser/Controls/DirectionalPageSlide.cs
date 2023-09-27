using Avalonia;
using Avalonia.Animation;
using System.Threading;
using System.Threading.Tasks;

namespace ActiproSoftware.SampleBrowser {

	/// <summary>
	/// Implements a <see cref="PageSlide"/> transition that can change direction.
	/// </summary>
	public class DirectionalPageSlide : PageSlide {
		
		/////////////////////////////////////////////////////////////////////////////////////////////////////
		// PUBLIC PROCEDURES
		/////////////////////////////////////////////////////////////////////////////////////////////////////

		public override Task Start(Visual? from, Visual? to, bool forward, CancellationToken cancellationToken)
			=> base.Start(from, to, UseForwardDirection, cancellationToken);

		public bool UseForwardDirection { get; set; } = true;

	}

}
