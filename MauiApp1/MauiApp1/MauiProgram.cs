using MauiApp1.DB;
using Microsoft.Extensions.Logging;

namespace MauiApp1;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
        // Настройка пути к базе данных
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Math_Adventure.db");
        builder.Services.AddSingleton(s => new LocalDatabase(dbPath));
        builder.Services.AddSingleton<App>(); // Регистрируем App как Singleton
        return builder.Build();
    }
}
