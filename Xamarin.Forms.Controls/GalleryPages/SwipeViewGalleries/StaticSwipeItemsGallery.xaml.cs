namespace Xamarin.Forms.Controls.GalleryPages.SwipeViewGalleries
{
	public partial class StaticSwipeItemsGallery : ContentPage
	{
		public StaticSwipeItemsGallery()
		{
			InitializeComponent();

			BindingContext = new SwipeViewGalleryViewModel();

			MessagingCenter.Subscribe<SwipeViewGalleryViewModel, object>(this, "delete", (sender, args) =>
			{
				DisplayAlert("SwipeView", $"{((Message)args).Title}", "Ok");
			});
		}
	}
}