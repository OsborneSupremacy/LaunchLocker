using LaunchLocker.Interface;
using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class Configuration : IConfiguration
    {
        public IFileSystem FileSystem { get; set; }

        public Configuration(IFileSystem fileSystem)
        {
            FileSystem = fileSystem;
        }

        public IFileInfo TargetFileInfo { get; private set; }

        public bool CheckIfValid(string[] args, out string message)
        {
            message = string.Empty;

            if (args.Length == 0)
            {
                message = "At least one command line argument is required.";
                return false;
            };

            string targetFileName = args[0];

            if (string.IsNullOrEmpty(targetFileName))
            {
                message = "The first command line argument should be the file to be lauched.";
                return false;
            }

            TargetFileInfo = FileSystem.FileInfo.FromFileName(targetFileName);

            if (!TargetFileInfo.Exists)
            {
                message = $"File `{TargetFileInfo.FullName}` not found";
                return false;
            }

            return true;
        }
    }
}
