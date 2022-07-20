namespace LaunchLocker.Interface;

public interface IJsonOperations
{
    string Serialize(object input);

    ILaunchLock Deserialize(string fileName, string input);
}
