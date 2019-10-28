using LaunchLocker.Interface;
using System;

namespace LaunchLocker.Library
{
    public class LockBuilder : ILockBuilder
    {
        public IConfiguration Configuration { get; set; }

        public LockBuilder(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new System.ArgumentException(nameof(configuration));
        }

        public ILaunchLock LaunchLock { get; private set; }

        public void Build()
        {
            LaunchLock = new LaunchLock()
            {
                LockTime = DateTime.Now,
                Username = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                FileName = $"{Configuration.TargetFileInfo.FullName}.{Guid.NewGuid()}.launchlock"
            };
        }    

    }
}
