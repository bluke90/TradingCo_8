using TradingCo.Data;
using TradingCo.Mechanics;

namespace TradingCo;

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

		builder.Services.AddSingleton<MaterialContext>();
        builder.Services.AddSingleton<MaterialStorage>();
        builder.Services.AddSingleton<Market>();
		builder.Services.AddSingleton<Account>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<AccountPage>();
        builder.Services.AddSingleton<WarehousePage>();

        return builder.Build();
	}
}
