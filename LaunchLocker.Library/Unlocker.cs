using System.IO.Abstractions;
using System.Runtime.InteropServices;

namespace LaunchLocker.Library;

public class Unlocker : IUnlocker
{
    private readonly ILockReader _lockReader;

    private readonly IFileSystem _fileSystem;

    private readonly ILockBuilder _lockBuilder;

    public Unlocker(IFileSystem fileSystem, ILockReader lockReader, ILockBuilder lockBuilder)
    {
        _fileSystem = fileSystem ?? throw new ArgumentException(null, nameof(fileSystem));
        _lockReader = lockReader ?? throw new ArgumentException(null, nameof(lockReader));
        _lockBuilder = lockBuilder ?? throw new ArgumentException(null, nameof(lockBuilder));
    }

    public void RemoveLock(ILaunchLock launchLock) =>
        _fileSystem.File.Delete(launchLock.FileName);

    public void RemoveObsoleteLocks(IFileInfo[] lockInfoCollection)
    {
        var username =
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
            System.Security.Principal.WindowsIdentity.GetCurrent().Name : "Non-windows user";

        var obsoleteLocks = _lockReader
            .Read(lockInfoCollection)
            .Where(x => !x.IsValid || x.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        foreach (var launchLock in obsoleteLocks)
            _fileSystem.File.Delete(launchLock.FileName);
    }
}
