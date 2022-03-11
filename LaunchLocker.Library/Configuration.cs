using LaunchLocker.Interface;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace LaunchLocker.Library
{
    public class Settings
    {
        public Settings()
        {
            ProblemIndicators = new List<string>();
        }

        public List<string> ProblemIndicators { get; set; }
    }

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

            if (args.Count() < 2)
            {
                message = "At least two command line arguments are required.";
                return false;
            };

            string targetFileName = args[1];

            if (string.IsNullOrEmpty(targetFileName))
            {
                message = "The second command line argument should be the file to be launched.";
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
            if (args.Length > 2)
                targetClas.AddRange(args.Skip(2)); // don't return the second CLA, since that's the target file

            TargetClas = targetClas;

            return true;
        }
    }
}
