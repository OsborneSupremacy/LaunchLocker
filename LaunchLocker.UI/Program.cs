using LaunchLocker.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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

                    services.Configure<Settings>(configuration.GetSection(nameof(Settings)));

                    services.AddOptions();

                    services.AddSingleton(sp =>
                        sp.GetRequiredService<IOptions<Settings>>().Value);

                })
                .RunConsoleAsync()
                .GetAwaiter()
                .GetResult(); 
        }
    }
}
