using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.Runtime.InteropServices;

namespace WeatherApp
{
    internal class Program
    {
        string apiKey = "91274abeabcd1ff8b14dc582b7d94e96";
        public string city;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a city to view weather");
            
        }

        void GetWeather()
        {
            using (WebClient client = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q=london&appid=91274abeabcd1ff8b14dc582b7d94e96", city , apiKey);
                var json = client.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                
                
                
            }

        }
    }
}
