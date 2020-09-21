using Android.Widget;
using Android.Views;
using AButton = Android.Widget.Button;
using AOrientation = Android.Widget.Orientation;

namespace Xamarin.Platform.Handlers
{
	public partial class StepperHandler : AbstractViewHandler<IStepper, LinearLayout> , IStepperHandler
	{
		AButton _downButton;
		AButton _upButton;

		IStepper IStepperHandler.Element => VirtualView;

		AButton IStepperHandler.UpButton => _upButton;

		AButton IStepperHandler.DownButton => _downButton;

		protected override LinearLayout CreateView()
		{
			var aStepper = new LinearLayout(Context)
			{
				Orientation = AOrientation.Horizontal,
				Focusable = true,
				DescendantFocusability = DescendantFocusability.AfterDescendants
			};

			StepperHandlerManager.CreateStepperButtons(this, out _downButton, out _upButton);
			aStepper.AddView(_downButton, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent));
			aStepper.AddView(_upButton, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent));

			return aStepper;
		}

		public static void MapMinimum(IViewHandler handler, IStepper slider) => (handler as StepperHandler)?.UpdateButtons();

		public static void MapMaximum(IViewHandler handler, IStepper slider) => (handler as StepperHandler)?.UpdateButtons();

		public static void MapIncrement(IViewHandler handler, IStepper slider) => (handler as StepperHandler)?.UpdateButtons();

		public static void MapValue(IViewHandler handler, IStepper slider) =>  (handler as StepperHandler)?.UpdateButtons();

		public static void MapIsEnabled(IViewHandler handler, IStepper slider)
		{
			ViewHandler.MapPropertyIsEnabled(handler, slider);
			(handler as StepperHandler)?.UpdateButtons();
		}

		public virtual void UpdateButtons()
		{
			StepperHandlerManager.UpdateButtons(this, _downButton, _upButton);
		}

		AButton IStepperHandler.CreateButton()
		{
			var button = new AButton(Context);

			return button;
		}
	}
}