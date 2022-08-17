using System.IO.Abstractions;
using System.Threading.Tasks;
using LaunchLocker.Interface;
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

        var configuration = builder.Build();

        await new RuntimeArgsBuilder().Build(new FileSystem(), args)
            .Match
            (
                async runtimeArgs =>
                {
                    return await RunAsync(configuration, runtimeArgs);
                },
                exception =>
                {
                    return Task.FromResult(false);
                }
            );
    }

    protected static async Task<bool> RunAsync(IConfigurationRoot configuration, IRuntimeArgs args)
    {
        await Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ConsoleHostedService>();

                services.AddSingleton(args);
                services.RegisterServices();

                services.Configure<Settings>(configuration.GetSection(nameof(Settings)));

                services.AddOptions();

                services.AddSingleton(sp =>
                    sp.GetRequiredService<IOptions<Settings>>().Value);
            })
            .RunConsoleAsync();
        return true;
    }

}
