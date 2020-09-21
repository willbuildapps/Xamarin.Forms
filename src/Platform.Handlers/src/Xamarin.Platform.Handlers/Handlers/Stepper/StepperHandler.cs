namespace Xamarin.Platform.Handlers
{
	public partial class StepperHandler
	{
		public static PropertyMapper<IStepper> StepperMapper = new PropertyMapper<IStepper>(ViewHandler.ViewMapper)
		{
			[nameof(IStepper.Minimum)] = MapMinimum,
			[nameof(IStepper.Maximum)] = MapMaximum,
			[nameof(IStepper.Increment)] = MapIncrement,
			[nameof(IStepper.Value)] = MapValue
#if __ANDROID__ || NETCOREAPP
			,[nameof(IStepper.IsEnabled)] = MapIsEnabled
#endif
		};

		public StepperHandler() : base(StepperMapper)
		{

		}

		public StepperHandler(PropertyMapper mapper) : base(mapper ?? StepperMapper)
		{

		}
	}
}