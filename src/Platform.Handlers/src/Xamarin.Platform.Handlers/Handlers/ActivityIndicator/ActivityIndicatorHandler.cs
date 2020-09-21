namespace Xamarin.Platform.Handlers
{
	public partial class ActivityIndicatorHandler
	{
		public static PropertyMapper<IActivityIndicator> ActivityIndicatorMapper = new PropertyMapper<IActivityIndicator>(ViewHandler.ViewMapper)
		{
			[nameof(IActivityIndicator.Color)] = MapColor,
			[nameof(IActivityIndicator.IsRunning)] = MapIsRunning
		};

		public ActivityIndicatorHandler() : base(ActivityIndicatorMapper)
		{

		}

		public ActivityIndicatorHandler(PropertyMapper mapper) : base(mapper ?? ActivityIndicatorMapper)
		{

		}
	}
}