using System.Text.Json.Serialization;

namespace LaunchLocker.Library;

public class LaunchLock : ILaunchLock
{
    [JsonIgnore]
    public bool IsValid { get; set; }

    [JsonIgnore]
    public string FileName { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("locktime")]
    public DateTime LockTime { get; set; }
}
