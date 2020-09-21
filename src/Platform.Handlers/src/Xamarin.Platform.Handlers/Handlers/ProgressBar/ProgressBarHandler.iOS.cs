using UIKit;
using Xamarin.Forms;

namespace Xamarin.Platform.Handlers
{
	public partial class ProgressBarHandler : AbstractViewHandler<IProgress, UIProgressView>
	{
		protected override UIProgressView CreateView()
		{
			return new UIProgressView(UIProgressViewStyle.Default);
		}

		public static void MapProgress(IViewHandler handler, IProgress progressBar)
		{
			(handler as ProgressBarHandler)?.UpdateProgress();
		}

		public static void MapProgressColor(IViewHandler handler, IProgress progressBar)
		{
			(handler as ProgressBarHandler)?.UpdateProgressColor();
		}

		void UpdateProgress()
		{
			TypedNativeView.Progress = (float)VirtualView.Progress;
		}

		void UpdateProgressColor()
		{
			TypedNativeView.ProgressTintColor = VirtualView.ProgressColor == Color.Default ? null : VirtualView.ProgressColor.ToNative();
		}
	}
}