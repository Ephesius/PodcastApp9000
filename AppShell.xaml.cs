using PodcastApp9000.Features.Presentation.Views;

namespace PodcastApp9000
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute(nameof(PodcastDetailsPage), typeof(PodcastDetailsPage));
        }
    }
}
