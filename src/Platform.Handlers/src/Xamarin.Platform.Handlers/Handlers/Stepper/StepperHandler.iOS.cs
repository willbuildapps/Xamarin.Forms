using System;
using System.Drawing;
using UIKit;

namespace Xamarin.Platform.Handlers
{
	public partial class StepperHandler : AbstractViewHandler<IStepper, UIStepper>
	{
		protected override UIStepper CreateView()
		{
			var uIStepper = new UIStepper(RectangleF.Empty);
			uIStepper.ValueChanged += OnValueChanged;
			return uIStepper;
		}

		protected override void DisposeView(UIStepper uIStepper)
		{
			uIStepper.ValueChanged -= OnValueChanged;
			base.DisposeView(uIStepper);
		}

		public static void MapMinimum(IViewHandler handler, IStepper slider) => (handler as StepperHandler)?.UpdateMinimum();

		public static void MapMaximum(IViewHandler handler, IStepper slider) => (handler as StepperHandler)?.UpdateMaximum();

		public static void MapIncrement(IViewHandler handler, IStepper slider) => (handler as StepperHandler)?.UpdateIncrement();

		public static void MapValue(IViewHandler handler, IStepper slider) => (handler as StepperHandler)?.UpdateValue();

		void OnValueChanged(object sender, EventArgs e)
			=> VirtualView.Value = TypedNativeView.Value;

		void UpdateIncrement()
		{
			TypedNativeView.StepValue = VirtualView.Increment;
		}

		void UpdateMaximum()
		{
			TypedNativeView.MaximumValue = VirtualView.Maximum;
		}

		void UpdateMinimum()
		{
			TypedNativeView.MinimumValue = VirtualView.Minimum;
		}

		void UpdateValue()
		{
			if (TypedNativeView.Value != VirtualView.Value)
				TypedNativeView.Value = VirtualView.Value;
		}
	}
}