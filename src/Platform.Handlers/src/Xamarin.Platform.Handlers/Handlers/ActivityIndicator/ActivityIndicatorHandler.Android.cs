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
			if (!(handler.NativeView is AProgressBar aProgressBar))
				return;

			aProgressBar.Visibility = activityIndicator.IsRunning ? ViewStates.Visible : ViewStates.Invisible;
		}

		public static void MapColor(IViewHandler handler, IActivityIndicator activityIndicator)
		{
			if (!(handler.NativeView is AProgressBar aProgressBar))
				return;

			Color color = activityIndicator.Color;

			if (!color.IsDefault)
				aProgressBar.IndeterminateDrawable?.SetColorFilter(color.ToNative(), FilterMode.SrcIn);
			else
				aProgressBar.IndeterminateDrawable?.ClearColorFilter();
		}
	}
}