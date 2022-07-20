using System.IO.Abstractions;

namespace LaunchLocker.Library;

public class LockFinder : ILockFinder
{
    private readonly Settings _settings;

    public IFileSystem FileSystem { get; set; }

    public IConfiguration Configuration { get; set; }

    public const string LockFileExtension = "launchlock";

    public LockFinder(
        IFileSystem fileSystem,
        IConfiguration configuration,
        Settings settings
    )
    {
        FileSystem = fileSystem ?? throw new ArgumentException(null, nameof(fileSystem));
        Configuration = configuration ?? throw new ArgumentException(null, nameof(configuration));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public (bool lockExists, IFileInfo[] lockInfoCollection) DoesLockExist()
    {
        var lockInfoCollection = Configuration.TargetFileInfo.Directory.GetFiles($@"{Configuration.TargetFileInfo.Name}.*.{LockFileExtension}");
        var lockExists = (lockInfoCollection.Length > 0);
        return (lockExists, lockInfoCollection);
    }

    public (bool problemIndicatorExists, IFileInfo[] problemIndicatorCollection) DoesProblemIndicatorExist()
    {
        if (!_settings.ProblemIndicators.Any())
            return (false, Enumerable.Empty<IFileInfo>().ToArray());

        var allFiles = Configuration
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
