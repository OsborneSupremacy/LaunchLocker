
using System;

namespace LaunchLocker.Interface
{
    /// <summary>
    /// Representation of the information contained in lock files
    /// </summary>
    public interface ILaunchLock
    {
        bool IsValid { get; set; }

        string FileName { get; set; }

        string Username { get; set; }

        DateTime LockTime { get; set; }
    }
}
