using CoreGraphics;
using UIKit;
using Xamarin.Forms;

namespace Xamarin.Platform.Handlers
{
	public partial class ActivityIndicatorHandler : AbstractViewHandler<IActivityIndicator, UIActivityIndicatorView>
	{
		protected override UIActivityIndicatorView CreateView()
		{
#if __XCODE11__
			if(NativeVersion.Supports(NativeApi.UIActivityIndicatorViewStyleMedium))
				return new UIActivityIndicatorView(CGRect.Empty) { ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Medium };
			else
#endif
			return new UIActivityIndicatorView(CGRect.Empty) { ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray };
		}

		public static void MapIsRunning(IViewHandler handler, IActivityIndicator activityIndicator)
		{
			if (!(handler.NativeView is UIActivityIndicatorView uIActivityIndicatorView))
				return;

			if (activityIndicator.IsRunning)
				uIActivityIndicatorView.StartAnimating();
			else
				uIActivityIndicatorView.StopAnimating();
		}

		public static void MapColor(IViewHandler handler, IActivityIndicator activityIndicator)
		{
			if (!(handler.NativeView is UIActivityIndicatorView uIActivityIndicatorView))
				return;

			uIActivityIndicatorView.Color = activityIndicator.Color == Color.Default ? null : activityIndicator.Color.ToNative();
		}
	}
}