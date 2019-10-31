using System;
using System.Text.Json.Serialization;

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

        [JsonPropertyName("username")]
        string Username { get; set; }

        [JsonPropertyName("locktime")]
        DateTime LockTime { get; set; }
    }
}
