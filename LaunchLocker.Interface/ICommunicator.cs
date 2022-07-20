namespace LaunchLocker.Interface;

/// <summary>
/// Interact with user.
/// 
/// This is generally going to be writing messages.
/// </summary>
public interface ICommunicator
{
    void Write(string message);

    void WriteSentence(string message);

    void Exit();

    void WriteLockInfo(IEnumerable<ILaunchLock> launchLocks);
}
