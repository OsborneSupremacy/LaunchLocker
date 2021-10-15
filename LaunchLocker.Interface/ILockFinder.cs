using System.IO.Abstractions;

namespace LaunchLocker.Interface
{
    /// <summary>
    /// Finds existing lock files from disk
    /// </summary>
    public interface ILockFinder
    {
        (bool lockExists, IFileInfo[] lockInfoCollection) DoesLockExist();

        (bool problemIndicatorExists, IFileInfo[] problemIndicatorCollection) DoesProblemIndicatorExist();
    }
}
