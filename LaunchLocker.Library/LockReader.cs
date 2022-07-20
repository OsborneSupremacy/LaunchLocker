using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using LaunchLocker.Interface;

namespace LaunchLocker.Library;

public class LockReader : ILockReader
{
    public IFileSystem FileSytem { get; set; }

    public IJsonOperations JsonOperations { get; set; }

    public IEnumerable<ILaunchLock> LaunchLocks { get; set; }

    public LockReader(IFileSystem fileSystem, IJsonOperations jsonOperations)
    {
        FileSytem = fileSystem ?? throw new ArgumentException(null, nameof(fileSystem));
        JsonOperations = jsonOperations ?? throw new ArgumentException(null, nameof(jsonOperations));
    }

    public void Read(IFileInfo[] lockInfoCollection)
    {
        var launchLocks = new List<LaunchLock>();

        foreach (var fileInfo in lockInfoCollection)
        {
            var launchLockString = FileSytem.File.ReadAllText(fileInfo.FullName);
            launchLocks.Add(JsonOperations.Deserialize(fileInfo.FullName, launchLockString) as LaunchLock);
        }

        LaunchLocks = launchLocks;
    }
}
