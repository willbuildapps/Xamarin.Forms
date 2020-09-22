namespace Xamarin.Platform.Handlers
{
	public partial class PickerRenderer
	{
		public static PropertyMapper<IPicker> PickerMapper = new PropertyMapper<IPicker>(ViewHandler.ViewMapper)
		{
			[nameof(IPicker.Title)] = MapTitle,
			[nameof(IPicker.TitleColor)] = MapTitleColor,
			[nameof(IPicker.Color)] = MapTextColor,
			[nameof(IPicker.SelectedIndex)] = MapSelectedIndex,

			[nameof(IText.Font)] = MapFont,
			[nameof(IText.FontAttributes)] = MapFont,
			[nameof(IText.FontFamily)] = MapFont,
			[nameof(IText.FontSize)] = MapFont,
			[nameof(IText.CharacterSpacing)] = MapCharacterSpacing,
			[nameof(IText.HorizontalTextAlignment)] = MapHorizontalTextAlignment,
			[nameof(IText.VerticalTextAlignment)] = MapVerticalTextAlignment
		};

		public PickerRenderer() : base(PickerMapper)
		{

		}

		public PickerRenderer(PropertyMapper mapper) : base(mapper)
		{

		}
	}
}