using System.IO.Abstractions;
using System.Runtime.InteropServices;

namespace LaunchLocker.Library;

public class Unlocker : IUnlocker
{
    public ILockReader LockReader { get; set; }

    public IFileSystem FileSystem { get; set; }

    public ILockBuilder LockBuilder { get; set; }

    public Unlocker(IFileSystem fileSystem, ILockReader lockReader, ILockBuilder lockBuilder)
    {
        FileSystem = fileSystem ?? throw new ArgumentException(null, nameof(fileSystem));
        LockReader = lockReader ?? throw new ArgumentException(null, nameof(lockReader));
        LockBuilder = lockBuilder ?? throw new ArgumentException(null, nameof(lockBuilder));
    }

    public void RemoveLock() =>
        FileSystem.File.Delete(LockBuilder.LaunchLock.FileName);

    public void RemoveObsoleteLocks()
    {
        var username =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
            System.Security.Principal.WindowsIdentity.GetCurrent().Name : "Non-windows user";

        var obsoleteLocks = LockReader
            .LaunchLocks
            .Where(x => !x.IsValid || x.Username.Equals(username, StringComparison.OrdinalIgnoreCase)).ToList();

        foreach (var launchLock in obsoleteLocks)
            FileSystem.File.Delete(launchLock.FileName);
    }
}
