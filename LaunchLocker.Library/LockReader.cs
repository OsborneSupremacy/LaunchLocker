using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LaunchLocker.Library
{
    public class LockReader
    {
        public FileInfo TargetFileInfo { get; set; }

        public const string lockFileExtension = ".launchlock";

        public FileInfo[] LockInfos { get; set; }

        public LockReader(FileInfo targetFileInfo)
        {
            TargetFileInfo = targetFileInfo;
        }

        public bool DoesLockExist()
        {
            LockInfos = TargetFileInfo.Directory.GetFiles($"*.{lockFileExtension}");
            return (LockInfos.Length > 0);
        }

    }
}
