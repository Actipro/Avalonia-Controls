using Avalonia;
using Avalonia.Animation;
using Avalonia.Media;
using Avalonia.Styling;
using System.Collections.Generic;
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

		public override async Task Start(Visual? from, Visual? to, bool forward, CancellationToken cancellationToken) {
			// Update the direction based on the property
			forward = UseForwardDirection;

			// The rest of this method is a copy of the base class implementation except that FillMode is set to ensure the "from" visual
			//   doesn't arrange again at its original location after the animation occurs, which was allowing adornments to blink there
			//   before everything was removed from the visual tree

			if (cancellationToken.IsCancellationRequested)
				return;

			var tasks = new List<Task>();
			var parent = GetVisualParent(from, to);
			var distance = Orientation == SlideAxis.Horizontal ? parent.Bounds.Width : parent.Bounds.Height;
			var translateProperty = Orientation == SlideAxis.Horizontal ? TranslateTransform.XProperty : TranslateTransform.YProperty;

			if (from != null) {
				var animation = new Animation {
					Easing = SlideOutEasing,
					FillMode = FillMode.Forward,
					Children = {
						new KeyFrame {
							Setters = {
								new Setter {
									Property = translateProperty,
									Value = 0d
								}
							},
							Cue = new Cue(0d)
						},
						new KeyFrame {
							Setters = {
								new Setter {
									Property = translateProperty,
									Value = forward ? -distance : distance
								},
							},
							Cue = new Cue(1d)
						}
					},
					Duration = Duration
				};
				tasks.Add(animation.RunAsync(from, cancellationToken));
			}

			if (to != null) {
				to.IsVisible = true;
				var animation = new Animation {
					Easing = SlideInEasing,
					FillMode = FillMode.Forward,
					Children = {
						new KeyFrame {
							Setters = {
								new Setter
								{
									Property = translateProperty,
									Value = forward ? distance : -distance
								}
							},
							Cue = new Cue(0d)
						},
						new KeyFrame
						{
							Setters = {
								new Setter {
									Property = translateProperty, 
									Value = 0d 
								}
							},
							Cue = new Cue(1d)
						}
					},
					Duration = Duration
				};
				tasks.Add(animation.RunAsync(to, cancellationToken));
			}

			await Task.WhenAll(tasks);

			if (from != null && !cancellationToken.IsCancellationRequested)
				from.IsVisible = false;
		}

		public bool UseForwardDirection { get; set; } = true;

	}

}
