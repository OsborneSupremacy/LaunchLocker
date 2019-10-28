﻿using LaunchLocker.Interface;
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
            serviceCollection.AddSingleton<IJsonSerializer, JsonSerializer>();
            serviceCollection.AddSingleton<IConfiguration, Configuration>();
            serviceCollection.AddSingleton<ILockFinder, LockFinder>();
            serviceCollection.AddSingleton<ILockBuilder, LockBuilder>();
            serviceCollection.AddSingleton<ILockWriter, LockWriter>();

            return serviceCollection.BuildServiceProvider();
        }

    }
}
