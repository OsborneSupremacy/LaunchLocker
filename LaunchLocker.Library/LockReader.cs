using LaunchLocker.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class LockReader : ILockReader
    {
        public IFileSystem FileSytem { get; set; }

        public ILockFinder LockFinder { get; set; }

        public IEnumerable<ILaunchLock> LaunchLocks { get; set; }

        public LockReader(IFileSystem fileSystem, ILockFinder lockFinder)
        {
            FileSytem = fileSystem ?? throw new ArgumentException(nameof(fileSystem));
            LockFinder = lockFinder ?? throw new ArgumentException(nameof(lockFinder));
        }

        public void Read()
        {
            var launchLocks = new List<LaunchLock>();

            foreach (var fileInfo in LockFinder.LockInfoCollection) {
                var lauchLockString = FileSytem.File.ReadAllText(fileInfo.FullName);
                try
                {
                    var launchLock = JsonConvert.DeserializeObject<LaunchLock>(lauchLockString);
                    launchLock.FileName = fileInfo.FullName;
                    launchLock.IsValid = true;
                    launchLocks.Add(launchLock);
                } catch (JsonReaderException)
                {
                    launchLocks.Add(new LaunchLock() { IsValid = false });
                }
            }

            LaunchLocks = launchLocks;
        }
    }
}
