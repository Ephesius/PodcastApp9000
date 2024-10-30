using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PodcastApp9000.Features.Presentation.Views;
using PodcastApp9000.Models;
using PodcastApp9000.PodcastApp9000.Core.Services;

namespace PodcastApp9000.Features.Presentation.ViewModels
{
    public partial class SearchViewModel(IPodcastService podcastService) : ObservableObject
    {
        private readonly IPodcastService _podcastService = podcastService;

        [ObservableProperty]
        private string searchTerm = string.Empty;

        [ObservableProperty]
        private bool isSearching;

        [ObservableProperty]
        private bool isNotSearching = true;

        [ObservableProperty]
        private ObservableCollection<Podcast> searchResults = [];

        partial void OnIsSearchingChanged(bool value)
        {
            IsNotSearching = !value;
        }

        [RelayCommand]
        private async Task SearchAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                return;
            }

            try
            {
                IsSearching = true;
                SearchResults.Clear();

                var results = await _podcastService.SearchPodcastsAsync(SearchTerm);

                foreach (var podcast in results)
                {
                    SearchResults.Add(podcast);
                }
            }
            catch (System.Exception ex)
            {
                // We'll add proper error handling later
                System.Diagnostics.Debug.WriteLine($"Search error: {ex.Message}");
            }
            finally
            {
                IsSearching = false;
            }
        }

        [RelayCommand]
        private static async Task ShowDetails(Podcast podcast)
        {
            var viewModel = new PodcastDetailsViewModel();
            viewModel.Initialize(podcast);

            var navigationParameter = new Dictionary<string, object>
            {
                { "ViewModel", viewModel }
            };

            await Shell.Current.GoToAsync(nameof(PodcastDetailsPage), navigationParameter);
        }
    }
}
