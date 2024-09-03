using CopilotDemo.IServices;
using Microsoft.Extensions.DependencyInjection;
using CopilotDemo.Services;

namespace CopilotDemo.Extensions
{
    public class ServiceCollectionExtensions
    {
        public static void AddCustomServices(IServiceCollection services)
        {
            services.AddTransient<IWeatherService, WeatherService>();
        }
    }
}
