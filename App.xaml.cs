using PodcastApp9000.Features.Presentation.Views;

namespace PodcastApp9000
{
    public partial class App : Application
    {
        public App(IServiceProvider services)
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = services.GetService<SearchPage>();
        }
    }
}
