using LaunchLocker.Interface;
using System.IO.Abstractions;
using System;

namespace LaunchLocker.Library
{
    public class LockFinder : ILockFinder
    {
        public IFileSystem FileSystem { get; set; }

        public IConfiguration Configuration { get; set; }

        public const string lockFileExtension = "launchlock";

        public IFileInfo[] LockInfoCollection { get; set; }

        public LockFinder(IFileSystem fileSystem, IConfiguration configuration)
        {
            FileSystem = fileSystem ?? throw new ArgumentException(nameof(fileSystem));
            Configuration = configuration ?? throw new ArgumentException(nameof(configuration));
        }

        public bool DoesLockExist()
        {
            LockInfoCollection = Configuration.TargetFileInfo.Directory.GetFiles($@"{Configuration.TargetFileInfo.Name}.*.{lockFileExtension}");
            return (LockInfoCollection.Length > 0);
        }
    }
}
