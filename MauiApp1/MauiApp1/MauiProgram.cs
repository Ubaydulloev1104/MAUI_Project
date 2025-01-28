using MauiApp1.DB;
using MauiApp1.Services.Auth;
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
        builder.Services.AddSingleton<IAuthService, AuthService>();

        builder.Services.AddScoped<IdentityHttpClient>(provider =>
        {
            var client = new IdentityHttpClient
            {
                BaseAddress = new Uri("https://localhost:7279/api/")
            };
            return client;
        });



        // Настройка пути к базе данных
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Math_Adventure.db");
        builder.Services.AddSingleton(s => new LocalDatabase(dbPath));
        builder.Services.AddSingleton<App>(); // Регистрируем App как Singleton
        return builder.Build();
    }
}
