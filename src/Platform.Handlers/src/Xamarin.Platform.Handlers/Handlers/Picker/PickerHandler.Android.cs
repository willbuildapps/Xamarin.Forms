using System;
using System.Collections.Specialized;
using System.Linq;
using Android.App;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Xamarin.Forms;
using AColor = Android.Graphics.Color;
using AResource = Android.Resource;

namespace Xamarin.Platform.Handlers
{
	public partial class PickerRenderer : AbstractViewHandler<IPicker, NativePicker>
	{
		AlertDialog _dialog;
		TextColorSwitcher _textColorSwitcher;
		int _originalHintTextColor;

		protected override NativePicker CreateView()
		{
			var mauiPicker = new NativePicker(Context);

			mauiPicker.Click += OnClick;
			((INotifyCollectionChanged)VirtualView.Items).CollectionChanged += OnCollectionChanged;

			return mauiPicker;
		}

		protected override void SetupDefaults()
		{
			base.SetupDefaults();

			_textColorSwitcher = new TextColorSwitcher(TypedNativeView.TextColors);
			_originalHintTextColor = TypedNativeView.CurrentHintTextColor;
		}

		protected override void DisposeView(NativePicker mauiPickerText)
		{
			mauiPickerText.Click -= OnClick;
			((INotifyCollectionChanged)VirtualView.Items).CollectionChanged -= OnCollectionChanged;

			base.DisposeView(mauiPickerText);
		}

		public static void MapTitle(IViewHandler handler, IPicker picker)
		{
			(handler as PickerRenderer)?.UpdatePicker();
		}

		public static void MapTitleColor(IViewHandler handler, IPicker picker)
		{
			(handler as PickerRenderer)?.UpdatePicker();
		}

		public static void MapTextColor(IViewHandler handler, IPicker picker)
		{
			(handler as PickerRenderer)?.UpdateTextColor();
		}

		public static void MapSelectedIndex(IViewHandler handler, IPicker picker)
		{
			(handler as PickerRenderer)?.UpdatePicker();
		}

		public static void MapFont(IViewHandler handler, IText text)
		{
			(handler as PickerRenderer)?.UpdateFont();
		}

		public static void MapCharacterSpacing(IViewHandler handler, IText text)
		{
			(handler as PickerRenderer)?.UpdateCharacterSpacing();
		}

		public static void MapHorizontalTextAlignment(IViewHandler handler, IText text)
		{
			(handler as PickerRenderer)?.UpdateTextAlignment();
		}

		public static void MapVerticalTextAlignment(IViewHandler handler, IText text)
		{
			(handler as PickerRenderer)?.UpdateTextAlignment();
		}

		void UpdatePicker()
		{
			TypedNativeView.Hint = VirtualView.Title;

			if (VirtualView.TitleColor != Color.Default)
				TypedNativeView.SetHintTextColor(VirtualView.TitleColor.ToNative());
			else
				TypedNativeView.SetHintTextColor(new AColor(_originalHintTextColor));

			if (VirtualView.SelectedIndex == -1 || VirtualView.Items == null || VirtualView.SelectedIndex >= VirtualView.Items.Count)
				TypedNativeView.Text = null;
			else
				TypedNativeView.Text = VirtualView.Items[VirtualView.SelectedIndex];

			UpdateSelectedItem();
		}

		void UpdateTextColor()
		{
			_textColorSwitcher?.UpdateTextColor(TypedNativeView, VirtualView.Color);
		}

		void UpdateFont()
		{
			// TODO: Port FontExtensions to Xamarin.Platform.
		}

		void UpdateCharacterSpacing()
		{
			if (NativeVersion.IsAtLeast(21))
			{
				TypedNativeView.LetterSpacing = VirtualView.CharacterSpacing.ToEm();
			}
		}

		void UpdateTextAlignment()
		{
			TypedNativeView.Gravity = VirtualView.HorizontalTextAlignment.ToHorizontalGravityFlags() | VirtualView.VerticalTextAlignment.ToVerticalGravityFlags();
		}

		void UpdateSelectedItem()
		{
			if (VirtualView == null)
				return;

			int index = VirtualView.SelectedIndex;

			if (index == -1)
			{
				VirtualView.SelectedItem = null;
				return;
			}

			if (VirtualView.ItemsSource != null)
			{
				VirtualView.SelectedItem = VirtualView.ItemsSource[index];
				return;
			}

			VirtualView.SelectedItem = VirtualView.Items[index];
		}

		void OnClick(object sender, EventArgs e)
		{
			if (VirtualView == null)
				return;

			if (_dialog == null)
			{
				using (var builder = new AlertDialog.Builder(Context))
				{
					if (VirtualView.TitleColor == Color.Default)
					{
						builder.SetTitle(VirtualView.Title ?? string.Empty);
					}
					else
					{
						var title = new SpannableString(VirtualView.Title ?? string.Empty);
						title.SetSpan(new ForegroundColorSpan(VirtualView.TitleColor.ToNative()), 0, title.Length(), SpanTypes.ExclusiveExclusive);
						builder.SetTitle(title);
					}

					string[] items = VirtualView.Items.ToArray();
					builder.SetItems(items, (s, e) =>
					{
						var selectedIndex = e.Which;
						VirtualView.SelectedIndex = selectedIndex;
						VirtualView.SelectedIndexChanged();
						UpdatePicker();
					});

					builder.SetNegativeButton(AResource.String.Cancel, (o, args) => { });

					_dialog = builder.Create();
				}

				_dialog.SetCanceledOnTouchOutside(true);

				_dialog.DismissEvent += (sender, args) =>
				{
					_dialog.Dispose();
					_dialog = null;
				};

				_dialog.Show();
			}
		}

		void OnCollectionChanged(object sender, EventArgs e)
		{
			UpdatePicker();
		}
	}
}