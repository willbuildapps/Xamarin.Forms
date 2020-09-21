using System;

namespace Xamarin.Platform.Handlers
{
	public partial class StepperHandler : AbstractViewHandler<IStepper, object>
	{
		protected override object CreateView() => throw new NotImplementedException();

		public static void MapMinimum(IViewHandler handler, IStepper slider) { }
		public static void MapMaximum(IViewHandler handler, IStepper slider) { }
		public static void MapIncrement(IViewHandler handler, IStepper slider) { }
		public static void MapValue(IViewHandler handler, IStepper slider) { }
	}
}