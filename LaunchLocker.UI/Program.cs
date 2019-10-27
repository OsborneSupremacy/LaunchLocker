using System;
using System.IO.Abstractions;

namespace LaunchLocker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IFileSystem fileSystem = new FileSystem();

            var config = new Library.Configuration(fileSystem);
            if (!config.CheckIfValid(args, out string message))
                ExitWithMessage(message);

            var lockReader = new Library.LockReader(fileSystem, config);
            if(lockReader.DoesLockExist())
                ExitWithMessage("lock exists");

            ExitWithMessage("no issues");
        }

        public static void ExitWithMessage(string Message)
        {
            Console.WriteLine(Message);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(-1);
        }
    }
}
