using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Input;
using Xamarin.Essentials;
using System.Diagnostics;
using Xamarin.Forms;
using App2H4.Services;

namespace App2H4.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            callCW = new Command(async () => call_current_weather());
            callSCW = new Command(async () => call_searched_city_weather());
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            if (e.DisplayInfo.Orientation.ToString() == "landscape")
            {

            }
            else if (e.DisplayInfo.Orientation.ToString() == "portrait")
            {

            }
        }

        public async void call_current_weather()
        {
            try
            {
                var location = Geolocation.GetLastKnownLocationAsync();            
                if (location != null)
                {
                    var json = new WebClient().DownloadString($"https://api.openweathermap.org/data/2.5/forecast?lat={location.Result.Latitude}&lon={location.Result.Longitude}&appid=97d122bde818513159961ab3df1a0527&units=metric");
                    var mylist = JsonConvert.DeserializeObject<OverallWeather.Root>(json);
                    Temp = mylist.list[0].main.temp.ToString() + "°";
                    City = mylist.city.name;
                    string iconID = mylist.list[0].weather[0].icon;
                    Icon = "https://openweathermap.org/img/wn/" + iconID + "@4x.png";
                    future_weather_forecast(mylist);
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Your location is currently unavailable.", "OK");
            }
        }

        public async void call_searched_city_weather()
        {
            try
            {
                var json = new WebClient().DownloadString($"https://api.openweathermap.org/data/2.5/forecast?q={City}&appid=97d122bde818513159961ab3df1a0527&units=metric");
                var mylist = JsonConvert.DeserializeObject<OverallWeather.Root>(json);
                Temp = mylist.list[0].main.temp.ToString() + "°";
                future_weather_forecast(mylist);

                //this is for the weather icon
                string iconID = mylist.list[0].weather[0].icon;
                Icon = "https://openweathermap.org/img/wn/" + iconID + "@4x.png";
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", City + " is not a city", "OK");
            }

        }

        public void future_weather_forecast(OverallWeather.Root mylist)
        {
            int i = 0;
            foreach (var item in mylist.list)
            {
                Console.WriteLine(i);
                DateTime forecastDay = DateTime.Parse(item.dt_txt);
                if (forecastDay.Hour == 12)
                {
                    i++;
                    switch (i)
                    {
                        case 1:
                            Day1 = forecastDay.DayOfWeek.ToString() + " --> " + item.main.temp + "°";
                            break;
                        case 2:
                            Day2 = forecastDay.DayOfWeek.ToString() + " --> " + item.main.temp + "°";
                            break;
                        case 3:
                            Day3 = forecastDay.DayOfWeek.ToString() + " --> " + item.main.temp + "°";
                            break;
                        case 4:
                            Day4 = forecastDay.DayOfWeek.ToString() + " --> " + item.main.temp + "°";
                            break;
                        case 5:
                            Day5 = forecastDay.DayOfWeek.ToString() + " --> " + item.main.temp + "°";
                            break;
                    }
                }
            }
        }

        public ICommand callCW { get; }
        public ICommand callSCW { get; }
    }
}