using PodcastApp9000.Features.Presentation.Views;

namespace PodcastApp9000
{
    public partial class App : Application
    {
        public App(IServiceProvider services)
        {
            if (services is not null)
            {
                InitializeComponent();

                MainPage = new AppShell();
            }
            else
            {
                throw new ArgumentNullException(nameof(services));
            }
            //MainPage = services.GetService<SearchPage>();
        }
    }
}
