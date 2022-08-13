using System.IO.Abstractions;

namespace LaunchLocker.Interface;

public interface IUnlocker
{
    void RemoveLock(ILaunchLock launchLock);

    void RemoveObsoleteLocks(IFileInfo[] lockInfoCollection);

}
