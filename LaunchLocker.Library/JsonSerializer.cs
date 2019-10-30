using LaunchLocker.Interface;
using System.Text.Json;

namespace LaunchLocker.Library
{
    public class JsonOperations : IJsonOperations
    {
        public ILaunchLock Deserialize(string fileName, string input)
        {
            LaunchLock launchLock;
            try
            {
                launchLock = JsonSerializer.Deserialize<LaunchLock>(input);
                launchLock.IsValid = true;
            }
            catch (JsonException ex)
            {
                launchLock = new LaunchLock() { IsValid = false };
            }
            launchLock.FileName = fileName;
            return launchLock;
        }

        public string Serialize(object input) =>
            JsonSerializer.Serialize(input);
    }
}
