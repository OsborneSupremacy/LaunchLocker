using LaunchLocker.Interface;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace LaunchLocker.Library
{
    public class Configuration : IConfiguration
    {
        public IFileSystem FileSystem { get; set; }

        public IEnumerable<string> TargetClas { get; set; }

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
                message = "The first command line argument should be the file to be launched.";
                return false;
            }

            TargetFileInfo = FileSystem.FileInfo.FromFileName(targetFileName);

            if (!TargetFileInfo.Exists)
            {
                message = $"File `{TargetFileInfo.FullName}` not found";
                return false;
            }

            // handle additional CLAs
            var targetClas = new List<string>();
            if(args.Length > 1)            
                targetClas.AddRange(args.Skip(1)); // don't return the first CLA, since that's the target file

            TargetClas = targetClas;

            return true;
        }
    }
}
