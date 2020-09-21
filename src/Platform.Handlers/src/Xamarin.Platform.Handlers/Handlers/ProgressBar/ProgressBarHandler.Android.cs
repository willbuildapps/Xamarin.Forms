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
			if (!(handler.NativeView is AProgressBar aProgressBar))
				return;

			aProgressBar.Progress = (int)(progressBar.Progress * 10000);
		}

		public static void MapProgressColor(IViewHandler handler, IProgress progressBar)
		{
			if (!(handler.NativeView is AProgressBar aProgressBar))
				return;

			Forms.Color color = progressBar.ProgressColor;

			if (color.IsDefault)
			{
				(aProgressBar.Indeterminate ? aProgressBar.IndeterminateDrawable :
					aProgressBar.ProgressDrawable).ClearColorFilter();
			}
			else
			{
				if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
				{
					(aProgressBar.Indeterminate ? aProgressBar.IndeterminateDrawable :
						aProgressBar.ProgressDrawable).SetColorFilter(color, FilterMode.SrcIn);
				}
				else
				{
					var tintList = ColorStateList.ValueOf(color.ToNative());
					if (aProgressBar.Indeterminate)
						aProgressBar.IndeterminateTintList = tintList;
					else
						aProgressBar.ProgressTintList = tintList;
				}
			}
		}
	}
}