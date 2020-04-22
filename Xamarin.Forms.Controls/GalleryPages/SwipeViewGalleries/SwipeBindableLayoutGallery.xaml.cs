using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Controls.GalleryPages.SwipeViewGalleries
{
	[Preserve(AllMembers = true)]
	public partial class SwipeBindableLayoutGallery : ContentPage
	{
		public SwipeBindableLayoutGallery()
		{
			InitializeComponent();
			BindingContext = new SwipeViewGalleryViewModel();

			MessagingCenter.Subscribe<SwipeViewGalleryViewModel, object>(this, "favourite", (sender, args) =>
			{
				DisplayAlert("SwipeView", $"Favourite {((Message)args).Title}", "Ok");
			});

			MessagingCenter.Subscribe<SwipeViewGalleryViewModel, object>(this, "delete", (sender, args) =>
			{
				DisplayAlert("SwipeView", $"Delete {((Message)args).Title}", "Ok");
			});
		}
	}

	[Preserve(AllMembers = true)]
	public class Message
	{
		public string Title { get; set; }
		public string SubTitle { get; set; }
		public string Description { get; set; }
		public string Date { get; set; }

		public override string ToString()
		{
			return Title;
		}
	}

	[Preserve(AllMembers = true)]
	public class SwipeViewGalleryViewModel : BindableObject
	{
		ObservableCollection<Message> _messages;

		public SwipeViewGalleryViewModel()
		{
			Messages = new ObservableCollection<Message>();
			LoadMessages();
		}

		public ObservableCollection<Message> Messages
		{
			get { return _messages; }
			set
			{
				_messages = value;
				OnPropertyChanged();
			}
		}

		public ICommand FavouriteCommand => new Command<object>(OnFavourite);
		public ICommand DeleteCommand => new Command<object>(OnDelete);

		void LoadMessages()
		{
			for (int i = 0; i < 100; i++)
			{
				Messages.Add(new Message { Title = $"Lorem ipsum {i + 1}", SubTitle = "Lorem ipsum dolor sit amet", Date = "Yesterday", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." });
			}
		}

		void OnFavourite(object parameter)
		{
			MessagingCenter.Send(this, "favourite", parameter);
		}

		void OnDelete(object parameter)
		{
			MessagingCenter.Send(this, "delete", parameter);
		}
	}
}