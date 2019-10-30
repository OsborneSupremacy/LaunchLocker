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

        public ILockBuilder LockBuilder { get; set; }

        public ILockWriter LockWriter { get; set; }

        public ICommunicator Communicator { get; set; }

        public IFileSystem FileSystem { get; set; }

        public LaunchLockProcess(
            IConfiguration configuration,
            ILockFinder lockFinder,
            ILockReader lockReader,
            ILockBuilder lockBuilder,
            ILockWriter lockWriter,
            ICommunicator communicator, 
            IFileSystem fileSystem)
        {
            Configuration = configuration ?? throw new System.ArgumentException(nameof(configuration));
            LockFinder = lockFinder ?? throw new ArgumentException(nameof(lockFinder));
            LockBuilder = lockBuilder ?? throw new ArgumentException(nameof(lockBuilder));
            LockReader = lockReader ?? throw new ArgumentException(nameof(lockReader));
            LockWriter = lockWriter ?? throw new ArgumentException(nameof(lockWriter));
            Communicator = communicator ?? throw new ArgumentException(nameof(communicator));
            FileSystem = fileSystem ?? throw new ArgumentException(nameof(fileSystem));
        }

        public void Execute(string[] args)
        {
            if (!Configuration.CheckIfValid(args, out string message))
            {
                Communicator.WriteSentence(message);
                Communicator.Exit();
                return;
            }

            if (LockFinder.DoesLockExist())
            {
                LockReader.Read();
                // Unlocker.RemoveBadLocks(); // remove malformed locks, or locks from current user
                if(LockFinder.DoesLockExist()) // after removing bad locks, see if locks still exist
                {
                    LockReader.Read();
                    Communicator.WriteSentence("File is locked and should not be opened.");
                    //Communicator.WriteLockInfo(LockReader.LaunchLocks);
                    Communicator.WriteSentence("Locks can be manually deleted if you believe them to be obsolete.");
                    Communicator.Exit();
                    return;
                }
            }

            LockBuilder.Build();
            LockWriter.Write();
            Communicator.WriteSentence("Lock file created.");

            Communicator.WriteSentence("Launching file.");
            // Launcher.Launch();
            // Unlocker.RemoveLock();
            Communicator.WriteSentence("Lock file removed.");
            return;
        }

    }
}
