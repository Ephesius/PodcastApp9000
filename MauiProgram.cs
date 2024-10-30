using Microsoft.Extensions.Logging;
using PodcastApp9000.Features.Presentation.ViewModels;
using PodcastApp9000.Features.Presentation.Views;
using PodcastApp9000.PodcastApp9000.Core.Services;

namespace PodcastApp9000
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register PodcastService with dependency injection
            builder.Services.AddSingleton<IPodcastService, PodcastService>();

            // ViewModels
            builder.Services.AddTransient<SearchViewModel>();

            // Search Page
            builder.Services.AddTransient<SearchPage>();

            // App
            builder.Services.AddTransient<App>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
