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
            BackgroundColor = "#BDFFFF";

            // Her bliver ICommand interfacet brugt til at vi kan lave en comman, som en knap køre, når den bliver trykket på.
            callCW = new Command(() => call_current_weather());
            callSCW = new Command(() => call_searched_city_weather());

            // DeviceDisplay har en masse nyttit information omkring displayet, hvis man vil lave noget til en specifik slaks resolution eller lign.
            // Her bliver MainDisplayInfoChanged event handleren brugt, som går ind og kalder den her funktion hver gang hvor telefonen rotere.
            DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged;
        }

        private void DeviceDisplay_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        {
            // Orientation bliver converteret til en string, så vi kan sammenligne det, og gøre noget specifikt, hvis orientationen er fx. landscape eller portrait.
            if (e.DisplayInfo.Orientation.ToString() == "Landscape")
            {
                BackgroundColor = "#D0FFFF";
            }
            else if (e.DisplayInfo.Orientation.ToString() == "Portrait")
            {
                BackgroundColor = "#BDFFFF";
            }
        }

        public void call_current_weather()
        {
            try
            {
                // Xamarin.essentials bliver brugt i dette tilfælde til og hente enhedens location
                var location = Geolocation.GetLastKnownLocationAsync();            
                if (location != null)
                {
                    // henter json date fra openweathers api, hvor det derefter bliver deserialized ind i en class.
                    var json = new WebClient().DownloadString($"https://api.openweathermap.org/data/2.5/forecast?lat={location.Result.Latitude}&lon={location.Result.Longitude}&appid=97d122bde818513159961ab3df1a0527&units=metric");
                    var mylist = JsonConvert.DeserializeObject<OverallWeather.Root>(json);
                    // Temp og City er binds som er lavet til labels på vores side, og de er defineret i BaseViewModel.cs
                    Temp = mylist.list[0].main.temp.ToString() + "°";
                    City = mylist.city.name;
                    string iconID = mylist.list[0].weather[0].icon;
                    Icon = "https://openweathermap.org/img/wn/" + iconID + "@4x.png";
                    future_weather_forecast(mylist);
                }
            }
            catch (Exception)
            {
                // Her gør vi brug af displayAlert som går ind og laver en popup men den error besked som man har sat.
                App.Current.MainPage.DisplayAlert("Error", "Your location is currently unavailable.", "OK");
            }
        }

        public void call_searched_city_weather()
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
                App.Current.MainPage.DisplayAlert("Error", City + " is not a city", "OK");
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
                        default:
                            App.Current.MainPage.DisplayAlert("Error", "Something went wrong, while trying to temperature for the comming days.", "OK");
                            break;
                    }
                }
            }
        }

        public ICommand callCW { get; }
        public ICommand callSCW { get; }
    }
}