
using System;

namespace LaunchLocker.Interface
{
    /// <summary>
    /// Representation of the information contained in lock files
    /// </summary>
    public interface ILaunchLock
    {
        string FileName { get; set; }

        string Username { get; set; }

        DateTime LockTime { get; set; }
    }
}
