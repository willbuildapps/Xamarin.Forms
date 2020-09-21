using System;
using System.Drawing;
using UIKit;

namespace Xamarin.Platform.Handlers
{
	public partial class SwitchHandler : AbstractViewHandler<ISwitch, UISwitch>
	{
		UIColor _defaultOnColor;
		UIColor _defaultThumbColor;

		protected override UISwitch CreateView()
		{
			var uISwitch = new UISwitch(RectangleF.Empty);
			uISwitch.ValueChanged += OnControlValueChanged;
			return uISwitch;
		}

		protected override void SetupDefaults()
		{
			_defaultOnColor = UISwitch.Appearance.OnTintColor;
			_defaultThumbColor = UISwitch.Appearance.ThumbTintColor;
			base.SetupDefaults();
		}

		protected override void DisposeView(UISwitch uISwitch)
		{
			uISwitch.ValueChanged -= OnControlValueChanged;
			base.DisposeView(uISwitch);
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
			TypedNativeView.SetState(VirtualView.IsToggled, true);
		}

		void UpdateOnColor()
		{
			if (VirtualView != null)
			{
				if (VirtualView.OnColor == Forms.Color.Default)
					TypedNativeView.OnTintColor = _defaultOnColor;
				else
					TypedNativeView.OnTintColor = VirtualView.OnColor.ToNative();
			}
		}

		void UpdateThumbColor()
		{
			if (VirtualView == null)
				return;

			Forms.Color thumbColor = VirtualView.ThumbColor;
			TypedNativeView.ThumbTintColor = thumbColor.IsDefault ? _defaultThumbColor : thumbColor.ToNative();
		}

		void OnControlValueChanged(object sender, EventArgs e)
		{
			VirtualView.IsToggled = TypedNativeView.On;
			VirtualView.Toggled();
		}
	}
}
