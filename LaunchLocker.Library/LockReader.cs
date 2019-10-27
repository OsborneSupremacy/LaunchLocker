using LaunchLocker.Interface;
using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class LockReader : ILockReader
    {
        public IFileSystem FileSystem { get; set; }

        public IConfiguration Configuration { get; set; }

        public const string lockFileExtension = "launchlock";

        public IFileInfo[] LockInfoCollection { get; set; }

        public LockReader(IFileSystem fileSystem, IConfiguration configuration)
        {
            FileSystem = fileSystem ?? throw new System.ArgumentException(nameof(fileSystem));
            Configuration = configuration ?? throw new System.ArgumentException(nameof(configuration));
        }

        public bool DoesLockExist()
        {
            LockInfoCollection = Configuration.TargetFileInfo.Directory.GetFiles($@"{Configuration.TargetFileInfo.Name}.*.{lockFileExtension}");
            return (LockInfoCollection.Length > 0);
        }
    }
}
