namespace LaunchLocker.Interface
{
    public interface IUnlocker
    {
        void RemoveLock();

        void RemoveObsoleteLocks();

    }
}
