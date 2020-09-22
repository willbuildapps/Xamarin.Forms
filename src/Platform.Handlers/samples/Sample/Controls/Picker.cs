using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
	public class Picker : View, IPicker
	{
		public Picker()
		{
		}

		public string Title { get; set; }

		public Color TitleColor { get; set; }

		public IList<string> Items { get; set; }

		public IList ItemsSource { get; set; }

		public int SelectedIndex { get; set; }

		public object SelectedItem { get; set; }

		public string Text => throw new System.NotImplementedException();

		public Color Color => throw new System.NotImplementedException();

		public Font Font => throw new System.NotImplementedException();

		public TextTransform TextTransform => throw new System.NotImplementedException();

		public double CharacterSpacing => throw new System.NotImplementedException();

		public FontAttributes FontAttributes => throw new System.NotImplementedException();

		public string FontFamily => throw new System.NotImplementedException();

		public double FontSize => throw new System.NotImplementedException();

		public TextAlignment HorizontalTextAlignment => throw new System.NotImplementedException();

		public TextAlignment VerticalTextAlignment => throw new System.NotImplementedException();
	}
}