using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using CommunityToolkit.Maui;
using HavenlyBookingApp.Models.ViewModels;
using HavenlyBookingApp.Views;

namespace HavenlyBookingApp
{
    public static class MauiProgram
    {
        public static IServiceProvider Services { get; private set; }
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //By default, appshell does not pass arguments using DI. You have to manually pass the argument instances yourself
            builder.Services.AddSingleton<HavenlyDatabase>();
            var app = builder.Build();
            Services = app.Services;
            return app;
        }
    }
}
