using System.IO.Abstractions;
using System.Threading.Tasks;
using LaunchLocker.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace LaunchLocker.UI;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

        var runtimeArgs = new RuntimeArgs(new FileSystem(), args);

        var configuration = builder.Build();

        await Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ConsoleHostedService>();

                services.AddSingleton(runtimeArgs);
                services.RegisterServices();

                services.Configure<Settings>(configuration.GetSection(nameof(Settings)));

                services.AddOptions();

                services.AddSingleton(sp =>
                    sp.GetRequiredService<IOptions<Settings>>().Value);

            })
            .RunConsoleAsync();

    }
}
