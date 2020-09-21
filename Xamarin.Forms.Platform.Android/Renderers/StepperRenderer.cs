using System;
using System.ComponentModel;
using Android.Content;
using Android.Widget;
using Android.Views;
using Xamarin.Platform;
using AButton = Android.Widget.Button;
using AOrientation = Android.Widget.Orientation;

namespace Xamarin.Forms.Platform.Android
{
	public class StepperRenderer : ViewRenderer<Stepper, LinearLayout>, IStepperRenderer
	{
		[PortHandler]
		AButton _downButton;
		AButton _upButton;

		public StepperRenderer(Context context) : base(context)
		{
			AutoPackage = false;
		}

		[Obsolete("This constructor is obsolete as of version 2.5. Please use StepperRenderer(Context) instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public StepperRenderer()
		{
			AutoPackage = false;
		}

		[PortHandler]
		protected override LinearLayout CreateNativeControl()
		{
			return new LinearLayout(Context)
			{
				Orientation = AOrientation.Horizontal,
				Focusable = true,
				DescendantFocusability = DescendantFocusability.AfterDescendants
			};
		}

		[PortHandler]
		protected override void OnElementChanged(ElementChangedEventArgs<Stepper> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				var layout = CreateNativeControl();
				StepperRendererManager.CreateStepperButtons(this, out _downButton, out _upButton);
				layout.AddView(_downButton, new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.MatchParent));
				layout.AddView(_upButton, new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.MatchParent));
				SetNativeControl(layout);
			}

			StepperRendererManager.UpdateButtons(this, _downButton, _upButton);
		}

		[PortHandler]
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.IsDisposed())
			{
				return;
			}

			base.OnElementPropertyChanged(sender, e);

			StepperRendererManager.UpdateButtons(this, _downButton, _upButton, e);
		}

		[PortHandler]
		Stepper IStepperRenderer.Element => Element;

		[PortHandler]
		AButton IStepperRenderer.UpButton => _upButton;

		[PortHandler]
		AButton IStepperRenderer.DownButton => _downButton;

		[PortHandler]
		AButton IStepperRenderer.CreateButton()
		{
			var button = new AButton(Context);
			button.SetHeight((int)Context.ToPixels(10.0));
			return button;
		}
	}
}
