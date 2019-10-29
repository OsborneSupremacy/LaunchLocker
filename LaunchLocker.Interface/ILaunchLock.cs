using System.Text.Json.Serialization;
using System;

namespace LaunchLocker.Interface
{
    /// <summary>
    /// Representation of the information contained in lock files
    /// </summary>
    public interface ILaunchLock
    {
        [JsonIgnore]
        bool IsValid { get; set; }

        [JsonIgnore]
        string FileName { get; set; }

        string Username { get; set; }

        DateTime LockTime { get; set; }
    }
}
