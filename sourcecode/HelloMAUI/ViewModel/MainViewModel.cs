using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelloMAUI.Model;
using HelloMAUI.Services;

namespace HelloMAUI.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        readonly IWeatherService _weatherService;
        public ObservableCollection<ForecastDay> TenDayForecast { get; } = new();        

        [ObservableProperty]
        string currentCity;
        [ObservableProperty]
        int currentTemp;
        [ObservableProperty]
        string currentCondition;
        [ObservableProperty]
        int feelsLike;
        [ObservableProperty]
        string currentIcon;

        public MainViewModel(IWeatherService weatherService)
        {            
            _weatherService = weatherService;
        }        

        [RelayCommand]
        async Task GetWeatherAsync()
        {
            try
            {
                if (!_weatherService.CanConnectToInternet())
                {
                    await Shell.Current.DisplayAlert("Error!", "You are not connected to the Internet", "OK");
                }
                else
                {
                    var weatherResults = await _weatherService.GetCurrentWeather();
                    
                    CurrentCity = weatherResults.location.name;
                    CurrentTemp = (int)weatherResults.current.feelslike_f;
                    CurrentCondition = weatherResults.current.condition.text;
                    FeelsLike = (int)weatherResults.current.feelslike_f;
                    CurrentIcon = $"https:{weatherResults.current.condition.icon}";

                    if (TenDayForecast.Count != 0)
                        TenDayForecast.Clear();

                    foreach (var forecast in weatherResults.forecast.forecastday)
                    {
                        var dow = "Today";
                        if (DateTime.Parse(forecast.date) != DateTime.Today)
                        {
                            dow = DateTime.Parse(forecast.date).ToString("ddd");
                        }
                        
                        var forecastDay = new ForecastDay()
                        {
                            DayOfWeek = dow,
                            MinTemp = (int)forecast.day.mintemp_f,
                            MaxTemp = (int)forecast.day.maxtemp_f,
                            ImageUrl = $"https:{forecast.day.condition.icon}"
                        };

                        TenDayForecast.Add(forecastDay);
                    }
                }                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", "Weather Service Call Failed", "OK");
            }
        }
    }
}