namespace Xamarin.Platform.Handlers
{
	public partial class SwitchHandler
	{
		public static PropertyMapper<ISwitch> SwitchMapper = new PropertyMapper<ISwitch>(ViewHandler.ViewMapper)
		{
			[nameof(ISwitch.IsToggled)] = MapIsToggled,
			[nameof(ISwitch.OnColor)] = MapOnColor,
			[nameof(ISwitch.ThumbColor)] = MapThumbColor
		};

		public SwitchHandler() : base(SwitchMapper)
		{

		}

		public SwitchHandler(PropertyMapper mapper) : base(mapper ?? SwitchMapper)
		{

		}
	}
}