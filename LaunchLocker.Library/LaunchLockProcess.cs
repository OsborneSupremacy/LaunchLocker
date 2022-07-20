using System;
using System.IO.Abstractions;
using LaunchLocker.Interface;

namespace LaunchLocker.Library;

public class LaunchLockProcess : ILaunchLockProcess
{
    public IConfiguration Configuration { get; set; }

    public ILockFinder LockFinder { get; set; }

    public ILockReader LockReader { get; set; }

    public ILockBuilder LockBuilder { get; set; }

    public ILockWriter LockWriter { get; set; }

    public ICommunicator Communicator { get; set; }

    public IFileSystem FileSystem { get; set; }

    public IUnlocker Unlocker { get; set; }

    public ILauncher Launcher { get; set; }

    public LaunchLockProcess(
        IConfiguration configuration,
        ILockFinder lockFinder,
        ILockReader lockReader,
        ILockBuilder lockBuilder,
        ILockWriter lockWriter,
        ICommunicator communicator,
        IFileSystem fileSystem,
        IUnlocker unlocker,
        ILauncher launcher)
    {
        Configuration = configuration ?? throw new ArgumentException(null, nameof(configuration));
        LockFinder = lockFinder ?? throw new ArgumentException(null, nameof(lockFinder));
        LockBuilder = lockBuilder ?? throw new ArgumentException(null, nameof(lockBuilder));
        LockReader = lockReader ?? throw new ArgumentException(null, nameof(lockReader));
        LockWriter = lockWriter ?? throw new ArgumentException(null, nameof(lockWriter));
        Communicator = communicator ?? throw new ArgumentException(null, nameof(communicator));
        FileSystem = fileSystem ?? throw new ArgumentException(null, nameof(fileSystem));
        Unlocker = unlocker ?? throw new ArgumentException(null, nameof(unlocker));
        Launcher = launcher ?? throw new ArgumentException(null, nameof(launcher));
    }

    public bool Execute(string[] args)
    {
        if (!Configuration.CheckIfValid(args, out string message))
        {
            Communicator.WriteSentence(message);
            Communicator.Exit();
            return false;
        }

        var (lockExists, lockInfoCollection) = LockFinder.DoesLockExist();

        if (lockExists)
        {
            LockReader.Read(lockInfoCollection);

            Unlocker.RemoveObsoleteLocks();

            (lockExists, lockInfoCollection) = LockFinder.DoesLockExist();

            if (lockExists) // see if locks still exist
            {
                LockReader.Read(lockInfoCollection);
                Communicator.WriteSentence("File is locked and should not be opened.");
                Communicator.WriteLockInfo(LockReader.LaunchLocks);
                Communicator.WriteSentence("Locks can be manually deleted if you believe them to be obsolete.");
                Communicator.Exit();
                return false;
            }
        }

        var (problemIndicatorExists, problemIndicatorCollection) = LockFinder.DoesProblemIndicatorExist();

        if (problemIndicatorExists)
        {
            Communicator.WriteSentence("Synchronization problem found.");
            Communicator.WriteSentence("File is not locked, however there are files that suggest there may be a synchronization problem. The files are:");
            foreach (var pi in problemIndicatorCollection)
                Communicator.WriteSentence(pi.Name);
            Communicator.WriteSentence("This should be investigated and manually resolved.");
            Communicator.WriteSentence("If this is a false positive, the problem indicator configuration will need to be adjusted.");
            return false;
        }

        LockBuilder.Build();
        LockWriter.Write();
        Communicator.WriteSentence("Lock file created.");

        Communicator.WriteSentence("Launching file.");

        try
        {
            Launcher.Run();
            Communicator.WriteSentence("File closed.");
        }
        catch
        {
            Communicator.WriteSentence("Process ended unexpectedly.");
        }
        finally
        {
            Unlocker.RemoveLock();
        }

        Communicator.WriteSentence("Lock file removed.");
        return true;
    }

}
