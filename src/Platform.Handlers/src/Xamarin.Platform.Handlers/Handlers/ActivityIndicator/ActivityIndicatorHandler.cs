#if __IOS__
using CoreGraphics;
using UIKit;
using NativeView = UIKit.UIActivityIndicatorView;
#elif MONOANDROID
using NativeView = Android.Widget.ProgressBar;
#elif NETSTANDARD
using NativeView = System.Object;
#endif

namespace Xamarin.Platform.Handlers
{
	public partial class ActivityIndicatorHandler : AbstractViewHandler<IActivityIndicator, NativeView>
	{
		public static PropertyMapper<IActivityIndicator, ActivityIndicatorHandler> ActivityIndicatorMapper = new PropertyMapper<IActivityIndicator, ActivityIndicatorHandler>(ViewHandler.ViewMapper)
		{
			[nameof(IActivityIndicator.IsRunning)] = MapIsRunning,
			[nameof(IActivityIndicator.Color)] = MapColor
		};

		public static void MapIsRunning(ActivityIndicatorHandler handler, IActivityIndicator activityIndicator)
		{
			handler.TypedNativeView.UpdateIsRunning(activityIndicator);
		}

		public static void MapColor(ActivityIndicatorHandler handler, IActivityIndicator activityIndicator)
		{
			handler.TypedNativeView.UpdateColor(activityIndicator);
		}

#if MONOANDROID
		protected override NativeView CreateView() => new NativeView(this.Context) { Indeterminate = true };
#elif __IOS__
		protected override NativeView CreateView() => new NativeView(CGRect.Empty) { ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray };
#else
		protected override NativeView CreateView() => new NativeView();
#endif

		public ActivityIndicatorHandler() : base(ActivityIndicatorMapper)
		{

		}

		public ActivityIndicatorHandler(PropertyMapper mapper) : base(mapper ?? ActivityIndicatorMapper)
		{

		}
	}
}