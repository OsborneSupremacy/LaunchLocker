using System.IO.Abstractions;
using LaunchLocker.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace LaunchLocker.Library;

public static class RegistrationService
{
    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IFileSystem, FileSystem>();
        serviceCollection.AddSingleton<IJsonOperations, JsonOperations>();
        serviceCollection.AddSingleton<IConfiguration, Configuration>();
        serviceCollection.AddSingleton<ILockFinder, LockFinder>();
        serviceCollection.AddSingleton<ILockBuilder, LockBuilder>();
        serviceCollection.AddSingleton<ILockWriter, LockWriter>();
        serviceCollection.AddSingleton<ILockReader, LockReader>();
        serviceCollection.AddSingleton<ICommunicator, ConsoleCommunicator>();
        serviceCollection.AddSingleton<ILaunchLockProcess, LaunchLockProcess>();
        serviceCollection.AddSingleton<IUnlocker, Unlocker>();
        serviceCollection.AddSingleton<ILauncher, Launcher>();
        return serviceCollection;
    }

}
