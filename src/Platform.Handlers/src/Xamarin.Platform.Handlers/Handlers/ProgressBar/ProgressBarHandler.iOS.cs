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
			if (!(handler.NativeView is UIProgressView uIProgressView))
				return;

			uIProgressView.Progress = (float)progressBar.Progress;
		}

		public static void MapProgressColor(IViewHandler handler, IProgress progressBar)
		{
			if (!(handler.NativeView is UIProgressView uIProgressView))
				return;

			uIProgressView.ProgressTintColor = progressBar.ProgressColor == Color.Default ? null : progressBar.ProgressColor.ToNative();
		}
	}
}