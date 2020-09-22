using System;
using Xamarin.Platform;

namespace Xamarin.Forms
{
	public partial class Button : IButton
	{
		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }

		public string UpdateTransformedText(string source, TextTransform textTransform)
		{
			throw new NotImplementedException();
		}

		void IButton.Clicked()
		{
			throw new NotImplementedException();
		}

		void IButton.Pressed()
		{
			throw new NotImplementedException();
		}

		void IButton.Released()
		{
			throw new NotImplementedException();
		}
	}
}