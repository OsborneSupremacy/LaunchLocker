using LaunchLocker.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class ContainerConfig
    {
        public static ServiceProvider Configure()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IFileSystem, FileSystem>();
            serviceCollection.AddSingleton<IConfiguration, Configuration>();
            serviceCollection.AddSingleton<ILockReader, LockReader>();

            return serviceCollection.BuildServiceProvider();
        }

    }
}
