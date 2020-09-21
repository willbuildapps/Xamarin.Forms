using System;

namespace Xamarin.Platform.Handlers
{
	public partial class ProgressBarHandler : AbstractViewHandler<IProgress, object>
	{
		protected override object CreateView() => throw new NotImplementedException();

		public static void MapProgress(IViewHandler handler, IProgress progressBar) { }
		public static void MapProgressColor(IViewHandler handler, IProgress progressBar) { }
	}
}