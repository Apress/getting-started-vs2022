using HelloMAUI.Services;
using HelloMAUI.ViewModel;

namespace HelloMAUI;

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

		
        builder.Services.AddSingleton<IToDoService, ToDoService>();        
		builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<MainPage>();
		
        return builder.Build();
	}
}



