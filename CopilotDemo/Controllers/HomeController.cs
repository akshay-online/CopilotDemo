using CopilotDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CopilotDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public string GetWeatherDescription(WeatherData data)
        {
            switch (data.Temperature)
            {
                case 0:
                    return "Freezing";
                case 15:
                    return "Cold";
                case 30:
                    return "Hot";
                case 50:
                    return "Very Hot";
                case 60:
                    return "Extremely Hot";
                default:
                    return "Unknown";
            }
        }

        public void LoadWeatherData()
        {
            var weather = new WeatherData { City = "New York", Temperature = 22 };
            var updatedWeather = new WeatherData { City = weather.City, Temperature = 25 };
        }
        public class WeatherService
        {
            public WeatherData GetWeather(string json)
            {
                return JsonConvert.DeserializeObject<WeatherData>(json);
            }
        }

        public async Task<string> FetchWeatherDataAsync(string city)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://api.weather.com/{city}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }

        public IEnumerable<WeatherData> FilterWarmWeather(IEnumerable<WeatherData> data)
        {
            return data.Where(d => d.Temperature > 20).ToList();
        }
    }
}
