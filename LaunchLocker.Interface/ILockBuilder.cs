
namespace LaunchLocker.Interface
{
    /// <summary>
    /// Build ILaunchLock objects
    /// </summary>
    public interface ILockBuilder
    {
        ILaunchLock LaunchLock { get; }

        void Build();
    }
}
