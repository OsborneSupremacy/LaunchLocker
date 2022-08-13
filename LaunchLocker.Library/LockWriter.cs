using System.IO.Abstractions;

namespace LaunchLocker.Library;

public class LockWriter : ILockWriter
{
    private readonly IFileSystem _fileSystem;

    private readonly IJsonOperations _jsonOperations;

    public LockWriter(IFileSystem fileSystem, IJsonOperations jsonOperations)
    {
        _fileSystem = fileSystem ?? throw new ArgumentException(null, nameof(fileSystem));
        _jsonOperations = jsonOperations ?? throw new ArgumentException(null, nameof(jsonOperations));
    }

    public void Write(ILaunchLock launchLock)
    {
        var lockJson = _jsonOperations.Serialize(launchLock);
        _fileSystem.File.WriteAllText(launchLock.FileName, lockJson);
    }
}
