using System;
using System.Collections.Specialized;
using Foundation;
using UIKit;
using Xamarin.Forms;
using RectangleF = CoreGraphics.CGRect;

namespace Xamarin.Platform.Handlers
{
	public partial class PickerRenderer : AbstractViewHandler<IPicker, NativePicker>
	{
		UIPickerView _picker;
		UIColor _defaultTextColor;

		protected override NativePicker CreateView()
		{
			var nativePicker = new NativePicker { BorderStyle = UITextBorderStyle.RoundedRect };

			nativePicker.EditingChanged += OnEditing;

			_picker = new UIPickerView();

			var width = UIScreen.MainScreen.Bounds.Width;
			var toolbar = new UIToolbar(new RectangleF(0, 0, width, 44)) { BarStyle = UIBarStyle.Default, Translucent = true };
			var spacer = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (o, a) =>
			{
				var pickerSource = (PickerSource)_picker.Model;

				if (VirtualView.SelectedIndex == -1 && VirtualView.Items != null && VirtualView.Items.Count > 0)
					UpdateSelectedIndex(0);

				nativePicker.Text = pickerSource.SelectedItem;
				nativePicker.ResignFirstResponder();
			});

			toolbar.SetItems(new[] { spacer, doneButton }, false);

			nativePicker.InputView = _picker;
			nativePicker.InputAccessoryView = toolbar;

			nativePicker.InputView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
			nativePicker.InputAccessoryView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;

			if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
			{
				nativePicker.InputAssistantItem.LeadingBarButtonGroups = null;
				nativePicker.InputAssistantItem.TrailingBarButtonGroups = null;
			}

			_defaultTextColor = nativePicker.TextColor;

			nativePicker.AccessibilityTraits = UIAccessibilityTrait.Button;

			_picker.Model = new PickerSource(VirtualView);

			((INotifyCollectionChanged)VirtualView.Items).CollectionChanged += OnCollectionChanged;

			return nativePicker;
		}

		protected override void DisposeView(NativePicker mauiPicker)
		{
			_defaultTextColor = null;

			if (_picker != null)
			{
				if (_picker.Model != null)
				{
					_picker.Model.Dispose();
					_picker.Model = null;
				}

				_picker.RemoveFromSuperview();
				_picker.Dispose();
				_picker = null;
			}

			mauiPicker.EditingChanged -= OnEditing;

			((INotifyCollectionChanged)VirtualView.Items).CollectionChanged -= OnCollectionChanged;

			base.DisposeView(mauiPicker);
		}

		public static void MapTitle(IViewHandler handler, IPicker picker)
		{
			(handler as PickerRenderer)?.UpdatePicker();
		}

		public static void MapTitleColor(IViewHandler handler, IPicker picker)
		{
			(handler as PickerRenderer)?.UpdateTitleColor();
		}

		public static void MapTextColor(IViewHandler handler, IPicker picker)
		{
			(handler as PickerRenderer)?.UpdateTextColor();
		}

		public static void MapSelectedIndex(IViewHandler handler, IPicker picker)
		{
			(handler as PickerRenderer)?.UpdateSelectedIndex(picker.SelectedIndex);
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
			(handler as PickerRenderer)?.UpdateHorizontalTextAlignment();
		}

		public static void MapVerticalTextAlignment(IViewHandler handler, IText text)
		{
			(handler as PickerRenderer)?.UpdateVerticalTextAlignment();
		}

		void UpdatePicker()
		{
			var selectedIndex = VirtualView.SelectedIndex;
			var items = VirtualView.Items;

			UpdateTitleColor();

			TypedNativeView.Text = selectedIndex == -1 || items == null || selectedIndex >= items.Count ? string.Empty : items[selectedIndex];
			_picker.ReloadAllComponents();

			if (items == null || items.Count == 0)
				return;

			UpdateSelectedIndex(selectedIndex);
			UpdateSelectedItem();
			UpdateCharacterSpacing();
		}

		void UpdateTextColor()
		{
			if (VirtualView == null || TypedNativeView == null)
				return;

			var textColor = VirtualView.Color;

			if (textColor.IsDefault || (!VirtualView.IsEnabled))
				TypedNativeView.TextColor = _defaultTextColor;
			else
				TypedNativeView.TextColor = textColor.ToNative();

			// HACK This forces the color to update; there's probably a more elegant way to make this happen
			TypedNativeView.Text = TypedNativeView.Text;
		}

		void UpdateSelectedIndex(int selectedIndex)
		{
			if (VirtualView == null)
				return;

			VirtualView.SelectedIndex = selectedIndex;
			VirtualView.SelectedIndexChanged();

			var source = (PickerSource)_picker.Model;
			source.SelectedIndex = selectedIndex;
			source.SelectedItem = selectedIndex >= 0 ? VirtualView.Items[selectedIndex] : null;
			_picker.Select(Math.Max(selectedIndex, 0), 0, true);
		}

		void UpdateTitleColor()
		{
			if (VirtualView == null)
				return;

			var title = VirtualView.Title;

			if (string.IsNullOrEmpty(title))
				return;

			var titleColor = VirtualView.TitleColor;

			UpdateAttributedPlaceholder(new NSAttributedString(title, null, titleColor.ToNative()));
		}

		void UpdateAttributedPlaceholder(NSAttributedString nsAttributedString)
		{
			if (TypedNativeView == null)
				return;

			TypedNativeView.AttributedPlaceholder = nsAttributedString;
		}

		void UpdateFont()
		{
			// TODO: Port FontExtensions to Xamarin.Platform.
		}

		void UpdateCharacterSpacing()
		{
			var textAttr = TypedNativeView.AttributedText.AddCharacterSpacing(TypedNativeView.Text, VirtualView.CharacterSpacing);

			if (textAttr != null)
				TypedNativeView.AttributedText = textAttr;

			var placeHolder = TypedNativeView.AttributedPlaceholder.AddCharacterSpacing(VirtualView.Title, VirtualView.CharacterSpacing);

			if (placeHolder != null)
				UpdateAttributedPlaceholder(placeHolder);
		}

		void UpdateHorizontalTextAlignment()
		{
			TypedNativeView.TextAlignment = VirtualView.HorizontalTextAlignment.ToNativeTextAlignment(EffectiveFlowDirection.Explicit);
		}

		void UpdateVerticalTextAlignment()
		{
			TypedNativeView.VerticalAlignment = VirtualView.VerticalTextAlignment.ToNativeTextAlignment();
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

		void OnCollectionChanged(object sender, EventArgs e)
		{
			UpdatePicker();
		}

		void OnEditing(object sender, EventArgs eventArgs)
		{
			// Reset the TextField's Text so it appears as if typing with a keyboard does not work.
			var selectedIndex = VirtualView.SelectedIndex;
			var items = VirtualView.Items;
			TypedNativeView.Text = selectedIndex == -1 || items == null ? "" : items[selectedIndex];

			// Also clears the undo stack (undo/redo possible on iPads)
			TypedNativeView.UndoManager.RemoveAllActions();
		}
	}

	class PickerSource : UIPickerViewModel
	{
		IPicker _virtualView;
		bool _disposed;

		public PickerSource(IPicker virtualView)
		{
			_virtualView = virtualView;
		}

		public int SelectedIndex { get; internal set; }

		public string SelectedItem { get; internal set; }

		public override nint GetComponentCount(UIPickerView picker)
		{
			return 1;
		}

		public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
		{
			return _virtualView.Items != null ? _virtualView.Items.Count : 0;
		}

		public override string GetTitle(UIPickerView picker, nint row, nint component)
		{
			return _virtualView.Items[(int)row];
		}

		public override void Selected(UIPickerView picker, nint row, nint component)
		{
			if (_virtualView.Items.Count == 0)
			{
				SelectedItem = null;
				SelectedIndex = -1;
			}
			else
			{
				SelectedItem = _virtualView.Items[(int)row];
				SelectedIndex = (int)row;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			_disposed = true;

			if (disposing)
				_virtualView = null;

			base.Dispose(disposing);
		}
	}
}