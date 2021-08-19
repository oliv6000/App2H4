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
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            callCW = new Command(async () => call_current_weather());
            callSCW = new Command(async () => call_searched_city_weather());
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
                    Temp = mylist.list[0].main.temp;
                    Console.WriteLine(mylist.city.name);
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
                Temp = mylist.list[0].main.temp;
                Console.WriteLine(mylist.city.name);

                //this is for the weather icon
                string iconID = mylist.list[0].weather[0].icon;
                Icon = "https://openweathermap.org/img/wn/" + iconID + "@4x.png";
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", City + " is not a city", "OK");
            }
        }

        public ICommand callCW { get; }
        public ICommand callSCW { get; }

        public ICommand OpenWebCommand { get; }

        public class SalesTransactions
        {
            public List<Overall> overall { get; set; }
            public int count { get; set; }
        }

        public class Overall
        {
            public string City { get; set; }
            public string Country { get; set; }
            public string Lat { get; set; }
            public string Lon { get; set; }
            public string Description { get; set; }
            public string Humidity { get; set; }
            public string TempFeelsLike { get; set; }
            public string Temp { get; set; }
            public string TempMax { get; set; }
            public string TempMin { get; set; }
            public string WeatherIcon { get; set; }
        }

        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public double feels_like { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public int sea_level { get; set; }
            public int grnd_level { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
            public double gust { get; set; }
        }

        public class Rain
        {
            public double _1h { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class Root
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public int visibility { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public int timezone { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }
    }
}