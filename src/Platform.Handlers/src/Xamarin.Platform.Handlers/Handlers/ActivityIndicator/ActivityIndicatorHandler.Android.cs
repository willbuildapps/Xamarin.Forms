using Android.Views;
using Xamarin.Forms;
using AProgressBar = Android.Widget.ProgressBar;

namespace Xamarin.Platform.Handlers
{
	public partial class ActivityIndicatorHandler : AbstractViewHandler<IActivityIndicator, AProgressBar>
	{
		protected override AProgressBar CreateView()
		{
			return new AProgressBar(Context) { Indeterminate = true };
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
			if (VirtualView == null || TypedNativeView == null)
				return;

			TypedNativeView.Visibility = VirtualView.IsRunning ? ViewStates.Visible : ViewStates.Invisible;
		}

		void UpdateColor()
		{
			if (VirtualView == null || TypedNativeView == null)
				return;

			Color color = VirtualView.Color;

			if (!color.IsDefault)
				TypedNativeView.IndeterminateDrawable?.SetColorFilter(color.ToNative(), FilterMode.SrcIn);
			else
				TypedNativeView.IndeterminateDrawable?.ClearColorFilter();
		}
	}
}