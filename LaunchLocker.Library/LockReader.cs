using System.IO.Abstractions;

namespace LaunchLocker.Library;

public class LockReader : ILockReader
{
    private readonly IFileSystem _fileSytem;

    private readonly IJsonOperations _jsonOperations;

    public LockReader(IFileSystem fileSystem, IJsonOperations jsonOperations)
    {
        _fileSytem = fileSystem ?? throw new ArgumentException(null, nameof(fileSystem));
        _jsonOperations = jsonOperations ?? throw new ArgumentException(null, nameof(jsonOperations));
    }

    public IEnumerable<ILaunchLock> Read(IFileInfo[] lockInfoCollection)
    {
        foreach (var fileInfo in lockInfoCollection)
        {
            var launchLockString = _fileSytem.File.ReadAllText(fileInfo.FullName);
            yield return _jsonOperations.Deserialize(fileInfo.FullName, launchLockString);
        }
    }
}
