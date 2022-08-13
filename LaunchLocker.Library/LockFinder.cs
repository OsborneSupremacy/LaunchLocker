using System.IO.Abstractions;

namespace LaunchLocker.Library;

public class LockFinder : ILockFinder
{
    private readonly Settings _settings;

    private readonly RuntimeArgs _runtimeArgs;

    private const string LockFileExtension = "launchlock";

    public LockFinder(
        RuntimeArgs runtimeArgs,
        Settings settings
    )
    {
        _runtimeArgs = runtimeArgs ?? throw new ArgumentException(null, nameof(runtimeArgs));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public (bool lockExists, IFileInfo[] lockInfoCollection) DoesLockExist()
    {
        var lockInfoCollection = _runtimeArgs.TargetFileInfo.Directory.GetFiles($@"{_runtimeArgs.TargetFileInfo.Name}.*.{LockFileExtension}");
        var lockExists = lockInfoCollection.Length > 0;
        return (lockExists, lockInfoCollection);
    }

    public (bool problemIndicatorExists, IFileInfo[] problemIndicatorCollection) DoesProblemIndicatorExist()
    {
        if (!_settings.ProblemIndicators.Any())
            return (false, Enumerable.Empty<IFileInfo>().ToArray());

        var allFiles = _runtimeArgs
            .TargetFileInfo
            .Directory
            .GetFiles();

        var problemFiles = new HashSet<IFileInfo>();

        foreach (var file in allFiles)
            foreach (var indicator in _settings.ProblemIndicators)
            {
                if (file.Name.ToLowerInvariant().Contains(indicator.ToLowerInvariant()))
                {
                    problemFiles.Add(file);
                    break;
                }
            }

        return (problemFiles.Any(), problemFiles.ToArray());
    }

}
