using LaunchLocker.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace LaunchLocker.UI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            await Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddHostedService<ConsoleHostedService>();
                    services.RegisterServices();

                    services.Configure<Settings>(configuration.GetSection(nameof(Settings)));

                    services.AddOptions();

                    services.AddSingleton(sp =>
                        sp.GetRequiredService<IOptions<Settings>>().Value);

                })
                .RunConsoleAsync();

        }
    }
}
