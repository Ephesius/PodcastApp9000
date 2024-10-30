using PodcastApp9000.Models;

namespace PodcastApp9000.PodcastApp9000.Core.Services
{
    public interface IPodcastService
    {
        Task<IEnumerable<Podcast>> SearchPodcastsAsync(string searchTerm);
    }
}
