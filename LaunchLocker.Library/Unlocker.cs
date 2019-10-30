using LaunchLocker.Interface;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace LaunchLocker.Library
{
    public class Unlocker : IUnlocker
    {
        public ILockReader LockReader { get; set; }

        public IFileSystem FileSystem { get; set; }

        public ILockBuilder LockBuilder { get; set; }

        public Unlocker(IFileSystem fileSystem, ILockReader lockReader, ILockBuilder lockBuilder)
        {
            FileSystem = fileSystem ?? throw new ArgumentException(nameof(fileSystem));
            LockReader = lockReader ?? throw new ArgumentException(nameof(lockReader));
            LockBuilder = lockBuilder ?? throw new ArgumentException(nameof(lockBuilder));
        }

        public void RemoveLock() =>
            FileSystem.File.Delete(LockBuilder.LaunchLock.FileName);            

        public void RemoveObsoleteLocks()
        {
            var currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            var obsoleteLocks = LockReader
                .LaunchLocks
                .Where(x => !x.IsValid || x.Username.Equals(currentUser, StringComparison.OrdinalIgnoreCase)).ToList();

            foreach (var launchLock in obsoleteLocks)
                FileSystem.File.Delete(launchLock.FileName);
        }
    }
}
