using LaunchLocker.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LaunchLocker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) => {

                    services.AddHostedService<ConsoleHostedService>();
                    services.RegisterServices();

                    services.AddOptions();
                    services.Configure<Settings>(configuration.GetSection("Settings"));
                })
                .RunConsoleAsync()
                .GetAwaiter()
                .GetResult(); 
        }
    }
}
