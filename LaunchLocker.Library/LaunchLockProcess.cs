namespace LaunchLocker.Library;

public class LaunchLockProcess : ILaunchLockProcess
{
    private readonly ILockFinder _lockFinder;

    private readonly ILockReader _lockReader;

    private readonly ILockBuilder _lockBuilder;

    private readonly ILockWriter _lockWriter;

    private readonly ICommunicator _communicator;

    private readonly IUnlocker _unlocker;

    private readonly ILauncher _launcher;

    public LaunchLockProcess(
        ILockFinder lockFinder,
        ILockReader lockReader,
        ILockBuilder lockBuilder,
        ILockWriter lockWriter,
        ICommunicator communicator,
        IUnlocker unlocker,
        ILauncher launcher)
    {
        _lockFinder = lockFinder ?? throw new ArgumentException(null, nameof(lockFinder));
        _lockBuilder = lockBuilder ?? throw new ArgumentException(null, nameof(lockBuilder));
        _lockReader = lockReader ?? throw new ArgumentException(null, nameof(lockReader));
        _lockWriter = lockWriter ?? throw new ArgumentException(null, nameof(lockWriter));
        _communicator = communicator ?? throw new ArgumentException(null, nameof(communicator));
        _unlocker = unlocker ?? throw new ArgumentException(null, nameof(unlocker));
        _launcher = launcher ?? throw new ArgumentException(null, nameof(launcher));
    }

    public async Task<bool> ExecuteAsync()
    {
        var (lockExists, lockInfoCollection) = _lockFinder.DoesLockExist();

        if (lockExists)
        {
            _unlocker.RemoveObsoleteLocks(lockInfoCollection);

            (lockExists, lockInfoCollection) = _lockFinder.DoesLockExist();

            if (lockExists) // see if locks still exist
            {
                _communicator.WriteSentence("File is locked and should not be opened.");
                _communicator.WriteLockInfo(_lockReader.Read(lockInfoCollection));
                _communicator.WriteSentence("Locks can be manually deleted if you believe them to be obsolete.");
                _communicator.Exit();
                return false;
            }
        }

        var (problemIndicatorExists, problemIndicatorCollection) = _lockFinder.DoesProblemIndicatorExist();

        if (problemIndicatorExists)
        {
            _communicator.WriteSentence("Synchronization problem found.");
            _communicator.WriteSentence("File is not locked, however there are files that suggest there may be a synchronization problem. The files are:");
            foreach (var pi in problemIndicatorCollection)
                _communicator.WriteSentence(pi.Name);
            _communicator.WriteSentence("This should be investigated and manually resolved.");
            _communicator.WriteSentence("If this is a false positive, the problem indicator configuration will need to be adjusted.");
            return false;
        }

        var lauchLock = _lockBuilder.Build();
        _lockWriter.Write(lauchLock);
        _communicator.WriteSentence("Lock file created.");

        _communicator.WriteSentence("Launching file.");

        try
        {
            await _launcher.RunAsync();
            _communicator.WriteSentence("File closed.");
        }
        catch
        {
            _communicator.WriteSentence("Process ended unexpectedly.");
        }
        finally
        {
            _unlocker.RemoveLock(lauchLock);
        }

        _communicator.WriteSentence("Lock file removed.");
        return true;
    }

}
