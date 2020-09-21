using System;

namespace Xamarin.Platform.Handlers
{
	public partial class SwitchHandler : AbstractViewHandler<ISwitch, object>
	{
		public static void MapIsToggled(IViewHandler handler, ISwitch view) { }
		public static void MapOnColor(IViewHandler handler, ISwitch view) { }
		public static void MapThumbColor(IViewHandler handler, ISwitch view) { }

		protected override object CreateView() => throw new NotImplementedException();
	}
}