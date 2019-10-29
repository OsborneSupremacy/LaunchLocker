
namespace LaunchLocker.Interface
{
    /// <summary>
    /// Builds ILaunchLock objects
    /// </summary>
    public interface ILockBuilder
    {
        ILaunchLock LaunchLock { get; }

        void Build();
    }
}
