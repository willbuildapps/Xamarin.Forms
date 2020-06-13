using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Controls.GalleryPages.CollectionViewGalleries
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemReuseGallery : ContentPage
	{
		public ItemReuseGallery()
		{
			InitializeComponent();

			var source = new ObservableCollection<string>();

			for (int n = 0; n < 100; n++)
			{
				source.Add($"Item {n}");
			}

			BindingContext = source;

			ColV.PrepareItemForReuse = (item) => {
				foreach (var child in item.LogicalChildren)
				{
					if (child is CheckBox cb)
					{
						cb.IsChecked = false;
					}
				}
			};
		}
	}
}