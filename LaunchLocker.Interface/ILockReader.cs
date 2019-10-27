using System.IO.Abstractions;

namespace LaunchLocker.Interface
{
    public interface ILockReader
    {
        IFileInfo[] LockInfoCollection { get; set; }

        bool DoesLockExist();
    }
}
