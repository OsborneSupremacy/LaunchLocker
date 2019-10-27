using System.IO;
using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class LockReader
    {
        public IFileSystem FileSystem { get; set; }

        public IFileInfo TargetFileInfo { get; set; }

        public const string lockFileExtension = ".launchlock";

        public IFileInfo[] LockInfos { get; set; }

        public LockReader(IFileSystem fileSystem, IFileInfo targetFileInfo)
        {
            FileSystem = fileSystem;
            TargetFileInfo = targetFileInfo;
        }

        public bool DoesLockExist()
        {
            LockInfos = TargetFileInfo.Directory.GetFiles($"*.{lockFileExtension}");
            return (LockInfos.Length > 0);
        }

    }
}
