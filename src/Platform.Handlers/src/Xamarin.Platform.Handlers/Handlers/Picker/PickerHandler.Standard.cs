using System;

namespace Xamarin.Platform.Handlers
{
	public partial class PickerRenderer : AbstractViewHandler<IPicker, object>
	{
		protected override object CreateView() => throw new NotImplementedException();

		public static void MapTitle(IViewHandler handler, IPicker picker) { }
		public static void MapTitleColor(IViewHandler handler, IPicker picker) { }
		public static void MapTextColor(IViewHandler handler, IPicker picker) { }
		public static void MapSelectedIndex(IViewHandler handler, IPicker picker) { }
		public static void MapFont(IViewHandler handler, IText text) { }
		public static void MapCharacterSpacing(IViewHandler handler, IText text) { }
		public static void MapHorizontalTextAlignment(IViewHandler handler, IText text) { }
		public static void MapVerticalTextAlignment(IViewHandler handler, IText text) { }
	}
}