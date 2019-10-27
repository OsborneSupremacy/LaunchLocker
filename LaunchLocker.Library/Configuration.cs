using System;
using System.IO;

namespace LaunchLocker.Library
{
    public class Configuration
    {
        public string[] Args { get; set; }

        public Configuration(string[] args)
        {
            Args = args;
        }

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

            var targetFileInfo = new FileInfo(targetFileName);

            if(!targetFileInfo.Exists)
            {
                message = $"File `{targetFileInfo}` not found";
                return false;
            }

            return true;
        }
    }
}
