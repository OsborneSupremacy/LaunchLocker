using System;
using System.IO.Abstractions;
using LaunchLocker.Interface;

namespace LaunchLocker.Library;

public class LockWriter : ILockWriter
{
    public IFileSystem FileSystem { get; set; }

    public IJsonOperations JsonOperations { get; set; }

    public ILockBuilder LockBuilder { get; set; }

    public LockWriter(IFileSystem fileSystem, IJsonOperations jsonSerializer, ILockBuilder lockBuilder)
    {
        FileSystem = fileSystem ?? throw new ArgumentException(null, nameof(fileSystem));
        JsonOperations = jsonSerializer ?? throw new ArgumentException(null, nameof(jsonSerializer));
        LockBuilder = lockBuilder ?? throw new ArgumentException(null, nameof(lockBuilder));
    }

    public void Write()
    {
        var lockJson = JsonOperations.Serialize(LockBuilder.LaunchLock);
        FileSystem.File.WriteAllText(LockBuilder.LaunchLock.FileName, lockJson);
    }
}
