using Android.Graphics.Drawables;
using Android.Widget;
using Xamarin.Forms;
using ASwitch = Android.Widget.Switch;

namespace Xamarin.Platform.Handlers
{
	public partial class SwitchHandler : AbstractViewHandler<ISwitch, ASwitch>
	{
		Drawable _defaultTrackDrawable;
		bool _changedThumbColor;
		OnListener _onListener;

		protected override ASwitch CreateView()
		{
			_onListener = new OnListener(this);

			var aSwitch = new ASwitch(Context);

			aSwitch.SetOnCheckedChangeListener(_onListener);

			return aSwitch;
		}

		protected override void SetupDefaults()
		{
			_defaultTrackDrawable = TypedNativeView.TrackDrawable;
			base.SetupDefaults();
		}

		protected override void DisposeView(ASwitch nativeView)
		{
			if (_onListener != null)
			{
				nativeView.SetOnCheckedChangeListener(null);
				_onListener = null;
			}

			_defaultTrackDrawable?.Dispose();
			_defaultTrackDrawable = null;

			base.DisposeView(nativeView);
		}

		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			SizeRequest sizeConstraint = base.GetDesiredSize(widthConstraint, heightConstraint);

			if (sizeConstraint.Request.Width == 0)
			{
				int width = (int)widthConstraint;
				if (widthConstraint <= 0)
					width = (int)Context.GetThemeAttributeDp(global::Android.Resource.Attribute.SwitchMinWidth);

				sizeConstraint = new SizeRequest(new Size(width, sizeConstraint.Request.Height), new Size(width, sizeConstraint.Minimum.Height));
			}

			return sizeConstraint;
		}

		public static void MapIsToggled(IViewHandler handler, ISwitch view)
		{
			(handler as SwitchHandler)?.UpdateIsToggled();
		}

		public static void MapOnColor(IViewHandler handler, ISwitch view)
		{
			(handler as SwitchHandler)?.UpdateOnColor();
		}

		public static void MapThumbColor(IViewHandler handler, ISwitch view)
		{
			(handler as SwitchHandler)?.UpdateThumbColor();
		}

		void UpdateIsToggled()
		{
			TypedNativeView.Checked = VirtualView.IsToggled;
		}

		internal void UpdateIsToggled(bool isChecked)
		{
			VirtualView.IsToggled = isChecked;
			VirtualView.Toggled();
		}

		internal void UpdateOnColor()
		{
			if (TypedNativeView.Checked)
			{
				var onColor = VirtualView.OnColor;

				if (onColor.IsDefault)
				{
					TypedNativeView.TrackDrawable = _defaultTrackDrawable;
				}
				else
				{
					TypedNativeView.TrackDrawable?.SetColorFilter(onColor.ToNative(), FilterMode.Multiply);
				}
			}
			else
			{
				TypedNativeView.TrackDrawable?.ClearColorFilter();
			}
		}

		void UpdateThumbColor()
		{
			var thumbColor = VirtualView.ThumbColor;

			if (!thumbColor.IsDefault)
			{
				TypedNativeView.ThumbDrawable.SetColorFilter(thumbColor, FilterMode.Multiply);
				_changedThumbColor = true;
			}
			else
			{
				if (_changedThumbColor)
				{
					TypedNativeView.ThumbDrawable?.ClearColorFilter();
					_changedThumbColor = false;
				}
			}

			TypedNativeView.ThumbDrawable.SetColorFilter(thumbColor, FilterMode.Multiply);
		}
	}

	class OnListener : Java.Lang.Object, CompoundButton.IOnCheckedChangeListener
	{
        readonly SwitchHandler _switchHandler;

		public OnListener(SwitchHandler switchHandler)
		{
			_switchHandler = switchHandler;
		}

		void CompoundButton.IOnCheckedChangeListener.OnCheckedChanged(CompoundButton buttonView, bool isToggled)
		{
			_switchHandler.UpdateIsToggled(isToggled);
			_switchHandler.UpdateOnColor();
		}
	}
}
