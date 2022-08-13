namespace LaunchLocker.Interface;

public interface ILaunchLockProcess
{
    Task<bool> ExecuteAsync();
}
