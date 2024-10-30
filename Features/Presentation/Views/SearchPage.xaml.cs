using PodcastApp9000.Features.Presentation.ViewModels;

namespace PodcastApp9000.Features.Presentation.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}