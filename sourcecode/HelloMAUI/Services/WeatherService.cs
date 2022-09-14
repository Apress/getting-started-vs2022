using System.Net.Http.Json;
using HelloMAUI.Model;

namespace HelloMAUI.Services
{
    public class WeatherService : IWeatherService
    {
        IConnectivity _connectivity;

        WeatherResults weather = new();

        HttpClient httpClient;

        public WeatherService(IConnectivity connectivity)
        {
            _connectivity = connectivity;
            httpClient = new HttpClient();
        }

        public bool CanConnectToInternet()
        {
            return _connectivity?.NetworkAccess == NetworkAccess.Internet;
        }

        public async Task<WeatherResults> GetCurrentWeather()
        {            
            var baseUrl = "https://api.weatherapi.com/v1";
            var endP = "forecast.json";
            var key = "91e3647d6ece446d969130840220309";
            var city = "Los Angeles";

            var url = $"{baseUrl}/{endP}?key={key}&q={city}&days=10&aqi=no&alerts=no";            

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                weather = await response.Content.ReadFromJsonAsync<WeatherResults>();
            }

            return weather;
        }        
    }
}