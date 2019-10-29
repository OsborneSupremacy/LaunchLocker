using System;
using LaunchLocker.Interface;
using Newtonsoft.Json;

namespace LaunchLocker.Library
{
    public class LaunchLock : ILaunchLock
    {
        [JsonIgnore]
        public bool IsValid { get; set; }

        [JsonIgnore]
        public string FileName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("locktime")]
        public DateTime LockTime { get; set; }
    }
}
