using System.Runtime.InteropServices;

namespace LaunchLocker.Library;

public class LockBuilder : ILockBuilder
{
    private readonly RuntimeArgs _runtimeArgs;

    public LockBuilder(RuntimeArgs runtimeArgs)
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
            FileName = $"{_runtimeArgs.TargetFileInfo.FullName}.{Guid.NewGuid()}.launchlock"
        };
    }
}
