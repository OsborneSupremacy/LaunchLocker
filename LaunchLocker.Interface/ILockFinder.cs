using System.IO.Abstractions;

namespace LaunchLocker.Interface
{
    /// <summary>
    /// Finds existing lock files from disk
    /// </summary>
    public interface ILockFinder
    {
        IFileInfo[] LockInfoCollection { get; set; }

        bool DoesLockExist();
    }
}
