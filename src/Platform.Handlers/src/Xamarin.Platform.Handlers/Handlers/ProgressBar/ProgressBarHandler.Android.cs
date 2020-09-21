using Android.Content.Res;
using Android.OS;
using Xamarin.Forms;
using static Android.Resource;
using AProgressBar = Android.Widget.ProgressBar;

namespace Xamarin.Platform.Handlers
{
	public partial class ProgressBarHandler : AbstractViewHandler<IProgress, AProgressBar>
	{
		protected override AProgressBar CreateView()
		{
			return new AProgressBar(Context, null, Attribute.ProgressBarStyleHorizontal) { Indeterminate = false, Max = 10000 };
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
			TypedNativeView.Progress = (int)(VirtualView.Progress * 10000);
		}

		void UpdateProgressColor()
		{
			if (VirtualView == null || TypedNativeView == null)
				return;

			Forms.Color color = VirtualView.ProgressColor;

			if (color.IsDefault)
			{
				(TypedNativeView.Indeterminate ? TypedNativeView.IndeterminateDrawable :
					TypedNativeView.ProgressDrawable).ClearColorFilter();
			}
			else
			{
				if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
				{
					(TypedNativeView.Indeterminate ? TypedNativeView.IndeterminateDrawable :
						TypedNativeView.ProgressDrawable).SetColorFilter(color, FilterMode.SrcIn);
				}
				else
				{
					var tintList = ColorStateList.ValueOf(color.ToNative());
					if (TypedNativeView.Indeterminate)
						TypedNativeView.IndeterminateTintList = tintList;
					else
						TypedNativeView.ProgressTintList = tintList;
				}
			}
		}
	}
}