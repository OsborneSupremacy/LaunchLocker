using System.Collections.Generic;

namespace LaunchLocker.Interface
{

    /// <summary>
    /// Reads lock json and creates ILaunchLock objects
    /// </summary>
    public interface ILockReader
    {
        IEnumerable<ILaunchLock> LaunchLocks { get; }

        void Read();
    }
}
