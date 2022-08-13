using System.IO.Abstractions;

namespace LaunchLocker.Interface;

/// <summary>
/// Reads lock json and creates ILaunchLock objects
/// </summary>
public interface ILockReader
{
    IEnumerable<ILaunchLock> Read(IFileInfo[] lockInfoCollection);
}
