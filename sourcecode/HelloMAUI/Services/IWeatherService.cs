using HelloMAUI.Model;

namespace HelloMAUI.Services
{
    public interface IWeatherService
    {        
        bool CanConnectToInternet();

        Task<WeatherResults> GetCurrentWeather();
    }
}