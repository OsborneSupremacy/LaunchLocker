using LaunchLocker.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class LaunchLockProcess : ILaunchLockProcess
    {
        public IConfiguration Configuration { get; set; }

        public ILockFinder LockFinder { get; set; }

        public ILockReader LockReader { get; set; }

        public ILockWriter LockWriter { get; set; }

        public ICommunicator Communicator { get; set; }

        public IFileSystem FileSystem { get; set; }

        public LaunchLockProcess(
            IConfiguration configuration,
            ILockFinder lockFinder,
            ILockReader lockReader,
            ILockWriter lockWriter,
            ICommunicator communicator, 
            IFileSystem fileSystem)
        {
            Configuration = configuration ?? throw new System.ArgumentException(nameof(configuration));
            LockFinder = lockFinder ?? throw new ArgumentException(nameof(lockFinder));
            LockReader = lockReader ?? throw new ArgumentException(nameof(lockReader));
            LockWriter = lockWriter ?? throw new ArgumentException(nameof(lockWriter));
            Communicator = communicator ?? throw new ArgumentException(nameof(communicator));
            FileSystem = fileSystem ?? throw new ArgumentException(nameof(fileSystem));
        }

        public void Execute(string[] args)
        {
            if (!Configuration.CheckIfValid(args, out string message))
                ExitWithMessage(message);

            if (LockFinder.DoesLockExist())
            {
                LockReader.Read();
                // Unlocker.RemoveBadLocks(); // remove malformed locks, or locks from current user
                if(LockFinder.DoesLockExist()) // after removing bad locks, see if locks still exist
                {
                    LockReader.Read();
                    //Communicator.WriteLockInfo(LockReader.LaunchLocks);
                    Communicator.Exit();
                }
            }

            LockWriter.Write();
            Communicator.WriteSentence("Lock file created.");

            // Launcher.Launch();
            // Unlocker.RemoveLock();
            Communicator.WriteSentence("Lock file removed.");
        }

        public void ExitWithMessage(string Message)
        {
            Communicator.WriteSentence(Message);
            Communicator.Exit();
        }
    }
}
