using CommunityToolkit.Mvvm.ComponentModel;
using PodcastApp9000.Models;

namespace PodcastApp9000.Features.Presentation.ViewModels
{
    public partial class PodcastDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private Podcast? podcast;

        public PodcastDetailsViewModel()
        {
        }

        public void Initialize(Podcast podcast)
        {
            Podcast = podcast;
        }
    }
}