using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.Runtime.InteropServices;
using System.Linq;
namespace WeatherApp
{
    internal class Program
    {
        
        

        static void Main(string[] args)
        {
            Console.SetWindowSize(155,40);
            
            string title = " ___       __   ________  ________  ___       ________          ___       __   _______   ________  _________  ___  ___  _______   ________  ___       \r\n|\\  \\     |\\  \\|\\   __  \\|\\   __  \\|\\  \\     |\\   ___ \\        |\\  \\     |\\  \\|\\  ___ \\ |\\   __  \\|\\___   ___\\\\  \\|\\  \\|\\  ___ \\ |\\   __  \\|\\  \\      \r\n\\ \\  \\    \\ \\  \\ \\  \\|\\  \\ \\  \\|\\  \\ \\  \\    \\ \\  \\_|\\ \\       \\ \\  \\    \\ \\  \\ \\   __/|\\ \\  \\|\\  \\|___ \\  \\_\\ \\  \\\\\\  \\ \\   __/|\\ \\  \\|\\  \\ \\  \\     \r\n \\ \\  \\  __\\ \\  \\ \\  \\\\\\  \\ \\   _  _\\ \\  \\    \\ \\  \\ \\\\ \\       \\ \\  \\  __\\ \\  \\ \\  \\_|/_\\ \\   __  \\   \\ \\  \\ \\ \\   __  \\ \\  \\_|/_\\ \\   _  _\\ \\  \\    \r\n  \\ \\  \\|\\__\\_\\  \\ \\  \\\\\\  \\ \\  \\\\  \\\\ \\  \\____\\ \\  \\_\\\\ \\       \\ \\  \\|\\__\\_\\  \\ \\  \\_|\\ \\ \\  \\ \\  \\   \\ \\  \\ \\ \\  \\ \\  \\ \\  \\_|\\ \\ \\  \\\\  \\\\ \\__\\   \r\n   \\ \\____________\\ \\_______\\ \\__\\\\ _\\\\ \\_______\\ \\_______\\       \\ \\____________\\ \\_______\\ \\__\\ \\__\\   \\ \\__\\ \\ \\__\\ \\__\\ \\_______\\ \\__\\\\ _\\\\|__|   \r\n    \\|____________|\\|_______|\\|__|\\|__|\\|_______|\\|_______|        \\|____________|\\|_______|\\|__|\\|__|    \\|__|  \\|__|\\|__|\\|_______|\\|__|\\|__|   ___ \r\n                                                                                                                                                 |\\__\\\r\n                                                                                                                                                 \\|__|";
            string apiKey = "OPENWEATHERAPIKEY";
            string city;

            Console.WriteLine($"{title}\n\nEnter a city to view weather");
            city = Console.ReadLine().ToLower();
            
            Console.WriteLine($"\nFetching weather data for {city}...\n");

            GetWeather(city, apiKey);
        }

         static void GetWeather(string city, string apiKey)
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", city, apiKey);
                var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                foreach (var weather in Info.weather)
                {
                    Console.WriteLine($"\nCurrent Conditions: {weather.main}    Description: {weather.description}");
                }

                Console.WriteLine(
                    $"Curent Temperature: {ConvertToCelcius(Info.main.temp)}ºC     Maximum Temperature: {ConvertToCelcius(Info.main.temp_max)}ºC    Minimum Temperature: {ConvertToCelcius(Info.main.temp_min)}ºC " +
                    $"\nWind: {Info.wind.speed} m/s    Direction: {Info.wind.deg}º" +
                    $"\nSunrise: {ConvertDateTime(Info.sys.sunrise)} AM\nSunset:  {ConvertDateTime(Info.sys.sunset)} PM" +
                    $"\nPressure: {Info.main.pressure}hPa    Humidity: {Info.main.humidity}%");



            }

         static double ConvertToCelcius(double temp)
            {
                double temperature = temp - 273.15;
                
                return Math.Round(temperature,1);
            }

            DateTime ConvertDateTime(string millisec)
            {
                DateTime day = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc);
                day = day.AddSeconds(Convert.ToDouble(millisec)).ToLocalTime();
                return day;
            }
        }
    }
}
