using LaunchLocker.Interface;
using LaunchLocker.Library;
using Microsoft.Extensions.DependencyInjection;

namespace LaunchLocker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceScopeFactory = ContainerConfig.Configure().GetRequiredService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var launchLockProcess = scope.ServiceProvider.GetRequiredService<ILaunchLockProcess>();
                launchLockProcess.Execute(args);
            }
        }
    }
}
