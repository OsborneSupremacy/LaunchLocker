using LaunchLocker.Interface;
using LaunchLocker.Library;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO.Abstractions;

namespace LaunchLocker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceScopeFactory = ContainerConfig.Configure().GetRequiredService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var fileSystem = scope.ServiceProvider.GetRequiredService<IFileSystem>();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var lockReader = scope.ServiceProvider.GetRequiredService<ILockReader>();

                if (!config.CheckIfValid(args, out string message))
                    ExitWithMessage(message);

                if (lockReader.DoesLockExist())
                    ExitWithMessage("lock exists");

                ExitWithMessage("no issues");
            }
                                 
        }

        public static void ExitWithMessage(string Message)
        {
            Console.WriteLine(Message);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(-1);
        }
    }
}
