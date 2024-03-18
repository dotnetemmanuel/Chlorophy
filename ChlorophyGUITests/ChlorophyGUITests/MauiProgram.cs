using Microsoft.Extensions.Logging;

namespace ChlorophyGUITests
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
                    fonts.AddFont("bloomings.otf", "bloomings");
                    fonts.AddFont("futurabook.ttf", "futurabook");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            SetHandler();


            return builder.Build();
        }

        private static void SetHandler()
        {

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
            {
                if (view is Entry)
                {
#if ANDROID
handler.PlatformView.Background = null;
#endif
                }
            });
        }
    }
}
