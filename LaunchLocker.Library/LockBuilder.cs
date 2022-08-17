using System.Runtime.InteropServices;

namespace LaunchLocker.Library;

public class LockBuilder : ILockBuilder
{
    private readonly IRuntimeArgs _runtimeArgs;

    public LockBuilder(IRuntimeArgs runtimeArgs)
    {
        _runtimeArgs = runtimeArgs ?? throw new ArgumentException(null, nameof(runtimeArgs));
    }

    public ILaunchLock Build()
    {
        var username =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
            System.Security.Principal.WindowsIdentity.GetCurrent().Name : "Non-windows user";

        return new LaunchLock()
        {
            LockTime = DateTime.Now,
            Username = username,
            FileName = $"{_runtimeArgs.TargetFile.FullName}.{Guid.NewGuid()}.launchlock"
        };
    }
}
