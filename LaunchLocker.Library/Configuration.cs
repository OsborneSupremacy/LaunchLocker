using System.IO.Abstractions;

namespace LaunchLocker.Library
{
    public class Configuration
    {
        public IFileSystem FileSystem { get; set; }

        public string[] Args { get; set; }

        public Configuration(IFileSystem fileSystem, string[] args)
        {
            FileSystem = fileSystem;
            Args = args;
        }

        public IFileInfo TargetFileInfo { get; private set; }

        public bool CheckIfValid(out string message)
        {
            message = string.Empty;

            if (Args.Length == 0)
            {
                message = "At least one command line argument is required.";
                return false;
            };

            string targetFileName = Args[0];

            if(string.IsNullOrEmpty(targetFileName))
            {
                message = "The first command line argument should be the file to be lauched.";
                return false;
            }

            TargetFileInfo = FileSystem.FileInfo.FromFileName(targetFileName);

            if(!TargetFileInfo.Exists)
            {
                message = $"File `{TargetFileInfo.FullName}` not found";
                return false;
            }

            return true;
        }
    }
}
