using System;

namespace Xamarin.Platform.Handlers
{
	public partial class ActivityIndicatorHandler : AbstractViewHandler<IActivityIndicator, object>
	{
		public static void MapIsRunning(IViewHandler handler, IActivityIndicator activityIndicator) { }
		public static void MapColor(IViewHandler handler, IActivityIndicator activityIndicator) { }

		protected override object CreateView() => throw new NotImplementedException();
	}
}