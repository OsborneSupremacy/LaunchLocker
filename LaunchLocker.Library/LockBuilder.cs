using System.Runtime.InteropServices;

namespace LaunchLocker.Library;

public class LockBuilder : ILockBuilder
{
    public IConfiguration Configuration { get; set; }

    public LockBuilder(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentException(null, nameof(configuration));
    }

    public ILaunchLock LaunchLock { get; private set; }

    public void Build()
    {
        var username =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
            System.Security.Principal.WindowsIdentity.GetCurrent().Name : "Non-windows user";

        LaunchLock = new LaunchLock()
        {
            LockTime = DateTime.Now,
            Username = username,
            FileName = $"{Configuration.TargetFileInfo.FullName}.{Guid.NewGuid()}.launchlock"
        };
    }

}
