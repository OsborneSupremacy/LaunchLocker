using System.Text.Json;
using LaunchLocker.Interface;

namespace LaunchLocker.Library;

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
        catch (JsonException)
        {
            launchLock = new LaunchLock() { IsValid = false };
        }
        launchLock.FileName = fileName;
        return launchLock;
    }

    public string Serialize(object input) =>
        JsonSerializer.Serialize(input);
}
