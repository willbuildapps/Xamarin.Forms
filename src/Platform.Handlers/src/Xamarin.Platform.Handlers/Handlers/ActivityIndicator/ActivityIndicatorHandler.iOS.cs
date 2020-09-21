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
			(handler as ActivityIndicatorHandler)?.UpdateIsRunning();
		}

		public static void MapColor(IViewHandler handler, IActivityIndicator activityIndicator)
		{
			(handler as ActivityIndicatorHandler)?.UpdateColor();
		}

		void UpdateIsRunning()
		{
			if (TypedNativeView?.Superview == null)
				return;

			if (VirtualView.IsRunning)
				TypedNativeView.StartAnimating();
			else
				TypedNativeView.StopAnimating();
		}

		void UpdateColor()
		{
			TypedNativeView.Color = VirtualView.Color == Color.Default ? null : VirtualView.Color.ToUIColor();
		}
	}
}