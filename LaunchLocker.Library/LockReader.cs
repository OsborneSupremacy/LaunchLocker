using LaunchLocker.Interface;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class LockReader : ILockReader
    {
        public IFileSystem FileSytem { get; set; }

        public ILockFinder LockFinder { get; set; }

        public IJsonOperations JsonOperations { get; set; }

        public IEnumerable<ILaunchLock> LaunchLocks { get; set; }

        public LockReader(IFileSystem fileSystem, ILockFinder lockFinder, IJsonOperations jsonOperations)
        {
            FileSytem = fileSystem ?? throw new ArgumentException(nameof(fileSystem));
            LockFinder = lockFinder ?? throw new ArgumentException(nameof(lockFinder));
            JsonOperations = jsonOperations ?? throw new ArgumentException(nameof(jsonOperations));
        }

        public void Read()
        {
            var launchLocks = new List<LaunchLock>();

            foreach (var fileInfo in LockFinder.LockInfoCollection) {
                var lauchLockString = FileSytem.File.ReadAllText(fileInfo.FullName);
                launchLocks.Add(JsonOperations.Deserialize(fileInfo.FullName, lauchLockString) as LaunchLock);
            }

            LaunchLocks = launchLocks;
        }
    }
}
