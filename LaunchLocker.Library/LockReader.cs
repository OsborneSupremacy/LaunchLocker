using System.IO;
using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class LockReader
    {
        public IFileSystem FileSystem { get; set; }

        public Configuration Configuration { get; set; }

        public const string lockFileExtension = "launchlock";

        public IFileInfo[] LockInfos { get; set; }

        public LockReader(IFileSystem fileSystem, Configuration configuration)
        {
            FileSystem = fileSystem;
            Configuration = configuration;
        }

        public bool DoesLockExist()
        {
            LockInfos = Configuration.TargetFileInfo.Directory.GetFiles($@"{Configuration.TargetFileInfo.Name}.*.{lockFileExtension}");
            return (LockInfos.Length > 0);
        }
    }
}
