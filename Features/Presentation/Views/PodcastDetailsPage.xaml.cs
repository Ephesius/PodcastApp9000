using PodcastApp9000.Features.Presentation.ViewModels;

namespace PodcastApp9000.Features.Presentation.Views;

public partial class PodcastDetailsPage : ContentPage
{
	public PodcastDetailsPage()
	{
		InitializeComponent();
	}

	//[QueryProperty(nameof(ViewModels), "ViewModel")]
	public PodcastDetailsViewModel ViewModel
	{
		get => BindingContext as PodcastDetailsViewModel;
		set => BindingContext = value;
	}
}