using LaunchLocker.Interface;
using System;
using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class LockWriter : ILockWriter
    {
        public IFileSystem FileSystem { get; set; }

        public IJsonOperations JsonOperations { get; set; }

        public ILockBuilder LockBuilder { get; set; }

        public LockWriter(IFileSystem fileSystem, IJsonOperations jsonSerializer, ILockBuilder lockBuilder)
        {
            FileSystem = fileSystem ?? throw new ArgumentException(nameof(fileSystem));
            JsonOperations = jsonSerializer ?? throw new ArgumentException(nameof(jsonSerializer));
            LockBuilder = lockBuilder ?? throw new ArgumentException(nameof(lockBuilder));
        }

        public void Write()
        {
            var lockJson = JsonOperations.Serialize(LockBuilder.LaunchLock);
            FileSystem.File.WriteAllText(LockBuilder.LaunchLock.FileName, lockJson);
        }
    }
}
