namespace Xamarin.Platform.Handlers
{
	public partial class ProgressBarHandler
	{
		public static PropertyMapper<IProgress> ProgressMapper = new PropertyMapper<IProgress>(ViewHandler.ViewMapper)
		{
			[nameof(IProgress.Progress)] = MapProgress,
			[nameof(IProgress.ProgressColor)] = MapProgressColor,
#if __IOS__
			[nameof(IView.BackgroundColor)] = MapBackgroundColor
#endif
		};

		public ProgressBarHandler() : base(ProgressMapper)
		{

		}

		public ProgressBarHandler(PropertyMapper mapper) : base(mapper ?? ProgressMapper)
		{

		}
	}
}
